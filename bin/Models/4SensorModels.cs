// Decompiled with JetBrains decompiler
// Type: Data.DeviceStatusModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Data;

internal class DeviceStatusModel
{
  [DBMethod("DeviceStatusModel_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF @AccountID IN (2,2372, 657)\r\n  SET @AccountID = 0;\r\n\r\nCREATE TABLE #Temp1\r\n(\r\n  Type VARCHAR(300),\r\n  Active INT,\r\n  Total INT,\r\n  Alerting INT\r\n);\r\n\r\nSELECT\r\ns.SensorID, \r\nMeetsNotificationRequirement = CASE WHEN    LastCommunicationDate >= DATEADD(minute, -2.0* ReportInterval, GETUTCDATE()) AND LastCommunicationDate < DATEADD(MINUTE, 10, GETUTCDATE()) THEN ISNULL(CONVERT(INT, MeetsNotificationRequirement), -1)\r\n                                      ELSE  CASE WHEN ISNULL(CONVERT(INT, MeetsNotificationRequirement), -1) = 1 THEN 2 \r\n                                      ELSE  CASE WHEN ISNULL(CONVERT(INT, MeetsNotificationRequirement), -1) = 0 THEN  3  \r\n                                      ELSE 4  END END END\r\nINTO #SensorList \r\nFROM dbo.[Sensor] s WITH (NOLOCK)\r\nLEFT JOIN dbo.[DataMessage_Last] d WITH (NOLOCK) ON d.SensorID = s.SensorID --and LastCommunicationDate >= dateadd(day, -90, getutcdate())\r\nWHERE AccountID = @AccountID;\r\n\r\nSELECT\r\ng.GatewayID,\r\nMeetsNotificationRequirement = CASE WHEN    LastCommunicationDate >=  DATEADD(minute, -2.0* ReportInterval, GETUTCDATE()) AND LastCommunicationDate < DATEADD(MINUTE, 10, GETUTCDATE()) THEN ISNULL(CONVERT(INT, MeetsNotificationRequirement), -1)\r\n                                      ELSE  CASE WHEN ISNULL(CONVERT(INT, MeetsNotificationRequirement), -1) = 1 THEN 2 \r\n                                      ELSE  CASE WHEN ISNULL(CONVERT(INT, MeetsNotificationRequirement), -1) = 0 THEN  3  \r\n                                      ELSE 4  END END END\r\nINTO #GatewayList\r\nFROM dbo.[Gateway] g WITH (NOLOCK)\r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON g.CSNetID = c.CSNetID\r\nLEFT JOIN dbo.[GatewayMessage] gm WITH (NOLOCK) ON g.GatewayID = gm.GatewayID AND gm.ReceivedDate = g.LastCommunicationDate \r\nWHERE AccountID = @AccountID\r\n  AND g.SensorID IS NULL;\r\n\r\n  WITH CTE_Results AS\r\n  (\r\n    SELECT \r\n      *\r\n    FROM #SensorList\r\n  )\r\n  INSERT INTO #Temp1\r\n  SELECT\r\n    Type      = 'Sensor',\r\n    Active    = (SELECT COUNT(*) FROM CTE_Results where MeetsNotificationRequirement not in ( -1, 2, 3,4)),\r\n    Total     = (SELECT COUNT(*) FROM CTE_Results ),\r\n    Alerting  = (SELECT COUNT(*) FROM CTE_Results where MeetsNotificationRequirement in (1, 2));\r\n    \r\n  WITH CTE_Results2 AS\r\n  (\r\n    SELECT \r\n      * \r\n    FROM #GatewayList\r\n  )\r\n  INSERT INTO #Temp1\r\n  SELECT\r\n    Type      = 'Gateway',\r\n    Active    = (SELECT COUNT(*) FROM CTE_Results2 where MeetsNotificationRequirement NOT IN ( -1, 2, 3,4)),\r\n    Total     = (SELECT COUNT(*) FROM CTE_Results2 ),\r\n    Alerting  = (SELECT COUNT(*) FROM CTE_Results2 where MeetsNotificationRequirement IN (1, 2));\r\n\r\n  SELECT * FROM #temp1 \r\n  ORDER BY Type DESC;\r\n")]
  internal class LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<iMonnit.Models.DeviceStatusModel> Result { get; private set; }

    public LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<iMonnit.Models.DeviceStatusModel>(this.ToDataTable());
    }
  }
}
