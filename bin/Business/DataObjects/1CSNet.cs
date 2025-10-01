// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CSNet
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class CSNet
{
  [DBMethod("CSNet_SetGatewaysUrgentTrafficFlag")]
  [DBMethodBody(DBMS.SqlServer, "\r\n--Don't include CRAPPY CSNetID's\r\nIF @CSNetID NOT IN (SELECT CSNetID FROM dbo.CSNet WITH(NOLOCK) WHERE AccountID = 2372)\r\n  AND @CSNetID !=2\r\nBEGIN\r\n\r\n\tUPDATE g\r\n\t  SET UrgentTraffic = 1\r\n\tFROM dbo.[Gateway] g WITH(ROWLOCK)\r\n\tWHERE g.CSNetID = @CSNetID;\r\n\r\nEND\r\n")]
  [DBMethodBody(DBMS.SQLite, "UPDATE Gateway set UrgentTraffic = 1 WHERE CSNetID = @CSNetID")]
  internal class SetGatewaysUrgentTrafficFlag : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public SetGatewaysUrgentTrafficFlag(long csnetID)
    {
      this.CSNetID = csnetID;
      this.Execute();
    }
  }

  [DBMethod("CSNet_RequiresUrgentProcessing")]
  [DBMethodBody(DBMS.SqlServer, "\r\nWITH CTE_Rows AS\r\n(\r\nSELECT TOP 1 *\r\nFROM Sensor s WITH (NoLock) \r\nWHERE s.CSNetID = @csnetid\r\n  AND s.IsDeleted = 0\r\n  AND s.ApplicationID IN (12,13,76))\r\nSELECT COUNT(*) FROM CTE_Rows\r\n")]
  internal class RequiresUrgentProcessing : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public bool Result { get; set; }

    public RequiresUrgentProcessing(long csnetID)
    {
      this.CSNetID = csnetID;
      this.Result = this.ToScalarValue<bool>();
    }
  }

  [DBMethod("CSNet_IgnoreCSNetIDList")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT CSNetID\r\nFROM CSNet WHERE AccountID in (2,2372,64860)\r\n")]
  internal class IgnoreCSNetIDList : BaseDBMethod
  {
    public List<long> Result { get; set; }

    public IgnoreCSNetIDList()
    {
      this.Result = new List<long>();
      try
      {
        foreach (DataRow row in (InternalDataCollectionBase) this.ToDataTable().Rows)
          this.Result.Add(row[0].ToLong());
      }
      catch
      {
      }
    }
  }
}
