﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpeckleCore
{
  /// <summary>
  /// A basic abstract class that all Speckle converters should implement. 
  /// Provided some convenience methods for casting POCOs to SpeckleAbstract types. 
  /// </summary>
  public abstract class Converter
  {
    public abstract IEnumerable<SpeckleObject> ToSpeckle( IEnumerable<object> _objects );
    public abstract SpeckleObject ToSpeckle( object _object );

    public abstract IEnumerable<object> ToNative( IEnumerable<SpeckleObject> _objects );
    public abstract object ToNative( SpeckleObject _object );

    public static string getBase64( object obj )
    {
      using ( System.IO.MemoryStream ms = new System.IO.MemoryStream() )
      {
        new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Serialize( ms, obj );
        return Convert.ToBase64String( ms.ToArray() );
      }
    }

    public static object getObjFromString( string base64String )
    {
      if ( base64String == null ) return null;
      byte[ ] bytes = Convert.FromBase64String( base64String );
      return getObjFromBytes( bytes );
    }

    public static object getObjFromBytes( byte[ ] bytes )
    {
      using ( System.IO.MemoryStream ms = new System.IO.MemoryStream( bytes, 0, bytes.Length ) )
      {
        ms.Write( bytes, 0, bytes.Length );
        ms.Position = 0;
        return new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter().Deserialize( ms );
      }
    }

    public static string bytesToBase64( byte[ ] arr )
    {
      return Convert.ToBase64String( arr );
    }

    public static byte[ ] base64ToBytes( string str )
    {
      return Convert.FromBase64String( str );
    }

    // https://stackoverflow.com/a/299526/3446736
    public static IEnumerable<MethodInfo> GetExtensionMethods( Assembly assembly, Type extendedType, string methodName )
    {
      var query = from type in assembly.GetTypes()
                  where type.IsSealed && !type.IsGenericType && !type.IsNested
                  from method in type.GetMethods( BindingFlags.Static
                    | BindingFlags.Public | BindingFlags.NonPublic )
                  where method.IsDefined( typeof( System.Runtime.CompilerServices.ExtensionAttribute ), false )
                  where method.GetParameters()[ 0 ].ParameterType == extendedType
                  where method.Name == methodName
                  select method;
      return query;
    }

    /// <summary>
    /// Will look for an extension method called "ToSpeckle" for this object type in all loaded assemblies (named "Speckle*Converter*"). If found, it will invoke it and return the SpeckleObject. If it can't find it, returns null.
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public static SpeckleObject TryGetSpeckleObject( object o )
    {
      List<Assembly> myAss = System.AppDomain.CurrentDomain.GetAssemblies().ToList().FindAll( s => s.FullName.Contains( "Speckle" ) && s.FullName.Contains( "Converter" ) );
      List<MethodInfo> methods = new List<MethodInfo>();
      foreach ( var ass in myAss )
        methods.AddRange( Converter.GetExtensionMethods( ass, o.GetType(), "ToSpeckle" ) );

      if ( methods.Count == 0 )
        return null;

      if ( methods.Count >= 1 )
        System.Diagnostics.Debug.WriteLine( "More ToSpeckle methods found for the same object." );

      var result = methods[ 0 ].Invoke( o, new object[ ] { o } );
      if ( result != null )
        return result as SpeckleObject;

      return null;
    }

    public static object TryGetNative( SpeckleObject o )
    {
      List<Assembly> myAss = System.AppDomain.CurrentDomain.GetAssemblies().ToList().FindAll( s => s.FullName.Contains( "Speckle" ) && s.FullName.Contains( "Converter" ) );
      List<MethodInfo> methods = new List<MethodInfo>();
      foreach ( var ass in myAss )
        methods.AddRange( Converter.GetExtensionMethods( ass, o.GetType(), "ToNative" ) );

      if ( methods.Count == 0 )
        return null;

      if ( methods.Count >= 1 )
        System.Diagnostics.Debug.WriteLine( "More ToNative methods found for the same object." );

      var result = methods[ 0 ].Invoke( o, new object[ ] { o } );
      if ( result != null )
        return result;

      return o;
    }

    /// <summary>
    /// Tries to cast an object back to its native type if the assembly it belongs to is present.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static object FromAbstract( SpeckleAbstract obj, object root = null )
    {
      if ( obj._Type == "ref" )
        return null;

      var assembly = System.AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault( a => a.FullName == obj._Assembly );

      if ( assembly == null ) // we can't deserialise for sure
        return obj;

      var type = assembly.GetTypes().FirstOrDefault( t => t.Name == obj._Type );
      if ( type == null ) // something wrong in the type
        return obj;

      object myObject = null;
      try
      {
        myObject = Activator.CreateInstance( type );
      }
      catch ( Exception e )
      {
        myObject = System.Runtime.Serialization.FormatterServices.GetUninitializedObject( type );
      }

      if ( root == null )
        root = myObject;


      var keys = obj.Properties.Keys;
      foreach ( string key in keys )
      {
        var prop = type.GetProperty( key );

        if ( prop == null ) continue;

        if ( obj.Properties[ key ] == null ) continue;

        var value = ReadValue( obj.Properties[ key ], root );

        // handles both hashsets and lists or whatevers
        if ( value is IEnumerable<object> )
        {
          var mySubList = Activator.CreateInstance( prop.PropertyType );
          foreach ( var myObj in ( ( IEnumerable<object> ) value ) )
            mySubList.GetType().GetMethod( "Add" ).Invoke( mySubList, new object[ ] { myObj } );

          value = mySubList;
        }

        // guids are a pain
        if ( prop.PropertyType == typeof( Guid ) ) value = new Guid( ( string ) value );

        // take care with enums
        if ( prop.SetMethod != null )
          try
          {
            prop.SetValue( myObject, Convert.ChangeType( value, prop.PropertyType ) );
          }
          catch ( Exception e )
          {
            try
            {
              prop.SetValue( myObject, value );
            }
            catch { }
          }

      }

      //  we done yet?
      if ( root == myObject )
      {
        Converter.ResolveRefs( obj, myObject, "root" );
        //Converter.SetReferences( obj, root, root );
      }
      return myObject;
    }

    public static object ReadValue( object myObject, object root = null )
    {
      if ( myObject == null ) return null;

      if ( myObject.GetType().IsPrimitive || myObject.GetType() == typeof( string ) )
        return myObject;

      if ( myObject is SpeckleAbstract )
        return Converter.FromAbstract( ( SpeckleAbstract ) myObject, root );

      if ( myObject is SpeckleObject )
        return Converter.TryGetNative( ( SpeckleObject ) myObject );

      if ( myObject is IEnumerable<object> )
        return ( ( IEnumerable<object> ) myObject ).Select( o => ReadValue( o, root ) ).ToList();

      if ( myObject is IDictionary<string, object> )
        return ( ( IDictionary<string, object> ) myObject ).Select( kvp => new KeyValuePair<string, object>( kvp.Key, ReadValue( kvp.Value, root ) ) ).ToDictionary( kvp => kvp.Key, kvp => kvp.Value );

      return null;
    }


    private static void ResolveRefs( object original, object root, string currentPath )
    {
      if ( original is SpeckleAbstract )
      {
        // if original is SpkAbstract ref -> set ref(root, whereToSet = currentPath, fromWhere = original._ref
        //else iterateBithc
        SpeckleAbstract myObj = ( SpeckleAbstract ) original;
        if ( myObj._Type == "ref" )
          Converter.LinkRef( root, myObj._Ref, currentPath );
        else
          foreach ( var key in myObj.Properties.Keys )
            Converter.ResolveRefs( myObj.Properties[ key ], root, currentPath + "/" + key );
      }
      if ( original is Dictionary<string, object> )
      {
        Dictionary<string, object> myDict = ( Dictionary<string, object> ) original;
        foreach ( string key in myDict.Keys )
          Converter.ResolveRefs( myDict[ key ], root, currentPath + "/{" + key + "}" );
      }
      if ( original is List<object> )
      {
        List<object> myList = ( List<object> ) original; int index = 0;
        foreach ( object obj in myList )
          Converter.ResolveRefs( obj, root, currentPath + "/[" + index++ + "]" );
      }
    }

    private static void LinkRef( object target, string fromWhere, string toWhere )
    {
      var sourceAddress = fromWhere.Split( '/' );
      var targetAddress = toWhere.Split( '/' );

      object propSource = target;
      foreach ( string s in sourceAddress )
      {
        if ( s == "root" ) continue;
        if ( s.Contains( "{" ) ) // special handler for dicts
        {
          propSource = ( ( Dictionary<string, object> ) propSource )[ s.Substring( 1, s.Length - 2 ) ];
          continue;
        }
        if ( s.Contains( "[" ) ) // special handler for lists
        {
          propSource = ( ( IEnumerable<object> ) propSource ).ToList()[ int.Parse( s.Substring( 1, s.Length - 2 ) ) ];
          continue;
        }

        propSource = propSource.GetType().GetProperty( s ).GetValue( target );
      }
      var ccc = propSource;

      object propTarget = target;
      for ( int i = 1; i < targetAddress.Length - 1; i++ )
      {
        var s = targetAddress[ i ];

        if ( s == "root" ) continue;
        if ( s.Contains( "{" ) ) // special handler for dicts
        {
          propTarget = ( ( Dictionary<string, object> ) propTarget )[ s.Substring( 1, s.Length - 2 ) ];
          continue;
        }

        if ( s.Contains( "[" ) ) // special handler for lists
        {
          propTarget = ( ( IList<object> ) propTarget )[ int.Parse( s.Substring( 1, s.Length - 2 ) ) ];
          continue;
        }

        propTarget = propTarget.GetType().GetProperty( s ).GetValue( target );
      }

      var last = targetAddress.Last();

      if ( last.Contains( '{' ) )
      {
        ( ( Dictionary<string, object> ) propTarget )[ last.Substring( 1, last.Length - 2 ) ] = propSource;
        return;
      }
      if ( last.Contains( '[' ) )
      {
        ( ( IList<object> ) propTarget )[ int.Parse( last.Substring( 1, last.Length - 2 ) ) ] = propSource;
        return;
      }

      PropertyInfo toSet = propTarget.GetType().GetProperty( last );
      toSet.SetValue( propTarget, propSource, null );
    }

    /// <summary>
    /// Tries to cast a POCO to a SpeckleAbstract object. The type needs to have public properties with get and set methods.
    /// </summary>
    /// <param name="source"></param>
    /// <param name="recursionDepth"></param>
    /// <returns></returns>
    public static SpeckleObject ToAbstract( object source, int recursionDepth = 0, Dictionary<int, string> traversed = null, string path = "" )
    {
      if ( source == null ) return new SpeckleNull();

      if ( traversed == null ) traversed = new Dictionary<int, string>();

      if ( path == "" ) path = "root";

      if ( traversed.ContainsKey( source.GetHashCode() ) )
        return new SpeckleAbstract() { _Type = "ref", _Ref = traversed[ source.GetHashCode() ] };
      else
        traversed.Add( source.GetHashCode(), path );

      var spk = Converter.TryGetSpeckleObject( source );
      if ( spk != null )
        return spk;

      SpeckleAbstract result = new SpeckleAbstract();
      result._Type = source.GetType().Name;
      result._Assembly = source.GetType().Assembly.FullName;

      Dictionary<string, object> dict = new Dictionary<string, object>();

      var properties = source.GetType().GetProperties( BindingFlags.Instance | BindingFlags.Public );

      foreach ( var prop in properties )
      {
        try
        {
          var value = prop.GetValue( source );
          dict[ prop.Name ] = WriteValue( value, recursionDepth, traversed, path + "/" + prop.Name );
        }
        catch ( Exception e )
        {
          var copy = e;
        }
      }


      result.Properties = dict;
      result.SetHashes( result );

      return result;
    }

    public static object WriteValue( object myObject, int recursionDepth, Dictionary<int, string> traversed = null, string path = "" )
    {
      if ( myObject == null || recursionDepth > 8 ) return null;
      if ( myObject is Enum ) return Convert.ChangeType( ( Enum ) myObject, ( ( Enum ) myObject ).GetTypeCode() );

      if ( myObject.GetType().IsPrimitive || myObject is string )
        return myObject;

      if ( myObject is IEnumerable<object> )
        return ( ( IEnumerable<object> ) myObject ).Select( ( o, index ) => WriteValue( o, recursionDepth + 1, traversed, path + "/[" + index + "]" ) ).ToList();


      if ( myObject is IDictionary<string, object> )
        return ( ( IDictionary<string, object> ) myObject ).Select( kvp => new KeyValuePair<string, object>( kvp.Key, WriteValue( kvp.Value, recursionDepth, traversed, path + "/{" + kvp.Key + "}" ) ) ).ToDictionary( kvp => kvp.Key, kvp => kvp.Value );

      if ( !myObject.GetType().AssemblyQualifiedName.Contains( "System" ) )
        return Converter.ToAbstract( myObject, recursionDepth + 1, traversed, path );

      return myObject.ToString();
    }

  }
}
