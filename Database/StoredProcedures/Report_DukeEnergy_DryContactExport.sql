USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_DukeEnergy_DryContactExport]    Script Date: 6/24/2024 3:07:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_DukeEnergy_DryContactExport]
  @Default_OwnerAccountID BIGINT
AS
/************************************

  Last readings for all dry contact
  sensors under the Duke Account Tree

*************************************/
DECLARE @TimeZoneIDString VARCHAR(100)

SELECT
  @TimeZoneIDString = t.TimeZoneIDString
FROM dbo.[Account] a
INNER JOIN dbo.[TimeZone] t on a.TimeZoneID = t.TimeZoneID
WHERE a.AccountID = @Default_OwnerAccountID;

SELECT
  d.[DataMessageGUID], 
  d.[SensorID],
  s.[SensorName],
  [Date]                  = dbo.GetLocalTime([MessageDate], @TimeZoneIDString),
  [Value]                 = CASE WHEN [Data] = 'False' THEN 0 ELSE 1 END,
  [Formatted Value]       = CASE WHEN [Data] = 'True' THEN 'Paging Ceased' ELSE 'Paging Active' END,
  d.[Battery],
  [Raw Data]              = d.[Data],
  [GatewayID]             = d.[GatewayID], 
  [Alert Sent]            = CASE WHEN d.[MeetsNotificationRequirement] = 1 THEN 'TRUE' ELSE 'FALSE' END,
  [SignalStrength]        = [SignalStrength],
  d.[Voltage],
  [Special Export Value]  = '',
  [GenerationType]        = s.[GenerationType],
  [DeviceTypeID]          = s.[SensorTypeID]
INTO #tempSensor
FROM dbo.[Sensor] s 
INNER JOIN dbo.[Account] a ON s.[AccountID] = a.[AccountID]
INNER JOIN dbo.[DataMessage] d ON s.[SensorID] = d.[SensorID] AND s.[LastDataMessageGUID] = d.[DataMessageGUID] AND s.[LastCommunicationDate] = d.[MessageDate]
WHERE a.[AccountIDTree] LIKE '%*'+CONVERT(VARCHAR(30), @Default_OwnerAccountID)+'*%'
  AND s.[ApplicationID] = 3;


UPDATE #tempSensor
SET SignalStrength = (SignalStrength + 90) + 60
WHERE  GenerationType = 'GEN2' AND SignalStrength > -90;

UPDATE #tempSensor
SET SignalStrength = ROUND((SignalStrength + 115) * 2.4, 0)
WHERE GenerationType = 'GEN2' AND SignalStrength <= -90;

UPDATE #tempSensor
SET SignalStrength = ((SignalStrength+70)*2)+60
WHERE [DeviceTypeID] = 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength > -70;

UPDATE #tempSensor
SET SignalStrength = ROUND((SignalStrength+84) * 4.2857, 0)
WHERE [DeviceTypeID] = 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength <= -70;

UPDATE #tempSensor
SET SignalStrength = ((SignalStrength+80) * 4.0 / 3.0) + 60
WHERE [DeviceTypeID] != 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength > -80;

UPDATE #tempSensor
SET SignalStrength = (SignalStrength+100) * 3
WHERE [DeviceTypeID] != 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength <= -80;

UPDATE #tempSensor 
SET SignalStrength = 100 where SignalStrength >= 100;

SELECT 
  [DataMessageGUID], 
  [SensorID],
  [SensorName],
  [Date],
  [Value],
  [Formatted Value],
  [Battery],
  [Raw Data],
  [GatewayID], 
  [Alert Sent],
  [SignalStrength],
  [Voltage],
  [Special Export Value]
FROM #tempSensor
ORDER BY SensorName;

GO


