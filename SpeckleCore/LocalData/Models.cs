﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SpeckleCore
{
  /// <summary>
  /// A class for a generic speckle account, composed of all the identification props for a user & server combination. 
  /// </summary>
  public class Account
  {
    [PrimaryKey, AutoIncrement]
    public int AccountId { get; set; }

    public string ServerName { get; set; }

    [Indexed]
    public string RestApi { get; set; }

    [Indexed]
    public string Email { get; set; }

    public string Token { get; set; }

    public bool IsDefault { get; set; } = false;
  }

  /// <summary>
  /// A class for storing cached objects (that have been retrieved from a database).
  /// </summary>
  public class CachedObject
  {
    [PrimaryKey, Indexed]
    public string Hash { get; set; }

    [Indexed]
    public string RestApi { get; set; }

    public byte[ ] Bytes { get; set; }
  }
}
