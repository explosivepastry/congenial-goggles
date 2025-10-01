// Decompiled with JetBrains decompiler
// Type: Monnit.Sensor
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using BaseApplication;
using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;

#nullable disable
namespace Monnit;

[DBClass("Sensor")]
[MetadataType(typeof (SensorMetadata))]
public class Sensor : BaseDBObject, ISensor, INotifyPropertyChanged, ISuppressPropertyChanged
{
  private bool _SuppressPropertyChangedEvent = true;
  private long _SensorID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _MonnitApplicationID = long.MinValue;
  private long _CSNetID = long.MinValue;
  private DateTime _CreateDate = DateTime.UtcNow;
  private DateTime _StartDate = DateTime.MinValue;
  private string _SensorName = string.Empty;
  private double _ReportInterval = double.MinValue;
  private double _ActiveStateInterval = double.MinValue;
  private int _MinimumCommunicationFrequency = int.MinValue;
  private DateTime _LastCommunicationDate = DateTime.MinValue;
  private Guid _LastDataMessageGUID = Guid.Empty;
  private bool _PendingActionControlCommand = false;
  private bool _IsActive = false;
  private bool _ForceOverwrite = false;
  private bool _GeneralConfigDirty = false;
  private bool _GeneralConfig2Dirty = false;
  private bool _GeneralConfig3Dirty = false;
  private bool _RFConfig1Dirty = false;
  private long _ChannelMask = long.MinValue;
  private int _StandardMessageDelay = int.MinValue;
  private int _TransmitIntervalLink = int.MinValue;
  private int _TransmitIntervalTest = int.MinValue;
  private int _TestTransmitCount = int.MinValue;
  private int _MaximumNetworkHops = int.MinValue;
  private int _RetryCount = int.MinValue;
  private int _Recovery = int.MinValue;
  private byte[] _TimeOfDayActive = (byte[]) null;
  private int _ListenBeforeTalkValue = (int) byte.MaxValue;
  private int _LinkAcceptance = -50;
  private int _CrystalStartTime = 3;
  private int _DMExchangeDelayMultiple = int.MinValue;
  private long _SHID1 = (long) int.MinValue;
  private long _SHID2 = (long) int.MinValue;
  private int _CryptRequired = int.MinValue;
  private int _Pingtime = int.MinValue;
  private int _BSNUrgentDelay = int.MinValue;
  private int _UrgentRetries = int.MinValue;
  private int _TransmitPower = int.MinValue;
  private int _TransmitPowerOptions = 0;
  private int _ReceiveSensitivity = 0;
  private bool _ReadGeneralConfig1 = false;
  private bool _ReadGeneralConfig2 = false;
  private bool _ReadGeneralConfig3 = false;
  private int _TimeOffset = 0;
  private int _TimeOfDayControl = 0;
  private bool _ProfileConfigDirty = false;
  private bool _ProfileConfig2Dirty = false;
  private bool _ReadProfileConfig1 = false;
  private bool _ReadProfileConfig2 = false;
  private int _MeasurementsPerTransmission = int.MinValue;
  private int _TransmitOffset = int.MinValue;
  private long _Hysteresis = long.MinValue;
  private long _MinimumThreshold = long.MinValue;
  private long _MaximumThreshold = long.MinValue;
  private long _Calibration1 = long.MinValue;
  private long _Calibration2 = long.MinValue;
  private long _Calibration3 = long.MinValue;
  private long _Calibration4 = long.MinValue;
  private int _EventDetectionType = int.MinValue;
  private int _EventDetectionPeriod = int.MinValue;
  private int _EventDetectionCount = int.MinValue;
  private int _RearmTime = int.MinValue;
  private int _BiStable = int.MinValue;
  private bool _SensorhoodConfigDirty = false;
  private long _SensorhoodID = long.MinValue;
  private int _SensorhoodTransmitions = int.MinValue;
  private string _FirmwareVersion = string.Empty;
  private string _RadioBand = string.Empty;
  private long _PowerSourceID = long.MinValue;
  private bool _IsSleeping = false;
  private string _ExternalID = string.Empty;
  private eWitType _WitType = eWitType.Wit;
  private long _SensorTypeID = long.MinValue;
  private bool _ForceToBootloader = false;
  private bool _DataLog = false;
  private int _LocalConfigOptions = 0;
  private string _TagString = string.Empty;
  private bool _IsDeleted = false;
  private bool _IsNewToNetwork = false;
  private long _SensorApplicationID = long.MinValue;
  private string _CalibrationCertification = string.Empty;
  private long _CalibrationFacilityID = long.MinValue;
  private long _ParentID = long.MinValue;
  private DateTime _resumeNotificationDate = new DateTime(2010, 1, 1);
  private string _Make = string.Empty;
  private string _Model = string.Empty;
  private string _SerialNumber = string.Empty;
  private string _Location = string.Empty;
  private string _Description = string.Empty;
  private string _Note = string.Empty;
  private long _SensorFirmwareID = long.MinValue;
  private bool _SendFirmwareUpdate = false;
  private bool _FirmwareUpdateInProgress = false;
  private bool _FirmwareDownloadComplete = false;
  private string _GenerationType = string.Empty;
  private string _SKU = string.Empty;
  private byte[] _SensorPrint = (byte[]) null;
  private bool _SensorPrintDirty = false;
  private PowerSource _PowerSource;
  private long _TemplateSensorID = long.MinValue;
  private DateTime _WarrantyStartDate = DateTime.UtcNow;
  private bool _IsCableEnabled = false;
  public long _CableID = long.MinValue;
  private List<eDatumType> DatumTypes = (List<eDatumType>) null;
  private List<eDatumStruct> DatumStructs = (List<eDatumStruct>) null;
  private MonnitApplication _MonnitApplication;
  private SensorApplication _SensorApplication;
  private bool _QuickCacheChecked = false;
  private DataMessage _LastDataMessage;
  private Dictionary<string, object> _Defaults;

  public bool SuppressPropertyChangedEvent
  {
    get => this._SuppressPropertyChangedEvent;
    set => this._SuppressPropertyChangedEvent = value;
  }

