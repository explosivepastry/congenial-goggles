// Decompiled with JetBrains decompiler
// Type: Monnit.SensorMessageAudit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("SensorMessageAudit")]
public class SensorMessageAudit : BaseDBObject
{
  private long _SensorMessageAuditID = long.MinValue;
  private long _SensorID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private DateTime _MessageDate = DateTime.MinValue;
  private string _MessageEvent = string.Empty;
  private bool _IsSuccess = false;

  [DBProp("SensorMessageAuditID", IsPrimaryKey = true)]
  public long SensorMessageAuditID
  {
    get => this._SensorMessageAuditID;
    set => this._SensorMessageAuditID = value;
  }

  [DBProp("SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("MessageDate")]
  public DateTime MessageDate
  {
    get => this._MessageDate;
    set => this._MessageDate = value;
  }

  [DBProp("MessageEvent", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string MessageEvent
  {
    get => this._MessageEvent;
    set => this._MessageEvent = value;
  }

  [DBProp("IsSuccess")]
  public bool IsSuccess
  {
    get => this._IsSuccess;
    set => this._IsSuccess = value;
  }

  public static SensorMessageAudit PopulateData(
    long sensorID,
    long gatewayID,
    DateTime messageDate,
    string Event,
    bool isSuccess)
  {
    if (!(ConfigData.AppSettings(nameof (SensorMessageAudit)).ToString() == "All") && !(ConfigData.AppSettings(nameof (SensorMessageAudit)).ToString() == gatewayID.ToString()))
      return (SensorMessageAudit) null;
    SensorMessageAudit sensorMessageAudit = new SensorMessageAudit();
    sensorMessageAudit.SensorID = sensorID;
    sensorMessageAudit.GatewayID = gatewayID;
    sensorMessageAudit.MessageDate = messageDate;
    sensorMessageAudit.MessageEvent = Event;
    sensorMessageAudit.IsSuccess = isSuccess;
    sensorMessageAudit.Save();
    return sensorMessageAudit;
  }

  public static List<SensorMessageAudit.SensorMessageAuditCountModel> GetOtherEventCount(
    long sensorID,
    DateTime from,
    DateTime to)
  {
    List<DataMessage> source = DataMessage.LoadBySensorAndDateRange(sensorID, from, to, 10000, new Guid?());
    List<SensorMessageAudit.SensorMessageAuditCountModel> otherEventCount = new List<SensorMessageAudit.SensorMessageAuditCountModel>();
    SensorMessageAudit.SensorMessageAuditCountModel messageAuditCountModel1 = new SensorMessageAudit.SensorMessageAuditCountModel();
    SensorMessageAudit.SensorMessageAuditCountModel messageAuditCountModel2 = new SensorMessageAudit.SensorMessageAuditCountModel();
    SensorMessageAudit.SensorMessageAuditCountModel messageAuditCountModel3 = new SensorMessageAudit.SensorMessageAuditCountModel();
    SensorMessageAudit.SensorMessageAuditCountModel messageAuditCountModel4 = new SensorMessageAudit.SensorMessageAuditCountModel();
    DataTable dataTable = DataMessage.LoadMissedBySensorAndDateRange(sensorID, from, to);
    int num1 = 0;
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    int num5 = 0;
    DateTime dateTime = DateTime.MinValue;
    DataMessage dataMessage1 = DataMessage.LoadLastBefore(sensorID, from);
    if (dataMessage1 != null)
      dateTime = dataMessage1.MessageDate;
    foreach (DataMessage dataMessage2 in source.OrderByDescending<DataMessage, DateTime>((System.Func<DataMessage, DateTime>) (x => x.MessageDate)).ToList<DataMessage>())
    {
      ++num4;
      DateTime messageDate = dataMessage2.MessageDate;
      if (messageDate.AddMinutes(-2.0) > dateTime)
        num2 = 0;
      Sensor sensor = Sensor.Load(dataMessage2.SensorID);
      TimeSpan timeSpan;
      if (sensor.GenerationType.ToUpper().Contains("GEN1"))
      {
        messageDate = dataMessage2.MessageDate;
        timeSpan = messageDate.AddMinutes(sensor.ReportInterval) - dateTime;
        if (Math.Abs(timeSpan.TotalSeconds) > 4.0)
          ++num3;
      }
      else if (sensor.GenerationType.ToUpper().Contains("GEN2"))
      {
        messageDate = dataMessage2.MessageDate;
        timeSpan = messageDate.AddMinutes(sensor.ReportInterval) - dateTime;
        if (Math.Abs(timeSpan.TotalSeconds) > 2.0)
          ++num3;
      }
      if (dataMessage2.LinkQuality == 0 || dataMessage2.SignalStrength == 0)
        ++num5;
      dateTime = dataMessage2.MessageDate;
      if ((dataMessage2.State & 1) == 1)
      {
        if (num2 == 0)
          ++num1;
        ++num2;
        if (num2 > 4)
          num2 = 0;
      }
      else
        num2 = 0;
    }
    messageAuditCountModel1.EventType = "Restart";
    messageAuditCountModel1.Success = 0;
    messageAuditCountModel1.Total = num1;
    messageAuditCountModel2.EventType = "TimeShift";
    messageAuditCountModel2.Success = num3;
    messageAuditCountModel2.Total = num4;
    messageAuditCountModel4.EventType = "DeliveredLate";
    messageAuditCountModel4.Success = 0;
    messageAuditCountModel4.Total = num5;
    messageAuditCountModel3.EventType = "Missed";
    messageAuditCountModel3.Success = 0;
    messageAuditCountModel3.Total = dataTable.Rows.Count;
    otherEventCount.Add(messageAuditCountModel3);
    otherEventCount.Add(messageAuditCountModel4);
    otherEventCount.Add(messageAuditCountModel1);
    otherEventCount.Add(messageAuditCountModel2);
    return otherEventCount;
  }

  public static SensorMessageAudit.SensorMessageAuditDataModel ConvertOtherData(
    DataMessage dm,
    string EventType)
  {
    return new SensorMessageAudit.SensorMessageAuditDataModel()
    {
      SensorID = dm.SensorID,
      GatewayID = dm.GatewayID,
      MessageDate = dm.MessageDate,
      Success = "N/A",
      MessageEvent = EventType
    };
  }

  public static SensorMessageAudit.SensorMessageAuditDataModel ConvertData(SensorMessageAudit audit)
  {
    return new SensorMessageAudit.SensorMessageAuditDataModel()
    {
      SensorID = audit.SensorID,
      GatewayID = audit.GatewayID,
      MessageDate = audit.MessageDate,
      Success = audit.IsSuccess.ToString(),
      MessageEvent = audit.MessageEvent
    };
  }

  public static List<SensorMessageAudit> Search(
    long sensorID,
    DateTime fromDate,
    DateTime toDate,
    string messageEvent)
  {
    return new Monnit.Data.SensorMessageAudit.Search(sensorID, fromDate, toDate, messageEvent).Result;
  }

  public static List<SensorMessageAudit.SensorMessageAuditCountModel> GetCounts(
    long sensorID,
    DateTime fromDate,
    DateTime toDate)
  {
    return new Monnit.Data.SensorMessageAudit.GetCounts(sensorID, fromDate, toDate).Result;
  }

  public static SensorMessageAudit Load(long id) => BaseDBObject.Load<SensorMessageAudit>(id);

  public class SensorMessageAuditCountModel
  {
    public string EventType { get; set; }

    public int Success { get; set; }

    public int Total { get; set; }
  }

  public class SensorMessageAuditDataModel
  {
    public long SensorID { get; set; }

    public long GatewayID { get; set; }

    public string MessageEvent { get; set; }

    public DateTime MessageDate { get; set; }

    public string Success { get; set; }
  }
}
