// Decompiled with JetBrains decompiler
// Type: Monnit.APIKey
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("APIKey")]
public class APIKey : BaseDBObject
{
  private long _APIKeyID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _Name = string.Empty;
  private byte[] _Salt = new byte[24];
  private int _WorkFactor = 0;
  private string _KeyValue = string.Empty;
  private byte[] _HashedSecret = new byte[24];
  private DateTime _LastUsedDate = DateTime.MinValue;
  private long _CustomerID = long.MinValue;

  [DBProp("APIKeyID", IsPrimaryKey = true)]
  public long APIKeyID
  {
    get => this._APIKeyID;
    set => this._APIKeyID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Name", MaxLength = 50, International = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Salt", MaxLength = 24, AllowNull = false)]
  public byte[] Salt
  {
    get => this._Salt;
    set => this._Salt = value;
  }

  [DBProp("WorkFactor", AllowNull = false)]
  public int WorkFactor
  {
    get => this._WorkFactor;
    set => this._WorkFactor = value;
  }

  [DBProp("KeyValue", MaxLength = 16 /*0x10*/, AllowNull = false)]
  public string KeyValue
  {
    get => this._KeyValue;
    set => this._KeyValue = value;
  }

  [DBProp("HashedSecret", MaxLength = 24, AllowNull = false)]
  public byte[] HashedSecret
  {
    get => this._HashedSecret;
    set => this._HashedSecret = value;
  }

  [DBProp("LastUsedDate")]
  public DateTime LastUsedDate
  {
    get => this._LastUsedDate;
    set => this._LastUsedDate = value;
  }

  [DBProp("CustomerID", AllowNull = false)]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  public static APIKey Load(long id) => BaseDBObject.Load<APIKey>(id);

  public static List<APIKey> LoadByAccount(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<APIKey>("AccountID", (object) accountID);
  }

  public static List<APIKey> LoadByKeyValue(string keyValue)
  {
    return new Monnit.Data.APIKey.LoadByKeyValue(keyValue).Result;
  }
}
