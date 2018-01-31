﻿using System.Collections.Generic;

namespace SpeckleCore
{

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseBase
    {
        /// <summary>Besides the http status code, this tells you whether the call succeeded or not.</summary>
        [Newtonsoft.Json.JsonProperty("success", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool? Success { get; set; }

        /// <summary>Either an error or a confirmation.</summary>
        [Newtonsoft.Json.JsonProperty("message", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Message { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static ResponseBase FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseBase>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseAccountRegister : ResponseBase
    {
        /// <summary>Session token, expires in 1 day.</summary>
        [Newtonsoft.Json.JsonProperty("token", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Token { get; set; }

        /// <summary>API token, expires in 1 year.</summary>
        [Newtonsoft.Json.JsonProperty("apiToken", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ApiToken { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseAccountRegister FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseAccountRegister>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseAccountLogin : ResponseBase
    {
        /// <summary>Session token, expires in 1 day.</summary>
        [Newtonsoft.Json.JsonProperty("token", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Token { get; set; }

        /// <summary>API token, expires in 1 year.</summary>
        [Newtonsoft.Json.JsonProperty("apiToken", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ApiToken { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseAccountLogin FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseAccountLogin>(data);
        }
    }
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseAccountStreams : ResponseBase
    {
        /// <summary>The user's streams.</summary>
        [Newtonsoft.Json.JsonProperty("streams", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<DataStream> OwnedStreams { get; set; }

        /// <summary>The streams that are shared with the user.</summary>
        [Newtonsoft.Json.JsonProperty("sharedStreams", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<DataStream> SharedWithStreams { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseAccountStreams FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseAccountStreams>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseAccountClients : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("clients", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<SpeckleClient> Clients { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseAccountClients FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseAccountClients>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseAccountProfile : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("user", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public User User { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseAccountProfile FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseAccountProfile>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseClientCreate : ResponseBase
    {
        /// <summary>the client's uuid. save & serialise this!</summary>
        [Newtonsoft.Json.JsonProperty("clientId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ClientId { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseClientCreate FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseClientCreate>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseClientGet : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("client", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public SpeckleClient Client { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseClientGet FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseClientGet>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseStreamCreate : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("stream", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DataStream Stream { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseStreamCreate FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseStreamCreate>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseStreamGet : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("stream", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public DataStream Stream { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseStreamGet FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseStreamGet>(data);
        }
    }

    

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseStreamLayersGet : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("layers", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<SpeckleLayer> Layers { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseStreamLayersGet FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseStreamLayersGet>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.4.2.0")]
    public partial class ResponseStreamNameGet : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("name", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Name { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseStreamNameGet FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseStreamNameGet>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class ResponseStreamClone : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("clone", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Clone Clone { get; set; }

        [Newtonsoft.Json.JsonProperty("parent", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Parent Parent { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseStreamClone FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseStreamClone>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class ResponseStreamDiff : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("objects", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Objects Objects { get; set; }

        [Newtonsoft.Json.JsonProperty("layers", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Layers Layers { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseStreamDiff FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseStreamDiff>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class Objects
    {
        [Newtonsoft.Json.JsonProperty("common", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<string> Common { get; set; }

        [Newtonsoft.Json.JsonProperty("inA", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<string> InA { get; set; }

        [Newtonsoft.Json.JsonProperty("inB", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<string> InB { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static Objects FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Objects>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class Layers
    {
        [Newtonsoft.Json.JsonProperty("common", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<SpeckleLayer> Common { get; set; }

        [Newtonsoft.Json.JsonProperty("inA", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<SpeckleLayer> InA { get; set; }

        [Newtonsoft.Json.JsonProperty("inB", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<SpeckleLayer> InB { get; set; }

        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public static Layers FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<Layers>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class ResponseObjectGet : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("speckleObject", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public SpeckleObject SpeckleObject { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseObjectGet FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseObjectGet>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class ResponseStreamUpdate : ResponseBase
    {
        /// <summary>Ordered array of the objects databaseId (_id).</summary>
        [Newtonsoft.Json.JsonProperty("objects", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<string> Objects { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseStreamUpdate FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseStreamUpdate>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class ResponseSingleLayer : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("layer", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public SpeckleLayer Layer { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseSingleLayer FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseSingleLayer>(data);
        }
    }

    public partial class ResponseGetObjects : ResponseBase
    {
        [Newtonsoft.Json.JsonProperty("objects", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<SpeckleObject> Objects { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponseGetObjects FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseGetObjects>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class ResponsePostObjects : ResponseBase
    {
        /// <summary>Ordered array of the objects databaseId (_id).</summary>
        [Newtonsoft.Json.JsonProperty("objectIds", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public List<string> Objects { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponsePostObjects FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponsePostObjects>(data);
        }
    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "9.6.3.0")]
    public partial class ResponsePostObject : ResponseBase
    {
        /// <summary>Ordered array of the objects databaseId (_id).</summary>
        [Newtonsoft.Json.JsonProperty("objectId", Required = Newtonsoft.Json.Required.Default, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string ObjectId { get; set; }

        public new string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public new static ResponsePostObjects FromJson(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ResponsePostObjects>(data);
        }
    }
}
