// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Notification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class Notification
{
  [DBMethod("Notification_LoadBySensorID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT DISTINCT\r\n  n.* \r\nFROM dbo.[Notification] n\r\nINNER JOIN dbo.[SensorNotification] sn on sn.NotificationID = n.NotificationID\r\nWHERE sn.SensorID  = @SensorID\r\n  AND (n.IsDeleted = 0 OR n.IsDeleted IS NULL);\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM Notification n INNER JOIN SensorNotification sn ON sn.NotificationID = n.NotificationID WHERE sn.SensorID = @SensorID AND (n.IsDeleted = 0 OR n.IsDeleted is null)")]
  internal class LoadBySensorID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public List<Monnit.Notification> Result { get; private set; }

    public LoadBySensorID(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = BaseDBObject.Load<Monnit.Notification>(this.ToDataTable());
    }
  }

  [DBMethod("Notification_LoadByGatewayID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT DISTINCT\r\n  n.*\r\nFROM dbo.[Notification] n\r\nINNER JOIN dbo.[GatewayNotification] gn ON gn.NotificationID = n.NotificationID\r\nWHERE gn.GatewayID = @GatewayID\r\n  AND (n.IsDeleted = 0 OR n.IsDeleted IS NULL);\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM Notification WHERE 1=0")]
  internal class LoadByGatewayID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.Notification> Result { get; private set; }

    public LoadByGatewayID(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.Notification>(this.ToDataTable());
    }
  }

  [DBMethod("Notification_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Notification]\r\nWHERE AccountID  = @AccountID\r\n  AND (IsDeleted = 0 OR IsDeleted IS NULL);\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT n.* FROM Notification n INNER JOIN Sensor s on s.SensorID = n.SensorID Where s.AccountID = @AccountID AND (IsDeleted = 0 OR IsDeleted is null)")]
  internal class LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.Notification> Result { get; private set; }

    public LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.Notification>(this.ToDataTable());
    }
  }

  [DBMethod("Notification_LoadByCustomerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT n.* \r\n\tFROM NotificationRecipient nr\r\n\tJOIN Notification n on n.NotificationID = nr.NotificationID\r\n\tWHERE nr.CustomerToNotifyID = @CustomerID\r\n\tAND n.IsDeleted = 0\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT n.* FROM NotificationRecipient nr JOIN Notification n on n.NotificationID = nr.NotificationID WHERE nr.CustomerToNotifyID = @CustomerID")]
  internal class LoadByCustomerID : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public List<Monnit.Notification> Result { get; private set; }

    public LoadByCustomerID(long customerID)
    {
      this.CustomerID = customerID;
      this.Result = BaseDBObject.Load<Monnit.Notification>(this.ToDataTable());
    }
  }

  [DBMethod("Notification_RuleFilter")]
  [DBMethodBody(DBMS.SqlServer, "\r\nselect * \r\ninto #RuleFilterNotificationsAll\r\nfrom Notification n with(nolock)\r\nwhere AccountID = @AccountID\r\nAND (n.IsDeleted = 0 OR n.IsDeleted IS NULL)\r\n;\r\n\r\n--First DataSet Returned is the Count of all Notifications for account (Not Deleted)\r\nselect count(*) as TotalNotifications\r\nfrom #RuleFilterNotificationsAll\r\n;\r\n\r\nselect * \r\ninto #RuleFilterNotifications\r\nfrom #RuleFilterNotificationsAll n\r\nwhere AccountID = @AccountID\r\nand (\r\n\t\t(@IsActive is null) \r\n\t\tor \r\n\t\t(@IsActive is not null AND n.IsActive = @IsActive)\r\n\t)\r\nand (\r\n\t\t(@eNotificationClass is null) \r\n\t\tor \r\n\t\t(@eNotificationClass is not null AND n.eNotificationClass = @eNotificationClass)\r\n\t)\r\nand (\r\n\t\t(@Name is null) \r\n\t\tor \r\n\t\t(@Name is not null AND n.Name like '%' + @Name + '%')\r\n\t)\r\n;\r\n\r\n\r\nWITH NtfcsTrgrd AS (\r\n\tSELECT t.*\r\n\tFROM #RuleFilterNotifications n\r\n\tJOIN NotificationTriggered t  WITH(Nolock, FORCESEEK)\r\n\t\tON n.NotificationID = t.NotificationID\r\n\tWHERE 1 = 1 \r\n\t\tAND t.StartTime IS NOT NULL\r\n\t\tAND (t.resetTime IS NULL OR t.AcknowledgedTime IS NULL)\r\n)\r\n\r\n, NtfcsTrgrdIdx AS (\r\n\tSELECT \r\n\tROW_NUMBER() OVER (\r\n\t\t\t\t\tPARTITION BY\r\n\t\t\t\t\t\tNotificationID\r\n\t\t\t\t\tORDER BY \r\n\t\t\t\t\t\tNotificationTriggeredID DESC            \r\n\t\t\t\t) AS Idx\r\n\t, * \r\n\tFROM NtfcsTrgrd\r\n)\r\n\r\nSELECT ntidx.*\r\nINTO #RuleFilterNotificationsTriggered\r\nFROM NtfcsTrgrdIdx ntidx\r\nWHERE Idx = 1\r\nORDER BY NotificationID\r\n;\r\n\r\nIF @IsAlertingOnly IS NOT NULL AND  @IsAlertingOnly = 1\r\n\tSELECT rfn.* \r\n\tFROM #RuleFilterNotifications AS rfn\r\n\tINNER JOIN #RuleFilterNotificationsTriggered AS rfnt\r\n\ton rfn.NotificationID = rfnt.NotificationID\r\n\t;\r\nELSE\r\n\tSELECT * FROM #RuleFilterNotifications;\r\n;\r\n\r\nSELECT * FROM #RuleFilterNotificationsTriggered\r\n;\r\n\r\nDROP TABLE IF EXISTS #RuleFilterNotificationsAll;\r\nDROP TABLE IF EXISTS #RuleFilterNotifications;\r\nDROP TABLE IF EXISTS #RuleFilterNotificationsTriggered;\r\n")]
  internal class RuleFilter : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("IsActive", typeof (bool))]
    public bool? IsActive { get; private set; }

    [DBMethodParam("ENotificationClass", typeof (int))]
    public int? ENotificationClass { get; private set; }

    [DBMethodParam("Name", typeof (string))]
    public string Name { get; private set; }

    [DBMethodParam("IsAlertingOnly", typeof (bool))]
    public bool? IsAlertingOnly { get; private set; }

    public Monnit.Notification.RuleFilterResult Result { get; private set; }

    public RuleFilter(
      long accountID,
      bool? isActive,
      int? eNotificationClass,
      string name,
      bool? isAlertingOnly)
    {
      this.AccountID = accountID;
      this.IsActive = isActive;
      this.ENotificationClass = eNotificationClass;
      this.Name = name;
      this.IsAlertingOnly = isAlertingOnly;
      DataSet dataSet = this.ToDataSet();
      DataTable table1 = dataSet.Tables[0];
      DataTable table2 = dataSet.Tables[1];
      DataTable table3 = dataSet.Tables[2];
      long totalNotifications = 0;
      List<Monnit.Notification> notifications = new List<Monnit.Notification>();
      List<Monnit.NotificationTriggered> notificationsTriggered = new List<Monnit.NotificationTriggered>();
      if (table1 != null && table1.Rows != null && table1.Rows[0] != null && table1.Rows[0].ItemArray != null && table1.Rows[0].ItemArray[0] != null)
        totalNotifications = table1.Rows[0].ItemArray[0].ToLong();
      if (table2 != null)
        notifications = BaseDBObject.Load<Monnit.Notification>(table2);
      if (table3 != null)
        notificationsTriggered = BaseDBObject.Load<Monnit.NotificationTriggered>(table3);
      this.Result = new Monnit.Notification.RuleFilterResult(totalNotifications, notifications, notificationsTriggered);
    }
  }

  [DBMethod("Notification_RuleFilterHomePage")]
  [DBMethodBody(DBMS.SqlServer, "\r\nselect * \r\ninto #RuleFilterNotifications\r\nfrom Notification n\r\nwhere AccountID = @AccountID\r\nAND (n.IsDeleted = 0 OR n.IsDeleted IS NULL)\r\nAND n.IsActive = 1\r\n;\r\n\r\n\r\n\r\nSELECT ROW_NUMBER() OVER (\r\n\t\t\t\t\tPARTITION BY\r\n\t\t\t\t\t\tt.NotificationID\r\n\t\t\t\t\tORDER BY \r\n\t\t\t\t\t\tt.NotificationTriggeredID DESC            \r\n\t\t\t\t) AS Idx\r\n\t,\r\n\tt.*\r\n\tINTO #RuleFilterNotificationsTriggered\r\n\tFROM #RuleFilterNotifications n\r\n\tINNER JOIN NotificationTriggered t  --WITH(FORCESEEK)\r\n\t\tON n.NotificationID = t.NotificationID\r\n\tWHERE 1 = 1 \r\n\t\tAND t.StartTime IS NOT NULL\r\n\t\tAND (t.resetTime IS NULL OR t.AcknowledgedTime IS NULL)\r\n\r\nselect * \r\nfrom #RuleFilterNotifications n\r\nINNER JOIN #RuleFilterNotificationsTriggered nt\r\non n.notificationid = nt.notificationid\r\nWHERE nt.Idx = 1\r\nORDER BY nt.NotificationID\r\n\r\nSELECT \r\n[NotificationTriggeredID],\r\n[NotificationID],\r\n[StartTime],\r\n[AcknowledgedTime],\r\n[AcknowledgedBy],\r\n[SensorNotificationID],\r\n[GatewayNotificationID],\r\n[Reading],\r\n[ReadingDate],\r\n[AcknowledgeMethod],\r\n[resetTime],\r\n[OriginalReading],\r\n[OriginalReadingDate],\r\n[HasNote]\r\nFROM #RuleFilterNotificationsTriggered nt\r\nWHERE nt.Idx = 1\r\nORDER BY nt.NotificationID\r\n\r\nDROP TABLE IF EXISTS #RuleFilterNotifications;\r\nDROP TABLE IF EXISTS #RuleFilterNotificationsTriggered;\r\n")]
  internal class RuleFilterHomePage : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public Monnit.Notification.RuleFilterResult Result { get; private set; }

    public RuleFilterHomePage(long accountID)
    {
      this.AccountID = accountID;
      DataSet dataSet = this.ToDataSet();
      DataTable table1 = dataSet.Tables[0];
      DataTable table2 = dataSet.Tables[1];
      long totalNotifications = 0;
      List<Monnit.Notification> notifications = new List<Monnit.Notification>();
      List<Monnit.NotificationTriggered> notificationsTriggered = new List<Monnit.NotificationTriggered>();
      if (table1 != null && table1.Rows != null && table1.Rows.Count > 0 && table1.Rows[0] != null && table1.Rows[0].ItemArray != null && table1.Rows[0].ItemArray[0] != null)
        notifications = BaseDBObject.Load<Monnit.Notification>(table1);
      if (table2 != null && table2.Rows != null && table2.Rows.Count > 0)
        notificationsTriggered = BaseDBObject.Load<Monnit.NotificationTriggered>(table2);
      this.Result = new Monnit.Notification.RuleFilterResult(totalNotifications, notifications, notificationsTriggered);
    }
  }

  [DBMethod("Notification_LoadAutomatedNotification")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  n.*,\r\n  a.[AutomatedNotificationID],\r\n  a.[LastProcessDate],\r\n  a.[ProcessFrequency],\r\n  a.[TriggerDate],\r\n  a.[AutoReset],\r\n  a.[Description],\r\n  a.[ExternalID]\r\nFROM dbo.[Notification] n WITH (NOLOCK)\r\nINNER JOIN dbo.[AutomatedNotification] a WITH (NOLOCK) ON a.[NotificationID] = n.[NotificationID]\r\nWHERE  n.[IsDeleted] = 0\r\n  AND (a.[LastProcessDate] IS NULL OR GETUTCDATE() > DATEADD(MINUTE, a.[ProcessFrequency],a.[LastProcessDate]))\r\nORDER BY\r\n  n.[NotificationID]\r\nOPTION (optimize FOR UNKNOWN)  ;\r\n")]
  internal class LoadAutomatedNotification : BaseDBMethod
  {
    public DataTable Result { get; private set; }

    public LoadAutomatedNotification() => this.Result = this.ToDataTable();
  }

  [DBMethod("Notification_NotificationsForDatum")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Notification]\r\nWHERE NotificationID IN (\r\n                          SELECT\r\n                            NotificationID\r\n                          FROM dbo.[SensorNotification]\r\n                          WHERE SensorID   = @SensorID\r\n                            AND DatumIndex = @DatumIndex\r\n                        );\r\n")]
  internal class NotificationsForDatum : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("DatumIndex", typeof (long))]
    public long DatumIndex { get; private set; }

    public List<Monnit.Notification> Result { get; private set; }

    public NotificationsForDatum(long sensorID, int datumindex)
    {
      this.SensorID = sensorID;
      this.DatumIndex = (long) datumindex;
      this.Result = BaseDBObject.Load<Monnit.Notification>(this.ToDataTable());
    }
  }
}
