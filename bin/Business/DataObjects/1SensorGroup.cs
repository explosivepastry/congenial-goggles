// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SensorGroup
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class SensorGroup
{
  [DBMethod("SensorGroup_LoadSensors")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[SensorGroupSensor] sgs\r\nINNER JOIN dbo.[Sensor] s ON s.SensorID = sgs.SensorID\r\nWHERE sgs.SensorGroupID = @SensorGroupID\r\nORDER BY\r\n  sgs.Position;\r\n")]
  internal class LoadSensors : BaseDBMethod
  {
    [DBMethodParam("SensorGroupID", typeof (long))]
    public long SensorGroupID { get; private set; }

    public List<SensorGroupSensor> Result { get; private set; }

    public LoadSensors(long sensorGroupID)
    {
      this.SensorGroupID = sensorGroupID;
      DataTable dataTable = this.ToDataTable();
      this.Result = BaseDBObject.Load<SensorGroupSensor>(dataTable);
      foreach (Monnit.Sensor sensor in BaseDBObject.Load<Monnit.Sensor>(dataTable))
      {
        foreach (SensorGroupSensor sensorGroupSensor in this.Result)
        {
          if (sensorGroupSensor.SensorID == sensor.SensorID)
            sensorGroupSensor.Sensor = sensor;
        }
      }
    }
  }

  [DBMethod("SensorGroup_RemoveSensors")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[SensorGroupSensor]\r\nWHERE SensorGroupID = @SensorGroupID;\r\n")]
  internal class RemoveSensors : BaseDBMethod
  {
    [DBMethodParam("SensorGroupID", typeof (long))]
    public long SensorGroupID { get; private set; }

    public RemoveSensors(long sensorGroupID)
    {
      this.SensorGroupID = sensorGroupID;
      this.Execute();
    }
  }

  [DBMethod("SensorGroup_LoadGateways")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[SensorGroupGateway] sgg\r\nINNER JOIN dbo.[Gateway] g ON g.GatewayID = sgg.GatewayID\r\nWHERE sgg.SensorGroupID = @SensorGroupID\r\nORDER BY\r\n  sgg.Position;\r\n")]
  internal class LoadGateways : BaseDBMethod
  {
    [DBMethodParam("SensorGroupID", typeof (long))]
    public long SensorGroupID { get; private set; }

    public List<SensorGroupGateway> Result { get; private set; }

    public LoadGateways(long sensorGroupID)
    {
      this.SensorGroupID = sensorGroupID;
      DataTable dataTable = this.ToDataTable();
      this.Result = BaseDBObject.Load<SensorGroupGateway>(dataTable);
      foreach (Monnit.Gateway gateway in BaseDBObject.Load<Monnit.Gateway>(dataTable))
      {
        foreach (SensorGroupGateway sensorGroupGateway in this.Result)
        {
          if (sensorGroupGateway.GatewayID == gateway.GatewayID)
            sensorGroupGateway.Gateway = gateway;
        }
      }
    }
  }

  [DBMethod("SensorGroup_RemoveGateways")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[SensorGroupGateway]\r\nWHERE SensorGroupID = @SensorGroupID;\r\n")]
  internal class RemoveGateways : BaseDBMethod
  {
    [DBMethodParam("SensorGroupID", typeof (long))]
    public long SensorGroupID { get; private set; }

    public RemoveGateways(long sensorGroupID)
    {
      this.SensorGroupID = sensorGroupID;
      this.Execute();
    }
  }

  [DBMethod("SensorGroup_LoadByParentID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[SensorGroup] WITH (NOLOCK)\r\nWHERE ParentID = @ParentID;\r\n")]
  internal class LoadByParentID : BaseDBMethod
  {
    [DBMethodParam("ParentID", typeof (long))]
    public long ParentID { get; private set; }

    public List<Monnit.SensorGroup> Result { get; private set; }

    public LoadByParentID(long parentID)
    {
      this.ParentID = parentID;
      this.Result = BaseDBObject.Load<Monnit.SensorGroup>(this.ToDataTable());
    }
  }

  [DBMethod("SensorGroup_LoadByAccountIDandType")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[SensorGroup] WITH (NOLOCK)\r\nWHERE AccountID = @AccountID \r\n  AND [type]  like '%' + @Type + '%'\r\nORDER BY \r\n Name DESC;\r\n")]
  internal class LoadByAccountIDandType : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("Type", typeof (string))]
    public string Type { get; private set; }

    public List<Monnit.SensorGroup> Result { get; private set; }

    public LoadByAccountIDandType(long accountID, string type)
    {
      this.AccountID = accountID;
      this.Type = type;
      this.Result = BaseDBObject.Load<Monnit.SensorGroup>(this.ToDataTable());
    }
  }

  [DBMethod("SensorGroup_LoadByType")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[SensorGroup] WITH (NOLOCK)\r\nWHERE [type]  like '%' + @Type + '%';\r\n\r\n")]
  internal class LoadByType : BaseDBMethod
  {
    [DBMethodParam("Type", typeof (string))]
    public string Type { get; private set; }

    public List<Monnit.SensorGroup> Result { get; private set; }

    public LoadByType(string type)
    {
      this.Type = type;
      this.Result = BaseDBObject.Load<Monnit.SensorGroup>(this.ToDataTable());
    }
  }
}
