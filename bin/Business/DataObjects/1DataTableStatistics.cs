// Decompiled with JetBrains decompiler
// Type: Monnit.Data.DataTableStatistics
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit.Data;

internal class DataTableStatistics
{
  [DBMethod("DataTableStatistics_InsertData")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSelect TableName , EvaluationDate , TotalRowCount into #TEMPOLD from DataTableStatistics where DataTableStatisticsID in (Select MAX(DataTableStatisticsID) from DataTableStatistics group by TableName)\t\r\nSELECT  tbl.name as TableName,GetUTCDate() as EvaluationDate,CAST(p.rows AS float) TotalRowCount\r\ninto #TEMP\r\nFROM sys.tables AS tbl\r\nINNER JOIN sys.indexes AS idx ON idx.object_id = tbl.object_id and idx.index_id < 2\r\nINNER JOIN sys.partitions AS p ON p.object_id=CAST(tbl.object_id AS int)\r\nAND p.index_id=idx.index_id\r\nWHERE ((tbl.name in (N'DataMessage'\r\n\t,N'ExternalDataSubscriptionAttempt'\r\n\t,N'ExternalDataSubscriptionResponse'\r\n\t,N'GatewayMessage'\r\n\t,N'NotificationRecorded'\r\n    ,N'ExportStatistics')\r\nAND SCHEMA_NAME(tbl.schema_id)='dbo'))\r\n\t\t\r\n\t\t\t\t\r\ninsert into DataTableStatistics(TableName,EvaluationDate,TotalRowCount,SecondsSinceLastEvaluation,RowsSincelastEvaluation,Rate) Select t.TableName\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t , t.EvaluationDate\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t , t.TotalRowCount\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t , datediff(second,t1.EvaluationDate,t.EvaluationDate) as SecondsSinceLastEvaluation\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t , (t.TotalRowCount - t1.TotalRowCount ) as RowsSincelastEvaluation\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t , (t.TotalRowCount - t1.TotalRowCount ) /  datediff(second,t1.EvaluationDate,t.EvaluationDate) as Rate \r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t     from #TEMP t\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t     left join #TEMPOLD t1 on t1.TableName = t.TableName")]
  internal class InsertData : BaseDBMethod
  {
    public bool Result { get; private set; }

    public InsertData()
    {
      try
      {
        this.Execute();
        this.Result = true;
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
        this.Result = false;
      }
    }
  }
}
