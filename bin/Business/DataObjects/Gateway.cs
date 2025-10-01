// Decompiled with JetBrains decompiler
// Type: Monnit.Gateway
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;

#nullable disable
namespace Monnit;

[DBClass("Gateway")]
public class Gateway : BaseDBObject, INotifyPropertyChanged
{
  private long _GatewayID = long.MinValue;
  private bool _SaveFullObject = false;
  private GatewayStatus _Status = new GatewayStatus();
  private long _CSNetID = long.MinValue;
  private DateTime _CreateDate = DateTime.UtcNow;
  private string _Name = string.Empty;
  private long _GatewayTypeID = long.MinValue;
  private string _Housing = string.Empty;
  private string _RadioBand = string.Empty;
  private string _APNFirmwareVersion = string.Empty;
  private string _GatewayFirmwareVersion = string.Empty;
  private GatewayType _GatewayType = (GatewayType) null;
  private double _ReportInterval = double.MinValue;
  private double _PollInterval = double.MinValue;
  private string _ServerHostAddress = string.Empty;
  private string _ServerHostAddress2 = string.Empty;
  private int _Port = int.MinValue;
  private int _Port2 = int.MinValue;
  private long _ChannelMask = (long) uint.MaxValue;
  private int _NetworkIDFilter = 0;
  private string _GatewayIP = string.Empty;
  private string _GatewaySubnet = string.Empty;
  private string _DefaultRouterIP = string.Empty;
  private string _GatewayDNS = string.Empty;
  private string _SecondaryDNS = string.Empty;
  private int _AutoConfigTime = 0;
  private bool _IsDirty = false;
  private bool _SendResetNetworkRequest = false;
  private bool _ObserveAware = true;
  private double _NetworkListInterval = double.MinValue;
  private bool _ForceToBootloader = false;
  private bool _SendSensorInterpretor = false;
  private string _SensorInterpretorVersion = string.Empty;
  private string _CellAPNName = string.Empty;
  private string _Username = string.Empty;
  private string _Password = string.Empty;
  private string _MacAddress = string.Empty;
  private long _SensorID = long.MinValue;
  private double _ErrorHeartbeat = double.MinValue;
  private int _WiFiNetworkCount = int.MinValue;
  private eWIFITransmitionRates _TransmitRate1 = eWIFITransmitionRates._Invalid;
  private eWIFITransmitionRates _TransmitRate2 = eWIFITransmitionRates._Invalid;
  private eWIFITransmitionRates _TransmitRate3 = eWIFITransmitionRates._Invalid;
  private eWIFITransmitionRates _TransmitRateCTS = eWIFITransmitionRates._Invalid;
  private int _TransmitPower = int.MinValue;
  private int _LedActiveTime = int.MinValue;
  private int _LastKnownChannel = int.MinValue;
  private int _LastKnownNetworkID = int.MinValue;
  private int _LastKnownDeviceCount = 0;
  private bool _RetreiveNetworkInfo = false;
  private int _CurrentSignalStrength = 0;
  private bool _IsDeleted = false;
  private bool _IgnoreData = false;
  private int _CellNetworkType = 0;
  private bool _MobileIPAvaliable = false;
  private byte[] _CurrentPublicKey = (byte[]) null;
  private byte[] _CurrentEncryptionKey = (byte[]) null;
  private byte[] _CurrentEncryptionIVCounter = (byte[]) null;
  private byte[] _ServerPublicKey = (byte[]) null;
  private string _ExternalID = string.Empty;
  private double _SingleQueueExpiration = double.MinValue;
  private bool _ServerInterfaceActive = true;
  private bool _RealTimeInterfaceActive = false;
  private double _RealTimeInterfaceTimeout = double.MinValue;
  private int _RealTimeInterfacePort = int.MinValue;
  private bool _ModbusInterfaceActive = false;
  private double _ModbusInterfaceTimeout = double.MinValue;
  private int _ModbusInterfacePort = int.MinValue;
  private bool _SNMPInterface1Active = false;
  private bool _SNMPTrap1Active = false;
  private bool _SNMPInterface2Active = false;
  private bool _SNMPTrap2Active = false;
  private bool _SNMPInterface3Active = false;
  private bool _SNMPTrap3Active = false;
  private bool _SNMPInterface4Active = false;
  private bool _SNMPTrap4Active = false;
  private string _SNMPInterfaceAddress1 = string.Empty;
  private string _SNMPInterfaceAddress2 = string.Empty;
  private string _SNMPInterfaceAddress3 = string.Empty;
  private string _SNMPInterfaceAddress4 = string.Empty;
  private int _SNMPInterfacePort1 = int.MinValue;
  private int _SNMPInterfacePort2 = int.MinValue;
  private int _SNMPInterfacePort3 = int.MinValue;
  private int _SNMPInterfacePort4 = int.MinValue;
  private int _SNMPTrapPort1 = int.MinValue;
  private int _SNMPTrapPort2 = int.MinValue;
  private int _SNMPTrapPort3 = int.MinValue;
  private int _SNMPTrapPort4 = int.MinValue;
  private string _SNMPCommunityString = string.Empty;
  private bool _RequestConfigPage = false;
  private double _GPSReportInterval = double.MinValue;
  private long _PowerSourceID = long.MinValue;
  private PowerSource _PowerSource;
  private eGatewayPowerMode _GatewayPowerMode = eGatewayPowerMode.Standard;
  private bool _SensorhoodConfigDirty = false;
  private long _SensorhoodID = long.MinValue;
  private int _SensorhoodTransmitions = int.MinValue;
  private bool _isEnterpriseHost = false;
  private bool _UrgentTraffic = false;
  private DateTime _DecryptFailureDate = DateTime.MinValue;
  private bool _SendWatchDogTest = false;
  private bool _SendUpdateModuleCommand = false;
  private DateTime _resumeNotificationDate = new DateTime(2010, 1, 1);
  private bool _IsUnlocked = false;
  private byte[] _SecurityKey = (byte[]) null;
  private long _RadioFirmwareUpdateID = long.MinValue;
  private bool _UpdateRadioFirmware = false;
  private bool _RadioFirmwareUpdateInProgress = false;
  private string _GenerationType = string.Empty;
  private DateTime _StartDate = DateTime.MinValue;
  private string _Make = string.Empty;
  private string _Model = string.Empty;
  private string _SerialNumber = string.Empty;
  private string _Location = string.Empty;
  private string _Description = string.Empty;
  private string _Note = string.Empty;
  private eGatewayCommunicationPreference _GatewayCommunicationPreference = eGatewayCommunicationPreference.Cellular_Only;
  private int _SystemOptions = 0;
  private int _MQTTPort = int.MinValue;
  private string _MQTTBrokerAddress = string.Empty;
  private string _MQTTUser = string.Empty;
  private string _MQTTPassword = string.Empty;
  private string _MQTTClientID = string.Empty;
  private string _MQTTClientTopic = string.Empty;
  private int _MQTTPublicationInterval = int.MinValue;
  private int _MQTTKeepAlive = int.MinValue;
  private int _MQTTAckTimeout = int.MinValue;
  private int _MQTTQueueFlushLimit = int.MinValue;
  private int _MQTTBehaviorFlags = int.MinValue;
  private bool _MQTTInterfaceActive = false;
  private bool _NTPInterfaceActive = false;
  private string _NTPServerIP = string.Empty;
  private double _NTPMinSampleRate = double.MinValue;
  private bool _HTTPInterfaceActive = false;
  private double _HTTPServiceTimeout = double.MinValue;
  private bool _BacnetInterfaceActive = false;
  private bool _DisableNetworkOnServerError = false;
  private int _ResetInterval = 0;
  private string _SKU = string.Empty;
  private long _M1BandMask = 0;
  private long _NB1BandMask = 0;
  private int _UMNOProf = 1;
  private int _SIMAuthType = 0;
  private bool _OTARequestActive = false;
  private bool _IsGPSUnlocked = false;
  private bool _GPSPing = false;
  private DateTime _WarrantyStartDate = DateTime.MinValue;
  private bool _hasActionControlCommand = false;
  private int _AutoConfigActionCommandTime = 5;
  private bool _SendUnlockRequest = false;
  private bool _SendGPSUnlockRequest = false;
  private bool _SuppressPropertyChangedEvent = true;

  [DBProp("GatewayID", IsPrimaryKey = true)]
  public long GatewayID
  {
    get => this._GatewayID;
    set
    {
      this._GatewayID = value;
      this._Status.GatewayID = this._GatewayID;
    }
  }

  public void PrepFullSave() => this._SaveFullObject = true;

  public override void Save()
  {
    this._Status.Save();
    if (!this._SaveFullObject)
      return;
    base.Save();
  }

  [DBProp("CurrentPower")]
  public int CurrentPower
  {
    get => this._Status.CurrentPower;
    set => this._Status.CurrentPower = value;
  }

  [DBProp("LastCommunicationDate")]
  public DateTime LastCommunicationDate
  {
    get => this._Status.LastCommunicationDate;
    set => this._Status.LastCommunicationDate = value;
  }

