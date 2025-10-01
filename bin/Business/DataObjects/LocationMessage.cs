// Decompiled with JetBrains decompiler
// Type: Monnit.LocationMessage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable disable
namespace Monnit;

[DBClass("LocationMessage")]
public class LocationMessage : BaseDBObject
{
  private Guid _LocationMessageGUID = Guid.Empty;
  private DateTime _LocationDate = DateTime.MinValue;
  private long _DeviceID = long.MinValue;
  private DateTime _InsertDate = DateTime.UtcNow;
  private int _State = 0;
  private double _Longitude = 0.0;
  private double _Latitude = 0.0;
  private int _Altitude = 0;
  private int _CourseOverGround = 0;
  private int _Speed = 0;
  private int _FixTime = 0;
  private int _NumberSatellites = 0;
  private int _Uncertainty = 0;

  [DBProp("LocationMessageGUID", IsGuidPrimaryKey = true)]
  public Guid LocationMessageGUID
  {
    get => this._LocationMessageGUID;
    set => this._LocationMessageGUID = value;
  }

  [DBProp("LocationDate", AllowNull = false)]
  public DateTime LocationDate
  {
    get => this._LocationDate;
    set => this._LocationDate = value;
  }

  [DBProp("DeviceID", AllowNull = false)]
  public long DeviceID
  {
    get => this._DeviceID;
    set => this._DeviceID = value;
  }

  [DBProp("InsertDate", AllowNull = false)]
  public DateTime InsertDate
  {
    get => this._InsertDate;
    set => this._InsertDate = value;
  }

  [DBProp("State", AllowNull = false)]
  public int State
  {
    get => this._State;
    set => this._State = value;
  }

  public bool isValid => (this.State & 16 /*0x10*/) == 0;

  [DBProp("Longitude", AllowNull = true)]
  public double Longitude
  {
    get => this._Longitude;
    set => this._Longitude = value;
  }

  [DBProp("Latitude", AllowNull = true)]
  public double Latitude
  {
    get => this._Latitude;
    set => this._Latitude = value;
  }

  [DBProp("Altitude", AllowNull = false)]
  public int Altitude
  {
    get => this._Altitude;
    set => this._Altitude = value;
  }

  [DBProp("CourseOverGround", AllowNull = false)]
  public int CourseOverGround
  {
    get => this._CourseOverGround;
    set => this._CourseOverGround = value;
  }

  [DBProp("Speed", AllowNull = false)]
  public int Speed
  {
    get => this._Speed;
    set => this._Speed = value;
  }

  [DBProp("FixTime", AllowNull = false)]
  public int FixTime
  {
    get => this._FixTime;
    set => this._FixTime = value;
  }

  [DBProp("NumberSatellites", AllowNull = false)]
  public int NumberSatellites
  {
    get => this._NumberSatellites;
    set => this._NumberSatellites = value;
  }

  [DBProp("Uncertainty", AllowNull = false)]
  public int Uncertainty
  {
    get => this._Uncertainty;
    set => this._Uncertainty = value;
  }

  public static List<LocationMessage> BulkInsert(List<LocationMessage> messageList)
  {
    if (messageList.Count == 0)
      return messageList;
    foreach (LocationMessage message in messageList)
    {
      try
      {
        message.Save();
      }
      catch (Exception ex)
      {
        ex.Log($"LocationMessage.BulkInsert/ LocationMessage LM.LocationDate: {message.LocationDate} LM.DeviceID: {message.DeviceID}");
      }
    }
    for (int index = messageList.Count<LocationMessage>() - 1; index >= 0; --index)
    {
      if (messageList[index].LocationMessageGUID == Guid.Empty)
        messageList.RemoveAt(index);
    }
    return messageList;
  }

  public static List<LocationMessage> LoadByGatewayID(long gatewayID)
  {
    return BaseDBObject.LoadByForeignKey<LocationMessage>("DeviceID", (object) gatewayID);
  }

  public static List<LocationMessage> LoadByGatewayIDAndDateRange(
    long gatewayID,
    DateTime fromDate,
    DateTime toDate)
  {
    return new Monnit.Data.LocationMessage.LoadByDeviceIDAndRange(gatewayID, fromDate, toDate).Result;
  }

  public static List<LocationMessage> LoadRecentByVisualMapID(long visualMapID)
  {
    return new Monnit.Data.LocationMessage.LoadRecentByVisualMapID(visualMapID).Result;
  }

  public static List<LocationMessage> LoadByDeviceID(long deviceID)
  {
    return new Monnit.Data.LocationMessage.LoadByDeviceID(deviceID).Result;
  }

  public static double Distance(LocationMessage from, LocationMessage to)
  {
    double radians1 = LocationMessage.toRadians(from.Latitude);
    double radians2 = LocationMessage.toRadians(from.Longitude);
    double radians3 = LocationMessage.toRadians(to.Latitude);
    double num = LocationMessage.toRadians(to.Longitude) - radians2;
    return 2.0 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin((radians3 - radians1) / 2.0), 2.0) + Math.Cos(radians1) * Math.Cos(radians3) * Math.Pow(Math.Sin(num / 2.0), 2.0))) * 6371.0;
  }

  private static double toRadians(double angle) => angle * Math.PI / 180.0;

  public string ToCSVString(long deviceID, long timeZoneID, string dateFormat, string timeFormat)
  {
    return $"\"{this.LocationMessageGUID}\",\"{this.DeviceID}\",\"{this.LocationDate}\",\"{this.InsertDate}\",\"{this.Latitude}\",\"{this.Longitude}\",\"{this.Altitude}\",\"{this.Speed}\",\"{this.CourseOverGround}\",\"{this.Uncertainty}\",\"{this.FixTime}\",\"{this.NumberSatellites}\",\"{this.State}\"";
  }

  public static string ToCSVHeaderString()
  {
    return $"\"{"LocationMessageGUID"}\",\"{"DeviceID"}\",\"{"LocationDate"}\",\"{"InsertDate"}\",\"{"Latitude"}\",\"{"Longitude"}\",\"{"Altitude"}\",\"{"Speed"}\",\"{"CourseOverGround"}\",\"{"Accuracy"}\",\"{"FixTime"}\",\"{"SatelliteCount"}\",\"{"State"}\"";
  }

  public static string ToCSVFile(
    long deviceID,
    DateTime fromDate,
    DateTime toDate,
    long timeZoneID,
    string dateFormat,
    string timeFormat)
  {
    List<LocationMessage> locationMessageList = LocationMessage.LoadByGatewayIDAndDateRange(deviceID, fromDate, toDate);
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(LocationMessage.ToCSVHeaderString());
    foreach (LocationMessage locationMessage in locationMessageList)
      stringBuilder.AppendFormat("{0}\r\n", (object) locationMessage.ToCSVString(deviceID, timeZoneID, dateFormat, timeFormat));
    return stringBuilder.ToString();
  }
}
