USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_AuthorityDataTempWater_OnDemand]    Script Date: 6/24/2024 2:38:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_AuthorityDataTempWater_OnDemand]
  @Default_OwnerAccountID BIGINT,
  @FromDate DATETIME,
  @ToDate DATETIME
AS

DECLARE @SQL VARCHAR(5000)
DECLARE @TimeZoneIDString VARCHAR(100)

IF DATEDIFF(HOUR, @FromDate, @ToDate) <= 180
BEGIN

    CREATE TABLE #Results
    ( TimeZoneIDString              VARCHAR(300),
      AccountNumber                 VARCHAR(500),
      SensorID                      BIGINT,
      Network                       VARCHAR(500),
      ApplicationID           INT,
      GenerationType                VARCHAR(100),
      SensorType                    INT,
      ApplicationName               VARCHAR(100),
      GatewayID                     BIGINT,
      MessageDate                   DATETIME,
      Data                          VARCHAR(100),
      Battery                       INT,
      Voltage                       DECIMAL(18,4),
      SignalStrength                INT,
      MeetsNotificationRequirement  BIT,
      SignalStrength2               DECIMAL(18,4)
    )

    --SET @ToDate = GETUTCDATE()
    --SET @FromDate = DATEADD(Week, -1, @ToDate)

    SELECT
      @TimeZoneIDString = t.TimeZoneIDString
    FROM Account a
    INNER JOIN TimeZOne t on a.TimeZoneID = t.TimeZoneID
    WHERE a.AccountID = @Default_OwnerAccountID

    SET @ToDate = DATEADD(DAY, 1, @ToDate)

    --SET @FromDate = DATEADD(HOUR, DATEDIFF(HOUR, GETUTCDATE(), dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString)) ,@FromDate)
    --SET @ToDate = DATEADD(HOUR, DATEDIFF(HOUR, GETUTCDATE(), dbo.GetLocalTime(GETUTCDATE(), @TimeZoneIDString)) ,@ToDate)

    --select @fromdate, @todate

    SELECT TOP 500
      TimeZoneIDString,
      AccountNumber, 
      SensorID,
      Network = c.Name, 
      s.ApplicationID, 
      GenerationType, 
      SensorTypeID, 
      ApplicationName
    INTO #Temp1
    FROM dbo.Sensor s with (NOLOCK)
    INNER JOIN dbo.Account a WITH (NOLOCK) on a.AccountID = s.AccountID
    INNER JOIN dbo.CSNet c WITH (NOLOCK) on s.CSNetID = c.CSNetID
    INNER JOIN dbo.Application m WITH (NOLOCK) on s.ApplicationID = m.ApplicationID
    INNER JOIN dbo.TimeZone t WITH (NOLOCK) on t.TimeZoneID = a.TimeZoneID
    WHERE a.AccountIDTree like '%*'+CONVERT(VARCHAR(30), @Default_OwnerAccountID)+'*%'
      AND LastCommunicationDate < '2099-01-01'
      AND s.ApplicationID IN (2,4,35,46, 65)
    ORDER BY LastCommunicationDate DESC

    SET @SQL = 
    '
    SELECT
      t.*, 
      d.GatewayID, 
      d.messagedate, 
      d.Data, 
      d.Battery, 
      d.Voltage, 
      d.SignalStrength, 
      d.MeetsNotificationRequirement, 
      null
    FROM dbo.[DataMessage] d WITH (NOLOCK)
    INNER JOIN #Temp1 t on d.SensorID = t.SensorID
    AND d.MessageDate >= DATEADD(HOUR, -1 * DATEDIFF(Hour, ''' + CONVERT(VARCHAR(30), @FromDate,120) + ''', dbo.GetLocalTime(''' + CONVERT(VARCHAR(30), @FromDate,120) + ''', t.TimeZoneIDSTring)), CONVERT(VARCHAR(10), ''' + CONVERT(VARCHAR(30), @FromDate,120) + ''', 120) + '' 00:00'')
      AND d.MessageDate <DATEADD(HOUR, -1 * DATEDIFF(Hour, ''' + CONVERT(VARCHAR(30), @ToDate,120) + ''', dbo.GetLocalTime(''' + CONVERT(VARCHAR(30), @ToDate,120) + ''', t.TimeZoneIDSTring)), CONVERT(VARCHAR(10), ''' + CONVERT(VARCHAR(30), @ToDate,120) + ''', 120) + '' 00:00'')
    '

    INSERT INTO #Results
    EXEC(@SQL);


    UPDATE #Results
    set SignalStrength2 = (SignalStrength + 90) + 60
    where GenerationType = 'GEN2' AND SignalStrength > -90

    UPDATE #Results
    Set SignalStrength2 = ROUND((SignalStrength + 115) * 2.4, 0)
    where  GenerationType = 'GEN2' AND SignalStrength <= -90


    UPDATE #Results
    SET SignalStrength2 = ((SignalStrength+70)*2)+60
    where SensorType = 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength > -70

    UPDATE #Results
    SET SignalStrength2 = ROUND((SignalStrength+84) * 4.2857, 0)
    where  SensorType = 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength <= -70

    UPDATE #Results
    SET SignalStrength2 = ((SignalStrength+80) * 4.0 / 3.0) + 60
    where SensorType != 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength > -80

    UPDATE #Results
    SET SignalStrength2 = (SignalStrength+100)*3
    where  SensorType != 4 AND ISNULL(GenerationType, 'GEN1') != 'Gen2' AND SignalStrength <= -80

    UPDATE #Results
    SET SignalStrength2 = 100
    where SignalStrength2 > 100
     
    select 
      AccountNumber,
      Network,
      GatewayID,
      SensorID,
      SensorType      = ApplicationName,
      DateTime        = iMonnit.dbo.GetLocalTime(MessageDate, TimeZoneIDString),
      TempValue       = CASE WHEN ApplicationID = 2 AND convert(decimal(18,8), data) > -997.0 THEN CONVERT(VARCHAR(300), CONVERT(DECIMAL(18,1), ROUND((CONVERT(DECIMAL(18,1), Data) * (9.0/5.0)) + 32, 1))) ELSE 
                          CASE WHEN  ApplicationID = 2 AND convert(decimal(18,1), data) < -997.0 THEN 'Invalid Reading' ELSE NULL END END, 
      WaterSensed     = CASE WHEN ApplicationID = 4 THEN Data ELSE NULL END,
      Alert_Sent      = MeetsNotificationRequirement,
      SignalStrength  = SignalStrength2,
      Battery, 
      Voltage
    from #Results
    ORDER BY AccountNumber, Network, SensorID, MessageDate

    drop table #Results
    DROP TABLE #Temp1

END ELSE
BEGIN

    SELECT Message = 'Date Range Greater Than 1 Week'

END

GO


