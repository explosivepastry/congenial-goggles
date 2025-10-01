// Decompiled with JetBrains decompiler
// Type: Data.NotificationHistory
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Data;

internal class NotificationHistory : BaseDBObject
{
  [DBMethod("NotificationHistory_LoadByNotificationIDandDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nCREATE TABLE #NotiTriggered\r\n(\r\n  [NotificationTriggeredID] BIGINT\r\n)\r\n\r\nINSERT INTO #NotiTriggered (NotificationTriggeredID)\r\nSELECT TOP (@Count)\r\n  NotificationTriggeredID\r\nFROM dbo.[NotificationTriggered] n WITH (NOLOCK)\r\nWHERE n.[NotificationID] = @NotificationID\r\n  AND n.StartTime BETWEEN @StartDate AND @EndDate\r\nORDER BY n.StartTime DESC;\r\n\r\nSELECT\r\n  n.NotificationTriggeredID,\r\n  n.NotificationID,\r\n  n.StartTime,\r\n  n.SensorNotificationID,\r\n  n.GatewayNotificationID,\r\n  n.AcknowledgedTime,\r\n  AcknowledgedBy              = c.FirstName + ' ' + c.LastName,\r\n  n.Reading,\r\n  n.ReadingDate,\r\n  n.AcknowledgeMethod,\r\n  n.resetTime,\r\n  n.HasNote\r\nFROM dbo.[NotificationTriggered] n WITH (NOLOCK)\r\nINNER JOIN #NotiTriggered nt on n.NotificationTriggeredID = nt.NotificationTriggeredID\r\nLEFT JOIN dbo.[Customer] c WITH (NOLOCK) on n.AcknowledgedBy = c.CustomerID\r\nORDER BY n.StartTime;\r\n\r\nSELECT\r\n  nr.NotificationRecordedID,\r\n  nr.NotificationTriggeredID,\r\n  nr.NotificationDate,\r\n  nr.eNotificationType,\r\n  nr.Status,\r\n  nr.SerializedRecipientProperties,\r\n  nr.SentTo,\r\n  RecipientCustomer             = c.FirstName + ' ' + c.LastName,\r\n  nr.SentToDeviceID,\r\n  nr.NotifyingOn,\r\n  nr.Delivered\r\nFROM dbo.[NotificationRecorded] nr WITH (NOLOCK)\r\nINNER JOIN #NotiTriggered n on n.NotificationTriggeredID = nr.NotificationTriggeredID\r\nLEFT JOIN dbo.[Customer] c on nr.CustomerID = c.CustomerID\r\nWHERE nr.NotificationDate BETWEEN @StartDate AND DATEADD(DAY, 1, @EndDate)\r\n  AND nr.NotificationID   = @NotificationID\r\nORDER BY nr.NotificationTriggeredID, nr.NotificationDate;\r\n\r\n")]
  internal class LoadByNotificationIDandDateRange : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("StartDate", typeof (DateTime))]
    public DateTime StartDate { get; private set; }

    [DBMethodParam("EndDate", typeof (DateTime))]
    public DateTime EndDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public int Count { get; private set; }

    public Dictionary<long, iMonnit.Models.NotificationHistory> Result { get; private set; }

    public LoadByNotificationIDandDateRange(
      long notificationID,
      DateTime startDate,
      DateTime endDate,
      int count)
    {
      this.NotificationID = notificationID;
      this.StartDate = startDate;
      this.EndDate = endDate;
      this.Count = count;
      DataSet dataSet = this.ToDataSet();
      this.Result = new Dictionary<long, iMonnit.Models.NotificationHistory>();
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
      {
        try
        {
          iMonnit.Models.NotificationHistory notificationHistory = new iMonnit.Models.NotificationHistory();
          NotificationEvent notificationEvent = new NotificationEvent();
          notificationEvent.Load(row);
          notificationHistory.Event = notificationEvent;
          notificationHistory.HasNote = notificationEvent.HasNote;
          notificationHistory.NotificationActionList = new List<NotificationAction>();
          this.Result.Add(notificationEvent.NotificationTriggeredID, notificationHistory);
        }
        catch (Exception ex)
        {
          ex.Log($"NotificationHistory.LoadByNotificationIDandDateRange | notificationID = {notificationID}, startDate = {startDate}, endDate = {endDate}, count = {count} | ds.Tables[0].Rows");
        }
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[1].Rows)
      {
        try
        {
          NotificationAction notificationAction = new NotificationAction();
          notificationAction.Load(row);
          this.Result[notificationAction.NotificationTriggeredID].NotificationActionList.Add(notificationAction);
        }
        catch (Exception ex)
        {
          ex.Log($"NotificationHistory.LoadByNotificationIDandDateRange | notificationID = {notificationID}, startDate = {startDate}, endDate = {endDate}, count = {count} | ds.Tables[1].Rows");
        }
      }
    }

    public static Dictionary<long, iMonnit.Models.NotificationHistory> Exec(
      long notificationID,
      DateTime startDate,
      DateTime endDate,
      int count)
    {
      return new NotificationHistory.LoadByNotificationIDandDateRange(notificationID, startDate, endDate, count).Result;
    }
  }

  [DBMethod("NotificationHistory_LoadOngoingByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nCREATE TABLE #NotiTriggered\r\n(\r\n  [NotificationTriggeredID] BIGINT\r\n)\r\n\r\nINSERT INTO #NotiTriggered (NotificationTriggeredID)\r\nSELECT TOP (@Count)\r\n  NotificationTriggeredID\r\nFROM dbo.[NotificationTriggered] n WITH (NOLOCK)\r\nWHERE n.[NotificationID] = @NotificationID\r\n  AND n.StartTime BETWEEN @StartDate AND @EndDate \r\n  AND resetTime IS NULL\r\nORDER BY n.StartTime DESC;\r\n\r\nSELECT\r\n  n.NotificationTriggeredID,\r\n  n.NotificationID,\r\n  n.StartTime,\r\n  n.SensorNotificationID,\r\n  n.GatewayNotificationID,\r\n  n.AcknowledgedTime,\r\n  AcknowledgedBy              = c.FirstName + ' ' + c.LastName,\r\n  n.Reading,\r\n  n.ReadingDate,\r\n  n.AcknowledgeMethod,\r\n  n.resetTime,\r\n  n.HasNote\r\nFROM dbo.[NotificationTriggered] n WITH (NOLOCK)\r\nINNER JOIN #NotiTriggered nt on n.NotificationTriggeredID = nt.NotificationTriggeredID\r\nLEFT JOIN dbo.[Customer] c WITH (NOLOCK) on n.AcknowledgedBy = c.CustomerID\r\nORDER BY n.StartTime;\r\n\r\n\r\nSELECT\r\n  nr.NotificationRecordedID,\r\n  nr.NotificationTriggeredID,\r\n  nr.NotificationDate,\r\n  nr.eNotificationType,\r\n  nr.Status,\r\n  nr.SerializedRecipientProperties,\r\n  nr.SentTo,\r\n  RecipientCustomer             = c.FirstName + ' ' + c.LastName,\r\n  nr.SentToDeviceID,\r\n  nr.NotifyingOn,\r\n  nr.Delivered\r\nFROM dbo.[NotificationRecorded] nr WITH (NOLOCK)\r\nINNER JOIN #NotiTriggered n on n.NotificationTriggeredID = nr.NotificationTriggeredID\r\nLEFT JOIN dbo.[Customer] c on nr.CustomerID = c.CustomerID\r\nWHERE nr.NotificationDate BETWEEN @StartDate AND DATEADD(DAY, 1, @EndDate)\r\n  AND nr.NotificationID   = @NotificationID\r\nORDER BY nr.NotificationTriggeredID, nr.NotificationDate;\r\n\r\n\r\ndrop table #NotiTriggered\r\n")]
  internal class LoadOngoingByNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("StartDate", typeof (DateTime))]
    public DateTime StartDate { get; private set; }

    [DBMethodParam("EndDate", typeof (DateTime))]
    public DateTime EndDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public int Count { get; private set; }

    public Dictionary<long, iMonnit.Models.NotificationHistory> Result { get; private set; }

    public LoadOngoingByNotificationID(
      long notificationID,
      DateTime startDate,
      DateTime endDate,
      int count)
    {
      this.NotificationID = notificationID;
      this.StartDate = startDate;
      this.EndDate = endDate;
      this.Count = count;
      DataSet dataSet = this.ToDataSet();
      this.Result = new Dictionary<long, iMonnit.Models.NotificationHistory>();
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[0].Rows)
      {
        try
        {
          iMonnit.Models.NotificationHistory notificationHistory = new iMonnit.Models.NotificationHistory();
          NotificationEvent notificationEvent = new NotificationEvent();
          notificationEvent.Load(row);
          notificationHistory.Event = notificationEvent;
          notificationHistory.HasNote = notificationEvent.HasNote;
          notificationHistory.NotificationActionList = new List<NotificationAction>();
          this.Result.Add(notificationEvent.NotificationTriggeredID, notificationHistory);
        }
        catch (Exception ex)
        {
        }
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataSet.Tables[1].Rows)
      {
        try
        {
          NotificationAction notificationAction = new NotificationAction();
          notificationAction.Load(row);
          this.Result[notificationAction.NotificationTriggeredID].NotificationActionList.Add(notificationAction);
        }
        catch (Exception ex)
        {
          ex.Log($"NotificationHistory.LoadOngoingByNotificationID | notificationID = {notificationID}, startDate = {startDate}, endDate = {endDate}, count = {count}");
        }
      }
    }

    public static Dictionary<long, iMonnit.Models.NotificationHistory> Exec(
      long notificationID,
      DateTime startDate,
      DateTime endDate,
      int count)
    {
      return new NotificationHistory.LoadOngoingByNotificationID(notificationID, startDate, endDate, count).Result;
    }
  }
}
