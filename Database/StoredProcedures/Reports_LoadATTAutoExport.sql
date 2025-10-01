USE [iMonnit]
GO

/****** Object:  StoredProcedure [dbo].[Report_LoadATTAutoExport]    Script Date: 6/28/2024 4:08:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_LoadATTAutoExport] 
	@AccountID BIGINT
AS

	DECLARE @PreviousDataMessage DATETIME, 
		@MaxCount INT,
		@FirstDataMessage DATETIME,
		@LastDatMessage DATETIME,
		@Count INT,
    @SQL VARCHAR(2000),
    @_AccountID BIGINT = @AccountID



	SELECT @PreviousDataMessage = (SELECT TOP 1 LastMessageDate FROM ExportStatistics WHERE AccountID = @_AccountID ORDER BY ExportStatisticsID DESC)

  DECLARE @FromDate DATETIME
  DECLARE @ToDate DATETIME

  SET @FromDate = DATEADD(HOUR, -48, GETUTCDATE())
  SET @ToDate = DATEADD(HOUR, 100, GETUTCDATE())


	SELECT @MaxCount = CASE WHEN Floor(MAX(MessageCount) * 1.1) < 2500 THEN 2500 WHEN Floor(MAX(MessageCount) * 1.1) > 15000 THEN 15000 ELSE Floor(MAX(MessageCount) * 1.1) END
	FROM (SELECT TOP 5 *
		FROM ExportStatistics
    WHERE AccountID = @_AccountID
		ORDER BY ExportStatisticsID DESC) es
	--Temp Override
	--SELECT @MaxCount = 50000


  CREATE TABLE #InsertTable (
    SensorID BIGINT,
    MessageDate DATETIME,
    DataMessageGUID UNIQUEIDENTIFIER
  )

  CREATE TABLE #SensorList
  (
    SensorID BIGINT
  )

  INSERT INTO #SensorList 
  SELECT
  SensorID
  FROM Sensor s
  INNER JOIN Account a WITH (NOLOCK) on s.AccountID = a.AccountID
  WHERE a.AccountIDTree LIKE '%*'+CONVERT(VARCHAR(10), @_AccountID)+'*%'


  SET @SQL = 
  'SELECT TOP '++CONVERT(VARCHAR(30), 14000)+ '
    d.sensorid,
    d.messagedate,
    d.DataMessageGUID
  FROM #SensorList s
  INNER JOIN dbo.[DataMessage] d WITH (NOLOCK) on s.sensorid = d.SensorID
  where InsertDate >= '''+CONVERT(VARCHAR(30), @PreviousDataMessage, 120)+'''
    AND d.MessageDate >= '''+CONVERT(VARCHAR(30),@FromDate,120)+'''
    AND d.MessageDate <= '''+CONVERT(VARCHAR(30),@ToDate, 120)+'''
  ORDER BY InsertDate'

  INSERT INTO #InsertTable (SensorID, MessageDate, DataMessageGUID)
  EXEC (@SQL)
	
	CREATE TABLE #DataMessages
	(
		DataMessageGUID UNIQUEIDENTIFIER,
		SensorID bigint,
		MessageDate DateTime,
		State int,
		SignalStrength int,
		LinkQuality int,
		Battery int,
		Data varchar(2000),
		Voltage Decimal(5,4),
		MeetsNotificationRequirement bit,
		InsertDate datetime,
		GatewayID bigint,		
		AccountID bigint,
		MonnitApplicationID bigint,
		CSNetID bigint,
		SensorName nvarchar(255),
		ReportInterval decimal(18, 3),
		ActiveStateInterval decimal(18, 3),
		MinimumCommunicationFrequency int,
		LastCommunicationDate datetime,
		IsDirty bit,
		ChannelMask bigint,
		StandardMessageDelay int,
		TransmitIntervalLink int,
		TransmitIntervalTest int,
		TestTransmitCount int,
		MaximumNetworkHops int,
		RetryCount int,
		Recovery int,
		TimeOfDayActive varbinary(max),
		MeasurementsPerTransmission int,
		TransmitOffset int,
		Hysteresis bigint,
		MinimumThreshold bigint,
		MaximumThreshold bigint,
		Calibration1 bigint,
		Calibration2 bigint,
		Calibration3 bigint,
		Calibration4 bigint,
		EventDetectionType int,
		EventDetectionPeriod int,
		EventDetectionCount int,
		RearmTime int,
		BiStable int,
		PendingActionControlCommand bit,
		IsActive bit,
		FirmwareVersion varchar(255),
		RadioBand varchar(255),
		PowerSourceID bigint,
		IsSleeping bit,
		ExternalID varchar(255),
		IsDirtyProfile bit,
		LastDataMessageGUID varchar(255),
		IsDirtyGeneral2 bit,
		IsDirtyProfile2 bit,
		eWitType int,
		SensorTypeID bigint,
		ForceToBootloader bit,
		DataLog bit,
		TagString varchar(2000),
		IsDeleted bit,
		IsNewToNetwork bit,
		CreateDate datetime,
		SensorApplicationID bigint,
		CalibrationCertification varchar(255),
		SensorhoodConfigDirty bit,
		SensorhoodID bigint,
		SensorhoodTransmitions int,
		CalibrationCertificationValidUntil datetime,
		CalibrationFacilityID bigint,
		ParentID bigint,
		resumeNotificationDate datetime,
		ListenBeforeTalk bit,
		IsDirtyGeneral3 bit,
		ListenBeforeTalkValue int,
		Make varchar(255),
		Model varchar(255),
		SerialNumber varchar(255),
		Description varchar(255),
    HasNote BIT,
    Note	varchar	(2000),
    Location	varchar	(255),
    IsDirtyRFConfig1	bit	,
    LinkAcceptance	int	,
    TransmitPower	int	,
    ReceiveSensitivity	int	,
    SensorFirmwareID	bigint	,
    SendFirmwareUpdate	bit	,
    GenerationType	varchar	(255),
    StartDate	datetime	,
    CrystalStartTime	int	,
    DMExchangeDelayMultiple	int	,
    SHID1	bigint	,
    SHID2	bigint	,
    CryptRequired	int	,
    Pingtime	int	,
    FirmwareUpdateInProgress	bit	,
    BSNUrgentDelay	int	,
    UrgentRetries	int	,
    ReadGeneralConfig1	bit	,
    ReadGeneralConfig2	bit	,
    ReadGeneralConfig3	bit	,
    ReadProfileConfig1	bit	,
    ReadProfileConfig2	bit	,
    LastNormalDate	datetime	,
    FirstAwareDate	datetime	,
    LastAwareDate	datetime,
    ApplicationID INT,
	IsAuthenticated bit,
	TransmitPowerOptions int,
	TimeOfDayControl int,
	SKU varchar(75),
	SensorPrint varbinary(max),
	SensorPrintDirty bit,
	TimeOffset int
	)
	
	
	INSERT INTO #DataMessages (DataMessageGUID,
		SensorID,
		MessageDate,
		State,
		SignalStrength,
		LinkQuality,
		Battery,
		Data,
		Voltage,
		MeetsNotificationRequirement,
		InsertDate,
		GatewayID,
		AccountID,
		MonnitApplicationID,
		CSNetID,
		SensorName,
		ReportInterval,
		ActiveStateInterval,
		MinimumCommunicationFrequency,
		LastCommunicationDate,
		IsDirty,
		ChannelMask,
		StandardMessageDelay,
		TransmitIntervalLink,
		TransmitIntervalTest,
		TestTransmitCount,
		MaximumNetworkHops,
		RetryCount,
		Recovery,
		TimeOfDayActive,
		MeasurementsPerTransmission,
		TransmitOffset,
		Hysteresis,
		MinimumThreshold,
		MaximumThreshold,
		Calibration1,
		Calibration2,
		Calibration3,
		Calibration4,
		EventDetectionType,
		EventDetectionPeriod,
		EventDetectionCount,
		RearmTime,
		BiStable,
		PendingActionControlCommand,
		IsActive,
		FirmwareVersion,
		RadioBand,
		PowerSourceID,
		IsSleeping,
		ExternalID,
		IsDirtyProfile,
		LastDataMessageGUID,
		IsDirtyGeneral2,
		IsDirtyProfile2,
		eWitType,
		SensorTypeID,
		ForceToBootloader,
		DataLog,
		TagString,
		IsDeleted,
		IsNewToNetwork,
		CreateDate,
		SensorApplicationID,
		CalibrationCertification,
		SensorhoodConfigDirty,
		SensorhoodID,
		SensorhoodTransmitions,
		CalibrationCertificationValidUntil,
		CalibrationFacilityID,
		ParentID,
		resumeNotificationDate,
		ListenBeforeTalk,
		IsDirtyGeneral3,
		ListenBeforeTalkValue,
		Make,
		Model,
		SerialNumber,
		Description,
    HasNote,
    Note	,
    Location	,
    IsDirtyRFConfig1	,
    LinkAcceptance	,
    TransmitPower	,
    ReceiveSensitivity	,
    SensorFirmwareID	,
    SendFirmwareUpdate	,
    GenerationType	,
    StartDate	,
    CrystalStartTime	,
    DMExchangeDelayMultiple	,
    SHID1	,
    SHID2	,
    CryptRequired	,
    Pingtime	,
    FirmwareUpdateInProgress	,
    BSNUrgentDelay	,
    UrgentRetries	,
    ReadGeneralConfig1	,
    ReadGeneralConfig2	,
    ReadGeneralConfig3	,
    ReadProfileConfig1	,
    ReadProfileConfig2	,
    LastNormalDate	,
    FirstAwareDate	,
    LastAwareDate,
    ApplicationID,
	IsAuthenticated,
	TransmitPowerOptions,
	TimeOfDayControl,
	SKU,
	SensorPrint,
	SensorPrintDirty,
	TimeOffset)
		SELECT TOP (14000)
			dm.DataMessageGUID,
			dm.SensorID,
			dm.MessageDate,
			dm.State,
			dm.SignalStrength,
			dm.LinkQuality,
			dm.Battery,
			dm.Data,
			dm.Voltage,
			dm.MeetsNotificationRequirement,
			dm.InsertDate,
			dm.GatewayID,
			s.AccountID,
			MonnitApplicationID = s.ApplicationID,
			s.CSNetID,
			s.SensorName,
			s.ReportInterval,
			s.ActiveStateInterval,
			s.MinimumCommunicationFrequency,
			s.LastCommunicationDate,
			s.IsDirty,
			s.ChannelMask,
			s.StandardMessageDelay,
			s.TransmitIntervalLink,
			s.TransmitIntervalTest,
			s.TestTransmitCount,
			s.MaximumNetworkHops,
			s.RetryCount,
			s.Recovery,
			s.TimeOfDayActive,
			s.MeasurementsPerTransmission,
			s.TransmitOffset,
			s.Hysteresis,
			s.MinimumThreshold,
			s.MaximumThreshold,
			s.Calibration1,
			s.Calibration2,
			s.Calibration3,
			s.Calibration4,
			s.EventDetectionType,
			s.EventDetectionPeriod,
			s.EventDetectionCount,
			s.RearmTime,
			s.BiStable,
			s.PendingActionControlCommand,
			s.IsActive,
			s.FirmwareVersion,
			s.RadioBand,
			s.PowerSourceID,
			s.IsSleeping,
			s.ExternalID,
			s.IsDirtyProfile,
			s.LastDataMessageGUID,
			s.IsDirtyGeneral2,
			s.IsDirtyProfile2,
			s.eWitType,
			s.SensorTypeID,
			s.ForceToBootloader,
			s.DataLog,
			s.TagString,
			s.IsDeleted,
			s.IsNewToNetwork,
			s.CreateDate,
			s.SensorApplicationID,
			s.CalibrationCertification,
			s.SensorhoodConfigDirty,
			s.SensorhoodID,
			s.SensorhoodTransmitions,
			s.CalibrationCertificationValidUntil,
			s.CalibrationFacilityID,
			s.ParentID,
			s.resumeNotificationDate,
			s.ListenBeforeTalk,
			s.IsDirtyGeneral3,
			s.ListenBeforeTalkValue,
			s.Make,
			s.Model,
			s.SerialNumber,
			s.Description,
      dm.HasNote,
      s.Note,
      s.Location,
      s.IsDirtyRFConfig1,
      s.LinkAcceptance,
      s.TransmitPower,
      s.ReceiveSensitivity,
      s.SensorFirmwareID,
      s.SendFirmwareUpdate,
      s.GenerationType,
      s.StartDate,
      s.CrystalStartTime,
      s.DMExchangeDelayMultiple,
      s.SHID1,
      s.SHID2,
      s.CryptRequired,
      s.Pingtime,
      s.FirmwareUpdateInProgress,
      s.BSNUrgentDelay,
      s.UrgentRetries,
      s.ReadGeneralConfig1,
      s.ReadGeneralConfig2,
      s.ReadGeneralConfig3,
      s.ReadProfileConfig1,
      s.ReadProfileConfig2,
      s.LastNormalDate,
      s.FirstAwareDate,
      s.LastAwareDate,
      s.ApplicationID,
      dm.IsAuthenticated,
	  s.TransmitPowerOptions,
	  s.TimeOfDayControl,
	  s.SKU,
	  s.SensorPrint,
	  s.SensorPrintDirty,
	  s.timeoffset
		FROM DataMessage dm WITH (NOLOCK)
    INNER JOIN #InsertTable t ON dm.Messagedate = t.MessageDate AND dm.SensorID = t.SensorID AND dm.DataMessageGUID = t.DataMessageGUID
		INNER JOIN Sensor s WITH (NOLOCK) ON s.SensorID = dm.SensorID
		WHERE dm.MessageDate >= @FromDate
      AND dm.MessageDate <= @ToDate
    OPTION (Optimize FOR UNKNOWN);

	SELECT * from #DataMessages order by InsertDate
	
	SELECT DISTINCT sa.*
	FROM SensorAttribute sa WITH (NOLOCK)
	INNER JOIN #SensorList dm ON dm.SensorID = sa.SensorID

	SELECT @FirstDataMessage = ISNULL(MIN(InsertDate), @PreviousDataMessage), @LastDatMessage = ISNULL(MAX(InsertDate), @PreviousDataMessage), @Count = ISNULL(COUNT(MessageDate),0)
	FROM #DataMessages

	INSERT INTO ExportStatistics (ExportDate,FirstMessageDate, LastMessageDate,MessageCount, AccountID)
		VALUES (GETUTCDATE(),@FirstDataMessage,@LastDatMessage, @Count, @_AccountID)
	
	SELECT * FROM ExportStatistics WITH (NOLOCK) WHERE ExportStatisticsID = @@IDENTITY
	
	SELECT @PreviousDataMessage


GO


