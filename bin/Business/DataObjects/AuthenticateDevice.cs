// Decompiled with JetBrains decompiler
// Type: Monnit.AuthenticateDevice
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AuthenticateDevice")]
public class AuthenticateDevice : BaseDBObject
{
  private long _AuthenticateDeviceID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private string _DeviceName = string.Empty;
  private string _DisplayName = string.Empty;
  private byte[] _DeviceHash = new byte[24];
  private byte[] _Salt = new byte[24];
  private int _WorkFactor = 0;
  private DateTime _CreateDate = DateTime.MinValue;
  private DateTime _LastLoginDate = DateTime.MinValue;

  [DBProp("AuthenticateDeviceID", IsPrimaryKey = true)]
  public long AuthenticateDeviceID
  {
    get => this._AuthenticateDeviceID;
    set => this._AuthenticateDeviceID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("DeviceName", AllowNull = false, MaxLength = 255 /*0xFF*/, International = true)]
  public string DeviceName
  {
    get => this._DeviceName;
    set
    {
      if (value == null)
        this._DeviceName = string.Empty;
      else
        this._DeviceName = value;
    }
  }

  [DBProp("DisplayName", AllowNull = false, MaxLength = 255 /*0xFF*/, International = true)]
  public string DisplayName
  {
    get => this._DisplayName;
    set
    {
      if (value == null)
        this._DisplayName = string.Empty;
      else
        this._DisplayName = value;
    }
  }

  [DBProp("DeviceHash", MaxLength = 24, AllowNull = false)]
  public byte[] DeviceHash
  {
    get => this._DeviceHash;
    set => this._DeviceHash = value;
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

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("LastLoginDate")]
  public DateTime LastLoginDate
  {
    get => this._LastLoginDate;
    set => this._LastLoginDate = value;
  }

  public static AuthenticateDevice Load(long id) => BaseDBObject.Load<AuthenticateDevice>(id);

  public static List<AuthenticateDevice> LoadByCustomerID(long customerID)
  {
    return new Monnit.Data.AuthenticateDevice.LoadByCustomerID(customerID).Result;
  }
}
