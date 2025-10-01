// Decompiled with JetBrains decompiler
// Type: Monnit.SensorAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("SensorAttribute")]
public class SensorAttribute : BaseDBObject
{
  private long _SensorAttributeID = long.MinValue;
  private long _SensorID = long.MinValue;
  private string _Name = string.Empty;
  private string _Value = string.Empty;

  [DBProp("SensorAttributeID", IsPrimaryKey = true)]
  public long SensorAttributeID
  {
    get => this._SensorAttributeID;
    set => this._SensorAttributeID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID", AllowNull = false)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, International = true)]
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

  [DBProp("Value", MaxLength = 255 /*0xFF*/, International = true)]
  public string Value
  {
    get => this._Value;
    set
    {
      if (value == null)
        this._Value = string.Empty;
      else
        this._Value = value;
    }
  }

  public static SensorAttribute Load(long ID) => BaseDBObject.Load<SensorAttribute>(ID);

  public static List<SensorAttribute> LoadBySensorID(long sensorID)
  {
    string key = $"SensorAttribute{sensorID}";
    List<SensorAttribute> sensorAttributeList = TimedCache.RetrieveObject<List<SensorAttribute>>(key);
    if (sensorAttributeList == null)
    {
      sensorAttributeList = BaseDBObject.LoadByForeignKey<SensorAttribute>("SensorID", (object) sensorID);
      if (sensorAttributeList != null)
        TimedCache.AddObjectToCach(key, (object) sensorAttributeList, new TimeSpan(1, 0, 0));
    }
    return sensorAttributeList;
  }

  public static void CacheByAccount(long accountID)
  {
    List<Sensor> sensorList = Sensor.LoadByAccountID(accountID);
    List<SensorAttribute> result = new Monnit.Data.SensorAttribute.LoadAllByAccountID(accountID).Result;
    foreach (Sensor sensor in sensorList)
    {
      Sensor sens = sensor;
      string key = $"SensorAttribute{sens.SensorID}";
      if (TimedCache.RetrieveObject<List<SensorAttribute>>(key) == null)
      {
        List<SensorAttribute> list = result.Where<SensorAttribute>((Func<SensorAttribute, bool>) (c => c.SensorID == sens.SensorID)).ToList<SensorAttribute>();
        if (list != null)
          TimedCache.AddObjectToCach(key, (object) list, new TimeSpan(1, 0, 0));
      }
    }
  }

  public static void ResetAttributeList(long sensorID)
  {
    try
    {
      TimedCache.RemoveObject($"SensorAttribute{sensorID}");
    }
    catch
    {
    }
  }

  public static void SetDatumName(long sid, int DatumIndex, string name)
  {
    SensorAttribute sensorAttribute = new Monnit.Data.SensorAttribute.LoadByDatum(sid, DatumIndex).Result ?? new SensorAttribute();
    sensorAttribute.Name = "DatumIndex|" + DatumIndex.ToString();
    sensorAttribute.Value = name;
    sensorAttribute.SensorID = sid;
    sensorAttribute.Save();
    SensorAttribute.ResetAttributeList(sid);
  }

  public static string GetDatumName(long sensorID, int datumIndex)
  {
    SensorAttribute sensorAttribute = SensorAttribute.LoadBySensorID(sensorID).Where<SensorAttribute>((Func<SensorAttribute, bool>) (c => c.Name == "DatumIndex|" + datumIndex.ToString())).FirstOrDefault<SensorAttribute>();
    return sensorAttribute == null ? (string) null : sensorAttribute.Value;
  }

  public static int GetQueueID(long sensorID)
  {
    SensorAttribute.ResetAttributeList(sensorID);
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
        return sensorAttribute.Value.ToInt();
    }
    return 1;
  }

  public static int SetQueueID(long sensorID)
  {
    int queueID = 1;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
        queueID = sensorAttribute.Value.ToInt() + 1;
    }
    return SensorAttribute.SetQueueID(sensorID, queueID);
  }

  public static int SetQueueID(long sensorID, int queueID)
  {
    bool flag = false;
    if (queueID > 254)
      queueID = 1;
    if (queueID < 1)
      queueID = 1;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "QueueID")
      {
        sensorAttribute.Value = queueID.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "QueueID",
        Value = queueID.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
    return queueID;
  }

  public class SensorScaleBadge
  {
    public long SensorID { get; set; }

    public string BadgeText { get; set; }

    public string Attribute1 { get; set; }

    public string Attribute2 { get; set; }

    public string Label { get; set; }
  }
}
