// Decompiled with JetBrains decompiler
// Type: Monnit.QuadContact
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class QuadContact : MonnitApplicationBase
{
  private SensorAttribute _ZeroValue;
  private SensorAttribute _OneValue;
  private SensorAttribute _Contact1;
  private SensorAttribute _Contact2;
  private SensorAttribute _Contact3;
  private SensorAttribute _Contact4;

  public static long MonnitApplicationID => (long) sbyte.MaxValue;

  public static string ApplicationName => "Quad Contact";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => QuadContact.MonnitApplicationID;

  public bool Contact1 { get; set; }

  public bool Contact2 { get; set; }

  public bool Contact3 { get; set; }

  public bool Contact4 { get; set; }

  public override string Serialize()
  {
    return $"{this.Contact1.ToString()}|{this.Contact2.ToString()}|{this.Contact3.ToString()}|{this.Contact4.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[4]
      {
        new AppDatum(eDatumType.DryContact, "Contact1", this.Contact1),
        new AppDatum(eDatumType.DryContact, "Contact2", this.Contact2),
        new AppDatum(eDatumType.DryContact, "Contact3", this.Contact3),
        new AppDatum(eDatumType.DryContact, "Contact4", this.Contact4)
      });
    }
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) QuadContact.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      string str1 = this.ZeroValue.Value;
      string str2 = this.OneValue.Value;
      return $"{this.Contact1Name.Value}: {(this.Contact1 ? str2 : str1)},{this.Contact2Name.Value}: {(this.Contact2 ? str2 : str1)},{this.Contact3Name.Value}: {(this.Contact3 ? str2 : str1)},{this.Contact4Name.Value}: {(this.Contact4 ? str2 : str1)}";
    }
  }

  public override object PlotValue => (object) (this.Contact1 ? 1 : 0);

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[4]
    {
      (object) this.Contact1,
      (object) this.Contact2,
      (object) this.Contact3,
      (object) this.Contact4
    });
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ZeroValue")
        this._ZeroValue = sensorAttribute;
      if (sensorAttribute.Name == "OneValue")
        this._OneValue = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|0")
        this._Contact1 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|1")
        this._Contact2 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|2")
        this._Contact3 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|3")
        this._Contact4 = sensorAttribute;
    }
  }

  public SensorAttribute ZeroValue
  {
    get
    {
      if (this._ZeroValue == null)
        this._ZeroValue = new SensorAttribute()
        {
          Value = "Closed",
          Name = nameof (ZeroValue)
        };
      return this._ZeroValue;
    }
  }

  public SensorAttribute OneValue
  {
    get
    {
      if (this._OneValue == null)
        this._OneValue = new SensorAttribute()
        {
          Value = "Open",
          Name = nameof (OneValue)
        };
      return this._OneValue;
    }
  }

  public SensorAttribute Contact1Name
  {
    get
    {
      if (this._Contact1 == null)
        this._Contact1 = new SensorAttribute()
        {
          Name = "DatumIndex|0",
          Value = "Contact One"
        };
      return this._Contact1;
    }
  }

  public SensorAttribute Contact2Name
  {
    get
    {
      if (this._Contact2 == null)
        this._Contact2 = new SensorAttribute()
        {
          Name = "DatumIndex|1",
          Value = "Contact Two"
        };
      return this._Contact2;
    }
  }

  public SensorAttribute Contact3Name
  {
    get
    {
      if (this._Contact3 == null)
        this._Contact3 = new SensorAttribute()
        {
          Name = "DatumIndex|2",
          Value = "Contact Three"
        };
      return this._Contact3;
    }
  }

  public SensorAttribute Contact4Name
  {
    get
    {
      if (this._Contact4 == null)
        this._Contact4 = new SensorAttribute()
        {
          Name = "DatumIndex|3",
          Value = "Contact Four"
        };
      return this._Contact4;
    }
  }

  public static string GetZeroValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ZeroValue")
        return sensorAttribute.Value.ToStringSafe();
    }
    return "Closed";
  }

  public static void SetZeroValue(long sensorID, string zeroValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ZeroValue")
      {
        sensorAttribute.Value = zeroValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ZeroValue",
        Value = zeroValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetOneValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "OneValue")
        return sensorAttribute.Value.ToStringSafe();
    }
    return "Open";
  }

  public static void SetOneValue(long sensorID, string oneValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "OneValue")
      {
        sensorAttribute.Value = oneValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "OneValue",
        Value = oneValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetContact4Label(long sensorID) => Sensor.GetDatumName(sensorID, 3);

  public static string GetContact3Label(long sensorID) => Sensor.GetDatumName(sensorID, 2);

  public static string GetContact2Label(long sensorID) => Sensor.GetDatumName(sensorID, 1);

  public static string GetContact1Label(long sensorID) => Sensor.GetDatumName(sensorID, 0);

  public static void SetContact4Label(long sensorID, string name)
  {
    Sensor.Load(sensorID).SetDatumName(3, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetContact3Label(long sensorID, string name)
  {
    Sensor.Load(sensorID).SetDatumName(2, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetContact2Label(long sensorID, string name)
  {
    Sensor.Load(sensorID).SetDatumName(1, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void SetContact1Label(long sensorID, string name)
  {
    Sensor.Load(sensorID).SetDatumName(0, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return $"\"{((QuadContact) app).Contact1}\",\"{((QuadContact) app).Contact2}\",\"{((QuadContact) app).Contact3}\",\"{((QuadContact) app).Contact4}\"";
  }

  public static QuadContact Deserialize(string version, string serialized)
  {
    QuadContact quadContact = new QuadContact();
    if (!string.IsNullOrEmpty(serialized))
    {
      string[] strArray = serialized.Split('|');
      quadContact.Contact1 = strArray[0].ToBool();
      if (strArray.Length == 4)
      {
        quadContact.Contact2 = strArray[1].ToBool();
        quadContact.Contact3 = strArray[2].ToBool();
        quadContact.Contact4 = strArray[3].ToBool();
      }
      else
      {
        quadContact.Contact2 = quadContact.Contact1;
        quadContact.Contact3 = quadContact.Contact1;
        quadContact.Contact4 = quadContact.Contact1;
      }
    }
    else
    {
      quadContact.Contact1 = false;
      quadContact.Contact2 = false;
      quadContact.Contact3 = false;
      quadContact.Contact4 = false;
    }
    return quadContact;
  }

  public static QuadContact Create(byte[] sdm, int startIndex)
  {
    return new QuadContact()
    {
      Contact1 = sdm[startIndex].ToBool(),
      Contact2 = sdm[startIndex + 1].ToBool(),
      Contact3 = sdm[startIndex + 2].ToBool(),
      Contact4 = sdm[startIndex + 3].ToBool()
    };
  }

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    sensor.ChannelMask = sensor.DefaultValue<long>("DefaultChannelMask");
    sensor.StandardMessageDelay = sensor.DefaultValue<int>("DefaultStandardMessageDelay");
    sensor.TransmitIntervalLink = sensor.DefaultValue<int>("DefaultTransmitIntervalLink");
    sensor.TransmitIntervalTest = sensor.DefaultValue<int>("DefaultTransmitIntervalTest");
    sensor.TestTransmitCount = sensor.DefaultValue<int>("DefaultTestTransmitCount");
    sensor.MaximumNetworkHops = sensor.DefaultValue<int>("DefaultMaximumNetworkHops");
    sensor.RetryCount = sensor.DefaultValue<int>("DefaultRetryCount");
    sensor.Recovery = sensor.DefaultValue<int>("DefaultRecovery");
    sensor.TimeOfDayActive = sensor.DefaultValue<byte[]>("DefaultTimeOfDayActive");
    sensor.MinimumCommunicationFrequency = (int) (sensor.ReportInterval * 2.0) + 10;
    sensor.ListenBeforeTalkValue = sensor.DefaultValue<int>("DefaultListenBeforeTalk");
    sensor.LinkAcceptance = sensor.DefaultValue<int>("DefaultLinkAcceptance");
    sensor.CrystalStartTime = sensor.DefaultValue<int>("DefaultCrystalDefaultTime");
    sensor.DMExchangeDelayMultiple = sensor.DefaultValue<int>("DefaultDMExchangeDelayMultiple");
    sensor.SHID1 = sensor.DefaultValue<long>("DefaultSensorHood1");
    sensor.SHID2 = sensor.DefaultValue<long>("DefaultSensorHood2");
    sensor.CryptRequired = sensor.DefaultValue<int>("DefaultCryptRequired");
    sensor.Pingtime = sensor.DefaultValue<int>("DefaultPingtime");
    sensor.BSNUrgentDelay = sensor.DefaultValue<int>("DefaultBSNUrgentDelay");
    sensor.UrgentRetries = sensor.DefaultValue<int>("DefaultUrgentRetries");
    sensor.MeasurementsPerTransmission = sensor.DefaultValue<int>("DefaultMeasurementsPerTransmission");
    sensor.TransmitOffset = sensor.DefaultValue<int>("DefaultTransmitOffset");
    sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
    sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    sensor.EventDetectionType = sensor.DefaultValue<int>("DefaultEventDetectionType");
    sensor.EventDetectionPeriod = sensor.DefaultValue<int>("DefaultEventDetectionPeriod");
    sensor.EventDetectionCount = sensor.DefaultValue<int>("DefaultEventDetectionCount");
    sensor.RearmTime = sensor.DefaultValue<int>("DefaultRearmTime");
    sensor.BiStable = sensor.DefaultValue<int>("DefaultBiStable");
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = QuadContact.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["EventDetectionTypeInput1_Manual"]))
    {
      int o = collection["EventDetectionTypeInput1_Manual"].ToInt();
      if (o < 0)
        o = 0;
      if (o > 2)
        o = 2;
      QuadContact.SetEventDetectionTypeInput1(sensor, o.ToInt());
      QuadContact.SetEventDetectionTypeInput2(sensor, o.ToInt());
      QuadContact.SetEventDetectionTypeInput3(sensor, o.ToInt());
      QuadContact.SetEventDetectionTypeInput4(sensor, o.ToInt());
    }
    if (string.IsNullOrEmpty(collection["RearmTime_Custom"]))
      return;
    int num = collection["RearmTime_Custom"].ToInt();
    if (num < 1)
      num = 1;
    if (num > 600)
      num = 600;
    sensor.Calibration1 = (long) num;
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["zeroValueLabel"]))
      QuadContact.SetZeroValue(sensor.SensorID, collection["zeroValueLabel"].ToStringSafe());
    if (string.IsNullOrEmpty(collection["oneValueLabel"]))
      return;
    QuadContact.SetOneValue(sensor.SensorID, collection["oneValueLabel"].ToStringSafe());
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.MarkClean(false);
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return new List<byte[]>()
    {
      QuadContactBase.getCalibrationValue(sensor.SensorID),
      sensor.ReadProfileConfig(29)
    };
  }

  public static string HystForUI(Sensor sensor) => "Not Used";

  public static string MinThreshForUI(Sensor sensor) => "Not Used";

  public static string MaxThreshForUI(Sensor sensor) => "Not Used";

  public static void SetEventDetectionTypeInput1(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294967040U;
    sensor.Calibration1 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetEventDetectionTypeInput1(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration1 & (long) byte.MaxValue));
  }

  public static void SetEventDetectionTypeInput2(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4294902015U;
    sensor.Calibration1 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetEventDetectionTypeInput2(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration1 & 65280L) >> 8);
  }

  public static void SetEventDetectionTypeInput3(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration1) & 4278255615U;
    sensor.Calibration1 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 16 /*0x10*/));
  }

  public static int GetEventDetectionTypeInput3(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration1 & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
  }

  public static void SetEventDetectionTypeInput4(Sensor sensor, int value)
  {
    uint num = (uint) (Convert.ToInt32(sensor.Calibration1) & 16777215 /*0xFFFFFF*/);
    sensor.Calibration1 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 24));
  }

  public static int GeEventDetectionTypeInput4(Sensor sensor)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sensor.Calibration1 & 4278190080L /*0xFF000000*/) >> 24);
  }

  public static void SetEventDetectionPeriodInput1(Sensor sensor, int value)
  {
    uint uint32 = Convert.ToUInt32(sensor.Calibration2 & 4294901760L);
    sensor.Calibration2 = (long) (uint32 | (uint) value);
  }

  public static int GetEventDetectionPeriodInput1(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration2 & (long) ushort.MaxValue);
  }

  public static void SetEventDetectionPeriodInput2(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration2) & (uint) ushort.MaxValue;
    sensor.Calibration2 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetEventDetectionPeriodInput2(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration2 & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetEventDetectionPeriodInput3(Sensor sensor, int value)
  {
    uint uint32 = Convert.ToUInt32(sensor.Calibration3 & 4294901760L);
    sensor.Calibration3 = (long) (uint32 | (uint) value);
  }

  public static int GetEventDetectionPeriodInput3(Sensor sensor)
  {
    return Convert.ToInt32(sensor.Calibration3 & (long) ushort.MaxValue);
  }

  public static void SetEventDetectionPeriodInput4(Sensor sensor, int value)
  {
    uint num = Convert.ToUInt32(sensor.Calibration3) & (uint) ushort.MaxValue;
    sensor.Calibration3 = (long) (num | (uint) (value << 16 /*0x10*/));
  }

  public static int GetEventDetectionPeriodInput4(Sensor sensor)
  {
    return Convert.ToInt32((sensor.Calibration3 & 4294901760L) >> 16 /*0x10*/);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(QuadContact left, QuadContact right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(QuadContact left, QuadContact right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(QuadContact left, QuadContact right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(QuadContact left, QuadContact right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(QuadContact left, QuadContact right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(QuadContact left, QuadContact right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is QuadContact && this.Equals((MonnitApplicationBase) (obj as QuadContact));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is QuadContact && this.Contact1 == (right as QuadContact).Contact1;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is QuadContact && this.Contact1 != (right as QuadContact).Contact1;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'bool' and 'bool'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'bool' and 'bool'");
  }
}
