// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ScheduledReportsToStorage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class ScheduledReportsToStorage
{
  [DBMethod("ScheduledReportsToStorage_ListOfSafeToDeleteReports")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[ScheduledReportsToStorage]\r\nWHERE DateFileCreated IS NOT NULL\r\n  AND DateFileCreated < @LessThanThisDate;\r\n")]
  internal class ListOfSafeToDeleteReports : BaseDBMethod
  {
    [DBMethodParam("LessThanThisDate", typeof (DateTime))]
    public DateTime LessThanThisDate { get; private set; }

    public List<Monnit.ScheduledReportsToStorage> Result { get; private set; }

    public ListOfSafeToDeleteReports(DateTime lessTHanThisDate)
    {
      this.LessThanThisDate = lessTHanThisDate;
      this.Result = BaseDBObject.Load<Monnit.ScheduledReportsToStorage>(this.ToDataTable());
    }
  }

  [DBMethod("ScheduledReportsToStorage_BatchOfReportsToDelete")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 100 \r\n\t*\r\nFROM ScheduledReportsToStorage\r\nWHERE ScheduledReportsToStorageID < 4821705 -- First ID for Reports starting 1/1/24\r\n\tAND ScheduledReportsToStorageID > ISNULL(@BatchStartScheduledReportsToStorageID, 0)\r\nORDER BY ScheduledReportsToStorageID ASC;\r\n")]
  internal class BatchOfReportsToDelete : BaseDBMethod
  {
    [DBMethodParam("BatchStartScheduledReportsToStorageID", typeof (long))]
    public long BatchStartScheduledReportsToStorageID { get; private set; }

    public List<Monnit.ScheduledReportsToStorage> Result { get; private set; }

    public BatchOfReportsToDelete(long batchStartScheduledReportsToStorageID)
    {
      this.BatchStartScheduledReportsToStorageID = batchStartScheduledReportsToStorageID;
      this.Result = BaseDBObject.Load<Monnit.ScheduledReportsToStorage>(this.ToDataTable());
    }
  }
}
