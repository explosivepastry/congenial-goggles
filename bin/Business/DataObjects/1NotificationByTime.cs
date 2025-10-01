// Decompiled with JetBrains decompiler
// Type: Monnit.Data.NotificationByTime
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit.Data;

internal class NotificationByTime
{
  [DBMethod("NotificationByTime_UpdateNextEvaluationDate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE dbo.[NotificationByTime]\r\nSET NextEvaluationDate = @NextEvaluationDate\r\nWHERE NotificationByTimeID = @NotificationByTimeID;\r\n")]
  internal class UpdateNextEvaluationDate : BaseDBMethod
  {
    [DBMethodParam("NotificationByTimeID", typeof (long))]
    public long NotificationByTimeID { get; private set; }

    [DBMethodParam("NextEvaluationDate", typeof (DateTime))]
    public DateTime NextEvaluationDate { get; private set; }

    public UpdateNextEvaluationDate(long notificationByTimeID, DateTime nextEvaluationDate)
    {
      this.NotificationByTimeID = notificationByTimeID;
      this.NextEvaluationDate = nextEvaluationDate;
      this.Execute();
    }
  }
}
