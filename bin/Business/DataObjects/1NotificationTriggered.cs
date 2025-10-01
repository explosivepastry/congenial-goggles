// Decompiled with JetBrains decompiler
// Type: Monnit.Data.NotificationTriggered
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class NotificationTriggered
{
  [DBMethod("NotificationTriggered_LoadLastByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP(@Count)\r\n  *\r\nFROM dbo.[NotificationTriggered] WITH (NOLOCK)\r\nWHERE NotificationID = @NotificationID\r\nORDER BY\r\n  StartTime DESC;\r\n")]
  internal class LoadLastByNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationTriggered> Result { get; private set; }

    public LoadLastByNotificationID(long notificationID, int count)
    {
      this.NotificationID = notificationID;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationTriggered>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationTriggered_LoadActiveByDeviceIDandNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  nt.*\r\nFROM dbo.[NotificationTriggered] nt WITH (NOLOCK)\r\nLEFT JOIN dbo.[SensorNotification] sn   WITH (NOLOCK) ON sn.SensorNotificationID  = nt.SensorNotificationID\r\nLEFT JOIN dbo.[GatewayNotification] gn  WITH (NOLOCK) ON gn.GatewayNotificationID = nt.GatewayNotificationID\r\nWHERE nt.NotificationID = @NotificationID \r\n  AND (@SensorID  IS NULL OR sn.SensorID  = @SensorID)\r\n  AND (@GatewayID IS NULL OR gn.GatewayID = @GatewayID)\r\n  AND StartTime IS NOT NULL \r\n  AND (resetTime  IS NULL OR AcknowledgedTime IS NULL);\r\n")]
  internal class LoadActiveByDeviceIDandNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.NotificationTriggered> Result { get; private set; }

    public LoadActiveByDeviceIDandNotificationID(
      long notificationID,
      long? sensorID,
      long? gatewayID)
    {
      this.NotificationID = notificationID;
      this.SensorID = sensorID ?? long.MinValue;
      this.GatewayID = gatewayID ?? long.MinValue;
      this.Result = BaseDBObject.Load<Monnit.NotificationTriggered>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationTriggered_LoadActiveByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  nt.*\r\nFROM dbo.[NotificationTriggered] nt WITH (NOLOCK)\r\nINNER JOIN dbo.[Notification] n ON n.NotificationID = nt.NotificationID\r\nWHERE n.AccountID          = @AccountID\r\n  AND n.IsDeleted          = 0\r\n  AND n.IsActive           = 1\r\n  AND nt.StartTime         IS NOT NULL \r\n  AND (nt.resetTime        IS NULL\r\n   OR  nt.AcknowledgedTime IS NULL);\r\n")]
  internal class LoadActiveByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.NotificationTriggered> Result { get; private set; }

    public LoadActiveByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.NotificationTriggered>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationTriggered_LoadActiveBySensorID")]
  [DBMethodBody(DBMS.SqlServer, " \r\nSELECT \r\n  nt.*\r\nFROM dbo.NotificationTriggered nt WITH (NOLOCK)\r\nJOIN dbo.SensorNotification sn ON sn.NotificationID = nt.NotificationID\r\nWHERE sn.SensorID             = @SensorID\r\n  AND sn.SensorNotificationID = nt.SensorNotificationID\r\n  AND StartTime IS NOT NULL \r\n  AND (resetTime IS NULL OR AcknowledgedTime IS NULL);\r\n")]
  internal class LoadActiveBySensorID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public List<Monnit.NotificationTriggered> Result { get; private set; }

    public LoadActiveBySensorID(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = BaseDBObject.Load<Monnit.NotificationTriggered>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationTriggered_LoadActiveByGatewayID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  nt.*\r\nFROM dbo.[NotificationTriggered] nt \r\nJOIN dbo.[GatewayNotification] gn ON gn.NotificationID = nt.NotificationID\r\nWHERE gn.GatewayID             = @GatewayID\r\n  AND gn.GatewayNotificationID = nt.GatewayNotificationID\r\n  AND StartTime IS NOT NULL\r\n  AND (resetTime IS NULL or AcknowledgedTime IS NULL);\r\n")]
  internal class LoadActiveByGatewayID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.NotificationTriggered> Result { get; private set; }

    public LoadActiveByGatewayID(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.NotificationTriggered>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationTriggered_LoadActiveByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[NotificationTriggered]\r\nWHERE NotificationID = @NotificationID \r\n  AND StartTime IS NOT NULL\r\n  AND (resetTime IS NULL OR AcknowledgedTime IS NULL);\r\n")]
  internal class LoadActiveByNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    public List<Monnit.NotificationTriggered> Result { get; private set; }

    public LoadActiveByNotificationID(long notificationID)
    {
      this.NotificationID = notificationID;
      this.Result = BaseDBObject.Load<Monnit.NotificationTriggered>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationTriggered_LoadByNotificationRecordedID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[NotificationTriggered] nt\r\nINNER JOIN dbo.[NotificationRecorded] nr ON nt.NotificationTriggeredID = nr.NotificationTriggeredID \r\nWHERE nr.NotificationRecordedID = @NotificationRecordedID;\r\n")]
  internal class LoadByNotificationRecordedID : BaseDBMethod
  {
    [DBMethodParam("NotificationRecordedID", typeof (long))]
    public long NotificationRecordedID { get; private set; }

    public Monnit.NotificationTriggered Result { get; private set; }

    public LoadByNotificationRecordedID(long notificationRecordedID)
    {
      this.NotificationRecordedID = notificationRecordedID;
      this.Result = BaseDBObject.Load<Monnit.NotificationTriggered>(this.ToDataTable()).FirstOrDefault<Monnit.NotificationTriggered>();
    }
  }

  [DBMethod("NotificationTriggered_LoadReady")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  n.NotificationID, \r\n  nt.NotificationTriggeredID, \r\n  nr.NotificationRecipientID, \r\n  sn.SensorNotificationID, \r\n  gn.GatewayNotificationID, \r\n  s.SensorID, \r\n  g.GatewayID, \r\n  cs.CSNetID,\r\n  cl.CustomerGroupLinkID,\r\n  gr.CustomerGroupRecipientID,\r\n  cl.CustomerID,\r\n  cl.eNotificationType\r\nINTO #Temp\r\nFROM dbo.[Notification] n WITH (NOLOCK)\r\nINNER JOIN dbo.[NotificationTriggered] nt With(nolock) ON n.NotificationID = nt.NotificationID \r\nINNER JOIN dbo.[NotificationRecipient] nr With(nolock) ON n.NotificationID = nr.NotificationID\r\nLEFT JOIN dbo.[SensorNotification] sn With(nolock) ON sn.SensorNotificationID = nt.SensorNotificationID\r\nLEFT JOIN dbo.[GatewayNotification] gn With(nolock) ON gn.GatewayNotificationID = nt.GatewayNotificationID\r\nLEFT JOIN dbo.[Sensor] s With(nolock) ON s.SensorID = sn.SensorID\r\nLEFT JOIN dbo.[Gateway] g With(nolock) ON g.GatewayID = gn.GatewayID\r\nLEFT JOIN dbo.[CSNet] cs With(nolock) ON (cs.CSNetID = s.CSNetID OR cs.CSNetID = g.CSNetID)\r\nLEFT JOIN dbo.[CustomerGroupLink] cl With(nolock) ON nr.CustomerGroupID = cl.CustomerGroupID AND DATEADD(MINUTE, ISNULL(cl.DelayMinutes, 0), nt.StartTime) < @DateTimeNow\r\nLEFT JOIN dbo.[CustomerGroupRecipient] gr With(nolock) ON cl.CustomerGroupLinkID = gr.CustomerGroupLinkID and gr.NotificationID = n.NotificationID-- AND DATEADD(MINUTE,ISNULL(n.SnoozeDuration,60), ISNULL(gr.LastSentTime,'2010-01-01')) < @DateTimeNow \r\nWHERE nt.AcknowledgedBy   IS NULL\r\n  AND nt.AcknowledgedTime IS NULL\r\n  AND nt.StartTime                                            < @DateTimeNow\r\n  AND DATEADD(MINUTE,ISNULL(nr.DelayMinutes,0), nt.StartTime) < @DateTimeNow \r\n  AND DATEADD(HOUR,25, nt.StartTime)                          > @DateTimeNow\r\n  AND (n.IsDeleted = 0        OR n.IsDeleted IS NULL)\r\n  AND (n.IsActive = 1         OR n.IsActive  IS NULL)\r\n  AND (s.SensorID IS NOT NULL OR g.GatewayID IS NOT NULL)\r\n  AND (DATEADD(MINUTE,ISNULL(n.SnoozeDuration,60), ISNULL(nr.LastSentTime,'2010-01-01')) < @DateTimeNow \r\n\t  OR ISNULL(nr.LastSentTime,'2010-01-01') < nt.StartTime)\r\n  AND (ISNULL(n.SnoozeDuration,60) > 0                       -- snooze must be greater than 0\r\n\t  OR ISNULL(nr.LastSentTime,'2010-01-01') < nt.StartTime)\r\n  AND ((nr.eNotificationType = 14 and (cl.CustomerGroupLinkID IS NOT NULL AND (DATEADD(MINUTE,ISNULL(n.SnoozeDuration,60), ISNULL(gr.LastSentTime,'2010-01-01')) < @DateTimeNow OR ISNULL(gr.LastSentTime, '2010-01-01') < nt.StartTime))) or (nr.eNotificationType != 14)) -- or user hasn't received it yet for this NotificationTrigger\r\nOPTION(MAXDOP 1);\r\n\r\nSELECT DISTINCT\r\n  NotificationID, \r\n  NotificationTriggeredID, \r\n  NotificationRecipientID, \r\n  SensorNotificationID, \r\n  GatewayNotificationID, \r\n  SensorID, \r\n  GatewayID, \r\n  CSNetID, \r\n  CustomerGroupLinkID,\r\n  CustomerGroupRecipientID,\r\n  CustomerID,\r\n  eNotificationType\r\nFROM #Temp;\r\n\r\nSELECT DISTINCT\r\n  n.*\r\nFROM dbo.[Notification] n \r\nINNER JOIN #Temp t ON t.NotificationID = n.NotificationID;\r\n\r\nSELECT DISTINCT\r\n  nt.*\r\nFROM dbo.[NotificationTriggered] nt \r\nINNER JOIN #Temp t ON t.NotificationTriggeredID = nt.NotificationTriggeredID;\r\n\r\nSELECT DISTINCT\r\nnr.*\r\nFROM dbo.[NotificationRecipient] nr\r\nINNER JOIN #Temp t on nr.NotificationRecipientID = t.NotificationRecipientID\r\n\r\n\r\nSELECT DISTINCT\r\n  sn.*\r\nFROM dbo.[SensorNotification] sn \r\nINNER JOIN #Temp t ON t.SensorNotificationID = sn.SensorNotificationID;\r\n\r\nSELECT DISTINCT\r\n  gn.*\r\nFROM dbo.[GatewayNotification] gn \r\nINNER JOIN #Temp t ON t.GatewayNotificationID = gn.GatewayNotificationID;\r\n\r\nSELECT DISTINCT\r\n  s.*\r\nFROM dbo.[Sensor] s \r\nINNER JOIN #Temp t ON t.SensorID = s.SensorID;\r\n\r\nSELECT DISTINCT\r\n  g.*\r\nFROM dbo.[Gateway] g \r\nINNER JOIN #Temp t ON t.GatewayID = g.GatewayID;\r\n\r\nSELECT DISTINCT\r\n  cs.*\r\nFROM dbo.[CSNet] cs \r\nINNER JOIN #Temp t ON t.CSNetID = cs.CSNetID;\r\n\r\nSELECT DISTINCT\r\ngr.*\r\nFROM  dbo.[CustomerGroupRecipient] gr \r\nINNER JOIN #Temp t ON gr.CustomerGroupRecipientID = t.CustomerGroupRecipientID and t.NotificationID = gr.NotificationID\r\n\r\nDrop Table #Temp;\r\n")]
  internal class LoadReady : BaseDBMethod
  {
    [DBMethodParam("DateTimeNow", typeof (DateTime))]
    public DateTime DateTimeNow { get; private set; }

    public DataSet Result { get; private set; }

    public LoadReady()
    {
      this.DateTimeNow = DateTime.UtcNow;
      this.Result = this.ToDataSet();
    }

    public static DataSet Exec() => new NotificationTriggered.LoadReady().Result;
  }

  [DBMethod("NotificationTriggered_LoadImmediate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  nr.NotificationRecipientID, \r\n  cl.CustomerGroupLinkID,\r\n  gr.CustomerGroupRecipientID,\r\n  cl.CustomerID,\r\n  cl.eNotificationType\r\nINTO #Temp\r\nFROM dbo.[NotificationRecipient] nr with(nolock)\r\nLEFT JOIN dbo.[CustomerGroupLink] cl ON nr.CustomerGroupID = cl.CustomerGroupID AND cl.DelayMinutes = 0\r\nLEFT JOIN dbo.[CustomerGroupRecipient] gr ON cl.CustomerGroupLinkID = gr.CustomerGroupLinkID AND gr.NotificationID = @NotificationID\r\nWHERE nr.NotificationID = @NotificationID\r\n  AND ISNULL(nr.DelayMinutes,0) = 0 \r\n  AND ((nr.eNotificationType = 14 and cl.CustomerGroupLinkID IS NOT NULL) or (nr.eNotificationType != 14));\r\n\r\n\r\nSELECT DISTINCT\r\n  NotificationRecipientID, \r\n  CustomerGroupLinkID,\r\n  CustomerGroupRecipientID,\r\n  CustomerID,\r\n  eNotificationType\r\nFROM #Temp;\r\n\r\nSELECT DISTINCT\r\nnr.*\r\nFROM dbo.[NotificationRecipient] nr\r\nINNER JOIN #Temp t on nr.NotificationRecipientID = t.NotificationRecipientID\r\n\r\n\r\nSELECT DISTINCT\r\ngr.*\r\nFROM  dbo.[CustomerGroupRecipient] gr \r\nINNER JOIN #Temp t ON gr.CustomerGroupRecipientID = t.CustomerGroupRecipientID\r\n\r\n----UncommentFor Debug\r\n--SELECT DISTINCT\r\n--gr.*\r\n--FROM  dbo.[CustomerGroupLink] gr \r\n--INNER JOIN #Temp t ON gr.CustomerGroupLinkID = t.CustomerGroupLinkID\r\n\r\n\r\nDrop Table #Temp;\r\n")]
  internal class LoadImmediate : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    public DataSet Result { get; private set; }

    public LoadImmediate(long notiID)
    {
      this.NotificationID = notiID;
      this.Result = this.ToDataSet();
    }

    public static DataSet Exec(long notiID)
    {
      return new NotificationTriggered.LoadImmediate(notiID).Result;
    }
  }
}
