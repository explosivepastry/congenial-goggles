// Decompiled with JetBrains decompiler
// Type: Monnit.Cassandra.GatewayMessage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit.Cassandra;

internal class GatewayMessage
{
  public static void InsertGatewayMessage(Monnit.GatewayMessage gwmsg)
  {
    if (!CassandraHelper.isCassandraWriter || !CassandraHelper.enableGatewayMessages)
      return;
    CassandraHelper.InsertGatewayMessage(gwmsg);
  }

  public static void UpdateGatewayMessage(Monnit.GatewayMessage gwmsg)
  {
    if (!CassandraHelper.isCassandraWriter || !CassandraHelper.enableGatewayMessages)
      return;
    CassandraHelper.UpdateGatewayMessage(gwmsg);
  }

  public static Monnit.GatewayMessage CassandraLoadLastByGateway(long gatewayID)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableGatewayMessages)
      return (Monnit.GatewayMessage) null;
    Monnit.GatewayMessage gatewayMessage = (Monnit.GatewayMessage) null;
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      Gateway gateway = Gateway.Load(gatewayID);
      int num = gateway.LastCommunicationDate.Year * 100 + gateway.LastCommunicationDate.Month;
      gatewayMessage = CassandraHelper.mapper.Fetch<Monnit.GatewayMessage>("SELECT * FROM GatewayMessageByGatewayYearMonth WHERE gatewayid = ? and yearmonth = ? and receiveddate = ?", (object) gatewayID, (object) num, (object) gateway.LastCommunicationDate).FirstOrDefault<Monnit.GatewayMessage>();
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] GatewayMessage.CassandraLoadLastByGateway[GatewayID: {gatewayID.ToString()}]");
    }
    return gatewayMessage;
  }

  public static List<Monnit.GatewayMessage> CassandraLoadByNetwork(
    long networkID,
    DateTime from,
    DateTime to,
    int limit)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableGatewayMessages || !(from > CassandraHelper.cassandraGatewayMessageCutoffDate))
      return (List<Monnit.GatewayMessage>) null;
    List<Monnit.GatewayMessage> source = (List<Monnit.GatewayMessage>) null;
    limit = limit > 0 ? limit : 1;
    int num1 = 0;
    long num2 = 0;
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      num2 = CSNet.Load(networkID).AccountID;
      foreach (Gateway gateway in Gateway.LoadByCSNetID(networkID))
      {
        if (num1 < limit)
        {
          List<Monnit.GatewayMessage> collection = GatewayMessage.CassandraLoadByGatewayAndDateRange(gateway.GatewayID, new DateTime?(from), new DateTime?(to), limit - num1, new Guid?(), nameof (CassandraLoadByNetwork), utcNow);
          if (source == null)
            source = collection;
          else
            source.AddRange((IEnumerable<Monnit.GatewayMessage>) collection);
          num1 += collection.Count;
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] GatewayMessage.CassandraLoadByNetwork[networkID: {networkID.ToString()}][fromDate: {from.ToShortDateString()}] [toDate: {to.ToShortDateString()}]  ");
    }
    if (source != null)
      source.OrderByDescending<Monnit.GatewayMessage, DateTime>((Func<Monnit.GatewayMessage, DateTime>) (DataMessage => DataMessage.ReceivedDate)).ToList<Monnit.GatewayMessage>();
    return source;
  }

  public static List<Monnit.GatewayMessage> CassandraLoadByGatewayAndDateRange(
    long gatewayID,
    DateTime? fromDateNullable,
    DateTime? toDateNullable,
    int limit,
    Guid? gatewayMessageGuid,
    string method,
    DateTime logDate)
  {
    List<Monnit.GatewayMessage> source = (List<Monnit.GatewayMessage>) null;
    limit = limit > 0 ? limit : 1;
    logDate = logDate;
    DateTime dateTime1 = DateTime.UtcNow;
    DateTime dateTime2 = DateTime.UtcNow;
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableGatewayMessages || !(dateTime2 > CassandraHelper.cassandraGatewayMessageCutoffDate))
      return (List<Monnit.GatewayMessage>) null;
    try
    {
      Gateway gateway = Gateway.Load(gatewayID);
      dateTime1 = !toDateNullable.HasValue ? gateway.LastCommunicationDate : toDateNullable.ToDateTime();
      dateTime2 = !fromDateNullable.HasValue ? dateTime1.AddMonths(-6) : fromDateNullable.ToDateTime();
      if (gatewayMessageGuid.HasValue && gatewayMessageGuid.ToString() != "00000000-0000-0000-0000-000000000000")
      {
        List<Monnit.GatewayMessage> list = CassandraHelper.mapper.Fetch<Monnit.GatewayMessage>("SELECT * FROM GatewayMessageByGatewayYearMonth WHERE gatewaymessageguid = ?", (object) gatewayMessageGuid).ToList<Monnit.GatewayMessage>();
        if (list != null && list.Count > 0)
          dateTime1 = list.First<Monnit.GatewayMessage>().ReceivedDate;
      }
      if (dateTime2 > dateTime1)
        return new List<Monnit.GatewayMessage>();
      bool flag = false;
      int num1 = 0;
      DateTime dateTime3 = new DateTime(dateTime1.Year, dateTime1.Month, 1);
      int num2 = dateTime1.Year * 100 + dateTime1.Month;
      while (!flag)
      {
        int num3 = dateTime3.Year * 100 + dateTime3.Month;
        List<Monnit.GatewayMessage> list = CassandraHelper.mapper.Fetch<Monnit.GatewayMessage>("SELECT * FROM GatewayMessageByGatewayYearMonth WHERE gatewayID = ? and YearMonth = ? AND ReceivedDate >= ? and ReceivedDate <= ? LIMIT ?", (object) gatewayID, (object) num3, (object) dateTime2, (object) dateTime1, (object) (limit - num1)).ToList<Monnit.GatewayMessage>();
        if (source == null)
          source = list;
        else
          source.AddRange((IEnumerable<Monnit.GatewayMessage>) list);
        num1 += list.Count;
        if (num1 >= limit)
          flag = true;
        dateTime3 = dateTime3.AddMonths(-1);
        if (dateTime3.Year <= dateTime2.Year && dateTime3.Month < dateTime2.Month)
          flag = true;
      }
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] GatewayMessage.LoadByGatewayAndDateRange[gatewayID: {gatewayID.ToString()}][fromDate: {dateTime2.ToShortDateString()}] [toDate: {dateTime1.ToShortDateString()}]  ");
    }
    if (source != null)
      source.OrderByDescending<Monnit.GatewayMessage, DateTime>((Func<Monnit.GatewayMessage, DateTime>) (DataMessage => DataMessage.ReceivedDate)).ToList<Monnit.GatewayMessage>();
    return source;
  }

  public static List<Monnit.GatewayMessage> CassandraLoadByAccount(
    long accountID,
    DateTime from,
    DateTime to,
    int limit)
  {
    if (!CassandraHelper.isCassandraReader || !CassandraHelper.enableGatewayMessages || !(from > CassandraHelper.cassandraGatewayMessageCutoffDate))
      return (List<Monnit.GatewayMessage>) null;
    List<Monnit.GatewayMessage> source = (List<Monnit.GatewayMessage>) null;
    limit = limit > 0 ? limit : 1;
    int num = 0;
    DateTime utcNow = DateTime.UtcNow;
    try
    {
      foreach (Gateway gateway in Gateway.LoadByAccountID(accountID))
      {
        if (num < limit)
        {
          List<Monnit.GatewayMessage> collection = GatewayMessage.CassandraLoadByGatewayAndDateRange(gateway.GatewayID, new DateTime?(from), new DateTime?(to), limit - num, new Guid?(), nameof (CassandraLoadByAccount), utcNow);
          if (source == null)
            source = collection;
          else
            source.AddRange((IEnumerable<Monnit.GatewayMessage>) collection);
          num += collection.Count;
        }
      }
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] GatewayMessage.CassandraLoadByAccount[accountID: {accountID.ToString()}][fromDate: {from.ToShortDateString()}] [toDate: {to.ToShortDateString()}]  ");
    }
    if (source != null)
      source.OrderByDescending<Monnit.GatewayMessage, DateTime>((Func<Monnit.GatewayMessage, DateTime>) (DataMessage => DataMessage.ReceivedDate)).ToList<Monnit.GatewayMessage>();
    return source;
  }
}
