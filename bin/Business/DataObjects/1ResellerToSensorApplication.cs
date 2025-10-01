// Decompiled with JetBrains decompiler
// Type: Monnit.Data.ResellerToSensorApplication
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class ResellerToSensorApplication
{
  [DBMethod("ResellerToSensorApplication_LoadByResellerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[ResellerToSensorApplication]\r\nWHERE ResellerAccountID = @ResellerID;\r\n")]
  internal class LoadByResellerID : BaseDBMethod
  {
    [DBMethodParam("ResellerID", typeof (long))]
    public long ResellerID { get; private set; }

    public List<Monnit.ResellerToSensorApplication> Result { get; private set; }

    public LoadByResellerID(long resellerID)
    {
      this.ResellerID = resellerID;
      this.Result = BaseDBObject.Load<Monnit.ResellerToSensorApplication>(this.ToDataTable());
    }
  }

  [DBMethod("ResellerToSensorApplication_DeleteByResellerID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[ResellerToSensorApplication]\r\nWHERE ResellerAccountID = @ResellerID\r\n  AND SensorApplicationID IN (SELECT \r\n                                SensorApplicationID\r\n                              FROM dbo.[SensorApplication] \r\n                              WHERE ApplicationID = @ApplicationID);\r\n")]
  internal class DeleteByResellerID : BaseDBMethod
  {
    [DBMethodParam("ResellerID", typeof (long))]
    public long ResellerID { get; private set; }

    [DBMethodParam("ApplicationID", typeof (long))]
    public long ApplicationID { get; private set; }

    public DeleteByResellerID(long resellerID, long monnitApplicationID)
    {
      this.ApplicationID = monnitApplicationID;
      this.ResellerID = resellerID;
      this.Execute();
    }
  }
}
