// Decompiled with JetBrains decompiler
// Type: Monnit.SensorGroup
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SensorGroup")]
public class SensorGroup : BaseDBObject
{
  private long _SensorGroupID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _Name = string.Empty;
  private string _Location = string.Empty;
  private string _Size = string.Empty;
  private long _ParentID = long.MinValue;
  private string _Type = string.Empty;
  private string _TagString = string.Empty;
  private List<SensorGroupSensor> _Sensors = (List<SensorGroupSensor>) null;

  [DBProp("SensorGroupID", IsPrimaryKey = true)]
  public long SensorGroupID
  {
    get => this._SensorGroupID;
    set => this._SensorGroupID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true, International = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Location", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Location
  {
    get => this._Location;
    set => this._Location = value;
  }

  [DBProp("Size", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Size
  {
    get => this._Size;
    set => this._Size = value;
  }

  [DBProp("ParentID")]
  public long ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
  }

  [DBProp("Type", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  public string[] Tags
  {
    get => this.TagString.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
  }

  public void AddTag(string tag)
  {
    this.TagString = $"{this.TagString}|{tag}";
    this.Save();
  }

  public void RemoveTag(string tag)
  {
    string[] tags = this.Tags;
    this.TagString = string.Empty;
    foreach (string str in tags)
    {
      if (str.ToLower().Trim() != tag.ToLower().Trim())
        this.TagString = $"{this.TagString}|{str}";
    }
    this.Save();
  }

  [DBProp("TagString", MaxLength = 2000, AllowNull = true)]
  public string TagString
  {
    get => this._TagString;
    set => this._TagString = value;
  }

  public bool HasTag(string tag)
  {
    foreach (string tag1 in this.Tags)
    {
      if (tag1.ToLower().Trim() == tag.ToLower().Trim())
        return true;
    }
    return false;
  }

  public List<SensorGroupSensor> Sensors
  {
    get
    {
      if (this._Sensors == null)
        this._Sensors = SensorGroup.LoadSensors(this.SensorGroupID);
      return this._Sensors;
    }
  }

  public void AddSensor(long sensorId, int position, int datumIndex)
  {
    SensorGroupSensor sensorGroupSensor = new SensorGroupSensor();
    sensorGroupSensor.SensorGroupID = this.SensorGroupID;
    sensorGroupSensor.SensorID = sensorId;
    sensorGroupSensor.Position = position;
    sensorGroupSensor.DatumIndex = datumIndex;
    sensorGroupSensor.Save();
    this.Sensors.Add(sensorGroupSensor);
  }

  public void AddSensorByDatum(long sensorId, int datumIndex, string alias, bool saveSgs)
  {
    SensorGroupSensor sensorGroupSensor = new SensorGroupSensor();
    sensorGroupSensor.SensorGroupID = this.SensorGroupID;
    sensorGroupSensor.SensorID = sensorId;
    sensorGroupSensor.Alias = alias;
    sensorGroupSensor.DatumIndex = datumIndex;
    if (saveSgs)
      sensorGroupSensor.Save();
    this.Sensors.Add(sensorGroupSensor);
  }

  public void AddSensor(long sensorId, int position)
  {
    SensorGroupSensor sensorGroupSensor = new SensorGroupSensor();
    sensorGroupSensor.SensorGroupID = this.SensorGroupID;
    sensorGroupSensor.SensorID = sensorId;
    sensorGroupSensor.Position = position;
    sensorGroupSensor.Save();
    this.Sensors.Add(sensorGroupSensor);
  }

  public void AddSensor(long sensorId)
  {
    SensorGroupSensor sensorGroupSensor = new SensorGroupSensor();
    sensorGroupSensor.SensorGroupID = this.SensorGroupID;
    sensorGroupSensor.SensorID = sensorId;
    sensorGroupSensor.Save();
    this.Sensors.Add(sensorGroupSensor);
  }

  public void RemoveSensor(long sensorId, int datumIndex)
  {
    int index = 0;
    foreach (SensorGroupSensor sensor in this.Sensors)
    {
      if (sensor.SensorID == sensorId && sensor.DatumIndex == datumIndex)
      {
        this.Sensors.RemoveAt(index);
        return;
      }
      ++index;
    }
    this._Sensors = (List<SensorGroupSensor>) null;
  }

  public void RemoveSensor(long sensorId)
  {
    foreach (SensorGroupSensor sensor in this.Sensors)
    {
      if (sensor.SensorID == sensorId)
        sensor.Delete();
    }
    this._Sensors = (List<SensorGroupSensor>) null;
  }

  public static SensorGroup Load(long id) => BaseDBObject.Load<SensorGroup>(id);

  public static List<SensorGroupSensor> LoadSensors(long sensorGroupId)
  {
    return new Monnit.Data.SensorGroup.LoadSensors(sensorGroupId).Result;
  }

  public static void RemoveSensors(long sensorGroupID)
  {
    Monnit.Data.SensorGroup.RemoveSensors removeSensors = new Monnit.Data.SensorGroup.RemoveSensors(sensorGroupID);
  }

  public static List<SensorGroup> LoadByType(string type)
  {
    return new Monnit.Data.SensorGroup.LoadByType(type).Result;
  }

  public static List<SensorGroup> LoadByAccountIDandType(long accountID, string type)
  {
    return new Monnit.Data.SensorGroup.LoadByAccountIDandType(accountID, type).Result;
  }

  public static List<SensorGroup> LoadByAccount(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<SensorGroup>("AccountID", (object) accountID);
  }
}
