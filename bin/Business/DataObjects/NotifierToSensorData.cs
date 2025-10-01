// Decompiled with JetBrains decompiler
// Type: Monnit.NotifierToSensorData
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("NotifierToSensorData")]
public class NotifierToSensorData : BaseDBObject
{
  private long _NotifierToSensorID = long.MinValue;
  private long _NotifierID = long.MinValue;
  private long _SensorID = long.MinValue;
  private List<Sensor> _SensorsThatSendData = (List<Sensor>) null;

  [DBProp("NotifierToSensorID", IsPrimaryKey = true)]
  public long NotifierToSensorID
  {
    get => this._NotifierToSensorID;
    set => this._NotifierToSensorID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("NotifierID ", AllowNull = false)]
  public long NotifierID
  {
    get => this._NotifierID;
    set => this._NotifierID = value;
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID ", AllowNull = false)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  public List<Sensor> SensorsThatSendData
  {
    get
    {
      if (this._SensorsThatSendData == null)
      {
        foreach (NotifierToSensorData notifierToSensorData in NotifierToSensorData.GetAllSensorByNotifier(this.NotifierID))
          this._SensorsThatSendData.Add(Sensor.Load(notifierToSensorData.SensorID));
      }
      return this._SensorsThatSendData;
    }
  }

  public static NotifierToSensorData Load(long ID) => BaseDBObject.Load<NotifierToSensorData>(ID);

  public static List<NotifierToSensorData> LoadAll()
  {
    return BaseDBObject.LoadAll<NotifierToSensorData>().ToList<NotifierToSensorData>();
  }

  public static List<NotifierToSensorData> GetAllNotifiersBySensorID(long sensorID)
  {
    return BaseDBObject.LoadByForeignKey<NotifierToSensorData>("SensorID", (object) sensorID);
  }

  public static List<NotifierToSensorData> GetAllSensorByNotifier(long notifierID)
  {
    return BaseDBObject.LoadByForeignKey<NotifierToSensorData>("NotifierID", (object) notifierID);
  }

  public static List<NotifierToSensorData> LoadBySensorID(long sensorID)
  {
    return BaseDBObject.LoadByForeignKey<NotifierToSensorData>("SensorID", (object) sensorID);
  }
}
