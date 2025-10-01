// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Sensor
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class Sensor
{
  [DBMethod("Sensor_GetTimeZoneIDBySensorID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  acct.TimeZoneID\r\nFROM dbo.[Sensor] sens\r\nINNER JOIN dbo.[CSNet] cs ON cs.CSNetID = sens.CSNetID\r\nINNER JOIN dbo.[Account] acct ON acct.AccountID = cs.AccountID\r\nWHERE sens.SensorID = @SensorID;\r\n")]
  [DBMethodBody(DBMS.SQLite, "Select acct.TimeZoneID from Sensor sens\r\n                                         inner join CSNet cs on cs.CSNetID = sens.CSNetID\r\n                                         inner join Account acct on acct.AccountID = cs.AccountID\r\n                                         where sens.SensorID = @SensorID")]
  internal class GetTimeZoneIDBySensorID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public long Result { get; private set; }

    public GetTimeZoneIDBySensorID(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = this.ToScalarValue<long>();
    }
  }

  [DBMethod("Sensor_NextSensorID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT MAX(SensorID) + 1 FROM dbo.[Sensor];\r\n")]
  internal class NextSensorID : BaseDBMethod
  {
    public long Result { get; private set; }

    public NextSensorID() => this.Result = this.ToScalarValue<long>();
  }

  [DBMethod("Sensor_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Sensor] WITH (NOLOCK)\r\nWHERE AccountID  = @AccountID\r\n  AND (IsDeleted = 0 OR IsDeleted IS NULL);\r\n")]
  internal class LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.Sensor> Result { get; private set; }

    public LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.Sensor>(this.ToDataTable());
      Monnit.Sensor.EnablePropertyChangedEvent(this.Result);
    }
  }

  [DBMethod("Sensor_LoadByCSNetID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Sensor] WITH(NOLOCK)\r\nWHERE CSNetID = @CSNetID\r\n  AND (IsDeleted = 0 OR IsDeleted IS NULL)\r\nORDER BY SensorID;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM Sensor WHERE CSNetID = @CSNetID AND (IsDeleted = 0 OR IsDeleted is null) ORDER BY SensorID")]
  internal class LoadByCSNetID : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public DataTable Result { get; private set; }

    public LoadByCSNetID(long cSNetID)
    {
      this.CSNetID = cSNetID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Sensor_LoadByCalibrationFacilityID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Sensor]\r\nWHERE CalibrationFacilityID = @CalibrationFacilityID;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM Sensor WHERE CalibrationFacilityID = @CalibrationFacilityID")]
  internal class LoadByCalibrationFacilityID : BaseDBMethod
  {
    [DBMethodParam("CalibrationFacilityID", typeof (long))]
    public long CalibrationFacilityID { get; private set; }

    public DataTable Result { get; private set; }

    public LoadByCalibrationFacilityID(long calibrationFacilityID)
    {
      this.CalibrationFacilityID = calibrationFacilityID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Sensor_LoadByApplicationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM (SELECT\r\n        rownumber = ROW_NUMBER() OVER (ORDER BY LastCommunicationDate DESC),\r\n        *\r\n      FROM dbo.[Sensor] WITH (NOLOCK)\r\n      where ApplicationID = @ApplicationID\r\n        AND LastCommunicationDate < '2098-01-01'\r\n      ) AS temp\r\nWHERE rownumber <= @Limit;\r\n")]
  internal class LoadByApplicationID : BaseDBMethod
  {
    [DBMethodParam("ApplicationID", typeof (long))]
    public long ApplicationID { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.Sensor> Result { get; private set; }

    public LoadByApplicationID(long applicationID, int limit)
    {
      this.ApplicationID = applicationID;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.Sensor>(this.ToDataTable());
    }
  }

  [DBMethod("Sensor_LoadByAccountIDAndApplicationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Sensor] WITH (NOLOCK)\r\nWHERE ApplicationID = @ApplicationID\r\n  AND AccountID = @AccountID\r\nORDER BY SensorName ASC;\r\n")]
  internal class LoadByAccountIDAndApplicationID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("ApplicationID", typeof (long))]
    public long ApplicationID { get; private set; }

    public List<Monnit.Sensor> Result { get; private set; }

    public LoadByAccountIDAndApplicationID(long accountID, long applicationID)
    {
      this.AccountID = accountID;
      this.ApplicationID = applicationID;
      this.Result = BaseDBObject.Load<Monnit.Sensor>(this.ToDataTable());
    }
  }

  [DBMethod("Sensor_LoadByAccountIDAndApplicationIDIncludeAllSubAccounts")]
  [DBMethodBody(DBMS.SqlServer, "\r\n;WITH\r\n    cteAccounts\r\n    AS\r\n    (\r\n\t\tSELECT *, Level = 1\r\n\t\tFROM Account\r\n\t\tWHERE AccountID = @AccountID\r\n\r\n\t\tUNION ALL\r\n\r\n\t\tSELECT a.*, Level = Level+1\r\n\t\tFROM Account a\r\n\t\tINNER JOIN cteAccounts ctea ON ctea.AccountID = a.RetailAccountID\r\n    )\r\n\r\nSELECT s.* \r\nFROM cteAccounts a\r\nLEFT JOIN Sensor s\r\nON a.AccountID = s.AccountID\r\nWHERE s.ApplicationID = @ApplicationID\r\n  AND LastCommunicationDate < '2099-01-01'\r\n")]
  internal class LoadByAccountIDAndApplicationIDIncludeAllSubAccounts : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("ApplicationID", typeof (long))]
    public long ApplicationID { get; private set; }

    public List<Monnit.Sensor> Result { get; private set; }

    public LoadByAccountIDAndApplicationIDIncludeAllSubAccounts(long accountID, long applicationID)
    {
      this.AccountID = accountID;
      this.ApplicationID = applicationID;
      this.Result = BaseDBObject.Load<Monnit.Sensor>(this.ToDataTable());
    }
  }

  [DBMethod("Sensor_LoadDirtyByCsNetID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Sensor]\r\nWHERE (IsDeleted  = 0 OR IsDeleted IS NULL)\r\n  AND CSNetID     = @CSNetID \r\n  AND (IsDirty                      = 1 \r\n    OR IsDirtyProfile               = 1 \r\n    OR IsDirtyGeneral2              = 1\r\n    OR IsDirtyProfile2              = 1 \r\n    OR PendingActionControlCommand  = 1);\r\n")]
  internal class LoadDirtyByCsNetID : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public DataTable Result { get; private set; }

    public LoadDirtyByCsNetID(long cSNetID)
    {
      this.CSNetID = cSNetID;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Sensor_LoadByCSNetIDAndApplicationID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Sensor] WITH (NOLOCK)\r\nWHERE CSNetID       = @CSNetID\r\n  AND ApplicationID = @ApplicationID\r\nORDER BY SensorName ASC;   \r\n")]
  internal class LoadByCSNetIDAndApplicationID : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    [DBMethodParam("ApplicationID", typeof (long))]
    public long ApplicationID { get; private set; }

    public List<Monnit.Sensor> Result { get; private set; }

    public LoadByCSNetIDAndApplicationID(long csNetID, long applicationID)
    {
      this.CSNetID = csNetID;
      this.ApplicationID = applicationID;
      this.Result = BaseDBObject.Load<Monnit.Sensor>(this.ToDataTable());
    }
  }

  [DBMethod("Sensor_ForceInsert")]
  [DBMethodBody(DBMS.SqlServer, "\r\nINSERT INTO dbo.[Sensor] (SensorID,AccountID, ApplicationID,CSNetID,SensorName,ReportInterval,ActiveStateInterval,MinimumCommunicationFrequency,LastCommunicationDate,IsCableEnabled)\r\nVALUES (@SensorID,@AccountID,@ApplicationID,@CSNetID,@SensorName,@ReportInterval,@ActiveStateInterval,@MinimumCommunicationFrequency,@LastCommunicationDate,@IsCableEnabled);\r\n")]
  [DBMethodBody(DBMS.SQLite, "INSERT INTO Sensor (SensorID, AccountID, ApplicationID, CSNetID, SensorName, ReportInterval, ActiveStateInterval, MinimumCommunicationFrequency, LastCommunicationDate, IsCableEnabled) \r\n\t                                    values (@SensorID,@AccountID,@ApplicationID,@CSNetID,'@SensorName',@ReportInterval,@ActiveStateInterval,@MinimumCommunicationFrequency,'@LastCommunicationDate',@IsCableEnabled)")]
  internal class ForceInsert : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("ApplicationID", typeof (long))]
    public long MonnitApplicationID { get; private set; }

    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    [DBMethodParam("SensorName", typeof (string))]
    public string SensorName { get; private set; }

    [DBMethodParam("ReportInterval", typeof (double))]
    public double ReportInterval { get; private set; }

    [DBMethodParam("ActiveStateInterval", typeof (int))]
    public double ActiveStateInterval { get; private set; }

    [DBMethodParam("MinimumCommunicationFrequency", typeof (int))]
    public int MinimumCommunicationFrequency { get; private set; }

    [DBMethodParam("LastCommunicationDate", typeof (DateTime))]
    public DateTime LastCommunicationDate { get; private set; }

    [DBMethodParam("IsCableEnabled", typeof (bool))]
    public bool IsCableEnabled { get; private set; }

    public ForceInsert(Monnit.Sensor sensor)
    {
      this.SensorID = sensor.SensorID;
      this.AccountID = sensor.AccountID;
      this.MonnitApplicationID = sensor.ApplicationID;
      this.CSNetID = sensor.CSNetID;
      this.SensorName = sensor.SensorName;
      this.ReportInterval = sensor.ReportInterval;
      this.ActiveStateInterval = sensor.ActiveStateInterval;
      this.MinimumCommunicationFrequency = sensor.MinimumCommunicationFrequency;
      this.LastCommunicationDate = sensor.LastCommunicationDate;
      this.IsCableEnabled = sensor.IsCableEnabled;
      this.Execute();
      sensor.Save();
    }
  }

  [DBMethod("Sensor_ClearLastCommunicationDate")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE dbo.[Sensor]\r\nSET LastDataMessageGUID   = NULL,\r\n    LastCommunicationDate = '2099-01-01',\r\n    IsSleeping            = 1\r\nWHERE SensorID = @SensorID;\r\n")]
  internal class ClearLastCommunicationDate : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public ClearLastCommunicationDate(long sensorID)
    {
      this.SensorID = sensorID;
      this.Execute();
    }
  }

  [DBMethod("Sensor_ClearHistory")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSET NOCOUNT ON;\r\n\r\nUPDATE dbo.[Sensor]\r\nSET LastDataMessageID = NULL,\r\n    StartDate = GETUTCDATE()\r\nWHERE SensorID = @SensorID;\r\n\r\n/*\r\nCommenting out for the momemnt: Was taking 30 seconds for a loop which was causing 30 second blocking for DataMMessage inserts\r\nsolution: change sensor's startdate\r\n*/\r\n--SET ROWCOUNT 3500;\r\n\r\n--WHILE 1=1\r\n--BEGIN\r\n    \r\n--    WAITFOR DELAY '00:00:01' --Allow one second between each loop to allow other processes access to the table\r\n\r\n--    DELETE FROM dbo.DataMessage\r\n--    WHERE SensorID = @SensorID\r\n    \r\n--    IF @@ROWCOUNT = 0\r\n--    BREAK\r\n\r\n--END;\r\nSET NOCOUNT OFF;")]
  internal class ClearHistory : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public ClearHistory(long sensorID)
    {
      this.SensorID = sensorID;
      this.Execute();
    }
  }

  [DBMethod("Sensor_SensorOverView")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  sg.[SensorGroupID],\r\n  [SensorGroupName]     = sg.[Name],\r\n  [sens].*,\r\n  dm.[MessageDate],\r\n  [dmSensorID]          = dm.[SensorID],\r\n  dm.[DataMessageGUID],\r\n  dm.[InsertDate],\r\n  dm.[State],\r\n  dm.[SignalStrength],\r\n  dm.[LinkQuality],\r\n  dm.[Battery],\r\n  dm.[Data],\r\n  dm.[Voltage],\r\n  dm.[MeetsNotificationRequirement],\r\n  dm.[GatewayID],\r\n  dm.[HasNote],\r\n  dm.[IsAuthenticated],\r\n  dm.[ApplicationID]\r\nFROM dbo.[Sensor] sens                WITH(NOLOCK) \r\nLEFT JOIN dbo.[SensorGroupSensor] sgs WITH(NOLOCK) ON sgs.[SensorID] = sens.[SensorID]\r\nLEFT JOIN dbo.[SensorGroup] sg        WITH(NOLOCK) ON sg.[SensorGroupID] = sgs.[SensorGroupID]\r\nLEFT JOIN dbo.[DataMessage_Last] dm        WITH(NOLOCK) ON dm.[SensorID] = sens.[SensorID] AND dm.[MessageDate] = sens.[LastCommunicationDate] AND dm.[DataMessageGUID] = sens.[LastDataMessageGUID]\r\nWHERE sens.[AccountID] = @AccountID\r\n  AND sens.[CSNetID] = ISNULL(@CSNetID, sens.[CSNetID]);\r\n")]
  internal class SensorOverView : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public DataTable Result { get; private set; }

    public SensorOverView(long accountID, long? csNetID)
    {
      this.AccountID = accountID;
      this.CSNetID = csNetID ?? long.MinValue;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Sensor_totalCount")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  COUNT(*)\r\nFROM dbo.[Sensor];\r\n")]
  internal class totalCount : BaseDBMethod
  {
    public int Result { get; private set; }

    public totalCount() => this.Result = this.ToScalarValue<int>();
  }

  [DBMethod("Sensor_UpdateByCableID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @OldCableID BIGINT\r\n\r\n/*\r\n  Type 1 = Plug in\r\n  Type 2 = Plug out\r\n  Type 3 = Auto remove old and insert new\r\n*/\r\n\r\nIF @Type = 1\r\nBEGIN\r\n\r\n  UPDATE s\r\n    Set CableID = @CableID\r\n  FROM dbo.[Sensor] s\r\n  WHERE SensorID = @SensorID;\r\n\r\n  INSERT INTO CableAudit\r\n  VALUES (@CableID, @SensorID, GETUTCDATE(), 1);\r\n\r\nEND ELSE \r\nIF @Type = 2\r\nBEGIN\r\n\r\n  UPDATE s\r\n    Set CableID = NULL\r\n  FROM dbo.[Sensor] s\r\n  WHERE SensorID = @SensorID;\r\n\r\n  INSERT INTO CableAudit\r\n  VALUES (@CableID, @SensorID, GETUTCDATE(), 2)\r\n\r\nEND ELSE \r\nIF @Type = 3\r\nBEGIN\r\n\r\n  SET @OldCableID = (SELECT CableID FROM Sensor where SensorID = @SensorID);\r\n\r\n  UPDATE s\r\n    Set CableID = @CableID\r\n  FROM dbo.[Sensor] s\r\n  WHERE SensorID = @SensorID;\r\n\r\n\r\n  INSERT INTO CableAudit\r\n  VALUES (@OldCableID, @SensorID, GETUTCDATE(), 3);\r\n\r\n  INSERT INTO CableAudit\r\n  VALUES (@CableID, @SensorID, GETUTCDATE(), 1);\r\n\r\nEND\r\n")]
  internal class UpdateByCableID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    [DBMethodParam("CableID", typeof (long))]
    public long CableID { get; private set; }

    [DBMethodParam("Type", typeof (int))]
    public int Type { get; private set; }

    public DataTable Result { get; private set; }

    public UpdateByCableID(long sensorID, long cableID, int type)
    {
      this.SensorID = sensorID;
      this.CableID = cableID;
      this.Type = type;
      this.Result = this.ToDataTable();
    }
  }

  [DBMethod("Sensor_DeleteAncillaryObjects")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE FROM [dbo].[SensorNotification] WHERE SensorID = @SensorID\r\nUPDATE [dbo].[Notification] SET SensorID = null WHERE SensorID = @SensorID\r\n\r\nDELETE FROM [dbo].[CustomerFavorite] WHERE SensorID = @SensorID\r\nDELETE FROM [dbo].[NotifierToSensorData] WHERE [SensorID ] = @SensorID\r\nDELETE FROM [dbo].[OTARequestSensor] WHERE SensorID = @SensorID\r\nDELETE FROM [dbo].[SensorGroupSensor] WHERE SensorID = @SensorID\r\nDELETE FROM [dbo].[VisualMapSensor] WHERE SensorID = @SensorID\r\n\r\n/*-\r\n--Skip--\r\nDataMessage\r\nDataMessageNote\r\nGateway\r\nSensorAttribute\r\nSensorData\r\nSensorMessageAudit\r\nAcknowledgementRecorded\r\nCable\r\nCableAudit\r\nCalibrationCertificate\r\nCreditLog\r\nEquipmentSensor\r\nExternalDataSubscription\r\nExternalDataSubscriptionAttempt\r\nNetworkAudit\r\nNonCachedAttribute\r\nPreAggregation\r\nProgrammerAudit\r\nSensorFile -- not used\r\n*/\r\n")]
  internal class DeleteAncillaryObjects : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public DeleteAncillaryObjects(long sensorID)
    {
      this.SensorID = sensorID;
      this.Execute();
    }
  }
}
