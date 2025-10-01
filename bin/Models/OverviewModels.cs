// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CellDataUseModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace iMonnit.Models;

public class CellDataUseModel
{
  public DateTime Date { get; set; }

  public int Month => this.Date.Month;

  public int Day => this.Date.Day;

  public double MB { get; set; }

  public double KB => this.MB * 1000.0;

  public static List<CellDataUseModel> LoadFromDataTable(DataTable dt)
  {
    List<CellDataUseModel> cellDataUseModelList = new List<CellDataUseModel>();
    foreach (DataRow row in (InternalDataCollectionBase) dt.Rows)
      cellDataUseModelList.Add(new CellDataUseModel()
      {
        Date = row["Date"].ToDateTime(),
        MB = row["MB"].ToDouble()
      });
    return cellDataUseModelList;
  }
}
