// Decompiled with JetBrains decompiler
// Type: Monnit.Data.LoadActiveBySensorID
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

[DBMethod("OTARequest_LoadActiveBySensorID")]
[DBMethodBody(DBMS.SqlServer, "\r\nSELECT * \r\nFROM OTARequestSensor s\r\nWHERE s.SensorID = @SensorID\r\nAND s.Status in (0,1)\r\n")]
internal class LoadActiveBySensorID : BaseDBMethod
{
  [DBMethodParam("SensorID", typeof (long))]
  public long SensorID { get; private set; }

  public List<OTARequestSensor> Result { get; private set; }

  public LoadActiveBySensorID(long sensorID)
  {
    this.SensorID = sensorID;
    this.Result = BaseDBObject.Load<OTARequestSensor>(this.ToDataTable());
  }

  public static List<OTARequestSensor> Exec(long sensorID)
  {
    return new LoadActiveBySensorID(sensorID).Result;
  }
}
