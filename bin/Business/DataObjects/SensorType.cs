// Decompiled with JetBrains decompiler
// Type: Monnit.SensorType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SensorType")]
public class SensorType : BaseDBObject
{
  private long _SensorTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _LatestFirmwareVersion = string.Empty;
  private string _LatestFirmwarePathFormat = string.Empty;
  private eWitType _WitType = eWitType.Wit;

  [DBProp("SensorTypeID", IsPrimaryKey = true)]
  public long SensorTypeID
  {
    get => this._SensorTypeID;
    set => this._SensorTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  [DBProp("LatestFirmwareVersion", MaxLength = 64 /*0x40*/)]
  public string LatestFirmwareVersion
  {
    get => this._LatestFirmwareVersion;
    set => this._LatestFirmwareVersion = value;
  }

  [DBProp("LatestFirmwarePathFormat", MaxLength = 255 /*0xFF*/)]
  public string LatestFirmwarePathFormat
  {
    get => this._LatestFirmwarePathFormat;
    set => this._LatestFirmwarePathFormat = value;
  }

  [DBProp("eWitType", DefaultValue = 1)]
  public eWitType WitType
  {
    get => this._WitType;
    set => this._WitType = value;
  }

  public string LatestFirmwarePath(long applicationID)
  {
    return string.Format(this.LatestFirmwarePathFormat, (object) applicationID);
  }

  public int OTAChunkSize => 1024 /*0x0400*/;

  public static SensorType Load(long ID)
  {
    string key = $"SensorType_{ID}";
    SensorType sensorType = TimedCache.RetrieveObject<SensorType>(key);
    if (sensorType == null)
    {
      sensorType = BaseDBObject.Load<SensorType>(ID);
      if (sensorType == null)
        sensorType = new SensorType() { Name = "Undefined" };
      TimedCache.AddObjectToCach(key, (object) sensorType);
    }
    return sensorType;
  }

  public static List<SensorType> LoadAll() => BaseDBObject.LoadAll<SensorType>();
}
