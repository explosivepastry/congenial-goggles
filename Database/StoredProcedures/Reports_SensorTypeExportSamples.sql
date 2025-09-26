USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_SensorTypeExportSamples]    Script Date: 7/1/2024 9:03:45 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_SensorTypeExportSamples]
  @Default_OwnerAccountID BIGINT,
  @AccountID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS

CREATE TABLE #DMtemp(
[MessageDate] DATETIME,
[SensorID] BIGINT,
[DataMessageGUID] VARCHAR(255),
[State] INT,
[SignalStrength] INT,
[LinkQuality] INT,
[Battery] INT,
[Data] VARCHAR(500),
[Voltage] DECIMAL(5,4),
[MeetsNotificationRequirement] BIT,
[InsertDate] DATETIME,
[GatewayID] BIGINT,
[HasNote] BIT
)

/*----- Testing Parameters ------*/
--DECLARE @SensorID BIGINT = 88559;
--DECLARE @FromDate DATETIME = '2016-01-01 00:00';
--DECLARE @ToDate DATETIME = '2016-03-01 00:00 '
--DECLARE @Default_OwnerAccountID BIGINT = 16737


--Proc Specific
DECLARE @SQL VARCHAR(2000)
declare @timezoneidentifier varchar(255)
declare @offsetminutes int
DECLARE @StartMonth DATETIME
DECLARE @EndMOnth DATETIME



  SELECT 
   @FromDate = DATEADD(minute, DATEDIFF(MINUTE, dbo.GetLocalTime(GetUTCDATE(), TimeZoneIDSTring), GETUTCDATE()), @FromDate),
   @ToDate = DATEADD(minute, DATEDIFF(MINUTE, dbo.GetLocalTime(GetUTCDATE(), TimeZoneIDSTring), GETUTCDATE()), @ToDate)
 FROM dbo.[TimeZone] tz      WITH(NOLOCK) 
 INNER JOIN dbo.[Account] a  WITH(NOLOCK) ON a.[TimeZoneID] = tz.[TimeZoneID]
 WHERE a.[AccountID] = 25056


SET @StartMonth = CONVERT(VARCHAR(7), @FromDate, 120)+'-01'
SET @EndMonth = CONVERT(VARCHAR(10), @ToDate, 120)

 SELECT 
   @TimezoneIdentifier = tz.[TimeZoneIDString] 
 FROM dbo.[TimeZone] tz      WITH(NOLOCK) 
 INNER JOIN dbo.[Account] a  WITH(NOLOCK) ON a.[TimeZoneID] = tz.[TimeZoneID]
 WHERE a.[AccountID] = 25056


 -- TABLE 0 TYPE REPORT TO RUN --
SELECT 'Account' AS 'Type_Of_Sensor_Report';

-- TABLE 1 BASIC INFORMATION NEEDED TO RUN REPORT --
SELECT @FromDate AS 'From'
 ,@ToDate AS 'To'
 ,@TimezoneIdentifier AS 'Time_Zone_StringID'
 ,@AccountID AS 'AccountID'
 ,53052 AS 'CSNetID'

