// Decompiled with JetBrains decompiler
// Type: Monnit.MonnitApplication
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("Application")]
public class MonnitApplication : BaseDBObject
{
  private long _MonnitApplicationID = long.MinValue;
  private string _ApplicationName = string.Empty;
  private eApplicationProfileType _IsTriggerProfile = eApplicationProfileType.Interval;
  private bool _HasDefaultRanges = false;
  private long _AccountThemeID = long.MinValue;
  private long _SensorFirmwareID = long.MinValue;
  private static List<MonnitApplication> _List;

  [DBProp("ApplicationID", IsPrimaryKey = true)]
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

  [DBProp("ApplicationName", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string ApplicationName
  {
    get => this._ApplicationName;
    set
    {
      if (value == null)
        this._ApplicationName = string.Empty;
      else
        this._ApplicationName = value;
    }
  }

  [DBProp("IsTriggerProfile", AllowNull = true)]
  public eApplicationProfileType IsTriggerProfile
  {
    get => this._IsTriggerProfile;
    set => this._IsTriggerProfile = value;
  }

  [DBProp("HasDefaultRanges", AllowNull = true)]
  public bool HasDefaultRanges
  {
    get => this._HasDefaultRanges;
    set => this._HasDefaultRanges = value;
  }

  [DBForeignKey("AccountTheme", "AccountThemeID")]
  [DBProp("AccountThemeID", AllowNull = true)]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("SensorFirmwareID")]
  [DBForeignKey("SensorFirmware", "SensorFirmwareID")]
  public long SensorFirmwareID
  {
    get => this._SensorFirmwareID;
    set => this._SensorFirmwareID = value;
  }

  public static MonnitApplication Load(long ID)
  {
    foreach (MonnitApplication monnitApplication in MonnitApplication.LoadAll())
    {
      if (monnitApplication.ApplicationID == ID)
        return monnitApplication;
    }
    return BaseDBObject.Load<MonnitApplication>(ID);
  }

  public static List<MonnitApplication> LoadAll()
  {
    if (MonnitApplication._List == null)
      MonnitApplication._List = BaseDBObject.LoadAll<MonnitApplication>();
    return MonnitApplication._List;
  }

  public static List<MonnitApplication> LoadByAccountThemeID(long accountThemeID)
  {
    List<MonnitApplication> result = new Monnit.Data.MonnitApplication.LoadByThemeID(accountThemeID).Result;
    return result.Count > 0 ? result : MonnitApplication.LoadAll();
  }

  public static List<MonnitApplication> LoadByAccountID(long accountID)
  {
    return Sensor.LoadByAccountID(accountID).GroupBy<Sensor, long>((Func<Sensor, long>) (s => s.ApplicationID)).Select<IGrouping<long, Sensor>, MonnitApplication>((Func<IGrouping<long, Sensor>, MonnitApplication>) (g => MonnitApplication.Load(g.Key))).OrderBy<MonnitApplication, string>((Func<MonnitApplication, string>) (s => s.ApplicationName)).ToList<MonnitApplication>();
  }

  public static List<MonnitApplication> LoadByCSNetID(long csnetID)
  {
    return BaseDBObject.Load<Sensor>(Sensor.LoadTableByCsNetID(csnetID)).GroupBy<Sensor, long>((Func<Sensor, long>) (s => s.ApplicationID)).Select<IGrouping<long, Sensor>, MonnitApplication>((Func<IGrouping<long, Sensor>, MonnitApplication>) (g => MonnitApplication.Load(g.Key))).OrderBy<MonnitApplication, string>((Func<MonnitApplication, string>) (s => s.ApplicationName)).ToList<MonnitApplication>();
  }
}
