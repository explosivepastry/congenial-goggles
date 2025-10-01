// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SensorProfileIndex
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class SensorProfileIndex
{
  [DBMethod("SensorProfileIndex_LoadBySensorProfileID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  * \r\nFROM dbo.[SensorProfileIndex] WITH (NOLOCK)\r\nWHERE SensorProfileID = @SensorProfileID \r\nORDER BY StartIndex ASC;\r\n")]
  internal class LoadBySensorProfileID : BaseDBMethod
  {
    [DBMethodParam("SensorProfileID", typeof (long))]
    public long SensorProfileID { get; private set; }

    public List<Monnit.SensorProfileIndex> Result { get; private set; }

    public LoadBySensorProfileID(long sensorProfileID)
    {
      this.SensorProfileID = sensorProfileID;
      this.Result = (List<Monnit.SensorProfileIndex>) null;
      DataTable dataTable = this.ToDataTable();
      if (dataTable.Rows.Count <= 0)
        return;
      this.Result = BaseDBObject.Load<Monnit.SensorProfileIndex>(dataTable);
    }
  }
}
