// Decompiled with JetBrains decompiler
// Type: Monnit.Tilt
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;

#nullable disable
namespace Monnit;

public class Tilt : MonnitApplicationBase, ISensorAttribute
{
  private bool isNotification = false;
  private SensorAttribute _RorD;
  private SensorAttribute _PitchOffSet;
  private SensorAttribute _RollOffSet;

  public static long MonnitApplicationID => TiltBase.MonnitApplicationID;

  public static string ApplicationName => "Accelerometer - Tilt";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "Line";

  public override long ApplicationID => Tilt.MonnitApplicationID;

  public double Pitch { get; set; }

  public double PitchWithOffset
  {
    get
    {
      if (this.isNotification)
        return this.Pitch;
      double pitchWithOffset = this.Pitch + this.PitchOffSet.Value.ToDouble();
      if (pitchWithOffset <= -180.0)
        pitchWithOffset += 360.0;
      if (pitchWithOffset > 180.0)
        pitchWithOffset += -360.0;
      return pitchWithOffset;
    }
  }

  public double Roll { get; set; }

  public double RollWithOffSet
  {
    get
    {
      if (this.isNotification)
        return this.Roll;
      double rollWithOffSet = this.Roll + this.RollOffSet.Value.ToDouble();
      if (rollWithOffSet <= -180.0)
        rollWithOffSet += 360.0;
      if (rollWithOffSet > 180.0)
        rollWithOffSet += -360.0;
      return rollWithOffSet;
    }
  }

  public int state { get; set; }

