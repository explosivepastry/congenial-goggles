// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Gateway
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class Gateway
{
  [DBMethod("Gateway_ForceInsert")]
  [DBMethodBody(DBMS.SqlServer, "\r\nINSERT INTO dbo.[Gateway] (GatewayID, CSNetID, Name, GatewayTypeID)\r\nVALUES (@GatewayID,@CSNetID,@Name,@GatewayTypeID);\r\n\r\nINSERT INTO dbo.[GatewayStatus] (GatewayID) VALUES (@GatewayID);\r\n")]
  [DBMethodBody(DBMS.SQLite, "INSERT INTO Gateway (GatewayID, CSNetID, Name, GatewayTypeID) values (@GatewayID,@CSNetID,'@Name',@GatewayTypeID)")]
  internal class ForceInsert : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    [DBMethodParam("Name", typeof (string))]
    public string Name { get; private set; }

    [DBMethodParam("GatewayTypeID", typeof (long))]
    public long GatewayTypeID { get; private set; }

    public ForceInsert(Monnit.Gateway gateway)
    {
      this.GatewayID = gateway.GatewayID;
      this.CSNetID = gateway.CSNetID;
      this.Name = gateway.Name;
      this.GatewayTypeID = gateway.GatewayTypeID;
      this.Execute();
      gateway.PrepFullSave();
      gateway.Save();
    }
  }

  [DBMethod("Gateway_LoadByID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  g.GatewayID, g.CSNetID, g.Name, g.GatewayTypeID, g.RadioBand, g.APNFirmwareVersion, g.GatewayFirmwareVersion, g.ReportInterval, g.ServerHostAddress\r\n  , g.Port, g.ChannelMask, g.NetworkIDFilter, g.GatewayIP, g.GatewaySubnet, g.DefaultRouterIP, g.GatewayDNS, g.SecondaryDNS, g.IsDirty\r\n  , g.SendResetNetworkRequest, g.ObserveAware, g.NetworkListInterval, g.ForceToBootloader, g.CellAPNName, g.Username, g.Password, g.MacAddress, g.SensorID\r\n  , g.ErrorHeartbeat, g.WIFISecurityMode, g.WiFiNetworkCount, g.TransmitRate1, g.TransmitRate2, g.TransmitRate3, g.TransmitRateCTS, g.TransmitPower\r\n  , g.LedActiveTime, g.LastKnownChannel, g.LastKnownNetworkID, g.RetreiveNetworkInfo, g.CurrentSignalStrength, g.IsDeleted, g.IgnoreData, g.ServerHostAddress2\r\n  , g.Port2, g.LastKnownDeviceCount, g.CellNetworkType, g.MobileIPAvaliable, g.CurrentEncryptionKey, g.CurrentEncryptionIVCounter, g.ExternalID, g.CreateDate\r\n  , g.SendSensorInterpretor, g.SensorInterpretorVersion, g.SingleQueueExpiration, g.ServerInterfaceActive, g.RealTimeInterfaceActive, g.RealTimeInterfaceTimeout\r\n  , g.RealTimeInterfacePort, g.ModbusInterfaceActive, g.ModbusInterfaceTimeout, g.ModbusInterfacePort, g.SNMPInterface1Active, g.SNMPTrap1Active\r\n  , g.SNMPInterface2Active, g.SNMPTrap2Active, g.SNMPInterface3Active, g.SNMPTrap3Active, g.SNMPInterface4Active, g.SNMPTrap4Active, g.SNMPInterfaceAddress1\r\n  , g.SNMPInterfaceAddress2, g.SNMPInterfaceAddress3, g.SNMPInterfaceAddress4, g.SNMPInterfacePort1, g.SNMPInterfacePort2, g.SNMPInterfacePort3\r\n  , g.SNMPInterfacePort4, g.SNMPTrapPort1, g.SNMPTrapPort2, g.SNMPTrapPort3, g.SNMPTrapPort4, g.RequestConfigPage, g.GPSReportInterval, g.ForceLowPower\r\n  , g.PowerSourceID, g.GatewayPowerMode, g.isEnterpriseHost, g.SensorhoodConfigDirty, g.SensorhoodID, g.SensorhoodTransmitions, g.PollInterval, g.UrgentTraffic\r\n  , g.DecryptFailureDate, g.SendWatchDogTest, g.SecurityKey, g.AutoConfigTime, g.hasActionControlCommand, g.AutoConfigActionCommandTime\r\n  , g.resumeNotificationDate, g.IsUnlocked, g.SendUnlockRequest, g.Make, g.Model, g.SerialNumber, g.Location, g.Description, g.Note, g.Housing\r\n  , g.RadioFirmwareUpdateID, g.UpdateRadioFirmware, g.GenerationType, g.RadioFirmwareUpdateInProgress, g.StartDate, g.SNMPCommunityString\r\n  , g.GatewayCommunicationPreference, g.SystemOptions, g.MQTTPort, g.MQTTBrokerAddress, g.MQTTUser, g.MQTTPassword, g.MQTTClientID, g.MQTTClientTopic\r\n  , g.MQTTPublicationInterval, g.MQTTKeepAlive, g.MQTTAckTimeout, g.MQTTQueueFlushLimit, g.MQTTBehaviorFlags, g.MQTTInterfaceActive, g.NTPInterfaceActive\r\n  , g.NTPServerIP, g.NTPMinSampleRate, g.HTTPInterfaceActive, g.HTTPServiceTimeout, g.BacnetInterfaceActive, g.DisableNetworkOnServerError, g.ResetInterval\r\n  , g.SKU, g.M1BandMask, g.NB1BandMask, g.UMNOProf, g.SIMAuthType, g.OTARequestActive, g.CurrentPublicKey, g.ServerPublicKey, g.SendUpdateModuleCommand\r\n  , g.IsGPSUnlocked, g.SendGPSUnlockRequest, g.GPSPing, g.WarrantyStartDate\r\n  ,IsNull(gs.LastCommunicationDate,g.LastCommunicationDate) LastCommunicationDate\r\n  ,IsNull(gs.CurrentPower, g.CurrentPower) CurrentPower\r\n  ,IsNull(gs.LastInboundIPAddress,g.LastInboundIPAddress) LastInboundIPAddress\r\n  ,IsNull(gs.LastSequence,g.LastSequence) LastSequence\r\n  ,IsNull(gs.LastLocationDate,g.LastLocationDate) LastLocationDate\r\nFROM dbo.[Gateway] g WITH (NOLOCK)\r\nLEFT JOIN [GatewayStatus] gs with(nolock) on g.GatewayID = gs.GatewayID\r\nWHERE g.GatewayID = @GatewayID\r\n  AND (g.IsDeleted = 0 OR g.IsDeleted IS NULL);\r\n")]
  [DBMethodBody(DBMS.SQLite, "Select * from Gateway where GatewayID = @GatewayID AND (IsDeleted = 0 OR IsDeleted is null)")]
  internal class LoadByID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public Monnit.Gateway Result { get; private set; }

    public LoadByID(long ID)
    {
      this.GatewayID = ID;
      this.Result = BaseDBObject.Load<Monnit.Gateway>(this.ToDataTable()).FirstOrDefault<Monnit.Gateway>();
    }
  }

  [DBMethod("Gateway_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  g.GatewayID, g.CSNetID, g.Name, g.GatewayTypeID, g.RadioBand, g.APNFirmwareVersion, g.GatewayFirmwareVersion, g.ReportInterval, g.ServerHostAddress\r\n  , g.Port, g.ChannelMask, g.NetworkIDFilter, g.GatewayIP, g.GatewaySubnet, g.DefaultRouterIP, g.GatewayDNS, g.SecondaryDNS, g.IsDirty\r\n  , g.SendResetNetworkRequest, g.ObserveAware, g.NetworkListInterval, g.ForceToBootloader, g.CellAPNName, g.Username, g.Password, g.MacAddress, g.SensorID\r\n  , g.ErrorHeartbeat, g.WIFISecurityMode, g.WiFiNetworkCount, g.TransmitRate1, g.TransmitRate2, g.TransmitRate3, g.TransmitRateCTS, g.TransmitPower\r\n  , g.LedActiveTime, g.LastKnownChannel, g.LastKnownNetworkID, g.RetreiveNetworkInfo, g.CurrentSignalStrength, g.IsDeleted, g.IgnoreData, g.ServerHostAddress2\r\n  , g.Port2, g.LastKnownDeviceCount, g.CellNetworkType, g.MobileIPAvaliable, g.CurrentEncryptionKey, g.CurrentEncryptionIVCounter, g.ExternalID, g.CreateDate\r\n  , g.SendSensorInterpretor, g.SensorInterpretorVersion, g.SingleQueueExpiration, g.ServerInterfaceActive, g.RealTimeInterfaceActive, g.RealTimeInterfaceTimeout\r\n  , g.RealTimeInterfacePort, g.ModbusInterfaceActive, g.ModbusInterfaceTimeout, g.ModbusInterfacePort, g.SNMPInterface1Active, g.SNMPTrap1Active\r\n  , g.SNMPInterface2Active, g.SNMPTrap2Active, g.SNMPInterface3Active, g.SNMPTrap3Active, g.SNMPInterface4Active, g.SNMPTrap4Active, g.SNMPInterfaceAddress1\r\n  , g.SNMPInterfaceAddress2, g.SNMPInterfaceAddress3, g.SNMPInterfaceAddress4, g.SNMPInterfacePort1, g.SNMPInterfacePort2, g.SNMPInterfacePort3\r\n  , g.SNMPInterfacePort4, g.SNMPTrapPort1, g.SNMPTrapPort2, g.SNMPTrapPort3, g.SNMPTrapPort4, g.RequestConfigPage, g.GPSReportInterval, g.ForceLowPower\r\n  , g.PowerSourceID, g.GatewayPowerMode, g.isEnterpriseHost, g.SensorhoodConfigDirty, g.SensorhoodID, g.SensorhoodTransmitions, g.PollInterval, g.UrgentTraffic\r\n  , g.DecryptFailureDate, g.SendWatchDogTest, g.SecurityKey, g.AutoConfigTime, g.hasActionControlCommand, g.AutoConfigActionCommandTime\r\n  , g.resumeNotificationDate, g.IsUnlocked, g.SendUnlockRequest, g.Make, g.Model, g.SerialNumber, g.Location, g.Description, g.Note, g.Housing\r\n  , g.RadioFirmwareUpdateID, g.UpdateRadioFirmware, g.GenerationType, g.RadioFirmwareUpdateInProgress, g.StartDate, g.SNMPCommunityString\r\n  , g.GatewayCommunicationPreference, g.SystemOptions, g.MQTTPort, g.MQTTBrokerAddress, g.MQTTUser, g.MQTTPassword, g.MQTTClientID, g.MQTTClientTopic\r\n  , g.MQTTPublicationInterval, g.MQTTKeepAlive, g.MQTTAckTimeout, g.MQTTQueueFlushLimit, g.MQTTBehaviorFlags, g.MQTTInterfaceActive, g.NTPInterfaceActive\r\n  , g.NTPServerIP, g.NTPMinSampleRate, g.HTTPInterfaceActive, g.HTTPServiceTimeout, g.BacnetInterfaceActive, g.DisableNetworkOnServerError, g.ResetInterval\r\n  , g.SKU, g.M1BandMask, g.NB1BandMask, g.UMNOProf, g.SIMAuthType, g.OTARequestActive, g.CurrentPublicKey, g.ServerPublicKey, g.SendUpdateModuleCommand\r\n  , g.IsGPSUnlocked, g.SendGPSUnlockRequest, g.GPSPing, g.WarrantyStartDate\r\n  ,IsNull(gs.LastCommunicationDate,g.LastCommunicationDate) LastCommunicationDate\r\n  ,IsNull(gs.CurrentPower, g.CurrentPower) CurrentPower\r\n  ,IsNull(gs.LastInboundIPAddress,g.LastInboundIPAddress) LastInboundIPAddress\r\n  ,IsNull(gs.LastSequence,g.LastSequence) LastSequence\r\n  ,IsNull(gs.LastLocationDate,g.LastLocationDate) LastLocationDate\r\nFROM dbo.[Gateway] g WITH (NOLOCK)\r\nLEFT JOIN [GatewayStatus] gs with(nolock) on g.GatewayID = gs.GatewayID\r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) ON c.CSNetID = g.CSNetID\r\nWHERE c.AccountID  = @AccountID\r\n  AND (g.IsDeleted = 0 OR g.IsDeleted IS NULL);\r\n")]
  internal class LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.Gateway> Result { get; private set; }

    public LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.Gateway>(this.ToDataTable());
      Monnit.Gateway.EnablePropertyChangedEvent(this.Result);
    }
  }

  [DBMethod("Gateway_LoadBySensorID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  g.GatewayID, g.CSNetID, g.Name, g.GatewayTypeID, g.RadioBand, g.APNFirmwareVersion, g.GatewayFirmwareVersion, g.ReportInterval, g.ServerHostAddress\r\n  , g.Port, g.ChannelMask, g.NetworkIDFilter, g.GatewayIP, g.GatewaySubnet, g.DefaultRouterIP, g.GatewayDNS, g.SecondaryDNS, g.IsDirty\r\n  , g.SendResetNetworkRequest, g.ObserveAware, g.NetworkListInterval, g.ForceToBootloader, g.CellAPNName, g.Username, g.Password, g.MacAddress, g.SensorID\r\n  , g.ErrorHeartbeat, g.WIFISecurityMode, g.WiFiNetworkCount, g.TransmitRate1, g.TransmitRate2, g.TransmitRate3, g.TransmitRateCTS, g.TransmitPower\r\n  , g.LedActiveTime, g.LastKnownChannel, g.LastKnownNetworkID, g.RetreiveNetworkInfo, g.CurrentSignalStrength, g.IsDeleted, g.IgnoreData, g.ServerHostAddress2\r\n  , g.Port2, g.LastKnownDeviceCount, g.CellNetworkType, g.MobileIPAvaliable, g.CurrentEncryptionKey, g.CurrentEncryptionIVCounter, g.ExternalID, g.CreateDate\r\n  , g.SendSensorInterpretor, g.SensorInterpretorVersion, g.SingleQueueExpiration, g.ServerInterfaceActive, g.RealTimeInterfaceActive, g.RealTimeInterfaceTimeout\r\n  , g.RealTimeInterfacePort, g.ModbusInterfaceActive, g.ModbusInterfaceTimeout, g.ModbusInterfacePort, g.SNMPInterface1Active, g.SNMPTrap1Active\r\n  , g.SNMPInterface2Active, g.SNMPTrap2Active, g.SNMPInterface3Active, g.SNMPTrap3Active, g.SNMPInterface4Active, g.SNMPTrap4Active, g.SNMPInterfaceAddress1\r\n  , g.SNMPInterfaceAddress2, g.SNMPInterfaceAddress3, g.SNMPInterfaceAddress4, g.SNMPInterfacePort1, g.SNMPInterfacePort2, g.SNMPInterfacePort3\r\n  , g.SNMPInterfacePort4, g.SNMPTrapPort1, g.SNMPTrapPort2, g.SNMPTrapPort3, g.SNMPTrapPort4, g.RequestConfigPage, g.GPSReportInterval, g.ForceLowPower\r\n  , g.PowerSourceID, g.GatewayPowerMode, g.isEnterpriseHost, g.SensorhoodConfigDirty, g.SensorhoodID, g.SensorhoodTransmitions, g.PollInterval, g.UrgentTraffic\r\n  , g.DecryptFailureDate, g.SendWatchDogTest, g.SecurityKey, g.AutoConfigTime, g.hasActionControlCommand, g.AutoConfigActionCommandTime\r\n  , g.resumeNotificationDate, g.IsUnlocked, g.SendUnlockRequest, g.Make, g.Model, g.SerialNumber, g.Location, g.Description, g.Note, g.Housing\r\n  , g.RadioFirmwareUpdateID, g.UpdateRadioFirmware, g.GenerationType, g.RadioFirmwareUpdateInProgress, g.StartDate, g.SNMPCommunityString\r\n  , g.GatewayCommunicationPreference, g.SystemOptions, g.MQTTPort, g.MQTTBrokerAddress, g.MQTTUser, g.MQTTPassword, g.MQTTClientID, g.MQTTClientTopic\r\n  , g.MQTTPublicationInterval, g.MQTTKeepAlive, g.MQTTAckTimeout, g.MQTTQueueFlushLimit, g.MQTTBehaviorFlags, g.MQTTInterfaceActive, g.NTPInterfaceActive\r\n  , g.NTPServerIP, g.NTPMinSampleRate, g.HTTPInterfaceActive, g.HTTPServiceTimeout, g.BacnetInterfaceActive, g.DisableNetworkOnServerError, g.ResetInterval\r\n  , g.SKU, g.M1BandMask, g.NB1BandMask, g.UMNOProf, g.SIMAuthType, g.OTARequestActive, g.CurrentPublicKey, g.ServerPublicKey, g.SendUpdateModuleCommand\r\n  , g.IsGPSUnlocked, g.SendGPSUnlockRequest, g.GPSPing, g.WarrantyStartDate\r\n  ,IsNull(gs.LastCommunicationDate,g.LastCommunicationDate) LastCommunicationDate\r\n  ,IsNull(gs.CurrentPower, g.CurrentPower) CurrentPower\r\n  ,IsNull(gs.LastInboundIPAddress,g.LastInboundIPAddress) LastInboundIPAddress\r\n  ,IsNull(gs.LastSequence,g.LastSequence) LastSequence\r\n  ,IsNull(gs.LastLocationDate,g.LastLocationDate) LastLocationDate\r\nFROM dbo.[Gateway] g WITH (NOLOCK)\r\nLEFT JOIN [GatewayStatus] gs with(nolock) on g.GatewayID = gs.GatewayID\r\nWHERE g.SensorID = @SensorID\r\n  AND (g.IsDeleted = 0 OR g.IsDeleted IS NULL);\r\n")]
  [DBMethodBody(DBMS.SQLite, "Select * from Gateway where SensorID = @SensorID AND (IsDeleted = 0 OR IsDeleted is null)")]
  internal class LoadBySensorID : BaseDBMethod
  {
    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

    public List<Monnit.Gateway> Result { get; private set; }

    public LoadBySensorID(long sensorID)
    {
      this.SensorID = sensorID;
      this.Result = BaseDBObject.Load<Monnit.Gateway>(this.ToDataTable());
    }
  }

  [DBMethod("Gateway_LoadByCSNetID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  g.GatewayID, g.CSNetID, g.Name, g.GatewayTypeID, g.RadioBand, g.APNFirmwareVersion, g.GatewayFirmwareVersion, g.ReportInterval, g.ServerHostAddress\r\n  , g.Port, g.ChannelMask, g.NetworkIDFilter, g.GatewayIP, g.GatewaySubnet, g.DefaultRouterIP, g.GatewayDNS, g.SecondaryDNS, g.IsDirty\r\n  , g.SendResetNetworkRequest, g.ObserveAware, g.NetworkListInterval, g.ForceToBootloader, g.CellAPNName, g.Username, g.Password, g.MacAddress, g.SensorID\r\n  , g.ErrorHeartbeat, g.WIFISecurityMode, g.WiFiNetworkCount, g.TransmitRate1, g.TransmitRate2, g.TransmitRate3, g.TransmitRateCTS, g.TransmitPower\r\n  , g.LedActiveTime, g.LastKnownChannel, g.LastKnownNetworkID, g.RetreiveNetworkInfo, g.CurrentSignalStrength, g.IsDeleted, g.IgnoreData, g.ServerHostAddress2\r\n  , g.Port2, g.LastKnownDeviceCount, g.CellNetworkType, g.MobileIPAvaliable, g.CurrentEncryptionKey, g.CurrentEncryptionIVCounter, g.ExternalID, g.CreateDate\r\n  , g.SendSensorInterpretor, g.SensorInterpretorVersion, g.SingleQueueExpiration, g.ServerInterfaceActive, g.RealTimeInterfaceActive, g.RealTimeInterfaceTimeout\r\n  , g.RealTimeInterfacePort, g.ModbusInterfaceActive, g.ModbusInterfaceTimeout, g.ModbusInterfacePort, g.SNMPInterface1Active, g.SNMPTrap1Active\r\n  , g.SNMPInterface2Active, g.SNMPTrap2Active, g.SNMPInterface3Active, g.SNMPTrap3Active, g.SNMPInterface4Active, g.SNMPTrap4Active, g.SNMPInterfaceAddress1\r\n  , g.SNMPInterfaceAddress2, g.SNMPInterfaceAddress3, g.SNMPInterfaceAddress4, g.SNMPInterfacePort1, g.SNMPInterfacePort2, g.SNMPInterfacePort3\r\n  , g.SNMPInterfacePort4, g.SNMPTrapPort1, g.SNMPTrapPort2, g.SNMPTrapPort3, g.SNMPTrapPort4, g.RequestConfigPage, g.GPSReportInterval, g.ForceLowPower\r\n  , g.PowerSourceID, g.GatewayPowerMode, g.isEnterpriseHost, g.SensorhoodConfigDirty, g.SensorhoodID, g.SensorhoodTransmitions, g.PollInterval, g.UrgentTraffic\r\n  , g.DecryptFailureDate, g.SendWatchDogTest, g.SecurityKey, g.AutoConfigTime, g.hasActionControlCommand, g.AutoConfigActionCommandTime\r\n  , g.resumeNotificationDate, g.IsUnlocked, g.SendUnlockRequest, g.Make, g.Model, g.SerialNumber, g.Location, g.Description, g.Note, g.Housing\r\n  , g.RadioFirmwareUpdateID, g.UpdateRadioFirmware, g.GenerationType, g.RadioFirmwareUpdateInProgress, g.StartDate, g.SNMPCommunityString\r\n  , g.GatewayCommunicationPreference, g.SystemOptions, g.MQTTPort, g.MQTTBrokerAddress, g.MQTTUser, g.MQTTPassword, g.MQTTClientID, g.MQTTClientTopic\r\n  , g.MQTTPublicationInterval, g.MQTTKeepAlive, g.MQTTAckTimeout, g.MQTTQueueFlushLimit, g.MQTTBehaviorFlags, g.MQTTInterfaceActive, g.NTPInterfaceActive\r\n  , g.NTPServerIP, g.NTPMinSampleRate, g.HTTPInterfaceActive, g.HTTPServiceTimeout, g.BacnetInterfaceActive, g.DisableNetworkOnServerError, g.ResetInterval\r\n  , g.SKU, g.M1BandMask, g.NB1BandMask, g.UMNOProf, g.SIMAuthType, g.OTARequestActive, g.CurrentPublicKey, g.ServerPublicKey, g.SendUpdateModuleCommand\r\n  , g.IsGPSUnlocked, g.SendGPSUnlockRequest, g.GPSPing, g.WarrantyStartDate\r\n  ,IsNull(gs.LastCommunicationDate,g.LastCommunicationDate) LastCommunicationDate\r\n  ,IsNull(gs.CurrentPower, g.CurrentPower) CurrentPower\r\n  ,IsNull(gs.LastInboundIPAddress,g.LastInboundIPAddress) LastInboundIPAddress\r\n  ,IsNull(gs.LastSequence,g.LastSequence) LastSequence\r\n  ,IsNull(gs.LastLocationDate,g.LastLocationDate) LastLocationDate\r\nFROM dbo.[Gateway] g with(nolock)\r\nLEFT JOIN [GatewayStatus] gs with(nolock) on g.GatewayID = gs.GatewayID\r\nWHERE g.CSNetID = @CSNetID \r\n  AND (g.IsDeleted = 0 OR g.IsDeleted IS NULL);\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM Gateway WHERE CSNetID = @CSNetID AND (IsDeleted = 0 OR IsDeleted is null)")]
  internal class LoadByCSNetID : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    public List<Monnit.Gateway> Result { get; private set; }

    public LoadByCSNetID(long cSNetID)
    {
      this.CSNetID = cSNetID;
      this.Result = BaseDBObject.Load<Monnit.Gateway>(this.ToDataTable());
    }
  }

  [DBMethod("Gateway_LoadByMAC")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  g.GatewayID, g.CSNetID, g.Name, g.GatewayTypeID, g.RadioBand, g.APNFirmwareVersion, g.GatewayFirmwareVersion, g.ReportInterval, g.ServerHostAddress\r\n  , g.Port, g.ChannelMask, g.NetworkIDFilter, g.GatewayIP, g.GatewaySubnet, g.DefaultRouterIP, g.GatewayDNS, g.SecondaryDNS, g.IsDirty\r\n  , g.SendResetNetworkRequest, g.ObserveAware, g.NetworkListInterval, g.ForceToBootloader, g.CellAPNName, g.Username, g.Password, g.MacAddress, g.SensorID\r\n  , g.ErrorHeartbeat, g.WIFISecurityMode, g.WiFiNetworkCount, g.TransmitRate1, g.TransmitRate2, g.TransmitRate3, g.TransmitRateCTS, g.TransmitPower\r\n  , g.LedActiveTime, g.LastKnownChannel, g.LastKnownNetworkID, g.RetreiveNetworkInfo, g.CurrentSignalStrength, g.IsDeleted, g.IgnoreData, g.ServerHostAddress2\r\n  , g.Port2, g.LastKnownDeviceCount, g.CellNetworkType, g.MobileIPAvaliable, g.CurrentEncryptionKey, g.CurrentEncryptionIVCounter, g.ExternalID, g.CreateDate\r\n  , g.SendSensorInterpretor, g.SensorInterpretorVersion, g.SingleQueueExpiration, g.ServerInterfaceActive, g.RealTimeInterfaceActive, g.RealTimeInterfaceTimeout\r\n  , g.RealTimeInterfacePort, g.ModbusInterfaceActive, g.ModbusInterfaceTimeout, g.ModbusInterfacePort, g.SNMPInterface1Active, g.SNMPTrap1Active\r\n  , g.SNMPInterface2Active, g.SNMPTrap2Active, g.SNMPInterface3Active, g.SNMPTrap3Active, g.SNMPInterface4Active, g.SNMPTrap4Active, g.SNMPInterfaceAddress1\r\n  , g.SNMPInterfaceAddress2, g.SNMPInterfaceAddress3, g.SNMPInterfaceAddress4, g.SNMPInterfacePort1, g.SNMPInterfacePort2, g.SNMPInterfacePort3\r\n  , g.SNMPInterfacePort4, g.SNMPTrapPort1, g.SNMPTrapPort2, g.SNMPTrapPort3, g.SNMPTrapPort4, g.RequestConfigPage, g.GPSReportInterval, g.ForceLowPower\r\n  , g.PowerSourceID, g.GatewayPowerMode, g.isEnterpriseHost, g.SensorhoodConfigDirty, g.SensorhoodID, g.SensorhoodTransmitions, g.PollInterval, g.UrgentTraffic\r\n  , g.DecryptFailureDate, g.SendWatchDogTest, g.SecurityKey, g.AutoConfigTime, g.hasActionControlCommand, g.AutoConfigActionCommandTime\r\n  , g.resumeNotificationDate, g.IsUnlocked, g.SendUnlockRequest, g.Make, g.Model, g.SerialNumber, g.Location, g.Description, g.Note, g.Housing\r\n  , g.RadioFirmwareUpdateID, g.UpdateRadioFirmware, g.GenerationType, g.RadioFirmwareUpdateInProgress, g.StartDate, g.SNMPCommunityString\r\n  , g.GatewayCommunicationPreference, g.SystemOptions, g.MQTTPort, g.MQTTBrokerAddress, g.MQTTUser, g.MQTTPassword, g.MQTTClientID, g.MQTTClientTopic\r\n  , g.MQTTPublicationInterval, g.MQTTKeepAlive, g.MQTTAckTimeout, g.MQTTQueueFlushLimit, g.MQTTBehaviorFlags, g.MQTTInterfaceActive, g.NTPInterfaceActive\r\n  , g.NTPServerIP, g.NTPMinSampleRate, g.HTTPInterfaceActive, g.HTTPServiceTimeout, g.BacnetInterfaceActive, g.DisableNetworkOnServerError, g.ResetInterval\r\n  , g.SKU, g.M1BandMask, g.NB1BandMask, g.UMNOProf, g.SIMAuthType, g.OTARequestActive, g.CurrentPublicKey, g.ServerPublicKey, g.SendUpdateModuleCommand\r\n  , g.IsGPSUnlocked, g.SendGPSUnlockRequest, g.GPSPing, g.WarrantyStartDate\r\n  ,IsNull(gs.LastCommunicationDate,g.LastCommunicationDate) LastCommunicationDate\r\n  ,IsNull(gs.CurrentPower, g.CurrentPower) CurrentPower\r\n  ,IsNull(gs.LastInboundIPAddress,g.LastInboundIPAddress) LastInboundIPAddress\r\n  ,IsNull(gs.LastSequence,g.LastSequence) LastSequence\r\n  ,IsNull(gs.LastLocationDate,g.LastLocationDate) LastLocationDate\r\nFROM dbo.[Gateway] g with(nolock)\r\nLEFT JOIN [GatewayStatus] gs with(nolock) on g.GatewayID = gs.GatewayID\r\nWHERE MacAddress = @MAC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT * FROM Gateway WHERE MacAddress = '@MAC'")]
  internal class LoadByMAC : BaseDBMethod
  {
    [DBMethodParam("MAC", typeof (string))]
    public string MAC { get; private set; }

    public Monnit.Gateway Result { get; private set; }

    public LoadByMAC(string mac)
    {
      this.MAC = mac;
      DataTable dataTable = this.ToDataTable();
      if (dataTable.Rows.Count <= 0)
        return;
      this.Result = new Monnit.Gateway();
      this.Result.Load(dataTable.Rows[0]);
    }
  }

  [DBMethod("Gateway_HasUrgentTraffic")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  UrgentTraffic\r\nFROM dbo.[Gateway] g\r\nWHERE g.GatewayID = @GatewayID;\r\n")]
  internal class HasUrgentTraffic : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public bool Result { get; private set; }

    public HasUrgentTraffic(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = this.ToScalarValue<bool>();
    }
  }

  [DBMethod("Gateway_LoadByAccountIDForUnlockList")]
  [DBMethodBody(DBMS.SqlServer, "\r\n--Type 30 AND GatewayFirmwareVersion is greater than 2.0.1.2\r\nSELECT \r\n  g.GatewayID, g.CSNetID, g.Name, g.GatewayTypeID, g.RadioBand, g.APNFirmwareVersion, g.GatewayFirmwareVersion, g.ReportInterval, g.ServerHostAddress\r\n  , g.Port, g.ChannelMask, g.NetworkIDFilter, g.GatewayIP, g.GatewaySubnet, g.DefaultRouterIP, g.GatewayDNS, g.SecondaryDNS, g.IsDirty\r\n  , g.SendResetNetworkRequest, g.ObserveAware, g.NetworkListInterval, g.ForceToBootloader, g.CellAPNName, g.Username, g.Password, g.MacAddress, g.SensorID\r\n  , g.ErrorHeartbeat, g.WIFISecurityMode, g.WiFiNetworkCount, g.TransmitRate1, g.TransmitRate2, g.TransmitRate3, g.TransmitRateCTS, g.TransmitPower\r\n  , g.LedActiveTime, g.LastKnownChannel, g.LastKnownNetworkID, g.RetreiveNetworkInfo, g.CurrentSignalStrength, g.IsDeleted, g.IgnoreData, g.ServerHostAddress2\r\n  , g.Port2, g.LastKnownDeviceCount, g.CellNetworkType, g.MobileIPAvaliable, g.CurrentEncryptionKey, g.CurrentEncryptionIVCounter, g.ExternalID, g.CreateDate\r\n  , g.SendSensorInterpretor, g.SensorInterpretorVersion, g.SingleQueueExpiration, g.ServerInterfaceActive, g.RealTimeInterfaceActive, g.RealTimeInterfaceTimeout\r\n  , g.RealTimeInterfacePort, g.ModbusInterfaceActive, g.ModbusInterfaceTimeout, g.ModbusInterfacePort, g.SNMPInterface1Active, g.SNMPTrap1Active\r\n  , g.SNMPInterface2Active, g.SNMPTrap2Active, g.SNMPInterface3Active, g.SNMPTrap3Active, g.SNMPInterface4Active, g.SNMPTrap4Active, g.SNMPInterfaceAddress1\r\n  , g.SNMPInterfaceAddress2, g.SNMPInterfaceAddress3, g.SNMPInterfaceAddress4, g.SNMPInterfacePort1, g.SNMPInterfacePort2, g.SNMPInterfacePort3\r\n  , g.SNMPInterfacePort4, g.SNMPTrapPort1, g.SNMPTrapPort2, g.SNMPTrapPort3, g.SNMPTrapPort4, g.RequestConfigPage, g.GPSReportInterval, g.ForceLowPower\r\n  , g.PowerSourceID, g.GatewayPowerMode, g.isEnterpriseHost, g.SensorhoodConfigDirty, g.SensorhoodID, g.SensorhoodTransmitions, g.PollInterval, g.UrgentTraffic\r\n  , g.DecryptFailureDate, g.SendWatchDogTest, g.SecurityKey, g.AutoConfigTime, g.hasActionControlCommand, g.AutoConfigActionCommandTime\r\n  , g.resumeNotificationDate, g.IsUnlocked, g.SendUnlockRequest, g.Make, g.Model, g.SerialNumber, g.Location, g.Description, g.Note, g.Housing\r\n  , g.RadioFirmwareUpdateID, g.UpdateRadioFirmware, g.GenerationType, g.RadioFirmwareUpdateInProgress, g.StartDate, g.SNMPCommunityString\r\n  , g.GatewayCommunicationPreference, g.SystemOptions, g.MQTTPort, g.MQTTBrokerAddress, g.MQTTUser, g.MQTTPassword, g.MQTTClientID, g.MQTTClientTopic\r\n  , g.MQTTPublicationInterval, g.MQTTKeepAlive, g.MQTTAckTimeout, g.MQTTQueueFlushLimit, g.MQTTBehaviorFlags, g.MQTTInterfaceActive, g.NTPInterfaceActive\r\n  , g.NTPServerIP, g.NTPMinSampleRate, g.HTTPInterfaceActive, g.HTTPServiceTimeout, g.BacnetInterfaceActive, g.DisableNetworkOnServerError, g.ResetInterval\r\n  , g.SKU, g.M1BandMask, g.NB1BandMask, g.UMNOProf, g.SIMAuthType, g.OTARequestActive, g.CurrentPublicKey, g.ServerPublicKey, g.SendUpdateModuleCommand\r\n  , g.IsGPSUnlocked, g.SendGPSUnlockRequest, g.GPSPing, g.WarrantyStartDate\r\n  ,IsNull(gs.LastCommunicationDate,g.LastCommunicationDate) LastCommunicationDate\r\n  ,IsNull(gs.CurrentPower, g.CurrentPower) CurrentPower\r\n  ,IsNull(gs.LastInboundIPAddress,g.LastInboundIPAddress) LastInboundIPAddress\r\n  ,IsNull(gs.LastSequence,g.LastSequence) LastSequence\r\n  ,IsNull(gs.LastLocationDate,g.LastLocationDate) LastLocationDate\r\nFROM dbo.[Gateway] g WITH (NOLOCK)\r\nLEFT JOIN [GatewayStatus] gs with(nolock) on g.GatewayID = gs.GatewayID\r\nINNER JOIN dbo.[CSNet] c WITH (NOLOCK) on c.CSNetID = g.CSNetID\r\nWHERE g.GatewayTypeID = 30\r\n  AND c.AccountID     = @AccountID\r\n  AND (\r\n        dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 0) > 2\r\n        OR (dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 0) = 2 AND dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 1) > 0 )\r\n        OR (dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 0) = 2 AND dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 1) = 0 AND dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 2) > 1 )\r\n        OR (dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 0) = 2 AND dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 1) = 0 AND dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 2) = 1 AND dbo.SplitIndex(g.GatewayFirmwareVersion, '.', 3) >= 2  )\r\n      );\r\n")]
  internal class LoadByAccountIDForUnlockGpsList : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<Monnit.Gateway> Result { get; private set; }

    public LoadByAccountIDForUnlockGpsList(long accountID)
    {
      this.AccountID = accountID;
      this.Result = BaseDBObject.Load<Monnit.Gateway>(this.ToDataTable());
    }
  }

  [DBMethod("Gateway_DeleteAncillaryObjects")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE FROM [dbo].[GatewayNotification] WHERE GatewayID = @GatewayID\r\nUPDATE [dbo].[Notification] SET GatewayID = null WHERE GatewayID = @GatewayID\r\n\r\nDELETE FROM [dbo].[CustomerFavorite] WHERE GatewayID = @GatewayID\r\nDELETE FROM [dbo].[ExternalDataSubscription] WHERE GatewayID = @GatewayID\r\n--DELETE FROM [dbo].[NotificationRecorded] WHERE GatewayID = @GatewayID -- Don't delete for now 1/16/24\r\nDELETE FROM [dbo].[GatewayWIFICredential] WHERE GatewayID = @GatewayID\r\nDELETE FROM [dbo].[OTARequest] WHERE GatewayID = @GatewayID\r\nDELETE FROM [dbo].[SensorGroupGateway] WHERE GatewayID = @GatewayID\r\nDELETE FROM [dbo].[VisualMapGateway] WHERE GatewayID = @GatewayID\r\n\r\n/*\r\n--Skip--\r\nAcknowledgementRecorded\r\nDataMessage\r\nDataUseList\r\nDataUseLog\r\nExternalDataSubscriptionAttempt -- no longer used\r\nFirmwareCache -- no longer used\r\nGateway\r\nGatewayAttribute\r\nGatewayLocation\r\nGatewayMessage\r\nNetworkAudit\r\nNonCachedAttribute\r\nNotificationRecordedViewTest -- no longer exists\r\nProgrammerAudit\r\nSensorMessageAudit\r\nThirdStone_GatewayAgg\r\n*/\r\n")]
  internal class DeleteAncillaryObjects : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public DeleteAncillaryObjects(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Execute();
    }
  }
}
