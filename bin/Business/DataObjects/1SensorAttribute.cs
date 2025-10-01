// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SensorAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class SensorAttribute
{
  [DBMethod("SensorAttribute_LoadByDatum")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[SensorAttribute]\r\nWHERE [SensorID] = @SensorID\r\n  AND [Name]     = ('DatumIndex|'+CAST(@DatumIndex AS VARCHAR));\r\n")]
  internal class LoadByDatum : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("DatumIndex", typeof (int))]
    public int DatumIndex { get; private set; }

    public Monnit.SensorAttribute Result { get; private set; }

    public LoadByDatum(long sensorID, int datumindex)
    {
      this.SensorID = sensorID;
      this.DatumIndex = datumindex;
      this.Result = BaseDBObject.Load<Monnit.SensorAttribute>(this.ToDataTable()).FirstOrDefault<Monnit.SensorAttribute>();
    }
  }

  [DBMethod("SensorAttribute_LoadAllByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n a.*\r\nFROM dbo.[SensorAttribute] a WITH (NOLOCK)\r\nINNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON a.SensorID = s.SensorID\r\nWHERE s.AccountID = @AccountID;\r\n")]
  internal class LoadAllByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.SensorAttribute> Result { get; private set; }

    public LoadAllByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.SensorAttribute>(this.ToDataTable());
    }
  }
}
