// Decompiled with JetBrains decompiler
// Type: Monnit.Cassandra.DataMessage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit.Cassandra;

internal class DataMessage
{
  public static void BulkInsert(List<Monnit.DataMessage> messageList)
  {
    if (!CassandraHelper.isCassandraWriter || messageList.Count <= 0 || !CassandraHelper.enableDataMessages)
      return;
    CassandraHelper.BulkInsert(messageList);
  }

  public static void UpdateDataMessage(Monnit.DataMessage msg)
  {
    if (!CassandraHelper.isCassandraWriter || !CassandraHelper.enableDataMessages)
      return;
    CassandraHelper.UpdateDataMessage(msg);
  }

  public static Monnit.DataMessage LoadByGuid(Guid ID)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages)
      return (Monnit.DataMessage) null;
    Monnit.DataMessage dataMessage = (Monnit.DataMessage) null;
    try
    {
      dataMessage = CassandraHelper.mapper.SingleAsync<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE DataMessageGuid = ?", (object) ID).Result;
    }
    catch (Exception ex)
    {
      ex.Log($"DataMessage.CassandraLoadByGuid[Guid: {ID.ToString()}]");
    }
    return dataMessage;
  }

  public static Monnit.DataMessage LoadLastBySensor(Sensor sensor)
  {
    if (sensor == null || sensor.LastDataMessageGUID == Guid.Empty || sensor.LastCommunicationDate == new DateTime(2099, 1, 1) || !CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages || !(sensor.LastCommunicationDate > CassandraHelper.cassandraDataMessageCutoffDate))
      return (Monnit.DataMessage) null;
    Monnit.DataMessage dataMessage = (Monnit.DataMessage) null;
    try
    {
      DateTime dateTime = DateTime.UtcNow;
      int year1 = dateTime.Year;
      dateTime = sensor.LastCommunicationDate;
      int year2 = dateTime.Year;
      int num1 = Math.Max(year1, year2);
      while (true)
      {
        int num2 = num1;
        dateTime = sensor.LastCommunicationDate;
        int year3 = dateTime.Year;
        if (num2 >= year3)
        {
          dataMessage = CassandraHelper.mapper.Fetch<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE SensorID = ? and year = ? order by MessageDate desc LIMIT 1", (object) sensor.SensorID, (object) num1).FirstOrDefault<Monnit.DataMessage>();
          if (dataMessage == null)
            --num1;
          else
            break;
        }
        else
          break;
      }
    }
    catch (Exception ex)
    {
      ex.Log($"CassandraLoadLastBySensor[SensorID: {sensor.SensorID.ToString()}]");
    }
    return dataMessage;
  }

  public static Monnit.DataMessage QuickLoad(
    long SensorID,
    Guid ID,
    DateTime LastCommunicationDate)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages || !(LastCommunicationDate > CassandraHelper.cassandraDataMessageCutoffDate))
      return (Monnit.DataMessage) null;
    Monnit.DataMessage dataMessage = (Monnit.DataMessage) null;
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      Sensor.Load(SensorID);
      dataMessage = CassandraHelper.mapper.Fetch<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE SensorID = ? and datamessageguid = ? AND MessageDate = ? and year = ? LIMIT 1", (object) SensorID, (object) ID, (object) LastCommunicationDate, (object) LastCommunicationDate.Year).FirstOrDefault<Monnit.DataMessage>();
    }
    catch (Exception ex)
    {
      ex.Log($"CassandraQuickLoad[SensorID: {SensorID.ToString()}][Guid: {ID.ToString()}] [LastCommunicationDate: {LastCommunicationDate.ToString()}]");
    }
    return dataMessage;
  }

  public static Monnit.DataMessage LoadLastByGateway(
    long SensorID,
    Guid ID,
    DateTime LastCommunicationDate)
  {
    Monnit.DataMessage dataMessage = (Monnit.DataMessage) null;
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      Sensor.Load(SensorID);
      dataMessage = CassandraHelper.mapper.Fetch<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE SensorID = ? and datamessageguid = ? AND MessageDate = ? and year = ? LIMIT 1", (object) SensorID, (object) ID, (object) LastCommunicationDate, (object) LastCommunicationDate.Year).FirstOrDefault<Monnit.DataMessage>();
    }
    catch (Exception ex)
    {
      ex.Log($"CassandraQuickLoad[SensorID: {SensorID.ToString()}][Guid: {ID.ToString()}] [LastCommunicationDate: {LastCommunicationDate.ToString()}]");
    }
    return dataMessage;
  }

  public static List<Monnit.DataMessage> LoadBySensorAndDateRange(
    long sensorID,
    DateTime fromDate,
    DateTime toDate,
    int limit,
    Guid? datamessageGuid,
    string method,
    DateTime logDate)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages || !(fromDate > CassandraHelper.cassandraDataMessageCutoffDate))
      return (List<Monnit.DataMessage>) null;
    List<Monnit.DataMessage> source = (List<Monnit.DataMessage>) null;
    limit = limit > 0 ? limit : 1;
    logDate = logDate;
    try
    {
      bool flag = false;
      int year = toDate.Year;
      int num = 0;
      if (datamessageGuid.HasValue && datamessageGuid.ToString() != "00000000-0000-0000-0000-000000000000")
      {
        Monnit.DataMessage dataMessage = CassandraHelper.mapper.Fetch<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE DataMessageGuid = ?", (object) datamessageGuid).DefaultIfEmpty<Monnit.DataMessage>(new Monnit.DataMessage()).FirstOrDefault<Monnit.DataMessage>();
        if (dataMessage.MessageDate < toDate)
          toDate = dataMessage.MessageDate;
      }
      for (; year >= fromDate.Year && !flag; --year)
      {
        List<Monnit.DataMessage> list = CassandraHelper.mapper.Fetch<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE SensorID = ? and Year = ? AND MessageDate >= ? and MessageDate <= ? LIMIT ?", (object) sensorID, (object) year, (object) fromDate, (object) toDate, (object) (limit - num)).ToList<Monnit.DataMessage>();
        if (source == null)
          source = list;
        else
          source.AddRange((IEnumerable<Monnit.DataMessage>) list);
        num += list.Count<Monnit.DataMessage>();
        if (num >= limit)
          flag = true;
      }
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] DataMessageCassandraLoadBySensorAndDateRange[sensorID: {sensorID.ToString()}][fromDate: {fromDate.ToShortDateString()}] [toDate: {toDate.ToShortDateString()}] ");
    }
    if (source != null)
      source.OrderByDescending<Monnit.DataMessage, DateTime>((Func<Monnit.DataMessage, DateTime>) (DataMessage => DataMessage.MessageDate)).ToList<Monnit.DataMessage>();
    return source;
  }

  public static List<Monnit.DataMessage> LoadRecentBySensor(
    long sensorID,
    int minutes,
    Guid? datamessageGuid)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages || !(DateTime.Now.AddMinutes((double) (-1 * minutes)) > CassandraHelper.cassandraDataMessageCutoffDate))
      return (List<Monnit.DataMessage>) null;
    List<Monnit.DataMessage> source = (List<Monnit.DataMessage>) null;
    DateTime now = DateTime.Now;
    DateTime dateTime = now.AddMinutes((double) (-1 * minutes));
    try
    {
      if (datamessageGuid.HasValue && datamessageGuid.ToString() != "00000000-0000-0000-0000-000000000000")
      {
        Monnit.DataMessage dataMessage = CassandraHelper.mapper.Fetch<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE DataMessageGuid = ?", (object) datamessageGuid).DefaultIfEmpty<Monnit.DataMessage>(new Monnit.DataMessage()).FirstOrDefault<Monnit.DataMessage>();
        if (dataMessage.MessageDate > dateTime)
          dateTime = dataMessage.MessageDate;
      }
      bool flag = false;
      for (int year = now.Year; year >= dateTime.Year && !flag; --year)
      {
        List<Monnit.DataMessage> list = CassandraHelper.mapper.Fetch<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE SensorID = ? and Year = ? AND MessageDate >= ? and MessageDate <= ?", (object) sensorID, (object) year, (object) dateTime, (object) now).ToList<Monnit.DataMessage>();
        if (source == null)
          source = list;
        else
          source.AddRange((IEnumerable<Monnit.DataMessage>) list);
      }
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] DataMessage.CassandrLoadBySensorAndDateRange[sensorID: {sensorID}] [fromDate: {dateTime.ToShortDateString()}] [toDate: {now.ToShortDateString()}] ");
    }
    if (source != null)
      source.OrderByDescending<Monnit.DataMessage, DateTime>((Func<Monnit.DataMessage, DateTime>) (DataMessage => DataMessage.MessageDate)).ToList<Monnit.DataMessage>();
    return source;
  }

  public static List<DataPoint> CountByDate_LoadForChart(
    long sensorID,
    DateTime fromDate,
    DateTime toDate,
    int maxCount)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages || !(fromDate > CassandraHelper.cassandraDataMessageCutoffDate))
      return (List<DataPoint>) null;
    List<DataPoint> source = (List<DataPoint>) null;
    int num = 0;
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      bool flag = false;
      for (int year = toDate.Year; year >= fromDate.Year && !flag; --year)
      {
        List<DataPoint> list = CassandraHelper.mapper.Fetch<DataPoint>("SELECT ToTimeStamp(ToDate(MessageDate)) AS Date, Count(*) AS Value FROM DataMessageBySensorYear WHERE SensorID = ? and Year = ? AND MessageDate >= ? and MessageDate <= ? GROUP BY ToDate(MessageDate)", (object) sensorID, (object) year, (object) fromDate, (object) toDate).ToList<DataPoint>();
        if (source == null)
          source = list;
        else
          source.AddRange((IEnumerable<DataPoint>) list);
        foreach (DataPoint dataPoint in list)
          num += dataPoint.Value.ToInt();
        if (num >= maxCount)
          flag = true;
      }
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] DataMessage.CassandraCountByDate_LoadForChart[sensorID: {sensorID}][fromDate: {fromDate.ToShortDateString()}] [toDate: {toDate.ToShortDateString()}] ");
    }
    if (source != null)
      source.OrderByDescending<DataPoint, DateTime>((Func<DataPoint, DateTime>) (DataPoint => DataPoint.Date)).ToList<DataPoint>();
    return source;
  }

  public static List<Monnit.DataMessage> LoadByNetwork(
    long networkID,
    DateTime from,
    DateTime to,
    int limit)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages || !(from > CassandraHelper.cassandraDataMessageCutoffDate))
      return (List<Monnit.DataMessage>) null;
    List<Monnit.DataMessage> source = (List<Monnit.DataMessage>) null;
    limit = limit > 0 ? limit : 1;
    int num = 0;
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      foreach (Sensor sensor in Sensor.LoadByCsNetID(networkID))
      {
        if (num < limit)
        {
          List<Monnit.DataMessage> collection = DataMessage.LoadBySensorAndDateRange(sensor.SensorID, from, to, limit - num, new Guid?(), "CassandraLoadByNetwork", utcNow);
          if (collection != null)
          {
            if (source == null)
              source = collection;
            else
              source.AddRange((IEnumerable<Monnit.DataMessage>) collection);
            num += collection.Count;
          }
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] DataMessage.CassandraLoadByNetwork[networkID: {networkID}][fromDate: {from.ToShortDateString()}] [toDate: {to.ToShortDateString()}] ");
    }
    if (source != null)
      source.OrderByDescending<Monnit.DataMessage, DateTime>((Func<Monnit.DataMessage, DateTime>) (DataMessage => DataMessage.MessageDate)).ToList<Monnit.DataMessage>();
    return source;
  }

  public static List<Monnit.DataMessage> LoadByAccount(
    long accountID,
    DateTime from,
    DateTime to,
    int limit)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages || !(from > CassandraHelper.cassandraDataMessageCutoffDate))
      return (List<Monnit.DataMessage>) null;
    List<Monnit.DataMessage> source = (List<Monnit.DataMessage>) null;
    limit = limit > 0 ? limit : 1;
    int num = 0;
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      foreach (Sensor sensor in Sensor.LoadByAccountID(accountID))
      {
        List<Monnit.DataMessage> collection = DataMessage.LoadBySensorAndDateRange(sensor.SensorID, from, to, limit - num, new Guid?(), "CassandraLoadByAccount", utcNow);
        if (collection != null)
        {
          if (source == null)
            source = collection;
          else
            source.AddRange((IEnumerable<Monnit.DataMessage>) collection);
          num += collection.Count;
        }
      }
    }
    catch (Exception ex)
    {
      if (source.Count == 0)
        source = (List<Monnit.DataMessage>) null;
      ex.Log($"[{Environment.MachineName}] DataMessage.CassandraLoadByAccount[accountID: {accountID.ToString()}][fromDate: {from.ToShortDateString()}] [toDate: {to.ToShortDateString()}]  ");
    }
    if (source != null)
      source.OrderByDescending<Monnit.DataMessage, DateTime>((Func<Monnit.DataMessage, DateTime>) (DataMessage => DataMessage.MessageDate)).ToList<Monnit.DataMessage>();
    return source;
  }

  public static Monnit.DataMessage LoadLastBefore(long sensorID, DateTime toDate)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableDataMessages || !(toDate.AddDays(-1.0) > CassandraHelper.cassandraDataMessageCutoffDate))
      return (Monnit.DataMessage) null;
    try
    {
      DateTime dateTime = toDate.AddYears(-1);
      for (int year = toDate.Year; year >= dateTime.Year; --year)
      {
        List<Monnit.DataMessage> list = CassandraHelper.mapper.Fetch<Monnit.DataMessage>("SELECT * FROM DataMessageBySensorYear WHERE SensorID = ? and Year = ? AND MessageDate < ? LIMIT 1", (object) sensorID, (object) year, (object) toDate).ToList<Monnit.DataMessage>();
        if (list.Count > 0)
          return list.First<Monnit.DataMessage>();
      }
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] DataMessage.CassandraLoadLastBefore[sensorID: {sensorID.ToString()}] [toDate: {toDate.ToShortDateString()}]  ");
    }
    return (Monnit.DataMessage) null;
  }
}