  public override string Serialize()
  {
    return $"{this.Pitch.ToString()},{this.Roll.ToString()},{this.state.ToString()}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[2]
      {
        new AppDatum(eDatumType.Angle, "Pitch", this.Pitch),
        new AppDatum(eDatumType.Angle, "Roll", this.Roll)
      });
    }
  }

  public override List<object> GetPlotValues(long sensorID)
  {
    return new List<object>()
    {
      (object) this.PitchWithOffset,
      (object) this.RollWithOffSet
    };
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>() { "Pitch", "Roll" };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) Tilt.Deserialize(version, serialized);
  }

  public override bool IsValid => (this.state & 16 /*0x10*/) != 16 /*0x10*/;

  public override string NotificationString
  {
    get
    {
      int num = this.state >> 4;
      if (num > 0)
      {
        if ((num & 3) == 3)
          return "Readings Unstable";
        if ((num & 1) == 1)
          return "Hardware Not Detected";
        if ((num & 2) == 2)
          return "Comm Failure";
      }
      return $" Pitch: {this.PitchWithOffset.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)} Roll: {this.RollWithOffSet.ToString("#0.##", (IFormatProvider) CultureInfo.InvariantCulture)}";
    }
  }

  public static Tilt Deserialize(string version, string serialized)
  {
    Tilt tilt = new Tilt();
    if (string.IsNullOrEmpty(serialized))
    {
      tilt.Pitch = 0.0;
      tilt.Roll = 0.0;
      tilt.state = 0;
    }
    else
    {
      string[] strArray = serialized.Split(',');
      tilt.Pitch = Convert.ToDouble(strArray[0]).ToDouble();
      if (strArray.Length > 1)
      {
        tilt.Roll = Convert.ToDouble(strArray[1]);
        try
        {
          tilt.state = strArray[2].ToInt();
        }
        catch
        {
          tilt.state = 0;
        }
      }
      else
      {
        tilt.isNotification = true;
        tilt.Roll = tilt.Pitch;
      }
    }
    return tilt;
  }

  public static Tilt Create(byte[] sdm, int startIndex)
  {
    return new Tilt()
    {
      state = (int) sdm[startIndex - 1],
      Pitch = Convert.ToDouble((double) BitConverter.ToInt16(sdm, startIndex) / 100.0),
      Roll = Convert.ToDouble((double) BitConverter.ToInt16(sdm, startIndex + 2) / 100.0)
    };
  }

  public static bool IsDegrees(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RorD" && sensorAttribute.Value == "R")
        return false;
    }
    return true;
  }

  public static void MakeDegree(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RorD")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeRadian(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RorD")
      {
        if (sensorAttribute.Value != "R")
        {
          sensorAttribute.Value = "R";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "RorD",
        Value = "R",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute RorD => this._RorD;

  public SensorAttribute PitchOffSet
  {
    get
    {
      if (this._PitchOffSet != null)
        return this._PitchOffSet;
      return new SensorAttribute()
      {
        Name = nameof (PitchOffSet),
        Value = "0"
      };
    }
  }

  public SensorAttribute RollOffSet
  {
    get
    {
      if (this._RollOffSet != null)
        return this._RollOffSet;
      return new SensorAttribute()
      {
        Name = nameof (RollOffSet),
        Value = "0"
      };
    }
  }

  public static void setOffset(long sensorID, double pitchValue, double rollValue)
  {
    bool flag1 = false;
    bool flag2 = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "PitchOffSet")
      {
        sensorAttribute.Value = pitchValue.ToString();
        sensorAttribute.Save();
        flag1 = true;
      }
      if (sensorAttribute.Name == "RollOffSet")
      {
        sensorAttribute.Value = rollValue.ToString();
        sensorAttribute.Save();
        flag2 = true;
      }
    }
    if (!flag1)
      new SensorAttribute()
      {
        Name = "PitchOffSet",
        SensorID = sensorID,
        Value = pitchValue.ToString()
      }.Save();
    if (!flag2)
      new SensorAttribute()
      {
        Name = "RollOffSet",
        SensorID = sensorID,
        Value = rollValue.ToString()
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "RorD")
        this._RorD = sensorAttribute;
      if (sensorAttribute.Name == "PitchOffSet")
        this._PitchOffSet = sensorAttribute;
      if (sensorAttribute.Name == "RollOffSet")
        this._RollOffSet = sensorAttribute;
    }
  }

  public override object PlotValue => (object) this.PitchWithOffset;

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
      SnoozeDuration = 60,
      CompareValue = "0",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = Tilt.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static Dictionary<string, string> NotificationScaleValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "D",
        "Degree"
      },
      {
        "R",
        "Radian"
      }
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (!string.IsNullOrWhiteSpace(collection["ReportInterval"]))
      sensor.ReportInterval = collection["ReportInterval"].ToDouble();
    if (!string.IsNullOrWhiteSpace(collection["ActiveStateInterval"]))
      sensor.ActiveStateInterval = collection["ActiveStateInterval"].ToDouble() <= sensor.ReportInterval ? collection["ActiveStateInterval"].ToDouble() : sensor.ReportInterval;
    bool flag = Tilt.IsDegrees(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["BasicThreshold"]) && !string.IsNullOrEmpty(collection["BasicThresholdDirection"]) && collection["BasicThresholdDirection"] != "-1")
    {
      sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if (collection["BasicThresholdDirection"].ToInt() == 0)
        sensor.MinimumThreshold = ((flag ? collection["BasicThreshold"].ToDouble().ToRadian() : collection["BasicThreshold"].ToDouble()) * 1000.0).ToLong();
      else if (collection["BasicThresholdDirection"].ToInt() == 1)
        sensor.MaximumThreshold = ((flag ? collection["BasicThreshold"].ToDouble().ToRadian() : collection["BasicThreshold"].ToDouble()) * 1000.0).ToLong();
      sensor.MeasurementsPerTransmission = 6;
    }
    else
    {
      double result;
      if ((string.IsNullOrEmpty(collection["Hysteresis_Manual"]) || !double.TryParse(collection["Hysteresis_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("Hysteresis_Manual"))
        sensor.Hysteresis = sensor.DefaultValue<long>("DefaultHysteresis");
      if ((string.IsNullOrEmpty(collection["MinimumThreshold_Manual"]) || !double.TryParse(collection["MinimumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MinimumThreshold_Manual"))
        sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
      if ((string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]) || !double.TryParse(collection["MaximumThreshold_Manual"], out result)) && ((IEnumerable<string>) collection.AllKeys).Contains<string>("MaximumThreshold_Manual"))
        sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
      if (!string.IsNullOrWhiteSpace(collection["Hysteresis_Manual"]))
        sensor.Hysteresis = (long) collection["Hysteresis_Manual"].ToInt();
      if (!string.IsNullOrEmpty(collection["pitchMin_Manual"]) && !string.IsNullOrEmpty(collection["pitchMax_Manual"]))
      {
        int num1 = collection["pitchMin_Manual"].ToInt();
        int num2 = collection["pitchMax_Manual"].ToInt();
        if (num1 < -180)
          num1 = -180;
        if (num2 > 180)
          num2 = 180;
        if (num1 > num2)
          num1 = num2;
        if (num2 < num1)
          num2 = num1;
        Tilt.SetPitchMin(sensor, num1);
        Tilt.SetPitchMax(sensor, num2);
      }
      if (!string.IsNullOrEmpty(collection["rollMin_Manual"]) && !string.IsNullOrEmpty(collection["rollMax_Manual"]))
      {
        int num3 = collection["rollMin_Manual"].ToInt();
        int num4 = collection["rollMax_Manual"].ToInt();
        if (num3 < -180)
          num3 = -180;
        if (num4 > 180)
          num4 = 180;
        if (num3 > num4)
          num3 = num4;
        if (num4 < num3)
          num4 = num3;
        Tilt.SetRollMin(sensor, num3);
        Tilt.SetRollMax(sensor, num4);
      }
      if (!string.IsNullOrEmpty(collection["PrincipalAxisSelection"]))
      {
        int num = collection["PrincipalAxisSelection"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 2)
          num = 2;
        sensor.Calibration2 = (long) num;
      }
      if (!string.IsNullOrEmpty(collection["DominantAxisSelection"]))
      {
        int num = collection["DominantAxisSelection"].ToInt();
        if (num < 0)
          num = 0;
        if (num > 2)
          num = 2;
        sensor.Calibration4 = (long) num;
      }
      if (!string.IsNullOrEmpty(collection["InvertPitch"]))
        Tilt.SetInvertPitch(sensor, collection["InvertPitch"].ToInt());
      if (!string.IsNullOrEmpty(collection["InvertRoll"]))
        Tilt.SetInvertRoll(sensor, collection["InvertRoll"].ToInt());
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    double pitchValue = !string.IsNullOrWhiteSpace(collection["PitchOffset"]) ? (collection["PitchOffset"].ToDouble() <= 180.0 ? (collection["PitchOffset"].ToDouble() >= -180.0 ? collection["PitchOffset"].ToDouble() : -180.0) : 180.0) : 0.0;
    double rollValue = !string.IsNullOrWhiteSpace(collection["RollOffset"]) ? (collection["RollOffset"].ToDouble() <= 180.0 ? (collection["RollOffset"].ToDouble() >= -180.0 ? collection["RollOffset"].ToDouble() : -180.0) : 180.0) : 0.0;
    Tilt.setOffset(sensor.SensorID, pitchValue, rollValue);
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : Convert.ToDouble(sensor.Hysteresis).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.MinimumThreshold) / 1000.0).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : (Convert.ToDouble(sensor.MaximumThreshold) / 1000.0).ToString("#0.###", (IFormatProvider) CultureInfo.InvariantCulture);
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(Tilt left, Tilt right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(Tilt left, Tilt right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(Tilt left, Tilt right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(Tilt left, Tilt right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(Tilt left, Tilt right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(Tilt left, Tilt right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is Tilt && this.Equals((MonnitApplicationBase) (obj as Tilt));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is Tilt && this.PitchWithOffset == (right as Tilt).PitchWithOffset;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is Tilt && this.PitchWithOffset != (right as Tilt).PitchWithOffset;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is Tilt && this.PitchWithOffset < (right as Tilt).PitchWithOffset;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is Tilt && this.PitchWithOffset <= (right as Tilt).PitchWithOffset;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is Tilt && this.PitchWithOffset > (right as Tilt).PitchWithOffset;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is Tilt && this.PitchWithOffset >= (right as Tilt).PitchWithOffset;
  }

  public static void SetPitchMin(Sensor sens, int value)
  {
    int num = (int) sens.MinimumThreshold & (int) ushort.MaxValue;
    sens.MinimumThreshold = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetPitchMin(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) ((long) (int) sens.MinimumThreshold & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetPitchMax(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.MinimumThreshold & 4294901760L);
    sens.MinimumThreshold = (long) (num | (int) (ushort) value);
  }

  public static int GetPitchMax(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) sens.MinimumThreshold & (uint) ushort.MaxValue);
  }

  public static void SetRollMin(Sensor sens, int value)
  {
    int num = (int) sens.MaximumThreshold & (int) ushort.MaxValue;
    sens.MaximumThreshold = (long) (num | (int) (ushort) value << 16 /*0x10*/);
  }

  public static int GetRollMin(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) ((long) (int) sens.MaximumThreshold & 4294901760L) >> 16 /*0x10*/);
  }

  public static void SetRollMax(Sensor sens, int value)
  {
    int num = (int) ((long) (int) sens.MaximumThreshold & 4294901760L);
    sens.MaximumThreshold = (long) (num | (int) (ushort) value);
  }

  public static int GetRollMax(Sensor sens)
  {
    return (int) (short) (ushort) ((uint) (int) sens.MaximumThreshold & (uint) ushort.MaxValue);
  }

  public static void SetInvertPitch(Sensor sens, int value)
  {
    uint num = Convert.ToUInt32(sens.Calibration3) & 4294967040U;
    sens.Calibration3 = (long) (num | (uint) (value & (int) byte.MaxValue));
  }

  public static int GetInvertPitch(Sensor sens)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sens.Calibration3 & (long) byte.MaxValue));
  }

  public static void SetInvertRoll(Sensor sens, int value)
  {
    uint num = Convert.ToUInt32(sens.Calibration3) & 4294902015U;
    sens.Calibration3 = (long) (num | (uint) ((value & (int) byte.MaxValue) << 8));
  }

  public static int GetInvertRoll(Sensor sens)
  {
    return (int) Convert.ToUInt16(Convert.ToUInt32(sens.Calibration3 & 65280L) >> 8);
  }

  public static long DefaultMinThreshold => -180;

  public static long DefaultMaxThreshold => 180;

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = sensor.DefaultValue<long>("DefaultCalibration1");
    sensor.Calibration2 = sensor.DefaultValue<long>("DefaultCalibration2");
    sensor.Calibration3 = sensor.DefaultValue<long>("DefaultCalibration3");
    sensor.Calibration4 = sensor.DefaultValue<long>("DefaultCalibration4");
    foreach (BaseDBObject baseDbObject in SensorAttribute.LoadBySensorID(sensor.SensorID))
      baseDbObject.Delete();
    SensorAttribute.ResetAttributeList(sensor.SensorID);
  }
}
