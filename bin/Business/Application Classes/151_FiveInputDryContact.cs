// Decompiled with JetBrains decompiler
// Type: Monnit.FiveInputDryContact
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class FiveInputDryContact : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _ChartFormat;
  private SensorAttribute _Contact1_ZeroValue;
  private SensorAttribute _Contact1_OneValue;
  private SensorAttribute _Contact2_ZeroValue;
  private SensorAttribute _Contact2_OneValue;
  private SensorAttribute _Contact3_ZeroValue;
  private SensorAttribute _Contact3_OneValue;
  private SensorAttribute _Contact4_ZeroValue;
  private SensorAttribute _Contact4_OneValue;
  private SensorAttribute _Contact5_ZeroValue;
  private SensorAttribute _Contact5_OneValue;
  private SensorAttribute _Contact1;
  private SensorAttribute _Contact2;
  private SensorAttribute _Contact3;
  private SensorAttribute _Contact4;
  private SensorAttribute _Contact5;

  public static long MonnitApplicationID => FiveInputDryContactBase.MonnitApplicationID;

  public static string ApplicationName => "Five Input Dry Contact";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Point";

  public override long ApplicationID => FiveInputDryContact.MonnitApplicationID;

  public bool Contact1 { get; set; }

  public bool Contact2 { get; set; }

  public bool Contact3 { get; set; }

  public bool Contact4 { get; set; }

  public bool Contact5 { get; set; }

  public override string Serialize()
  {
    return $"{this.Contact1.ToString()}|{this.Contact2.ToString()}|{this.Contact3.ToString()}|{this.Contact4.ToString()}|{this.Contact5.ToString()}";
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) FiveInputDryContact.Deserialize(version, serialized);
  }

  public override string NotificationString
  {
    get
    {
      return $"{this.Contact1Name.Value}: {(this.Contact1 ? this.Contact1_ZeroValue.Value : this.Contact1_OneValue.Value)}, {this.Contact2Name.Value}: {(this.Contact2 ? this.Contact2_ZeroValue.Value : this.Contact2_OneValue.Value)}, {this.Contact3Name.Value}: {(this.Contact3 ? this.Contact3_ZeroValue.Value : this.Contact3_OneValue.Value)}, {this.Contact4Name.Value}: {(this.Contact4 ? this.Contact4_ZeroValue.Value : this.Contact4_OneValue.Value)}, {this.Contact5Name.Value}: {(this.Contact5 ? this.Contact5_ZeroValue.Value : this.Contact5_OneValue.Value)}";
    }
  }

  public override object PlotValue => (object) (this.Contact1 ? 1 : 0);

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>((IEnumerable<object>) new object[5]
    {
      (object) this.Contact1,
      (object) this.Contact2,
      (object) this.Contact3,
      (object) this.Contact4,
      (object) this.Contact5
    });
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>((IEnumerable<string>) new string[5]
    {
      this.Contact1Name.Value,
      this.Contact2Name.Value,
      this.Contact3Name.Value,
      this.Contact4Name.Value,
      this.Contact5Name.Value
    });
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[5]
      {
        new AppDatum(eDatumType.DryContact, "Contact1", this.Contact1),
        new AppDatum(eDatumType.DryContact, "Contact2", this.Contact2),
        new AppDatum(eDatumType.DryContact, "Contact3", this.Contact3),
        new AppDatum(eDatumType.DryContact, "Contact4", this.Contact4),
        new AppDatum(eDatumType.DryContact, "Contact5", this.Contact5)
      });
    }
  }

  public static string SpecialExportValue(MonnitApplicationBase app)
  {
    return $"\"{((FiveInputDryContact) app).Contact1}\",\"{((FiveInputDryContact) app).Contact2}\",\"{((FiveInputDryContact) app).Contact3}\",\"{((FiveInputDryContact) app).Contact4}\",\"{((FiveInputDryContact) app).Contact5}\"";
  }

  public static FiveInputDryContact Deserialize(string version, string serialized)
  {
    FiveInputDryContact fiveInputDryContact = new FiveInputDryContact();
    if (!string.IsNullOrEmpty(serialized))
    {
      string[] strArray = serialized.Split('|');
      fiveInputDryContact.Contact1 = strArray[0].ToBool();
      if (strArray.Length == 5)
      {
        fiveInputDryContact.Contact2 = strArray[1].ToBool();
        fiveInputDryContact.Contact3 = strArray[2].ToBool();
        fiveInputDryContact.Contact4 = strArray[3].ToBool();
        fiveInputDryContact.Contact5 = strArray[4].ToBool();
      }
      else
      {
        fiveInputDryContact.Contact2 = fiveInputDryContact.Contact1;
        fiveInputDryContact.Contact3 = fiveInputDryContact.Contact1;
        fiveInputDryContact.Contact4 = fiveInputDryContact.Contact1;
        fiveInputDryContact.Contact5 = fiveInputDryContact.Contact1;
      }
    }
    else
    {
      fiveInputDryContact.Contact1 = false;
      fiveInputDryContact.Contact2 = false;
      fiveInputDryContact.Contact3 = false;
      fiveInputDryContact.Contact4 = false;
      fiveInputDryContact.Contact5 = false;
    }
    return fiveInputDryContact;
  }

  public static FiveInputDryContact Create(byte[] sdm, int startIndex)
  {
    return new FiveInputDryContact()
    {
      Contact1 = sdm[startIndex].ToBool(),
      Contact2 = sdm[startIndex + 1].ToBool(),
      Contact3 = sdm[startIndex + 2].ToBool(),
      Contact4 = sdm[startIndex + 3].ToBool(),
      Contact5 = sdm[startIndex + 4].ToBool()
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
    sensor.TimeOffset = sensor.DefaultValue<int>("TimeOffset");
    sensor.TimeOfDayControl = sensor.DefaultValue<int>("TimeOfDayControl");
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
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Less_Than,
      CompareValue = "True",
      NotificationClass = eNotificationClass.Application,
      ApplicationID = FiveInputDryContact.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrEmpty(collection["EventDetectionTypeInput1"]))
    {
      long num = collection["EventDetectionTypeInput1"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 2L)
        num = 2L;
      sensor.MinimumThreshold = num;
    }
    if (!string.IsNullOrEmpty(collection["EventDetectionTypeInput2"]))
    {
      long num = collection["EventDetectionTypeInput2"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 2L)
        num = 2L;
      sensor.MaximumThreshold = num;
    }
    if (!string.IsNullOrEmpty(collection["EventDetectionTypeInput3"]))
    {
      long num = collection["EventDetectionTypeInput3"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 2L)
        num = 2L;
      sensor.Hysteresis = num;
    }
    if (!string.IsNullOrEmpty(collection["EventDetectionTypeInput4"]))
    {
      long num = collection["EventDetectionTypeInput4"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 2L)
        num = 2L;
      sensor.Calibration1 = num;
    }
    if (!string.IsNullOrEmpty(collection["EventDetectionTypeInput5"]))
    {
      long num = collection["EventDetectionTypeInput5"].ToLong();
      if (num < 0L)
        num = 0L;
      if (num > 2L)
        num = 2L;
      sensor.Calibration2 = num;
    }
    if (string.IsNullOrEmpty(collection["RearmTime_Custom"]) || string.IsNullOrEmpty(collection["ActiveStateInterval"]))
      return;
    long num1 = collection["RearmTime_Custom"].ToLong();
    double o = collection["ActiveStateInterval"].ToDouble() * 60.0;
    if (num1 < 1L)
      num1 = 1L;
    if ((double) num1 > o)
      num1 = o.ToLong();
    sensor.Calibration3 = num1;
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    FiveInputDryContact.SetContactLabel(sensor.SensorID, 1, string.IsNullOrWhiteSpace(collection["datumName0"]) ? "Contact 1 (White)" : collection["datumName0"].ToStringSafe());
    FiveInputDryContact.SetContact_ZeroValue(sensor.SensorID, 1, string.IsNullOrWhiteSpace(collection["Contact1zeroValueLabel"]) ? "Closed" : collection["Contact1zeroValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContact_OneValue(sensor.SensorID, 1, string.IsNullOrWhiteSpace(collection["Contact1oneValueLabel"]) ? "Open" : collection["Contact1oneValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContactLabel(sensor.SensorID, 2, string.IsNullOrWhiteSpace(collection["datumName1"]) ? "Contact 2 (Yellow)" : collection["datumName1"].ToStringSafe());
    FiveInputDryContact.SetContact_ZeroValue(sensor.SensorID, 2, string.IsNullOrWhiteSpace(collection["Contact2zeroValueLabel"]) ? "Closed" : collection["Contact2zeroValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContact_OneValue(sensor.SensorID, 2, string.IsNullOrWhiteSpace(collection["Contact2oneValueLabel"]) ? "Open" : collection["Contact2oneValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContactLabel(sensor.SensorID, 3, string.IsNullOrWhiteSpace(collection["datumName2"]) ? "Contact 3 (Red)" : collection["datumName2"].ToStringSafe());
    FiveInputDryContact.SetContact_ZeroValue(sensor.SensorID, 3, string.IsNullOrWhiteSpace(collection["Contact3zeroValueLabel"]) ? "Closed" : collection["Contact3zeroValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContact_OneValue(sensor.SensorID, 3, string.IsNullOrWhiteSpace(collection["Contact3oneValueLabel"]) ? "Open" : collection["Contact3oneValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContactLabel(sensor.SensorID, 4, string.IsNullOrWhiteSpace(collection["datumName3"]) ? "Contact 4 (Blue)" : collection["datumName3"].ToStringSafe());
    FiveInputDryContact.SetContact_ZeroValue(sensor.SensorID, 4, string.IsNullOrWhiteSpace(collection["Contact4zeroValueLabel"]) ? "Closed" : collection["Contact4zeroValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContact_OneValue(sensor.SensorID, 4, string.IsNullOrWhiteSpace(collection["Contact4oneValueLabel"]) ? "Open" : collection["Contact4oneValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContactLabel(sensor.SensorID, 5, string.IsNullOrWhiteSpace(collection["datumName4"]) ? "Contact 5 (Green)" : collection["datumName4"].ToStringSafe());
    FiveInputDryContact.SetContact_ZeroValue(sensor.SensorID, 5, string.IsNullOrWhiteSpace(collection["Contact5zeroValueLabel"]) ? "Closed" : collection["Contact5zeroValueLabel"].ToStringSafe());
    FiveInputDryContact.SetContact_OneValue(sensor.SensorID, 5, string.IsNullOrWhiteSpace(collection["Contact5oneValueLabel"]) ? "Open" : collection["Contact5oneValueLabel"].ToStringSafe());
  }

  public static string GetChartFormat(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        return sensorAttribute.Value.ToString();
    }
    return "Line";
  }

  public static void SetChartFormat(long sensorID, string chartFormat)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
      {
        sensorAttribute.Value = chartFormat.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "ChartFormat",
        Value = chartFormat.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute ChartFormat
  {
    get
    {
      if (this._ChartFormat == null)
        this._ChartFormat = new SensorAttribute()
        {
          Value = "Line",
          Name = nameof (ChartFormat)
        };
      return this._ChartFormat;
    }
  }

  public SensorAttribute Contact1_ZeroValue
  {
    get
    {
      if (this._Contact1_ZeroValue == null)
        this._Contact1_ZeroValue = new SensorAttribute()
        {
          Value = "Closed",
          Name = nameof (Contact1_ZeroValue)
        };
      return this._Contact1_ZeroValue;
    }
  }

  public SensorAttribute Contact1_OneValue
  {
    get
    {
      if (this._Contact1_OneValue == null)
        this._Contact1_OneValue = new SensorAttribute()
        {
          Value = "Open",
          Name = nameof (Contact1_OneValue)
        };
      return this._Contact1_OneValue;
    }
  }

  public SensorAttribute Contact2_ZeroValue
  {
    get
    {
      if (this._Contact2_ZeroValue == null)
        this._Contact2_ZeroValue = new SensorAttribute()
        {
          Value = "Closed",
          Name = nameof (Contact2_ZeroValue)
        };
      return this._Contact2_ZeroValue;
    }
  }

  public SensorAttribute Contact2_OneValue
  {
    get
    {
      if (this._Contact2_OneValue == null)
        this._Contact2_OneValue = new SensorAttribute()
        {
          Value = "Open",
          Name = nameof (Contact2_OneValue)
        };
      return this._Contact2_OneValue;
    }
  }

  public SensorAttribute Contact3_ZeroValue
  {
    get
    {
      if (this._Contact3_ZeroValue == null)
        this._Contact3_ZeroValue = new SensorAttribute()
        {
          Value = "Closed",
          Name = nameof (Contact3_ZeroValue)
        };
      return this._Contact3_ZeroValue;
    }
  }

  public SensorAttribute Contact3_OneValue
  {
    get
    {
      if (this._Contact3_OneValue == null)
        this._Contact3_OneValue = new SensorAttribute()
        {
          Value = "Open",
          Name = nameof (Contact3_OneValue)
        };
      return this._Contact3_OneValue;
    }
  }

  public SensorAttribute Contact4_ZeroValue
  {
    get
    {
      if (this._Contact4_ZeroValue == null)
        this._Contact4_ZeroValue = new SensorAttribute()
        {
          Value = "Closed",
          Name = nameof (Contact4_ZeroValue)
        };
      return this._Contact4_ZeroValue;
    }
  }

  public SensorAttribute Contact4_OneValue
  {
    get
    {
      if (this._Contact4_OneValue == null)
        this._Contact4_OneValue = new SensorAttribute()
        {
          Value = "Open",
          Name = nameof (Contact4_OneValue)
        };
      return this._Contact4_OneValue;
    }
  }

  public SensorAttribute Contact5_ZeroValue
  {
    get
    {
      if (this._Contact5_ZeroValue == null)
        this._Contact5_ZeroValue = new SensorAttribute()
        {
          Value = "Closed",
          Name = nameof (Contact5_ZeroValue)
        };
      return this._Contact5_ZeroValue;
    }
  }

  public SensorAttribute Contact5_OneValue
  {
    get
    {
      if (this._Contact5_OneValue == null)
        this._Contact5_OneValue = new SensorAttribute()
        {
          Value = "Open",
          Name = nameof (Contact5_OneValue)
        };
      return this._Contact5_OneValue;
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
          Value = "Contact 1"
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
          Value = "Contact 2"
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
          Value = "Contact 3"
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
          Value = "Contact 4"
        };
      return this._Contact4;
    }
  }

  public SensorAttribute Contact5Name
  {
    get
    {
      if (this._Contact5 == null)
        this._Contact5 = new SensorAttribute()
        {
          Name = "DatumIndex|4",
          Value = "Contact 5"
        };
      return this._Contact5;
    }
  }

  public static string GetContact_ZeroValue(long sensorID, int contactNumber)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == $"Contact{contactNumber.ToString()}_ZeroValue")
        return sensorAttribute.Value.ToStringSafe();
    }
    return "Closed";
  }

  public static void SetContact_ZeroValue(long sensorID, int contactNumber, string zeroValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == $"Contact{contactNumber.ToString()}_ZeroValue")
      {
        sensorAttribute.Value = zeroValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = $"Contact{contactNumber.ToString()}_ZeroValue",
        Value = zeroValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetContact_OneValue(long sensorID, int contactNumber)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == $"Contact{contactNumber.ToString()}_OneValue")
        return sensorAttribute.Value.ToStringSafe();
    }
    return "Open";
  }

  public static void SetContact_OneValue(long sensorID, int contactNumber, string oneValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == $"Contact{contactNumber.ToString()}_OneValue")
      {
        sensorAttribute.Value = oneValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = $"Contact{contactNumber.ToString()}_OneValue",
        Value = oneValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetContactLabel(long sensorID, int contactNumber)
  {
    return Sensor.GetDatumName(sensorID, contactNumber - 1);
  }

  public static void SetContactLabel(long sensorID, int contactNumber, string name)
  {
    Sensor.Load(sensorID).SetDatumName(contactNumber - 1, name);
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "ChartFormat")
        this._ChartFormat = sensorAttribute;
      if (sensorAttribute.Name == "Contact1_ZeroValue")
        this._Contact1_ZeroValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact1_OneValue")
        this._Contact1_OneValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact2_ZeroValue")
        this._Contact2_ZeroValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact2_OneValue")
        this._Contact2_OneValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact3_ZeroValue")
        this._Contact3_ZeroValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact3_OneValue")
        this._Contact3_OneValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact4_ZeroValue")
        this._Contact4_ZeroValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact4_OneValue")
        this._Contact4_OneValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact5_ZeroValue")
        this._Contact5_ZeroValue = sensorAttribute;
      if (sensorAttribute.Name == "Contact5_OneValue")
        this._Contact5_OneValue = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|0")
        this._Contact1 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|1")
        this._Contact2 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|2")
        this._Contact3 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|3")
        this._Contact4 = sensorAttribute;
      if (sensorAttribute.Name == "DatumIndex|4")
        this._Contact5 = sensorAttribute;
    }
  }

  public static string ValueForZero => "Closed";

  public static string ValueForOne => "Open";

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(FiveInputDryContact left, FiveInputDryContact right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(FiveInputDryContact left, FiveInputDryContact right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(FiveInputDryContact left, FiveInputDryContact right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(FiveInputDryContact left, FiveInputDryContact right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(FiveInputDryContact left, FiveInputDryContact right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(FiveInputDryContact left, FiveInputDryContact right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is FiveInputDryContact && this.Equals((MonnitApplicationBase) (obj as FiveInputDryContact));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is FiveInputDryContact && this.Contact1 == (right as FiveInputDryContact).Contact1;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is FiveInputDryContact && this.Contact1 != (right as FiveInputDryContact).Contact1;
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
