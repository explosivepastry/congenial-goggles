// Decompiled with JetBrains decompiler
// Type: Monnit.CalibrationFacility
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("CalibrationFacility")]
public class CalibrationFacility : BaseDBObject
{
  private long _CalibrationFacilityID = long.MinValue;
  private string _Name = string.Empty;
  private string _CertificationPath = string.Empty;

  [DBProp("CalibrationFacilityID", IsPrimaryKey = true)]
  public long CalibrationFacilityID
  {
    get => this._CalibrationFacilityID;
    set => this._CalibrationFacilityID = value;
  }

  [DBProp("Name", AllowNull = false)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("CertificationPath", AllowNull = true)]
  public string CertificationPath
  {
    get => this._CertificationPath;
    set => this._CertificationPath = value;
  }

  public static CalibrationFacility Load(long ID)
  {
    return CalibrationFacility.LoadAll().FirstOrDefault<CalibrationFacility>((Func<CalibrationFacility, bool>) (obj => obj.CalibrationFacilityID == ID));
  }

  public static List<CalibrationFacility> LoadAll()
  {
    string key = "CalibrationFacilityList";
    List<CalibrationFacility> calibrationFacilityList = TimedCache.RetrieveObject<List<CalibrationFacility>>(key);
    if (calibrationFacilityList == null)
    {
      calibrationFacilityList = BaseDBObject.LoadAll<CalibrationFacility>();
      if (calibrationFacilityList != null)
        TimedCache.AddObjectToCach(key, (object) calibrationFacilityList, new TimeSpan(6, 0, 0));
    }
    return calibrationFacilityList;
  }

  public static CalibrationFacility LoadByName(string name)
  {
    return new Monnit.Data.CalibrationFacility.LoadByName(name).Result;
  }
}
