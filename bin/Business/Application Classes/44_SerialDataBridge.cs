// Decompiled with JetBrains decompiler
// Type: Monnit.SerialDataBridge
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

#nullable disable
namespace Monnit;

public class SerialDataBridge : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _TerminalValue;
  private SensorAttribute _Label;

  public static long MonnitApplicationID => 44;

  public static string ApplicationName => "Binary Data";

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "terminalValue")
        this._TerminalValue = sensorAttribute;
      if (sensorAttribute.Name == "Label")
        this._Label = sensorAttribute;
    }
  }

  public SensorAttribute TerminalValue
  {
    get
    {
      if (this._TerminalValue == null)
        this._TerminalValue = new SensorAttribute()
        {
          Value = "1"
        };
      return this._TerminalValue;
    }
  }

  public SensorAttribute Label
  {
    get
    {
      if (this._Label == null)
        this._Label = new SensorAttribute()
        {
          Value = "Hex"
        };
      return this._Label;
    }
  }

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => SerialDataBridge.MonnitApplicationID;

  public string Data { get; set; }

  public int RxBytes { get; set; }

  public int TxBytes { get; set; }

  public int Error { get; set; }

  public bool isReport { get; set; }

  public override string Serialize()
  {
    if (!this.isReport)
      return $"{"false"}|{this.Data}";
    return $"{"true"}|{this.RxBytes}|{this.TxBytes}|{this.Error}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[5]
      {
        new AppDatum(eDatumType.Data, "Data", this.Data),
        new AppDatum(eDatumType.Count, "RxBytes", this.RxBytes),
        new AppDatum(eDatumType.Count, "TxBytes", this.TxBytes),
        new AppDatum(eDatumType.Count, "Error", this.Error),
        new AppDatum(eDatumType.BooleanData, "isReport", this.isReport)
      });
    }
  }

  public override List<string> GetPlotLabels(long sensorID)
  {
    return new List<string>()
    {
      "Data",
      "RxBytes",
      "TxBytes",
      "Error",
      "isReport"
    };
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) SerialDataBridge.Deserialize(version, serialized);
  }

  public override bool IsValid => true;

  public override string NotificationString
  {
    get
    {
      return this.isReport ? $"RX Bytes: {this.RxBytes}, TX Bytes: {this.TxBytes}, Errors: {this.Error}" : SerialDataBridge.HexConverter(this.Data, this.Label.Value);
    }
  }

  public override object PlotValue => (object) (this.RxBytes + this.TxBytes);

  public static SerialDataBridge Deserialize(string version, string serialized)
  {
    SerialDataBridge serialDataBridge = new SerialDataBridge();
    string[] strArray = serialized.Split('|');
    if (strArray[0].ToBool())
    {
      serialDataBridge.isReport = true;
      serialDataBridge.RxBytes = strArray[1].ToInt();
      serialDataBridge.TxBytes = strArray[2].ToInt();
      serialDataBridge.Error = strArray[3].ToInt();
      serialDataBridge.Data = string.Empty;
    }
    else
    {
      serialDataBridge.isReport = false;
      serialDataBridge.RxBytes = 0;
      serialDataBridge.TxBytes = 0;
      serialDataBridge.Error = 0;
      serialDataBridge.Data = strArray.Length > 1 ? strArray[1].ToString() : strArray[0].ToString();
    }
    return serialDataBridge;
  }

  public static SerialDataBridge Create(byte[] sdm, int startIndex)
  {
    SerialDataBridge serialDataBridge = new SerialDataBridge();
    serialDataBridge.isReport = ((int) sdm[startIndex - 1] & 16 /*0x10*/) != 16 /*0x10*/;
    if (serialDataBridge.isReport)
    {
      serialDataBridge.RxBytes = (int) BitConverter.ToUInt16(sdm, startIndex);
      serialDataBridge.TxBytes = (int) BitConverter.ToUInt16(sdm, startIndex + 2);
      serialDataBridge.Error = (int) BitConverter.ToUInt16(sdm, startIndex + 4);
      serialDataBridge.Data = string.Empty;
    }
    else
    {
      serialDataBridge.RxBytes = 0;
      serialDataBridge.TxBytes = 0;
      serialDataBridge.Error = 0;
      serialDataBridge.Data = BitConverter.ToString(sdm, startIndex).Replace("-", "");
    }
    return serialDataBridge;
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
      CompareValue = "",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Equal,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = SerialDataBridge.MonnitApplicationID,
      SnoozeDuration = 60,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    int result1 = 0;
    if (!string.IsNullOrWhiteSpace(collection["Baudrate"]) && int.TryParse(collection["Baudrate"], out result1))
      sensor.Calibration1 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration1, SerialDataBridge.SignificanceIndex.Byte_0, result1);
    if (!string.IsNullOrWhiteSpace(collection["DataBits"]) && int.TryParse(collection["DataBits"], out result1))
      sensor.Calibration1 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration1, SerialDataBridge.SignificanceIndex.Byte_1, result1);
    if (!string.IsNullOrWhiteSpace(collection["Parity"]) && int.TryParse(collection["Parity"], out result1))
      sensor.Calibration1 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration1, SerialDataBridge.SignificanceIndex.Byte_2, result1);
    if (!string.IsNullOrWhiteSpace(collection["StopBits"]) && int.TryParse(collection["StopBits"], out result1))
      sensor.Calibration1 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration1, SerialDataBridge.SignificanceIndex.Byte_3, result1);
    if (!string.IsNullOrWhiteSpace(collection["PacketSize"]) && int.TryParse(collection["PacketSize"], out result1))
      sensor.Calibration3 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration3, SerialDataBridge.SignificanceIndex.Byte_0, result1);
    if (!string.IsNullOrWhiteSpace(collection["Group"]) && int.TryParse(collection["Group"], out result1))
      sensor.Calibration3 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration3, SerialDataBridge.SignificanceIndex.Byte_1, result1);
    if (!string.IsNullOrWhiteSpace(collection["GroupInterval"]) && int.TryParse(collection["GroupInterval"], out result1))
      sensor.Calibration3 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration3, SerialDataBridge.SignificanceIndex.Byte_2, result1);
    if (!string.IsNullOrWhiteSpace(collection["PacketInterval"]) && int.TryParse(collection["PacketInterval"], out result1))
      sensor.Calibration3 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration3, SerialDataBridge.SignificanceIndex.Byte_3, result1);
    if (!string.IsNullOrWhiteSpace(collection["DeafOnSTX"]) && int.TryParse(collection["DeafOnSTX"], out result1))
      sensor.Calibration2 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration2, SerialDataBridge.SignificanceIndex.Byte_0, result1);
    if (!string.IsNullOrWhiteSpace(collection["RadioRX"]) && int.TryParse(collection["RadioRX"], out result1))
      sensor.Calibration2 = SerialDataBridge.SetCalibrationByteValue(sensor.Calibration2, SerialDataBridge.SignificanceIndex.Byte_1, result1);
    if (!string.IsNullOrWhiteSpace(collection["PollInterval"]) && int.TryParse(collection["PollInterval"], out result1))
    {
      if (result1 > (int) ushort.MaxValue)
        result1 = (int) ushort.MaxValue;
      sensor.Calibration4 = (long) result1;
    }
    if (!string.IsNullOrWhiteSpace(collection["MinimumThreshold_Manual"]) && int.TryParse(collection["MinimumThreshold_Manual"], out result1))
    {
      if (result1 < 1)
        result1 = 1;
      if (result1 > (int) ushort.MaxValue)
        result1 = (int) ushort.MaxValue;
      sensor.MinimumThreshold = (long) result1;
    }
    else
      sensor.MinimumThreshold = sensor.DefaultValue<long>("DefaultMinimumThreshold");
    if (!string.IsNullOrWhiteSpace(collection["MaximumThreshold_Manual"]) && int.TryParse(collection["MaximumThreshold_Manual"], out result1))
    {
      if (result1 < 1)
        result1 = 1;
      if (result1 > (int) ushort.MaxValue)
        result1 = (int) ushort.MaxValue;
      sensor.MaximumThreshold = (long) result1;
    }
    else
      sensor.MaximumThreshold = sensor.DefaultValue<long>("DefaultMaximumThreshold");
    double result2 = 0.0;
    if (!string.IsNullOrWhiteSpace(collection["Hysteresis_Manual"]) && double.TryParse(collection["Hysteresis_Manual"], out result2))
    {
      if (result2 < 0.01)
        result2 = 0.01;
      if (result2 > 30.0)
        result2 = 30.0;
      sensor.Hysteresis = (result2 * 1000.0).ToLong();
    }
    if (string.IsNullOrWhiteSpace(collection["ReportInterval"]) || !double.TryParse(collection["ReportInterval"], out result2))
      return;
    sensor.ReportInterval = result2;
    sensor.ActiveStateInterval = result2;
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    if (string.IsNullOrEmpty(collection["Label"]))
      return;
    SerialDataBridge.GetLabel(sensor.SensorID);
    string empty = string.Empty;
    switch (collection["Label"])
    {
      case "ascii":
        SerialDataBridge.SetLabel(sensor.SensorID, "ascii");
        break;
      case "s16bit":
        SerialDataBridge.SetLabel(sensor.SensorID, "Signed 16 bit");
        break;
      case "s32bit":
        SerialDataBridge.SetLabel(sensor.SensorID, "Signed 32 bit");
        break;
      case "s64bit":
        SerialDataBridge.SetLabel(sensor.SensorID, "Signed 64 bit");
        break;
      case "s8bit":
        SerialDataBridge.SetLabel(sensor.SensorID, "Signed 8 bit");
        break;
      case "u16bit":
        SerialDataBridge.SetLabel(sensor.SensorID, "Unsigned 16 bit");
        break;
      case "u32bit":
        SerialDataBridge.SetLabel(sensor.SensorID, "Unsigned 32 bit");
        break;
      case "u64bit":
        SerialDataBridge.SetLabel(sensor.SensorID, "Unsigned 64 bit");
        break;
      case "u8bit":
        SerialDataBridge.SetLabel(sensor.SensorID, "Unsigned 8 bit");
        break;
      default:
        SerialDataBridge.SetLabel(sensor.SensorID, "hex");
        break;
    }
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    List<byte[]> numArrayList = new List<byte[]>();
    foreach (NotificationRecorded notificationRecorded in NotificationRecorded.LoadGetNoneDevliveredMessagesForControl(sensor.SensorID))
    {
      string Data = string.Empty;
      string DataType = string.Empty;
      SerialDataBridge.ParseSerializedRecipientProperties(notificationRecorded.SerializedRecipientProperties, out DataType, out Data);
      byte[] byteArray = Data.StringToByteArray();
      if (new Version(sensor.FirmwareVersion) < new Version("2.5.5.0"))
        numArrayList.Add(SerialDataBridgeBase.getCalibrationValue(sensor.SensorID, byteArray));
      else
        numArrayList.Add(SerialDataBridgeBase.getNewCalibrationValue(sensor.SensorID, notificationRecorded.QueueID, byteArray));
    }
    if (sensor.PendingActionControlCommand && numArrayList.Count == 0)
    {
      sensor.PendingActionControlCommand = false;
      sensor.Save();
    }
    return numArrayList;
  }

  public new static void ClearPendingActionControlCommand(
    Sensor sensor,
    string version,
    int actionCommand,
    bool success,
    byte[] data)
  {
    try
    {
      if (new Version(sensor.FirmwareVersion) < new Version("2.3.0.0"))
      {
        foreach (NotificationRecorded notificationRecorded in NotificationRecorded.LoadNonAcknowledgedNotificationForDevice(sensor.SensorID))
        {
          notificationRecorded.Delivered = true;
          notificationRecorded.Status = "Sent";
          notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded.Save();
        }
      }
      else if (new Version(sensor.FirmwareVersion) == new Version("2.3.0.0"))
      {
        foreach (NotificationRecorded notificationRecorded in NotificationRecorded.LoadNonAcknowledgedNotificationForDevice(sensor.SensorID).OrderByDescending<NotificationRecorded, DateTime>((Func<NotificationRecorded, DateTime>) (d => d.NotificationDate)).ToList<NotificationRecorded>())
        {
          notificationRecorded.Delivered = true;
          notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
          notificationRecorded.Status = "Sent";
          notificationRecorded.Save();
          if (notificationRecorded.NotificationDate.AddHours(2.0) < DateTime.UtcNow && ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
          {
            long num = notificationRecorded.NotificationRecordedID;
            string str1 = num.ToString();
            num = sensor.SensorID;
            string str2 = num.ToString();
            new Exception($"Monnit.SerialDataBridge.ClearPendingActionControlCommand - Delivered acknowledgment was over two hours old for NotificationRecordedID: {str1} SensorID: {str2}").Log();
          }
        }
      }
      else
      {
        int num1 = data.Length != 0 ? (int) data[0] : 0;
        bool flag = false;
        foreach (NotificationRecorded notificationRecorded in NotificationRecorded.LoadNonAcknowledgedNotificationForDevice(sensor.SensorID))
        {
          if (notificationRecorded.QueueID == num1)
          {
            flag = true;
            notificationRecorded.Delivered = true;
            notificationRecorded.AcknowledgedDate = DateTime.UtcNow;
            notificationRecorded.Status = "Sent";
            notificationRecorded.Save();
            if (notificationRecorded.NotificationDate.AddHours(2.0) < DateTime.UtcNow && ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
            {
              string[] strArray = new string[6];
              strArray[0] = "Monnit.Control_1.ClearPendingActionControlCommand- Delivered acknowledgment was over two hours old for NotificationRecordedID: ";
              long num2 = notificationRecorded.NotificationRecordedID;
              strArray[1] = num2.ToString();
              strArray[2] = " SensorID: ";
              num2 = sensor.SensorID;
              strArray[3] = num2.ToString();
              strArray[4] = " QueueID: ";
              strArray[5] = num1.ToString();
              new Exception(string.Concat(strArray)).Log();
            }
          }
        }
        if (!flag && ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
          new Exception($"Monnit.Control_1.ClearPendingActionControlCommand- Delivered message not found for SensorID: {sensor.SensorID.ToString()} QueueID: {num1.ToString()}").Log();
      }
    }
    catch (Exception ex)
    {
      if (!ConfigData.AppSettings("LogClearPendingExceptions", "False").ToBool())
        return;
      ex.Log("Monnit.Control_1.ClearPendingActionControlCommand(Sensor sensor, string version, int actionCommand, bool success, byte[] data)");
    }
  }

  public static string ConverterToHex(string value, string label)
  {
    StringBuilder stringBuilder = new StringBuilder();
    switch (label.ToLower())
    {
      case "ascii":
        return Encoding.ASCII.GetBytes(value).FormatBytesToStringArray();
      case "64 bit":
        string[] strArray1 = value.Split(' ');
        int num1 = 0;
        foreach (string o in strArray1)
        {
          if (num1 != 3)
          {
            stringBuilder.Append(BitConverter.GetBytes(o.ToLong()).FormatBytesToStringArray());
            ++num1;
          }
          else
            break;
        }
        break;
      case "32 bit":
        string[] strArray2 = value.Split(' ');
        int num2 = 0;
        foreach (string o in strArray2)
        {
          if (num2 != 6)
          {
            stringBuilder.Append(BitConverter.GetBytes(o.ToInt()).FormatBytesToStringArray());
            ++num2;
          }
          else
            break;
        }
        break;
      case "16 bit":
        string[] strArray3 = value.Split(' ');
        int num3 = 0;
        foreach (string o in strArray3)
        {
          if (num3 != 12)
          {
            stringBuilder.Append(BitConverter.GetBytes(o.ToInt()).FormatBytesToStringArray());
            ++num3;
          }
          else
            break;
        }
        break;
      case "8 bit":
        string[] strArray4 = value.Split(' ');
        int num4 = 0;
        foreach (string o in strArray4)
        {
          if (num4 != 24)
          {
            stringBuilder.Append(BitConverter.GetBytes(o.ToInt()).FormatBytesToStringArray());
            ++num4;
          }
          else
            break;
        }
        break;
      default:
        return value;
    }
    return stringBuilder.ToString();
  }

  public static string HexConverter(string data, string label)
  {
    ulong num1 = 0;
    BitConverter.GetBytes(num1);
    string empty = string.Empty;
    string s = label;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(s))
    {
      case 301489840:
        if (s == "ascii")
        {
          try
          {
            int num2 = data.Length % 2 == 0 ? data.Length : data.Length - 1;
            StringBuilder stringBuilder = new StringBuilder();
            for (int startIndex = 0; startIndex < num2; startIndex += 2)
            {
              string str = data.Substring(startIndex, 2);
              stringBuilder.Append(Convert.ToChar(Convert.ToUInt32(str, 16 /*0x10*/)));
            }
            return stringBuilder.ToString();
          }
          catch
          {
            return "Value does not have an ascii value.";
          }
        }
        else
          break;
      case 533448741:
        if (s == "Unsigned 8 bit")
        {
          try
          {
            return byte.Parse(data, NumberStyles.HexNumber).ToString();
          }
          catch
          {
            return "Value too big or too small to be Unsigned 8bit value.";
          }
        }
        else
          break;
      case 862905410:
        if (s == "Unsigned 16 bit")
        {
          try
          {
            return ushort.Parse(data, NumberStyles.HexNumber).ToString();
          }
          catch
          {
            return "Value too big or too small to be Unsigned 16bit value.";
          }
        }
        else
          break;
      case 1459226147:
        if (s == "Unsigned 64 bit")
        {
          try
          {
            return ulong.Parse(data, NumberStyles.HexNumber).ToString();
          }
          catch
          {
            return "Value too big or too small to be Unsigned 64bit value.";
          }
        }
        else
          break;
      case 1558280034:
        if (s == "Signed 64 bit")
        {
          try
          {
            return long.Parse(data, NumberStyles.HexNumber).ToString();
          }
          catch
          {
            return "Value too big or too small to be Signed 64bit value.";
          }
        }
        else
          break;
      case 2328764161:
        if (s == "Signed 32 bit")
        {
          try
          {
            return int.Parse(data, NumberStyles.HexNumber).ToString();
          }
          catch
          {
            return "Value too big or too small to be Signed 32bit value.";
          }
        }
        else
          break;
      case 2743546327:
        if (s == "Signed 16 bit")
        {
          try
          {
            return short.Parse(data, NumberStyles.HexNumber).ToString();
          }
          catch
          {
            return "Value too big or too small to be Signed 16bit value.";
          }
        }
        else
          break;
      case 3386487114:
        if (s == "Signed 8 bit")
        {
          try
          {
            return sbyte.Parse(data, NumberStyles.HexNumber).ToString();
          }
          catch
          {
            return "Value too big or too small to be Signed 8bit value.";
          }
        }
        else
          break;
      case 4008710644:
        if (s == "Unsigned 32 bit")
        {
          try
          {
            uint.Parse(data, NumberStyles.HexNumber);
            return num1.ToString();
          }
          catch
          {
            return "Value too big or too small to be Unsigned 32bit value.";
          }
        }
        else
          break;
      case 4273249610:
        if (s == "hex")
          break;
        break;
    }
    return data;
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "Hex";
  }

  public static void SetLabel(long sensorID, string label)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
      {
        sensorAttribute.Value = label;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "Label",
        Value = label,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetTerminalValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "terminalValue")
        return sensorAttribute.Value;
    }
    return "0x0000";
  }

  public static void SetTerminalValue(long sensorID, string transformValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "terminalValue")
      {
        sensorAttribute.Value = transformValue;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "terminalValue",
        Value = transformValue,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : ((double) sensor.Hysteresis / 1000.0).ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : sensor.MaximumThreshold.ToString();
  }

  public new static bool ActionControlCommandIsUrgent(Sensor sensor, string version) => true;

  public static void Terminal(Sensor sensor, NameValueCollection collection)
  {
    string data = collection["Data"];
    string DataType = collection["DataTranslation"];
    try
    {
      new NotificationRecorded()
      {
        SentToDeviceID = sensor.SensorID,
        QueueID = NotificationRecorded.NextQueueID(sensor.SensorID),
        NotificationDate = DateTime.UtcNow,
        NotificationType = eNotificationType.SerialDataBridge_Terminal,
        SerializedRecipientProperties = $"{DataType}|{SerialDataBridge.ChangeDataValueToHex(data, DataType)}"
      }.Save();
      sensor.PendingActionControlCommand = true;
      sensor.Save();
      CSNet.SetGatewaysUrgentTrafficFlag(sensor.CSNetID);
    }
    catch (ArgumentException ex)
    {
      throw ex;
    }
    catch (Exception ex)
    {
      throw new Exception("Unable to Process Terminal Command.");
    }
  }

  public static string ChangeDataValueToHex(string data, string DataType)
  {
    switch (DataType)
    {
      case "ascii":
        char[] chArray = data.Length < 26 && data.Length != 0 ? data.ToCharArray() : throw new ArgumentException("ASCII comand must contain at least 1 character and no more then 25 characters.");
        string empty1 = string.Empty;
        foreach (char ch in chArray)
        {
          int int32 = Convert.ToInt32(ch);
          empty1 += $"{int32:X}";
        }
        return empty1;
      case "hex":
        if (data.Length == 1)
          data = "0" + data;
        if (data.Length > 50 || data.Length < 0)
          throw new ArgumentException("A hex comand must be between 1 and 50 characters 0-9 A-F.");
        if (!Regex.IsMatch(data, "\\A\\b[0-9a-fA-F]+\\b\\Z"))
          throw new ArgumentException("A hex comand must be between 1 and 50 characters 0-9 A-F.");
        break;
      case "s16bit":
        short int16 = Convert.ToInt16(data);
        if (int16 > short.MaxValue || int16 < short.MinValue)
          throw new ArgumentException("A signed 16 bit comand must be between -32768 and 32767.");
        string empty2 = string.Empty;
        byte[] bytes1 = BitConverter.GetBytes(int16);
        for (int index = bytes1.Length - 1; index >= 0; --index)
          empty2 += bytes1[index].ToString("X");
        return empty2;
      case "s32bit":
        int int32_1 = Convert.ToInt32(data);
        if (int32_1 > int.MaxValue || int32_1 < int.MinValue)
          throw new ArgumentException("A signed 32 bit comand must be between -2147483648 and 2147483647.");
        string empty3 = string.Empty;
        byte[] bytes2 = BitConverter.GetBytes(int32_1);
        for (int index = bytes2.Length - 1; index >= 0; --index)
          empty3 += bytes2[index].ToString("X");
        return empty3;
      case "s64bit":
        long int64 = Convert.ToInt64(data);
        if (int64 > long.MaxValue || int64 < long.MinValue)
          throw new ArgumentException("A signed 64 bit comand must be between -9223372036854775808 and 9223372036854775807.");
        string empty4 = string.Empty;
        byte[] bytes3 = BitConverter.GetBytes(int64);
        for (int index = bytes3.Length - 1; index >= 0; --index)
          empty4 += bytes3[index].ToString("X");
        return empty4;
      case "s8bit":
        sbyte num1 = Convert.ToSByte(data);
        if (num1 >= sbyte.MaxValue || num1 < sbyte.MinValue)
          throw new ArgumentException("A signed 8 bit comand must be between -128 and 127.");
        string empty5 = string.Empty;
        byte[] bytes4 = BitConverter.GetBytes((short) num1);
        for (int index = bytes4.Length - 1; index >= 0; --index)
          empty5 += bytes4[index].ToString("X");
        return empty5;
      case "u16bit":
        ushort uint16 = Convert.ToUInt16(data);
        if (uint16 > ushort.MaxValue || uint16 < (ushort) 0)
          throw new ArgumentException("An unsigned 16 bit comand must be between 0 and 65535.");
        string empty6 = string.Empty;
        byte[] bytes5 = BitConverter.GetBytes(uint16);
        for (int index = bytes5.Length - 1; index >= 0; --index)
          empty6 += bytes5[index].ToString("X");
        return empty6;
      case "u32bit":
        uint uint32 = Convert.ToUInt32(data);
        if (uint32 > uint.MaxValue || uint32 < 0U)
          throw new ArgumentException("An unsigned 32 bit comand must be between 0 and 4294967295.");
        string empty7 = string.Empty;
        byte[] bytes6 = BitConverter.GetBytes(uint32);
        for (int index = bytes6.Length - 1; index >= 0; --index)
          empty7 += bytes6[index].ToString("X");
        return empty7;
      case "u64bit":
        ulong uint64 = Convert.ToUInt64(data);
        if (uint64 > ulong.MaxValue || uint64 < 0UL)
          throw new ArgumentException("An unsigned 64 bit comand must be between 0 and 18446744073709551615.");
        string empty8 = string.Empty;
        byte[] bytes7 = BitConverter.GetBytes(uint64);
        for (int index = bytes7.Length - 1; index >= 0; --index)
          empty8 += bytes7[index].ToString("X");
        return empty8;
      case "u8bit":
        byte num2 = Convert.ToByte(data);
        if (num2 >= byte.MaxValue || num2 < (byte) 0)
          throw new ArgumentException("An unsigned 8 bit comand must be between 0 and 255.");
        string empty9 = string.Empty;
        byte[] bytes8 = BitConverter.GetBytes((short) num2);
        for (int index = bytes8.Length - 1; index >= 0; --index)
          empty9 += bytes8[index].ToString("X");
        return empty9;
    }
    return data;
  }

  public static string ChangeDataValueFromHex(string data, string DataType)
  {
    ulong num = 0;
    BitConverter.GetBytes(num);
    string empty = string.Empty;
    switch (DataType)
    {
      case "ascii":
        StringBuilder stringBuilder = new StringBuilder();
        for (int startIndex = 0; startIndex < data.Length; startIndex += 2)
        {
          string str = data.Substring(startIndex, 2);
          stringBuilder.Append(Convert.ToChar(Convert.ToUInt32(str, 16 /*0x10*/)));
        }
        return stringBuilder.ToString();
      case "s16bit":
        return short.Parse(data, NumberStyles.HexNumber).ToString();
      case "s32bit":
        return int.Parse(data, NumberStyles.HexNumber).ToString();
      case "s64bit":
        return long.Parse(data, NumberStyles.HexNumber).ToString();
      case "s8bit":
        return sbyte.Parse(data, NumberStyles.HexNumber).ToString();
      case "u16bit":
        return ushort.Parse(data, NumberStyles.HexNumber).ToString();
      case "u32bit":
        uint.Parse(data, NumberStyles.HexNumber);
        return num.ToString();
      case "u64bit":
        return ulong.Parse(data, NumberStyles.HexNumber).ToString();
      case "u8bit":
        return byte.Parse(data, NumberStyles.HexNumber).ToString();
      default:
        return data;
    }
  }

  public static string CreateSerializedRecipientProperties(string DataType, string Data)
  {
    if (string.IsNullOrWhiteSpace(DataType))
      DataType = "hex";
    if (string.IsNullOrWhiteSpace(Data))
      Data = string.Empty;
    return $"{DataType}|{Data}";
  }

  public static void ParseSerializedRecipientProperties(
    string serialized,
    out string DataType,
    out string Data)
  {
    Data = "";
    DataType = "";
    try
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      DataType = strArray[0];
      Data = strArray[1];
    }
    catch
    {
      string[] strArray = serialized.Split(new char[1]
      {
        '|'
      }, StringSplitOptions.RemoveEmptyEntries);
      DataType = strArray[0];
      Data = "";
    }
  }

  public static int GetCalibrationByteValue(
    long CalibrationValue,
    SerialDataBridge.SignificanceIndex index)
  {
    int calibrationByteValue = 0;
    switch (index)
    {
      case SerialDataBridge.SignificanceIndex.Byte_0:
        calibrationByteValue = Convert.ToInt32(CalibrationValue & (long) byte.MaxValue);
        break;
      case SerialDataBridge.SignificanceIndex.Byte_1:
        calibrationByteValue = Convert.ToInt32((CalibrationValue & 65280L) >> 8);
        break;
      case SerialDataBridge.SignificanceIndex.Byte_2:
        calibrationByteValue = Convert.ToInt32((CalibrationValue & 16711680L /*0xFF0000*/) >> 16 /*0x10*/);
        break;
      case SerialDataBridge.SignificanceIndex.Byte_3:
        calibrationByteValue = Convert.ToInt32((CalibrationValue & 4278190080L /*0xFF000000*/) >> 24);
        break;
    }
    return calibrationByteValue;
  }

  public static long SetCalibrationByteValue(
    long CalibrationValue,
    SerialDataBridge.SignificanceIndex index,
    int value)
  {
    switch (index)
    {
      case SerialDataBridge.SignificanceIndex.Byte_0:
        CalibrationValue = (long) ((uint) ((ulong) Convert.ToInt32(CalibrationValue) & 4294967040UL) | (uint) value);
        break;
      case SerialDataBridge.SignificanceIndex.Byte_1:
        CalibrationValue = (long) ((uint) ((ulong) Convert.ToInt32(CalibrationValue) & 4294902015UL) | (uint) (value << 8));
        break;
      case SerialDataBridge.SignificanceIndex.Byte_2:
        CalibrationValue = (long) ((uint) ((ulong) Convert.ToInt32(CalibrationValue) & 4278255615UL) | (uint) (value << 16 /*0x10*/));
        break;
      case SerialDataBridge.SignificanceIndex.Byte_3:
        CalibrationValue = (long) ((uint) (Convert.ToInt32(CalibrationValue) & 16777215 /*0xFFFFFF*/) | (uint) (value << 24));
        break;
    }
    return CalibrationValue;
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(SerialDataBridge left, SerialDataBridge right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(SerialDataBridge left, SerialDataBridge right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(SerialDataBridge left, SerialDataBridge right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(SerialDataBridge left, SerialDataBridge right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(SerialDataBridge left, SerialDataBridge right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(SerialDataBridge left, SerialDataBridge right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is SerialDataBridge && this.Equals((MonnitApplicationBase) (obj as SerialDataBridge));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is SerialDataBridge && this.Data == (right as SerialDataBridge).Data;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is SerialDataBridge && this.Data != (right as SerialDataBridge).Data;
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

  public static Dictionary<string, string> BaudRateValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "0",
        "1200"
      },
      {
        "1",
        "2400"
      },
      {
        "3",
        "4800"
      },
      {
        "4",
        "9600"
      },
      {
        "5",
        "14400"
      },
      {
        "6",
        "19200"
      },
      {
        "7",
        "28800"
      },
      {
        "8",
        "38400"
      },
      {
        "9",
        "57600"
      },
      {
        "10",
        "115200"
      },
      {
        "11",
        "230400"
      }
    };
  }

  public static Dictionary<string, string> DataBitValues()
  {
    return new Dictionary<string, string>() { { "8", "8" } };
  }

  public static Dictionary<string, string> PariityValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "0",
        "None"
      },
      {
        "1",
        "Odd"
      },
      {
        "2",
        "Even"
      }
    };
  }

  public static Dictionary<string, string> StopBitsValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "1",
        "1"
      },
      {
        "2",
        "2"
      }
    };
  }

  public static Dictionary<string, string> GroupValues()
  {
    return new Dictionary<string, string>()
    {
      {
        "1",
        "1"
      },
      {
        "2",
        "2"
      },
      {
        "3",
        "3"
      },
      {
        "4",
        "4"
      },
      {
        "5",
        "5"
      }
    };
  }

  public static Dictionary<string, string> MeasurementScaleValue()
  {
    return new Dictionary<string, string>()
    {
      {
        "hex",
        "Hex"
      },
      {
        "ascii",
        "ASCII"
      },
      {
        "Unsigned 64 bit",
        "Unsigned 64 Bit Decimal"
      },
      {
        "Unsigned 32 bit",
        "Unsigned 32 Bit Decimal"
      },
      {
        "Unsigned 16 bit",
        "Unsigned 16 Bit Decimal"
      },
      {
        "Unsigned 8 bit",
        "Unsigned 8 Bit Decima"
      },
      {
        "Signed 64 bit",
        "Signed 64 Bit Decimal"
      },
      {
        "Signed 32 bit",
        "Signed 32 Bit Decimal"
      },
      {
        "Signed 16 bit",
        "Signed 16 Bit Decimal"
      },
      {
        "Signed 8 bit",
        "Signed 8 Bit Decimal"
      }
    };
  }

  public new static NotificationScaleInfoModel GetScalingInfo(Sensor sens)
  {
    NotificationScaleInfoModel scalingInfo = new NotificationScaleInfoModel();
    SerialDataBridge serialDataBridge = new SerialDataBridge();
    serialDataBridge.SetSensorAttributes(sens.SensorID);
    scalingInfo.Label = serialDataBridge.Label.Value.ToStringSafe();
    scalingInfo.sensor = sens;
    return scalingInfo;
  }

  public enum SignificanceIndex
  {
    Byte_0,
    Byte_1,
    Byte_2,
    Byte_3,
  }
}
