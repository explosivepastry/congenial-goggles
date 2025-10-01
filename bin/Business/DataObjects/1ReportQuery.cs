// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ReportQuery
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class ReportQuery
{
  [DBMethod("ReportQuery_LoadByAccount")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @IsReseller BIT = (SELECT IsReseller From dbo.[Account] WHERE AccountID = @AccountID);\r\n\r\nSELECT DISTINCT\r\n  Tags = f.item\r\nINTO #temp_AccountTags \r\nFROM dbo.[Account] a with (NOLOCK)\r\nCROSS APPLY dbo.Split(Tags, '|') f\r\nWHERE AccountID = @AccountID;\r\n\r\nIF @IsReseller = 1 \r\n  SET @IsReseller = NULL;\r\n\r\n \r\nWITH CTE_Results as (\r\nSELECT \r\n  rq.*,\r\n  f.Item\r\nFROM dbo.[ReportQuery] rq WITH (NOLOCK)\r\nCROSS APPLY dbo.Split(Tags, '|') f\r\nWHERE (ISNULL(rq.AccountID, @AccountID) = @AccountID\r\n  AND (rq.AccountThemeID = @AccountThemeID OR rq.AccountThemeID IS NULL))\r\n  AND rq.IsDeleted = 0\r\n  AND TAGS IS NOT NULL\r\n  AND ISNULL(@IsReseller, CustomerAccess) = CustomerAccess\r\n  AND ReportTypeID = ISNULL(@ReportTypeID, ReportTypeID)\r\nUNION ALL\r\nSELECT \r\n  rq.*,\r\n  Item = null\r\nFROM dbo.[ReportQuery] rq WITH (NOLOCK)\r\nWHERE (ISNULL(rq.AccountID, @AccountID) = @AccountID\r\n  AND (rq.AccountThemeID = @AccountThemeID OR rq.AccountThemeID IS NULL))\r\n  AND rq.IsDeleted = 0\r\n  AND Tags IS NULL\r\n  AND ISNULL(@IsReseller, CustomerAccess) = CustomerAccess\r\n  AND ReportTypeID = ISNULL(@ReportTypeID, ReportTypeID)\r\n)\r\nSELECT DISTINCT \r\n  ReportQueryID,\r\n  Name,\r\n  Description,\r\n  AccountID,\r\n  AccountThemeID,\r\n  ReportBuilder,\r\n  SQL,\r\n  IsDeleted,\r\n  ScheduleAnnually,\r\n  ScheduleMonthly,\r\n  ScheduleWeekly,\r\n  ScheduleDaily,\r\n  ScheduleImmediately,\r\n  CustomerAccess,\r\n  MaxRunTime,\r\n  Tags,\r\n  ReportTypeID,\r\n  RequiresPreAggs,\r\n  SensorLimit\r\nFROM CTE_REsults\r\nWHERE Item IS NULL \r\n   OR Item IN (SELECT tags from #temp_AccountTags)\r\n\r\ndrop table #temp_AccountTags;\r\n")]
  internal class LoadByAccount : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("IsPremiere", typeof (bool))]
    public bool IsPremiere { get; private set; }

    [DBMethodParam("ReportTypeID", typeof (long))]
    public long ReportTypeID { get; private set; }

    public List<Monnit.ReportQuery> Result { get; private set; }

    public LoadByAccount(long accountThemeID, long accountID, bool isPremiere, long reportTypeID)
    {
      this.AccountThemeID = accountThemeID;
      this.AccountID = accountID;
      this.IsPremiere = isPremiere;
      this.ReportTypeID = reportTypeID;
      this.Result = BaseDBObject.Load<Monnit.ReportQuery>(this.ToDataTable());
    }
  }
}
