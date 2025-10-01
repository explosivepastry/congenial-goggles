// Decompiled with JetBrains decompiler
// Type: Monnit.GatewayType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("GatewayType")]
public class GatewayType : BaseDBObject
{
  private long _GatewayTypeID = long.MinValue;
  private string _Name = string.Empty;
  private bool _SupportsRemoteNetworkReset = false;
  private bool _SupportsHeartbeat = false;
  private double _DefaultReportInterval = double.MinValue;
  private bool _SupportsPollInterval = false;
  private double _DefaultPollInterval = double.MinValue;
  private bool _SupportsHostAddress = false;
  private string _DefaultServerHostAddress = string.Empty;
  private bool _SupportsHostAddress2 = false;
  private string _DefaultServerHostAddress2 = string.Empty;
  private bool _SupportsHostPort = false;
  private int _DefaultPort = int.MinValue;
  private bool _SupportsHostPort2 = false;
  private int _DefaultPort2 = int.MinValue;
  private bool _SupportsChannel = false;
  private long _DefaultChannelMask = long.MinValue;
  private bool _SupportsNetworkIDFilter = false;
  private int _DefaultNetworkIDFilter = int.MinValue;
  private bool _SupportsGatewayIP = false;
  private string _DefaultGatewayIP = string.Empty;
  private string _DefaultGatewaySubnet = string.Empty;
  private string _DefaultGatewayDNS = string.Empty;
  private bool _SupportsObserveAware = false;
  private bool _DefaultObserveAware = false;
  private bool _SupportsNetworkListInterval = false;
  private double _DefaultNetworkListInterval = double.MinValue;
  private bool _SupportsCellAPNName = false;
  private string _DefaultCellAPNName = string.Empty;
  private bool _SupportsUsername = false;
  private string _DefaultUsername = string.Empty;
  private bool _SupportsPassword = false;
  private string _DefaultPassword = string.Empty;
  private bool _SupportsSecondaryDNS = false;
  private string _DefaultSecondaryDNS = string.Empty;
  private string _LatestGatewayVersion = string.Empty;
  private string _LatestGatewayPath = string.Empty;
  private bool _SupportsDefaultRouterIP = false;
  private string _DefaultRouterIP = string.Empty;
  private bool _SupportsErrorHeartbeat = false;
  private double _DefaultErrorHeartbeat = double.MinValue;
  private bool _SupportsWIFISecurityMode = false;
  private eWIFI_SecurityMode _DefaultWIFISecurityMode = eWIFI_SecurityMode.OPEN;
  private int _DefaultWiFiNetworkCount = int.MinValue;
  private eWIFITransmitionRates _DefaultTransmitRate1 = eWIFITransmitionRates._Invalid;
  private eWIFITransmitionRates _DefaultTransmitRate2 = eWIFITransmitionRates._Invalid;
  private eWIFITransmitionRates _DefaultTransmitRate3 = eWIFITransmitionRates._Invalid;
  private eWIFITransmitionRates _DefaultTransmitRateCTS = eWIFITransmitionRates._Invalid;
  private int _DefaultTransmitPower = int.MinValue;
  private int _DefaultLedActiveTime = int.MinValue;
  private int _MinHeartbeat = 0;
  private byte[] _StaticEncryptionKey = (byte[]) null;
  private bool _SupportsCustomEncryptionKey = false;
  private bool _SupportsGPSReportInterval = false;
  private double _DefaultGPSReportInterval = double.MinValue;
  private bool _SupportsForceLowPower = false;
  private double _DefaultSingleQueueExpiration = double.MinValue;
  private bool _DefaultServerInterfaceActive = true;
  private bool _SupportsRealTimeInterface = false;
  private bool _DefaultRealTimeInterfaceActive = false;
  private double _DefaultRealTimeInterfaceTimeout = double.MinValue;
  private int _DefaultRealTimeInterfacePort = int.MinValue;
  private bool _SupportsModbusInterface = false;
  private bool _DefaultModbusInterfaceActive = false;
  private double _DefaultModbusInterfaceTimeout = double.MinValue;
  private int _DefaultModbusInterfacePort = int.MinValue;
  private bool _SupportsSNMPInterface = false;
  private int _DefaultSNMPInterfacePort = int.MinValue;
  private int _DefaultSNMPTrapPort = int.MinValue;
  private bool _SupportsSensorInterpretor = false;
  private string _SensorInterpretorVersion = string.Empty;
  private string _SensorInterpretorPath = string.Empty;
  private bool _SupportsDataUsage = false;
  private bool _IsGatewayBase = false;
  private bool _SupportsOTASuite = false;
  private string _MinOTAFirmwareVersion = string.Empty;
  private bool _SupportsOTASuiteBSN = false;
  private string _ModuleUpdateURL = string.Empty;