-- Table 2 Column Names for Report --
SELECT 
  'True' AS 'DataMessageGUID'
 ,CONVERT(BIT, 1) AS 'Date'
 ,CONVERT(BIT, 1) AS 'GatewayID'
 ,'True' AS 'SensorID'
 ,CONVERT(BIT, 1) AS 'Sensor_Name'
 ,CONVERT(BIT, 1) AS 'Sensor_State'
 ,CONVERT(BIT, 1) AS 'Raw_Data'
 ,CONVERT(BIT, 1) AS 'Value'
 ,CONVERT(BIT, 1) AS 'Formatted_Value'
 ,CONVERT(BIT, 1) AS 'Alert_Sent'
 ,CONVERT(BIT, 1) AS 'Signal_Strength'
 ,CONVERT(BIT, 1) AS 'Voltage'
 ,CONVERT(BIT, 1) AS 'Battery'
 ,CONVERT(BIT, 1) AS 'Special';


 SELECT
 SensorID
 INTO #SensorList
 FROM Sensor
 WHERE AccountID = @AccountID
 AND SensorID IN (
 47688 ,
123671 ,
338219 ,
368655 ,
498332 ,
1003981,
314614 ,
175929 ,
10379  ,
10105  ,
201537 ,
473686 ,
337713 ,
106969 ,
358387 ,
23217  ,
351949 ,
310039 ,
11006  ,
21447  ,
179649 ,
446384 ,
377709 ,
418973 ,
484544 ,
25990  ,
450850 ,
206655 ,
161426 ,
38664  ,
450851 ,
19475  ,
75988  ,
34311  ,
420941 ,
175022 ,
51234  ,
475700 ,
308873 ,
412    ,
392116 ,
440    ,
31565  ,
80078  ,
375334 ,
345739 ,
19215  ,
385456 ,
457606 ,
45086  ,
201822 ,
24509  ,
409441 ,
87396  ,
45380  ,
29631  ,
504081 ,
495876 ,
154878 ,
92810  ,
261    ,
33858  ,
385848 ,
1004314,
439916,
454375,
103140,
51197 ,
432308,
409621,
410697,
62791 ,
495473,
196849,
432302,
82479 ,
131641,
446401,
316674,
63016 ,
301774,
332494,
146313,
71623 ,
363903,
79173 ,
414061,
145681,
406819,
85319 ,
491301,
462917,
493321,
308083,
324163,
362928,
441262,
442471,
353130,
364582,
459886,
463876,
496412,
491313,
366702,
500878,
439432,
439327,
469320,
445603,
446282,
491814,
479780,
498828,
471731,
492826,
497993)

 
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 47688   AND MessageDate BETWEEN '2019-07-08 10:34:18' and  '2019-07-08 16:34:18'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 123671   AND MessageDate BETWEEN '2019-07-08 10:36:20' and  '2019-07-08 16:36:20'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 338219   AND MessageDate BETWEEN '2019-07-08 10:36:20' and  '2019-07-08 16:36:20'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 368655   AND MessageDate BETWEEN '2019-07-08 10:36:20' and  '2019-07-08 16:36:20'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 498332   AND MessageDate BETWEEN '2019-07-08 10:36:20' and  '2019-07-08 16:36:20'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 1003981   AND MessageDate BETWEEN '2019-07-08 10:36:15' and  '2019-07-08 16:36:15'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 314614   AND MessageDate BETWEEN '2019-07-08 10:36:19' and  '2019-07-08 16:36:19'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 175929   AND MessageDate BETWEEN '2019-07-08 10:34:57' and  '2019-07-08 16:34:57'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201708  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 10379   AND MessageDate BETWEEN '2017-08-09 02:08:41' and  '2017-08-09 08:08:41'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201608  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 10105   AND MessageDate BETWEEN '2016-08-02 07:59:17' and  '2016-08-02 13:59:17'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 201537   AND MessageDate BETWEEN '2019-07-08 10:36:19' and  '2019-07-08 16:36:19'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 473686   AND MessageDate BETWEEN '2019-07-08 10:36:19' and  '2019-07-08 16:36:19'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 337713   AND MessageDate BETWEEN '2019-07-08 10:36:12' and  '2019-07-08 16:36:12'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 106969   AND MessageDate BETWEEN '2019-07-08 10:35:52' and  '2019-07-08 16:35:52'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 358387   AND MessageDate BETWEEN '2019-07-08 10:33:34' and  '2019-07-08 16:33:34'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 23217   AND MessageDate BETWEEN '2019-07-08 10:30:08' and  '2019-07-08 16:30:08'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 351949   AND MessageDate BETWEEN '2019-07-08 10:27:53' and  '2019-07-08 16:27:53'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 310039   AND MessageDate BETWEEN '2019-07-08 10:35:55' and  '2019-07-08 16:35:55'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 11006   AND MessageDate BETWEEN '2019-07-08 10:31:57' and  '2019-07-08 16:31:57'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 21447   AND MessageDate BETWEEN '2019-07-08 10:33:18' and  '2019-07-08 16:33:18'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 179649   AND MessageDate BETWEEN '2019-07-08 10:35:10' and  '2019-07-08 16:35:10'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 446384   AND MessageDate BETWEEN '2019-07-08 10:36:16' and  '2019-07-08 16:36:16'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 377709   AND MessageDate BETWEEN '2019-07-08 10:35:03' and  '2019-07-08 16:35:03'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 418973   AND MessageDate BETWEEN '2019-07-08 10:36:15' and  '2019-07-08 16:36:15'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 484544   AND MessageDate BETWEEN '2019-07-08 10:36:21' and  '2019-07-08 16:36:21'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 25990   AND MessageDate BETWEEN '2019-07-08 10:36:21' and  '2019-07-08 16:36:21'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 450850   AND MessageDate BETWEEN '2019-07-08 10:34:12' and  '2019-07-08 16:34:12'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201711  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 206655   AND MessageDate BETWEEN '2017-11-29 12:31:26' and  '2017-11-29 18:31:26'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 161426   AND MessageDate BETWEEN '2019-07-08 10:24:36' and  '2019-07-08 16:24:36'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 38664   AND MessageDate BETWEEN '2019-07-08 10:34:33' and  '2019-07-08 16:34:33'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 450851   AND MessageDate BETWEEN '2019-07-08 10:33:54' and  '2019-07-08 16:33:54'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 19475   AND MessageDate BETWEEN '2019-07-08 10:32:48' and  '2019-07-08 16:32:48'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 75988   AND MessageDate BETWEEN '2019-07-08 10:30:02' and  '2019-07-08 16:30:02'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 34311   AND MessageDate BETWEEN '2019-07-08 10:31:24' and  '2019-07-08 16:31:24'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 420941   AND MessageDate BETWEEN '2019-07-08 10:36:12' and  '2019-07-08 16:36:12'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 175022   AND MessageDate BETWEEN '2019-07-08 10:35:28' and  '2019-07-08 16:35:28'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 51234   AND MessageDate BETWEEN '2019-07-08 10:36:08' and  '2019-07-08 16:36:08'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 475700   AND MessageDate BETWEEN '2019-07-08 10:36:20' and  '2019-07-08 16:36:20'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 308873   AND MessageDate BETWEEN '2019-07-08 10:29:17' and  '2019-07-08 16:29:17'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201308  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 412   AND MessageDate BETWEEN '2013-08-13 11:11:32' and  '2013-08-13 17:11:32'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 392116   AND MessageDate BETWEEN '2019-07-08 09:09:03' and  '2019-07-08 15:09:03'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201307  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 440   AND MessageDate BETWEEN '2013-07-09 10:53:45' and  '2013-07-09 16:53:45'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 31565   AND MessageDate BETWEEN '2019-07-08 10:20:08' and  '2019-07-08 16:20:08'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 80078   AND MessageDate BETWEEN '2019-07-08 10:34:06' and  '2019-07-08 16:34:06'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 375334   AND MessageDate BETWEEN '2019-07-08 10:36:20' and  '2019-07-08 16:36:20'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 345739   AND MessageDate BETWEEN '2019-07-08 10:21:42' and  '2019-07-08 16:21:42'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 19215   AND MessageDate BETWEEN '2019-07-08 10:36:10' and  '2019-07-08 16:36:10'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 385456   AND MessageDate BETWEEN '2019-07-08 10:36:06' and  '2019-07-08 16:36:06'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 457606   AND MessageDate BETWEEN '2019-07-08 10:35:11' and  '2019-07-08 16:35:11'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 45086   AND MessageDate BETWEEN '2019-07-08 10:34:41' and  '2019-07-08 16:34:41'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 201822   AND MessageDate BETWEEN '2019-07-08 10:30:13' and  '2019-07-08 16:30:13'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201408  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 24509   AND MessageDate BETWEEN '2014-08-04 19:42:23' and  '2014-08-05 01:42:23'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 409441   AND MessageDate BETWEEN '2019-07-08 10:35:39' and  '2019-07-08 16:35:39'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 87396   AND MessageDate BETWEEN '2019-07-08 10:34:02' and  '2019-07-08 16:34:02'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 45380   AND MessageDate BETWEEN '2019-07-08 10:35:35' and  '2019-07-08 16:35:35'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 29631   AND MessageDate BETWEEN '2019-07-08 10:30:31' and  '2019-07-08 16:30:31'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201906  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 504081   AND MessageDate BETWEEN '2019-06-25 11:58:15' and  '2019-06-25 17:58:15'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 495876   AND MessageDate BETWEEN '2019-07-08 10:36:14' and  '2019-07-08 16:36:14'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 154878   AND MessageDate BETWEEN '2019-07-08 10:36:04' and  '2019-07-08 16:36:04'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201906  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 92810   AND MessageDate BETWEEN '2019-06-28 09:12:53' and  '2019-06-28 15:12:53'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201411  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 261   AND MessageDate BETWEEN '2014-11-03 11:05:11' and  '2014-11-03 17:05:11'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201711  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 33858   AND MessageDate BETWEEN '2017-11-21 22:55:21' and  '2017-11-22 04:55:21'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 385848   AND MessageDate BETWEEN '2019-07-08 10:36:19' and  '2019-07-08 16:36:19'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 1004314   AND MessageDate BETWEEN '2019-07-08 10:36:17' and  '2019-07-08 16:36:17'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 439916   AND MessageDate BETWEEN '2019-07-08 10:31:00' and  '2019-07-08 16:31:00'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 454375   AND MessageDate BETWEEN '2019-07-08 10:35:53' and  '2019-07-08 16:35:53'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2018.dbo.DataMessage_201811  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 103140   AND MessageDate BETWEEN '2018-11-28 14:20:33' and  '2018-11-28 20:20:33'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201708  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 51197   AND MessageDate BETWEEN '2017-08-21 13:36:40' and  '2017-08-21 19:36:40'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 432308   AND MessageDate BETWEEN '2019-07-08 10:35:25' and  '2019-07-08 16:35:25'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 409621   AND MessageDate BETWEEN '2019-07-08 10:36:15' and  '2019-07-08 16:36:15'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 410697   AND MessageDate BETWEEN '2019-07-08 10:35:37' and  '2019-07-08 16:35:37'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 62791   AND MessageDate BETWEEN '2019-07-08 10:35:37' and  '2019-07-08 16:35:37'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 495473   AND MessageDate BETWEEN '2019-07-08 10:36:18' and  '2019-07-08 16:36:18'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 196849   AND MessageDate BETWEEN '2019-07-08 10:36:16' and  '2019-07-08 16:36:16'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 432302   AND MessageDate BETWEEN '2019-07-08 10:35:19' and  '2019-07-08 16:35:19'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 82479   AND MessageDate BETWEEN '2019-07-08 10:35:06' and  '2019-07-08 16:35:06'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201706  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 131641   AND MessageDate BETWEEN '2017-06-27 22:27:47' and  '2017-06-28 04:27:47'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 446401   AND MessageDate BETWEEN '2019-07-08 10:36:13' and  '2019-07-08 16:36:13'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 316674   AND MessageDate BETWEEN '2019-07-08 10:35:15' and  '2019-07-08 16:35:15'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 63016   AND MessageDate BETWEEN '2019-07-08 10:33:49' and  '2019-07-08 16:33:49'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 301774   AND MessageDate BETWEEN '2019-07-08 10:36:06' and  '2019-07-08 16:36:06'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 332494   AND MessageDate BETWEEN '2019-07-08 10:35:10' and  '2019-07-08 16:35:10'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 146313   AND MessageDate BETWEEN '2019-07-08 10:36:00' and  '2019-07-08 16:36:00'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201506  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 71623   AND MessageDate BETWEEN '2015-06-09 17:11:49' and  '2015-06-09 23:11:49'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 363903   AND MessageDate BETWEEN '2019-07-08 10:36:19' and  '2019-07-08 16:36:19'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 79173   AND MessageDate BETWEEN '2019-07-08 10:36:12' and  '2019-07-08 16:36:12'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 414061   AND MessageDate BETWEEN '2019-07-08 10:36:18' and  '2019-07-08 16:36:18'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 145681   AND MessageDate BETWEEN '2019-07-08 10:35:32' and  '2019-07-08 16:35:32'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 406819   AND MessageDate BETWEEN '2019-07-08 10:36:04' and  '2019-07-08 16:36:04'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 85319   AND MessageDate BETWEEN '2019-07-08 10:36:17' and  '2019-07-08 16:36:17'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 491301   AND MessageDate BETWEEN '2019-07-08 10:36:13' and  '2019-07-08 16:36:13'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 462917   AND MessageDate BETWEEN '2019-07-08 10:35:18' and  '2019-07-08 16:35:18'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 493321   AND MessageDate BETWEEN '2019-07-08 10:34:55' and  '2019-07-08 16:34:55'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2018.dbo.DataMessage_201807  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 308083   AND MessageDate BETWEEN '2018-07-02 15:30:35' and  '2018-07-02 21:30:35'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages.dbo.DataMessage_201708  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 324163   AND MessageDate BETWEEN '2017-08-09 11:27:42' and  '2017-08-09 17:27:42'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 362928   AND MessageDate BETWEEN '2019-07-08 10:36:16' and  '2019-07-08 16:36:16'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 441262   AND MessageDate BETWEEN '2019-07-08 10:36:16' and  '2019-07-08 16:36:16'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 442471   AND MessageDate BETWEEN '2019-07-08 10:34:49' and  '2019-07-08 16:34:49'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 353130   AND MessageDate BETWEEN '2019-07-08 10:35:51' and  '2019-07-08 16:35:51'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2018.dbo.DataMessage_201807  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 364582   AND MessageDate BETWEEN '2018-07-01 07:43:38' and  '2018-07-01 13:43:38'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 459886   AND MessageDate BETWEEN '2019-07-08 10:35:47' and  '2019-07-08 16:35:47'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 463876   AND MessageDate BETWEEN '2019-07-08 10:32:14' and  '2019-07-08 16:32:14'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 496412   AND MessageDate BETWEEN '2019-07-08 10:35:43' and  '2019-07-08 16:35:43'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 491313   AND MessageDate BETWEEN '2019-07-08 10:36:14' and  '2019-07-08 16:36:14'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2018.dbo.DataMessage_201804  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 366702   AND MessageDate BETWEEN '2018-04-24 07:27:49' and  '2018-04-24 13:27:49'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 500878   AND MessageDate BETWEEN '2019-07-08 10:33:46' and  '2019-07-08 16:33:46'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 439432   AND MessageDate BETWEEN '2019-07-08 10:36:03' and  '2019-07-08 16:36:03'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 439327   AND MessageDate BETWEEN '2019-07-08 10:36:03' and  '2019-07-08 16:36:03'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 469320   AND MessageDate BETWEEN '2019-07-08 10:35:01' and  '2019-07-08 16:35:01'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 445603   AND MessageDate BETWEEN '2019-07-08 10:36:15' and  '2019-07-08 16:36:15'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 446282   AND MessageDate BETWEEN '2019-07-08 10:30:09' and  '2019-07-08 16:30:09'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 491814   AND MessageDate BETWEEN '2019-07-08 10:33:19' and  '2019-07-08 16:33:19'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 479780   AND MessageDate BETWEEN '2019-07-08 10:34:42' and  '2019-07-08 16:34:42'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 498828   AND MessageDate BETWEEN '2019-07-08 10:26:02' and  '2019-07-08 16:26:02'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 471731   AND MessageDate BETWEEN '2019-07-08 10:32:33' and  '2019-07-08 16:32:33'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 492826   AND MessageDate BETWEEN '2019-07-08 10:33:52' and  '2019-07-08 16:33:52'  
INSERT INTO #DMTemp SELECT TOP 2  d.*  FROM MonnitMessages2019.dbo.DataMessage_201907  d WITH (NOLOCK) INNER JOIN #SensorList s on d.SensorID = s.SensorID WHERE s.SensorID = 497993   AND MessageDate BETWEEN '2019-07-08 10:31:14' and  '2019-07-08 16:31:14'  

 
 --SET @OffsetMinutes = DATEDIFF(MINUTE,GETUTCDATE(),dbo.GetLocalTime(GETUTCDATE(),@TimezoneIdentifier));

--SELECT 
--    [MessageDate] = DATEADD(MINUTE, @offsetminutes, [MessageDate]),
--    [SensorID],
--    [DataMessageGUID],
--    [State],
--    [SignalStrength],
--    [LinkQuality],
--    [Battery],
--    [Data],
--    [Voltage],
--    [MeetsNotificationRequirement],
--    [InsertDate] = DATEADD(MINUTE, @offsetminutes, [InsertDate]),
--    [GatewayID],
--    [HasNote]
--FROM #DMtemp ORDER BY MessageDate


select  DataMessageGUID, 
    dm.SensorID,  
    dm.MessageDate 'MessageDate',
    State, 
    SignalStrength, 
    LinkQuality, 
    Battery, 
    Data, 
    Voltage, 
    MeetsNotificationRequirement, 
    InsertDate, 
    GatewayID, 
    dm.HasNote from #DMtemp dm
order by SensorID, MessageDate


drop table #DMtemp
GO


