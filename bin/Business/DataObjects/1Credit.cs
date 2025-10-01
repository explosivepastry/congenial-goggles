// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Credit
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class Credit
{
  [DBMethod("Credit_LoadAvailable")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  nc.* \r\nFROM dbo.[Credit] nc\r\nINNER JOIN dbo.[NotificationCreditType] nct ON nct.NotificationCreditTypeID = nc.CreditTypeID\r\nWHERE nc.AccountID      = @AccountID \r\n  AND nc.ActivationDate < DATEADD(DAY,1,GETUTCDATE())\r\n  AND ISNULL(nc.ExpirationDate, '2099-01-01') > GETUTCDATE()\r\n  AND ExhaustedDate                            IS NULL\r\n  AND (nc.UsedCredits    < nc.ActivatedCredits or nc.CreditTypeID = 7)\r\n  AND (nc.IsDeleted IS NULL OR nc.IsDeleted = 0)\r\n  AND nct.CreditClassification = @CreditClassification\r\nORDER BY\r\n  nct.Rank,\r\n  ExpirationDate,\r\n  ActivationDate;\r\n")]
  internal class LoadAvailable : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("CreditClassification", typeof (int))]
    public int CreditClassification { get; private set; }

    public List<Monnit.Credit> Result { get; private set; }

    public LoadAvailable(long accountID, int creditClassification)
    {
      this.AccountID = accountID;
      this.CreditClassification = creditClassification;
      this.Result = BaseDBObject.Load<Monnit.Credit>(this.ToDataTable());
    }
  }

  [DBMethod("Credit_LoadExhaustedByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  *\r\nFROM dbo.[Credit] c WITH (NOLOCK)\r\nINNER JOIN [NotificationCreditType] n WITH (NOLOCK) on c.CreditTypeID = n.NotificationCreditTypeID\r\nWHERE AccountID = @AccountID\r\n  AND ExhaustedDate IS NOT NULL\r\n  AND n.CreditClassification = '2'\r\n ORDER BY ExhaustedDate DESC;\r\n")]
  internal class LoadExhaustedCreditsByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.Credit> Result { get; private set; }

    public LoadExhaustedCreditsByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.Credit>(this.ToDataTable());
    }
  }

  [DBMethod("Credit_LoadOverdraft")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  nc.* \r\nFROM dbo.[Credit] nc\r\nINNER JOIN dbo.[NotificationCreditType] nct ON nct.NotificationCreditTypeID = nc.CreditTypeID\r\nWHERE nc.AccountID      = @AccountID\r\nAND nc.ActivationDate < DATEADD(DAY,1,GETUTCDATE())\r\n  AND nc.ExhaustedDate IS NULL\r\n  AND nc.ActivatedCredits - nc.UsedCredits <= 0\r\n  AND (nc.IsDeleted IS NULL OR nc.IsDeleted = 0)\r\n  AND nct.CreditClassification = @CreditClassification\r\nORDER BY\r\n  nct.Rank,\r\n  ExpirationDate,\r\n  ActivationDate;")]
  internal class LoadOverdraft : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("CreditClassification", typeof (int))]
    public int CreditClassification { get; private set; }

    public List<Monnit.Credit> Result { get; private set; }

    public LoadOverdraft(long accountID, int creditClassification)
    {
      this.AccountID = accountID;
      this.CreditClassification = creditClassification;
      this.Result = BaseDBObject.Load<Monnit.Credit>(this.ToDataTable());
    }
  }

  [DBMethod("Credit_LogHXUsage")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @ServerName  NVARCHAR(100),\r\n        @IsPrimary   BIT = 0,\r\n\t\t    @primaryname varchar(300);\r\n\r\n--SET @ServerName = @@SERVERNAME;\r\n\r\n\r\n--SELECT\r\n--@primaryname = ISNULL(replica_server_name, @ServerName)\r\n--FROM master.sys.availability_groups Groups\r\n--INNER JOIN master.sys.availability_replicas Replicas ON Groups.group_id = Replicas.group_id\r\n--INNER JOIN master.sys.dm_hadr_availability_group_states States ON Groups.group_id = States.group_id\r\n--where primary_replica  = replica_server_name;\r\n\r\n--IF @ServerName =  ISNULL(@primaryname, @ServerName)\r\n--BEGIN\r\n\r\n--\tSET @PrimaryName = ''\r\n\r\n--END ELSE\r\n--BEGIN\r\n\r\n--\tSET @PrimaryName = '['+@primaryname+'].'\r\n\r\n--END\r\n\r\nSET @primaryname = '';\r\n\r\nCREATE TABLE #DMCounts \r\n(\r\n  AccountID   BIGINT, \r\n  SensorID    BIGINT, \r\n  MessageDate DATETIME,\r\n  Counts      BIGINT,\r\n  Total       BIGINT\r\n);\r\n\r\nCREATE TABLE #ConsumedRecords\r\n(\r\n  AccountID BIGINT,\r\n  Consumed  INT\r\n);\r\n\r\nDECLARE @FromDate DATETIME = CONVERT(DATETIME, CONVERT(VARCHAR(13), DATEADD(HOUR, -1, GETUTCDATE()),120)+':00');\r\nDECLARE @SQL VARCHAR(8000);\r\n\r\n\r\nSET @SQL =\r\n'SELECT\r\n  s.AccountID, \r\n  s.SensorID,\r\n  MessageDate = CONVERT(DATE, d.MessageDate),\r\n  Counts      = COUNT(*),\r\n  Total       = NULL\r\nFROM dbo.[Sensor] s WITH (NOLOCK)\r\nINNER JOIN iMonnitMessages' +CONVERT(VARCHAR(4), DATEPART(YEAR, DATEADD(MONTH, -2, @FromDate)))+ '.dbo.[Datamessage_'+convert(varchar(6), DATEADD(MONTH, -2, @FromDate), 112)+'] d ON s.SensorID = d.SensorID\r\nINNER JOIN dbo.[Account] a on s.AccountID = a.AccountID\r\nWHERE d.InsertDate >= ''' + CONVERT(VARCHAR(30), @FromDate,120) +''' \r\n  AND d.InsertDate < DATEADD(HOUR, 1, ''' + CONVERT(VARCHAR(30), @FromDate,120) +''')\r\n  AND a.IsHXEnabled = 1\r\nGROUP BY s.AccountID, CONVERT(DATE, d.MessageDate),s.SensorID;'\r\nINSERT INTO #DMCounts (AccountID, SensorID, MessageDate, Counts, Total)\r\nEXEC (@SQL)\r\n--PRINT @SQL\r\n\r\n--1X calendar months\r\nSET @SQL =\r\n'SELECT\r\n  s.AccountID, \r\n  s.SensorID,\r\n  MessageDate = CONVERT(DATE, d.MessageDate),\r\n  Counts      = COUNT(*),\r\n  Total       = NULL\r\nFROM dbo.[Sensor] s WITH (NOLOCK)\r\nINNER JOIN iMonnitMessages' +CONVERT(VARCHAR(4), DATEPART(YEAR, DATEADD(MONTH, -1, @FromDate)))+ '.dbo.[Datamessage_'+convert(varchar(6), DATEADD(MONTH, -1, @FromDate), 112)+'] d ON s.SensorID = d.SensorID\r\nINNER JOIN dbo.[Account] a on s.AccountID = a.AccountID\r\nWHERE d.InsertDate >= ''' + CONVERT(VARCHAR(30), @FromDate,120) +''' \r\n  AND d.InsertDate < DATEADD(HOUR, 1, ''' + CONVERT(VARCHAR(30), @FromDate,120) +''')\r\n  AND a.IsHXEnabled = 1\r\nGROUP BY s.AccountID, CONVERT(DATE, d.MessageDate),s.SensorID;'\r\nINSERT INTO #DMCounts (AccountID, SensorID, MessageDate, Counts, Total)\r\nEXEC (@SQL)\r\n--PRINT @SQL\r\n\r\n--this calendar months\r\nSET @SQL =\r\n'SELECT\r\n  s.AccountID, \r\n  s.SensorID,\r\n  MessageDate = CONVERT(DATE, d.MessageDate),\r\n  Counts      = COUNT(*),\r\n  Total       = NULL\r\nFROM dbo.[Sensor] s WITH (NOLOCK)\r\nINNER JOIN iMonnitMessages' +CONVERT(VARCHAR(4), DATEPART(YEAR, @FromDate))+ '.dbo.[Datamessage_'+convert(varchar(6), @FromDate, 112)+'] d ON s.SensorID = d.SensorID\r\nINNER JOIN dbo.[Account] a on s.AccountID = a.AccountID\r\nWHERE d.InsertDate >= ''' + CONVERT(VARCHAR(30), @FromDate,120) +''' \r\n  AND d.InsertDate < DATEADD(HOUR, 1, ''' + CONVERT(VARCHAR(30), @FromDate,120) +''')\r\n  AND a.IsHXEnabled = 1\r\nGROUP BY s.AccountID, CONVERT(DATE, d.MessageDate),s.SensorID;'\r\nINSERT INTO #DMCounts (AccountID, SensorID, MessageDate, Counts, Total)\r\nEXEC (@SQL)\r\n--PRINT @SQL\r\n\r\nUPDATE d\r\nSET d.Total = d1.Counts2\r\nFROM #dmcounts d\r\nINNER JOIN (\r\n            SELECT \r\n              d1.SensorID, \r\n              d1.AccountID, \r\n              d1.MessageDate,\r\n              Counts2 = d1.counts + ISNULL(MAX(s.Total), 0)\r\n            from #dmcounts d1 \r\n            LEFT JOIN dbo.[CreditLog] s ON d1.MessageDate = s.MessageDate AND s.SensorID = d1.SensorID AND s.AccountID = d1.AccountID AND d1.MessageDate  != @FromDate\r\n            GROUP BY d1.SensorID, d1.AccountID, d1.MessageDate, d1.counts) d1 on d.MessageDate = d1.MessageDate AND d.SensorID = d1.SensorID AND d.AccountID = d1.AccountID AND d.MessageDate  != @FromDate;\r\n\r\n--Update if the sensor/account already exists for previous hour\r\nSET @SQL = \r\n'UPDATE l\r\nSET Hour' + CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate)) + ' = d.Counts,\r\n    Total = l.Total + d.Counts\r\nFROM ' + @PrimaryName + 'imonnit.dbo.[CreditLog] l\r\nINNER JOIN (SELECT \r\n              Counts = SUM(Counts), \r\n              SensorID, \r\n              AccountID,\r\n              MessageDate\r\n            FROM #DMCounts d\r\n            GROUP BY AccountID, SensorID, MessageDate\r\n           ) d ON d.AccountID = l.AccountID AND d.SensorID = l.SensorID AND d.MessageDate = l.MessageDate AND CONVERT(DATE, l.InsertDate) = CONVERT(DATE, ''' + CONVERT(VARCHAR(30), @FromDate,120) +''')\r\nWHERE Hour' + CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate)) + ' = 0';\r\n\r\n--PRINT @SQL\r\nEXEC (@SQL);\r\n\r\n--Insert if the sensor/account record doesn't already exist\r\nSET @SQL = \r\n'INSERT INTO ' + @PrimaryName + 'imonnit.dbo.[CreditLog] (AccountID, SensorID,  InsertDate, MessageDate, Hour' + CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate)) + ', Total)\r\nSELECT\r\n  d.AccountID,\r\n  d.SensorID,\r\n  InsertDate            = CONVERT(DATE, ''' + CONVERT(VARCHAR(30), @FromDate,120) + '''),\r\n  d.MessageDate,\r\n  HourX                 = SUM(Counts),\r\n  Total                 = ISNULL(d.Total,d.Counts)\r\nFROM #DMcounts d\r\nLEFT JOIN ' + @PrimaryName + 'imonnit.dbo.[CreditLog] l on d.AccountID = l.AccountID AND  d.SensorID = l.SensorID  AND d.MessageDate = l.MessageDate AND CONVERT(Date, l.InsertDate) = CONVERT(DATE, ''' + CONVERT(VARCHAR(30), @FromDate,120) +''')\r\nWHERE l.CreditLogID IS NULL\r\nGROUP BY d.AccountID, d.SensorID, d.MessageDate, d.Total, d.Counts';\r\n\r\n--PRINT @SQL\r\nEXEC (@SQL);\r\n\r\nSET @SQL =\r\n'\r\nUPDATE t\r\n  SET Consumed' + CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate)) + ' = tl.HourlyOverage\r\nFROM ' + @PrimaryName + 'imonnit.dbo.[CreditLog] t\r\nINNER JOIN (SELECT \r\n              s.AccountID,\r\n              s.SensorID,\r\n              s.MessageDate,\r\n              HourlyOverage = SUM(CASE WHEN Total - Hour' + CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate)) + ' - 150 < 0 THEN Total - Hour' + CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate)) + ' - 150 else 0 END + Hour' + CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate)) + ')\r\n            FROM ' + @PrimaryName + 'imonnit.dbo.[CreditLog] s\r\n            WHERE s.InsertDate = CONVERT(DATE,''' + CONVERT(VARCHAR(30), @FromDate,120) +''')\r\n              AND Total > 150\r\n            GROUP BY s.AccountID, s.SensorID, s.MessageDate\r\n           ) tl on t.SensorID = tl.SensorID and t.AccountID = tl.AccountID and t.MessageDate = tl.MessageDate and t.InsertDate = CONVERT(DATE, ''' + CONVERT(VARCHAR(30), @FromDate,120) + ''')\r\n';\r\n--print @sql;\r\nEXEC (@SQL);\r\n\r\nSET @SQL =\r\n'\r\nSELECT\r\n  AccountID,\r\n  Consumed = SUM(Consumed'+CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate))+')\r\nFROM ' + @PrimaryName + 'imonnit.dbo.[CreditLog]\r\nWHERE InsertDate =  CONVERT(DATE,''' + CONVERT(VARCHAR(30), @FromDate,120) +''')\r\nGROUP BY AccountID\r\nHAVING SUM(Consumed'+CONVERT(VARCHAR(2), DATEPART(HOUR, @FromDate))+') > 0\r\n';\r\n--print @sql;\r\nINSERT INTO #ConsumedRecords (AccountID, Consumed)\r\nEXEC (@SQL);\r\n\r\nSELECT * FROM #ConsumedRecords;\r\n\r\nSELECT \r\n  r.* \r\nFROM dbo.[Credit] r \r\nINNER JOIN #ConsumedRecords c on r.AccountID = c.AccountID\r\nWHERE ISNULL(IsDeleted, 0) = 0\r\n  and r.ExhaustedDate IS NULL;\r\n\r\ndrop table #DMCounts\r\ndrop table #ConsumedRecords\r\n")]
  internal class LogHXUsage : BaseDBMethod
  {
    public DataSet Result { get; private set; }

    public LogHXUsage() => this.Result = this.ToDataSet();

    public static DataSet Exec() => new Credit.LogHXUsage().Result;
  }

  [DBMethod("Credit_LoadRemaingCreditsForAccount")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  RemainingCredits  = (SUM(nc.ActivatedCredits) - SUM(nc.UsedCredits))\r\nFROM dbo.[Credit] nc\r\nINNER JOIN dbo.[NotificationCreditType] nct ON nct.NotificationCreditTypeID = nc.CreditTypeID\r\nWHERE nc.AccountID      = @AccountID\r\n  AND nct.CreditClassification = @CreditClassification \r\n  AND nc.ActivationDate < DATEADD(DAY, 1, GETUTCDATE())\r\n  AND (nc.ExpirationDate > GETUTCDATE() or nc.ExpirationDate IS NULL)\r\n  AND nc.UsedCredits    < nc.ActivatedCredits\r\n  AND (nc.IsDeleted IS NULL OR nc.IsDeleted = 0);\r\n")]
  internal class LoadRemaingCreditsForAccount : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("CreditClassification", typeof (int))]
    public int CreditClassification { get; private set; }

    public int Result { get; private set; }

    public LoadRemaingCreditsForAccount(long accountID, eCreditClassification creditClassification)
    {
      this.AccountID = accountID;
      this.CreditClassification = creditClassification.ToInt();
      this.Result = this.ToScalarValue<int>();
    }
  }
}