  [DBProp("GatewayTypeID", IsPrimaryKey = true)]
  public long GatewayTypeID
  {
    get => this._GatewayTypeID;
    set => this._GatewayTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  [DBProp("SupportsRemoteNetworkReset", AllowNull = true)]
  public bool SupportsRemoteNetworkReset
  {
    get => this._SupportsRemoteNetworkReset;
    set => this._SupportsRemoteNetworkReset = value;
  }

  [DBProp("SupportsHeartbeat", AllowNull = true)]
  public bool SupportsHeartbeat
  {
    get => this._SupportsHeartbeat;
    set => this._SupportsHeartbeat = value;
  }

  [DBProp("DefaultReportInterval")]
  public double DefaultReportInterval
  {
    get => this._DefaultReportInterval;
    set => this._DefaultReportInterval = value;
  }

  [DBProp("SupportsPollInterval", AllowNull = true)]
  public bool SupportsPollInterval
  {
    get => this._SupportsPollInterval;
    set => this._SupportsPollInterval = value;
  }

  [DBProp("DefaultPollInterval", DefaultValue = 0, AllowNull = false)]
  public double DefaultPollInterval
  {
    get => this._DefaultPollInterval;
    set => this._DefaultPollInterval = value;
  }

  [DBProp("SupportsHostAddress", AllowNull = true)]
  public bool SupportsHostAddress
  {
    get => this._SupportsHostAddress;
    set => this._SupportsHostAddress = value;
  }

  [DBProp("DefaultServerHostAddress", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string DefaultServerHostAddress
  {
    get => this._DefaultServerHostAddress;
    set => this._DefaultServerHostAddress = value;
  }

  [DBProp("SupportsHostAddress2", AllowNull = true)]
  public bool SupportsHostAddress2
  {
    get => this._SupportsHostAddress2;
    set => this._SupportsHostAddress2 = value;
  }

  [DBProp("DefaultServerHostAddress2", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string DefaultServerHostAddress2
  {
    get => this._DefaultServerHostAddress2;
    set => this._DefaultServerHostAddress2 = value;
  }

  [DBProp("SupportsHostPort", AllowNull = true)]
  public bool SupportsHostPort
  {
    get => this._SupportsHostPort;
    set => this._SupportsHostPort = value;
  }

  [DBProp("DefaultPort")]
  public int DefaultPort
  {
    get => this._DefaultPort;
    set => this._DefaultPort = value;
  }

  [DBProp("SupportsHostPort2", AllowNull = true)]
  public bool SupportsHostPort2
  {
    get => this._SupportsHostPort2;
    set => this._SupportsHostPort2 = value;
  }

  [DBProp("DefaultPort2")]
  public int DefaultPort2
  {
    get => this._DefaultPort2;
    set => this._DefaultPort2 = value;
  }

  [DBProp("SupportsChannel", AllowNull = true)]
  public bool SupportsChannel
  {
    get => this._SupportsChannel;
    set => this._SupportsChannel = value;
  }

  [DBProp("DefaultChannelMask")]
  public long DefaultChannelMask
  {
    get => this._DefaultChannelMask;
    set => this._DefaultChannelMask = value;
  }

  [DBProp("SupportsNetworkIDFilter", AllowNull = true)]
  public bool SupportsNetworkIDFilter
  {
    get => this._SupportsNetworkIDFilter;
    set => this._SupportsNetworkIDFilter = value;
  }

  [DBProp("DefaultNetworkIDFilter")]
  public int DefaultNetworkIDFilter
  {
    get => this._DefaultNetworkIDFilter;
    set => this._DefaultNetworkIDFilter = value;
  }

  [DBProp("SupportsGatewayIP", AllowNull = true)]
  public bool SupportsGatewayIP
  {
    get => this._SupportsGatewayIP;
    set => this._SupportsGatewayIP = value;
  }

  [DBProp("DefaultGatewayIP", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string DefaultGatewayIP
  {
    get => this._DefaultGatewayIP;
    set => this._DefaultGatewayIP = value;
  }

  [DBProp("DefaultGatewaySubnet", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string DefaultGatewaySubnet
  {
    get => this._DefaultGatewaySubnet;
    set => this._DefaultGatewaySubnet = value;
  }

  [DBProp("DefaultGatewayDNS", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string DefaultGatewayDNS
  {
    get => this._DefaultGatewayDNS;
    set => this._DefaultGatewayDNS = value;
  }

  [DBProp("SupportsObserveAware", AllowNull = true)]
  public bool SupportsObserveAware
  {
    get => this._SupportsObserveAware;
    set => this._SupportsObserveAware = value;
  }

  [DBProp("DefaultObserveAware")]
  public bool DefaultObserveAware
  {
    get => this._DefaultObserveAware;
    set => this._DefaultObserveAware = value;
  }

  [DBProp("SupportsNetworkListInterval", AllowNull = true)]
  public bool SupportsNetworkListInterval
  {
    get => this._SupportsNetworkListInterval;
    set => this._SupportsNetworkListInterval = value;
  }

  [DBProp("DefaultNetworkListInterval")]
  public double DefaultNetworkListInterval
  {
    get => this._DefaultNetworkListInterval;
    set => this._DefaultNetworkListInterval = value;
  }

  [DBProp("SupportsCellAPNName", AllowNull = true)]
  public bool SupportsCellAPNName
  {
    get => this._SupportsCellAPNName;
    set => this._SupportsCellAPNName = value;
  }

  [DBProp("DefaultCellAPNName", MaxLength = 64 /*0x40*/)]
  public string DefaultCellAPNName
  {
    get => this._DefaultCellAPNName;
    set => this._DefaultCellAPNName = value;
  }

  [DBProp("SupportsUsername", AllowNull = true)]
  public bool SupportsUsername
  {
    get => this._SupportsUsername;
    set => this._SupportsUsername = value;
  }

  [DBProp("DefaultUsername", MaxLength = 64 /*0x40*/)]
  public string DefaultUsername
  {
    get => this._DefaultUsername;
    set => this._DefaultUsername = value;
  }

  [DBProp("SupportsPassword", AllowNull = true)]
  public bool SupportsPassword
  {
    get => this._SupportsPassword;
    set => this._SupportsPassword = value;
  }

  [DBProp("DefaultPassword", MaxLength = 64 /*0x40*/)]
  public string DefaultPassword
  {
    get => this._DefaultPassword;
    set => this._DefaultPassword = value;
  }

  [DBProp("SupportsSecondaryDNS", AllowNull = true)]
  public bool SupportsSecondaryDNS
  {
    get => this._SupportsSecondaryDNS;
    set => this._SupportsSecondaryDNS = value;
  }

  [DBProp("DefaultSecondaryDNS", MaxLength = 64 /*0x40*/)]
  public string DefaultSecondaryDNS
  {
    get => this._DefaultSecondaryDNS;
    set => this._DefaultSecondaryDNS = value;
  }

  [DBProp("LatestGatewayVersion", MaxLength = 64 /*0x40*/)]
  public string LatestGatewayVersion
  {
    get => this._LatestGatewayVersion;
    set => this._LatestGatewayVersion = value;
  }

  [DBProp("LatestGatewayPath", MaxLength = 255 /*0xFF*/)]
  public string LatestGatewayPath
  {
    get => this._LatestGatewayPath;
    set => this._LatestGatewayPath = value;
  }

  [DBProp("SupportsDefaultRouterIP", AllowNull = true)]
  public bool SupportsDefaultRouterIP
  {
    get => this._SupportsDefaultRouterIP;
    set => this._SupportsDefaultRouterIP = value;
  }

  [DBProp("DefaultRouterIP", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string DefaultRouterIP
  {
    get => this._DefaultRouterIP;
    set => this._DefaultRouterIP = value;
  }

  [DBProp("SupportsErrorHeartbeat")]
  public bool SupportsErrorHeartbeat
  {
    get => this._SupportsErrorHeartbeat;
    set => this._SupportsErrorHeartbeat = value;
  }

  [DBProp("DefaultErrorHeartbeat")]
  public double DefaultErrorHeartbeat
  {
    get => this._DefaultErrorHeartbeat;
    set => this._DefaultErrorHeartbeat = value;
  }

  [DBProp("SupportsWIFISecurityMode")]
  public bool SupportsWIFISecurityMode
  {
    get => this._SupportsWIFISecurityMode;
    set => this._SupportsWIFISecurityMode = value;
  }

  [DBProp("DefaultWIFISecurityMode")]
  public eWIFI_SecurityMode DefaultWIFISecurityMode
  {
    get => this._DefaultWIFISecurityMode;
    set => this._DefaultWIFISecurityMode = value;
  }

  [DBProp("DefaultWiFiNetworkCount")]
  public int DefaultWiFiNetworkCount
  {
    get => this._DefaultWiFiNetworkCount;
    set => this._DefaultWiFiNetworkCount = value;
  }

  [DBProp("DefaultTransmitRate1")]
  public eWIFITransmitionRates DefaultTransmitRate1
  {
    get => this._DefaultTransmitRate1;
    set => this._DefaultTransmitRate1 = value;
  }

  [DBProp("DefaultTransmitRate2")]
  public eWIFITransmitionRates DefaultTransmitRate2
  {
    get => this._DefaultTransmitRate2;
    set => this._DefaultTransmitRate2 = value;
  }

  [DBProp("DefaultTransmitRate3")]
  public eWIFITransmitionRates DefaultTransmitRate3
  {
    get => this._DefaultTransmitRate3;
    set => this._DefaultTransmitRate3 = value;
  }

  [DBProp("DefaultTransmitRateCTS")]
  public eWIFITransmitionRates DefaultTransmitRateCTS
  {
    get => this._DefaultTransmitRateCTS;
    set => this._DefaultTransmitRateCTS = value;
  }

  [DBProp("DefaultTransmitPower")]
  public int DefaultTransmitPower
  {
    get => this._DefaultTransmitPower;
    set => this._DefaultTransmitPower = value;
  }

  [DBProp("DefaultLedActiveTime")]
  public int DefaultLedActiveTime
  {
    get => this._DefaultLedActiveTime;
    set => this._DefaultLedActiveTime = value;
  }

  [DBProp("MinHeartbeat", DefaultValue = 0)]
  public int MinHeartbeat
  {
    get => this._MinHeartbeat;
    set => this._MinHeartbeat = value;
  }

  [DBProp("StaticEncryptionKey", AllowNull = true, MaxLength = 16 /*0x10*/)]
  public byte[] StaticEncryptionKey
  {
    get => this._StaticEncryptionKey;
    set => this._StaticEncryptionKey = value;
  }

  [DBProp("SupportsCustomEncryptionKey", AllowNull = true)]
  public bool SupportsCustomEncryptionKey
  {
    get => this._SupportsCustomEncryptionKey;
    set => this._SupportsCustomEncryptionKey = value;
  }

  [DBProp("SupportsGPSReportInterval", AllowNull = true)]
  public bool SupportsGPSReportInterval
  {
    get => this._SupportsGPSReportInterval;
    set => this._SupportsGPSReportInterval = value;
  }

  [DBProp("DefaultGPSReportInterval")]
  public double DefaultGPSReportInterval
  {
    get => this._DefaultGPSReportInterval < 0.0 ? 720.0 : this._DefaultGPSReportInterval;
    set => this._DefaultGPSReportInterval = value;
  }

  [DBProp("SupportsForceLowPower", AllowNull = true)]
  public bool SupportsForceLowPower
  {
    get => this._SupportsForceLowPower;
    set => this._SupportsForceLowPower = value;
  }

  [DBProp("DefaultSingleQueueExpiration")]
  public double DefaultSingleQueueExpiration
  {
    get => this._DefaultSingleQueueExpiration;
    set => this._DefaultSingleQueueExpiration = value;
  }

  [DBProp("DefaultServerInterfaceActive")]
  public bool DefaultServerInterfaceActive
  {
    get => this._DefaultServerInterfaceActive;
    set => this._DefaultServerInterfaceActive = value;
  }

  [DBProp("SupportsRealTimeInterface")]
  public bool SupportsRealTimeInterface
  {
    get => this._SupportsRealTimeInterface;
    set => this._SupportsRealTimeInterface = value;
  }

  [DBProp("DefaultRealTimeInterfaceActive")]
  public bool DefaultRealTimeInterfaceActive
  {
    get => this._DefaultRealTimeInterfaceActive;
    set => this._DefaultRealTimeInterfaceActive = value;
  }

  [DBProp("DefaultRealTimeInterfaceTimeout")]
  public double DefaultRealTimeInterfaceTimeout
  {
    get => this._DefaultRealTimeInterfaceTimeout;
    set => this._DefaultRealTimeInterfaceTimeout = value;
  }

  [DBProp("DefaultRealTimeInterfacePort")]
  public int DefaultRealTimeInterfacePort
  {
    get => this._DefaultRealTimeInterfacePort;
    set => this._DefaultRealTimeInterfacePort = value;
  }

  [DBProp("SupportsModbusInterface")]
  public bool SupportsModbusInterface
  {
    get => this._SupportsModbusInterface;
    set => this._SupportsModbusInterface = value;
  }

  [DBProp("DefaultModbusInterfaceActive")]
  public bool DefaultModbusInterfaceActive
  {
    get => this._DefaultModbusInterfaceActive;
    set => this._DefaultModbusInterfaceActive = value;
  }

  [DBProp("DefaultModbusInterfaceTimeout")]
  public double DefaultModbusInterfaceTimeout
  {
    get => this._DefaultModbusInterfaceTimeout;
    set => this._DefaultModbusInterfaceTimeout = value;
  }

  [DBProp("DefaultModbusInterfacePort")]
  public int DefaultModbusInterfacePort
  {
    get => this._DefaultModbusInterfacePort;
    set => this._DefaultModbusInterfacePort = value;
  }

  [DBProp("SupportsSNMPInterface")]
  public bool SupportsSNMPInterface
  {
    get => this._SupportsSNMPInterface;
    set => this._SupportsSNMPInterface = value;
  }

  [DBProp("DefaultSNMPInterfacePort")]
  public int DefaultSNMPInterfacePort
  {
    get => this._DefaultSNMPInterfacePort;
    set => this._DefaultSNMPInterfacePort = value;
  }

  [DBProp("DefaultSNMPTrapPort")]
  public int DefaultSNMPTrapPort
  {
    get => this._DefaultSNMPTrapPort;
    set => this._DefaultSNMPTrapPort = value;
  }

  [DBProp("SupportsSensorInterpretor")]
  public bool SupportsSensorInterpretor
  {
    get => this._SupportsSensorInterpretor;
    set => this._SupportsSensorInterpretor = value;
  }

  [DBProp("SensorInterpretorVersion", MaxLength = 64 /*0x40*/)]
  public string SensorInterpretorVersion
  {
    get => this._SensorInterpretorVersion;
    set => this._SensorInterpretorVersion = value;
  }

  [DBProp("SensorInterpretorPath", MaxLength = 255 /*0xFF*/)]
  public string SensorInterpretorPath
  {
    get => this._SensorInterpretorPath;
    set => this._SensorInterpretorPath = value;
  }

  [DBProp("SupportsDataUsage", AllowNull = false, DefaultValue = false)]
  public bool SupportsDataUsage
  {
    get => this._SupportsDataUsage;
    set => this._SupportsDataUsage = value;
  }

  [DBProp("IsGatewayBase", AllowNull = false, DefaultValue = true)]
  public bool IsGatewayBase
  {
    get => this._IsGatewayBase;
    set => this._IsGatewayBase = value;
  }

  [DBProp("SupportsOTASuite")]
  public bool SupportsOTASuite
  {
    get => this._SupportsOTASuite;
    set => this._SupportsOTASuite = value;
  }

  [DBProp("MinOTAFirmwareVersion", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string MinOTAFirmwareVersion
  {
    get => this._MinOTAFirmwareVersion;
    set => this._MinOTAFirmwareVersion = value;
  }

  [DBProp("SupportsOTASuiteBSN")]
  public bool SupportsOTASuiteBSN
  {
    get => this._SupportsOTASuiteBSN;
    set => this._SupportsOTASuiteBSN = value;
  }

  [DBProp("ModuleUpdateURL", MaxLength = 700)]
  public string ModuleUpdateURL
  {
    get => this._ModuleUpdateURL;
    set => this._ModuleUpdateURL = value;
  }

  public int OTAChunkSize
  {
    get
    {
      switch (this.GatewayTypeID - 30L)
      {
        case 0:
        case 2:
        case 3:
        case 5:
        case 6:
          return 512 /*0x0200*/;
        default:
          return 1024 /*0x0400*/;
      }
    }
  }

  public int DownMessageSize
  {
    get
    {
      switch (this.GatewayTypeID - 30L)
      {
        case 0:
        case 2:
        case 3:
        case 5:
        case 6:
          return 960;
        default:
          return 1024 /*0x0400*/;
      }
    }
  }

  public static GatewayType Load(long ID)
  {
    string key = $"GatewayType_{ID}";
    GatewayType gatewayType = TimedCache.RetrieveObject<GatewayType>(key);
    if (gatewayType == null)
    {
      gatewayType = BaseDBObject.Load<GatewayType>(ID);
      if (gatewayType == null)
        gatewayType = new GatewayType()
        {
          Name = "Pre_USB_1.2.008"
        };
      TimedCache.AddObjectToCach(key, (object) gatewayType);
    }
    return gatewayType;
  }

  public static List<GatewayType> LoadAll()
  {
    string key = "GatewayTypeList";
    List<GatewayType> gatewayTypeList = TimedCache.RetrieveObject<List<GatewayType>>(key);
    if (gatewayTypeList == null)
    {
      gatewayTypeList = BaseDBObject.LoadAll<GatewayType>();
      if (gatewayTypeList != null)
        TimedCache.AddObjectToCach(key, (object) gatewayTypeList, new TimeSpan(1, 0, 0));
    }
    return gatewayTypeList;
  }
}
