// Decompiled with JetBrains decompiler
// Type: Monnit.Data.DataMessage
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

internal class DataMessage
{
  [DBMethod("Message_LoadLastBySensor")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  dm.* \r\nFROM dbo.[DataMessage_Last] dm WITH(NOLOCK)\r\nWHERE dm.SensorID = @SensorID;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM DataMessage WHERE SensorID = @SensorID ORDER BY MessageDate DESC LIMIT 1;")]
  internal class LoadLastBySensor : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public Monnit.DataMessage Result { get; private set; }

    public LoadLastBySensor(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable()).FirstOrDefault<Monnit.DataMessage>();
    }
  }

  [DBMethod("DataMessage_QuickLoad")]
  [DBMethodBody(DBMS.SqlServer, "\r\n--DECLARE @sql VARCHAR(3000)\r\n--SET @sql = \r\n--'SELECT\r\n-- dm.*\r\n--FROM dbo.[DataMessage] dm WITH(NOLOCK)\r\n--WHERE dm.SensorID = ' + CONVERT(VARCHAR(30), @SensorID) + '\r\n--  AND dm.MessageDate = ''' + CONVERT(VARCHAR(30), @LastCommunicationDate, 120) + '''\r\n--  AND dm.DataMessageGUID = ''' + CONVERT(VARCHAR(100), @LastDataMessageGUID) + ''';\r\n--'\r\n--EXEC (@sql);\r\n\r\n\r\nSELECT\r\n dm.*\r\nFROM dbo.[DataMessage_Last] dm WITH(NOLOCK)\r\nWHERE dm.SensorID = @SensorID\r\n  AND dm.MessageDate = @LastCommunicationDate\r\n  AND dm.DataMessageGUID = @LastDataMessageGUID;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM DataMessage dm WHERE SensorID = @SensorID AND dm.MessageDate = '@LastCommunicationDate' AND dm.DataMessageGUID = '@LastDataMessageGUID';")]
  internal class QuickLoad : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("LastDataMessageGUID", typeof (Guid))]
    public Guid LastDataMessageGUID { get; private set; }

    [DBMethodParam("LastCommunicationDate", typeof (DateTime))]
    public DateTime LastCommunicationDate { get; private set; }

    public Monnit.DataMessage Result { get; private set; }

    public QuickLoad(long sensorID, Guid lastDataMessageGUID, DateTime lastCommunicationDate)
    {
      this.SensorID = sensorID;
      this.LastDataMessageGUID = lastDataMessageGUID;
      this.LastCommunicationDate = lastCommunicationDate;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable()).FirstOrDefault<Monnit.DataMessage>();
    }
  }

  [DBMethod("Message_LoadBySensorAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/*********************************************************\r\n                      CHANGE LOG\r\n**********************************************************\r\n  Date          ModifiedBy        Comments\r\n  ------------  ----------------  ------------------------\r\n  6/29/2016     Greg Cardall      Created Proc\r\n  7/6/2016      Brandon Young     Written with dynamic sql generation\r\n                                  Instead of pulling from View\r\n  8/1/2016      Brandon Young     Updated @ToDate calculation from changing\r\n                                  by 1 month, to the last second of the previous month\r\n    \r\n**********************************************************\r\nDECLARE @SensorID         bigint\r\nDECLARE @FromDate         datetime\r\nDECLARE @ToDate           datetime\r\nDECLARE @Count            int\r\nDECLARE @DataMessageGUID  uniqueidentifier\r\nSET @SensorID         = 157664\r\nSET @FromDate         = '2016-12-12 00:00:00'\r\nSET @ToDate           = '2016-12-19 00:00:00'\r\nSET @Count            = 5000\r\nSET @DataMessageGUID  = NULL\r\nEXEC [dbo].[Message_LoadBySensorAndDateRange] @SensorID, @FromDate, @ToDate, @Count, @DataMessageGUID;\r\n*********************************************************/\r\n\r\nDECLARE @SQL        VARCHAR(500);\r\nDECLARE @Retreived  INT\r\nDECLARE @_SensorID BIGINT;\r\n\r\nSET @_SensorID = @SensorID\r\n\r\n--CREATE TABLE #MessageDate (MessageDate DATETIME, DataMessageGUID NVARCHAR(100));\r\n\r\n--If we have a valid @MessageID, use it\r\nIF @DataMessageGUID IS NOT NULL\r\nBEGIN\r\n\r\n    SELECT TOP 1\r\n      @ToDate = MessageDate\r\n    FROM dbo.[DataMessage] WITH (NOLOCK)\r\n    WHERE SensorID = @SensorID AND DataMessageGUID = @DataMessageGUID;\r\n\r\nEND\r\n\r\nSET @SQL =\r\n'SELECT TOP ('+CONVERT(VARCHAR(10), @Count)+')\r\n  *\r\nFROM dbo.[DataMessage] WITH (NOLOCK)\r\nWHERE SensorID    = '    + CONVERT(VARCHAR(30), @_SensorID) +'\r\n  AND MessageDate >= ''' + CONVERT(VARCHAR(30), @FromDate, 120) + '''\r\n  AND MessageDate <= ''' + CONVERT(VARCHAR(30), @ToDate, 120)   + '''\r\nORDER BY MessageDate DESC';\r\n\r\nEXEC (@SQL);\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM DataMessage WHERE SensorID = @SensorID AND MessageDate BETWEEN '@FromDate' AND '@ToDate' ORDER BY MessageDate DESC LIMIT @Count;")]
  internal class LoadBySensorAndDateRange : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public int Count { get; private set; }

    [DBMethodParam("DataMessageGUID", typeof (Guid))]
    public Guid DataMessageGUID { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public LoadBySensorAndDateRange(
      long sensorID,
      DateTime fromDate,
      DateTime toDate,
      int count,
      Guid? dataMessageGUID)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Count = count;
      this.DataMessageGUID = dataMessageGUID ?? Guid.Empty;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable());
    }
  }

  [DBMethod("Message_LoadMissedBySensorAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @_SensorID BIGINT;\r\nSET @_SensorID = @SensorID;\r\n\r\nWITH rows AS\r\n(\r\nSELECT\r\n  *,\r\n  rn = ROW_NUMBER() OVER (ORDER BY [MessageDate])\r\nFROM dbo.[DataMessage] WITH (NOLOCK)\r\nWHERE [SensorID]    = @_SensorID\r\n  AND [MessageDate] > @FromDate\r\n  AND [MessageDate] < @ToDate\r\n)\r\nSELECT\r\n  [MinutesBetween]      = DATEDIFF(MINUTE, mc.[MessageDate], mp.[MessageDate]),\r\n  [MissedMessageDate]   = CASE WHEN mc.[State] & 0x02 = 0x02\r\n                                THEN DATEADD(MINUTE, s.[ActiveStateInterval], mc.[MessageDate])\r\n                                ELSE DATEADD(MINUTE, s.[ReportInterval],      mc.[MessageDate])\r\n                          END\r\nFROM rows mc\r\nJOIN rows mp ON mc.[rn] = mp.[rn] - 1\r\nINNER JOIN Sensor s WITH (NOLOCK) on mc.[SensorID] = s.[SensorID]\r\nWhere (mc.[State] & 0x02 = 0x02\r\n  AND   DATEDIFF(SECOND, mc.[MessageDate], mp.[MessageDate]) > s.[ActiveStateInterval] * 60 * 1.25)\r\n   OR (mc.[State] & 0x02 = 0x00\r\n  AND   DATEDIFF(SECOND, mc.[MessageDate], mp.[MessageDate]) > s.[ReportInterval]      * 60 * 1.25)\r\nOPTION (OPTIMIZE FOR UNKNOWN);\r\n")]
  internal class LoadMissedBySensorAndDateRange : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public DataTable Result { get; private set; }

    public LoadMissedBySensorAndDateRange(long sensorID, DateTime fromDate, DateTime toDate)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Message_LoadForChart")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ActualCount  INT,\r\n        @Modulus      INT;\r\n\r\nCREATE TABLE #Messages(\r\n  [RowNumber]                     INT,\r\n  [MessageDate]                   DATETIME, \r\n  [SensorID]                      BIGINT, \r\n  [DataMessageGUID]               UNIQUEIDENTIFIER, \r\n  [State]                         INT,\r\n  [SignalStrength]                INT,\r\n  [LinkQuality]                   INT,\r\n  [Battery]                       INT,\r\n  [Data]                          VARCHAR(2000),\r\n  [Voltage]                       FLOAT,\r\n  [MeetsNotificationRequirement]  BIT,\r\n  [InsertDate]                    DATETIME,\r\n  [GatewayID]                     BIGINT,\r\n  [HasNote]                       BIT,\r\n  [IsAuthenticated]               BIT,\r\n  [ApplicationID]                 INT\r\n  );\r\n                                \t\r\nINSERT INTO #Messages (\r\n  [RowNumber],\r\n  [MessageDate],\r\n  [SensorID],\r\n  [DataMessageGUID],\r\n  [State],\r\n  [SignalStrength],\r\n  [LinkQuality],\r\n  [Battery],\r\n  [Data],\r\n  [Voltage],\r\n  [MeetsNotificationRequirement],\r\n  [InsertDate],\r\n  [GatewayID],\r\n  [HasNote],\r\n  [IsAuthenticated],\r\n  [ApplicationID]\r\n\t)\r\nSELECT\r\n  [RowNumber],\r\n  [MessageDate],\r\n  [SensorID],\r\n  [DataMessageGUID],\r\n  [State],\r\n  [SignalStrength],\r\n  [LinkQuality],\r\n  [Battery],\r\n  [Data],\r\n  [Voltage],\r\n  [MeetsNotificationRequirement],\r\n  [InsertDate],\r\n  [GatewayID],\r\n  [HasNote],\r\n  [IsAuthenticated],\r\n  [ApplicationID]\r\nFROM (\r\n      SELECT\r\n        [rownumber]     = ROW_NUMBER() OVER (ORDER BY [MessageDate]),\r\n        [MessageDate],\r\n        [SensorID],\r\n        [DataMessageGUID],\r\n        [State],\r\n        [SignalStrength],\r\n        [LinkQuality],\r\n        [Battery],\r\n        [Data],\r\n        [Voltage],\r\n        [MeetsNotificationRequirement],\r\n        [InsertDate],\r\n        [GatewayID],\r\n        [HasNote],\r\n        [IsAuthenticated],\r\n        [ApplicationID]\r\n      FROM dbo.[DataMessage] WITH(NOLOCK)\r\n      WHERE [SensorID] = @SensorID\r\n        AND [MessageDate] BETWEEN @FromDate AND @ToDate\r\n      ) AS temp\r\nOPTION(OPTIMIZE FOR UNKNOWN);\r\n\r\nSET @ActualCount = @@ROWCOUNT;\r\n\r\nSELECT @Modulus = CEILING(CONVERT(FLOAT,@ActualCount)/CONVERT(FLOAT,@MaxCount));\r\n\r\nSELECT\r\n  [RowNumber],\r\n  [MessageDate],\r\n  [SensorID],\r\n  [DataMessageGUID],\r\n  [State],\r\n  [SignalStrength],\r\n  [LinkQuality],\r\n  [Battery],\r\n  [Data],\r\n  [Voltage],\r\n  [MeetsNotificationRequirement],\r\n  [InsertDate],\r\n  [GatewayID],\r\n  [HasNote],\r\n  [IsAuthenticated],\r\n  [ApplicationID]\r\nFROM #Messages\r\nWHERE [RowNumber] % @Modulus = 0\r\nORDER BY MessageDate;\r\n")]
  internal class LoadForChart : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("MaxCount", typeof (int))]
    public int MaxCount { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public LoadForChart(long sensorID, DateTime fromDate, DateTime toDate, int maxCount)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.MaxCount = maxCount;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable());
    }
  }

  [DBMethod("Message_LoadAllForChart")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @SQL VARCHAR(1000)\r\n\r\nSET @SQL =\r\n'SELECT TOP 10000\r\n  dm.[MessageDate],\r\n  dm.[SensorID],\r\n  dm.[DataMessageGUID],\r\n  dm.[State],\r\n  dm.[SignalStrength],\r\n  dm.[LinkQuality],\r\n  dm.[Battery],\r\n  dm.[Data],\r\n  dm.[Voltage],\r\n  dm.[MeetsNotificationRequirement],\r\n  dm.[InsertDate],\r\n  dm.[GatewayID],\r\n  dm.[HasNote],\r\n  dm.[IsAuthenticated],\r\n  dm.[ApplicationID]\r\nFROM dbo.[DataMessage] dm WITH(NOLOCK)\r\nINNER JOIN dbo.[Sensor] s WITH (NOLOCK) on dm.SensorID = s.SensorID and dm.MessageDate >= IsNull(s.StartDate,s.CreateDate)\r\nWHERE dm.[SensorID] = ' + CONVERT(VARCHAR(20), @SensorID) + '\r\n  AND [MessageDate] BETWEEN ''' + CONVERT(VARCHAR(30), @FromDate,121) + ''' AND ''' + CONVERT(VARCHAR(30),@ToDate,121) + '''\r\nORDER BY MessageDate DESC';\r\n\r\nEXEC (@SQL);\r\n")]
  internal class LoadAllForChart : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public LoadAllForChart(long sensorID, DateTime fromDate, DateTime toDate)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable());
    }
  }

  [DBMethod("Message_CountAwareByDay_LoadForChart")]
  [DBMethodBody(DBMS.SqlServer, "\r\n--Declare Proc Variables\r\nDECLARE @_SensorID    BIGINT,\r\n        @ActualCount  INT,\r\n        @Modulus      INT;\r\n\r\nDECLARE @Counts TABLE(\r\n  [RowNumber] INT,\r\n  [Date]      DATETIME,\r\n  [Count]     INT\r\n);\r\n\r\n--To avoid recompiles\r\nSET @_SensorID = @SensorID;\r\n\r\nDECLARE @sql VARCHAR(MAX) = \r\n'SELECT\r\n  rownumber = ROW_NUMBER() OVER (ORDER BY temp2.[Date]),\r\n  temp2.[Date], \r\n  [Count]   = ISNULL(temp.[Count],0)\r\nFROM (SELECT\r\n        [Date]  = CONVERT(CHAR(10), [MessageDate], 120),\r\n        [Count] = COUNT([DataMessageGUID])\r\n      FROM dbo.[DataMessage] WITH(NOLOCK)\r\n      WHERE [SensorID]    = '+ CONVERT(VARCHAR(10), @_SensorID)+'\r\n        AND [MessageDate] BETWEEN '''+CONVERT(VARCHAR(20), @FromDate,120)+''' AND '''+CONVERT(VARCHAR(20),@ToDate,120)+'''\r\n        AND [State] & 2   = 2\r\n      GROUP BY CONVERT(CHAR(10), [MessageDate], 120)\r\n     ) AS temp\r\nRIGHT JOIN dbo.[GetDatesByRange]('''+CONVERT(VARCHAR(20), @FromDate,120)+''','''+CONVERT(VARCHAR(20),@ToDate,120)+''') temp2 on CONVERT(CHAR(10), temp2.[Date], 120) = temp.[Date]';\r\n\r\nINSERT INTO @Counts\r\nEXEC (@sql);\r\n\r\nSELECT @ActualCount = COUNT(*) from @Counts;\r\nSELECT @Modulus     = CEILING(CONVERT(FLOAT,@ActualCount)/CONVERT(FLOAT,@MaxCount));\r\n\r\nSELECT * FROM @Counts WHERE RowNumber % @Modulus = 0;\r\n")]
  internal class CountAwareByDay_LoadForChart : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("LocalOffset", typeof (int))]
    public int LocalOffset { get; private set; }

    [DBMethodParam("MaxCount", typeof (int))]
    public int MaxCount { get; private set; }

    public List<DataPoint> Result { get; private set; }

    public CountAwareByDay_LoadForChart(
      long sensorID,
      DateTime fromDate,
      DateTime toDate,
      int localOffset,
      int maxCount)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.LocalOffset = localOffset;
      this.MaxCount = maxCount;
      this.Result = new List<DataPoint>();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null || dataTable.Rows == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
        this.Result.Add(new DataPoint()
        {
          Date = Convert.ToDateTime(row["Date"]),
          Value = (object) Convert.ToInt32(row["Count"])
        });
    }
  }

  [DBMethod("Message_CountByDay_LoadForChart")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ActualCount  INT,\r\n        @Modulus      INT,\r\n        @_SensorID    BIGINT;\r\n\r\nSET @_SensorID = @SensorID\r\n\r\nDECLARE @Counts TABLE(\r\n  [RowNumber] INT,\r\n  [Date]      DATETIME,\r\n  [Count]     INT\r\n);\r\n\r\nINSERT INTO @Counts\r\nSELECT\r\n  [rownumber] = ROW_NUMBER() OVER (ORDER BY temp2.[Date]),\r\n  temp2.[Date],\r\n  [Count]     = ISNULL(temp.[Count],0)\r\nFROM (\r\n      SELECT\r\n        [Date]  = CONVERT(CHAR(10), MessageDate, 120),\r\n        [Count] = COUNT(DataMessageGUID)\r\n      FROM dbo.[DataMessage] WITH (NOLOCK)\r\n      WHERE [SensorID]    = @_SensorID\r\n        AND [MessageDate] BETWEEN @FromDate AND @ToDate\r\n      GROUP BY CONVERT(CHAR(10), [MessageDate], 120)\r\n     ) AS temp\r\nRIGHT JOIN dbo.[GetDatesByRange](@FromDate,@ToDate) temp2 ON CONVERT(CHAR(10), temp2.[Date], 120) = temp.[Date];\r\n\r\nSELECT @ActualCount = COUNT(*) FROM @Counts;\r\nSELECT @Modulus     = CEILING(CONVERT(FLOAT,@ActualCount)/CONVERT(FLOAT,@MaxCount));\r\n\r\nSELECT * FROM @Counts WHERE RowNumber %@Modulus = 0;\r\n")]
  internal class CountByDay_LoadForChart : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("MaxCount", typeof (int))]
    public int MaxCount { get; private set; }

    public List<DataPoint> Result { get; private set; }

    public CountByDay_LoadForChart(
      long sensorID,
      DateTime fromDate,
      DateTime toDate,
      int maxCount)
    {
      this.SensorID = sensorID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.MaxCount = maxCount;
      this.Result = new List<DataPoint>();
      foreach (DataRow row in (InternalDataCollectionBase) this.ToDataTable().Rows)
        this.Result.Add(new DataPoint()
        {
          Date = Convert.ToDateTime(row["Date"]),
          Value = (object) Convert.ToInt32(row["Count"])
        });
    }
  }

  [DBMethod("Message_LoadRangeByNetwork")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/*********************************************************\r\n                      CHANGE LOG\r\n**********************************************************\r\n  Date          ModifiedBy        Comments\r\n  ------------  ----------------  ------------------------\r\n  7/27/2016     Brandon Young     Written with dynamic sql generation\r\n                                  Instead of pulling from View\r\n  8/1/2016      Brandon Young     Updated @ToDate calculation from changing\r\n                                  by 1 month, to the last second of the previous month\r\n    \r\n**********************************************************\r\nDECLARE @NetworkID        bigint\r\nDECLARE @From             datetime\r\nDECLARE @To               datetime\r\nDECLARE @Limit            int\r\nSET @NetworkID        = 10052\r\nSET @From             = '2016-07-24 03:00:00'\r\nSET @To               = '2016-08-01 13:00:00'\r\nSET @Limit            = 500\r\nEXEC [dbo].[Message_LoadRangeByNetwork] @NetworkID, @From, @To, @Limit;\r\n*********************************************************/\r\nSET NOCOUNT ON;\r\n\r\nDECLARE @SQL              VARCHAR(MAX)\r\nDECLARE @Retreived        INT\r\n\r\nSET @SQL = ''\r\n\r\nSELECT SensorID INTO #SensorList FROM dbo.[Sensor] WITH (NOLOCK) where CSNetID = @NetworkID\r\n\t\r\nSET @SQL = @SQL +  \r\n'SELECT TOP (' + CONVERT(VARCHAR(6), @Limit)  + ')\r\n  dm.*\r\nFROM dbo.[DataMessage] dm WITH(NOLOCK)\r\nINNER JOIN #SensorList s ON dm.SensorID = s.SensorID\r\nWHERE MessageDate > ''' + CONVERT(VARCHAR(30), @FROM,120) + '''\r\n  AND MessageDate < ''' + CONVERT(VARCHAR(30), @To  ,120 ) + '''\r\nORDER BY MessageDate DESC;'\r\n\r\n--SELECT TOP (@Limit)\r\n--  dm.*\r\n--FROM dbo.[DataMessage] dm WITH (NOLOCK)\r\n--INNER JOIN #SensorList s on dm.SensorID = s.SensorID\r\n--WHERE MessageDate >= @From\r\n--and MessageDate <= @To\r\n--ORDER BY MessageDate DESC\r\n--OPTION (OPTIMIZE FOR UNKNOWN)\r\n\r\nPRINT @SQL\r\nEXEC (@SQL)\r\n\r\nSET NOCOUNT OFF;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM DataMessage WHERE  MessageDate >= '@From' AND MessageDate <= '@To' ORDER BY MessageDate, SensorID  Desc limit @Limit ;")]
  internal class LoadRangeByNetWork : BaseDBMethod
  {
    [DBMethodParam("NetworkID", typeof (long))]
    public long NetworkID { get; private set; }

    [DBMethodParam("From", typeof (DateTime))]
    public DateTime From { get; private set; }

    [DBMethodParam("To", typeof (DateTime))]
    public DateTime To { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public LoadRangeByNetWork(long networkID, DateTime from, DateTime to, int limit)
    {
      this.NetworkID = networkID;
      this.From = from;
      this.To = to;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable());
    }
  }

  [DBMethod("Message_LoadRangeByAccount")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/*********************************************************\r\n                      CHANGE LOG\r\n**********************************************************\r\n  Date          ModifiedBy        Comments\r\n  ------------  ----------------  ------------------------\r\n  7/27/2016     Brandon Young     Written with dynamic sql generation\r\n                                  Instead of pulling from View\r\n  8/1/2016      Brandon Young     Updated @ToDate calculation from changing\r\n                                  by 1 month, to the last second of the previous month\r\n    \r\n**********************************************************\r\nDECLARE @AccountID        bigint\r\nDECLARE @From             datetime\r\nDECLARE @To               datetime\r\nDECLARE @Limit            int\r\nSET @AccountID        = 1\r\nSET @From             = '2016-06-24 03:00:00'\r\nSET @To               = '2016-06-30 13:00:00'\r\nSET @Limit            = 500\r\nEXEC [dbo].[Message_LoadRangeByAccount] @AccountID, @From, @To, @Limit;\r\n*********************************************************/\r\n\r\n  DECLARE @TempSQL          VARCHAR(500);\r\n  DECLARE @PartSQL          VARCHAR(500);\r\n  DECLARE @Retreived        INT;\r\n\r\n  SET @TempSQL = \r\n   'SELECT TOP (' + CONVERT(VARCHAR(10), @Limit) + ')\r\n      d.*\r\n    FROM dbo.[DataMessage] d WITH (NOLOCK)\r\n    INNER JOIN dbo.[Sensor] s on d.SensorID = s.SensorID\r\n    WHERE s.AccountID = '+ CONVERT(VARCHAR(255), @AccountID) +'\r\n      AND d.MessageDate >= ''' + CONVERT(VARCHAR(20), @From, 120) + '''\r\n      AND d.MessageDate < ''' + CONVERT(VARCHAR(20), @To, 120) + '''\r\n      AND d.MessageDate >= ISNULL(s.StartDate, ''' + CONVERT(VARCHAR(255), @From) +  ''')\r\n    ORDER BY d.MessageDate DESC'\r\n\r\n    EXEC (@TempSQL);\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM DataMessage WHERE AccountID = @AccountID AND MessageDate >= '@From' AND MessageDate <= '@To' ORDER BY MessageDate Desc limit @Limit ;")]
  internal class LoadRangeByAccount : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("From", typeof (DateTime))]
    public DateTime From { get; private set; }

    [DBMethodParam("To", typeof (DateTime))]
    public DateTime To { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public LoadRangeByAccount(long accountID, DateTime from, DateTime to, int limit)
    {
      this.AccountID = accountID;
      this.From = from;
      this.To = to;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable());
    }
  }

  [DBMethod("Message_LoadRecentBySensor")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @CompareDate  DATETIME,\r\n        @SQL          NVARCHAR(MAX);\r\n\r\nSET @CompareDate = DATEADD(MINUTE,@Minutes * -1,GETUTCDATE());\r\n\t\r\nIF @LastDataMessageGUID IS NOT NULL\r\nBEGIN\r\n\r\n    SELECT @CompareDate = MessageDate FROM dbo.[DataMessage] WITH(NOLOCK) WHERE DataMessageGUID = @LastDataMessageGUID AND MessageDate > @CompareDate\r\n\t\r\nEND\r\n\r\nSET @SQL = N'SELECT *' + CHAR(13) + CHAR(10)\r\nSET @SQL = @SQL + N'FROM dbo.[DataMessage] WITH (NOLOCK)' + CHAR(13) + CHAR(10)\r\nSET @SQL = @SQL + N'WHERE SensorID = ' + CONVERT(NVARCHAR(20), @SensorID) + CHAR(13) + CHAR(10)\r\nSET @SQL = @SQL + N'AND MessageDate >= ''' + CONVERT(NVARCHAR(50), @CompareDate, 121) + '''' + CHAR(13) + CHAR(10)\r\nSET @SQL = @SQL + N'ORDER BY MessageDate DESC' + CHAR(13) + CHAR(10)\r\n--SET @SQL = @SQL + N'OPTION(OPTIMIZE FOR UNKNOWN);' + CHAR(13) + CHAR(10) + CHAR(13) + CHAR(10)\r\n\r\nEXEC (@SQL);\r\n")]
  internal class LoadRecentBySensor : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("Minutes", typeof (int))]
    public int Minutes { get; private set; }

    [DBMethodParam("LastDataMessageGUID", typeof (long))]
    public Guid LastDataMessageGUID { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public LoadRecentBySensor(long sensorID, int minutes, Guid lastDataMessageGUID)
    {
      this.SensorID = sensorID;
      this.Minutes = minutes;
      this.LastDataMessageGUID = lastDataMessageGUID;
      "".Encrypt();
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable());
    }
  }

  [DBMethod("Message_LoadLastByAccount")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  dm.*\r\nFROM dbo.[DataMessage_Last] dm WITH(NOLOCK)\r\nINNER JOIN dbo.[Sensor] s WITH(NOLOCK) ON dm.SensorID = s.SensorID\r\nWHERE AccountID = @AccountID;\r\n")]
  internal class LoadLastByAccount : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public LoadLastByAccount(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable());
    }
  }

  [DBMethod("Message_LoadLastByNetwork")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  dm.*\r\nFROM dbo.[DataMessage_Last] dm WITH(NOLOCK)\r\nINNER JOIN dbo.[Sensor] s WITH(NOLOCK) ON dm.SensorID = s.SensorID\r\nWHERE s.CSNetID = @CSNetID;\r\n")]
  internal class LoadLastByNetwork : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public LoadLastByNetwork(long csNetID)
    {
      this.CSNetID = csNetID;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable());
    }
  }

  [DBMethod("DataMessage_Insert")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSET NOCOUNT ON;\r\n\r\nIF NOT EXISTS (\r\n\t\t\t\tSELECT * FROM dbo.[DataMessage] (NOLOCK) --Allowing for dirty reads on purpose.  The odds of duplicates at this point is very low.\r\n\t\t\t\tWHERE [SensorID] = @SensorID\r\n\t\t\t\t\tAND [MessageDate] = @MessageDate\r\n\t\t\t\t\tAND [State] = @State\r\n\t\t\t\t)\r\nBEGIN\r\n\r\n\t  --Insert the new record\r\n\t  INSERT INTO dbo.[DataMessage]([MessageDate],[SensorID],[DataMessageGUID],[State],[SignalStrength],[LinkQuality],[Battery],[Data],[Voltage],[MeetsNotificationRequirement],[InsertDate],[GatewayID],[HasNote])\r\n\t  VALUES (@MessageDate, @SensorID, @DataMessageGUID, @State, @SignalStrength, @LinkQuality, @Battery, @Data, @Voltage, @MeetsNotificationRequirement, @InsertDate, @GatewayID, @HasNote);\r\n\r\n\t  UPDATE dbo.[Sensor]\r\n\t\t  SET LastDataMessageGUID   = @DataMessageGUID,\r\n\t\t\t    LastCommunicationDate = @MessageDate,\r\n\t\t\t    IsSleeping            = 0,\r\n\t\t\t    IsNewToNetwork        = 0\r\n\t  WHERE SensorID = @SensorID\r\n\t\t  AND (LastCommunicationDate < @MessageDate OR LastCommunicationDate = '2099-01-01 00:00:00.000')-- only if message is newer\r\n\t\t  AND DATEADD(month,-1,@MessageDate) < GETUTCDATE()\r\n\t\t  AND Sensor.IsActive = 1-- only if sensor is active\r\n\r\n\t  RETURN 1;\r\n\r\nEND ELSE\r\nBEGIN\r\n\t  RETURN 0;\r\nEND\r\n")]
  [DBMethodBody(DBMS.SQLite, "\r\n\t            INSERT INTO DataMessage ( DataMessageGUID ,SensorID,    MessageDate,  State,  SignalStrength,  LinkQuality,  Battery,    Data,  Voltage,  MeetsNotificationRequirement,    InsertDate,  GatewayID)\r\n\t                             VALUES ('@DataMessageGuid',@SensorID, '@MessageDate', @State, @SignalStrength, @LinkQuality, @Battery, '@Data', @Voltage, @MeetsNotificationRequirement, '@InsertDate', @GatewayID);\r\n                update Sensor\r\n                set LastCommunicationDate =  '@MessageDate', LastDataMessageGUID = '@DataMessageGuid', IsSleeping = 0\r\n                Where SensorID = @SensorID;\r\n\r\n                select 1;\r\n")]
  internal class Insert : BaseDBMethod
  {
    [DBMethodParam("MessageDate", typeof (DateTime))]
    public DateTime MessageDate { get; private set; }

    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("DataMessageGuid", typeof (Guid))]
    public Guid DataMessageGuid { get; private set; }

    [DBMethodParam("State", typeof (int))]
    public int State { get; private set; }

    [DBMethodParam("SignalStrength", typeof (int))]
    public int SignalStrength { get; private set; }

    [DBMethodParam("LinkQuality", typeof (int))]
    public int LinkQuality { get; private set; }

    [DBMethodParam("Battery", typeof (int))]
    public int Battery { get; private set; }

    [DBMethodParam("Data", typeof (string))]
    public string Data { get; private set; }

    [DBMethodParam("Voltage", typeof (double))]
    public double Voltage { get; private set; }

    [DBMethodParam("MeetsNotificationRequirement", typeof (bool))]
    public bool MeetsNotificationRequirement { get; private set; }

    [DBMethodParam("InsertDate", typeof (DateTime))]
    public DateTime InsertDate { get; private set; }

    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("HasNote", typeof (bool))]
    public bool HasNote { get; private set; }

    public bool Result { get; private set; }

    public Insert(Monnit.DataMessage message)
    {
      if (message.MessageDate < new DateTime(2010, 4, 1) || message.MessageDate > DateTime.UtcNow.AddDays(10.0))
      {
        this.Result = false;
      }
      else
      {
        this.MessageDate = message.MessageDate;
        this.SensorID = message.SensorID;
        this.DataMessageGuid = message.DataMessageGUID;
        this.State = message.State;
        this.SignalStrength = message.SignalStrength;
        this.LinkQuality = message.LinkQuality;
        this.Battery = message.Battery;
        this.Data = message.Data;
        this.Voltage = message.Voltage;
        this.MeetsNotificationRequirement = message.MeetsNotificationRequirement;
        this.InsertDate = message.InsertDate;
        this.GatewayID = message.GatewayID;
        this.HasNote = message.HasNote;
        if (this.ToScalarValue<int>() == 1)
          this.Result = true;
        else
          this.Result = false;
      }
    }
  }

  [DBMethod("DataMessage_BulkInsert")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSET NOCOUNT ON;\r\n\r\n--BEGIN TRAN\r\n\r\nDECLARE @DataMessageGUID UNIQUEIDENTIFIER;\r\n\r\nDECLARE @DataMessage TABLE(\r\n\t[MessageDate] [datetime] NULL,\r\n\t[SensorID] [bigint] NOT NULL,\r\n\t[DataMessageGUID] [uniqueidentifier] NOT NULL,\r\n\t[State] [int] NULL,\r\n\t[SignalStrength] [int] NULL,\r\n\t[LinkQuality] [int] NULL,\r\n\t[Battery] [int] NULL,\r\n\t[Data] [varchar](2000) NULL,\r\n\t[Voltage] [decimal](5, 4) NULL,\r\n\t[MeetsNotificationRequirement] [bit] NULL,\r\n\t[InsertDate] [datetime] NULL,\r\n\t[GatewayID] [bigint] NULL,\r\n\t[HasNote] [bit] NOT NULL,\r\n  [IsAuthenticated] [bit] NULL,\r\n  [ApplicationID] INT,\r\n\t[IsMaxDate] [bit] NOT NULL,\r\n  [LastNormalDate] [Datetime] NULL,\r\n  [FirstAwareDate] [Datetime] NULL,\r\n  [LastAwareDate] [Datetime] NULL\r\n\t);\r\n\r\nDECLARE @ValidGUIDs TABLE(\r\n\t[DataMessageGUID] [uniqueidentifier] NOT NULL,\r\n\t[SensorID] [bigint] NOT NULL,\r\n\t[MessageDate] [datetime] NULL,\r\n\t[State] [int] NULL,\r\n\t[IsMaxDate] [bit] NOT NULL,\r\n  [LastNormalDate] [DATETIME] NULL,\r\n  [FirstAwareDate] [Datetime] NULL,\r\n  [LastAwareDate] [Datetime] NULL\r\n\t);\r\n\r\n/*\r\n\tSome records coming in have contained duplicates.  This finds the unique\r\n\tresult set from all the records being passed in for filtering in the next step.\r\n\tGUID's need to be converted to a numeric value before using an Aggregate function,\r\n\tso I convert it first, then convert it back.\r\n*/\r\nINSERT INTO @ValidGUIDs(\r\n\t[DataMessageGUID],\r\n\t[SensorID],\r\n\t[MessageDate],\r\n\t[State],\r\n\t[IsMaxDate]\r\n  )\r\nSELECT\r\n\t [DataMessageGUID] = CONVERT(UNIQUEIDENTIFIER, MAX(CONVERT(BINARY, [DataMessageGUID])))\r\n\t,[SensorID]\r\n\t,[MessageDate]\r\n\t,[State]\r\n\t,[IsMaxDate]       = CONVERT(BIT, 0)\r\nFROM @MessageTable\r\nWHERE DATEADD(day,-70,[MessageDate]) < GETUTCDATE() --Only keep valid Messages (ignore future garbage messages\r\n  AND MessageDate >= DATEADD(month,-12,GETUTCDATE())\r\nGROUP BY\r\n\t [SensorID]\r\n\t,[MessageDate]\r\n\t,[State];\r\n\r\n/*\r\n  If one sensor came in with different dates in the same batch,\r\n  flag which one has the most current date (used to update\r\n  dbo.Sensor later).\r\n*/\r\nUPDATE v\r\n  SET [IsMaxDate]   = 1\r\nFROM @ValidGUIDs v\r\nINNER JOIN ( \r\n            SELECT\r\n\t             [SensorID]\r\n\t            ,[MessageDate]    = MAX([MessageDate])\r\n            FROM @ValidGUIDs\r\n            GROUP BY\r\n\t             [SensorID]\r\n\t          ) t   ON v.[SensorID] = t.[SensorID] AND v.[MessageDate] = t.[MessageDate];\r\n\r\nUPDATE v\r\n  SET v.[LastNormalDate]   = t.LastNormalDate\r\nFROM @ValidGUIDs v\r\nINNER JOIN ( \r\n            SELECT\r\n\t             [SensorID]\r\n\t            ,[LastNormalDate]    = MAX([MessageDate])\r\n            FROM @ValidGUIDs\r\n              WHERE State & 02 = 0\r\n            GROUP BY\r\n\t             [SensorID]\r\n\t          ) t   ON v.[SensorID] = t.[SensorID]\r\nWHERE v.IsMaxDate = 1;\r\n\r\n\r\nUPDATE v\r\n  SET v.[FirstAwareDate]   = ISNULL(t.[FirstAwareDate], '2099-01-01 00:00:00.000'),\r\n      v.[LastAwareDate]     = ISNULL(t2.[LastAwareDate], '2099-01-01 00:00:00.000')\r\nFROM @ValidGUIDs v\r\nLEFT JOIN ( \r\n            SELECT\r\n\t             [SensorID]\r\n\t            ,[FirstAwareDate]    = MIN([MessageDate])\r\n            FROM @ValidGUIDs v\r\n              WHERE State & 02 = 2\r\n                AND MessageDate > (SELECT TOP 1 ISNULL(LastNormalDate, '1900-01-01 00:00:00') FROM @ValidGuids v2 WHERE v2.SensorID = v.SensorID and v2.IsMaxDate = 1)\r\n            GROUP BY\r\n\t             [SensorID]\r\n            ) t   ON v.[SensorID] = t.[SensorID]\r\nLEFT JOIN ( \r\n            SELECT\r\n\t             [SensorID]\r\n\t            ,[LastAwareDate]    = max([MessageDate])\r\n            FROM @ValidGUIDs v\r\n              WHERE State & 02 = 2\r\n            GROUP BY\r\n\t             [SensorID]\r\n            ) t2   ON v.[SensorID] = t2.[SensorID]\r\nWHERE v.IsMaxDate = 1;\r\n\r\n/*\r\n\tOnly keep new records for insertion.  I'm going to use a Temp table for processing rather\r\n\tthan use this LEFT JOIN against the raw table for the INSERT to reduce the opportunity for\r\n\tlocking/blocking of other inserts/selects.  This needs to be as non-intrusive as possible.\r\n\tI'm also using the bove temp table as a filter to only grab unique rows from the passed in\r\n\tdata set.\r\n\t\r\n\tThe INNER JOIN to sensor filters any valid messages from sensors that are not included in the database\r\n\tThe LEFT JOIN and filtering on IS NULL will keep only the records which are unique/new.\r\n*/\r\nINSERT INTO @DataMessage([DataMessageGUID],[SensorID],[MessageDate],[State],[SignalStrength],[LinkQuality],[Battery],[Data],[Voltage],[MeetsNotificationRequirement],[InsertDate],[GatewayID],[HasNote], [IsAuthenticated], [ApplicationID], [IsMaxDate], [LastNormalDate], [FirstAwareDate], [LastAwareDate])\r\nSELECT t.[DataMessageGUID],t.[SensorID],t.[MessageDate],t.[State],t.[SignalStrength],t.[LinkQuality],t.[Battery],t.[Data],t.[Voltage],t.[MeetsNotificationRequirement],t.[InsertDate],t.[GatewayID],t.[HasNote], t.[IsAuthenticated], ApplicationID = ISNULL(t.[ApplicationID], s.[ApplicationID]), v.[IsMaxDate], v.LastNormalDate, v.[FirstAwareDate], v.[LastAwareDate]\r\nFROM @MessageTable t\r\nINNER JOIN Sensor s \r\n\tON t.SensorID = s.SensorID\r\nINNER JOIN @ValidGUIDs v\r\n\tON t.[DataMessageGUID] = v.[DataMessageGUID]\r\nLEFT JOIN [dbo].[DataMessage] d (NOLOCK) --Allowing for dirty reads on purpose.  The odds of duplicates at this point is very low.\r\n\tON d.[SensorID] = t.[SensorID]\r\n AND d.[MessageDate] = t.[MessageDate]\r\n AND d.[State] = t.[State]\r\nWHERE d.[SensorID] IS NULL;\r\n\r\nINSERT INTO [dbo].[DataMessage]([MessageDate],[SensorID],[DataMessageGUID],[State],[SignalStrength],[LinkQuality],[Battery],[Data],[Voltage],[MeetsNotificationRequirement],[InsertDate],[GatewayID],[HasNote], [IsAuthenticated], [ApplicationID])\r\nSELECT [MessageDate],[SensorID],[DataMessageGUID],[State],[SignalStrength],[LinkQuality],[Battery],[Data],[Voltage],[MeetsNotificationRequirement],[InsertDate],[GatewayID],[HasNote], [IsAuthenticated], [ApplicationID]\r\nFROM @DataMessage;\r\n\r\nUPDATE s\r\n  SET LastDataMessageGUID   = CASE WHEN s.LastCommunicationDate <= d.MessageDate OR s.LastCommunicationDate = '2099-01-01 00:00:00.000' THEN d.[DataMessageGUID] else s.LastDataMessageGUID END, \r\n      LastCommunicationDate = CASE WHEN s.LastCommunicationDate <= d.MessageDate OR s.LastCommunicationDate = '2099-01-01 00:00:00.000' THEN d.MessageDate else s.LastCommunicationDate END, \r\n      IsSleeping            = 0,\r\n      IsNewToNetwork        = 0,\r\n      LastNormalDate        = CASE WHEN d.LastNormalDate IS NOT NULL AND (s.LastNormalDate <= d.MessageDate OR s.LastNormalDate = '2099-01-01 00:00:00.000') THEN d.LastNormalDate \r\n                                    ELSE s.LastNormalDate END,\r\n      FirstAwareDate        = CASE WHEN (d.LastNormalDate IS NOT NULL and d.LastNormalDate > s.FirstAwareDate) \r\n                                     OR (d.FirstAwareDate < s.FirstAwareDate AND (d.FirstAwareDate > s.LastNormalDate OR s.LastNormalDate = '2099-01-01 00:00:00.000'))\r\n                                    -- OR (s.FirstAwareDate = '2099-01-01 00:00:00.000' and d.FirstAwareDate < s.FirstAwareDate and d.FirstAwareDate > s.LastNormalDate)\r\n                                     --OR (d.FirstAwareDate > s.LastNormalDate and d.FirstAwareDate < s.FirstAwareDate) \r\n                                   THEN d.FirstAwareDate\r\n                                   ELSE s.FirstAwareDate END,\r\n      LastAwareDate          = CASE WHEN d.LastAwareDate > s.LastAwareDate OR s.LastAwareDate = '2099-01-01 00:00:00.000' THEN d.LastAwareDate else s.LastAwareDate END\r\nFROM dbo.Sensor s\r\nINNER JOIN @DataMessage d   ON s.SensorID = d.SensorID\r\nWHERE s.IsActive  = 1\t\t-- only if sensor is active\r\n\tAND d.IsMaxDate = 1;\t-- See notes from above.\r\n\r\n  \r\nINSERT INTO [dbo].[DataMessage_Last] ([MessageDate],[SensorID],[DataMessageGUID],[State],[SignalStrength],[LinkQuality],[Battery],[Data],[Voltage],[MeetsNotificationRequirement],[InsertDate],[GatewayID],[HasNote], [IsAuthenticated], [ApplicationID])\r\nSELECT\r\nd.[MessageDate],\r\nd.[SensorID],\r\nd.[DataMessageGUID],\r\nd.[State],\r\nd.[SignalStrength],\r\nd.[LinkQuality],\r\nd.[Battery],\r\nd.[Data],\r\nd.[Voltage],\r\nd.[MeetsNotificationRequirement],\r\nd.[InsertDate],\r\nd.[GatewayID],\r\nd.[HasNote], \r\nd.[IsAuthenticated],\r\nApplicationID = ISNULL(d.[ApplicationID], s.[ApplicationID])\r\nFROM @DataMessage d\r\nINNER JOIN dbo.Sensor s on d.SensorID = s.SensorID and d.DataMessageGUID = s.LastDataMessageGUID\r\nLEFT JOIN DataMessage_Last dl on d.SensorID = dl.SensorID\r\nWHERE d.IsMaxDate = 1\r\n  AND dl.SensorID IS NULL\r\n\r\n\r\nUPDATE dl\r\nSET\r\n  dl.[MessageDate]                  = d.[MessageDate],\r\n  dl.[SensorID]                     = d.[SensorID],\r\n  dl.[DataMessageGUID]              = d.[DataMessageGUID],\r\n  dl.[State]                        = d.[State],\r\n  dl.[SignalStrength]               = d.[SignalStrength],\r\n  dl.[LinkQuality]                  = d.[LinkQuality],\r\n  dl.[Battery]                      = d.[Battery],\r\n  dl.[Data]                         = d.[Data],\r\n  dl.[Voltage]                      = d.[Voltage],\r\n  dl.[MeetsNotificationRequirement] = d.[MeetsNotificationRequirement],\r\n  dl.[InsertDate]                   = d.[InsertDate],\r\n  dl.[GatewayID]                    = d.[GatewayID],\r\n  dl.[HasNote]                      = d.[HasNote], \r\n  dl.[IsAuthenticated]\t\t\t\t      = d.[IsAuthenticated],\r\n  ApplicationID                     = ISNULL(d.[ApplicationID], s.[ApplicationID])\r\nFROM @DataMessage d\r\nINNER JOIN dbo.Sensor s on d.SensorID = s.SensorID and d.DataMessageGUID = s.LastDataMessageGUID\r\nINNER JOIN DataMessage_Last dl on d.SensorID = dl.SensorID\r\nWHERE d.IsMaxDate = 1\r\n  and dl.MessageDate < d.MessageDate\r\n\r\n--COMMIT TRAN\r\n\r\n--Pass back the recordset for only the data that was actually inserted\r\nSELECT\r\n\t[DataMessageGUID]\r\nFROM @DataMessage;\r\n")]
  internal class BulkInsert : BaseDBMethod
  {
    [DBMethodParam("MessageTable", typeof (DataTable), ParameterTableType = "DataMessageInsert_Type")]
    public DataTable MessageTable { get; private set; }

    public List<Monnit.DataMessage> Result { get; private set; }

    public BulkInsert(List<Monnit.DataMessage> messageList)
    {
      Dictionary<Guid, Monnit.DataMessage> dictionary = new Dictionary<Guid, Monnit.DataMessage>();
      this.MessageTable = new DataTable(nameof (DataMessage));
      this.MessageTable.Columns.Add("MessageDate", typeof (DateTime));
      this.MessageTable.Columns.Add("SensorID", typeof (long));
      this.MessageTable.Columns.Add("DataMessageGUID", typeof (Guid));
      this.MessageTable.Columns.Add("State", typeof (int));
      this.MessageTable.Columns.Add("SignalStrength", typeof (int));
      this.MessageTable.Columns.Add("LinkQuality", typeof (int));
      this.MessageTable.Columns.Add("Battery", typeof (int));
      this.MessageTable.Columns.Add("Data", typeof (string));
      this.MessageTable.Columns.Add("Voltage", typeof (Decimal));
      this.MessageTable.Columns.Add("MeetsNotificationRequirement", typeof (bool));
      this.MessageTable.Columns.Add("InsertDate", typeof (DateTime));
      this.MessageTable.Columns.Add("GatewayID", typeof (long));
      this.MessageTable.Columns.Add("HasNote", typeof (bool));
      this.MessageTable.Columns.Add("IsAuthenticated", typeof (bool));
      this.MessageTable.Columns.Add("ApplicationID", typeof (long));
      foreach (Monnit.DataMessage message in messageList)
      {
        if (!(message.MessageDate < new DateTime(2010, 4, 1)) && !(message.MessageDate > DateTime.UtcNow.AddDays(10.0)))
        {
          message.DataMessageGUID = Guid.NewGuid();
          dictionary.Add(message.DataMessageGUID, message);
          this.MessageTable.Rows.Add((object) message.MessageDate, (object) message.SensorID, (object) message.DataMessageGUID, (object) message.State, (object) message.SignalStrength, (object) message.LinkQuality, (object) message.Battery, (object) message.Data, (object) message.Voltage, (object) message.MeetsNotificationRequirement, (object) message.InsertDate, (object) message.GatewayID, (object) message.HasNote, (object) message.IsAuthenticated, (object) message.ApplicationID);
        }
      }
      this.Result = new List<Monnit.DataMessage>();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Monnit.DataMessage dataMessage = dictionary[(Guid) row["DataMessageGUID"]];
        if (dataMessage != null)
          this.Result.Add(dataMessage);
      }
      Monnit.Cassandra.DataMessage.BulkInsert(this.Result);
    }
  }

  [DBMethod("DataMessage_Update")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE dbo.[DataMessage] SET MeetsNotificationRequirement = @MeetsNotificationRequirement, HasNote = @HasNote WHERE DataMessageGUID = @DataMessageGUID AND SensorID = @SensorID AND MessageDate = @MessageDate;\r\n\r\nUPDATE dbo.[DataMessage_Last] SET MeetsNotificationRequirement = @MeetsNotificationRequirement, HasNote = @HasNote WHERE DataMessageGUID = @DataMessageGUID AND SensorID = @SensorID;\r\n")]
  [DBMethodBody(DBMS.SQLite, "\r\nUPDATE DataMessage SET MeetsNotificationRequirement = @MeetsNotificationRequirement, HasNote = @HasNote WHERE DataMessageGUID = @DataMessageGUID AND SensorID = @SensorID AND MessageDate = @MessageDate \r\n")]
  internal class Update : BaseDBMethod
  {
    [DBMethodParam("MessageDate", typeof (DateTime))]
    public DateTime MessageDate { get; private set; }

    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("DataMessageGuid", typeof (Guid))]
    public Guid DataMessageGuid { get; private set; }

    [DBMethodParam("MeetsNotificationRequirement", typeof (bool))]
    public bool MeetsNotificationRequirement { get; private set; }

    [DBMethodParam("HasNote", typeof (bool))]
    public bool HasNote { get; private set; }

    public bool Result { get; private set; }

    public Update(Monnit.DataMessage message)
    {
      this.MessageDate = message.MessageDate;
      this.SensorID = message.SensorID;
      this.DataMessageGuid = message.DataMessageGUID;
      this.MeetsNotificationRequirement = message.MeetsNotificationRequirement;
      this.HasNote = message.HasNote;
      try
      {
        this.Execute();
        this.Result = true;
      }
      catch
      {
        this.Result = false;
      }
    }
  }

  [DBMethod("Message_LoadLastBefore")]
  [DBMethodBody(DBMS.SqlServer, "\r\n    DECLARE @SQL      VARCHAR(MAX),\r\n        @FromDate DATETIME;\r\n\r\n/*\r\n  Temp Table used as a means of returning NULL, NULL if no result within 24 hours prior to @StartDate\r\n*/\r\nCREATE TABLE #Results\r\n(\r\n  [MessageDate]                   DATETIME,\r\n  [SensorID]                      BIGINT,\r\n  [DataMessageGUID]               UNIQUEIDENTIFIER,\r\n  [State]                         INT,\r\n  [SignalStrength]                INT,\r\n  [LinkQuality]                   INT,\r\n  [Battery]                       INT,\r\n  [Data]                          VARCHAR(500),\r\n  [Voltage]                       DECIMAL(5,4),\r\n  [MeetsNotificationRequirement]  BIT,\r\n  [InsertDate]                    DATETIME,\r\n  [GatewayID]                     BIGINT,\r\n  [HasNote]                       BIT,\r\n  [IsAuthenticated]               BIT,\r\n  [ApplicationID]\t\t\t\t  INT NULL\r\n);\r\n\r\nSET @FromDate  = DATEADD(DAY, -1, @StartDate);\r\n\r\nSET @SQL =\r\n'SELECT TOP 1\r\n  [MessageDate],\r\n  [SensorID],\r\n  [DataMessageGUID],\r\n  [State],\r\n  [SignalStrength],\r\n  [LinkQuality],\r\n  [Battery],\r\n  [Data],\r\n  [Voltage],\r\n  [MeetsNotificationRequirement],\r\n  [InsertDate],\r\n  [GatewayID],\r\n  [HasNote],\r\n  [IsAuthenticated],\r\n  [ApplicationID]\r\nFROM dbo.[DataMessage] WITH (NOLOCK)\r\nWHERE MessageDate >= ''' + CONVERT(VARCHAR(20), @FromDate) + '''\r\n  AND MessageDate < ''' + CONVERT(VARCHAR(20), @StartDate) + '''\r\n  AND SensorID = ' + CONVERT(VARCHAR(14), @SensorID) + '\r\nORDER BY MessageDate DESC';\r\n\r\nINSERT INTO #Results\r\n(\r\n  [MessageDate],\r\n  [SensorID],\r\n  [DataMessageGUID],\r\n  [State],\r\n  [SignalStrength],\r\n  [LinkQuality],\r\n  [Battery],\r\n  [Data],\r\n  [Voltage],\r\n  [MeetsNotificationRequirement],\r\n  [InsertDate],\r\n  [GatewayID],\r\n  [HasNote],\r\n  [IsAuthenticated],\r\n  [ApplicationID]\r\n)\r\nEXEC (@SQL);\r\n\r\nIF @@ROWCOUNT = 0\r\nBEGIN  \r\n  \r\n  SELECT\r\n    [MessageDate] = NULL,\r\n    [SensorID] = NULL,\r\n    [DataMessageGUID] = NULL,\r\n    [State] = NULL,\r\n    [SignalStrength] = NULL,\r\n    [LinkQuality] = NULL,\r\n    [Battery] = NULL,\r\n    [Data] = NULL,\r\n    [Voltage] = NULL,\r\n    [MeetsNotificationRequirement] = NULL,\r\n    [InsertDate] = NULL,\r\n    [GatewayID] = NULL,\r\n    [HasNote] = NULL,\r\n    [IsAuthenticated] = NULL,\r\n\t[ApplicationID] = NULL;\r\n    \r\nEND ELSE\r\nBEGIN\r\n\r\n  SELECT\r\n    [MessageDate],\r\n    [SensorID],\r\n    [DataMessageGUID],\r\n    [State],\r\n    [SignalStrength],\r\n    [LinkQuality],\r\n    [Battery],\r\n    [Data],\r\n    [Voltage],\r\n    [MeetsNotificationRequirement],\r\n    [InsertDate],\r\n    [GatewayID],\r\n    [HasNote],\r\n    [IsAuthenticated],\r\n\t[ApplicationID]\r\n  FROM #Results;\r\n\r\nEND\r\n")]
  internal class LoadLastBefore : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("StartDate", typeof (DateTime))]
    public DateTime StartDate { get; private set; }

    public Monnit.DataMessage Result { get; private set; }

    public LoadLastBefore(long sensorID, DateTime startDate)
    {
      this.SensorID = sensorID;
      this.StartDate = startDate;
      this.Result = BaseDBObject.Load<Monnit.DataMessage>(this.ToDataTable()).FirstOrDefault<Monnit.DataMessage>();
    }
  }

  [DBMethod("DataMessage_UpdateLast")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSET NOCOUNT ON;\r\nIF NOT EXISTS (\r\n\tSELECT\r\n\t\t*\r\n\tFROM\r\n\t\t[dbo].[DataMessage_Last] (NOLOCK) --Allowing for dirty reads on purpose.  The odds of duplicates at this point is very low.\r\n\tWHERE\r\n\t\t[SensorID]        = @SensorID\r\n\tAND [DataMessageGuid] = @DataMessageGuid\r\n\tAND [MessageDate]     = @MessageDate\r\n\t)\r\n\tBEGIN\r\n\t\tDECLARE @lastmsgdt DATETIME = (\r\n\t\t\tSELECT\r\n\t\t\t\tMAX(MessageDate)\r\n\t\t\tFROM\r\n\t\t\t\t[dbo].[DataMessage_Last] (NOLOCK) \r\n\t\t\tWHERE\r\n\t\t\t\t[SensorID] = @SensorID\r\n\t\t)\r\n\t\tIF\r\n\t\t\t@lastmsgdt IS NULL\r\n\t\t\tOR \r\n\t\t\t@lastmsgdt < @MessageDate\r\n\t\t\tBEGIN\r\n\t\t\t\tDELETE FROM\r\n\t\t\t\t\tDataMessage_Last\r\n\t\t\t\tWHERE\r\n\t\t\t\t\tSensorID = @SensorID;\r\n\t\t\t\t\r\n\t\t\t\tINSERT INTO [dbo].[DataMessage_Last]\r\n\t\t\t\t(\r\n\t\t\t\t\t[MessageDate]                 ,\r\n\t\t\t\t\t[SensorID]                    ,\r\n\t\t\t\t\t[DataMessageGUID]             ,\r\n\t\t\t\t\t[State]                       ,\r\n\t\t\t\t\t[SignalStrength]              ,\r\n\t\t\t\t\t[LinkQuality]                 ,\r\n\t\t\t\t\t[Battery]                     ,\r\n\t\t\t\t\t[Data]                        ,\r\n\t\t\t\t\t[Voltage]                     ,\r\n\t\t\t\t\t[MeetsNotificationRequirement],\r\n\t\t\t\t\t[InsertDate]                  ,\r\n\t\t\t\t\t[GatewayID]                   ,\r\n\t\t\t\t\t[HasNote]                     ,\r\n\t\t\t\t\t[IsAuthenticated]             ,\r\n\t\t\t\t\t[ApplicationID]\r\n\t\t\t\t)\r\n\t\t\t\tVALUES\r\n\t\t\t\t(\r\n\t\t\t\t\t@MessageDate                 ,\r\n\t\t\t\t\t@SensorID                    ,\r\n\t\t\t\t\t@DataMessageGUID             ,\r\n\t\t\t\t\t@State                       ,\r\n\t\t\t\t\t@SignalStrength              ,\r\n\t\t\t\t\t@LinkQuality                 ,\r\n\t\t\t\t\t@Battery                     ,\r\n\t\t\t\t\t@Data                        ,\r\n\t\t\t\t\t@Voltage                     ,\r\n\t\t\t\t\t@MeetsNotificationRequirement,\r\n\t\t\t\t\t@InsertDate                  ,\r\n\t\t\t\t\t@GatewayID                   ,\r\n\t\t\t\t\t@HasNote                     ,\r\n\t\t\t\t\t@IsAuthenticated             ,\r\n\t\t\t\t\t@ApplicationID\r\n\t\t\t\t);\r\n\t\t\t\t--SET @Result = 1;\r\n\t\t\t\tSELECT 1;\r\n\t\t\t\tRETURN (1);\r\n\t\t\tEND\r\n\t\tELSE\r\n\t\t\tBEGIN\r\n\t\t\t\t--SET @Result = 0;\r\n\t\t\t\tSELECT 0;\r\n\t\t\t\tRETURN (0);\r\n\t\t\tEND \r\n\tEND ELSE\r\n\t\tBEGIN\r\n\t\t\t--SET @Result = 0;\r\n\t\t\tSELECT 0;\r\n\t\t\tRETURN (0);\r\n\t\tEND\r\n--SELECT @Result;\r\n--RETURN (@Result);\r\n")]
  internal class UpdateLast : BaseDBMethod
  {
    [DBMethodParam("MessageDate", typeof (DateTime))]
    public DateTime MessageDate { get; private set; }

    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("DataMessageGuid", typeof (Guid))]
    public Guid DataMessageGuid { get; private set; }

    [DBMethodParam("State", typeof (int))]
    public int State { get; private set; }

    [DBMethodParam("SignalStrength", typeof (int))]
    public int SignalStrength { get; private set; }

    [DBMethodParam("LinkQuality", typeof (int))]
    public int LinkQuality { get; private set; }

    [DBMethodParam("Battery", typeof (int))]
    public int Battery { get; private set; }

    [DBMethodParam("Data", typeof (string))]
    public string Data { get; private set; }

    [DBMethodParam("Voltage", typeof (double))]
    public double Voltage { get; private set; }

    [DBMethodParam("MeetsNotificationRequirement", typeof (bool))]
    public bool MeetsNotificationRequirement { get; private set; }

    [DBMethodParam("InsertDate", typeof (DateTime))]
    public DateTime InsertDate { get; private set; }

    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("HasNote", typeof (bool))]
    public bool HasNote { get; private set; }

    [DBMethodParam("IsAuthenticated", typeof (bool))]
    public bool IsAuthenticated { get; private set; }

    [DBMethodParam("ApplicationID", typeof (long))]
    public long ApplicationID { get; private set; }

    public bool Result { get; private set; }

    public UpdateLast(Monnit.DataMessage dm)
    {
      try
      {
        if (dm == null || dm.DataMessageGUID == Guid.Empty)
        {
          this.Result = false;
        }
        else
        {
          this.MessageDate = dm.MessageDate;
          this.SensorID = dm.SensorID;
          this.DataMessageGuid = dm.DataMessageGUID;
          this.State = dm.State;
          this.SignalStrength = dm.SignalStrength;
          this.LinkQuality = dm.LinkQuality;
          this.Battery = dm.Battery;
          this.Data = dm.Data;
          this.Voltage = dm.Voltage;
          this.MeetsNotificationRequirement = dm.MeetsNotificationRequirement;
          this.InsertDate = dm.InsertDate;
          this.GatewayID = dm.GatewayID;
          this.HasNote = dm.HasNote;
          this.IsAuthenticated = dm.IsAuthenticated;
          this.ApplicationID = dm.ApplicationID;
          if (this.ToScalarValue<int>() == 1)
            this.Result = true;
          else
            this.Result = false;
        }
      }
      catch (Exception ex)
      {
        ex.Log("Data.DataMessage.UpdateLast");
        this.Result = false;
      }
    }
  }
}