  [DBProp("LastInboundIPAddress", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string LastInboundIPAddress
  {
    get => this._Status.LastInboundIPAddress;
    set => this._Status.LastInboundIPAddress = value;
  }

  [DBProp("LastSequence", DefaultValue = 0)]
  public int LastSequence
  {
    get => this._Status.LastSequence;
    set => this._Status.LastSequence = value;
  }

  [DBProp("LastLocationDate")]
  public DateTime LastLocationDate
  {
    get => this._Status.LastLocationDate;
    set => this._Status.LastLocationDate = value;
  }

  [DBForeignKey("CSNet", "CSNetID")]
  [DBProp("CSNetID", AllowNull = true)]
  public long CSNetID
  {
    get => this._CSNetID;
    set
    {
      long csNetId = this.CSNetID;
      this._CSNetID = value;
      if (csNetId == this.CSNetID)
        return;
      this.OnPropertyChanged(nameof (CSNetID));
    }
  }

  public CSNet Network => CSNet.Load(this.CSNetID);

  public Account Account
  {
    get => this.Network != null ? Account.Load(this.Network.AccountID) : (Account) null;
  }

  public long AccountID
  {
    get
    {
      Account account = this.Account;
      return account != null ? account.AccountID : long.MinValue;
    }
  }

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set
    {
      DateTime createDate = this.CreateDate;
      this._CreateDate = value;
      if (!(createDate != this.CreateDate))
        return;
      this.OnPropertyChanged(nameof (CreateDate));
    }
  }

  [DBProp("Name", AllowNull = false, MaxLength = 255 /*0xFF*/, International = true)]
  public string Name
  {
    get => this._Name;
    set
    {
      string name = this.Name;
      this._Name = value != null ? value : string.Empty;
      if (!(name != this.Name))
        return;
      this.OnPropertyChanged(nameof (Name));
    }
  }

  [DBForeignKey("GatewayType", "GatewayTypeID")]
  [DBProp("GatewayTypeID")]
  public long GatewayTypeID
  {
    get => this._GatewayTypeID;
    set
    {
      long gatewayTypeId = this.GatewayTypeID;
      this._GatewayTypeID = value;
      if (gatewayTypeId == this.GatewayTypeID)
        return;
      this._GatewayType = (GatewayType) null;
      this.OnPropertyChanged(nameof (GatewayTypeID));
    }
  }

  [DBProp("Housing", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Housing
  {
    get => this._Housing;
    set
    {
      string housing = this.Housing;
      this._Housing = value != null ? value : string.Empty;
      if (!(housing != this.Housing))
        return;
      this.OnPropertyChanged(nameof (Housing));
    }
  }

  [DBProp("RadioBand", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string RadioBand
  {
    get => this._RadioBand;
    set
    {
      string radioBand = this.RadioBand;
      this._RadioBand = value != null ? value : string.Empty;
      if (!(radioBand != this.RadioBand))
        return;
      this.OnPropertyChanged(nameof (RadioBand));
    }
  }

  [DBProp("APNFirmwareVersion", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string APNFirmwareVersion
  {
    get
    {
      if (string.IsNullOrEmpty(this._APNFirmwareVersion))
      {
        try
        {
          this._APNFirmwareVersion = ConfigData.FindValue("DefaultFirmwareVersion");
        }
        catch
        {
          this._APNFirmwareVersion = "2.0.0.0";
        }
      }
      return this._APNFirmwareVersion;
    }
    set
    {
      string apnFirmwareVersion = this._APNFirmwareVersion;
      this._APNFirmwareVersion = value != null ? value : string.Empty;
      if (!(apnFirmwareVersion != this._APNFirmwareVersion))
        return;
      this.OnPropertyChanged(nameof (APNFirmwareVersion));
    }
  }

  [DBProp("GatewayFirmwareVersion", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string GatewayFirmwareVersion
  {
    get => this._GatewayFirmwareVersion;
    set
    {
      string gatewayFirmwareVersion = this._GatewayFirmwareVersion;
      this._GatewayFirmwareVersion = value != null ? value : string.Empty;
      if (!(gatewayFirmwareVersion != this.GatewayFirmwareVersion))
        return;
      this.OnPropertyChanged(nameof (GatewayFirmwareVersion));
    }
  }

  public GatewayType GatewayType
  {
    get
    {
      if (this._GatewayType == null && !this.SuppressPropertyChangedEvent)
        this._GatewayType = GatewayType.Load(this._GatewayTypeID);
      return this._GatewayType;
    }
  }

  [DBProp("ReportInterval")]
  public double ReportInterval
  {
    get
    {
      if (this._ReportInterval == double.MinValue && this.GatewayType != null)
        this._ReportInterval = this.GatewayType.DefaultReportInterval;
      if (this.GatewayType != null && this._ReportInterval < (double) this.GatewayType.MinHeartbeat)
        this._ReportInterval = (double) this.GatewayType.MinHeartbeat;
      return this._ReportInterval;
    }
    set
    {
      if (this._ReportInterval != double.MinValue)
      {
        double reportInterval = this.ReportInterval;
        this._ReportInterval = value;
        if (reportInterval != this.ReportInterval)
          this.OnPropertyChanged(nameof (ReportInterval));
      }
      this._ReportInterval = value;
    }
  }

  [DBProp("PollInterval")]
  public double PollInterval
  {
    get
    {
      if (this._PollInterval == double.MinValue && this.GatewayType != null)
        this._PollInterval = this.GatewayType.DefaultPollInterval;
      return this._PollInterval < 0.0 || this._PollInterval > 720.0 ? 0.0 : this._PollInterval;
    }
    set
    {
      double pollInterval = this.PollInterval;
      this._PollInterval = value;
      if (this._PollInterval < 0.0)
        this._PollInterval = 0.0;
      if (pollInterval == this.PollInterval)
        return;
      this.OnPropertyChanged(nameof (PollInterval));
    }
  }

  [DBProp("ServerHostAddress", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string ServerHostAddress
  {
    get
    {
      if (string.IsNullOrEmpty(this._ServerHostAddress) && this.GatewayType != null)
        this._ServerHostAddress = this.GatewayType.DefaultServerHostAddress;
      return this._ServerHostAddress;
    }
    set
    {
      string serverHostAddress = this.ServerHostAddress;
      this._ServerHostAddress = value != null ? value : string.Empty;
      if (!(serverHostAddress != this.ServerHostAddress))
        return;
      this.OnPropertyChanged(nameof (ServerHostAddress));
    }
  }

  [DBProp("ServerHostAddress2", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string ServerHostAddress2
  {
    get
    {
      if (string.IsNullOrEmpty(this._ServerHostAddress2) && this.GatewayType != null)
        this._ServerHostAddress2 = this.GatewayType.DefaultServerHostAddress2;
      return this._ServerHostAddress2;
    }
    set
    {
      string serverHostAddress2 = this.ServerHostAddress2;
      this._ServerHostAddress2 = value != null ? value : string.Empty;
      if (!(serverHostAddress2 != this.ServerHostAddress2))
        return;
      this.OnPropertyChanged(nameof (ServerHostAddress2));
    }
  }

  [DBProp("Port")]
  public int Port
  {
    get
    {
      if (this._Port == int.MinValue && this.GatewayType != null)
        this._Port = this.GatewayType.DefaultPort;
      return this._Port;
    }
    set
    {
      int port = this.Port;
      this._Port = value;
      if (port == this.Port)
        return;
      this.OnPropertyChanged(nameof (Port));
    }
  }

  [DBProp("Port2")]
  public int Port2
  {
    get
    {
      if (this._Port2 == int.MinValue && this.GatewayType != null)
        this._Port2 = this.GatewayType.DefaultPort2;
      return this._Port2;
    }
    set
    {
      int port2 = this.Port2;
      this._Port2 = value;
      if (port2 == this.Port2)
        return;
      this.OnPropertyChanged(nameof (Port2));
    }
  }

  [DBProp("ChannelMask")]
  public long ChannelMask
  {
    get
    {
      if (this._ChannelMask == long.MinValue && this.GatewayType != null)
        this._ChannelMask = this.GatewayType.DefaultChannelMask;
      if (this._GatewayTypeID != long.MinValue && this._GatewayTypeID == 32L /*0x20*/ && this._ChannelMask > (long) ushort.MaxValue)
        this._ChannelMask = (long) ushort.MaxValue;
      return this._ChannelMask;
    }
    set
    {
      long channelMask = this.ChannelMask;
      this._ChannelMask = value;
      if (channelMask == this.ChannelMask)
        return;
      this.OnPropertyChanged(nameof (ChannelMask));
    }
  }

  [DBProp("NetworkIDFilter")]
  public int NetworkIDFilter
  {
    get
    {
      if (this._NetworkIDFilter == int.MinValue && this.GatewayType != null)
        this._NetworkIDFilter = this.GatewayType.DefaultNetworkIDFilter;
      return this._NetworkIDFilter;
    }
    set
    {
      int networkIdFilter = this.NetworkIDFilter;
      this._NetworkIDFilter = value;
      if (networkIdFilter == this.NetworkIDFilter)
        return;
      this.OnPropertyChanged(nameof (NetworkIDFilter));
    }
  }

  [DBProp("GatewayIP", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string GatewayIP
  {
    get
    {
      if (string.IsNullOrEmpty(this._GatewayIP) && this.GatewayType != null)
        this._GatewayIP = this.GatewayType.DefaultGatewayIP;
      return this._GatewayIP;
    }
    set
    {
      string gatewayIp = this.GatewayIP;
      this._GatewayIP = value != null ? value : string.Empty;
      if (!(gatewayIp != this.GatewayIP))
        return;
      this.OnPropertyChanged(nameof (GatewayIP));
    }
  }

  [DBProp("GatewaySubnet", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string GatewaySubnet
  {
    get
    {
      if (string.IsNullOrEmpty(this._GatewaySubnet) && this.GatewayType != null)
        this._GatewaySubnet = this.GatewayType.DefaultGatewaySubnet;
      return this._GatewaySubnet;
    }
    set
    {
      string gatewaySubnet = this.GatewaySubnet;
      this._GatewaySubnet = value != null ? value : string.Empty;
      if (!(gatewaySubnet != this.GatewaySubnet))
        return;
      this.OnPropertyChanged(nameof (GatewaySubnet));
    }
  }

  [DBProp("DefaultRouterIP", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string DefaultRouterIP
  {
    get
    {
      if (string.IsNullOrEmpty(this._DefaultRouterIP) && this.GatewayType != null)
        this._DefaultRouterIP = this.GatewayType.DefaultRouterIP;
      return this._DefaultRouterIP;
    }
    set
    {
      string defaultRouterIp = this.DefaultRouterIP;
      this._DefaultRouterIP = value != null ? value : string.Empty;
      if (!(defaultRouterIp != this.DefaultRouterIP))
        return;
      this.OnPropertyChanged(nameof (DefaultRouterIP));
    }
  }

  [DBProp("GatewayDNS", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string GatewayDNS
  {
    get
    {
      if (string.IsNullOrEmpty(this._GatewayDNS) && this.GatewayType != null)
        this._GatewayDNS = this.GatewayType.DefaultGatewayDNS;
      return this._GatewayDNS;
    }
    set
    {
      string gatewayDns = this.GatewayDNS;
      this._GatewayDNS = value != null ? value : string.Empty;
      if (!(gatewayDns != this.GatewayDNS))
        return;
      this.OnPropertyChanged(nameof (GatewayDNS));
    }
  }

  [DBProp("SecondaryDNS", MaxLength = 64 /*0x40*/)]
  public string SecondaryDNS
  {
    get
    {
      if (string.IsNullOrEmpty(this._SecondaryDNS) && this.GatewayType != null)
        this._SecondaryDNS = this.GatewayType.DefaultSecondaryDNS;
      return this._SecondaryDNS;
    }
    set
    {
      string secondaryDns = this.SecondaryDNS;
      this._SecondaryDNS = value != null ? value : string.Empty;
      if (!(secondaryDns != this.SecondaryDNS))
        return;
      this.OnPropertyChanged(nameof (SecondaryDNS));
    }
  }

  [DBProp("AutoConfigTime")]
  public int AutoConfigTime
  {
    get => this._AutoConfigTime;
    set
    {
      if (value < 0)
        return;
      int autoConfigTime = this.AutoConfigTime;
      this._AutoConfigTime = value;
      if (autoConfigTime != this.AutoConfigTime)
        this.OnPropertyChanged(nameof (AutoConfigTime));
    }
  }

  [DBProp("IsDirty")]
  public bool IsDirty
  {
    get => this._IsDirty;
    set
    {
      bool isDirty = this.IsDirty;
      this._IsDirty = value;
      if (isDirty == this.IsDirty)
        return;
      this.OnPropertyChanged(nameof (IsDirty));
    }
  }

  [DBProp("SendResetNetworkRequest")]
  public bool SendResetNetworkRequest
  {
    get => this._SendResetNetworkRequest;
    set
    {
      bool resetNetworkRequest = this.SendResetNetworkRequest;
      this._SendResetNetworkRequest = value;
      if (resetNetworkRequest == this.SendResetNetworkRequest)
        return;
      this.OnPropertyChanged(nameof (SendResetNetworkRequest));
    }
  }

  [DBProp("ObserveAware")]
  public bool ObserveAware
  {
    get => this._ObserveAware;
    set
    {
      bool observeAware = this.ObserveAware;
      this._ObserveAware = value;
      if (observeAware == this.ObserveAware)
        return;
      this.OnPropertyChanged(nameof (ObserveAware));
    }
  }

  [DBProp("NetworkListInterval")]
  public double NetworkListInterval
  {
    get
    {
      if (this._NetworkListInterval == double.MinValue && this.GatewayType != null)
        this._NetworkListInterval = this.GatewayType.DefaultNetworkListInterval;
      return this._NetworkListInterval;
    }
    set
    {
      double networkListInterval = this.NetworkListInterval;
      this._NetworkListInterval = value;
      if (networkListInterval == this.NetworkListInterval)
        return;
      this.OnPropertyChanged(nameof (NetworkListInterval));
    }
  }

  [DBProp("ForceToBootloader")]
  public bool ForceToBootloader
  {
    get => this._ForceToBootloader;
    set
    {
      bool forceToBootloader = this.ForceToBootloader;
      this._ForceToBootloader = value;
      if (forceToBootloader == this.ForceToBootloader)
        return;
      this.OnPropertyChanged(nameof (ForceToBootloader));
    }
  }

  [DBProp("SendSensorInterpretor")]
  public bool SendSensorInterpretor
  {
    get => this._SendSensorInterpretor;
    set
    {
      bool sensorInterpretor = this.SendSensorInterpretor;
      this._SendSensorInterpretor = value;
      if (sensorInterpretor == this.SendSensorInterpretor)
        return;
      this.OnPropertyChanged(nameof (SendSensorInterpretor));
    }
  }

  [DBProp("SensorInterpretorVersion", MaxLength = 64 /*0x40*/)]
  public string SensorInterpretorVersion
  {
    get => this._SensorInterpretorVersion;
    set
    {
      string interpretorVersion = this.SensorInterpretorVersion;
      this._SensorInterpretorVersion = value;
      if (!(interpretorVersion != this.SensorInterpretorVersion))
        return;
      this.OnPropertyChanged(nameof (SensorInterpretorVersion));
    }
  }

  [DBProp("CellAPNName", MaxLength = 64 /*0x40*/)]
  public string CellAPNName
  {
    get => this._CellAPNName;
    set
    {
      string cellApnName = this.CellAPNName;
      this._CellAPNName = value != null ? value : string.Empty;
      if (!(cellApnName != this.CellAPNName))
        return;
      this.OnPropertyChanged(nameof (CellAPNName));
    }
  }

  [DBProp("Username", MaxLength = 64 /*0x40*/)]
  public string Username
  {
    get => this._Username;
    set
    {
      string username = this.Username;
      this._Username = value != null ? value : string.Empty;
      if (!(username != this.Username))
        return;
      this.OnPropertyChanged(nameof (Username));
    }
  }

  [DBProp("Password", MaxLength = 64 /*0x40*/)]
  public string Password
  {
    get => this._Password;
    set
    {
      string password = this.Password;
      this._Password = value != null ? value : string.Empty;
      if (!(password != this.Password))
        return;
      this.OnPropertyChanged(nameof (Password));
    }
  }

  public DateTime NextCommunicationDate
  {
    get
    {
      if (!(this.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5.0)) || !(this.LastCommunicationDate > DateTime.MinValue) || this.ReportInterval <= 0.0)
        return DateTime.MinValue;
      DateTime communicationDate = this.LastCommunicationDate.AddMinutes(this.ReportInterval);
      while (communicationDate < DateTime.UtcNow)
        communicationDate = communicationDate.AddMinutes(this.ReportInterval);
      return communicationDate;
    }
  }

  [DBProp("MacAddress", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string MacAddress
  {
    get => this._MacAddress;
    set
    {
      string macAddress = this.MacAddress;
      this._MacAddress = value != null ? value.Replace(":", "").Replace("-", "").Replace("_", "") : string.Empty;
      if (!(macAddress != this.MacAddress))
        return;
      this.OnPropertyChanged(nameof (MacAddress));
    }
  }

  [DBForeignKey("Sensor", "SensorID")]
  [DBProp("SensorID", AllowNull = true)]
  public long SensorID
  {
    get => this._SensorID;
    set
    {
      long sensorId = this.SensorID;
      this._SensorID = value;
      if (sensorId == this.SensorID)
        return;
      this.OnPropertyChanged(nameof (SensorID));
    }
  }

  [DBProp("ErrorHeartbeat")]
  public double ErrorHeartbeat
  {
    get
    {
      if (this._ErrorHeartbeat == double.MinValue && this.GatewayType != null)
        this._ErrorHeartbeat = this.GatewayType.DefaultErrorHeartbeat;
      return this._ErrorHeartbeat;
    }
    set
    {
      double errorHeartbeat = this.ErrorHeartbeat;
      this._ErrorHeartbeat = value;
      if (errorHeartbeat == this.ErrorHeartbeat)
        return;
      this.OnPropertyChanged(nameof (ErrorHeartbeat));
    }
  }

  [DBProp("WiFiNetworkCount")]
  public int WiFiNetworkCount
  {
    get => this._WiFiNetworkCount;
    set
    {
      int wiFiNetworkCount = this.WiFiNetworkCount;
      this._WiFiNetworkCount = value;
      if (wiFiNetworkCount == this.WiFiNetworkCount)
        return;
      this.OnPropertyChanged(nameof (WiFiNetworkCount));
    }
  }

  [DBProp("TransmitRate1")]
  public eWIFITransmitionRates TransmitRate1
  {
    get => this._TransmitRate1;
    set
    {
      eWIFITransmitionRates transmitRate1 = this.TransmitRate1;
      this._TransmitRate1 = value;
      if (transmitRate1 == this.TransmitRate1)
        return;
      this.OnPropertyChanged(nameof (TransmitRate1));
    }
  }

  [DBProp("TransmitRate2")]
  public eWIFITransmitionRates TransmitRate2
  {
    get => this._TransmitRate2;
    set
    {
      eWIFITransmitionRates transmitRate2 = this.TransmitRate2;
      this._TransmitRate2 = value;
      if (transmitRate2 == this.TransmitRate2)
        return;
      this.OnPropertyChanged(nameof (TransmitRate2));
    }
  }

  [DBProp("TransmitRate3")]
  public eWIFITransmitionRates TransmitRate3
  {
    get => this._TransmitRate3;
    set
    {
      eWIFITransmitionRates transmitRate3 = this.TransmitRate3;
      this._TransmitRate3 = value;
      if (transmitRate3 == this.TransmitRate3)
        return;
      this.OnPropertyChanged(nameof (TransmitRate3));
    }
  }

  [DBProp("TransmitRateCTS")]
  public eWIFITransmitionRates TransmitRateCTS
  {
    get => this._TransmitRateCTS;
    set
    {
      eWIFITransmitionRates transmitRateCts = this.TransmitRateCTS;
      this._TransmitRateCTS = value;
      if (transmitRateCts == this.TransmitRateCTS)
        return;
      this.OnPropertyChanged(nameof (TransmitRateCTS));
    }
  }

  [DBProp("TransmitPower")]
  public int TransmitPower
  {
    get => this._TransmitPower;
    set
    {
      int transmitPower = this.TransmitPower;
      this._TransmitPower = value;
      if (transmitPower == this.TransmitPower)
        return;
      this.OnPropertyChanged(nameof (TransmitPower));
    }
  }

  [DBProp("LedActiveTime")]
  public int LedActiveTime
  {
    get => this._LedActiveTime;
    set
    {
      int ledActiveTime = this.LedActiveTime;
      this._LedActiveTime = value;
      if (ledActiveTime == this.LedActiveTime)
        return;
      this.OnPropertyChanged(nameof (LedActiveTime));
    }
  }

  [DBProp("LastKnownChannel")]
  public int LastKnownChannel
  {
    get => this._LastKnownChannel;
    set
    {
      int lastKnownChannel = this.LastKnownChannel;
      this._LastKnownChannel = value;
      if (lastKnownChannel == this.LastKnownChannel)
        return;
      this.OnPropertyChanged(nameof (LastKnownChannel));
    }
  }

  [DBProp("LastKnownNetworkID")]
  public int LastKnownNetworkID
  {
    get => this._LastKnownNetworkID;
    set
    {
      int lastKnownNetworkId = this.LastKnownNetworkID;
      this._LastKnownNetworkID = value;
      if (lastKnownNetworkId == this.LastKnownNetworkID)
        return;
      this.OnPropertyChanged(nameof (LastKnownNetworkID));
    }
  }

  [DBProp("LastKnownDeviceCount")]
  public int LastKnownDeviceCount
  {
    get => this._LastKnownDeviceCount;
    set
    {
      int knownDeviceCount = this.LastKnownDeviceCount;
      this._LastKnownDeviceCount = value;
      if (this._LastKnownDeviceCount < 0)
        this._LastKnownDeviceCount = 0;
      if (knownDeviceCount == this.LastKnownDeviceCount)
        return;
      this.OnPropertyChanged(nameof (LastKnownDeviceCount));
    }
  }

  [DBProp("RetreiveNetworkInfo")]
  public bool RetreiveNetworkInfo
  {
    get => this._RetreiveNetworkInfo;
    set
    {
      bool retreiveNetworkInfo = this.RetreiveNetworkInfo;
      this._RetreiveNetworkInfo = value;
      if (retreiveNetworkInfo == this.RetreiveNetworkInfo)
        return;
      this.OnPropertyChanged(nameof (RetreiveNetworkInfo));
    }
  }

  [DBProp("CurrentSignalStrength")]
  public int CurrentSignalStrength
  {
    get => this._CurrentSignalStrength;
    set
    {
      int currentSignalStrength = this.CurrentSignalStrength;
      this._CurrentSignalStrength = value;
      if (currentSignalStrength == this.CurrentSignalStrength)
        return;
      this.OnPropertyChanged(nameof (CurrentSignalStrength));
    }
  }

  [DBProp("IsDeleted")]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set
    {
      bool isDeleted = this.IsDeleted;
      this._IsDeleted = value;
      if (isDeleted == this.IsDeleted)
        return;
      this.OnPropertyChanged(nameof (IsDeleted));
    }
  }

  [DBProp("IgnoreData")]
  public bool IgnoreData
  {
    get => this._IgnoreData;
    set
    {
      bool ignoreData = this.IgnoreData;
      this._IgnoreData = value;
      if (ignoreData == this.IgnoreData)
        return;
      this.OnPropertyChanged(nameof (IgnoreData));
    }
  }

  [DBProp("CellNetworkType")]
  public int CellNetworkType
  {
    get => this._CellNetworkType;
    set
    {
      int cellNetworkType = this.CellNetworkType;
      this._CellNetworkType = value;
      if (cellNetworkType == this.CellNetworkType)
        return;
      this.OnPropertyChanged(nameof (CellNetworkType));
    }
  }

  [DBProp("MobileIPAvaliable")]
  public bool MobileIPAvaliable
  {
    get => this._MobileIPAvaliable;
    set
    {
      bool mobileIpAvaliable = this.MobileIPAvaliable;
      this._MobileIPAvaliable = value;
      if (mobileIpAvaliable == this.MobileIPAvaliable)
        return;
      this.OnPropertyChanged(nameof (MobileIPAvaliable));
    }
  }

  [DBProp("CurrentPublicKey", AllowNull = true, MaxLength = 64 /*0x40*/)]
  public byte[] CurrentPublicKey
  {
    get
    {
      if (this._CurrentPublicKey == null)
        this._CurrentPublicKey = new byte[1];
      return this._CurrentPublicKey;
    }
    set
    {
      if (StructuralComparisons.StructuralEqualityComparer.Equals((object) this._CurrentPublicKey, (object) value))
        return;
      this._CurrentPublicKey = value;
      this.OnPropertyChanged(nameof (CurrentPublicKey));
    }
  }

  [DBProp("CurrentEncryptionKey", AllowNull = true, MaxLength = 16 /*0x10*/)]
  public byte[] CurrentEncryptionKey
  {
    get
    {
      if (this._CurrentEncryptionKey == null)
        this._CurrentEncryptionKey = new byte[1];
      return this._CurrentEncryptionKey;
    }
    set
    {
      if (StructuralComparisons.StructuralEqualityComparer.Equals((object) this._CurrentEncryptionKey, (object) value))
        return;
      this._CurrentEncryptionKey = value;
      this.OnPropertyChanged(nameof (CurrentEncryptionKey));
    }
  }

  [DBProp("CurrentEncryptionIVCounter", AllowNull = true, MaxLength = 16 /*0x10*/)]
  public byte[] CurrentEncryptionIVCounter
  {
    get
    {
      if (this._CurrentEncryptionIVCounter == null)
        this._CurrentEncryptionIVCounter = new byte[1];
      return this._CurrentEncryptionIVCounter;
    }
    set
    {
      if (StructuralComparisons.StructuralEqualityComparer.Equals((object) this._CurrentEncryptionIVCounter, (object) value))
        return;
      this._CurrentEncryptionIVCounter = value;
      this.OnPropertyChanged(nameof (CurrentEncryptionIVCounter));
    }
  }

  [DBProp("ServerPublicKey", AllowNull = true, MaxLength = 64 /*0x40*/)]
  public byte[] ServerPublicKey
  {
    get
    {
      if (this._ServerPublicKey == null)
        this._ServerPublicKey = new byte[1];
      return this._ServerPublicKey;
    }
    set
    {
      if (StructuralComparisons.StructuralEqualityComparer.Equals((object) this._ServerPublicKey, (object) value))
        return;
      this._ServerPublicKey = value;
      this.OnPropertyChanged(nameof (ServerPublicKey));
    }
  }

  [DBProp("ExternalID", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string ExternalID
  {
    get => this._ExternalID;
    set
    {
      string externalId = this.ExternalID;
      this._ExternalID = value != null ? value : string.Empty;
      if (!(externalId != this.ExternalID) || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ExternalID));
    }
  }

  [DBProp("SingleQueueExpiration")]
  public double SingleQueueExpiration
  {
    get
    {
      if (this._SingleQueueExpiration == double.MinValue && this.GatewayType != null)
        this._SingleQueueExpiration = this.GatewayType.DefaultSingleQueueExpiration;
      return this._SingleQueueExpiration;
    }
    set
    {
      double singleQueueExpiration = this.SingleQueueExpiration;
      this._SingleQueueExpiration = value;
      if (singleQueueExpiration == this.SingleQueueExpiration)
        return;
      this.OnPropertyChanged(nameof (SingleQueueExpiration));
    }
  }

  [DBProp("ServerInterfaceActive")]
  public bool ServerInterfaceActive
  {
    get => this._ServerInterfaceActive;
    set
    {
      bool serverInterfaceActive = this.ServerInterfaceActive;
      this._ServerInterfaceActive = value;
      if (serverInterfaceActive == this.ServerInterfaceActive)
        return;
      this.OnPropertyChanged(nameof (ServerInterfaceActive));
    }
  }

  [DBProp("RealTimeInterfaceActive")]
  public bool RealTimeInterfaceActive
  {
    get => this._RealTimeInterfaceActive;
    set
    {
      bool timeInterfaceActive = this.RealTimeInterfaceActive;
      this._RealTimeInterfaceActive = value;
      if (timeInterfaceActive == this.RealTimeInterfaceActive)
        return;
      this.OnPropertyChanged(nameof (RealTimeInterfaceActive));
    }
  }

  [DBProp("RealTimeInterfaceTimeout")]
  [DisplayFormat(DataFormatString = "{0:G17}", ApplyFormatInEditMode = true)]
  public double RealTimeInterfaceTimeout
  {
    get
    {
      if (this._RealTimeInterfaceTimeout == double.MinValue && this.GatewayType != null)
        this._RealTimeInterfaceTimeout = this.GatewayType.DefaultRealTimeInterfaceTimeout;
      return this._RealTimeInterfaceTimeout;
    }
    set
    {
      double interfaceTimeout = this.RealTimeInterfaceTimeout;
      this._RealTimeInterfaceTimeout = value;
      if (interfaceTimeout == this.RealTimeInterfaceTimeout)
        return;
      this.OnPropertyChanged(nameof (RealTimeInterfaceTimeout));
    }
  }

  [DBProp("RealTimeInterfacePort")]
  public int RealTimeInterfacePort
  {
    get
    {
      if (this._RealTimeInterfacePort == int.MinValue && this.GatewayType != null)
        this._RealTimeInterfacePort = this.GatewayType.DefaultRealTimeInterfacePort;
      return this._RealTimeInterfacePort;
    }
    set
    {
      double timeInterfacePort = (double) this.RealTimeInterfacePort;
      this._RealTimeInterfacePort = value;
      if (timeInterfacePort == (double) this.RealTimeInterfacePort)
        return;
      this.OnPropertyChanged(nameof (RealTimeInterfacePort));
    }
  }

  [DBProp("ModbusInterfaceActive")]
  public bool ModbusInterfaceActive
  {
    get => this._ModbusInterfaceActive;
    set
    {
      bool modbusInterfaceActive = this.ModbusInterfaceActive;
      this._ModbusInterfaceActive = value;
      if (modbusInterfaceActive == this.ModbusInterfaceActive)
        return;
      this.OnPropertyChanged(nameof (ModbusInterfaceActive));
    }
  }

  [DBProp("ModbusInterfaceTimeout")]
  public double ModbusInterfaceTimeout
  {
    get
    {
      if (this._ModbusInterfaceTimeout == double.MinValue && this.GatewayType != null)
        this._ModbusInterfaceTimeout = this.GatewayType.DefaultModbusInterfaceTimeout;
      return this._ModbusInterfaceTimeout;
    }
    set
    {
      double interfaceTimeout = this.ModbusInterfaceTimeout;
      this._ModbusInterfaceTimeout = value;
      if (interfaceTimeout == this.ModbusInterfaceTimeout)
        return;
      this.OnPropertyChanged(nameof (ModbusInterfaceTimeout));
    }
  }

  [DBProp("ModbusInterfacePort")]
  public int ModbusInterfacePort
  {
    get
    {
      if (this._ModbusInterfacePort == int.MinValue && this.GatewayType != null)
        this._ModbusInterfacePort = this.GatewayType.DefaultModbusInterfacePort;
      return this._ModbusInterfacePort;
    }
    set
    {
      double modbusInterfacePort = (double) this.ModbusInterfacePort;
      this._ModbusInterfacePort = value;
      if (modbusInterfacePort == (double) this.ModbusInterfacePort)
        return;
      this.OnPropertyChanged(nameof (ModbusInterfacePort));
    }
  }

  [DBProp("SNMPInterface1Active")]
  public bool SNMPInterface1Active
  {
    get => this._SNMPInterface1Active;
    set
    {
      bool interface1Active = this.SNMPInterface1Active;
      this._SNMPInterface1Active = value;
      if (interface1Active == this.SNMPInterface1Active)
        return;
      this.OnPropertyChanged(nameof (SNMPInterface1Active));
    }
  }

  [DBProp("SNMPTrap1Active")]
  public bool SNMPTrap1Active
  {
    get => this._SNMPTrap1Active;
    set
    {
      bool snmpTrap1Active = this.SNMPTrap1Active;
      this._SNMPTrap1Active = value;
      if (snmpTrap1Active == this.SNMPTrap1Active)
        return;
      this.OnPropertyChanged(nameof (SNMPTrap1Active));
    }
  }

  [DBProp("SNMPInterface2Active")]
  public bool SNMPInterface2Active
  {
    get => this._SNMPInterface2Active;
    set
    {
      bool interface2Active = this.SNMPInterface2Active;
      this._SNMPInterface2Active = value;
      if (interface2Active == this.SNMPInterface2Active)
        return;
      this.OnPropertyChanged(nameof (SNMPInterface2Active));
    }
  }

  [DBProp("SNMPTrap2Active")]
  public bool SNMPTrap2Active
  {
    get => this._SNMPTrap2Active;
    set
    {
      bool serverInterfaceActive = this.ServerInterfaceActive;
      this._SNMPTrap2Active = value;
      if (serverInterfaceActive == this.SNMPTrap2Active)
        return;
      this.OnPropertyChanged(nameof (SNMPTrap2Active));
    }
  }

  [DBProp("SNMPInterface3Active")]
  public bool SNMPInterface3Active
  {
    get => this._SNMPInterface3Active;
    set
    {
      bool interface3Active = this.SNMPInterface3Active;
      this._SNMPInterface3Active = value;
      if (interface3Active == this.SNMPInterface3Active)
        return;
      this.OnPropertyChanged(nameof (SNMPInterface3Active));
    }
  }

  [DBProp("SNMPTrap3Active")]
  public bool SNMPTrap3Active
  {
    get => this._SNMPTrap3Active;
    set
    {
      bool snmpTrap3Active = this.SNMPTrap3Active;
      this._SNMPTrap3Active = value;
      if (snmpTrap3Active == this.SNMPTrap3Active)
        return;
      this.OnPropertyChanged(nameof (SNMPTrap3Active));
    }
  }

  [DBProp("SNMPInterface4Active")]
  public bool SNMPInterface4Active
  {
    get => this._SNMPInterface4Active;
    set
    {
      bool interface4Active = this.SNMPInterface4Active;
      this._SNMPInterface4Active = value;
      if (interface4Active == this.SNMPInterface4Active)
        return;
      this.OnPropertyChanged(nameof (SNMPInterface4Active));
    }
  }

  [DBProp("SNMPTrap4Active")]
  public bool SNMPTrap4Active
  {
    get => this._SNMPTrap4Active;
    set
    {
      bool serverInterfaceActive = this.ServerInterfaceActive;
      this._SNMPTrap4Active = value;
      if (serverInterfaceActive == this.SNMPTrap4Active)
        return;
      this.OnPropertyChanged(nameof (SNMPTrap4Active));
    }
  }

  [DBProp("SNMPInterfaceAddress1", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string SNMPInterfaceAddress1
  {
    get => this._SNMPInterfaceAddress1;
    set
    {
      string interfaceAddress1 = this.SNMPInterfaceAddress1;
      this._SNMPInterfaceAddress1 = value == null ? string.Empty : value;
      if (!(interfaceAddress1 != this.SNMPInterfaceAddress1))
        return;
      this.OnPropertyChanged(nameof (SNMPInterfaceAddress1));
    }
  }

  [DBProp("SNMPInterfaceAddress2", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string SNMPInterfaceAddress2
  {
    get => this._SNMPInterfaceAddress2;
    set
    {
      string interfaceAddress2 = this.SNMPInterfaceAddress2;
      this._SNMPInterfaceAddress2 = value == null ? string.Empty : value;
      if (!(interfaceAddress2 != this.SNMPInterfaceAddress2))
        return;
      this.OnPropertyChanged(nameof (SNMPInterfaceAddress2));
    }
  }

  [DBProp("SNMPInterfaceAddress3", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string SNMPInterfaceAddress3
  {
    get => this._SNMPInterfaceAddress3;
    set
    {
      string interfaceAddress3 = this.SNMPInterfaceAddress3;
      this._SNMPInterfaceAddress3 = value == null ? string.Empty : value;
      if (!(interfaceAddress3 != this.SNMPInterfaceAddress3))
        return;
      this.OnPropertyChanged(nameof (SNMPInterfaceAddress3));
    }
  }

  [DBProp("SNMPInterfaceAddress4", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string SNMPInterfaceAddress4
  {
    get => this._SNMPInterfaceAddress4;
    set
    {
      string interfaceAddress4 = this.SNMPInterfaceAddress4;
      this._SNMPInterfaceAddress4 = value == null ? string.Empty : value;
      if (!(interfaceAddress4 != this.SNMPInterfaceAddress4))
        return;
      this.OnPropertyChanged(nameof (SNMPInterfaceAddress4));
    }
  }

  [DBProp("SNMPInterfacePort1")]
  public int SNMPInterfacePort1
  {
    get
    {
      if (this._SNMPInterfacePort1 == int.MinValue && this.GatewayType != null)
        this._SNMPInterfacePort1 = this.GatewayType.DefaultSNMPInterfacePort;
      return this._SNMPInterfacePort1;
    }
    set
    {
      double snmpInterfacePort1 = (double) this.SNMPInterfacePort1;
      this._SNMPInterfacePort1 = value;
      if (snmpInterfacePort1 == (double) this.SNMPInterfacePort1)
        return;
      this.OnPropertyChanged(nameof (SNMPInterfacePort1));
    }
  }

  [DBProp("SNMPInterfacePort2")]
  public int SNMPInterfacePort2
  {
    get
    {
      if (this._SNMPInterfacePort2 == int.MinValue && this.GatewayType != null)
        this._SNMPInterfacePort2 = this.GatewayType.DefaultSNMPInterfacePort;
      return this._SNMPInterfacePort2;
    }
    set
    {
      double snmpInterfacePort2 = (double) this.SNMPInterfacePort2;
      this._SNMPInterfacePort2 = value;
      if (snmpInterfacePort2 == (double) this.SNMPInterfacePort2)
        return;
      this.OnPropertyChanged(nameof (SNMPInterfacePort2));
    }
  }

  [DBProp("SNMPInterfacePort3")]
  public int SNMPInterfacePort3
  {
    get
    {
      if (this._SNMPInterfacePort3 == int.MinValue && this.GatewayType != null)
        this._SNMPInterfacePort3 = this.GatewayType.DefaultSNMPInterfacePort;
      return this._SNMPInterfacePort3;
    }
    set
    {
      double snmpInterfacePort3 = (double) this.SNMPInterfacePort3;
      this._SNMPInterfacePort3 = value;
      if (snmpInterfacePort3 == (double) this.SNMPInterfacePort3)
        return;
      this.OnPropertyChanged(nameof (SNMPInterfacePort3));
    }
  }

  [DBProp("SNMPInterfacePort4")]
  public int SNMPInterfacePort4
  {
    get
    {
      if (this._SNMPInterfacePort4 == int.MinValue && this.GatewayType != null)
        this._SNMPInterfacePort4 = this.GatewayType.DefaultSNMPInterfacePort;
      return this._SNMPInterfacePort4;
    }
    set
    {
      double snmpInterfacePort4 = (double) this.SNMPInterfacePort4;
      this._SNMPInterfacePort4 = value;
      if (snmpInterfacePort4 == (double) this.SNMPInterfacePort4)
        return;
      this.OnPropertyChanged(nameof (SNMPInterfacePort4));
    }
  }

  [DBProp("SNMPTrapPort1")]
  public int SNMPTrapPort1
  {
    get
    {
      if (this._SNMPTrapPort1 == int.MinValue && this.GatewayType != null)
        this._SNMPTrapPort1 = this.GatewayType.DefaultSNMPTrapPort;
      return this._SNMPTrapPort1;
    }
    set
    {
      double snmpTrapPort1 = (double) this.SNMPTrapPort1;
      this._SNMPTrapPort1 = value;
      if (snmpTrapPort1 == (double) this.SNMPTrapPort1)
        return;
      this.OnPropertyChanged(nameof (SNMPTrapPort1));
    }
  }

  [DBProp("SNMPTrapPort2")]
  public int SNMPTrapPort2
  {
    get
    {
      if (this._SNMPTrapPort2 == int.MinValue && this.GatewayType != null)
        this._SNMPTrapPort2 = this.GatewayType.DefaultSNMPTrapPort;
      return this._SNMPTrapPort2;
    }
    set
    {
      double snmpTrapPort2 = (double) this.SNMPTrapPort2;
      this._SNMPTrapPort2 = value;
      if (snmpTrapPort2 == (double) this.SNMPTrapPort2)
        return;
      this.OnPropertyChanged(nameof (SNMPTrapPort2));
    }
  }

  [DBProp("SNMPTrapPort3")]
  public int SNMPTrapPort3
  {
    get
    {
      if (this._SNMPTrapPort3 == int.MinValue && this.GatewayType != null)
        this._SNMPTrapPort3 = this.GatewayType.DefaultSNMPTrapPort;
      return this._SNMPTrapPort3;
    }
    set
    {
      double snmpTrapPort3 = (double) this.SNMPTrapPort3;
      this._SNMPTrapPort3 = value;
      if (snmpTrapPort3 == (double) this.SNMPTrapPort3)
        return;
      this.OnPropertyChanged(nameof (SNMPTrapPort3));
    }
  }

  [DBProp("SNMPTrapPort4")]
  public int SNMPTrapPort4
  {
    get
    {
      if (this._SNMPTrapPort4 == int.MinValue && this.GatewayType != null)
        this._SNMPTrapPort4 = this.GatewayType.DefaultSNMPTrapPort;
      return this._SNMPTrapPort4;
    }
    set
    {
      double snmpTrapPort4 = (double) this.SNMPTrapPort4;
      this._SNMPTrapPort4 = value;
      if (snmpTrapPort4 == (double) this.SNMPTrapPort4)
        return;
      this.OnPropertyChanged(nameof (SNMPTrapPort4));
    }
  }

  [DBProp("SNMPCommunityString", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SNMPCommunityString
  {
    get => this._SNMPCommunityString;
    set
    {
      string snmpCommunityString = this._SNMPCommunityString;
      this._SNMPCommunityString = value;
      if (!(snmpCommunityString != this._SNMPCommunityString))
        return;
      this.OnPropertyChanged(nameof (SNMPCommunityString));
    }
  }

  [DBProp("RequestConfigPage")]
  public bool RequestConfigPage
  {
    get => this._RequestConfigPage;
    set
    {
      bool requestConfigPage = this.RequestConfigPage;
      this._RequestConfigPage = value;
      if (requestConfigPage == this.RequestConfigPage)
        return;
      this.OnPropertyChanged(nameof (RequestConfigPage));
    }
  }

  [DBProp("GPSReportInterval")]
  public double GPSReportInterval
  {
    get
    {
      if (this._GPSReportInterval == double.MinValue && this.GatewayType != null)
        this._GPSReportInterval = this.GatewayType.DefaultGPSReportInterval;
      return this._GPSReportInterval;
    }
    set
    {
      double gpsReportInterval = this.GPSReportInterval;
      this._GPSReportInterval = value;
      if (gpsReportInterval == this.GPSReportInterval)
        return;
      this.OnPropertyChanged(nameof (GPSReportInterval));
    }
  }

  [DBForeignKey("PowerSource", "PowerSourceID")]
  [DBProp("PowerSourceID", AllowNull = true)]
  public long PowerSourceID
  {
    get => this._PowerSourceID;
    set
    {
      long powerSourceId = this.PowerSourceID;
      this._PowerSourceID = value;
      if (powerSourceId == this.PowerSourceID)
        return;
      this.OnPropertyChanged(nameof (PowerSourceID));
    }
  }

  public PowerSource PowerSource
  {
    get
    {
      try
      {
        if (this._PowerSource == null)
          this._PowerSource = PowerSource.Load(this.PowerSourceID);
        return this._PowerSource;
      }
      catch (Exception ex)
      {
        ExceptionLog.Log(ex);
        return (PowerSource) null;
      }
    }
  }

  [DBProp("GatewayPowerMode", AllowNull = false, DefaultValue = 0)]
  public eGatewayPowerMode GatewayPowerMode
  {
    get => this._GatewayPowerMode;
    set
    {
      eGatewayPowerMode gatewayPowerMode = this.GatewayPowerMode;
      this._GatewayPowerMode = value;
      if (gatewayPowerMode == this.GatewayPowerMode)
        return;
      this.OnPropertyChanged(nameof (GatewayPowerMode));
    }
  }

  [DBProp("SensorhoodConfigDirty", AllowNull = false, DefaultValue = false)]
  public bool SensorhoodConfigDirty
  {
    get => this._SensorhoodConfigDirty;
    set
    {
      bool sensorhoodConfigDirty = this.SensorhoodConfigDirty;
      this._SensorhoodConfigDirty = value;
      if (sensorhoodConfigDirty == this.SensorhoodConfigDirty)
        return;
      this.OnPropertyChanged(nameof (SensorhoodConfigDirty));
    }
  }

  [DBProp("SensorhoodID", AllowNull = true)]
  public long SensorhoodID
  {
    get => this._SensorhoodID;
    set
    {
      long sensorhoodId = this.SensorhoodID;
      this._SensorhoodID = value;
      if (sensorhoodId == this.SensorhoodID || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (SensorhoodID));
    }
  }

  [DBProp("SensorhoodTransmitions", AllowNull = true)]
  public int SensorhoodTransmitions
  {
    get => this._SensorhoodTransmitions == int.MinValue ? 10 : this._SensorhoodTransmitions;
    set
    {
      int sensorhoodTransmitions = this.SensorhoodTransmitions;
      this._SensorhoodTransmitions = value;
      if (sensorhoodTransmitions == this.SensorhoodTransmitions || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (SensorhoodTransmitions));
    }
  }

  [DBProp("isEnterpriseHost", AllowNull = false, DefaultValue = 0)]
  public bool isEnterpriseHost
  {
    get => this._isEnterpriseHost;
    set
    {
      bool isEnterpriseHost = this.isEnterpriseHost;
      this._isEnterpriseHost = value;
      if (isEnterpriseHost == this.isEnterpriseHost)
        return;
      this.OnPropertyChanged(nameof (isEnterpriseHost));
    }
  }

  [DBProp("UrgentTraffic", AllowNull = false, DefaultValue = false)]
  public bool UrgentTraffic
  {
    get => this._UrgentTraffic;
    set
    {
      bool urgentTraffic = this.UrgentTraffic;
      this._UrgentTraffic = value;
      if (urgentTraffic == this.UrgentTraffic)
        return;
      this.OnPropertyChanged(nameof (UrgentTraffic));
    }
  }

  [DBProp("DecryptFailureDate")]
  public DateTime DecryptFailureDate
  {
    get => this._DecryptFailureDate;
    set
    {
      DateTime decryptFailureDate = this.DecryptFailureDate;
      this._DecryptFailureDate = value;
      if (!(decryptFailureDate != this.DecryptFailureDate))
        return;
      this.OnPropertyChanged(nameof (DecryptFailureDate));
    }
  }

  [DBProp("SendWatchDogTest")]
  public bool SendWatchDogTest
  {
    get => this._SendWatchDogTest;
    set
    {
      bool sendWatchDogTest = this.SendWatchDogTest;
      this._SendWatchDogTest = value;
      if (sendWatchDogTest == this.SendWatchDogTest)
        return;
      this.OnPropertyChanged(nameof (SendWatchDogTest));
    }
  }

  [DBProp("SendUpdateModuleCommand", DefaultValue = false, AllowNull = false)]
  public bool SendUpdateModuleCommand
  {
    get => this._SendUpdateModuleCommand;
    set
    {
      bool updateModuleCommand = this.SendUpdateModuleCommand;
      this._SendUpdateModuleCommand = value;
      if (updateModuleCommand == this.SendUpdateModuleCommand)
        return;
      this.OnPropertyChanged(nameof (SendUpdateModuleCommand));
    }
  }

  [DBProp("resumeNotificationDate", AllowNull = true)]
  public DateTime resumeNotificationDate
  {
    get => this._resumeNotificationDate;
    set
    {
      DateTime notificationDate = this.resumeNotificationDate;
      this._resumeNotificationDate = value;
      if (!(notificationDate != this.resumeNotificationDate))
        return;
      this.OnPropertyChanged(nameof (resumeNotificationDate));
    }
  }

  public bool isPaused() => this.resumeNotificationDate > DateTime.UtcNow;

  [DBProp("IsUnlocked", AllowNull = true)]
  public bool IsUnlocked
  {
    get => this._IsUnlocked;
    set
    {
      bool isUnlocked = this._IsUnlocked;
      this._IsUnlocked = value;
      if (isUnlocked == this.IsUnlocked)
        return;
      this.OnPropertyChanged(nameof (IsUnlocked));
    }
  }

  [DBProp("SecurityKey", AllowNull = true)]
  public byte[] SecurityKey
  {
    get
    {
      if (this._SecurityKey == null || this._SecurityKey.Length != 16 /*0x10*/)
        this._SecurityKey = Gateway.DefaultSecurityKey();
      return this._SecurityKey;
    }
    set
    {
      bool flag = false;
      if (!this.EqualToSecurityKey(value) && !this.SuppressPropertyChangedEvent)
        flag = true;
      this._SecurityKey = value;
      if (!flag)
        return;
      this.OnPropertyChanged(nameof (SecurityKey));
    }
  }

  public string SecurityKeyString
  {
    get => this.SecurityKey.FormatBytesToStringArray();
    set
    {
      this.SecurityKey = value.FormatStringToByteArray().Length == 16 /*0x10*/ ? value.FormatStringToByteArray() : throw new ArgumentException("Invalid Security Key Length");
    }
  }

  public static byte[] DefaultSecurityKey() => new byte[16 /*0x10*/];

  [DBProp("RadioFirmwareUpdateID")]
  [DBForeignKey("BSNFirmware", "BSNFirmwareID")]
  public long RadioFirmwareUpdateID
  {
    get => this._RadioFirmwareUpdateID;
    set
    {
      long firmwareUpdateId = this.RadioFirmwareUpdateID;
      this._RadioFirmwareUpdateID = value;
      if (firmwareUpdateId == this.RadioFirmwareUpdateID)
        return;
      this.OnPropertyChanged(nameof (RadioFirmwareUpdateID));
    }
  }

  [DBProp("UpdateRadioFirmware", DefaultValue = false)]
  public bool UpdateRadioFirmware
  {
    get => this._UpdateRadioFirmware;
    set
    {
      bool updateRadioFirmware = this.UpdateRadioFirmware;
      this._UpdateRadioFirmware = value;
      if (updateRadioFirmware == this.UpdateRadioFirmware)
        return;
      this.OnPropertyChanged(nameof (UpdateRadioFirmware));
    }
  }

  [DBProp("RadioFirmwareUpdateInProgress")]
  public bool RadioFirmwareUpdateInProgress
  {
    get => this._RadioFirmwareUpdateInProgress;
    set
    {
      bool updateInProgress = this.RadioFirmwareUpdateInProgress;
      this._RadioFirmwareUpdateInProgress = value;
      if (updateInProgress == this.RadioFirmwareUpdateInProgress)
        return;
      this.OnPropertyChanged(nameof (RadioFirmwareUpdateInProgress));
    }
  }

  [DBProp("GenerationType", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string GenerationType
  {
    get => this._GenerationType;
    set
    {
      string generationType = this.GenerationType;
      this._GenerationType = value != null ? value : string.Empty;
      if (!(generationType != this.GenerationType))
        return;
      this.OnPropertyChanged(nameof (GenerationType));
    }
  }

  [DBProp("StartDate")]
  public DateTime StartDate
  {
    get => this._StartDate;
    set
    {
      DateTime startDate = this.StartDate;
      this._StartDate = value;
      if (!(startDate != this.StartDate))
        return;
      this.OnPropertyChanged(nameof (StartDate));
    }
  }

  [DBProp("Make", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Make
  {
    get => this._Make;
    set
    {
      string make = this.Make;
      this._Make = value;
      if (!(make != this.Make))
        return;
      this.OnPropertyChanged(nameof (Make));
    }
  }

  [DBProp("Model", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Model
  {
    get => this._Model;
    set
    {
      string model = this.Model;
      this._Model = value;
      if (!(model != this.Model))
        return;
      this.OnPropertyChanged(nameof (Model));
    }
  }

  [DBProp("SerialNumber", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SerialNumber
  {
    get => this._SerialNumber;
    set
    {
      string serialNumber = this.SerialNumber;
      this._SerialNumber = value;
      if (!(serialNumber != this.SerialNumber))
        return;
      this.OnPropertyChanged(nameof (SerialNumber));
    }
  }

  [DBProp("Location", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Location
  {
    get => this._Location;
    set
    {
      string location = this.Location;
      this._Location = value;
      if (!(location != this.Location))
        return;
      this.OnPropertyChanged(nameof (Location));
    }
  }

  [DBProp("Description", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set
    {
      string description = this.Description;
      this._Description = value;
      if (!(description != this.Description))
        return;
      this.OnPropertyChanged(nameof (Description));
    }
  }

  [DBProp("Note", MaxLength = 2000, AllowNull = true)]
  public string Note
  {
    get => this._Note;
    set
    {
      string note = this.Note;
      this._Note = value;
      if (!(note != this.Note))
        return;
      this.OnPropertyChanged(nameof (Note));
    }
  }

  [DBProp("GatewayCommunicationPreference", AllowNull = false, DefaultValue = 0)]
  public eGatewayCommunicationPreference GatewayCommunicationPreference
  {
    get => this._GatewayCommunicationPreference;
    set
    {
      eGatewayCommunicationPreference communicationPreference = this._GatewayCommunicationPreference;
      this._GatewayCommunicationPreference = value;
      if (communicationPreference == this._GatewayCommunicationPreference)
        return;
      this.OnPropertyChanged(nameof (GatewayCommunicationPreference));
    }
  }

  [DBProp("SystemOptions")]
  public int SystemOptions
  {
    get => this._SystemOptions;
    set
    {
      int systemOptions = this.SystemOptions;
      this._SystemOptions = value;
      if (systemOptions == this.SystemOptions)
        return;
      this.OnPropertyChanged(nameof (SystemOptions));
    }
  }

  [DBProp("MQTTPort")]
  public int MQTTPort
  {
    get
    {
      if (this._MQTTPort == int.MinValue)
        this._MQTTPort = 1883;
      return this._MQTTPort;
    }
    set
    {
      int mqttPort = this.MQTTPort;
      this._MQTTPort = value;
      if (mqttPort == this.MQTTPort)
        return;
      this.OnPropertyChanged(nameof (MQTTPort));
    }
  }

  [DBProp("MQTTBrokerAddress", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string MQTTBrokerAddress
  {
    get
    {
      if (string.IsNullOrWhiteSpace(this._MQTTBrokerAddress))
        this._MQTTBrokerAddress = string.Empty;
      return this._MQTTBrokerAddress;
    }
    set
    {
      string mqttBrokerAddress = this.MQTTBrokerAddress;
      this._MQTTBrokerAddress = value != null ? value : string.Empty;
      if (!(mqttBrokerAddress != this.MQTTBrokerAddress))
        return;
      this.OnPropertyChanged(nameof (MQTTBrokerAddress));
    }
  }

  [DBProp("MQTTUser", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string MQTTUser
  {
    get
    {
      if (string.IsNullOrWhiteSpace(this._MQTTUser))
        this._MQTTUser = string.Empty;
      return this._MQTTUser;
    }
    set
    {
      string mqttUser = this.MQTTUser;
      this._MQTTUser = value != null ? value : string.Empty;
      if (!(mqttUser != this.MQTTUser))
        return;
      this.OnPropertyChanged(nameof (MQTTUser));
    }
  }

  [DBProp("MQTTPassword", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string MQTTPassword
  {
    get
    {
      if (string.IsNullOrWhiteSpace(this._MQTTPassword))
        this._MQTTPassword = string.Empty;
      return this._MQTTPassword;
    }
    set
    {
      string mqttPassword = this.MQTTPassword;
      this._MQTTPassword = value != null ? value : string.Empty;
      if (!(mqttPassword != this.MQTTPassword))
        return;
      this.OnPropertyChanged(nameof (MQTTPassword));
    }
  }

  [DBProp("MQTTClientID", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string MQTTClientID
  {
    get
    {
      if (string.IsNullOrWhiteSpace(this._MQTTClientID))
        this._MQTTClientID = string.Empty;
      return this._MQTTClientID;
    }
    set
    {
      string mqttClientId = this.MQTTClientID;
      this._MQTTClientID = value != null ? value : string.Empty;
      if (!(mqttClientId != this.MQTTClientID))
        return;
      this.OnPropertyChanged(nameof (MQTTClientID));
    }
  }

  [DBProp("MQTTClientTopic", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string MQTTClientTopic
  {
    get
    {
      if (string.IsNullOrWhiteSpace(this._MQTTClientTopic))
        this._MQTTClientTopic = string.Empty;
      return this._MQTTClientTopic;
    }
    set
    {
      string mqttClientTopic = this.MQTTClientTopic;
      this._MQTTClientTopic = value != null ? value : string.Empty;
      if (!(mqttClientTopic != this.MQTTClientTopic))
        return;
      this.OnPropertyChanged(nameof (MQTTClientTopic));
    }
  }

  [DBProp("MQTTPublicationInterval")]
  public int MQTTPublicationInterval
  {
    get
    {
      if (this._MQTTPublicationInterval == int.MinValue)
        this._MQTTPublicationInterval = 300;
      return this._MQTTPublicationInterval;
    }
    set
    {
      int publicationInterval = this.MQTTPublicationInterval;
      this._MQTTPublicationInterval = value;
      if (publicationInterval == this.MQTTPublicationInterval)
        return;
      this.OnPropertyChanged(nameof (MQTTPublicationInterval));
    }
  }

  [DBProp("MQTTKeepAlive")]
  public int MQTTKeepAlive
  {
    get
    {
      if (this._MQTTKeepAlive == int.MinValue)
        this._MQTTKeepAlive = 60;
      return this._MQTTKeepAlive;
    }
    set
    {
      int mqttKeepAlive = this.MQTTKeepAlive;
      this._MQTTKeepAlive = value;
      if (mqttKeepAlive == this.MQTTKeepAlive)
        return;
      this.OnPropertyChanged(nameof (MQTTKeepAlive));
    }
  }

  [DBProp("MQTTAckTimeout")]
  public int MQTTAckTimeout
  {
    get
    {
      if (this._MQTTAckTimeout == int.MinValue)
        this._MQTTAckTimeout = 30;
      return this._MQTTAckTimeout;
    }
    set
    {
      int mqttAckTimeout = this.MQTTAckTimeout;
      this._MQTTAckTimeout = value;
      if (mqttAckTimeout == this.MQTTAckTimeout)
        return;
      this.OnPropertyChanged(nameof (MQTTAckTimeout));
    }
  }

  [DBProp("MQTTQueueFlushLimit")]
  public int MQTTQueueFlushLimit
  {
    get
    {
      if (this._MQTTQueueFlushLimit == int.MinValue)
        this._MQTTQueueFlushLimit = 50;
      return this._MQTTQueueFlushLimit;
    }
    set
    {
      int mqttQueueFlushLimit = this.MQTTQueueFlushLimit;
      this._MQTTQueueFlushLimit = value;
      if (mqttQueueFlushLimit == this.MQTTQueueFlushLimit)
        return;
      this.OnPropertyChanged(nameof (MQTTQueueFlushLimit));
    }
  }

  [DBProp("MQTTBehaviorFlags")]
  public int MQTTBehaviorFlags
  {
    get
    {
      if (this._MQTTBehaviorFlags == int.MinValue)
        this._MQTTBehaviorFlags = 0;
      return this._MQTTBehaviorFlags;
    }
    set
    {
      int mqttBehaviorFlags = this.MQTTBehaviorFlags;
      this._MQTTBehaviorFlags = value;
      if (mqttBehaviorFlags == this.MQTTBehaviorFlags)
        return;
      this.OnPropertyChanged(nameof (MQTTBehaviorFlags));
    }
  }

  [DBProp("MQTTInterfaceActive")]
  public bool MQTTInterfaceActive
  {
    get => this._MQTTInterfaceActive;
    set
    {
      bool mqttInterfaceActive = this.MQTTInterfaceActive;
      this._MQTTInterfaceActive = value;
      if (mqttInterfaceActive == this.MQTTInterfaceActive)
        return;
      this.OnPropertyChanged(nameof (MQTTInterfaceActive));
    }
  }

  [DBProp("NTPInterfaceActive")]
  public bool NTPInterfaceActive
  {
    get => this._NTPInterfaceActive;
    set
    {
      bool ntpInterfaceActive = this.NTPInterfaceActive;
      this._NTPInterfaceActive = value;
      if (ntpInterfaceActive == this.NTPInterfaceActive)
        return;
      this.OnPropertyChanged(nameof (NTPInterfaceActive));
    }
  }

  [DBProp("NTPServerIP", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string NTPServerIP
  {
    get => this._NTPServerIP;
    set
    {
      string ntpServerIp = this.NTPServerIP;
      this._NTPServerIP = value;
      if (!(ntpServerIp != this.NTPServerIP))
        return;
      this.OnPropertyChanged(nameof (NTPServerIP));
    }
  }

  [DBProp("NTPMinSampleRate")]
  public double NTPMinSampleRate
  {
    get
    {
      if (this._NTPMinSampleRate < 0.0)
        this._NTPMinSampleRate = 0.0;
      return this._NTPMinSampleRate;
    }
    set
    {
      double ntpMinSampleRate = this.NTPMinSampleRate;
      this._NTPMinSampleRate = value;
      if (ntpMinSampleRate == this.NTPMinSampleRate)
        return;
      this.OnPropertyChanged(nameof (NTPMinSampleRate));
    }
  }

  [DBProp("HTTPInterfaceActive")]
  public bool HTTPInterfaceActive
  {
    get => this._HTTPInterfaceActive;
    set
    {
      bool httpInterfaceActive = this.HTTPInterfaceActive;
      this._HTTPInterfaceActive = value;
      if (httpInterfaceActive == this.HTTPInterfaceActive)
        return;
      this.OnPropertyChanged(nameof (HTTPInterfaceActive));
    }
  }

  [DBProp("HTTPServiceTimeout")]
  public double HTTPServiceTimeout
  {
    get
    {
      if (this._HTTPServiceTimeout < 0.0)
        this._HTTPServiceTimeout = 0.0;
      return this._HTTPServiceTimeout;
    }
    set
    {
      double httpServiceTimeout = this.HTTPServiceTimeout;
      this._HTTPServiceTimeout = value;
      if (httpServiceTimeout == this.HTTPServiceTimeout)
        return;
      this.OnPropertyChanged(nameof (HTTPServiceTimeout));
    }
  }

  [DBProp("BacnetInterfaceActive")]
  public bool BacnetInterfaceActive
  {
    get => this._BacnetInterfaceActive;
    set
    {
      bool bacnetInterfaceActive = this.BacnetInterfaceActive;
      this._BacnetInterfaceActive = value;
      if (bacnetInterfaceActive == this.BacnetInterfaceActive)
        return;
      this.OnPropertyChanged(nameof (BacnetInterfaceActive));
    }
  }

  [DBProp("DisableNetworkOnServerError")]
  public bool DisableNetworkOnServerError
  {
    get => this._DisableNetworkOnServerError;
    set
    {
      bool networkOnServerError = this.DisableNetworkOnServerError;
      this._DisableNetworkOnServerError = value;
      if (networkOnServerError == this.DisableNetworkOnServerError)
        return;
      this.OnPropertyChanged(nameof (DisableNetworkOnServerError));
    }
  }

  [DBProp("ResetInterval")]
  public int ResetInterval
  {
    get => this._ResetInterval;
    set
    {
      int resetInterval = this.ResetInterval;
      if (value >= 0)
        this._ResetInterval = value;
      if (resetInterval == this.ResetInterval)
        return;
      this.OnPropertyChanged(nameof (ResetInterval));
    }
  }

  [DBProp("SKU", MaxLength = 75, AllowNull = true)]
  public string SKU
  {
    get => this._SKU == null ? string.Empty : this._SKU;
    set
    {
      string sku = this.SKU;
      this._SKU = value;
      if (!(sku != this.SKU))
        return;
      this.OnPropertyChanged(nameof (SKU));
    }
  }

  [DBProp("M1BandMask")]
  public long M1BandMask
  {
    get
    {
      if (this._M1BandMask < 0L || this._M1BandMask > (long) uint.MaxValue)
        this._M1BandMask = 0L;
      return this._M1BandMask;
    }
    set
    {
      long m1BandMask = this.M1BandMask;
      this._M1BandMask = value;
      if (m1BandMask == this.M1BandMask)
        return;
      this.OnPropertyChanged(nameof (M1BandMask));
    }
  }

  [DBProp("NB1BandMask")]
  public long NB1BandMask
  {
    get
    {
      if (this._NB1BandMask < 0L || this._NB1BandMask > (long) uint.MaxValue)
        this._NB1BandMask = 0L;
      return this._NB1BandMask;
    }
    set
    {
      long nb1BandMask = this.NB1BandMask;
      this._NB1BandMask = value;
      if (nb1BandMask == this.NB1BandMask)
        return;
      this.OnPropertyChanged(nameof (NB1BandMask));
    }
  }

  [DBProp("UMNOProf")]
  public int UMNOProf
  {
    get
    {
      if (this._UMNOProf < 0 || this._UMNOProf > (int) byte.MaxValue)
        this._UMNOProf = 1;
      return this._UMNOProf;
    }
    set
    {
      int umnoProf = this.UMNOProf;
      this._UMNOProf = value;
      if (umnoProf == this.UMNOProf)
        return;
      this.OnPropertyChanged(nameof (UMNOProf));
    }
  }

  [DBProp("SIMAuthType")]
  public int SIMAuthType
  {
    get
    {
      if (this._SIMAuthType < 0 || this._SIMAuthType > 2)
        this._SIMAuthType = 0;
      return this._SIMAuthType;
    }
    set
    {
      int simAuthType = this.SIMAuthType;
      this._SIMAuthType = value;
      if (simAuthType == this.SIMAuthType)
        return;
      this.OnPropertyChanged(nameof (SIMAuthType));
    }
  }

  [DBProp("OTARequestActive")]
  public bool OTARequestActive
  {
    get => this._OTARequestActive;
    set
    {
      bool otaRequestActive = this.OTARequestActive;
      this._OTARequestActive = value;
      if (otaRequestActive == this.OTARequestActive)
        return;
      this.OnPropertyChanged(nameof (OTARequestActive));
    }
  }

  [DBProp("IsGPSUnlocked")]
  public bool IsGPSUnlocked
  {
    get => this._IsGPSUnlocked;
    set
    {
      bool isGpsUnlocked = this.IsGPSUnlocked;
      this._IsGPSUnlocked = value;
      if (isGpsUnlocked == this.IsGPSUnlocked)
        return;
      this.OnPropertyChanged(nameof (IsGPSUnlocked));
    }
  }

  [DBProp("GPSPing")]
  public bool GPSPing
  {
    get => this._GPSPing;
    set
    {
      bool gpsPing = this.GPSPing;
      this._GPSPing = value;
      if (gpsPing == this.GPSPing)
        return;
      this.OnPropertyChanged(nameof (GPSPing));
    }
  }

  [DBProp("WarrantyStartDate")]
  public DateTime WarrantyStartDate
  {
    get => this._WarrantyStartDate;
    set
    {
      DateTime warrantyStartDate = this.WarrantyStartDate;
      this._WarrantyStartDate = value;
      if (!(warrantyStartDate != this.WarrantyStartDate))
        return;
      this.OnPropertyChanged(nameof (WarrantyStartDate));
    }
  }

  [DBProp("hasActionControlCommand")]
  public bool hasActionControlCommand
  {
    get => this._hasActionControlCommand;
    set
    {
      bool actionControlCommand = this.hasActionControlCommand;
      this._hasActionControlCommand = value;
      if (actionControlCommand == this.hasActionControlCommand)
        return;
      this.OnPropertyChanged(nameof (hasActionControlCommand));
    }
  }

  [DBProp("AutoConfigActionCommandTime", DefaultValue = 5)]
  public int AutoConfigActionCommandTime
  {
    get => this._AutoConfigActionCommandTime;
    set
    {
      int actionCommandTime = this.AutoConfigActionCommandTime;
      this._AutoConfigActionCommandTime = value;
      if (actionCommandTime == this.AutoConfigActionCommandTime)
        return;
      this.OnPropertyChanged(nameof (AutoConfigActionCommandTime));
    }
  }

  [DBProp("SendUnlockRequest")]
  public bool SendUnlockRequest
  {
    get => this._SendUnlockRequest;
    set
    {
      bool sendUnlockRequest = this.SendUnlockRequest;
      this._SendUnlockRequest = value;
      if (sendUnlockRequest == this.SendUnlockRequest)
        return;
      this.OnPropertyChanged(nameof (SendUnlockRequest));
    }
  }

  [DBProp("SendGPSUnlockRequest")]
  public bool SendGPSUnlockRequest
  {
    get => this._SendGPSUnlockRequest;
    set
    {
      bool gpsUnlockRequest = this.SendGPSUnlockRequest;
      this._SendGPSUnlockRequest = value;
      if (gpsUnlockRequest == this.SendGPSUnlockRequest)
        return;
      this.OnPropertyChanged(nameof (SendGPSUnlockRequest));
    }
  }

  public bool EqualToSecurityKey(byte[] value)
  {
    try
    {
      if (value == null)
        return this.EqualToSecurityKey(Gateway.DefaultSecurityKey());
      for (int index = 0; index < 16 /*0x10*/; ++index)
      {
        if ((int) value[index] != (int) this.SecurityKey[index])
          return false;
      }
      return true;
    }
    catch
    {
      return false;
    }
  }

  public int Battery
  {
    get
    {
      if (this.CurrentPower > 5 && this.PowerSource != null)
      {
        try
        {
          return this.PowerSource.Percent(((int) Convert.ToUInt16(this.CurrentPower) & (int) short.MaxValue).ToDouble() / 1000.0).ToInt();
        }
        catch (Exception ex)
        {
          ex.Log("Gateway.Battery_get: GatewayID" + this.GatewayID.ToString());
        }
      }
      return 101;
    }
  }

  public string GatewayTypeIcon
  {
    get
    {
      string gatewayTypeIcon = "icon-ethernet-b";
      long num = this.GatewayTypeID - 1L;
      if ((ulong) num <= 37UL)
      {
        switch ((uint) num)
        {
          case 0:
          case 1:
          case 27:
            gatewayTypeIcon = "icon-usb-b";
            break;
          case 2:
          case 3:
          case 4:
          case 5:
          case 6:
          case 30:
          case 32 /*0x20*/:
          case 34:
            gatewayTypeIcon = "icon-ethernet-b";
            break;
          case 7:
          case 8:
          case 12:
          case 13:
          case 18:
          case 19:
          case 20:
            gatewayTypeIcon = "icon-iMetrik";
            break;
          case 9:
          case 10:
          case 37:
            gatewayTypeIcon = "icon-wi-fi";
            break;
          case 16 /*0x10*/:
          case 17:
          case 21:
          case 22:
          case 23:
          case 24:
          case 25:
          case 26:
          case 29:
          case 31 /*0x1F*/:
          case 35:
            gatewayTypeIcon = "icon-cellular";
            break;
        }
      }
      return gatewayTypeIcon;
    }
  }

  public GatewayWIFICredential WifiCredential(int index)
  {
    GatewayWIFICredential gatewayWifiCredential = GatewayWIFICredential.Load(this.GatewayID, index);
    if (gatewayWifiCredential == null)
    {
      gatewayWifiCredential = new GatewayWIFICredential();
      gatewayWifiCredential.GatewayID = this.GatewayID;
      gatewayWifiCredential.CredentialIndex = index;
    }
    return gatewayWifiCredential;
  }

  public eSensorStatus Status
  {
    get
    {
      if (this.LastCommunicationDate == DateTime.MinValue || this.LastCommunicationDate == "2099-01-01".ToDateTime() || this.ReportInterval > 0.0 && this.LastCommunicationDate.AddMinutes(this.ReportInterval * 2.0 + 1.0) < DateTime.UtcNow || this.ReportInterval == 0.0 && this.LastCommunicationDate.AddHours(24.0) < DateTime.UtcNow)
        return eSensorStatus.Offline;
      return this.CurrentPower > 5 && this.Battery < 101 && (this.CurrentPower & 32768 /*0x8000*/) == 0 ? eSensorStatus.Warning : eSensorStatus.OK;
    }
  }

  public static Gateway Load(long ID)
  {
    Gateway result = new Monnit.Data.Gateway.LoadByID(ID).Result;
    if (result != null)
      result.SuppressPropertyChangedEvent = false;
    return result;
  }

  public override void Delete()
  {
    this.IsDeleted = true;
    this.Save();
    foreach (Notification notification in GatewayNotification.LoadByGatewayID(this.GatewayID))
      notification.RemoveGateway(this);
  }

  public static void DeleteAncillaryObjects(long gatewayID)
  {
    Monnit.Data.Gateway.DeleteAncillaryObjects ancillaryObjects = new Monnit.Data.Gateway.DeleteAncillaryObjects(gatewayID);
  }

  public static List<Gateway> LoadByCSNetID(long csNetID)
  {
    List<Gateway> result = new Monnit.Data.Gateway.LoadByCSNetID(csNetID).Result;
    foreach (Gateway gateway in result)
      gateway.SuppressPropertyChangedEvent = false;
    return result;
  }

  public static List<Gateway> LoadByAccountID(long accountID)
  {
    List<Gateway> result = new Monnit.Data.Gateway.LoadByAccountID(accountID).Result;
    foreach (Gateway gateway in result)
      gateway.SuppressPropertyChangedEvent = false;
    return result;
  }

  public static Gateway LoadByMAC(string MAC)
  {
    Gateway result = new Monnit.Data.Gateway.LoadByMAC(MAC).Result;
    if (result != null)
      result.SuppressPropertyChangedEvent = false;
    return result;
  }

  public static Gateway LoadBySensorID(long sensorID)
  {
    Gateway gateway = new Monnit.Data.Gateway.LoadBySensorID(sensorID).Result.FirstOrDefault<Gateway>();
    if (gateway != null)
      gateway.SuppressPropertyChangedEvent = false;
    return gateway;
  }

  public static bool HasUrgentTraffic(long gatewayID)
  {
    return new Monnit.Data.Gateway.HasUrgentTraffic(gatewayID).Result;
  }

  public static List<Gateway> LoadByAccountIDForUnlockGpsList(long accountID)
  {
    return new Monnit.Data.Gateway.LoadByAccountIDForUnlockGpsList(accountID).Result;
  }

  public void ForceInsert()
  {
    Monnit.Data.Gateway.ForceInsert forceInsert = new Monnit.Data.Gateway.ForceInsert(this);
  }

  public void MarkClean(bool autoSave)
  {
    this.IsDirty = false;
    if (!autoSave)
      return;
    this.Save();
  }

  public void ClearResetNetworkRequest()
  {
    this.SendResetNetworkRequest = false;
    this.Save();
  }

  public bool SuppressPropertyChangedEvent
  {
    get => this._SuppressPropertyChangedEvent;
    set => this._SuppressPropertyChangedEvent = value;
  }

  public event PropertyChangedEventHandler PropertyChanged;

  public void OnPropertyChanged(string propertyName)
  {
    if (this.SuppressPropertyChangedEvent)
      return;
    this._SaveFullObject = true;
    try
    {
      if (this.PropertyChanged != null)
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    if (propertyName == "ReportInterval" && this.GatewayTypeID != 35L)
      this.IsDirty = true;
    if (propertyName == "PollInterval")
      this.IsDirty = true;
    if (propertyName == "ServerHostAddress")
      this.IsDirty = true;
    if (propertyName == "isEnterpriseHost")
      this.IsDirty = true;
    if (propertyName == "Port")
      this.IsDirty = true;
    if (propertyName == "ServerHostAddress2")
      this.IsDirty = true;
    if (propertyName == "Port2")
      this.IsDirty = true;
    if (propertyName == "ChannelMask")
      this.IsDirty = true;
    if (propertyName == "NetworkIDFilter")
      this.IsDirty = true;
    if (propertyName == "GatewayIP")
      this.IsDirty = true;
    if (propertyName == "GatewaySubnet")
      this.IsDirty = true;
    if (propertyName == "DefaultRouterIP")
      this.IsDirty = true;
    if (propertyName == "GatewayDNS")
      this.IsDirty = true;
    if (propertyName == "SecondaryDNS")
      this.IsDirty = true;
    if (propertyName == "ObserveAware")
      this.IsDirty = true;
    if (propertyName == "DisableNetworkOnServerError")
      this.IsDirty = true;
    if (propertyName == "NetworkListInterval")
      this.IsDirty = true;
    if (propertyName == "UMNOProf")
      this.IsDirty = true;
    if (propertyName == "CellAPNName")
      this.IsDirty = true;
    if (propertyName == "SIMAuthType")
      this.IsDirty = true;
    if (propertyName == "Username")
      this.IsDirty = true;
    if (propertyName == "Password")
      this.IsDirty = true;
    if (propertyName == "M1BandMask")
      this.IsDirty = true;
    if (propertyName == "NB1BandMask")
      this.IsDirty = true;
    if (propertyName == "ErrorHeartbeat")
      this.IsDirty = true;
    if (propertyName == "LedActiveTime")
      this.IsDirty = true;
    if (propertyName == "SingleQueueExpiration")
      this.IsDirty = true;
    if (propertyName == "ServerInterfaceActive")
      this.IsDirty = true;
    if (propertyName == "RealTimeInterfaceActive")
      this.IsDirty = true;
    if (propertyName == "ModbusInterfaceActive")
      this.IsDirty = true;
    if (propertyName == "RealTimeInterfaceTimeout")
      this.IsDirty = true;
    if (propertyName == "RealTimeInterfacePort")
      this.IsDirty = true;
    if (propertyName == "ModbusInterfaceTimeout")
      this.IsDirty = true;
    if (propertyName == "ModbusInterfacePort")
      this.IsDirty = true;
    if (propertyName == "SNMPInterface1Active")
      this.IsDirty = true;
    if (propertyName == "SNMPInterface2Active")
      this.IsDirty = true;
    if (propertyName == "SNMPInterface3Active")
      this.IsDirty = true;
    if (propertyName == "SNMPInterface4Active")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfaceAddress1")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfaceAddress2")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfaceAddress3")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfaceAddress4")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfacePort1")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfacePort2")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfacePort3")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfacePort4")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfaceTrapPort1")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfaceTrapPort2")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfaceTrapPort3")
      this.IsDirty = true;
    if (propertyName == "SNMPInterfaceTrapPort4")
      this.IsDirty = true;
    if (propertyName == "GPSReportInterval")
      this.IsDirty = true;
    if (propertyName == "GatewayPowerMode")
      this.IsDirty = true;
    if (propertyName == "SecurityKey")
      this.IsDirty = true;
    if (propertyName == "GatewayCommunicationPreference")
      this.IsDirty = true;
    if (propertyName == "SNMPCommunityString")
      this.IsDirty = true;
    if (propertyName == "LocalConfigTimeout")
      this.IsDirty = true;
    if (propertyName == "LocalConfigPort")
      this.IsDirty = true;
    if (propertyName == "HTTPInterfaceActive")
      this.IsDirty = true;
    if (propertyName == "HTTPServiceTimeout")
      this.IsDirty = true;
    if (propertyName == "ResetInterval")
      this.IsDirty = true;
    if (propertyName == "NTPServerIP")
      this.IsDirty = true;
    if (propertyName == "NTPMinSampleRate")
      this.IsDirty = true;
    if (propertyName == "SNMPTrapPort1")
      this.IsDirty = true;
    if (propertyName == "SNMPTrapPort2")
      this.IsDirty = true;
    if (propertyName == "SNMPTrapPort3")
      this.IsDirty = true;
    if (propertyName == "SNMPTrapPort4")
      this.IsDirty = true;
    if (propertyName == "SNMPTrap1Active")
      this.IsDirty = true;
    if (propertyName == "SNMPTrap2Active")
      this.IsDirty = true;
    if (propertyName == "SNMPTrap3Active")
      this.IsDirty = true;
    if (propertyName == "SNMPTrap4Active")
      this.IsDirty = true;
    if (propertyName == "SensorhoodID")
      this.SensorhoodConfigDirty = true;
    if (!(propertyName == "SensorhoodTransmitions"))
      return;
    this.SensorhoodConfigDirty = true;
  }

  public bool ResetToDefault(bool autoSave) => this.ResetToDefault(autoSave, this.GatewayType);

  public bool ResetToDefault(bool autoSave, GatewayType Type)
  {
    if (Type == null)
      return false;
    this.ReportInterval = Type.DefaultReportInterval;
    this.ServerHostAddress = Type.DefaultServerHostAddress;
    this.ServerHostAddress2 = Type.DefaultServerHostAddress2;
    this.Port = Type.DefaultPort;
    this.Port2 = Type.DefaultPort2;
    this.ChannelMask = Type.DefaultChannelMask;
    this.NetworkIDFilter = Type.DefaultNetworkIDFilter;
    this.SecondaryDNS = Type.DefaultSecondaryDNS;
    this.ObserveAware = Type.DefaultObserveAware;
    this.NetworkListInterval = Type.DefaultNetworkListInterval;
    this.ErrorHeartbeat = Type.DefaultErrorHeartbeat;
    this.GatewayPowerMode = eGatewayPowerMode.Standard;
    this.PollInterval = Type.DefaultPollInterval;
    if (this.GatewayTypeID != 38L)
      this.WiFiNetworkCount = Type.DefaultWiFiNetworkCount;
    this.TransmitRate1 = Type.DefaultTransmitRate1;
    this.TransmitRate2 = Type.DefaultTransmitRate2;
    this.TransmitRate3 = Type.DefaultTransmitRate3;
    this.TransmitRateCTS = Type.DefaultTransmitRateCTS;
    this.TransmitPower = Type.DefaultTransmitPower;
    this.LedActiveTime = Type.DefaultLedActiveTime;
    this.SingleQueueExpiration = Type.DefaultSingleQueueExpiration;
    this.ServerInterfaceActive = Type.DefaultServerInterfaceActive;
    this.RealTimeInterfaceActive = Type.DefaultRealTimeInterfaceActive;
    this.RealTimeInterfaceTimeout = Type.DefaultRealTimeInterfaceTimeout;
    this.RealTimeInterfacePort = Type.DefaultRealTimeInterfacePort;
    this.ModbusInterfaceActive = Type.DefaultModbusInterfaceActive;
    this.ModbusInterfaceTimeout = Type.DefaultModbusInterfaceTimeout;
    this.ModbusInterfacePort = Type.DefaultModbusInterfacePort;
    this.SNMPInterface1Active = false;
    this.SNMPInterface2Active = false;
    this.SNMPInterface3Active = false;
    this.SNMPInterface4Active = false;
    this.SNMPInterfaceAddress1 = string.Empty;
    this.SNMPInterfaceAddress2 = string.Empty;
    this.SNMPInterfaceAddress3 = string.Empty;
    this.SNMPInterfaceAddress4 = string.Empty;
    this.SNMPInterfacePort1 = Type.DefaultSNMPInterfacePort;
    this.SNMPInterfacePort2 = Type.DefaultSNMPInterfacePort;
    this.SNMPInterfacePort3 = Type.DefaultSNMPInterfacePort;
    this.SNMPInterfacePort4 = Type.DefaultSNMPInterfacePort;
    this.SNMPTrapPort1 = Type.DefaultSNMPTrapPort;
    this.SNMPTrapPort2 = Type.DefaultSNMPTrapPort;
    this.SNMPTrapPort3 = Type.DefaultSNMPTrapPort;
    this.SNMPTrapPort4 = Type.DefaultSNMPTrapPort;
    this.SNMPCommunityString = "public";
    this.AutoConfigTime = 0;
    this.AutoConfigActionCommandTime = 0;
    this.isEnterpriseHost = false;
    this.UpdateRadioFirmware = false;
    this.DisableNetworkOnServerError = false;
    this.MQTTInterfaceActive = false;
    this.NTPInterfaceActive = false;
    this.NTPServerIP = string.Empty;
    this.NTPMinSampleRate = 30.0;
    this.GPSReportInterval = 0.0;
    this.HTTPInterfaceActive = false;
    this.HTTPServiceTimeout = 0.0;
    this.BacnetInterfaceActive = false;
    this.ResetInterval = 168;
    if (this.GatewayTypeID != 38L)
    {
      this.GatewayIP = Type.DefaultGatewayIP;
      this.GatewaySubnet = Type.DefaultGatewaySubnet;
      this.DefaultRouterIP = Type.DefaultRouterIP;
      this.GatewayDNS = Type.DefaultGatewayDNS;
    }
    else
    {
      if (this.GatewayIP != "0.0.0.0")
        this.GatewayIP = this.GatewayIP;
      if (this.GatewaySubnet != "255.255.255.0")
        this.GatewaySubnet = this.GatewaySubnet;
      if (this.DefaultRouterIP != "0.0.0.0")
        this.DefaultRouterIP = this.DefaultRouterIP;
      if (this.GatewayDNS != "0.0.0.0")
        this.GatewayDNS = this.GatewayDNS;
    }
    if (this.UMNOProf == 100)
    {
      if (!string.IsNullOrEmpty(this.CellAPNName))
        this.CellAPNName = this.CellAPNName;
      if (this.SIMAuthType == 1 || this.SIMAuthType == 2)
      {
        this.UMNOProf = this.UMNOProf;
        this.SIMAuthType = this.SIMAuthType;
        this.Username = this.Username;
        this.Password = this.Password;
        this.M1BandMask = this.M1BandMask;
        this.NB1BandMask = this.NB1BandMask;
      }
    }
    else
    {
      this.UMNOProf = 1;
      this.CellAPNName = Type.DefaultCellAPNName;
      this.SIMAuthType = 0;
      this.Username = Type.DefaultUsername;
      this.Password = Type.DefaultPassword;
      this.M1BandMask = (long) uint.MaxValue;
      this.NB1BandMask = (long) uint.MaxValue;
    }
    this.GatewayCommunicationPreference = eGatewayCommunicationPreference.Cellular_Only;
    if (this.GatewayTypeID == 7L)
      this.HTTPInterfaceActive = true;
    else if (this.GatewayTypeID == 33L)
    {
      if (new Version(this.GatewayFirmwareVersion) >= new Version("1.0.6.6"))
        this.HTTPInterfaceActive = true;
      this.SNMPInterfaceAddress1 = "0.0.0.0";
      this.SNMPInterfaceAddress2 = "0.0.0.0";
      this.SNMPInterfaceAddress3 = "255.255.255.255";
      this.NTPServerIP = "0.0.0.0";
      this.NTPMinSampleRate = 10.0;
    }
    else if (this.GatewayTypeID == 30L)
    {
      this.HTTPInterfaceActive = true;
      this.SNMPInterfaceAddress1 = "0.0.0.0";
      this.SNMPInterfaceAddress2 = "0.0.0.0";
      this.SNMPInterfaceAddress3 = "255.255.255.255";
      this.NTPServerIP = "0.0.0.0";
      this.NTPMinSampleRate = 10.0;
      if (this.UMNOProf == 100)
      {
        this.M1BandMask = this.M1BandMask;
        this.NB1BandMask = this.NB1BandMask;
        this.UMNOProf = this.UMNOProf;
      }
      else
      {
        this.M1BandMask = 0L;
        this.NB1BandMask = 0L;
        this.UMNOProf = 0;
      }
      this.GatewayCommunicationPreference = eGatewayCommunicationPreference.Prefer_Ethernet;
    }
    else if (this.GatewayTypeID == 38L)
    {
      this.ErrorHeartbeat = 720.0;
      this.RealTimeInterfaceTimeout = 0.0;
      this.RealTimeInterfacePort = 2;
    }
    this.SetCustomCompanyDefaults(false);
    if (autoSave)
      this.Save();
    return true;
  }

  public bool SetCustomCompanyDefaults(bool autoSave, GatewayType type, long companyID)
  {
    return this.SetCustomCompanyDefaults(autoSave);
  }

  public bool SetCustomCompanyDefaults(bool autoSave)
  {
    bool flag = false;
    Dictionary<string, string> dictionary = CustomCompanyDevice.LoadSettings(this.GatewayID, false).ToDictionary<CustomCompanyDevice, string, string>((Func<CustomCompanyDevice, string>) (x => x.Name), (Func<CustomCompanyDevice, string>) (x => x.Value));
    if (dictionary.Count<KeyValuePair<string, string>>() > 0)
    {
      foreach (KeyValuePair<string, string> keyValuePair in dictionary)
      {
        switch (keyValuePair.Key)
        {
          case "AutoConfigActionCommandTime":
            this.AutoConfigActionCommandTime = keyValuePair.Value.ToInt();
            break;
          case "AutoConfigTime":
            this.AutoConfigTime = keyValuePair.Value.ToInt();
            break;
          case "CellAPNName":
            this.CellAPNName = keyValuePair.Value;
            break;
          case "ChannelMask":
            this.ChannelMask = keyValuePair.Value.ToLong();
            break;
          case "DefaultRouterIP":
            this.DefaultRouterIP = keyValuePair.Value;
            break;
          case "ErrorHeartbeat":
            this.ErrorHeartbeat = keyValuePair.Value.ToDouble();
            break;
          case "GatewayDNS":
            this.GatewayDNS = keyValuePair.Value;
            break;
          case "GatewayIP":
            this.GatewayIP = keyValuePair.Value;
            break;
          case "GatewaySubnet":
            this.GatewaySubnet = keyValuePair.Value;
            break;
          case "HTTPInterfaceActive":
            this.HTTPInterfaceActive = keyValuePair.Value.ToBool();
            break;
          case "HTTPServiceTimeout":
            this.HTTPServiceTimeout = keyValuePair.Value.ToDouble();
            break;
          case "IsEnterpriseHost":
            this.isEnterpriseHost = keyValuePair.Value.ToBool();
            break;
          case "IsGPSUnlocked":
            this.IsGPSUnlocked = keyValuePair.Value.ToBool();
            break;
          case "IsUnlocked":
            this.IsUnlocked = keyValuePair.Value.ToBool();
            break;
          case "LedActiveTime":
            this.LedActiveTime = keyValuePair.Value.ToInt();
            break;
          case "ModbusInterfaceActive":
            this.ModbusInterfaceActive = keyValuePair.Value.ToBool();
            break;
          case "ModbusInterfacePort":
            this.ModbusInterfacePort = keyValuePair.Value.ToInt();
            break;
          case "ModbusInterfaceTimeout":
            this.ModbusInterfaceTimeout = keyValuePair.Value.ToDouble();
            break;
          case "NetworkIDFilter":
            this.NetworkIDFilter = keyValuePair.Value.ToInt();
            break;
          case "NetworkListInterval":
            this.NetworkListInterval = keyValuePair.Value.ToDouble();
            break;
          case "ObserveAware":
            this.ObserveAware = keyValuePair.Value.ToBool();
            break;
          case "Password":
            this.Password = keyValuePair.Value;
            break;
          case "PollInterval":
            this.PollInterval = keyValuePair.Value.ToDouble();
            break;
          case "Port":
            this.Port = keyValuePair.Value.ToInt();
            break;
          case "Port2":
            this.Port2 = keyValuePair.Value.ToInt();
            break;
          case "ReportInterval":
            this.ReportInterval = keyValuePair.Value.ToDouble();
            break;
          case "ResetInterval":
            this.ResetInterval = keyValuePair.Value.ToInt();
            break;
          case "SNMPInterface1Active":
            this.SNMPInterface1Active = keyValuePair.Value.ToBool();
            break;
          case "SNMPInterface2Active":
            this.SNMPInterface2Active = keyValuePair.Value.ToBool();
            break;
          case "SNMPInterface3Active":
            this.SNMPInterface3Active = keyValuePair.Value.ToBool();
            break;
          case "SNMPInterface4Active":
            this.SNMPInterface4Active = keyValuePair.Value.ToBool();
            break;
          case "SNMPInterfaceAddress1":
            this.SNMPInterfaceAddress1 = keyValuePair.Value;
            break;
          case "SNMPInterfaceAddress2":
            this.SNMPInterfaceAddress2 = keyValuePair.Value;
            break;
          case "SNMPInterfaceAddress3":
            this.SNMPInterfaceAddress3 = keyValuePair.Value;
            break;
          case "SNMPInterfaceAddress4":
            this.SNMPInterfaceAddress4 = keyValuePair.Value;
            break;
          case "SNMPInterfacePort1":
            this.SNMPInterfacePort1 = keyValuePair.Value.ToInt();
            break;
          case "SNMPInterfacePort2":
            this.SNMPInterfacePort2 = keyValuePair.Value.ToInt();
            break;
          case "SNMPInterfacePort3":
            this.SNMPInterfacePort3 = keyValuePair.Value.ToInt();
            break;
          case "SNMPInterfacePort4":
            this.SNMPInterfacePort4 = keyValuePair.Value.ToInt();
            break;
          case "SNMPTrap1Active":
            this.SNMPTrap1Active = keyValuePair.Value.ToBool();
            break;
          case "SNMPTrap2Active":
            this.SNMPTrap2Active = keyValuePair.Value.ToBool();
            break;
          case "SNMPTrap3Active":
            this.SNMPTrap3Active = keyValuePair.Value.ToBool();
            break;
          case "SNMPTrap4Active":
            this.SNMPTrap4Active = keyValuePair.Value.ToBool();
            break;
          case "SNMPTrapPort1":
            this.SNMPTrapPort1 = keyValuePair.Value.ToInt();
            break;
          case "SNMPTrapPort2":
            this.SNMPTrapPort2 = keyValuePair.Value.ToInt();
            break;
          case "SNMPTrapPort3":
            this.SNMPTrapPort3 = keyValuePair.Value.ToInt();
            break;
          case "SNMPTrapPort4":
            this.SNMPTrapPort4 = keyValuePair.Value.ToInt();
            break;
          case "SecondaryDNS":
            this.SecondaryDNS = keyValuePair.Value;
            break;
          case "SendGPSUnlockRequest":
            this.SendGPSUnlockRequest = keyValuePair.Value.ToBool();
            break;
          case "SendUnlockRequest":
            this.SendUnlockRequest = keyValuePair.Value.ToBool();
            break;
          case "ServerHostAddress":
            this.ServerHostAddress = keyValuePair.Value;
            break;
          case "ServerHostAddress2":
            this.ServerHostAddress2 = keyValuePair.Value;
            break;
          case "SingleQueueExpiration":
            this.SingleQueueExpiration = keyValuePair.Value.ToDouble();
            break;
          case "TransmitPower":
            this.TransmitPower = keyValuePair.Value.ToInt();
            break;
          case "Username":
            this.Username = keyValuePair.Value;
            break;
          case "WiFiNetworkCount":
            this.WiFiNetworkCount = keyValuePair.Value.ToInt();
            break;
        }
      }
      flag = true;
    }
    if (autoSave)
      this.Save();
    return flag;
  }

  public static void EnablePropertyChangedEvent(List<Gateway> list)
  {
    foreach (Gateway gateway in list)
      gateway.SuppressPropertyChangedEvent = false;
  }

  public byte[] GenerateNewEncryptionKeys(byte[] GatewayPublicKey)
  {
    EllipticCurvePoint otherPartyPublicKey = new EllipticCurvePoint(GatewayPublicKey, 0);
    if (otherPartyPublicKey.IsInfinityPoint())
      throw new Exception("Gateway.GenerateNewEncryptionKeys GenerateNewEncryptionKeys is infinity point");
    BigInteger privateKey = (BigInteger) 0;
    EllipticCurveCryptoProvider curveCryptoProvider = new EllipticCurveCryptoProvider("Secp256R1");
    Exception innerException = (Exception) null;
    byte[] newEncryptionKeys = (byte[]) null;
    int num = 3;
    while (num > 0)
    {
      try
      {
        EllipticCurvePoint publicKey;
        curveCryptoProvider.MakeKeyPair(out privateKey, out publicKey);
        newEncryptionKeys = publicKey.toArray();
        if (!otherPartyPublicKey.IsInfinityPoint())
        {
          EllipticCurvePoint ellipticCurvePoint = curveCryptoProvider.DeriveSharedSecret(privateKey, otherPartyPublicKey);
          this.CurrentPublicKey = GatewayPublicKey;
          this.CurrentEncryptionKey = ellipticCurvePoint.extractAESKEY(0);
          this.CurrentEncryptionIVCounter = ellipticCurvePoint.extractAESIV(0);
          break;
        }
        break;
      }
      catch (Exception ex)
      {
        innerException = ex;
        --num;
      }
    }
    if (num == 0)
      throw new Exception("GenerateNewEncryptionKeys 3 sequential attempts failed to generate valid server PublicKey ", innerException);
    return newEncryptionKeys;
  }

  public static byte[] InitiateFirmwareUpdate(
    long gatewayID,
    long firmwareID,
    int firmwareSize,
    int flags)
  {
    byte[] destinationArray = new byte[14];
    destinationArray[0] = (byte) 244;
    Array.Copy((Array) BitConverter.GetBytes((uint) gatewayID), 0, (Array) destinationArray, 1, 4);
    Array.Copy((Array) BitConverter.GetBytes((uint) firmwareID), 0, (Array) destinationArray, 5, 4);
    Array.Copy((Array) BitConverter.GetBytes((uint) firmwareSize), 0, (Array) destinationArray, 9, 4);
    destinationArray[13] = (byte) flags;
    return destinationArray;
  }

  public static byte[] getAutoConfigCommand(long gatewayID, int time)
  {
    byte[] destinationArray = new byte[8];
    destinationArray[0] = (byte) 116;
    Array.Copy((Array) BitConverter.GetBytes(gatewayID), 0, (Array) destinationArray, 1, 4);
    destinationArray[5] = (byte) 4;
    Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt16(time)), 0, (Array) destinationArray, 6, 2);
    return destinationArray;
  }

  public static void DeleteWiFiCredentials(long id) => Gateway.Load(id)?.DeleteAllWiFiCredentials();

  public void DeleteAllWiFiCredentials()
  {
    this.WifiCredential(1)?.Delete();
    this.WifiCredential(2)?.Delete();
    this.WifiCredential(3)?.Delete();
    this.WiFiNetworkCount = 0;
  }
}
