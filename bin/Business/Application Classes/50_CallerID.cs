// Decompiled with JetBrains decompiler
// Type: Monnit.CallerID
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

#nullable disable
namespace Monnit;

public class CallerID : MonnitApplicationBase
{
  public const int CIDSV_UNINITIALIZED = 0;
  public const int CIDSV_NOLINE = 1;
  public const int CIDSV_OFFHOOK_BUSY = 2;
  public const int CIDSV_DSL_DETECTED = 3;
  public const int CIDSV_COMPROBLEM = 4;
  public const int CIDSV_OFFHOOK = 5;
  public const int CIDSV_ONHOOK = 6;
  public const int CIDSV_OUTBOUNDCALLINPROGRESS = 7;
  public const int CIDSV_OUTBOUNDCALLCOMPLETE = 8;
  public const int CIDSV_911CALLINPROGRESS = 9;
  public const int CIDSV_INBOUNDCALLINPROGRESS = 10;
  public const int CIDSV_INBOUNDCALLCOMPLETE = 11;

  public static long MonnitApplicationID => 50;

  public static string ApplicationName => "Caller ID";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => CallerID.MonnitApplicationID;

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) CallerID.Deserialize(version, serialized);
  }

  public byte EventSeqNumber { get; set; }

  public byte CurrentState { get; set; }

  public byte RingCount { get; set; }

  public ushort OnCallTime { get; set; }

  public string sPhoneNumber { get; set; }

  public string sCallerID { get; set; }

  public override string Serialize()
  {
    return $"{this.CurrentState}|{this.EventSeqNumber}|{(this.RingCount == byte.MaxValue ? (object) string.Empty : (object) this.RingCount.ToString())}|{(this.OnCallTime == ushort.MaxValue ? (object) string.Empty : (object) this.OnCallTime.ToString())}|{this.sPhoneNumber}|{this.sCallerID}";
  }

  public override List<AppDatum> Datums => new List<AppDatum>();

  public override string NotificationString
  {
    get
    {
      string notificationString;
      switch (this.CurrentState)
      {
        case 0:
          notificationString = "ERROR";
          break;
        case 1:
          notificationString = "No line detected!";
          break;
        case 2:
          notificationString = "Phone left off hook!";
          break;
        case 3:
          notificationString = "DSL detected on line!";
          break;
        case 4:
          notificationString = "Internal hardware problem!";
          break;
        case 5:
          notificationString = "Phone is off hook";
          break;
        case 6:
          notificationString = "Phone is on hook, system ready";
          break;
        case 7:
          notificationString = $"Outbound call in progress to {this.sPhoneNumber}, Duration: {this.OnCallTime.ToString()}";
          break;
        case 8:
          notificationString = $"Outbound call complete to {this.sPhoneNumber}, Duration: {this.OnCallTime.ToString()}";
          break;
        case 9:
          notificationString = "911 call detected!";
          break;
        case 10:
          if (this.RingCount == byte.MaxValue)
          {
            notificationString = "Caller ID: " + this.sCallerID;
            break;
          }
          string str = "Inbound call in Progress from " + (string.IsNullOrEmpty(this.sPhoneNumber) ? "\"Unknown\"" : this.sPhoneNumber);
          if (this.OnCallTime != ushort.MaxValue)
            str = $"{str}, Duration: {this.OnCallTime.ToString()}";
          notificationString = $"{str} RingCount: {this.RingCount.ToString()}";
          break;
        case 11:
          notificationString = $"Inbound call complete from {(string.IsNullOrEmpty(this.sPhoneNumber) ? "\"Unknown\"" : this.sPhoneNumber)}, Duration: {(this.OnCallTime == (ushort) 0 ? "MISSED!" : this.OnCallTime.ToString())} RingCount: {this.RingCount.ToString()}";
          break;
        default:
          notificationString = $"ERROR State ({this.CurrentState.ToString()})";
          break;
      }
      return notificationString;
    }
  }

  public override object PlotValue => (object) this.EventSeqNumber;

  public static CallerID Deserialize(string version, string serialized)
  {
    CallerID callerId = new CallerID();
    if (string.IsNullOrEmpty(serialized))
    {
      callerId.CurrentState = byte.MaxValue;
      callerId.EventSeqNumber = byte.MaxValue;
      callerId.RingCount = byte.MaxValue;
      callerId.OnCallTime = ushort.MaxValue;
      callerId.sPhoneNumber = string.Empty;
      callerId.sCallerID = string.Empty;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      if (strArray.Length != 6)
      {
        callerId.CurrentState = (byte) strArray[0].ToInt();
        callerId.EventSeqNumber = (byte) strArray[0].ToInt();
        callerId.RingCount = string.IsNullOrEmpty(strArray[0]) ? byte.MaxValue : (byte) strArray[2].ToInt();
        callerId.OnCallTime = string.IsNullOrEmpty(strArray[0]) ? ushort.MaxValue : (ushort) strArray[3].ToInt();
        callerId.sPhoneNumber = strArray[0];
        callerId.sCallerID = strArray[0];
      }
      else
      {
        callerId.CurrentState = (byte) strArray[0].ToInt();
        callerId.EventSeqNumber = (byte) strArray[1].ToInt();
        callerId.RingCount = string.IsNullOrEmpty(strArray[2]) ? byte.MaxValue : (byte) strArray[2].ToInt();
        callerId.OnCallTime = string.IsNullOrEmpty(strArray[3]) ? ushort.MaxValue : (ushort) strArray[3].ToInt();
        callerId.sPhoneNumber = strArray[4];
        callerId.sCallerID = strArray[5];
      }
    }
    return callerId;
  }

  public static CallerID Create(byte[] sdm, int startIndex)
  {
    CallerID callerId = new CallerID();
    switch ((int) sdm[startIndex - 1] >> 4)
    {
      case 0:
        callerId.EventSeqNumber = sdm[startIndex];
        callerId.CurrentState = sdm[startIndex + 1];
        callerId.RingCount = byte.MaxValue;
        callerId.OnCallTime = ushort.MaxValue;
        callerId.sCallerID = string.Empty;
        callerId.sPhoneNumber = string.Empty;
        break;
      case 1:
        callerId.EventSeqNumber = sdm[startIndex];
        callerId.CurrentState = sdm[startIndex + 1];
        int num1 = callerId.CurrentState == (byte) 7 ? 1 : (callerId.CurrentState == (byte) 8 ? 1 : 0);
        callerId.RingCount = num1 == 0 ? sdm[startIndex + 2] : byte.MaxValue;
        callerId.OnCallTime = BitConverter.ToUInt16(sdm, startIndex + 3);
        callerId.sCallerID = string.Empty;
        callerId.sPhoneNumber = string.Empty;
        for (int index = 0; index < 11; ++index)
        {
          int num2 = (int) sdm[startIndex + 5 + index] & 15;
          if (num2 <= 12)
          {
            switch (num2)
            {
              case 10:
                callerId.sPhoneNumber += "0";
                break;
              case 11:
                callerId.sPhoneNumber += "*";
                break;
              case 12:
                callerId.sPhoneNumber += "#";
                break;
              default:
                if (num2 != 0)
                {
                  callerId.sPhoneNumber += num2.ToString();
                  break;
                }
                goto label_20;
            }
            num2 = (int) sdm[startIndex + 5 + index] >> 4;
            if (num2 <= 12)
            {
              switch (num2)
              {
                case 10:
                  callerId.sPhoneNumber += "0";
                  break;
                case 11:
                  callerId.sPhoneNumber += "*";
                  break;
                case 12:
                  callerId.sPhoneNumber += "#";
                  break;
                default:
                  if (num2 != 0)
                  {
                    callerId.sPhoneNumber += num2.ToString();
                    break;
                  }
                  goto label_20;
              }
            }
            else
              break;
          }
          else
            break;
        }
        break;
      default:
        callerId.EventSeqNumber = sdm[startIndex];
        callerId.CurrentState = (byte) 10;
        callerId.OnCallTime = ushort.MaxValue;
        callerId.RingCount = byte.MaxValue;
        callerId.sPhoneNumber = string.Empty;
        callerId.sCallerID = new string(Encoding.ASCII.GetChars(sdm, startIndex + 1, 15)).Replace("\0", "");
        break;
    }
label_20:
    return callerId;
  }

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    sensor.ChannelMask = sensor.DefaultValue<long>("DefaultChannelMask");
    sensor.StandardMessageDelay = sensor.DefaultValue<int>("DefaultStandardMessageDelay");
    sensor.TransmitIntervalLink = new Version(sensor.FirmwareVersion) >= new Version("2.5.0.0") ? 1 : 101;
    sensor.TransmitIntervalTest = sensor.DefaultValue<int>("DefaultTransmitIntervalTest");
    sensor.TestTransmitCount = sensor.DefaultValue<int>("DefaultTestTransmitCount");
    sensor.MaximumNetworkHops = sensor.DefaultValue<int>("DefaultMaximumNetworkHops");
    sensor.RetryCount = sensor.DefaultValue<int>("DefaultRetryCount");
    sensor.Recovery = sensor.DefaultValue<int>("DefaultRecovery");
    sensor.TimeOfDayActive = sensor.DefaultValue<byte[]>("DefaultTimeOfDayActive");
    sensor.ReportInterval = 120.0;
    sensor.ActiveStateInterval = 120.0;
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
    sensor.Calibration1 = 673712650L;
    sensor.Calibration2 = 2629150L;
    sensor.Calibration3 = 0L;
    sensor.Calibration4 = (long) uint.MaxValue;
    sensor.EventDetectionType = 0;
    sensor.EventDetectionPeriod = 50;
    sensor.EventDetectionCount = 6;
    sensor.RearmTime = 1;
    sensor.BiStable = 1;
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      CompareValue = "",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = CallerID.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    byte[] numArray1 = new byte[4]
    {
      (byte) collection["HookOnTime"].ToInt(),
      (byte) collection["HookOffTime"].ToInt(),
      (byte) collection["RingOnTime"].ToInt(),
      (byte) collection["RingOffTime"].ToInt()
    };
    sensor.Calibration1 = (long) BitConverter.ToUInt32(numArray1, 0);
    int num1 = collection["CIDStopTime"].ToInt();
    int num2 = collection["CATTime"].ToInt();
    byte[] destinationArray = new byte[4]
    {
      (byte) num1,
      (byte) 0,
      (byte) 0,
      (byte) 0
    };
    Array.Copy((Array) BitConverter.GetBytes((short) num2), 0, (Array) destinationArray, 2, 2);
    sensor.Calibration2 = (long) BitConverter.ToUInt32(destinationArray, 0);
    byte[] numArray2 = new byte[4]
    {
      (byte) collection["OutsideLineSequence1"].ToInt(),
      (byte) collection["OutsideLineSequence2"].ToInt(),
      (byte) collection["OutsideLineSequence3"].ToInt(),
      (byte) collection["OutsideLineSequence4"].ToInt()
    };
    sensor.Calibration3 = (long) BitConverter.ToUInt32(numArray2, 0);
    sensor.Save();
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(CallerID left, CallerID right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(CallerID left, CallerID right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(CallerID left, CallerID right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(CallerID left, CallerID right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(CallerID left, CallerID right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(CallerID left, CallerID right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is CallerID && this.Equals((MonnitApplicationBase) (obj as CallerID));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is CallerID && (int) this.CurrentState == (int) (right as CallerID).CurrentState;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is CallerID && (int) this.CurrentState != (int) (right as CallerID).CurrentState;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '<=' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    throw new NotSupportedException("Operator '>=' cannot be applied to operands of type 'byte[]' and 'byte[]'");
  }

  public new static void DefaultCalibrationSettings(Sensor sensor)
  {
    sensor.Calibration1 = 673712650L;
    sensor.Calibration2 = 2629150L;
    sensor.Calibration3 = 0L;
    sensor.Calibration4 = (long) uint.MaxValue;
  }
}