  [DBProp("SensorID", IsPrimaryKey = true)]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set
    {
      long accountId = this.AccountID;
      this._AccountID = value;
      if (accountId == this.AccountID || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (AccountID));
    }
  }

  [DBForeignKey("Application", "ApplicationID")]
  [DBProp("ApplicationID", AllowNull = false)]
  public long MonnitApplicationID
  {
    get => this._MonnitApplicationID;
    set
    {
      long monnitApplicationId = this.MonnitApplicationID;
      this._MonnitApplicationID = value;
      if (monnitApplicationId == this.MonnitApplicationID || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged("ApplicationID");
    }
  }

  public long ApplicationID
  {
    get => this.MonnitApplicationID;
    set => this.MonnitApplicationID = value;
  }

  [DBProp("CSNetID", AllowNull = true)]
  public long CSNetID
  {
    get => this._CSNetID;
    set
    {
      long csNetId = this.CSNetID;
      this._CSNetID = value;
      if (csNetId == this.CSNetID || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (CSNetID));
    }
  }

  public CSNet Network => CSNet.Load(this.CSNetID);

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("StartDate")]
  public DateTime StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [DBProp("SensorName", AllowNull = false, MaxLength = 255 /*0xFF*/, International = true)]
  public string SensorName
  {
    get => this._SensorName;
    set
    {
      string sensorName = this.SensorName;
      this._SensorName = value != null ? value : string.Empty;
      if (!(sensorName != this.SensorName) || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (SensorName));
    }
  }

  public string Name => this.SensorName;

  [DBProp("ReportInterval")]
  public double ReportInterval
  {
    get => this._ReportInterval;
    set
    {
      double reportInterval = this.ReportInterval;
      this._ReportInterval = value;
      if (reportInterval == this.ReportInterval || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ReportInterval));
    }
  }

  [DBProp("ActiveStateInterval")]
  public double ActiveStateInterval
  {
    get => this._ActiveStateInterval;
    set
    {
      double activeStateInterval = this.ActiveStateInterval;
      this._ActiveStateInterval = value;
      if (activeStateInterval == this.ActiveStateInterval || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ActiveStateInterval));
    }
  }

  [DBProp("MinimumCommunicationFrequency")]
  public int MinimumCommunicationFrequency
  {
    get => this._MinimumCommunicationFrequency;
    set
    {
      int communicationFrequency = this.MinimumCommunicationFrequency;
      this._MinimumCommunicationFrequency = value;
      if (communicationFrequency == this.MinimumCommunicationFrequency || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (MinimumCommunicationFrequency));
    }
  }

  [DBProp("LastCommunicationDate", AutoUpdate = false)]
  public DateTime LastCommunicationDate
  {
    get => this._LastCommunicationDate;
    set
    {
      DateTime communicationDate = this.LastCommunicationDate;
      this._LastCommunicationDate = value;
      if (!(communicationDate != this.LastCommunicationDate) || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (LastCommunicationDate));
      this.OnPropertyChanged("SensorID");
    }
  }

  [DBProp("LastDataMessageGUID", AutoUpdate = false)]
  public Guid LastDataMessageGUID
  {
    get => this._LastDataMessageGUID;
    set
    {
      Guid lastDataMessageGuid = this._LastDataMessageGUID;
      this._LastDataMessageGUID = value;
      if (!(lastDataMessageGuid != this.LastDataMessageGUID))
        return;
      this.LastDataMessage = (DataMessage) null;
      if (!this.SuppressPropertyChangedEvent)
      {
        this.OnPropertyChanged(nameof (LastDataMessageGUID));
        this.OnPropertyChanged("LastDataMessage");
      }
    }
  }

  [DBProp("PendingActionControlCommand", AllowNull = false, DefaultValue = false)]
  public bool PendingActionControlCommand
  {
    get => this._PendingActionControlCommand;
    set
    {
      bool actionControlCommand = this.PendingActionControlCommand;
      this._PendingActionControlCommand = value;
      if (actionControlCommand == this.PendingActionControlCommand || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (PendingActionControlCommand));
    }
  }

  [DBProp("IsActive", AllowNull = false, DefaultValue = true)]
  public bool IsActive
  {
    get => this._IsActive;
    set
    {
      bool isActive = this.IsActive;
      this._IsActive = value;
      if (isActive == this.IsActive || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (IsActive));
    }
  }

  [DBProp("ForceOverwrite", AllowNull = true)]
  public bool ForceOverwrite
  {
    get => this._ForceOverwrite;
    set
    {
      bool forceOverwrite = this.ForceOverwrite;
      this._ForceOverwrite = value;
      if (forceOverwrite == this.ForceOverwrite || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ForceOverwrite));
    }
  }

  [DBProp("IsDirty", AllowNull = false, DefaultValue = false)]
  public bool GeneralConfigDirty
  {
    get => this._GeneralConfigDirty;
    set
    {
      bool generalConfigDirty = this.GeneralConfigDirty;
      this._GeneralConfigDirty = value;
      if (generalConfigDirty == this.GeneralConfigDirty || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (GeneralConfigDirty));
      this.OnPropertyChanged("IsDirty");
    }
  }

  [DBProp("IsDirtyGeneral2", AllowNull = false, DefaultValue = false)]
  public bool GeneralConfig2Dirty
  {
    get => this._GeneralConfig2Dirty;
    set
    {
      bool generalConfig2Dirty = this.GeneralConfig2Dirty;
      this._GeneralConfig2Dirty = value;
      if (generalConfig2Dirty == this.GeneralConfig2Dirty || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (GeneralConfig2Dirty));
    }
  }

  [DBProp("IsDirtyGeneral3", AllowNull = false, DefaultValue = false)]
  public bool GeneralConfig3Dirty
  {
    get => this._GeneralConfig3Dirty;
    set
    {
      bool generalConfig3Dirty = this.GeneralConfig3Dirty;
      this._GeneralConfig3Dirty = value;
      if (generalConfig3Dirty == this.GeneralConfig3Dirty || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (GeneralConfig3Dirty));
    }
  }

  [DBProp("IsDirtyRFConfig1", DefaultValue = false)]
  public bool RFConfig1Dirty
  {
    get => this._RFConfig1Dirty;
    set
    {
      bool rfConfig1Dirty = this.RFConfig1Dirty;
      this._RFConfig1Dirty = value;
      if (rfConfig1Dirty == this.RFConfig1Dirty || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (RFConfig1Dirty));
    }
  }

  [DBProp("ChannelMask", AllowNull = true)]
  public long ChannelMask
  {
    get => this._ChannelMask == long.MinValue ? (long) uint.MaxValue : this._ChannelMask;
    set
    {
      long channelMask = this.ChannelMask;
      this._ChannelMask = value;
      if (channelMask == this.ChannelMask || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ChannelMask));
    }
  }

  [DBProp("StandardMessageDelay", AllowNull = true)]
  public int StandardMessageDelay
  {
    get => this._StandardMessageDelay == int.MinValue ? 50 : this._StandardMessageDelay;
    set
    {
      int standardMessageDelay = this.StandardMessageDelay;
      this._StandardMessageDelay = value;
      if (standardMessageDelay == this.StandardMessageDelay || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (StandardMessageDelay));
    }
  }

  [DBProp("TransmitIntervalLink", AllowNull = true)]
  public int TransmitIntervalLink
  {
    get => this._TransmitIntervalLink == int.MinValue ? 2 : this._TransmitIntervalLink;
    set
    {
      int transmitIntervalLink = this.TransmitIntervalLink;
      this._TransmitIntervalLink = value;
      if (transmitIntervalLink == this.TransmitIntervalLink || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (TransmitIntervalLink));
    }
  }

  [DBProp("TransmitIntervalTest", AllowNull = true)]
  public int TransmitIntervalTest
  {
    get => this._TransmitIntervalTest == int.MinValue ? 1 : this._TransmitIntervalTest;
    set
    {
      int transmitIntervalTest = this.TransmitIntervalTest;
      this._TransmitIntervalTest = value;
      if (transmitIntervalTest == this.TransmitIntervalTest || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (TransmitIntervalTest));
    }
  }

  [DBProp("TestTransmitCount", AllowNull = true)]
  public int TestTransmitCount
  {
    get => this._TestTransmitCount == int.MinValue ? 10 : this._TestTransmitCount;
    set
    {
      int testTransmitCount = this.TestTransmitCount;
      this._TestTransmitCount = value;
      if (testTransmitCount == this.TestTransmitCount || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (TestTransmitCount));
    }
  }

  [DBProp("MaximumNetworkHops", AllowNull = true)]
  public int MaximumNetworkHops
  {
    get => this._MaximumNetworkHops == int.MinValue ? 1 : this._MaximumNetworkHops;
    set
    {
      int maximumNetworkHops = this.MaximumNetworkHops;
      this._MaximumNetworkHops = value;
      if (maximumNetworkHops == this.MaximumNetworkHops || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (MaximumNetworkHops));
    }
  }

  [DBProp("RetryCount", AllowNull = true)]
  public int RetryCount
  {
    get => this._RetryCount == int.MinValue ? 2 : this._RetryCount;
    set
    {
      int retryCount = this.RetryCount;
      this._RetryCount = value;
      if (retryCount == this.RetryCount || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (RetryCount));
    }
  }

  [DBProp("Recovery", AllowNull = true)]
  public int Recovery
  {
    get => this._Recovery == int.MinValue ? 2 : this._Recovery;
    set
    {
      int recovery = this.Recovery;
      this._Recovery = value;
      if (recovery == this.Recovery || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (Recovery));
    }
  }

  [DBProp("TimeOfDayActive", AllowNull = true)]
  public byte[] TimeOfDayActive
  {
    get
    {
      if (this._TimeOfDayActive == null)
        this._TimeOfDayActive = Sensor.DefaultTimeOfDayBytes(this.GenerationType);
      if (this._TimeOfDayActive.Length != 12 && this.GenerationType.ToUpper().Contains("GEN1"))
        this._TimeOfDayActive = Sensor.DefaultTimeOfDayBytes(this.GenerationType);
      if (this._TimeOfDayActive.Length != 14 && this.GenerationType.ToUpper().Contains("GEN2"))
        this._TimeOfDayActive = Sensor.DefaultTimeOfDayBytes(this.GenerationType);
      bool flag = true;
      for (int index = 0; index < this._TimeOfDayActive.Length; ++index)
      {
        if (this._TimeOfDayActive[index] > (byte) 0)
          flag = false;
      }
      if (flag)
        this._TimeOfDayActive = Sensor.DefaultTimeOfDayBytes(this.GenerationType);
      return this._TimeOfDayActive;
    }
    set
    {
      bool flag = false;
      if (!this.EqualToTimeOfDayActive(value) && !this.SuppressPropertyChangedEvent)
        flag = true;
      this._TimeOfDayActive = value;
      if (!flag)
        return;
      this.OnPropertyChanged(nameof (TimeOfDayActive));
    }
  }

  public static byte[] DefaultTimeOfDayBytes(string GenerationType)
  {
    byte[] destinationArray;
    if (!GenerationType.ToUpper().Contains("GEN2"))
    {
      destinationArray = new byte[12];
      Array.Copy((Array) BitConverter.GetBytes(281474976710655L /*0xFFFFFFFFFFFF*/), 0, (Array) destinationArray, 0, 6);
      Array.Copy((Array) BitConverter.GetBytes(281474976710655L /*0xFFFFFFFFFFFF*/), 0, (Array) destinationArray, 6, 6);
    }
    else
    {
      destinationArray = new byte[14];
      Array.Copy((Array) BitConverter.GetBytes(281474976710655L /*0xFFFFFFFFFFFF*/), 0, (Array) destinationArray, 0, 6);
      Array.Copy((Array) BitConverter.GetBytes(ulong.MaxValue), 0, (Array) destinationArray, 6, 8);
    }
    return destinationArray;
  }

  public static byte[] DefaultTimeZoneBytes(string GenerationType)
  {
    byte[] destinationArray;
    if (!GenerationType.ToUpper().Contains("GEN2"))
    {
      destinationArray = new byte[12];
      Array.Copy((Array) BitConverter.GetBytes(281474976710655L /*0xFFFFFFFFFFFF*/), 0, (Array) destinationArray, 0, 6);
      Array.Copy((Array) BitConverter.GetBytes(281474976710655L /*0xFFFFFFFFFFFF*/), 0, (Array) destinationArray, 6, 6);
    }
    else
    {
      destinationArray = new byte[14];
      Array.Copy((Array) BitConverter.GetBytes(281474976710655L /*0xFFFFFFFFFFFF*/), 0, (Array) destinationArray, 0, 6);
      Array.Copy((Array) BitConverter.GetBytes(ulong.MaxValue), 0, (Array) destinationArray, 6, 8);
    }
    return destinationArray;
  }

  [DBProp("ListenBeforeTalkValue", DefaultValue = 255 /*0xFF*/, AllowNull = false)]
  public int ListenBeforeTalkValue
  {
    get => this._ListenBeforeTalkValue;
    set
    {
      int listenBeforeTalkValue = this.ListenBeforeTalkValue;
      this._ListenBeforeTalkValue = value;
      if (listenBeforeTalkValue == this.ListenBeforeTalkValue || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ListenBeforeTalkValue));
    }
  }

  [DBProp("LinkAcceptance", DefaultValue = -50)]
  public int LinkAcceptance
  {
    get => this._LinkAcceptance;
    set
    {
      if (value > -30 || value < -110)
        value = -50;
      int linkAcceptance = this.LinkAcceptance;
      this._LinkAcceptance = value;
      if (linkAcceptance == this.LinkAcceptance || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (LinkAcceptance));
    }
  }

  [DBProp("CrystalStartTime", DefaultValue = 3)]
  public int CrystalStartTime
  {
    get => this._CrystalStartTime;
    set
    {
      int crystalStartTime = this.CrystalStartTime;
      this._CrystalStartTime = value;
      if (crystalStartTime == this.CrystalStartTime || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (CrystalStartTime));
    }
  }

  [DBProp("DMExchangeDelayMultiple", DefaultValue = 1)]
  public int DMExchangeDelayMultiple
  {
    get => this._DMExchangeDelayMultiple;
    set
    {
      int exchangeDelayMultiple = this.DMExchangeDelayMultiple;
      this._DMExchangeDelayMultiple = value;
      if (exchangeDelayMultiple == this.DMExchangeDelayMultiple || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (DMExchangeDelayMultiple));
    }
  }

  [DBProp("SHID1", DefaultValue = 4294967295 /*0xFFFFFFFF*/)]
  public long SHID1
  {
    get => this._SHID1;
    set
    {
      long shiD1 = this.SHID1;
      this._SHID1 = value;
      if (shiD1 == this.SHID1 || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (SHID1));
    }
  }

  [DBProp("SHID2", DefaultValue = 4294967295 /*0xFFFFFFFF*/)]
  public long SHID2
  {
    get => this._SHID2;
    set
    {
      long shiD2 = this.SHID2;
      this._SHID2 = value;
      if (shiD2 == this.SHID2 || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (SHID2));
    }
  }

  [DBProp("CryptRequired", DefaultValue = 0)]
  public int CryptRequired
  {
    get => this._CryptRequired;
    set
    {
      int cryptRequired = this.CryptRequired;
      this._CryptRequired = value;
      if (cryptRequired == this.CryptRequired || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (CryptRequired));
    }
  }

  [DBProp("Pingtime", DefaultValue = 72)]
  public int Pingtime
  {
    get => this._Pingtime;
    set
    {
      int pingtime = this.Pingtime;
      this._Pingtime = value;
      if (pingtime == this.Pingtime || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (Pingtime));
    }
  }

  [DBProp("BSNUrgentDelay", DefaultValue = 20)]
  public int BSNUrgentDelay
  {
    get => this._BSNUrgentDelay;
    set
    {
      int bsnUrgentDelay = this.BSNUrgentDelay;
      this._BSNUrgentDelay = value;
      if (bsnUrgentDelay == this.BSNUrgentDelay || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (BSNUrgentDelay));
    }
  }

  [DBProp("UrgentRetries", DefaultValue = 2)]
  public int UrgentRetries
  {
    get => this._UrgentRetries;
    set
    {
      int urgentRetries = this.UrgentRetries;
      this._UrgentRetries = value;
      if (urgentRetries == this.UrgentRetries || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (UrgentRetries));
    }
  }

  [DBProp("TransmitPower", DefaultValue = 201)]
  public int TransmitPower
  {
    get
    {
      if (this._TransmitPower == int.MinValue)
      {
        this._TransmitPower = 201;
        if (this.GenerationType.Contains("Gen2"))
          this._TransmitPower = (int) ushort.MaxValue;
      }
      return this._TransmitPower;
    }
    set
    {
      int transmitPower = this.TransmitPower;
      this._TransmitPower = value;
      if (transmitPower == this.TransmitPower || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (TransmitPower));
    }
  }

  [DBProp("TransmitPowerOptions")]
  public int TransmitPowerOptions
  {
    get
    {
      if (this._TransmitPowerOptions == int.MinValue)
      {
        this._TransmitPowerOptions = 201;
        if (this.GenerationType.Contains("Gen2"))
          this._TransmitPowerOptions = (int) ushort.MaxValue;
      }
      return this._TransmitPowerOptions;
    }
    set
    {
      int transmitPowerOptions = this.TransmitPowerOptions;
      this._TransmitPowerOptions = value;
      if (transmitPowerOptions == this.TransmitPowerOptions || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (TransmitPowerOptions));
    }
  }

  [DBProp("ReceiveSensitivity", DefaultValue = 0)]
  public int ReceiveSensitivity
  {
    get => this._ReceiveSensitivity;
    set
    {
      if (value > (int) byte.MaxValue || value < 0)
        value = 0;
      int receiveSensitivity = this.ReceiveSensitivity;
      this._ReceiveSensitivity = value;
      if (receiveSensitivity == this.ReceiveSensitivity || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ReceiveSensitivity));
    }
  }

  [DBProp("ReadGeneralConfig1", AllowNull = false, DefaultValue = false)]
  public bool ReadGeneralConfig1
  {
    get => this._ReadGeneralConfig1;
    set => this._ReadGeneralConfig1 = value;
  }

  [DBProp("ReadGeneralConfig2", AllowNull = false, DefaultValue = false)]
  public bool ReadGeneralConfig2
  {
    get => this._ReadGeneralConfig2;
    set => this._ReadGeneralConfig2 = value;
  }

  [DBProp("ReadGeneralConfig3", AllowNull = false, DefaultValue = false)]
  public bool ReadGeneralConfig3
  {
    get => this._ReadGeneralConfig3;
    set => this._ReadGeneralConfig3 = value;
  }

  [DBProp("TimeOffset", AllowNull = false, DefaultValue = 0)]
  public int TimeOffset
  {
    get => this._TimeOffset;
    set
    {
      bool flag = false;
      if (this._TimeOffset != value && !this.SuppressPropertyChangedEvent)
        flag = true;
      this._TimeOffset = value;
      if (!flag)
        return;
      this.OnPropertyChanged(nameof (TimeOffset));
    }
  }

  [DBProp("TimeOfDayControl", AllowNull = false, DefaultValue = 0)]
  public int TimeOfDayControl
  {
    get => this._TimeOfDayControl;
    set
    {
      bool flag = false;
      if (this._TimeOfDayControl != value && !this.SuppressPropertyChangedEvent)
        flag = true;
      this._TimeOfDayControl = value;
      if (!flag)
        return;
      this.OnPropertyChanged(nameof (TimeOfDayControl));
    }
  }

  [DBProp("IsDirtyProfile", AllowNull = false, DefaultValue = false)]
  public bool ProfileConfigDirty
  {
    get => this._ProfileConfigDirty;
    set
    {
      bool profileConfigDirty = this.ProfileConfigDirty;
      this._ProfileConfigDirty = value;
      if (profileConfigDirty == this.ProfileConfigDirty || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ProfileConfigDirty));
    }
  }

  [DBProp("IsDirtyProfile2", AllowNull = false, DefaultValue = false)]
  public bool ProfileConfig2Dirty
  {
    get => this._ProfileConfig2Dirty;
    set
    {
      bool profileConfig2Dirty = this.ProfileConfig2Dirty;
      this._ProfileConfig2Dirty = value;
      if (profileConfig2Dirty == this.ProfileConfig2Dirty || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (ProfileConfig2Dirty));
    }
  }

  [DBProp("ReadProfileConfig1", AllowNull = false, DefaultValue = false)]
  public bool ReadProfileConfig1
  {
    get => this._ReadProfileConfig1;
    set => this._ReadProfileConfig1 = value;
  }

  [DBProp("ReadProfileConfig2", AllowNull = false, DefaultValue = false)]
  public bool ReadProfileConfig2
  {
    get => this._ReadProfileConfig2;
    set => this._ReadProfileConfig2 = value;
  }

  [DBProp("MeasurementsPerTransmission", AllowNull = true)]
  public int MeasurementsPerTransmission
  {
    get
    {
      return this._MeasurementsPerTransmission == int.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval ? 1 : this._MeasurementsPerTransmission;
    }
    set
    {
      int measurementsPerTransmission = this.MeasurementsPerTransmission;
      this._MeasurementsPerTransmission = value;
      if (measurementsPerTransmission == this.MeasurementsPerTransmission || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (MeasurementsPerTransmission));
    }
  }

  [DBProp("TransmitOffset", AllowNull = true)]
  public int TransmitOffset
  {
    get
    {
      if (this._TransmitOffset == int.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval || this._TransmitOffset <= 0)
        return 0;
      if (this._TransmitOffset == 1)
        return 1;
      if (this._TransmitOffset <= 3)
        return 3;
      if (this._TransmitOffset <= 7)
        return 7;
      if (this._TransmitOffset <= 15)
        return 15;
      return this._TransmitOffset <= 31 /*0x1F*/ ? 31 /*0x1F*/ : 63 /*0x3F*/;
    }
    set
    {
      int transmitOffset = this.TransmitOffset;
      this._TransmitOffset = value;
      if (transmitOffset == this.TransmitOffset || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (TransmitOffset));
    }
  }

  [DBProp("Hysteresis", AllowNull = true)]
  public long Hysteresis
  {
    get
    {
      return this._Hysteresis == long.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval ? (long) uint.MaxValue : this._Hysteresis;
    }
    set
    {
      long hysteresis = this.Hysteresis;
      this._Hysteresis = value;
      if (hysteresis == this.Hysteresis || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (Hysteresis));
    }
  }

  [DBProp("MinimumThreshold", AllowNull = true)]
  public long MinimumThreshold
  {
    get
    {
      return this._MinimumThreshold == long.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval ? (long) uint.MaxValue : this._MinimumThreshold;
    }
    set
    {
      long minimumThreshold = this.MinimumThreshold;
      this._MinimumThreshold = value;
      if (minimumThreshold == this.MinimumThreshold || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (MinimumThreshold));
    }
  }

  [DBProp("MaximumThreshold", AllowNull = true)]
  public long MaximumThreshold
  {
    get
    {
      return this._MaximumThreshold == long.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval ? (long) uint.MaxValue : this._MaximumThreshold;
    }
    set
    {
      long maximumThreshold = this.MaximumThreshold;
      this._MaximumThreshold = value;
      if (maximumThreshold == this.MaximumThreshold || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (MaximumThreshold));
    }
  }

  [DBProp("Calibration1", AllowNull = true)]
  public long Calibration1
  {
    get
    {
      return this._Calibration1 == long.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval ? (long) uint.MaxValue : this._Calibration1;
    }
    set
    {
      long calibration1 = this._Calibration1;
      this._Calibration1 = value;
      if (calibration1 == this._Calibration1 || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (Calibration1));
    }
  }

  [DBProp("Calibration2", AllowNull = true)]
  public long Calibration2
  {
    get
    {
      return this._Calibration2 == long.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval ? (long) uint.MaxValue : this._Calibration2;
    }
    set
    {
      long calibration2 = this._Calibration2;
      this._Calibration2 = value;
      if (calibration2 == this._Calibration2 || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (Calibration2));
    }
  }

  [DBProp("Calibration3", AllowNull = true)]
  public long Calibration3
  {
    get
    {
      return this._Calibration3 == long.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval ? (long) uint.MaxValue : this._Calibration3;
    }
    set
    {
      long calibration3 = this._Calibration3;
      this._Calibration3 = value;
      if (calibration3 == this._Calibration3 || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (Calibration3));
    }
  }

  [DBProp("Calibration4", AllowNull = true)]
  public long Calibration4
  {
    get
    {
      return this._Calibration4 == long.MinValue && this.IsTriggerProfile == eApplicationProfileType.Interval ? (long) uint.MaxValue : this._Calibration4;
    }
    set
    {
      long calibration4 = this._Calibration4;
      this._Calibration4 = value;
      if (calibration4 == this._Calibration4 || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (Calibration4));
    }
  }

  [DBProp("EventDetectionType", AllowNull = true)]
  public int EventDetectionType
  {
    get
    {
      return this._EventDetectionType == int.MinValue && this.IsTriggerProfile == eApplicationProfileType.Trigger ? 0 : this._EventDetectionType;
    }
    set
    {
      int eventDetectionType = this.EventDetectionType;
      this._EventDetectionType = value;
      if (eventDetectionType == this.EventDetectionType || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (EventDetectionType));
    }
  }

  [DBProp("EventDetectionPeriod", AllowNull = true)]
  public int EventDetectionPeriod
  {
    get
    {
      return this._EventDetectionPeriod == int.MinValue && this.IsTriggerProfile == eApplicationProfileType.Trigger ? 100 : this._EventDetectionPeriod;
    }
    set
    {
      int eventDetectionPeriod = this.EventDetectionPeriod;
      this._EventDetectionPeriod = value;
      if (eventDetectionPeriod == this.EventDetectionPeriod || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (EventDetectionPeriod));
    }
  }

  [DBProp("EventDetectionCount", AllowNull = true)]
  public int EventDetectionCount
  {
    get
    {
      return this._EventDetectionCount == int.MinValue && this.IsTriggerProfile == eApplicationProfileType.Trigger ? 15 : this._EventDetectionCount;
    }
    set
    {
      int eventDetectionCount = this.EventDetectionCount;
      this._EventDetectionCount = value;
      if (eventDetectionCount == this.EventDetectionCount || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (EventDetectionCount));
    }
  }

  [DBProp("RearmTime", AllowNull = true)]
  public int RearmTime
  {
    get
    {
      return this._RearmTime == int.MinValue && this.IsTriggerProfile == eApplicationProfileType.Trigger ? 1 : this._RearmTime;
    }
    set
    {
      int rearmTime = this.RearmTime;
      this._RearmTime = value;
      if (rearmTime == this.RearmTime || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (RearmTime));
    }
  }

  [DBProp("BiStable", AllowNull = true)]
  public int BiStable
  {
    get
    {
      return this._BiStable == int.MinValue && this.IsTriggerProfile == eApplicationProfileType.Trigger ? 1 : this._BiStable;
    }
    set
    {
      int biStable = this.BiStable;
      this._BiStable = value;
      if (biStable == this.BiStable || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (BiStable));
    }
  }

  private eApplicationProfileType IsTriggerProfile
  {
    get
    {
      return this.MonnitApplication != null ? this.MonnitApplication.IsTriggerProfile : eApplicationProfileType.Interval;
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
      if (sensorhoodConfigDirty == this.SensorhoodConfigDirty || this.SuppressPropertyChangedEvent)
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

  [DBProp("FirmwareVersion", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public string FirmwareVersion
  {
    get
    {
      if (string.IsNullOrEmpty(this._FirmwareVersion))
      {
        try
        {
          this._FirmwareVersion = ConfigData.FindValue("DefaultFirmwareVersion");
        }
        catch
        {
          this._FirmwareVersion = "1.2";
        }
      }
      return this._FirmwareVersion;
    }
    set
    {
      string firmwareVersion = this.FirmwareVersion;
      this._FirmwareVersion = value != null ? value : string.Empty;
      if (!(firmwareVersion != this.FirmwareVersion) || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (FirmwareVersion));
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
      if (!(radioBand != this.RadioBand) || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (RadioBand));
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
      if (powerSourceId == this.PowerSourceID || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (PowerSourceID));
    }
  }

  [DBProp("IsSleeping", DefaultValue = false)]
  public bool IsSleeping
  {
    get => this._IsSleeping;
    set
    {
      bool isSleeping = this.IsSleeping;
      this._IsSleeping = value;
      if (isSleeping == this.IsSleeping || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (IsSleeping));
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

  [DBProp("eWitType", DefaultValue = 1)]
  public eWitType WitType
  {
    get => this._WitType;
    set => this._WitType = value;
  }

  [DBProp("SensorTypeID")]
  [DBForeignKey("SensorType", "SensorTypeID")]
  public long SensorTypeID
  {
    get => this._SensorTypeID;
    set => this._SensorTypeID = value;
  }

  public SensorType SensorType
  {
    get => SensorType.Load(this.SensorTypeID);
    set => this.SensorTypeID = value.SensorTypeID;
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

  [DBProp("DataLog", AllowNull = true)]
  public bool DataLog
  {
    get => this._DataLog;
    set
    {
      bool dataLog = this.DataLog;
      this._DataLog = value;
      if (dataLog == this.DataLog || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (DataLog));
    }
  }

  [DBProp("LocalConfigOptions", AllowNull = true)]
  public int LocalConfigOptions
  {
    get => this._LocalConfigOptions;
    set
    {
      if (value < 0)
        value = 0;
      int localConfigOptions = this.LocalConfigOptions;
      this._LocalConfigOptions = value;
      if (localConfigOptions == this.LocalConfigOptions || this.SuppressPropertyChangedEvent)
        return;
      this.OnPropertyChanged(nameof (LocalConfigOptions));
    }
  }

  [DBProp("TagString", MaxLength = 2000, AllowNull = true)]
  public string TagString
  {
    get => this._TagString;
    set => this._TagString = value;
  }

  [DBProp("IsDeleted")]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  [DBProp("IsNewToNetwork")]
  public bool IsNewToNetwork
  {
    get => this._IsNewToNetwork;
    set => this._IsNewToNetwork = value;
  }

  [DBProp("SensorApplicationID")]
  [DBForeignKey("SensorApplication", "SensorApplicationID")]
  public long SensorApplicationID
  {
    get => this._SensorApplicationID;
    set => this._SensorApplicationID = value;
  }

  [DBProp("CalibrationCertification", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CalibrationCertification
  {
    get => this._CalibrationCertification;
    set => this._CalibrationCertification = value;
  }

  [DBForeignKey("CalibrationFacility", "CalibrationFacilityID")]
  [DBProp("CalibrationFacilityID", AllowNull = true)]
  public long CalibrationFacilityID
  {
    get => this._CalibrationFacilityID;
    set => this._CalibrationFacilityID = value;
  }

  [DBProp("ParentID", AllowNull = true)]
  public long ParentID
  {
    get => this._ParentID;
    set => this._ParentID = value;
  }

  [DBProp("resumeNotificationDate", AllowNull = true)]
  public DateTime resumeNotificationDate
  {
    get => this._resumeNotificationDate;
    set => this._resumeNotificationDate = value;
  }

  [DBProp("Make", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Make
  {
    get => this._Make;
    set
    {
      if (value == null)
        this._Make = string.Empty;
      else
        this._Make = value;
    }
  }

  [DBProp("Model", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Model
  {
    get => this._Model;
    set
    {
      if (value == null)
        this._Model = string.Empty;
      else
        this._Model = value;
    }
  }

  [DBProp("SerialNumber", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SerialNumber
  {
    get => this._SerialNumber;
    set
    {
      if (value == null)
        this._SerialNumber = string.Empty;
      else
        this._SerialNumber = value;
    }
  }

  [DBProp("Location", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Location
  {
    get => this._Location;
    set
    {
      if (value == null)
        this._Location = string.Empty;
      else
        this._Location = value;
    }
  }

  [DBProp("Description", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set
    {
      if (value == null)
        this._Description = string.Empty;
      else
        this._Description = value;
    }
  }

  [DBProp("Note", MaxLength = 2000, AllowNull = true)]
  public string Note
  {
    get => this._Note;
    set
    {
      if (value == null)
        this._Note = string.Empty;
      else
        this._Note = value;
    }
  }

  [DBProp("SensorFirmwareID")]
  [DBForeignKey("SensorFirmware", "SensorFirmwareID")]
  public long SensorFirmwareID
  {
    get => this._SensorFirmwareID;
    set => this._SensorFirmwareID = value;
  }

  [DBProp("SendFirmwareUpdate")]
  public bool SendFirmwareUpdate
  {
    get => this._SendFirmwareUpdate;
    set => this._SendFirmwareUpdate = value;
  }

  [DBProp("FirmwareUpdateInProgress")]
  public bool FirmwareUpdateInProgress
  {
    get => this._FirmwareUpdateInProgress;
    set => this._FirmwareUpdateInProgress = value;
  }

  [DBProp("FirmwareDownloadComplete")]
  public bool FirmwareDownloadComplete
  {
    get => this._FirmwareDownloadComplete;
    set => this._FirmwareDownloadComplete = value;
  }

  [DBProp("GenerationType", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string GenerationType
  {
    get
    {
      if (string.IsNullOrEmpty(this._GenerationType))
        this._GenerationType = "Gen1";
      return this._GenerationType;
    }
    set => this._GenerationType = value;
  }

  [DBProp("SKU", MaxLength = 75, AllowNull = true)]
  public string SKU
  {
    get => this._SKU.ToUpper();
    set
    {
      if (value == null)
        this._SKU = string.Empty;
      else
        this._SKU = value;
    }
  }

  [DBProp("SensorPrint", AllowNull = true)]
  public byte[] SensorPrint
  {
    get
    {
      if (this._SensorPrint == null)
        this._SensorPrint = new byte[32 /*0x20*/];
      return this._SensorPrint;
    }
    set
    {
      bool flag = false;
      if (!this.SuppressPropertyChangedEvent && !this.EqualToSensorPrint(value))
        flag = true;
      this._SensorPrint = value;
      if (!flag)
        return;
      this.OnPropertyChanged(nameof (SensorPrint));
    }
  }

  public bool SensorPrintActive
  {
    get
    {
      return this._SensorPrint != null && !this.SensorPrintDirty && !this.EqualToSensorPrint(new byte[32 /*0x20*/]);
    }
  }

  [DBProp("SensorPrintDirty")]
  public bool SensorPrintDirty
  {
    get => this._SensorPrintDirty;
    set
    {
      Version version = new Version(this.FirmwareVersion);
      if (version.Major >= 16 /*0x10*/ && version.Minor >= 34 && this.GenerationType.Contains("Gen2"))
        this._SensorPrintDirty = value;
      else
        this._SensorPrintDirty = false;
    }
  }

  public bool isPaused() => this.resumeNotificationDate > DateTime.UtcNow;

  public bool IsDirty
  {
    get
    {
      return this._GeneralConfigDirty || this._GeneralConfig2Dirty || this._GeneralConfig3Dirty || this._ProfileConfigDirty || this._ProfileConfig2Dirty || this._RFConfig1Dirty;
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
    set
    {
      this._PowerSource = value;
      this.PowerSourceID = value.PowerSourceID;
    }
  }

  public bool CanUpdate
  {
    get
    {
      if (this.SensorTypeID == 4L || this.SensorTypeID == 8L)
      {
        Gateway gateway = Gateway.LoadBySensorID(this.SensorID);
        if (gateway != null && gateway.IsDirty)
          return false;
      }
      return this.MonnitApplicationID == 13L || this.MonnitApplicationID == 12L ? !this.IsDirty && !this.ForceToBootloader : (!this.IsDirty || this.LastCommunicationDate == new DateTime(2099, 1, 1)) && !this.PendingActionControlCommand && !this.ForceToBootloader;
    }
  }

  public DateTime NextCommunicationDate
  {
    get
    {
      double num1 = this.ReportInterval;
      if (this.LastDataMessage != null && (this.LastDataMessage.State & 2) == 2)
        num1 = this.ActiveStateInterval;
      DateTime communicationDate = this._LastCommunicationDate.AddMinutes(num1);
      if (communicationDate < DateTime.UtcNow.AddDays(-1.0))
        return DateTime.MinValue;
      for (int index = 0; communicationDate < DateTime.UtcNow && (index < this.RetryCount - 1 || this.SensorTypeID == 4L || this.SensorTypeID == 8L); ++index)
        communicationDate = communicationDate.AddMinutes(num1);
      if (this.GenerationType.ToUpper().Contains("GEN1") && new Version(this.FirmwareVersion) <= new Version(2, 3, 0, 0))
      {
        for (; communicationDate < DateTime.UtcNow; communicationDate = this.TransmitIntervalLink <= 100 ? communicationDate.AddHours((double) this.TransmitIntervalLink) : communicationDate.AddMinutes((double) (this.TransmitIntervalLink - 100)))
        {
          if (this.TransmitIntervalLink < 0)
            this.TransmitIntervalLink = 2;
        }
      }
      else
      {
        int num2 = 1;
        while (communicationDate < DateTime.UtcNow)
        {
          switch (num2)
          {
            case 1:
              communicationDate = communicationDate.AddMinutes(1.0);
              break;
            case 2:
              communicationDate = communicationDate.AddMinutes(10 < this.TransmitIntervalLink ? 10.0 : (double) this.TransmitIntervalLink);
              break;
            case 3:
              communicationDate = communicationDate.AddMinutes(30 < this.TransmitIntervalLink ? 30.0 : (double) this.TransmitIntervalLink);
              break;
            default:
              communicationDate = communicationDate.AddMinutes((double) this.TransmitIntervalLink);
              break;
          }
          ++num2;
        }
      }
      return communicationDate;
    }
  }

  public bool EstimatedLinkMode
  {
    get
    {
      double num = this.ReportInterval;
      if (this.LastDataMessage != null && (this.LastDataMessage.State & 2) == 2)
        num = this.ActiveStateInterval;
      DateTime dateTime = this._LastCommunicationDate.AddMinutes(num);
      for (int index = 0; dateTime < DateTime.UtcNow && index < this.RetryCount - 1; ++index)
        dateTime = dateTime.AddMinutes(num);
      return dateTime < DateTime.UtcNow;
    }
  }

  [DBProp("TemplateSensorID", AllowNull = true)]
  public long TemplateSensorID
  {
    get => this._TemplateSensorID;
    set => this._TemplateSensorID = value;
  }

  [DBProp("WarrantyStartDate")]
  public DateTime WarrantyStartDate
  {
    get => this._WarrantyStartDate;
    set => this._WarrantyStartDate = value;
  }

  [DBProp("IsCableEnabled", AllowNull = false, DefaultValue = false)]
  public bool IsCableEnabled
  {
    get => this._IsCableEnabled;
    set => this._IsCableEnabled = value;
  }

  [DBProp("CableID", AllowNull = true)]
  [DBForeignKey("Cable", "CableID")]
  public long CableID
  {
    get => this._CableID;
    set => this._CableID = value;
  }

  public static void ClearCache(long ID)
  {
    TimedCache.RemoveObject($"Sensor_{ID}");
    DataMessage.ClearLastBySensorCached(ID);
  }

  public void AddToCache(int cacheSeconds = 15)
  {
    TimedCache.AddObjectToCach($"Sensor_{this.SensorID}", (object) this, new TimeSpan(0, 0, cacheSeconds));
  }

  public static Sensor Load(long ID)
  {
    if (ID == long.MinValue)
      return (Sensor) null;
    string key = $"Sensor_{ID}";
    Sensor sensor = TimedCache.RetrieveObject<Sensor>(key);
    if (sensor == null)
    {
      sensor = BaseDBObject.Load<Sensor>(ID);
      if (sensor != null)
      {
        sensor.SuppressPropertyChangedEvent = false;
        TimedCache.AddObjectToCach(key, (object) sensor, new TimeSpan(0, 0, 15));
      }
    }
    return sensor;
  }

  public override void Save()
  {
    this.CheckLimits();
    if (new Version(this.FirmwareVersion) < new Version("2.0.0.0") && !this.IsWiFiSensor)
    {
      if (this.GeneralConfig2Dirty)
      {
        this.GeneralConfigDirty = true;
        this.GeneralConfig2Dirty = false;
      }
      if (this.ProfileConfig2Dirty)
      {
        this.ProfileConfigDirty = true;
        this.ProfileConfig2Dirty = false;
      }
    }
    TimedCache.RemoveObject($"Sensor_{this.SensorID}");
    if (this.PrimaryKeyValue > 0L)
      base.Save();
    else
      new Exception("Invalid SensorID").Log("Sensor.Save() ");
  }

  public void SetDatumName(int datumindex, string name)
  {
    SensorAttribute.SetDatumName(this.SensorID, datumindex, name);
  }

  public string GetDatumName(int datumindex)
  {
    string datumName = SensorAttribute.GetDatumName(this.SensorID, datumindex);
    if (datumName == null)
    {
      if (this.GetDatumTypes().Count == 1)
      {
        datumName = this.SensorName;
      }
      else
      {
        try
        {
          datumName = MonnitApplicationBase.GetAppDatums(this.ApplicationID)[datumindex].type;
        }
        catch
        {
          if (this.SensorName != null)
            return this.SensorName;
          datumName = this.GetDatumTypes()[datumindex].ToString();
        }
      }
    }
    return datumName;
  }

  public string GetOnlyDatumName(int datumindex)
  {
    string onlyDatumName = SensorAttribute.GetDatumName(this.SensorID, datumindex);
    if (onlyDatumName == null)
    {
      if (this.GetDatumTypes().Count == 1)
      {
        onlyDatumName = this.DatumTypes[0].ToString();
      }
      else
      {
        try
        {
          onlyDatumName = MonnitApplicationBase.GetAppDatums(this.ApplicationID)[datumindex].type;
        }
        catch
        {
          if (this.SensorName != null)
            return this.SensorName;
          onlyDatumName = this.GetDatumTypes()[datumindex].ToString();
        }
      }
    }
    return onlyDatumName;
  }

  public string GetBooleanTriggeredName(int datumindex = 0)
  {
    return MonnitApplicationBase.GetAppDatums(this.ApplicationID)[datumindex].type;
  }

  public static string GetDatumName(long sensorID, int datumindex)
  {
    return Sensor.Load(sensorID).GetDatumName(datumindex);
  }

  public List<Notification> NotificationsForDatum(int datumindex)
  {
    return Notification.NotificationsForDatum(this.SensorID, datumindex);
  }

  public static List<Notification> NotificationsForDatum(long sensorID, int datumindex)
  {
    return Notification.NotificationsForDatum(sensorID, datumindex);
  }

  public string GetDatumDefaultName(int datumindex) => this.GetDatumTypes()[datumindex].ToString();

  public List<eDatumType> GetDatumTypes()
  {
    if (this.DatumTypes == null)
      this.DatumTypes = MonnitApplicationBase.GetDatumTypes(this.ApplicationID);
    return this.DatumTypes;
  }

  public List<eDatumStruct> GetDatumStructs()
  {
    if (this.DatumStructs == null)
    {
      this.DatumStructs = this.GetDatumTypes().Select<eDatumType, eDatumStruct>((Func<eDatumType, int, eDatumStruct>) ((dt, index) => new eDatumStruct(dt, index))).ToList<eDatumStruct>();
      for (int index = 0; index < this.DatumStructs.Count; ++index)
      {
        eDatumStruct datumStruct = this.DatumStructs[index];
        string datumName = this.GetDatumName(datumStruct.datumindex);
        if (datumName != null && datumName != datumStruct.customname)
          datumStruct.customname = datumName;
      }
    }
    return this.DatumStructs;
  }

  public eDatumType getDatumType(int datumindex)
  {
    if (this.DatumTypes == null)
      this.DatumTypes = MonnitApplicationBase.GetDatumTypes(this.ApplicationID);
    return this.DatumTypes[datumindex];
  }

  public void ResetLastCommunicationDate()
  {
    Monnit.Data.Sensor.ClearLastCommunicationDate communicationDate = new Monnit.Data.Sensor.ClearLastCommunicationDate(this.SensorID);
  }

  public override void Delete()
  {
    this.IsDeleted = true;
    this.Save();
    foreach (Notification notification in Notification.LoadBySensorID(this.SensorID))
      notification.RemoveSensor(this);
  }

  public static void DeleteAncillaryObjects(long sensorID)
  {
    Monnit.Data.Sensor.DeleteAncillaryObjects ancillaryObjects = new Monnit.Data.Sensor.DeleteAncillaryObjects(sensorID);
  }

  public static long AppBaseID(long sensorID)
  {
    string key = $"AppBaseID_{sensorID}";
    long num = TimedCache.RetrieveObject<long>(key);
    try
    {
      if (num <= 0L)
      {
        num = Sensor.Load(sensorID).ApplicationID;
        if (num > 0L)
          TimedCache.AddObjectToCach(key, (object) num, new TimeSpan(365, 0, 0, 0, 0));
      }
    }
    catch
    {
    }
    return num;
  }

  public event PropertyChangedEventHandler PropertyChanged;

  public MonnitApplication MonnitApplication
  {
    get
    {
      try
      {
        if (this._MonnitApplication == null)
          this._MonnitApplication = MonnitApplication.Load(this.ApplicationID);
        return this._MonnitApplication;
      }
      catch (Exception ex)
      {
        try
        {
          ExceptionLog.Log(ex);
        }
        catch
        {
        }
        return (MonnitApplication) null;
      }
    }
    set
    {
      this._MonnitApplication = value;
      this.ApplicationID = value.ApplicationID;
    }
  }

  public string ApplicationName
  {
    get => this.MonnitApplication == null ? "" : this.MonnitApplication.ApplicationName;
  }

  public SensorApplication SensorApplication
  {
    get
    {
      if (this._SensorApplication == null)
        this._SensorApplication = SensorApplication.Load(this.SensorApplicationID);
      return this._SensorApplication;
    }
    set
    {
      this._SensorApplication = value;
      this.SensorApplicationID = value.SensorApplicationID;
    }
  }

  public string SensorApplicationName
  {
    get => this.SensorApplication == null ? "" : this.SensorApplication.Name;
  }

  public DataMessage LastDataMessage
  {
    get
    {
      try
      {
        Guid lastDataMessageGuid = this.LastDataMessageGUID;
        if (this.LastDataMessageGUID != Guid.Empty && this.LastCommunicationDate > DateTime.UtcNow.AddMonths(-12) && !this._QuickCacheChecked && (this._LastDataMessage == null || this._LastDataMessageGUID != this._LastDataMessage.DataMessageGUID))
        {
          this._LastDataMessage = DataMessage.LoadLastBySensorQuickCached(this);
          this._QuickCacheChecked = true;
        }
        return this._LastDataMessage;
      }
      catch (Exception ex)
      {
        try
        {
          ex.Log("Sensor.LastDataMessage");
        }
        catch
        {
        }
        return (DataMessage) null;
      }
    }
    set
    {
      DataMessage.ClearLastBySensorCached(this.SensorID);
      this._QuickCacheChecked = false;
      this._LastDataMessage = value;
      if (value == null)
        return;
      this.LastDataMessageGUID = value.DataMessageGUID;
    }
  }

  public eSensorStatus Status
  {
    get
    {
      if (!this.IsActive || this.IsSleeping || this.LastDataMessage == null)
        return eSensorStatus.Offline;
      if (this.LastDataMessage.MeetsNotificationRequirement)
        return eSensorStatus.Alert;
      DateTime dateTime = this.LastCommunicationDate.AddMinutes(this.ReportInterval * 2.0);
      if (dateTime.AddHours(2.0) < DateTime.UtcNow)
        return eSensorStatus.Offline;
      dateTime = this.LastCommunicationDate;
      if (dateTime.AddHours(-2.0) > DateTime.UtcNow)
        return eSensorStatus.Offline;
      MonnitApplicationBase monnitApplicationBase = MonnitApplicationBase.LoadMonnitApplication(this.FirmwareVersion, this.LastDataMessage.Data, this.ApplicationID, this.SensorID);
      return this.LastDataMessage.Battery < 10 || !monnitApplicationBase.IsValid ? eSensorStatus.Warning : eSensorStatus.OK;
    }
  }

  public bool IsInSleepWindow(DateTime dateTime)
  {
    if (this.ActiveStartTime == this.ActiveEndTime)
      return false;
    TimeSpan timeSpan = dateTime.Subtract(dateTime.Date);
    return (!(this.ActiveStartTime < this.ActiveEndTime) || !(timeSpan >= this.ActiveStartTime) || !(timeSpan <= this.ActiveEndTime)) && (!(this.ActiveStartTime > this.ActiveEndTime) || !(timeSpan >= this.ActiveStartTime) && !(timeSpan <= this.ActiveEndTime));
  }

  public string[] Tags
  {
    get => this.TagString.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
  }

  public void AddTag(string tag)
  {
    this.TagString = $"{this.TagString}|{tag}";
    this.Save();
  }

  public void RemoveTag(string tag)
  {
    string[] tags = this.Tags;
    this.TagString = string.Empty;
    foreach (string str in tags)
    {
      if (str.ToLower().Trim() != tag.ToLower().Trim())
        this.TagString = $"{this.TagString}|{str}";
    }
    this.Save();
  }

  public bool HasTag(string tag)
  {
    foreach (string tag1 in this.Tags)
    {
      if (tag1.ToLower().Trim() == tag.ToLower().Trim())
        return true;
    }
    return false;
  }

  public void OnPropertyChanged(string propertyName)
  {
    try
    {
      if (this.PropertyChanged != null)
        this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
    if (propertyName == "ReportInterval")
      this.GeneralConfigDirty = true;
    if (propertyName == "ActiveStateInterval")
      this.GeneralConfigDirty = true;
    if (propertyName == "ChannelMask")
      this.GeneralConfigDirty = true;
    if (propertyName == "StandardMessageDelay")
      this.GeneralConfigDirty = true;
    if (propertyName == "TransmitIntervalLink")
      this.GeneralConfigDirty = true;
    if (propertyName == "TransmitIntervalTest")
      this.GeneralConfigDirty = true;
    if (propertyName == "TestTransmitCount")
      this.GeneralConfigDirty = true;
    if (propertyName == "MaximumNetworkHops")
      this.GeneralConfigDirty = true;
    if (propertyName == "RetryCount")
      this.GeneralConfig2Dirty = true;
    if (propertyName == "Recovery")
      this.GeneralConfig2Dirty = true;
    if (propertyName == "TimeOfDayActive")
      this.GeneralConfig2Dirty = true;
    if (propertyName == "ListenBeforeTalk")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "ListenBeforeTalkValue")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "LinkAcceptance")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "CrystalStartTime")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "DMExchangeDelayMultiple")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "SHID1")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "SHID2")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "CryptRequired")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "Pingtime")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "BSNUrgentDelay")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "UrgentRetries")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "TimeOffset")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "TimeOfDayControl")
      this.GeneralConfig3Dirty = true;
    if (propertyName == "TransmitPower")
      this.RFConfig1Dirty = true;
    if (propertyName == "TransmitPowerOptions")
      this.RFConfig1Dirty = true;
    if (propertyName == "ReceiveSensitivity")
      this.RFConfig1Dirty = true;
    if (propertyName == "MeasurementsPerTransmission")
      this.ProfileConfigDirty = true;
    if (propertyName == "TransmitOffset")
      this.ProfileConfigDirty = true;
    if (propertyName == "Hysteresis")
      this.ProfileConfigDirty = true;
    if (propertyName == "MinimumThreshold")
      this.ProfileConfigDirty = true;
    if (propertyName == "MaximumThreshold")
      this.ProfileConfigDirty = true;
    if (propertyName == "Calibration1")
      this.ProfileConfig2Dirty = true;
    if (propertyName == "Calibration2")
      this.ProfileConfig2Dirty = true;
    if (propertyName == "Calibration3")
      this.ProfileConfig2Dirty = true;
    if (propertyName == "Calibration4")
      this.ProfileConfig2Dirty = true;
    if (propertyName == "EventDetectionType")
      this.ProfileConfigDirty = true;
    if (propertyName == "EventDetectionPeriod")
      this.ProfileConfigDirty = true;
    if (propertyName == "EventDetectionCount")
      this.ProfileConfigDirty = true;
    if (propertyName == "RearmTime")
      this.ProfileConfigDirty = true;
    if (propertyName == "BiStable")
      this.ProfileConfigDirty = true;
    if (propertyName == "SensorhoodID")
      this.SensorhoodConfigDirty = true;
    if (propertyName == "SensorhoodTransmitions")
      this.SensorhoodConfigDirty = true;
    if (propertyName == "DataLog")
      this.DataLogChanged = true;
    if (!(propertyName == "SensorPrint"))
      return;
    this.SensorPrintDirty = true;
  }

  public bool DataLogChanged { get; set; }

  public static List<Sensor> LoadByAccountID(long accountID)
  {
    return new Monnit.Data.Sensor.LoadByAccountID(accountID).Result;
  }

  public static List<Sensor> LoadByCsNetID(long cSNetID)
  {
    return BaseDBObject.Load<Sensor>(Sensor.LoadTableByCsNetID(cSNetID));
  }

  public static DataTable LoadTableByCsNetID(long cSNetID)
  {
    return new Monnit.Data.Sensor.LoadByCSNetID(cSNetID).Result;
  }

  public static List<Sensor> LoadByApplicationID(long applicationID, int limit)
  {
    return new Monnit.Data.Sensor.LoadByApplicationID(applicationID, limit).Result;
  }

  public static List<Sensor> LoadByAccountIDAndApplicationID(long accountID, long applicationID)
  {
    return new Monnit.Data.Sensor.LoadByAccountIDAndApplicationID(accountID, applicationID).Result;
  }

  public static List<Sensor> LoadByAccountIDAndApplicationIDIncludeAllSubAccounts(
    long accountID,
    long applicationID)
  {
    return new Monnit.Data.Sensor.LoadByAccountIDAndApplicationIDIncludeAllSubAccounts(accountID, applicationID).Result;
  }

  public static List<Sensor> LoadDirtyByCsNetID(long cSNetID)
  {
    return BaseDBObject.Load<Sensor>(Sensor.LoadTableDirtyByCsNetID(cSNetID));
  }

  public static List<Sensor> LoadByCalibrationFacilityID(long calibrationFacilityID)
  {
    return BaseDBObject.Load<Sensor>(Sensor.LoadTablesCalibrationFacilityID(calibrationFacilityID));
  }

  public static DataTable LoadTablesCalibrationFacilityID(long calibrationFacilityID)
  {
    return new Monnit.Data.Sensor.LoadByCalibrationFacilityID(calibrationFacilityID).Result;
  }

  public static DataTable LoadTableDirtyByCsNetID(long cSNetID)
  {
    return new Monnit.Data.Sensor.LoadDirtyByCsNetID(cSNetID).Result;
  }

  public static List<Sensor> LoadByCSNetIDAndApplicationID(long cSNetID, long applicationID)
  {
    return new Monnit.Data.Sensor.LoadByCSNetIDAndApplicationID(cSNetID, applicationID).Result;
  }

  public static Sensor LoadByCableID(long cableID)
  {
    return BaseDBObject.LoadByForeignKey<Sensor>("CableID", (object) cableID).FirstOrDefault<Sensor>();
  }

  public void ForceInsert()
  {
    Monnit.Data.Sensor.ForceInsert forceInsert = new Monnit.Data.Sensor.ForceInsert(this);
  }

  public void MarkClean(bool autoSave)
  {
    this.GeneralConfigDirty = false;
    this.GeneralConfig2Dirty = false;
    this.GeneralConfig3Dirty = false;
    this.ProfileConfigDirty = false;
    this.ProfileConfig2Dirty = false;
    this.RFConfig1Dirty = false;
    this.PendingActionControlCommand = false;
    if (!autoSave)
      return;
    this.Save();
  }

  public static string ToXMLFull(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    return sensor != null ? sensor.ToXMLFull() : "";
  }

  public string ToXMLFull()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<Sensor ");
    stringBuilder.AppendFormat("id = {0} ", (object) this.SensorID);
    stringBuilder.AppendFormat("applicationID = {0} ", (object) this.MonnitApplicationID);
    stringBuilder.AppendFormat("application = {0} ", (object) this.MonnitApplication.ApplicationName);
    stringBuilder.AppendFormat("networkID = {0} ", (object) this.CSNetID);
    stringBuilder.AppendFormat("name = {0} ", (object) this.SensorName);
    stringBuilder.AppendFormat("reportInterval = {0} ", (object) this.ReportInterval);
    stringBuilder.AppendFormat("activeStateInterval = {0} ", (object) this.ActiveStateInterval);
    stringBuilder.AppendFormat("minimumCommunicationFrequency = {0} ", (object) this.MinimumCommunicationFrequency);
    stringBuilder.AppendFormat("lastCommunicationDate = {0} ", (object) this.LastCommunicationDate);
    stringBuilder.AppendFormat("generalConfigDirty = {0} ", (object) this.GeneralConfigDirty);
    stringBuilder.AppendFormat("channelMask = {0} ", (object) this.ChannelMask);
    stringBuilder.AppendFormat("standardMessageDelay = {0} ", (object) this.StandardMessageDelay);
    stringBuilder.AppendFormat("transmitIntervalLink = {0} ", (object) this.TransmitIntervalLink);
    stringBuilder.AppendFormat("transmitIntervalTest = {0} ", (object) this.TransmitIntervalTest);
    stringBuilder.AppendFormat("testTransmitCount = {0} ", (object) this.TestTransmitCount);
    stringBuilder.AppendFormat("maximumNetworkHops = {0} ", (object) this.MaximumNetworkHops);
    stringBuilder.AppendFormat("retryCount = {0} ", (object) this.RetryCount);
    stringBuilder.AppendFormat("recovery = {0} ", (object) this.Recovery);
    stringBuilder.AppendFormat("timeOfDayActive = {0} ", (object) this.TimeOfDayActive);
    stringBuilder.AppendFormat("measurementsPerTransmission = {0} ", (object) this.MeasurementsPerTransmission);
    stringBuilder.AppendFormat("transmitOffset = {0} ", (object) this.TransmitOffset);
    stringBuilder.AppendFormat("hysterisis = {0} ", (object) this.Hysteresis);
    stringBuilder.AppendFormat("minimumThreshold = {0} ", (object) this.MinimumThreshold);
    stringBuilder.AppendFormat("maximunThreshold = {0} ", (object) this.MaximumThreshold);
    stringBuilder.AppendFormat("calibration1 = {0} ", (object) this.Calibration1);
    stringBuilder.AppendFormat("calibration2 = {0} ", (object) this.Calibration2);
    stringBuilder.AppendFormat("calibration3 = {0} ", (object) this.Calibration3);
    stringBuilder.AppendFormat("calibration4 = {0} ", (object) this.Calibration4);
    stringBuilder.AppendFormat("eventDetectionType = {0} ", (object) this.EventDetectionType);
    stringBuilder.AppendFormat("eventDetectionPeriod = {0} ", (object) this.EventDetectionPeriod);
    stringBuilder.AppendFormat("eventDetectionCount = {0} ", (object) this.EventDetectionCount);
    stringBuilder.AppendFormat("rearmTime = {0} ", (object) this.RearmTime);
    stringBuilder.AppendFormat("biStable = {0} ", (object) this.BiStable);
    stringBuilder.AppendFormat("PendingActionControlCommand = {0} ", (object) this.PendingActionControlCommand);
    stringBuilder.AppendFormat("isActive = {0} ", (object) this.IsActive);
    stringBuilder.AppendFormat("firmwareVersion = {0} ", (object) this.FirmwareVersion);
    stringBuilder.AppendFormat("radioBand = {0} ", (object) this.RadioBand);
    stringBuilder.AppendFormat("powerSourceID = {0} ", (object) this.PowerSourceID);
    stringBuilder.AppendFormat("powerSource = {0} ", (object) this.PowerSource.Name);
    stringBuilder.AppendFormat("externalID = {0} ", (object) this.ExternalID);
    stringBuilder.AppendFormat("profileConfigDirty = {0} ", (object) this.ProfileConfigDirty);
    stringBuilder.Append("/>");
    return stringBuilder.ToString();
  }

  public static string ToXML(long sensorID)
  {
    Sensor sensor = Sensor.Load(sensorID);
    return sensor != null ? sensor.ToXML() : "";
  }

  public string ToXML()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.Append("<Sensor ");
    stringBuilder.AppendFormat("id = {0} ", (object) this.SensorID);
    stringBuilder.AppendFormat("externalID = {0} ", (object) this.ExternalID);
    stringBuilder.AppendFormat("name = {0} ", (object) this.SensorName);
    stringBuilder.AppendFormat("status = {0} ", (object) this.Status);
    stringBuilder.AppendFormat("application = {0} ", (object) this.MonnitApplication.ApplicationName);
    stringBuilder.AppendFormat("networkID = {0} ", (object) this.CSNetID);
    stringBuilder.AppendFormat("reportInterval = {0} ", (object) this.ReportInterval);
    stringBuilder.AppendFormat("activeStateInterval = {0} ", (object) this.ActiveStateInterval);
    stringBuilder.AppendFormat("minimumCommunicationFrequency = {0} ", (object) this.MinimumCommunicationFrequency);
    stringBuilder.AppendFormat("lastCommunicationDate = {0} ", (object) this.LastCommunicationDate);
    stringBuilder.AppendFormat("minimumThreshold = {0} ", (object) this.MinimumThreshold);
    stringBuilder.AppendFormat("maximunThreshold = {0} ", (object) this.MaximumThreshold);
    stringBuilder.AppendFormat("firmwareVersion = {0} ", (object) this.FirmwareVersion);
    stringBuilder.AppendFormat("radioBand = {0} ", (object) this.RadioBand);
    stringBuilder.AppendFormat("powerSource = {0} ", (object) this.PowerSource.Name);
    stringBuilder.AppendFormat("pendingTransaction = {0} ", (object) !this.CanUpdate);
    stringBuilder.AppendFormat("cableID = {0} ", (object) this.CableID);
    stringBuilder.AppendFormat("IsCableEnabled = {0} ", (object) this.IsCableEnabled);
    stringBuilder.Append("/>");
    return stringBuilder.ToString();
  }

  public TimeSpan ActiveStartTime
  {
    get
    {
      bool flag = false;
      for (int index1 = 0; index1 < 12; ++index1)
      {
        for (int index2 = 0; index2 < 8; ++index2)
        {
          int num = ((int) this.TimeOfDayActive[index1] & 1 << index2) >> index2;
          if (num == 0)
            flag = true;
          if (flag && num == 1)
            return new TimeSpan(0, (index1 * 8 + index2) * 15, 0);
        }
      }
      return new TimeSpan(0, 0, 0);
    }
  }

  public TimeSpan ActiveEndTime
  {
    get
    {
      bool flag = false;
      for (int index1 = 0; index1 < 12; ++index1)
      {
        for (int index2 = 0; index2 < 8; ++index2)
        {
          int num = ((int) this.TimeOfDayActive[index1] & 1 << index2) >> index2;
          if (num == 1)
            flag = true;
          if (flag && num == 0)
            return new TimeSpan(0, (index1 * 8 + index2) * 15, 0);
        }
      }
      return new TimeSpan(0, 0, 0);
    }
  }

  public void SetTimeOfDayActive(TimeSpan startTime, TimeSpan endTime)
  {
    while (startTime.TotalHours < 0.0)
      startTime = startTime.Add(new TimeSpan(24, 0, 0));
    while (startTime.TotalHours >= 24.0)
      startTime = startTime.Subtract(new TimeSpan(24, 0, 0));
    while (endTime.TotalHours < 0.0)
      endTime = endTime.Add(new TimeSpan(24, 0, 0));
    while (endTime.TotalHours >= 24.0)
      endTime = endTime.Subtract(new TimeSpan(24, 0, 0));
    int num1 = (int) startTime.TotalMinutes / 15;
    int num2 = num1 / 8;
    int num3 = num1 % 8;
    int num4 = (int) endTime.TotalMinutes / 15;
    int num5 = num4 / 8;
    int num6 = num4 % 8;
    byte[] destinationArray = new byte[12];
    if (this.GenerationType.ToUpper().Contains("GEN2"))
    {
      destinationArray = new byte[14];
      Array.Copy((Array) this.TimeOfDayActive, (Array) destinationArray, 14);
    }
    else
      Array.Copy((Array) this.TimeOfDayActive, (Array) destinationArray, 12);
    if (startTime == endTime)
    {
      this.TimeOfDayActive = Sensor.DefaultTimeOfDayBytes(this.GenerationType);
      this.TimeOfDayControl = 0;
    }
    else if (startTime < endTime)
    {
      this.SetPartialTimeOfDayActive(0, 0, 0, num2, num3);
      this.SetPartialTimeOfDayActive(num2, num3, 1, num5, num6);
      this.SetPartialTimeOfDayActive(num5, num6, 0, 11, 7);
      this.TimeOfDayControl = 1;
    }
    else if (endTime < startTime)
    {
      this.SetPartialTimeOfDayActive(0, 0, 1, num5, num6);
      this.SetPartialTimeOfDayActive(num5, num6, 0, num2, num3);
      this.SetPartialTimeOfDayActive(num2, num3, 1, 11, 7);
      this.TimeOfDayControl = 1;
    }
    if (this.EqualToTimeOfDayActive(destinationArray))
      return;
    this.OnPropertyChanged("TimeOfDayActive");
  }

  private void SetPartialTimeOfDayActive(
    int fromByte,
    int fromBit,
    int value,
    int toByte,
    int toBit)
  {
    for (int index1 = fromByte; index1 <= toByte; ++index1)
    {
      int num1 = 0;
      int num2 = 7;
      if (index1 == fromByte)
        num1 = fromBit;
      if (index1 == toByte)
        num2 = toBit;
      for (int index2 = num2; index2 >= num1; --index2)
        this.TimeOfDayActive[index1] = value != 1 ? Convert.ToByte((int) this.TimeOfDayActive[index1] & ~(1 << index2)) : Convert.ToByte((int) this.TimeOfDayActive[index1] | 1 << index2);
    }
  }

  public bool EqualToTimeOfDayActive(byte[] value)
  {
    if (value == null)
      return this.EqualToTimeOfDayActive(Sensor.DefaultTimeOfDayBytes(this.GenerationType));
    for (int index = 0; index < (this.GenerationType.ToUpper().Contains("GEN2") ? 14 : 12) - 1; ++index)
    {
      if ((int) value[index] != (int) this.TimeOfDayActive[index])
        return false;
    }
    return true;
  }

  public bool TimeOfDayActiveAllOn(byte[] value)
  {
    if (this.GenerationType.ToUpper().Contains("GEN2"))
    {
      for (int index = 0; index < 13; index += 2)
      {
        value[index].ToInt();
        value[index + 1].ToInt();
        if (value[index].ToInt() != 0 && value[index].ToInt() != (int) byte.MaxValue || value[index + 1].ToInt() != 96 /*0x60*/ && value[index + 1].ToInt() != (int) byte.MaxValue)
          return false;
      }
    }
    else
    {
      for (int index = 0; index < 11; index += 2)
      {
        if (value[index].ToInt() != (int) byte.MaxValue && value[index + 1].ToInt() != (int) byte.MaxValue)
          return false;
      }
    }
    return true;
  }

  public bool EqualToSensorPrint(byte[] value)
  {
    if (value == null || value.Length != this.SensorPrint.Length)
      return false;
    for (int index = 0; index < this.SensorPrint.Length; ++index)
    {
      if ((int) value[index] != (int) this.SensorPrint[index])
        return false;
    }
    return true;
  }

  public static void EnablePropertyChangedEvent(List<Sensor> list)
  {
    foreach (Sensor sensor in list)
      sensor.SuppressPropertyChangedEvent = false;
  }

  public void CheckLimits()
  {
    if (this.ReportInterval < 0.017)
      this.ReportInterval = 0.017;
    if (this.ReportInterval > 720.0)
      this.ReportInterval = 720.0;
    if (this.ActiveStateInterval < 0.017)
      this.ActiveStateInterval = 0.017;
    if (this.ActiveStateInterval > this.ReportInterval)
      this.ActiveStateInterval = this.ReportInterval;
    if (this.MeasurementsPerTransmission < 1)
      this.MeasurementsPerTransmission = 1;
    if (this.MeasurementsPerTransmission > 250)
      this.MeasurementsPerTransmission = 250;
    while (this.ActiveStateInterval / (double) this.MeasurementsPerTransmission < 0.017 || this.ReportInterval / (double) this.MeasurementsPerTransmission < 0.017)
      --this.MeasurementsPerTransmission;
    if (this.MeasurementsPerTransmission < 1)
      this.MeasurementsPerTransmission = 1;
    if (this.TransmitOffset < 0)
      this.TransmitOffset = 0;
    if (this.TransmitOffset > 43199)
      this.TransmitOffset = 43199;
    if (this.RearmTime < 0)
      this.RearmTime = 0;
    if (this.RearmTime > 3600)
      this.RearmTime = 3600;
    this.MinimumCommunicationFrequency = this.ReportInterval.ToInt() * 2 + 5;
  }

  public void ReadyToShip()
  {
    this.IsSleeping = true;
    this.Save();
  }

  public Dictionary<string, object> Defaults
  {
    get
    {
      if (this._Defaults == null)
        this._Defaults = MonnitApplicationBase.GetDefaults(this);
      return this._Defaults;
    }
  }

  public T DefaultValue<T>(string name) => (T) Convert.ChangeType(this.Defaults[name], typeof (T));

  public void SetDefaults(bool autosave)
  {
    this.IsActive = true;
    this.ReportInterval = ConfigData.AppSettings("Reset_ReportInterval").ToDouble();
    this.ActiveStateInterval = ConfigData.AppSettings("Reset_ReportInterval").ToDouble();
    this.MinimumCommunicationFrequency = this.ReportInterval.ToInt() * 2 + 5;
    this.SensorhoodID = long.MinValue;
    this.SensorhoodConfigDirty = false;
    this.PendingActionControlCommand = false;
    this.ListenBeforeTalkValue = (int) byte.MaxValue;
    long calibration1 = this.Calibration1;
    long calibration2 = this.Calibration2;
    long calibration3 = this.Calibration3;
    long calibration4 = this.Calibration4;
    MonnitApplicationBase.DefaultConfigurationSettings(this);
    try
    {
      foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(this.SensorID))
        baseDbObject.Delete();
    }
    catch
    {
    }
    try
    {
      foreach (BaseDBObject baseDbObject in NonCachedAttribute.LoadBySensorID(this.SensorID))
        baseDbObject.Delete();
    }
    catch
    {
    }
    try
    {
      SensorAttribute.ResetAttributeList(this.SensorID);
    }
    catch
    {
    }
    if (!string.IsNullOrEmpty(this.CalibrationCertification) || this.ApplicationID == 36L || this.ApplicationID == 26L)
    {
      this.Calibration1 = calibration1;
      this.Calibration2 = calibration2;
      this.Calibration3 = calibration3;
      this.Calibration4 = calibration4;
    }
    if (this.GenerationType.ToUpper().Contains("GEN1"))
    {
      if (this.RadioBand.Contains("900"))
        this.TransmitPower = 196;
      else if (this.RadioBand.Contains("868"))
        this.TransmitPower = 192 /*0xC0*/;
      else if (this.RadioBand.Contains("433"))
        this.TransmitPower = 201;
      else if (this.RadioBand.Contains("920"))
        this.TransmitPower = 192 /*0xC0*/;
    }
    else if (this.GenerationType.ToUpper().Contains("GEN2"))
    {
      this.TransmitPower = (int) ushort.MaxValue;
      this.TransmitPowerOptions = 1;
    }
    this.SetCustomCompanyDefaults(false);
    Gateway gateway = Gateway.LoadBySensorID(this.SensorID);
    if (gateway != null)
    {
      if (this.SensorTypeID == 8L)
        this.LocalConfigOptions = 1;
      gateway.ResetToDefault(false);
      gateway.ReportInterval = this.ReportInterval;
      gateway.Save();
    }
    this.ProfileConfig2Dirty = true;
    this.ProfileConfigDirty = true;
    this.GeneralConfigDirty = true;
    this.GeneralConfig2Dirty = true;
    this.GeneralConfig3Dirty = true;
    if (!autosave)
      return;
    this.Save();
  }

  public void SetCustomCompanyDefaults(bool autosave, long companyID)
  {
    this.SetCustomCompanyDefaults(autosave);
  }

  public void SetCustomCompanyDefaults(bool autosave)
  {
    long calibration1 = this.Calibration1;
    long calibration2 = this.Calibration2;
    long calibration3 = this.Calibration3;
    long calibration4 = this.Calibration4;
    foreach (KeyValuePair<string, string> keyValuePair in CustomCompanyDevice.LoadSettings(this.SensorID, false).ToDictionary<CustomCompanyDevice, string, string>((System.Func<CustomCompanyDevice, string>) (x => x.Name), (System.Func<CustomCompanyDevice, string>) (x => x.Value)))
    {
      string key = keyValuePair.Key;
      switch (key)
      {
        case "ActiveStateInterval":
          this.ActiveStateInterval = keyValuePair.Value.ToDouble();
          break;
        case "BSNUrgentDelay":
          this.BSNUrgentDelay = keyValuePair.Value.ToInt();
          break;
        case "BiStable":
          this.BiStable = keyValuePair.Value.ToInt();
          break;
        case "Calibration1":
          if (string.IsNullOrEmpty(this.CalibrationCertification))
          {
            this.Calibration1 = keyValuePair.Value.ToLong();
            break;
          }
          break;
        case "Calibration2":
          if (string.IsNullOrEmpty(this.CalibrationCertification))
          {
            this.Calibration2 = keyValuePair.Value.ToLong();
            break;
          }
          break;
        case "Calibration3":
          if (string.IsNullOrEmpty(this.CalibrationCertification))
          {
            this.Calibration3 = keyValuePair.Value.ToLong();
            break;
          }
          break;
        case "Calibration4":
          if (string.IsNullOrEmpty(this.CalibrationCertification))
          {
            this.Calibration4 = keyValuePair.Value.ToLong();
            break;
          }
          break;
        case "ChannelMask":
          this.ChannelMask = keyValuePair.Value.ToLong();
          break;
        case "CryptRequired":
          this.CryptRequired = keyValuePair.Value.ToInt();
          break;
        case "CrystalStartTime":
          this.CrystalStartTime = keyValuePair.Value.ToInt();
          break;
        case "DMExchangeDelayMultiple":
          this.DMExchangeDelayMultiple = keyValuePair.Value.ToInt();
          break;
        case "EventDetectionCount":
          this.EventDetectionCount = keyValuePair.Value.ToInt();
          break;
        case "EventDetectionPeriod":
          this.EventDetectionPeriod = keyValuePair.Value.ToInt();
          break;
        case "EventDetectionType":
          this.EventDetectionType = keyValuePair.Value.ToInt();
          break;
        case "Hysteresis":
          this.Hysteresis = keyValuePair.Value.ToLong();
          break;
        case "LinkAcceptance":
          this.LinkAcceptance = keyValuePair.Value.ToInt();
          break;
        case "ListenBeforeTalkValue":
          this.ListenBeforeTalkValue = keyValuePair.Value.ToInt();
          break;
        case "MaximumNetworkHops":
          this.MaximumNetworkHops = keyValuePair.Value.ToInt();
          break;
        case "MaximumThreshold":
          this.MaximumThreshold = keyValuePair.Value.ToLong();
          break;
        case "MeasurementsPerTransmission":
          this.MeasurementsPerTransmission = keyValuePair.Value.ToInt();
          break;
        case "MinimumCommunicationFrequency":
          this.MinimumCommunicationFrequency = keyValuePair.Value.ToInt();
          break;
        case "MinimumThreshold":
          this.MinimumThreshold = keyValuePair.Value.ToLong();
          break;
        case "PingTime":
          this.Pingtime = keyValuePair.Value.ToInt();
          break;
        case "RearmTime":
          this.RearmTime = keyValuePair.Value.ToInt();
          break;
        case "Recovery":
          this.Recovery = keyValuePair.Value.ToInt();
          break;
        case "ReportInterval":
          this.ReportInterval = keyValuePair.Value.ToDouble();
          break;
        case "RetryCount":
          this.RetryCount = keyValuePair.Value.ToInt();
          break;
        case "SHID1":
          this.SHID1 = keyValuePair.Value.ToLong();
          break;
        case "SHID2":
          this.SHID2 = keyValuePair.Value.ToLong();
          break;
        case "StandardMessageDelay":
          this.StandardMessageDelay = keyValuePair.Value.ToInt();
          break;
        case "TestTransmitCount":
          this.TestTransmitCount = keyValuePair.Value.ToInt();
          break;
        case "TimeOfDayActive":
          this.TimeOfDayActive = BitConverter.GetBytes(keyValuePair.Value.ToLong());
          break;
        case "TransmitIntervalLink":
          this.TransmitIntervalLink = keyValuePair.Value.ToInt();
          break;
        case "TransmitIntervalTest":
          this.TransmitIntervalTest = keyValuePair.Value.ToInt();
          break;
        case "TransmitOffset":
          this.TransmitOffset = keyValuePair.Value.ToInt();
          break;
        case "UrgentRetries":
          this.UrgentRetries = keyValuePair.Value.ToInt();
          break;
      }
      if (key.StartsWith("Attribute|", StringComparison.InvariantCultureIgnoreCase))
      {
        string str = key.Replace("Attribute|", "");
        new SensorAttribute()
        {
          SensorID = this.SensorID,
          Name = str,
          Value = keyValuePair.Value
        }.Save();
      }
    }
    if (!string.IsNullOrEmpty(this.CalibrationCertification) || this.SensorTypeID == 36L || this.SensorTypeID == 26L)
    {
      this.Calibration1 = calibration1;
      this.Calibration2 = calibration2;
      this.Calibration3 = calibration3;
      this.Calibration4 = calibration4;
    }
    Gateway gateway = Gateway.LoadBySensorID(this.SensorID);
    if (gateway != null)
    {
      gateway.ResetToDefault(false);
      gateway.ReportInterval = this.ReportInterval;
      gateway.Save();
    }
    this.ProfileConfig2Dirty = true;
    this.ProfileConfigDirty = true;
    this.GeneralConfigDirty = true;
    this.GeneralConfig2Dirty = true;
    this.GeneralConfig3Dirty = true;
    if (!autosave)
      return;
    this.Save();
  }

  public void SetDefaultCalibration()
  {
    if (!string.IsNullOrEmpty(this.CalibrationCertification))
      return;
    MonnitApplicationBase.DefaultCalibrationSettings(this);
  }

  public DateTime GetCalibrationCertificationValidUntil()
  {
    CalibrationCertificate calibrationCertificate = CalibrationCertificate.LoadBySensor(this);
    return calibrationCertificate != null ? calibrationCertificate.CertificationValidUntil : DateTime.MinValue;
  }

  public bool SendNotification
  {
    get => this.IsActive && this.CSNetID > 0L && !this.isPaused() && this.Network.SendNotifications;
  }

  public static long NextSensorID() => new Monnit.Data.Sensor.NextSensorID().Result;

  public bool IsWiFiSensor => this.SensorTypeID == 4L || this.SensorTypeID == 8L;

  public bool IsPoESensor => this.SensorTypeID == 6L;

  public bool IsLTESensor => this.SensorTypeID == 7L;

  public bool IsWiFi2Sensor => this.SensorTypeID == 8L;

  public byte[] GeneralConfig(int sector)
  {
    if (!this.GenerationType.ToUpper().Contains("GEN2") && !this.IsWiFiSensor && new Version(this.FirmwareVersion) < new Version("2.0"))
    {
      byte[] destinationArray = new byte[38];
      destinationArray[0] = (byte) 114;
      Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt32(this.SensorID)), 0, (Array) destinationArray, 1, 4);
      destinationArray[5] = (byte) 12;
      Array.Copy((Array) BitConverter.GetBytes(this.ChannelMask), 0, (Array) destinationArray, 6, 4);
      Array.Copy((Array) BitConverter.GetBytes(this.ApplicationID), 0, (Array) destinationArray, 10, 2);
      Array.Copy((Array) BitConverter.GetBytes(this.StandardMessageDelay), 0, (Array) destinationArray, 12, 2);
      Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt16(this.ActiveStateInterval * 60.0)), 0, (Array) destinationArray, 14, 2);
      Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt16(this.ReportInterval * 60.0)), 0, (Array) destinationArray, 16 /*0x10*/, 2);
      destinationArray[18] = Convert.ToByte(this.TransmitIntervalLink);
      destinationArray[19] = Convert.ToByte(this.TransmitIntervalTest);
      destinationArray[20] = Convert.ToByte(this.TestTransmitCount);
      destinationArray[21] = Convert.ToByte(this.MaximumNetworkHops);
      destinationArray[22] = Convert.ToByte(this.RetryCount);
      destinationArray[23] = Convert.ToByte(this.Recovery);
      Array.Copy((Array) this.TimeOfDayActive, 0, (Array) destinationArray, 24, 12);
      Array.Copy((Array) BitConverter.GetBytes((int) ushort.MaxValue), 0, (Array) destinationArray, 36, 2);
      return destinationArray;
    }
    byte[] destinationArray1 = new byte[22];
    destinationArray1[0] = (byte) 114;
    Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt32(this.SensorID)), 0, (Array) destinationArray1, 1, 4);
    switch (sector)
    {
      case 24:
        destinationArray1[5] = (byte) 24;
        Array.Copy((Array) BitConverter.GetBytes(this.ChannelMask), 0, (Array) destinationArray1, 6, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.ApplicationID), 0, (Array) destinationArray1, 10, 2);
        Array.Copy((Array) BitConverter.GetBytes(this.StandardMessageDelay), 0, (Array) destinationArray1, 12, 2);
        Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt16(this.ActiveStateInterval * 60.0)), 0, (Array) destinationArray1, 14, 2);
        Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt16(this.ReportInterval * 60.0)), 0, (Array) destinationArray1, 16 /*0x10*/, 2);
        destinationArray1[18] = Convert.ToByte(this.TransmitIntervalLink);
        destinationArray1[19] = Convert.ToByte(this.TransmitIntervalTest);
        destinationArray1[20] = Convert.ToByte(this.TestTransmitCount);
        destinationArray1[21] = Convert.ToByte(this.MaximumNetworkHops);
        break;
      case 25:
        destinationArray1[5] = (byte) 25;
        destinationArray1[6] = Convert.ToByte(this.RetryCount);
        destinationArray1[7] = Convert.ToByte(this.Recovery);
        if (!this.GenerationType.ToUpper().Contains("GEN2"))
        {
          Array.Copy((Array) this.TimeOfDayActive, 0, (Array) destinationArray1, 8, 12);
          break;
        }
        Array.Copy((Array) this.TimeOfDayActive, 0, (Array) destinationArray1, 8, 14);
        break;
      case 27:
        destinationArray1[5] = (byte) 27;
        destinationArray1[6] = Convert.ToByte(this.ListenBeforeTalkValue);
        destinationArray1[7] = (byte) this.LinkAcceptance;
        if (new Version(this.FirmwareVersion) >= new Version("2.5.1.0") && new Version(this.FirmwareVersion) <= new Version("2.5.3.0") && (string.IsNullOrEmpty(this.GenerationType) || this.GenerationType.ToUpper().Contains("GEN1")))
        {
          destinationArray1[8] = Convert.ToByte(3);
          for (int index = 9; index < 22; ++index)
            destinationArray1[index] = byte.MaxValue;
        }
        else if (!string.IsNullOrEmpty(this.GenerationType) && this.GenerationType.ToUpper().Contains("GEN2"))
        {
          destinationArray1[8] = (byte) this.CrystalStartTime;
          destinationArray1[9] = (byte) this.DMExchangeDelayMultiple;
          Array.Copy((Array) BitConverter.GetBytes(this.SHID1), 0, (Array) destinationArray1, 10, 4);
          Array.Copy((Array) BitConverter.GetBytes(this.SHID2), 0, (Array) destinationArray1, 14, 4);
          destinationArray1[18] = (byte) this.CryptRequired;
          destinationArray1[19] = (byte) this.Pingtime;
          if (this.MonnitApplicationID == 0L)
          {
            destinationArray1[20] = (byte) this.BSNUrgentDelay;
            destinationArray1[21] = (byte) this.UrgentRetries;
          }
          else
          {
            destinationArray1[20] = (byte) this.TimeOffset;
            destinationArray1[21] = (byte) this.TimeOfDayControl;
          }
        }
        else
        {
          for (int index = 8; index < 22; ++index)
            destinationArray1[index] = byte.MaxValue;
        }
        break;
    }
    return destinationArray1;
  }

  public bool CompareGeneralConfig_Pre2(byte[] configData)
  {
    if (this.ChannelMask != (long) BitConverter.ToUInt32(configData, 0) || this.StandardMessageDelay != (int) BitConverter.ToUInt16(configData, 6) || (int) Convert.ToUInt16(this.ActiveStateInterval * 60.0) != (int) BitConverter.ToUInt16(configData, 8) || (int) Convert.ToUInt16(this.ReportInterval * 60.0) != (int) BitConverter.ToUInt16(configData, 10) || this.TransmitIntervalLink != Convert.ToInt32(configData[12]) || this.TransmitIntervalTest != Convert.ToInt32(configData[13]) || this.TestTransmitCount != Convert.ToInt32(configData[14]) || this.MaximumNetworkHops != Convert.ToInt32(configData[15]) || this.RetryCount != Convert.ToInt32(configData[16 /*0x10*/]) || this.Recovery != Convert.ToInt32(configData[17]))
      return false;
    byte[] destinationArray = new byte[12];
    Array.Copy((Array) configData, 18, (Array) destinationArray, 0, 12);
    return this.EqualToTimeOfDayActive(destinationArray);
  }

  public bool CompareGeneralConfig1(byte[] configData)
  {
    return this.ChannelMask == (long) BitConverter.ToUInt32(configData, 0) && this.StandardMessageDelay == (int) BitConverter.ToUInt16(configData, 6) && (int) Convert.ToUInt16(this.ActiveStateInterval * 60.0) == (int) BitConverter.ToUInt16(configData, 8) && (int) Convert.ToUInt16(this.ReportInterval * 60.0) == (int) BitConverter.ToUInt16(configData, 10) && this.TransmitIntervalLink == Convert.ToInt32(configData[12]) && this.TransmitIntervalTest == Convert.ToInt32(configData[13]) && this.TestTransmitCount == Convert.ToInt32(configData[14]) && this.MaximumNetworkHops == Convert.ToInt32(configData[15]);
  }

  public bool CompareGeneralConfig2(byte[] configData)
  {
    if (this.RetryCount != Convert.ToInt32(configData[0]) || this.Recovery != Convert.ToInt32(configData[1]))
      return false;
    byte[] destinationArray = new byte[14];
    Array.Copy((Array) configData, 2, (Array) destinationArray, 0, 14);
    return this.EqualToTimeOfDayActive(destinationArray);
  }

  public bool CompareGeneralConfig3(byte[] configData)
  {
    return this.ListenBeforeTalkValue == (int) configData[0] && this.LinkAcceptance == (int) configData[1] && this.CrystalStartTime == (int) configData[2] && this.DMExchangeDelayMultiple == (int) configData[3] && this.SHID1 == (long) BitConverter.ToUInt32(configData, 4) && this.SHID2 == (long) BitConverter.ToUInt32(configData, 8) && this.CryptRequired == (int) configData[12] && this.Pingtime == (int) configData[13] && this.UrgentRetries == (int) configData[14] && this.TimeOffset == (int) configData[15];
  }

  public static void FillGeneralConfig_Pre2(Sensor sensor, byte[] configData)
  {
    sensor.ChannelMask = (long) BitConverter.ToUInt32(configData, 2);
    sensor.StandardMessageDelay = (int) BitConverter.ToUInt16(configData, 8);
    sensor.ActiveStateInterval = (double) BitConverter.ToUInt16(configData, 10) / 60.0;
    sensor.ReportInterval = (double) BitConverter.ToUInt16(configData, 12) / 60.0;
    sensor.TransmitIntervalLink = Convert.ToInt32(configData[14]);
    sensor.TransmitIntervalTest = Convert.ToInt32(configData[15]);
    sensor.TestTransmitCount = Convert.ToInt32(configData[16 /*0x10*/]);
    sensor.MaximumNetworkHops = Convert.ToInt32(configData[17]);
    sensor.RetryCount = Convert.ToInt32(configData[18]);
    sensor.Recovery = Convert.ToInt32(configData[19]);
    Array.Copy((Array) configData, 20, (Array) sensor.TimeOfDayActive, 0, 12);
  }

  public static void FillGeneralConfig1(Sensor sensor, byte[] configData)
  {
    sensor.ChannelMask = (long) BitConverter.ToUInt32(configData, 0);
    sensor.StandardMessageDelay = (int) BitConverter.ToUInt16(configData, 6);
    sensor.ActiveStateInterval = (double) BitConverter.ToUInt16(configData, 8) / 60.0;
    sensor.ReportInterval = (double) BitConverter.ToUInt16(configData, 10) / 60.0;
    sensor.TransmitIntervalLink = Convert.ToInt32(configData[12]);
    sensor.TransmitIntervalTest = Convert.ToInt32(configData[13]);
    sensor.TestTransmitCount = Convert.ToInt32(configData[14]);
    sensor.MaximumNetworkHops = Convert.ToInt32(configData[15]);
  }

  public static void FillGeneralConfig2(Sensor sensor, byte[] configData)
  {
    sensor.RetryCount = Convert.ToInt32(configData[0]);
    sensor.Recovery = Convert.ToInt32(configData[1]);
    if (!sensor.GenerationType.ToUpper().Contains("GEN2"))
      Array.Copy((Array) configData, 2, (Array) sensor.TimeOfDayActive, 0, 12);
    else
      Array.Copy((Array) configData, 2, (Array) sensor.TimeOfDayActive, 0, 14);
  }

  public static void FillGeneralConfig3(Sensor sensor, byte[] configData)
  {
    sensor.ListenBeforeTalkValue = Convert.ToInt32(configData[0]);
    sensor.LinkAcceptance = (int) configData[1];
    sensor.CrystalStartTime = (int) configData[2];
    sensor.DMExchangeDelayMultiple = (int) configData[3];
    sensor.SHID1 = (long) BitConverter.ToUInt32(configData, 4);
    sensor.SHID2 = (long) BitConverter.ToUInt32(configData, 8);
    sensor.CryptRequired = (int) configData[12];
    sensor.Pingtime = (int) configData[13];
    sensor.BSNUrgentDelay = (int) configData[14];
    sensor.UrgentRetries = (int) configData[15];
    sensor.TimeOffset = (int) configData[14];
    sensor.TimeOfDayControl = (int) configData[15];
  }

  public byte[] RFConfig1(int sector)
  {
    byte[] destinationArray = new byte[22];
    destinationArray[0] = (byte) 114;
    Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt32(this.SensorID)), 0, (Array) destinationArray, 1, 4);
    destinationArray[5] = (byte) 8;
    if (this.GenerationType.ToUpper().Contains("GEN1"))
    {
      destinationArray[6] = Convert.ToByte(this.TransmitPower);
      destinationArray[7] = Convert.ToByte(this.ReceiveSensitivity);
      for (int index = 8; index < 22; ++index)
        destinationArray[index] = byte.MaxValue;
    }
    else if (this.GenerationType.Contains("Gen2"))
    {
      Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt16(this.TransmitPower)), 0, (Array) destinationArray, 6, 2);
      destinationArray[8] = Convert.ToByte(this.TransmitPowerOptions);
      destinationArray[9] = Convert.ToByte(this.ReceiveSensitivity);
      for (int index = 10; index < 22; ++index)
        destinationArray[index] = byte.MaxValue;
    }
    return destinationArray;
  }

  public bool CompareRFConfig1(byte[] configData)
  {
    return this.TransmitPower == Convert.ToInt32(configData[0]) && this.ReceiveSensitivity == Convert.ToInt32(configData[1]);
  }

  public static void FillRFConfig1(Sensor sensor, byte[] configData)
  {
    sensor.TransmitPower = Convert.ToInt32(configData[0]);
    sensor.ReceiveSensitivity = Convert.ToInt32(configData[1]);
  }

  public byte[] ProfileConfig(int sector)
  {
    if (new Version(this.FirmwareVersion) < new Version("2.0") && !this.IsWiFiSensor && !this.GenerationType.ToUpper().Contains("GEN2"))
    {
      byte[] destinationArray = new byte[38];
      destinationArray[0] = (byte) 114;
      Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt32(this.SensorID)), 0, (Array) destinationArray, 1, 4);
      destinationArray[5] = (byte) 14;
      if (MonnitApplicationBase.ProfileType(this.ApplicationID) == eApplicationProfileType.Interval)
      {
        destinationArray[6] = Convert.ToByte((object) eApplicationProfileType.Interval);
        destinationArray[7] = Convert.ToByte(this.MeasurementsPerTransmission);
        Array.Copy((Array) BitConverter.GetBytes(this.TransmitOffset), 0, (Array) destinationArray, 8, 2);
        Array.Copy((Array) BitConverter.GetBytes(this.Hysteresis), 0, (Array) destinationArray, 10, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.MinimumThreshold), 0, (Array) destinationArray, 14, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.MaximumThreshold), 0, (Array) destinationArray, 18, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.Calibration1), 0, (Array) destinationArray, 22, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.Calibration2), 0, (Array) destinationArray, 26, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.Calibration3), 0, (Array) destinationArray, 30, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.Calibration4), 0, (Array) destinationArray, 34, 4);
      }
      else if (MonnitApplicationBase.ProfileType(this.ApplicationID) == eApplicationProfileType.Trigger)
      {
        destinationArray[6] = Convert.ToByte((object) eApplicationProfileType.Trigger);
        destinationArray[7] = Convert.ToByte(this.EventDetectionType);
        Array.Copy((Array) BitConverter.GetBytes(this.EventDetectionPeriod), 0, (Array) destinationArray, 8, 2);
        Array.Copy((Array) BitConverter.GetBytes(this.EventDetectionCount), 0, (Array) destinationArray, 10, 2);
        Array.Copy((Array) BitConverter.GetBytes(this.RearmTime), 0, (Array) destinationArray, 12, 2);
        destinationArray[14] = Convert.ToByte(this.BiStable);
        Array.Copy((Array) BitConverter.GetBytes(72057594037927935L /*0xFFFFFFFFFFFFFF*/), 0, (Array) destinationArray, 15, 7);
        Array.Copy((Array) BitConverter.GetBytes(ulong.MaxValue), 0, (Array) destinationArray, 22, 8);
        Array.Copy((Array) BitConverter.GetBytes(ulong.MaxValue), 0, (Array) destinationArray, 30, 8);
      }
      return destinationArray;
    }
    byte[] numArray = new byte[22];
    numArray[0] = (byte) 114;
    Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt32(this.SensorID)), 0, (Array) numArray, 1, 4);
    if (sector == 28)
    {
      numArray[5] = (byte) 28;
      if (MonnitApplicationBase.ProfileType(this.ApplicationID) == eApplicationProfileType.Interval)
      {
        numArray[6] = Convert.ToByte((object) eApplicationProfileType.Interval);
        numArray[7] = Convert.ToByte(this.MeasurementsPerTransmission);
        Array.Copy((Array) BitConverter.GetBytes(this.TransmitOffset), 0, (Array) numArray, 8, 2);
        Array.Copy((Array) BitConverter.GetBytes(this.Hysteresis), 0, (Array) numArray, 10, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.MinimumThreshold), 0, (Array) numArray, 14, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.MaximumThreshold), 0, (Array) numArray, 18, 4);
      }
      else if (MonnitApplicationBase.ProfileType(this.ApplicationID) == eApplicationProfileType.Trigger)
      {
        numArray[6] = Convert.ToByte((object) eApplicationProfileType.Trigger);
        numArray[7] = Convert.ToByte(this.EventDetectionType);
        Array.Copy((Array) BitConverter.GetBytes(this.EventDetectionPeriod), 0, (Array) numArray, 8, 2);
        Array.Copy((Array) BitConverter.GetBytes(this.EventDetectionCount), 0, (Array) numArray, 10, 2);
        Array.Copy((Array) BitConverter.GetBytes(this.RearmTime), 0, (Array) numArray, 12, 2);
        numArray[14] = Convert.ToByte(this.BiStable);
        Array.Copy((Array) BitConverter.GetBytes(72057594037927935L /*0xFFFFFFFFFFFFFF*/), 0, (Array) numArray, 15, 7);
      }
    }
    else
    {
      numArray[5] = (byte) 29;
      if (MonnitApplicationBase.ProfileType(this.ApplicationID) == eApplicationProfileType.Interval)
      {
        Array.Copy((Array) BitConverter.GetBytes(this.Calibration1), 0, (Array) numArray, 6, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.Calibration2), 0, (Array) numArray, 10, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.Calibration3), 0, (Array) numArray, 14, 4);
        Array.Copy((Array) BitConverter.GetBytes(this.Calibration4), 0, (Array) numArray, 18, 4);
      }
      else if (MonnitApplicationBase.ProfileType(this.ApplicationID) == eApplicationProfileType.Trigger)
      {
        Array.Copy((Array) BitConverter.GetBytes(ulong.MaxValue), 0, (Array) numArray, 6, 8);
        Array.Copy((Array) BitConverter.GetBytes(ulong.MaxValue), 0, (Array) numArray, 14, 8);
      }
    }
    if (!this.IsCableEnabled)
      return numArray;
    byte[] destinationArray1 = new byte[26];
    destinationArray1[0] = (byte) 132;
    Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt32(this.SensorID)), 0, (Array) destinationArray1, 1, 4);
    Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt32(this.CableID)), 0, (Array) destinationArray1, 5, 4);
    Array.Copy((Array) numArray, 5, (Array) destinationArray1, 9, 17);
    if (this.ForceOverwrite)
      destinationArray1[9] = Convert.ToByte((int) destinationArray1[9] | 128 /*0x80*/);
    return destinationArray1;
  }

  public bool CompareProfileConfig(byte[] configData)
  {
    if ((int) Convert.ToUInt16(configData[0]) == (int) Convert.ToUInt16((object) eApplicationProfileType.Interval))
    {
      if (this.MonnitApplicationID == 8L)
        this.Calibration1 = (long) BitConverter.ToUInt32(configData, 16 /*0x10*/);
      if (this.MeasurementsPerTransmission != (int) Convert.ToUInt16(configData[1]) || this.TransmitOffset != (int) BitConverter.ToUInt16(configData, 2) || this.Hysteresis != (long) BitConverter.ToUInt32(configData, 4) || this.MinimumThreshold != (long) BitConverter.ToInt32(configData, 8) || this.MaximumThreshold != (long) BitConverter.ToInt32(configData, 12) || this.Calibration1 != (long) BitConverter.ToInt32(configData, 16 /*0x10*/) || this.Calibration2 != (long) BitConverter.ToInt32(configData, 20) || this.Calibration3 != (long) BitConverter.ToInt32(configData, 24) || this.Calibration4 != (long) BitConverter.ToInt32(configData, 28))
        return false;
    }
    else if (this.EventDetectionType != (int) Convert.ToUInt16(configData[1]) || this.EventDetectionPeriod != (int) BitConverter.ToUInt16(configData, 2) || this.EventDetectionCount != (int) BitConverter.ToUInt16(configData, 4) || this.RearmTime != (int) BitConverter.ToUInt16(configData, 6) || this.BiStable != (int) Convert.ToUInt16(configData[8]))
      return false;
    return true;
  }

  public bool CompareProfileConfig1(byte[] configData)
  {
    if ((int) Convert.ToUInt16(configData[0]) == (int) Convert.ToUInt16((object) eApplicationProfileType.Interval))
    {
      if (this.MonnitApplicationID == 8L)
        this.Calibration1 = (long) BitConverter.ToUInt32(configData, 16 /*0x10*/);
      if (this.MeasurementsPerTransmission != (int) Convert.ToUInt16(configData[1]) || this.TransmitOffset != (int) BitConverter.ToUInt16(configData, 2) || this.Hysteresis != (long) BitConverter.ToUInt32(configData, 4) || this.MinimumThreshold != (long) BitConverter.ToInt32(configData, 8) || this.MaximumThreshold != (long) BitConverter.ToInt32(configData, 12))
        return false;
    }
    else if (this.EventDetectionType != (int) Convert.ToUInt16(configData[1]) || this.EventDetectionPeriod != (int) BitConverter.ToUInt16(configData, 2) || this.EventDetectionCount != (int) BitConverter.ToUInt16(configData, 4) || this.RearmTime != (int) BitConverter.ToUInt16(configData, 6) || this.BiStable != (int) Convert.ToUInt16(configData[8]))
      return false;
    return true;
  }

  public bool CompareProfileConfig2(byte[] configData)
  {
    return this.Calibration1 == (long) BitConverter.ToInt32(configData, 0) && this.Calibration2 == (long) BitConverter.ToInt32(configData, 4) && this.Calibration3 == (long) BitConverter.ToInt32(configData, 8) && this.Calibration4 == (long) BitConverter.ToInt32(configData, 12);
  }

  public void FillProfileConfig(byte[] configData)
  {
    if ((int) Convert.ToUInt16(configData[0]) == (int) Convert.ToUInt16((object) eApplicationProfileType.Interval))
    {
      this.MeasurementsPerTransmission = (int) Convert.ToUInt16(configData[1]);
      this.TransmitOffset = (int) BitConverter.ToUInt16(configData, 2);
      this.Hysteresis = (long) BitConverter.ToUInt32(configData, 4);
      this.MinimumThreshold = (long) BitConverter.ToInt32(configData, 8);
      this.MaximumThreshold = (long) BitConverter.ToInt32(configData, 12);
      this.Calibration1 = (long) BitConverter.ToInt32(configData, 16 /*0x10*/);
      this.Calibration2 = (long) BitConverter.ToInt32(configData, 20);
      this.Calibration3 = (long) BitConverter.ToInt32(configData, 24);
      this.Calibration4 = (long) BitConverter.ToInt32(configData, 28);
    }
    else
    {
      this.EventDetectionType = (int) Convert.ToUInt16(configData[1]);
      this.EventDetectionPeriod = (int) BitConverter.ToUInt16(configData, 2);
      this.EventDetectionCount = (int) BitConverter.ToUInt16(configData, 4);
      this.RearmTime = (int) BitConverter.ToUInt16(configData, 6);
      this.BiStable = (int) Convert.ToUInt16(configData[8]);
    }
  }

  public void FillGeneralConfig1(byte[] configData) => Sensor.FillGeneralConfig1(this, configData);

  public void FillGeneralConfig2(byte[] configData) => Sensor.FillGeneralConfig2(this, configData);

  public void FillGeneralConfig3(byte[] configData) => Sensor.FillGeneralConfig3(this, configData);

  public void FillProfileConfig1(byte[] configData)
  {
    if ((int) Convert.ToUInt16(configData[0]) == (int) Convert.ToUInt16((object) eApplicationProfileType.Interval))
    {
      this.MeasurementsPerTransmission = (int) Convert.ToUInt16(configData[1]);
      this.TransmitOffset = (int) BitConverter.ToUInt16(configData, 2);
      this.Hysteresis = (long) BitConverter.ToUInt32(configData, 4);
      this.MinimumThreshold = (long) BitConverter.ToInt32(configData, 8);
      this.MaximumThreshold = (long) BitConverter.ToInt32(configData, 12);
    }
    else
    {
      this.EventDetectionType = (int) Convert.ToUInt16(configData[1]);
      this.EventDetectionPeriod = (int) BitConverter.ToUInt16(configData, 2);
      this.EventDetectionCount = (int) BitConverter.ToUInt16(configData, 4);
      this.RearmTime = (int) BitConverter.ToUInt16(configData, 6);
      this.BiStable = (int) Convert.ToUInt16(configData[8]);
    }
  }

  public void FillProfileConfig2(byte[] configData)
  {
    this.Calibration1 = (long) BitConverter.ToInt32(configData, 0);
    this.Calibration2 = (long) BitConverter.ToInt32(configData, 4);
    this.Calibration3 = (long) BitConverter.ToInt32(configData, 8);
    this.Calibration4 = (long) BitConverter.ToInt32(configData, 12);
  }

  public byte[] ReadProfileConfig(int section)
  {
    byte[] destinationArray;
    if (this.IsCableEnabled)
    {
      destinationArray = new byte[10];
      destinationArray[0] = (byte) 130;
      Array.Copy((Array) BitConverter.GetBytes(this.SensorID), 0, (Array) destinationArray, 1, 4);
      Array.Copy((Array) BitConverter.GetBytes(this.CableID), 0, (Array) destinationArray, 5, 4);
      destinationArray[9] = (byte) section;
    }
    else
    {
      destinationArray = new byte[6];
      destinationArray[0] = (byte) 112 /*0x70*/;
      Array.Copy((Array) BitConverter.GetBytes(this.SensorID), 0, (Array) destinationArray, 1, 4);
      int num = !(new Version(this.FirmwareVersion) < new Version("2.0")) ? 0 : (!this.IsWiFiSensor ? 1 : 0);
      destinationArray[5] = num == 0 ? (byte) section : (byte) 14;
    }
    return destinationArray;
  }

  public byte[] SensorhoodConfig()
  {
    return Sensor.SensorhoodConfig(this.SensorID, this.SensorhoodID, this.SensorhoodTransmitions);
  }

  public static byte[] SensorhoodConfig(
    long deviceID,
    long sensorhoodID,
    int sensorhoodTransmitions)
  {
    byte[] destinationArray = new byte[22];
    destinationArray[0] = (byte) 114;
    Array.Copy((Array) BitConverter.GetBytes(Convert.ToUInt32(deviceID)), 0, (Array) destinationArray, 1, 4);
    destinationArray[5] = (byte) 10;
    uint uint32 = sensorhoodID > 0L ? Convert.ToUInt32(sensorhoodID) : 0U;
    Array.Copy((Array) BitConverter.GetBytes(uint32 == 0U ? 0U : (sensorhoodTransmitions > 0 ? Convert.ToUInt32(sensorhoodTransmitions) : 1U)), 0, (Array) destinationArray, 10, 4);
    Array.Copy((Array) BitConverter.GetBytes(uint32), 0, (Array) destinationArray, 14, 4);
    return destinationArray;
  }

  public bool CompareSensorhoodConfig(byte[] configData)
  {
    return this.SensorhoodTransmitions == Convert.ToInt32(configData[4]) && this.SensorhoodID == (long) Convert.ToInt32(configData[8]);
  }

  public static void FillSensorhoodConfig(Sensor sensor, byte[] configData)
  {
    sensor.SensorhoodTransmitions = Convert.ToInt32(configData[4]);
    sensor.SensorhoodID = (long) Convert.ToInt32(configData[8]);
  }

  public static byte[] InitiateFirmwareUpdate(
    long sensorID,
    long firmwareID,
    int firmwareSize,
    int flags)
  {
    byte[] destinationArray = new byte[14];
    destinationArray[0] = (byte) 212;
    Array.Copy((Array) BitConverter.GetBytes((uint) sensorID), 0, (Array) destinationArray, 1, 4);
    Array.Copy((Array) BitConverter.GetBytes((uint) firmwareID), 0, (Array) destinationArray, 5, 4);
    Array.Copy((Array) BitConverter.GetBytes((uint) firmwareSize), 0, (Array) destinationArray, 9, 4);
    destinationArray[13] = (byte) flags;
    return destinationArray;
  }

  public List<byte[]> Calibrate(string version)
  {
    return MonnitApplicationBase.ActionControlCommand(this, version);
  }

  public void ClearLastCommunicationDate()
  {
    Monnit.Data.Sensor.ClearLastCommunicationDate communicationDate = new Monnit.Data.Sensor.ClearLastCommunicationDate(this.SensorID);
  }

  public void ClearHistory()
  {
    Monnit.Data.Sensor.ClearHistory clearHistory = new Monnit.Data.Sensor.ClearHistory(this.SensorID);
  }

  public void RemoveAttributes(long sensorID)
  {
    List<SensorAttribute> sensorAttributeList = SensorAttribute.LoadBySensorID(sensorID);
    if (sensorAttributeList != null)
    {
      foreach (BaseDBObject baseDbObject in sensorAttributeList)
        baseDbObject.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public List<Notification> DefaultNotifications()
  {
    List<Notification> notificationList = new List<Notification>();
    if (this.PowerSource.VoltageForOneHundredPercent > this.PowerSource.VoltageForZeroPercent)
      notificationList.Add(new Notification()
      {
        AccountID = this.AccountID,
        CompareType = eCompareType.Less_Than,
        CompareValue = "15",
        NotificationClass = eNotificationClass.Low_Battery,
        NotificationText = "Low Battery",
        Name = "Low Battery - " + this.SensorName,
        MonnitApplicationID = this.MonnitApplicationID,
        SnoozeDuration = 120,
        Version = "1"
      });
    notificationList.Add(new Notification()
    {
      AccountID = this.AccountID,
      CompareType = eCompareType.Less_Than,
      CompareValue = (this.ReportInterval * 2.0 + 10.0).ToString(),
      NotificationClass = eNotificationClass.Inactivity,
      NotificationText = "Inactivity",
      Name = "Inactivity - " + this.SensorName,
      MonnitApplicationID = this.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = "1"
    });
    return notificationList;
  }

  public static DataTable SensorOverView(long accountID, long? csNetID)
  {
    return new Monnit.Data.Sensor.SensorOverView(accountID, csNetID).Result;
  }

  public static long GetTimeZoneIDBySensorID(long sensorID)
  {
    return new Monnit.Data.Sensor.GetTimeZoneIDBySensorID(sensorID).Result;
  }

  public static int totalCount() => new Monnit.Data.Sensor.totalCount().Result;

  public static int accountTotal(long accountID) => Sensor.LoadByAccountID(accountID).Count;

  public void UpdateCable(long newCableID)
  {
    if (this.CableID == newCableID)
      return;
    int type = 1;
    if (newCableID == 0L)
      type = 2;
    else if (this.CableID > 0L)
      type = 3;
    Monnit.Data.Sensor.UpdateByCableID updateByCableId = new Monnit.Data.Sensor.UpdateByCableID(this.SensorID, newCableID, type);
    this.CableID = newCableID;
  }

  public void ResetSensorForShipping()
  {
    if (this.IsWiFiSensor)
    {
      Gateway gateway = Gateway.LoadBySensorID(this.SensorID);
      gateway.IsDirty = false;
      gateway.SendResetNetworkRequest = true;
      GatewayType gatewayType = gateway.GatewayType;
      gateway.GatewayIP = gatewayType.DefaultGatewayIP;
      gateway.GatewaySubnet = gatewayType.DefaultGatewaySubnet;
      gateway.DefaultRouterIP = gatewayType.DefaultRouterIP;
      gateway.GatewayDNS = gatewayType.DefaultGatewayDNS;
      gateway.DeleteAllWiFiCredentials();
      gateway.WiFiNetworkCount = gatewayType.DefaultWiFiNetworkCount;
      gateway.Save();
      if (this.SensorTypeID != 8L)
        return;
      this.SetDefaults(true);
    }
    else
      this.SetDefaults(true);
  }

  public static void ResetSensorForShipping(long id) => Sensor.Load(id)?.ResetSensorForShipping();
}
