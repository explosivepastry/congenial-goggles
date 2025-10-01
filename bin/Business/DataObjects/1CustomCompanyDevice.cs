// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CustomCompanyDevice
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class CustomCompanyDevice
{
  [DBMethod("CustomCompanyDevice_LoadSettings")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @SQL      VARCHAR(3000) = '',\r\n        @RowID    INT = 1,\r\n        @MaxRowID INT,\r\n        @Type     INT = 2; --2 = don't do update since there wasn't a device actually found \r\n\r\nCREATE TABLE #Temp1\r\n(\r\n  RowNum                INT,\r\n  CustomCompanyDeviceID BIGINT,\r\n  CompanyID             INT,\r\n  SKU                   VARCHAR(300),\r\n  Name                  VARCHAR(300),\r\n  Value                 VARCHAR(300),\r\n  ApplicationID         INT,\r\n  GatewayTypeID         INT,\r\n  IgnoreOnClose         BIT\r\n);\r\n\r\nIF @DoUpdate IS NULL\r\n  SET @DoUpdate = 0;\r\n\r\nINSERT INTO #Temp1\r\nSELECT\r\n  RowNum = Row_Number() OVER (PARTITION BY SensorID ORDER BY CustomCompanyDeviceID), \r\n  c2.CustomCompanyDeviceID,\r\n  c2.CompanyID,\r\n  c2.SKU,\r\n  c2.Name,\r\n  c2.Value,\r\n  c2.ApplicationID,\r\n  c2.GatewayTypeID,\r\n  c2.IgnoreOnClose\r\nFROM dbo.[Sensor] s with (NOLOCK)\r\nINNER JOIN dbo.[CustomCompanyDevice] c2  with (NOLOCK) on s.sku = c2.sku\r\nWHERE SensorID = @DeviceID;\r\n\r\nIF @@ROWCOUNT > 0\r\n  SET @Type = 0;\r\n\r\nINSERT INTO #Temp1\r\nSELECT\r\n  RowNum = Row_Number() OVER (PARTITION BY SensorID ORDER BY CustomCompanyDeviceID), \r\n  c2.CustomCompanyDeviceID,\r\n  c2.CompanyID,\r\n  c2.SKU,\r\n  c2.Name,\r\n  c2.Value,\r\n  c2.ApplicationID,\r\n  c2.GatewayTypeID,\r\n  c2.IgnoreOnClose\r\nFROM dbo.[Gateway] s with (NOLOCK)\r\nINNER JOIN dbo.[CustomCompanyDevice] c2  with (NOLOCK) on s.sku = c2.sku\r\nWHERE GatewayID = @DeviceID;\r\n\r\n\r\nIF @@ROWCOUNT > 0\r\n  SET @Type = 1;\r\n\r\nSELECT CustomCompanyDeviceID, CompanyID, Sku, Name, Value, ApplicationID, GatewayTypeID, IgnoreOnClose FROM #temp1;\r\n\r\nSET @MaxRowID = @@ROWCOUNT;\r\n\r\nIF @DoUpdate = 1\r\nBEGIN\r\n\r\n    IF @Type = 0\r\n    BEGIN\r\n\r\n        SET @SQL = 'UPDATE Sensor SET '\r\n\r\n        WHILE @RowID <= @MaxRowID\r\n        BEGIN\r\n        \r\n          SET @SQL = @SQL + \r\n          (SELECT \r\n              t.Name + ' = ' +  t.value + case when @RowID < @MaxRowID then ',' else '' END\r\n            FROM #temp1 t\r\n            WHERE RowNum = @RowID)\r\n\r\n            SET @RowID = @RowID + 1;\r\n\r\n        END\r\n\r\n        SET @SQL = @SQL + ' WHERE SensorID = ' + CONVERT(VARCHAR(20), @DeviceID) + ';'\r\n\r\n        PRINT @Sql\r\n\r\n    END ELSE IF @Type = 1\r\n    BEGIN\r\n\r\n    \r\n        SET @SQL = 'UPDATE Gateway SET '\r\n\r\n        WHILE @RowID <= @MaxRowID\r\n        BEGIN\r\n        \r\n            SET @SQL = @SQL + \r\n            (SELECT \r\n              t.Name + ' = ' +  t.value + case when @RowID < @MaxRowID then ',' else '' END\r\n             FROM #temp1 t\r\n             WHERE RowNum = @RowID)\r\n\r\n            SET @RowID = @RowID + 1;\r\n\r\n        END\r\n\r\n        SET @SQL = @SQL + ' WHERE GatewayID = ' + CONVERT(VARCHAR(20), @DeviceID) + ';'\r\n\r\n        PRINT @Sql\r\n\r\n    END\r\n\r\nEND\r\n")]
  internal class LoadSettings : BaseDBMethod
  {
    [DBMethodParam("DeviceID", typeof (long))]
    public long DeviceID { get; private set; }

    [DBMethodParam("DoUpdate", typeof (bool))]
    public bool DoUpdate { get; private set; }

    public List<Monnit.CustomCompanyDevice> Result { get; private set; }

    public LoadSettings(long deviceID, bool doUpdate)
    {
      this.DeviceID = deviceID;
      this.DoUpdate = doUpdate;
      this.Result = BaseDBObject.Load<Monnit.CustomCompanyDevice>(this.ToDataTable());
    }
  }
}
