// Decompiled with JetBrains decompiler
// Type: Monnit.BSNFirmware
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("BSNFirmware")]
public class BSNFirmware : BaseDBObject
{
  private long _BSNFirmwareID = long.MinValue;
  private long _GatewayTypeID = long.MinValue;
  private string _FirmwareVersion = string.Empty;
  private string _FirmwarePath = string.Empty;
  private int _FirmwareSize = int.MinValue;
  private int _Flags = int.MinValue;

  [DBProp("BSNFirmwareID", IsPrimaryKey = true)]
  public long BSNFirmwareID
  {
    get => this._BSNFirmwareID;
    set => this._BSNFirmwareID = value;
  }

  [DBProp("GatewayTypeID")]
  public long GatewayTypeID
  {
    get => this._GatewayTypeID;
    set => this._GatewayTypeID = value;
  }

  [DBProp("FirmwareVersion", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string FirmwareVersion
  {
    get => this._FirmwareVersion;
    set => this._FirmwareVersion = value;
  }

  [DBProp("FirmwarePath", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string FirmwarePath
  {
    get => this._FirmwarePath;
    set => this._FirmwarePath = value;
  }

  [DBProp("FirmwareSize")]
  public int FirmwareSize
  {
    get => this._FirmwareSize;
    set => this._FirmwareSize = value;
  }

  [DBProp("Flags")]
  public int Flags
  {
    get => this._Flags;
    set => this._Flags = value;
  }

  public static List<BSNFirmware> LoadAll()
  {
    string key = "GatewayFileList";
    List<BSNFirmware> bsnFirmwareList = TimedCache.RetrieveObject<List<BSNFirmware>>(key);
    if (bsnFirmwareList == null)
    {
      bsnFirmwareList = BaseDBObject.LoadAll<BSNFirmware>();
      if (bsnFirmwareList != null)
        TimedCache.AddObjectToCach(key, (object) bsnFirmwareList, new TimeSpan(6, 0, 0));
    }
    return bsnFirmwareList;
  }

  public static BSNFirmware Load(long bsnFirmwareID)
  {
    return BaseDBObject.Load<BSNFirmware>(bsnFirmwareID);
  }
}
