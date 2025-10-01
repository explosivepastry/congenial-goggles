// Decompiled with JetBrains decompiler
// Type: Monnit.Data.NotificationRecorded
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

internal class NotificationRecorded
{
  [DBMethod("Notification_ClearMessagesBySentToDeviceID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE dbo.[NotificationRecorded]\r\nSET\r\n  [Delivered] = 1,\r\n  [Status]    = 'Cancelled'\r\nWHERE SentToDeviceID = @DeviceID\r\n  AND (Delivered IS NULL OR Delivered = 0);\r\n")]
  internal class ClearMessagesBySentToDeviceID : BaseDBMethod
  {
    [DBMethodParam("DeviceID", typeof (long))]
    public long DeviceID { get; private set; }

    public bool Result { get; private set; }

    public ClearMessagesBySentToDeviceID(long deviceID)
    {
      try
      {
        this.DeviceID = deviceID;
        this.Execute();
        this.Result = true;
      }
      catch
      {
        this.Result = false;
      }
    }
  }

  [DBMethod("Notification_LoadRangeForNotificationRecordedBySensor")]
  [DBMethodBody(DBMS.SqlServer, "\t\r\nDECLARE @_SensorID BIGINT;\r\nSET @_SensorID = @SensorID;\r\n\r\nSELECT TOP(@Limit)\r\n  *\r\nFROM dbo.[NotificationRecorded] WITH (NOLOCK)\r\nWHERE SensorID         =  @_SensorID\r\n  AND NotificationDate >= @From\r\n  AND NotificationDate <= @To\r\nORDER BY\r\n  NotificationDate DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE SensorID = @SensorID AND NotificationDate >= '@From' AND NotificationDate <= '@To' ORDER BY NotificationDate Desc limit @Limit ;")]
  internal class LoadRangeForNotificationRecordedBySensor : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("From", typeof (DateTime))]
    public DateTime From { get; private set; }

    [DBMethodParam("To", typeof (DateTime))]
    public DateTime To { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadRangeForNotificationRecordedBySensor(
      long sensorID,
      DateTime from,
      DateTime to,
      int limit)
    {
      this.SensorID = sensorID;
      this.From = from;
      this.To = to;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadLastByNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP(@Count)\r\n  *\r\nFROM dbo.[NotificationRecorded] WITH (NOLOCK)\r\nWHERE NotificationID = @NotificationID\r\nORDER BY\r\n  NotificationDate DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE NotificationID = @NotificationID LIMIT @Count")]
  internal class LoadLastByNotificationID : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadLastByNotificationID(long notificationID, int count)
    {
      this.NotificationID = notificationID;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadLastBySensorID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @_SensorID BIGINT\r\n  SET @_SensorID = @SensorID\r\n\r\nSELECT TOP (@Count)\r\n  *\r\nFROM dbo.[NotificationRecorded]\r\nWHERE SensorID = @_SensorID\r\nORDER BY NotificationDate DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE SensorID = @SensorID LIMIT @Count")]
  internal class LoadLastBySensorID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadLastBySensorID(long sensorID, int count)
    {
      this.SensorID = sensorID;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadRecentBySensorIDAndNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP (@Count)\r\n  *\r\nFROM dbo.[NotificationRecorded]\r\nWHERE NotificationID = @NotificationID\r\n  AND SensorID       = @SensorID\r\nORDER BY\r\n  NotificationDate DESC\r\nOPTION(OPTIMIZE FOR UNKNOWN);\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE SensorID = @SensorID AND NotificationID = @NotificationID LIMIT @Count")]
  internal class LoadRecentBySensorIDAndNotificationID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadRecentBySensorIDAndNotificationID(long sensorID, long notificationID, int count)
    {
      this.SensorID = sensorID;
      this.Count = (long) count;
      this.NotificationID = notificationID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadBySensorAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSET ROWCOUNT @Count;\r\n\r\nSELECT\r\n  *,\r\n  [RowNum] = ROW_NUMBER() OVER(ORDER BY NotificationRecordedID DESC)\r\nFROM dbo.[NotificationRecorded] WITH(NOLOCK)\r\nWHERE SensorID = @SensorID\r\n  AND NotificationDate BETWEEN @FromDate AND @ToDate;\r\n\r\nSET ROWCOUNT 0; --Turn off\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE SensorID = @SensorID AND NotificationDate BETWEEN @FromDate AND @ToDate LIMIT @Count")]
  internal class LoadBySensorAndDateRange : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadBySensorAndDateRange(long sensorID, DateTime fromDate, DateTime toDate, int count)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadByNotificationAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM(\r\n      SELECT\r\n        *,\r\n        [RowNum] = ROW_NUMBER() OVER(ORDER BY [NotificationRecordedID] DESC) \r\n      FROM dbo.[NotificationRecorded] \r\n      WHERE NotificationID = @NotificationID\r\n        AND NotificationDate BETWEEN @FromDate AND @ToDate\r\n    ) sub\r\nWHERE [RowNum] <= @Count \r\nORDER BY [NotificationRecordedID] DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE NotificationID = @NotificationID AND NotificationDate BETWEEN @FromDate AND @ToDate LIMIT @Count")]
  internal class LoadByNotificationAndDateRange : BaseDBMethod
  {
    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadByNotificationAndDateRange(
      long notificationID,
      DateTime fromDate,
      DateTime toDate,
      int count)
    {
      this.NotificationID = notificationID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadLastByGatewayID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n*\r\nFROM (\r\n      SELECT \r\n        *,\r\n        [RowNum] = ROW_NUMBER() OVER(ORDER BY [NotificationRecordedID] DESC) \r\n      FROM dbo.[NotificationRecorded]\r\n      WHERE GatewayID = @GatewayID\r\n     ) sub\r\nWHERE [RowNum] <= @Count \r\nORDER BY [NotificationRecordedID] DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE GatewayID = @GatewayID LIMIT @Count")]
  internal class LoadLastByGatewayID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadLastByGatewayID(long gatewayID, int count)
    {
      this.GatewayID = gatewayID;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadRecentByGatewayIDAndNotificationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM (SELECT\r\n        *,\r\n        RowNum = ROW_NUMBER() OVER(ORDER BY NotificationRecordedID DESC)\r\n      FROM NotificationRecorded \r\n      WHERE GatewayID      = @GatewayID\r\n        AND NotificationID = @NotificationID) sub\r\nWHERE RowNum <= @Count \r\nORDER BY NotificationRecordedID DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE GatewayID = @GatewayID and NotificationID = @NotificationID LIMIT @Count")]
  internal class LoadRecentByGatewayIDAndNotificationID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("NotificationID", typeof (long))]
    public long NotificationID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadRecentByGatewayIDAndNotificationID(long gatewayID, long notificationID, int count)
    {
      this.GatewayID = gatewayID;
      this.Count = (long) count;
      this.NotificationID = notificationID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadByGatewayAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  * \r\nFROM (SELECT\r\n        *,\r\n        RowNum = ROW_NUMBER() OVER(ORDER BY NotificationRecordedID DESC) \r\n      FROM dbo.[NotificationRecorded]\r\n      WHERE GatewayID = @GatewayID\r\n        AND NotificationDate BETWEEN @FromDate AND @ToDate) sub\r\nWHERE RowNum <= @Count \r\nORDER BY NotificationRecordedID DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE GatewayID = @GatewayID AND NotificationDate BETWEEN @FromDate AND @ToDate LIMIT @Count")]
  internal class LoadByGatewayAndDateRange : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadByGatewayAndDateRange(
      long gatewayID,
      DateTime fromDate,
      DateTime toDate,
      int count)
    {
      this.GatewayID = gatewayID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadLastByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM (SELECT\r\n        nr.*,\r\n        RowNum = ROW_NUMBER() OVER(ORDER BY NotificationRecordedID DESC)\r\n      FROM dbo.[NotificationRecorded] nr\r\n      INNER JOIN dbo.[Sensor] s ON s.SensorID = nr.SensorID\r\n      WHERE s.AccountID = @AccountID) sub\r\nWHERE RowNum <= @Count\r\nORDER BY NotificationRecordedID DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT nr.* FROM NotificationRecorded nr INNER JOIN Sensor s on s.SensorID = nr.SensorID WHERE s.AccountID = @AccountID ORDER BY NotificationRecordedID DESC LIMIT @Count")]
  internal class LoadLastByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadLastByAccountID(long accountID, int count)
    {
      this.AccountID = accountID;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadByAccountAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP (@Count)\r\n  nr.NotificationRecordedID\r\nINTO #TempNoti\r\nFROM dbo.[NotificationRecorded] nr WITH (NOLOCK)\r\nINNER JOIN dbo.[Notification] n WITH (NOLOCK) on n.NotificationID = nr.NotificationID\r\nWHERE n.AccountID = @AccountID\r\n  AND nr.NotificationDate BETWEEN @FromDate AND @ToDate\r\nORDER BY nr.NotificationDate DESC\r\nOPTION (OPTIMIZE FOR UNKNOWN)\r\n\r\nSELECT\r\n  ROW_NUMBER() OVER(ORDER BY nr.NotificationRecordedID DESC) AS 'RowNum',\r\n  nr.*\r\n--INTO #Temp\r\nFROM dbo.[NotificationRecorded] nr WITH (NOLOCK)\r\nINNER JOIN #TempNoti n ON n.NotificationRecordedID = nr.NotificationRecordedID\r\nOPTION (OPTIMIZE FOR UNKNOWN);\r\n")]
  internal class LoadByAccountAndDateRange : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadByAccountAndDateRange(
      long accountID,
      DateTime fromDate,
      DateTime toDate,
      int count)
    {
      this.AccountID = accountID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadRecentByCSNetID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @_CSNetID BIGINT\r\nSET @_CSNetID = @CSNetID\r\n\r\nSELECT TOP (@Count)\r\nnr.NotificationRecordedID\r\nINTO #Temp1\r\nFROM dbo.[Sensor] s WITH (NOLOCK)\r\nINNER JOIN dbo.[NotificationRecorded] nr WITH (NOLOCK) ON s.SensorID = nr.SensorID\r\nWHERE s.CSNetID = @_CSNetID\r\n  AND nr.NotificationDate > ISNULL(s.StartDate, '2019-01-01') \r\nORDER BY NotificationDate DESC\r\n\r\n\r\nSELECT \r\nnr.*, \r\nRowNum = ROW_NUMBER() OVER (PARTITION BY 1 ORDER BY nr.NotificationRecordedID)\r\nFROM dbo.[NotificationRecorded] nr WITH (NOLOCK)\r\nINNER JOIN #Temp1 t ON nr.NotificationRecordedID = t.NotificationRecordedID\r\nORDER BY nr.NotificationDate desc\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT nr.* FROM NotificationRecorded nr INNER JOIN Sensor s on s.SensorID = nr.SensorID WHERE s.CSNetID = @CSNetID ORDER BY NotificationRecordedID DESC LIMIT @Count")]
  internal class LoadRecentByCSNetID : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public long Count { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadRecentByCSNetID(long csnetID, int count)
    {
      this.CSNetID = csnetID;
      this.Count = (long) count;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadEmailRetries")]
  [DBMethodBody(DBMS.SqlServer, "\r\n    DECLARE @PROCNAME NVARCHAR(50);\r\n\r\n    DECLARE @ERRORNUM       INT,\r\n            @ERRORPROCEDURE NVARCHAR(50),\r\n            @ERRORSYSMSG    NVARCHAR(MAX);\r\n\r\n    CREATE TABLE #TEMPNOTI\r\n      (\r\n         Notificationrecordedid BIGINT PRIMARY KEY CLUSTERED\r\n      );\r\n\r\n  BEGIN TRY\r\n      \r\n\t  SET @PROCNAME = Object_name(@@PROCID);\r\n      SET @CUTOFF = Dateadd(HOUR, -2, Getutcdate());\r\n\t  if(@CUTOFF < '2024-08-02 17:30')\r\n\t  Begin\r\n\t\tSET @CUTOFF = '2024-08-02 17:30';\r\n\t  END\r\n\r\n\t  --DECLARE @Cutoff DATETIME = DATEADD(HOUR, -1, GETUTCDATE())\r\n\t\t\t\t\t\t\t\t\t\t\t\t  \r\n\r\n      DECLARE @MINQUEUE DATETIME = Dateadd(MINUTE, -3, Getutcdate());\r\n\r\n      INSERT INTO #TEMPNOTI\r\n                  ([Notificationrecordedid])\r\n      SELECT [Notificationrecordedid]\r\n\t \r\n\t\t   \r\n\t\t\t\t\t\t\t  \r\n      FROM   DBO.[Notificationrecorded] WITH (NOLOCK)\r\n      WHERE  Notificationdate > @CUTOFF AND Notificationdate < @MINQUEUE;\r\n\t\t\t\t\t\t\t\t  \r\n\r\n      --Untill code fixes these clear messages that don't need to be resent\r\n      UPDATE nr\r\n      SET    DoRetry = 0\r\n      FROM   DBO.[Notificationrecorded] as nr\r\n             INNER JOIN #TEMPNOTI as i\r\n                     ON nr.[Notificationrecordedid] = i.[Notificationrecordedid]\r\n      WHERE  nr.[Status] LIKE '%Opted Out'\r\n             AND nr.[Doretry] = 1;\r\n\r\n      PRINT 'Opt Out';\r\n\r\n      UPDATE nr\r\n      SET    DoRetry = 0\r\n      FROM   DBO.[Notificationrecorded] as nr\r\n             INNER JOIN #TEMPNOTI as i\r\n                     ON nr.[Notificationrecordedid] = i.[Notificationrecordedid]\r\n      WHERE  nr.[Status] LIKE '%delivered:%'\r\n             AND nr.[Doretry] = 1;\r\n\r\n      PRINT 'delevered';\r\n\r\n      UPDATE nr\r\n      SET    DoRetry = 0\r\n      FROM   DBO.[Notificationrecorded] as nr\r\n             INNER JOIN #TEMPNOTI as i\r\n                     ON nr.[Notificationrecordedid] = i.[Notificationrecordedid]\r\n      WHERE  nr.[Status] LIKE '%completed:%'\r\n             AND nr.[Doretry] = 1;\r\n\r\n      PRINT 'completed';\r\n\r\n\t\t  \r\n\t  UPDATE nr\r\n      SET    DoRetry = 0\r\n      FROM   DBO.[Notificationrecorded] as nr\r\n             INNER JOIN #TEMPNOTI as i\r\n                     ON nr.[Notificationrecordedid] = i.[Notificationrecordedid]\r\n      WHERE  nr.[Status] LIKE '%limit exceeded%'\r\n             AND nr.[Doretry] = 1;\r\n\r\n      PRINT 'limit';\r\n\r\n      UPDATE nr\r\n      SET    DoRetry = 0\r\n      FROM   DBO.[Notificationrecorded] as nr\r\n             INNER JOIN #TEMPNOTI as i\r\n                     ON nr.[Notificationrecordedid] = i.[Notificationrecordedid]\r\n      WHERE  nr.[Status] LIKE '%delayed%'\r\n             AND nr.[Doretry] = 1;\r\n\r\n      PRINT 'delayed';\r\n\t\t\t\t\t\t\t\t\t\t \r\n\t\t\t\t\t\t\t\t\t\r\n\r\n      SELECT top 200 nr.*\r\n      FROM   DBO.[Notificationrecorded] as nr WITH (NOLOCK)\r\n             INNER JOIN #TEMPNOTI as i\r\n                     ON nr.[Notificationrecordedid] = i.[Notificationrecordedid]\r\n      WHERE  nr.[Doretry] = 1\r\n             AND nr.[Retrycount] < 3\r\n             AND Dateadd(MINUTE, nr.[Retrycount] * 4 + 4, Notificationdate) < Getutcdate();\r\n  END TRY\r\n\r\n  BEGIN CATCH\r\n      SET @ERRORNUM = Error_number();\r\n      SET @ERRORPROCEDURE = Error_procedure();\r\n      SET @ERRORSYSMSG = Error_message();\r\n\r\n      DECLARE @RECIPIENTS VARCHAR(500)\r\n      DECLARE @SUBJECT VARCHAR(30)\r\n      DECLARE @BODY VARCHAR(2000)\r\n      DECLARE @PARAM VARCHAR(1000)\r\n\r\n      SET @PARAM = '@Cutoff: ' + CONVERT(VARCHAR(100), @CUTOFF)\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t  \r\n\t\t\t\t\t\t\t  \r\n\r\n      INSERT INTO Dberrorlog\r\n                  (Procname,\r\n                   [Date],\r\n                   Urgency,\r\n                   [Message],\r\n                   Params)\r\n      VALUES      (@PROCNAME,\r\n                   Getutcdate(),\r\n                   1,\r\n                   @ERRORSYSMSG,\r\n                   @PARAM)\r\n\r\n      SET @BODY = '<p>Team, </p> <p>Critical Procedure Failed: '\r\n                  + @PROCNAME + '. Please Address. '\r\n                  + CONVERT(VARCHAR(20), Getdate() )\r\n                  + ' </p>    <p>ErrorMessage: '\r\n                  + CONVERT(VARCHAR(20), @ERRORNUM) + ' '\r\n                  + @ERRORSYSMSG\r\n                  + '</p> <p>Sincerely,</p><p>-DBA</p>'\r\n      SET @SUBJECT = 'MonnitDB ProcFail - Urgency 1'\r\n      SET @RECIPIENTS = (SELECT Value\r\n                         FROM   Configdata\r\n                         WHERE  Keyname = 'DB_Procfail_Contacts')\r\n\r\n      EXEC MSDB.DBO.Sp_send_dbmail\r\n        @PROFILE_NAME = 'Alerts',\r\n        @RECIPIENTS = @RECIPIENTS,\r\n        @SUBJECT = @SUBJECT,\r\n        @BODY = @BODY,\r\n        @BODY_FORMAT = 'HTML';\r\n\r\n  END CATCH\r\n")]
  internal class LoadEmailRetries : BaseDBMethod
  {
    [DBMethodParam("Cutoff", typeof (DateTime), DefaultValue = "1/1/2016")]
    public DateTime Cutoff { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadEmailRetries()
    {
      this.Cutoff = DateTime.Now.AddDays(-1.0);
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_Last7Days")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  [count] = COUNT(c.[NotificationRecordedID]),\r\n  [date]  = Convert(VARCHAR,d.[Dates],101)\r\nFROM (\r\n      SELECT DATEADD(DAY,-6,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-5,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-4,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-3,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-2,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-1,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY, 0,GETUTCDATE()) AS [Dates]\r\n     ) d \r\nLEFT JOIN(SELECT\r\n            nr.[NotificationRecordedID], \r\n            [nrdate]                       = CONVERT(VARCHAR,DATEADD(HOUR,@Offset,nr.[NotificationDate]),101) \r\n          FROM dbo.[NotificationRecorded] nr\r\n          INNER JOIN dbo.[Sensor] s ON s.SensorID = nr.SensorID \r\n          WHERE s.[AccountID] = @AccountID) c ON c.[nrdate] = CONVERT(VARCHAR,d.[Dates],101)\r\nGROUP BY CONVERT(VARCHAR,d.[Dates],101);\r\n")]
  internal class Last7Days : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("Offset", typeof (int))]
    public int Offset { get; private set; }

    public DataTable Result { get; private set; }

    public Last7Days(long accountID, int offset)
    {
      this.AccountID = accountID;
      this.Offset = offset;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("NotificationRecorded_GetMessageForDevice")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ProcName NVARCHAR(50);\r\n\r\nDECLARE @ErrorNum         INT,          \r\n        @ErrorProcedure   NVARCHAR(50), \r\n        @ErrorSysMsg      NVARCHAR(MAX);\r\n\r\nDECLARE @_SensorID BIGINT;\r\n  SET @_SensorID = @SensorID;\r\n\r\nBEGIN TRY\r\n\r\n    SET @ProcName = OBJECT_NAME(@@PROCID);\r\n\r\n    CREATE TABLE #IDs (\r\n      NotificationRecordedID BIGINT PRIMARY KEY CLUSTERED\r\n    );\r\n\r\n    INSERT INTO #IDs (\r\n      NotificationRecordedID\r\n    )\r\n    SELECT\r\n      NotificationRecordedID\r\n    FROM dbo.[NotificationRecorded] WITH(NOLOCK)\r\n    WHERE SentToDeviceID = @_SensorID\r\n      AND AcknowledgedDate IS NULL;\r\n    --OPTION(OPTIMIZE FOR UNKNOWN);\r\n\r\n    SELECT\r\n      n.*\r\n    FROM dbo.[NotificationRecorded] n WITH(NOLOCK)\r\n    INNER JOIN #IDs t ON t.NotificationRecordedID = n.NotificationRecordedID\r\n    WHERE n.Delivered = 0;\r\n    --OPTION(OPTIMIZE FOR UNKNOWN);\r\n\r\n    --SELECT\r\n    --  *\r\n    --FROM dbo.NotificationRecorded WITH(NOLOCK)\r\n    --WHERE SentToDeviceID = @SensorID\r\n    --  AND AcknowledgedDate IS NULL\r\n    --  AND Delivered = 0;\r\n\r\n    --LocalNotififierDisplay (datamessage), SerialBridgeTerminal\r\n    UPDATE nr\r\n      SET nr.[AcknowledgedDate] = GETUTCDATE(),\r\n          nr.[Status]           = ISNULL(nr.[Status], '') + ' - System Acknowledged'\r\n    FROM dbo.[NotificationRecorded] nr\r\n    INNER JOIN #IDs i ON nr.[NotificationRecordedID] = i.[NotificationRecordedID]\r\n    WHERE nr.[eNotificationType] in (10,11)\r\n      AND nr.[Delivered] = 1;\r\n\r\n    --SysAction\r\n    UPDATE nr\r\n      SET nr.[AcknowledgedDate] = GETUTCDATE(),\r\n          nr.[Status]           = ISNULL(nr.[Status], '') + ' - System Acknowledged'\r\n    FROM dbo.[NotificationRecorded] nr\r\n    INNER JOIN #IDs i ON nr.[NotificationRecordedID] = i.[NotificationRecordedID]\r\n    WHERE nr.[eNotificationType] in (12);\r\n\r\n    --LocalNotifier (alerts), Control\r\n    UPDATE nr\r\n      SET nr.[AcknowledgedDate] = GETUTCDATE(),\r\n          nr.[Status]           = ISNULL(nr.[Status], '') + ' - System Acknowledged'\r\n    FROM dbo.[NotificationRecorded] nr\r\n    INNER JOIN #IDs i ON nr.[NotificationRecordedID] = i.[NotificationRecordedID]\r\n    WHERE nr.[eNotificationType] in (4,5)\r\n      AND nr.[Delivered] = 1\r\n      AND DATEDIFF(HOUR, nr.[NotificationDate], GETUTCDATE()) > 24;\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n\tSET @ErrorNum = ERROR_NUMBER();\r\n\tSET @ErrorProcedure = ERROR_PROCEDURE();\r\n\tSET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n\tDECLARE @Recipients varchar(500)\r\n\tDECLARE @Subject varchar(30)\r\n\tDECLARE @Body VARCHAR(2000)\r\n\r\n  DECLARE @Param VARCHAR(1000)\r\n  SET @Param = '@SensorID: ' + CONVERT(VARCHAR(100), @SensorID)\r\n\r\n  INSERT INTO DBErrorLog (ProcName, Date, Urgency, Message, Params)\r\n  VALUES (@ProcName, GETUTCDATE(), 2, @ErrorSysMsg, @Param)\r\n\r\n\tSET @Body = '<p>Team, </p> <p>Critical Procedure Failed: '+@ProcName+'. Please Address. '+CONVERT(VARCHAR(20), GETDATE() )+' </p> \r\n  <p>ErrorMessage: '+CONVERT(VARCHAR(20), @ErrorNum) +' ' + @ErrorSysMsg+'</p>\r\n\t<p>Sincerely,</p><p>-DBA</p>'\r\n\r\n\tSET @Subject = 'MonnitDB ProcFail - Urgency 2'\r\n\tSET @Recipients = (select value from ConfigData where KeyName = 'DB_Procfail_Contacts')\r\n\r\n    EXEC msdb.dbo.sp_send_dbmail \r\n\t  @Profile_name = 'Alerts',\r\n\t  @Recipients = @Recipients , \r\n      @subject = @Subject,  \r\n      @body = @Body,  \r\n      @body_format = 'HTML' ;  \r\n\r\nEND CATCH\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE SentToDeviceID = @SensorID AND Delivered = 0 AND AcknowledgedDate IS NULL")]
  internal class GetMessageForDevice : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public GetMessageForDevice(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_GetNoneDeliveredMessages")]
  [DBMethodBody(DBMS.SqlServer, "\r\n--SELECT * \r\n--FROM NotificationRecorded with(NOLOCK) \r\n--WHERE SentToDeviceID = @SensorID \r\n--And (Delivered is null or Delivered = 0) \r\n--order by NotificationDate desc\r\n\r\nWITH CTE_Results AS\r\n(\r\n  SELECT\r\n    NotificationRecordedID\r\n  FROM dbo.[NotificationRecorded] WITH(NOLOCK)\r\n  WHERE SentToDeviceID = @SensorID\r\n    AND AcknowledgedDate IS NULL\r\n)\r\nSELECT\r\n  n.*\r\nFROM CTE_Results c\r\nINNER JOIN dbo.[NotificationRecorded] n WITH(NOLOCK) ON c.NotificationRecordedID = n.NotificationRecordedID\r\nWHERE n.Delivered = 0;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * \r\nFROM NotificationRecorded \r\nWHERE SentToDeviceID = @SensorID\r\nAnd (Delivered is null or Delivered = 0) \r\norder by NotificationDate desc\r\n")]
  internal class GetNoneDeliveredMessages : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public GetNoneDeliveredMessages(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_LoadNotificationForDeviceByQueueID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @_SensorID  BIGINT,\r\n        @_QueueID   INT;\r\n\r\nSET @_SensorID = @SensorID;\r\nSET @_QueueID = @QueueID;\r\n\r\nSELECT TOP 1\r\n  *\r\nFROM dbo.[NotificationRecorded] WITH (NOLOCK)\r\nWHERE SentToDeviceID = @_SensorID\r\n  AND QueueID = @_QueueID ORDER BY NotificationDate DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE SentToDeviceID = @SensorID AND QueueID = @QueueID ORDER BY NotificationRecordedID DESC")]
  internal class LoadNotificationForDeviceByQueueID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("QueueID", typeof (int))]
    public int QueueID { get; private set; }

    public Monnit.NotificationRecorded Result { get; private set; }

    public LoadNotificationForDeviceByQueueID(long sensorID, int queueID)
    {
      this.SensorID = sensorID;
      this.QueueID = queueID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable()).FirstOrDefault<Monnit.NotificationRecorded>();
    }
  }

  [DBMethod("NotificationRecorded_LoadNonAcknowledgedNotificationForDevice")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @_SensorID BIGINT\r\nSET @_SensorID = @SensorID;\r\n\r\nWITH CTE_Results AS\r\n(\r\n  SELECT\r\n    NotificationRecordedID\r\n  FROM dbo.[NotificationRecorded] WITH(NOLOCK)\r\n  WHERE SentToDeviceID = @_SensorID\r\n    AND AcknowledgedDate IS NULL\r\n)\r\nSELECT\r\n  *\r\nFROM dbo.[NotificationRecorded] n WITH(NOLOCK)\r\nINNER JOIN CTE_Results r ON n.NotificationRecordedID = r.NotificationRecordedID;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM NotificationRecorded WHERE SentToDeviceID = @SensorID AND AcknowledgedDate IS NULL")]
  internal class LoadNonAcknowledgedNotificationForDevice : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadNonAcknowledgedNotificationForDevice(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }

  [DBMethod("NotificationRecorded_Last7DaysByCSNetID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  [count] = COUNT(c.[NotificationRecordedID]),\r\n  [date]  = CONVERT(VARCHAR,d.[Dates],101)\r\nFROM (\r\n      SELECT DATEADD(DAY,-6,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-5,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-4,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-3,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-2,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY,-1,GETUTCDATE()) AS [Dates] UNION\r\n      SELECT DATEADD(DAY, 0,GETUTCDATE()) AS [Dates]\r\n     ) d \r\nLEFT JOIN(SELECT\r\n            nr.[NotificationRecordedID], \r\n            [nrdate]                       = CONVERT(VARCHAR,DATEADD(HOUR,@Offset,nr.[NotificationDate]),101) \r\n          FROM dbo.[NotificationRecorded] nr\r\n          INNER JOIN dbo.[Sensor] s ON s.SensorID = nr.SensorID \r\n          WHERE s.[CSNetID] = @CSNetID) c ON c.[nrdate] = CONVERT(VARCHAR,d.[Dates],101)\r\nGROUP BY CONVERT(VARCHAR,d.[Dates],101);\r\n")]
  internal class Last7DaysByCSNetID : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    [DBMethodParam("Offset", typeof (int))]
    public int Offset { get; private set; }

    public DataTable Result { get; private set; }

    public Last7DaysByCSNetID(long csnetID, int offset)
    {
      this.CSNetID = csnetID;
      this.Offset = offset;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("NotificationRecorded_LastQueueID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @_SensorID BIGINT;\r\nSET @_SensorID = @SensorID;\r\n\r\nSELECT TOP 1\r\n  QueueID\r\nFROM dbo.[NotificationRecorded] WITH (NOLOCK)\r\nWHERE SentToDeviceID = @_SensorID\r\nORDER BY\r\n  NotificationDate DESC,\r\n  QueueID DESC;\r\n--OPTION(OPTIMIZE FOR UNKNOWN);\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT QueueID FROM NotificationRecorded where SentToDeviceID = @SensorID order by NotificationDate desc, QueueID desc LIMIT 1")]
  internal class LastQueueID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public int Result { get; private set; }

    public LastQueueID(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = this.ToScalarValue<int>();
    }
  }

  [DBMethod("NotificationRecorded_LoadRecentForReseller")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @CompareDate DATETIME;\r\nSET @CompareDate = DATEADD(MINUTE,@Minutes * -1,GETUTCDATE());\r\n\r\n\r\nselect notificationrecordedid into #temp1 from notificationrecorded with (NOLOCK)\r\nwhere notificationdate >= @comparedate \r\n\r\nselect \r\nnr.*,\r\nn.*\r\nfrom dbo.[NotificationRecorded] nr with (NOLOCK) \r\ninner join #temp1 t on nr.notificationrecordedid = t.notificationrecordedid\r\nINNER JOIN dbo.[Notification] n WITH (NOLOCK) ON n.NotificationID = nr.NotificationID\r\nINNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.AccountID = n.AccountID \r\nwhere a.RetailAccountID = @ResellerAccountID\r\n\r\n\r\n--SELECT\r\n--  nr.*,\r\n--  n.*\r\n--FROM dbo.[NotificationRecorded] nr WITH (NOLOCK)\r\n--INNER JOIN dbo.[Notification] n WITH (NOLOCK) ON n.NotificationID = nr.NotificationID\r\n--INNER JOIN dbo.[Account] a WITH (NOLOCK) ON a.AccountID = n.AccountID \r\n--WHERE a.RetailAccountID         =  @ResellerAccountID \r\n--  AND nr.NotificationDate       >= @CompareDate\r\n--  AND nr.NotificationRecordedID >  @LastSentNotificationID \r\n--ORDER BY nr.NotificationDate\r\n--OPTION (OPTIMIZE FOR UNKNOWN);\r\n")]
  internal class LoadRecentForReseller : BaseDBMethod
  {
    [DBMethodParam("ResellerAccountID", typeof (long))]
    public long ResellerAccountID { get; private set; }

    [DBMethodParam("Minutes", typeof (int))]
    public int Minutes { get; private set; }

    [DBMethodParam("LastSentNotificationID", typeof (long))]
    public long LastSentNotificationID { get; private set; }

    public List<Monnit.NotificationRecorded> Result { get; private set; }

    public LoadRecentForReseller(long resellerAccountID, int minutes, long lastSentNotificationID)
    {
      this.ResellerAccountID = resellerAccountID;
      this.Minutes = minutes;
      this.LastSentNotificationID = lastSentNotificationID;
      this.Result = BaseDBObject.Load<Monnit.NotificationRecorded>(this.ToDataTable());
    }
  }
}
