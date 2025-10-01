// Decompiled with JetBrains decompiler
// Type: Monnit.SensorFirmware
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SensorFirmware")]
public class SensorFirmware : BaseDBObject
{
  private long _SensorFirmwareID = long.MinValue;
  private long _MonnitApplicationID = long.MinValue;
  private string _FirmwareVersion = string.Empty;
  private string _FirmwarePath = string.Empty;
  private int _FirmwareSize = int.MinValue;
  private int _Flags = int.MinValue;

  [DBProp("SensorFirmwareID", IsPrimaryKey = true)]
  public long SensorFirmwareID
  {
    get => this._SensorFirmwareID;
    set => this._SensorFirmwareID = value;
  }

  [DBProp("ApplicationID")]
  public long MonnitApplicationID
  {
    get => this._MonnitApplicationID;
    set => this._MonnitApplicationID = value;
  }

  public long ApplicationID
  {
    get => this.MonnitApplicationID;
    set => this.MonnitApplicationID = value;
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

  public static List<SensorFirmware> LoadAll()
  {
    string key = "SensorFIleList";
    List<SensorFirmware> sensorFirmwareList = TimedCache.RetrieveObject<List<SensorFirmware>>(key);
    if (sensorFirmwareList == null)
    {
      sensorFirmwareList = BaseDBObject.LoadAll<SensorFirmware>();
      if (sensorFirmwareList != null)
        TimedCache.AddObjectToCach(key, (object) sensorFirmwareList, new TimeSpan(6, 0, 0));
    }
    return sensorFirmwareList;
  }

  public static SensorFirmware Load(long sensorFirmwareID)
  {
    return BaseDBObject.Load<SensorFirmware>(sensorFirmwareID);
  }
}
