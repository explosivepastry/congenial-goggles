// Decompiled with JetBrains decompiler
// Type: Monnit.PeopleCounter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

#nullable disable
namespace Monnit;

public class PeopleCounter : MonnitApplicationBase, ISensorAttribute
{
  private SensorAttribute _CorF;
  private SensorAttribute _HeightUIValue;

  public static long MonnitApplicationID => PeopleCounterBase.MonnitApplicationID;

  public static string ApplicationName => "People Counter";

  public static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public override string ChartType => "None";

  public override long ApplicationID => PeopleCounter.MonnitApplicationID;

  public long OccupancyCount { get; set; }

  public long InCount { get; set; }

  public long OutCount { get; set; }

  public int stsState { get; set; }

  public static PeopleCounter Create(byte[] sdm, int startIndex)
  {
    return new PeopleCounter()
    {
      stsState = (int) sdm[startIndex - 1] >> 4,
      OccupancyCount = (long) BitConverter.ToInt32(sdm, startIndex),
      InCount = (long) BitConverter.ToUInt32(sdm, startIndex + 4),
      OutCount = (long) BitConverter.ToUInt32(sdm, startIndex + 8)
    };
  }

  public override string Serialize()
  {
    return $"{this.OccupancyCount}|{this.InCount}|{this.OutCount}|{this.stsState}";
  }

  public override List<AppDatum> Datums
  {
    get
    {
      return new List<AppDatum>((IEnumerable<AppDatum>) new AppDatum[3]
      {
        new AppDatum(eDatumType.CountSigned, "OccupancyCount", this.OccupancyCount),
        new AppDatum(eDatumType.Count, "InCount", this.InCount),
        new AppDatum(eDatumType.Count, "OutCount", this.OutCount)
      });
    }
  }

  public static PeopleCounter Deserialize(string version, string serialized)
  {
    PeopleCounter peopleCounter = new PeopleCounter();
    if (string.IsNullOrEmpty(serialized))
    {
      peopleCounter.stsState = 0;
      peopleCounter.OccupancyCount = 0L;
      peopleCounter.InCount = 0L;
      peopleCounter.OutCount = 0L;
    }
    else
    {
      string[] strArray = serialized.Split('|');
      peopleCounter.OccupancyCount = (long) Convert.ToInt32(strArray[0]);
      if (strArray.Length > 1)
      {
        peopleCounter.InCount = (long) Convert.ToUInt32(strArray[1]);
        peopleCounter.OutCount = (long) Convert.ToUInt32(strArray[2]);
        try
        {
          peopleCounter.stsState = strArray[3].ToInt();
        }
        catch
        {
          peopleCounter.stsState = 0;
        }
      }
      else
      {
        peopleCounter.InCount = (long) Convert.ToUInt32(strArray[0]);
        peopleCounter.OutCount = (long) Convert.ToUInt32(strArray[0]);
      }
    }
    return peopleCounter;
  }

  public override MonnitApplicationBase _Deserialize(string version, string serialized)
  {
    return (MonnitApplicationBase) PeopleCounter.Deserialize(version, serialized);
  }

  public static bool IsFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF" && sensorAttribute.Value == "C")
        return false;
    }
    return true;
  }

  public static void MakeFahrenheit(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        sensorAttribute.Delete();
    }
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static void MakeCelsius(long sensorID)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
      {
        if (sensorAttribute.Value != "C")
        {
          sensorAttribute.Value = "C";
          sensorAttribute.Save();
        }
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "CorF",
        Value = "C",
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public SensorAttribute CorF => this._CorF;

  public void SetSensorAttributes(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "CorF")
        this._CorF = sensorAttribute;
      if (sensorAttribute.Name == "HeightUIValue")
        this._HeightUIValue = sensorAttribute;
    }
  }

  public SensorAttribute HeightUIValue
  {
    get
    {
      if (this._HeightUIValue == null)
        this._HeightUIValue = new SensorAttribute()
        {
          Value = "0",
          Name = nameof (HeightUIValue)
        };
      return this._HeightUIValue;
    }
  }

  public static int GetHeightUIValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HeightUIValue")
        return sensorAttribute.Value.ToInt();
    }
    return 0;
  }

  public static void SetHeightUIValue(long sensorID, int height)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HeightUIValue")
      {
        sensorAttribute.Value = height.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "HeightUIValue",
        Value = height.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public override string NotificationString
  {
    get
    {
      return this.stsState == 1 ? "Sensor Failure" : $"Occupancy Count: {this.OccupancyCount} , In Count: {this.InCount} , Out Count: {this.OutCount}";
    }
  }

  public override object PlotValue => (object) this.OccupancyCount;

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
      ApplicationID = PeopleCounter.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    PeopleCounterBase.GetDefaults(new Version(sensor.FirmwareVersion), sensor.GenerationType);
    if (!string.IsNullOrEmpty(collection["MaximumThreshold_Manual"]))
    {
      sensor.MaximumThreshold = collection["MaximumThreshold_Manual"].ToLong();
      if (sensor.MaximumThreshold < (long) int.MinValue)
        sensor.MaximumThreshold = (long) int.MinValue;
      if (sensor.MaximumThreshold > (long) int.MaxValue)
        sensor.MaximumThreshold = (long) int.MaxValue;
    }
    sensor.MinimumThreshold = !(collection["ReportOnCount"] == "on") ? 0L : 1L;
    if (string.IsNullOrEmpty(collection["height"]))
      return;
    int height = collection["height"].ToInt();
    if (height < 5)
      height = 5;
    if (height > 10)
      height = 10;
    PeopleCounter.SetHeightUIValue(sensor.SensorID, height);
    switch (height)
    {
      case 6:
        sensor.Calibration1 = 7L;
        sensor.Calibration2 = 8L;
        sensor.Calibration3 = 7L;
        sensor.Calibration4 = 2L;
        break;
      case 7:
        sensor.Calibration1 = 7L;
        sensor.Calibration2 = 8L;
        sensor.Calibration3 = 5L;
        sensor.Calibration4 = 2L;
        break;
      case 8:
        sensor.Calibration1 = 7L;
        sensor.Calibration2 = 8L;
        sensor.Calibration3 = 3L;
        sensor.Calibration4 = 2L;
        break;
      case 9:
        sensor.Calibration1 = 6L;
        sensor.Calibration2 = 10L;
        sensor.Calibration3 = 0L;
        sensor.Calibration4 = 2L;
        break;
    }
  }

  public static string HystForUI(Sensor sensor)
  {
    return sensor.Hysteresis == (long) uint.MaxValue ? "" : sensor.Hysteresis.ToString();
  }

  public static string MinThreshForUI(Sensor sensor)
  {
    return sensor.MinimumThreshold == (long) uint.MaxValue ? "" : sensor.MinimumThreshold.ToString();
  }

  public static string MaxThreshForUI(Sensor sensor)
  {
    return sensor.MaximumThreshold == (long) uint.MaxValue ? "" : sensor.MaximumThreshold.ToString();
  }

  public static void SensorScale(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection returnData)
  {
    if (collection["TempScale"] == "on")
    {
      returnData["TempScale"] = "F";
      PeopleCounter.MakeFahrenheit(sensor.SensorID);
    }
    else
    {
      returnData["TempScale"] = "C";
      PeopleCounter.MakeCelsius(sensor.SensorID);
    }
  }

  public static void CalibrateSensor(Sensor sensor, NameValueCollection collection)
  {
    sensor.ProfileConfigDirty = false;
    sensor.ProfileConfig2Dirty = false;
    sensor.PendingActionControlCommand = true;
    sensor.Save();
  }

  public new static List<byte[]> ActionControlCommand(Sensor sensor, string version)
  {
    return new List<byte[]>()
    {
      PeopleCounterBase.CalibrateFrame(sensor.SensorID),
      sensor.ReadProfileConfig(29)
    };
  }

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(PeopleCounter left, PeopleCounter right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(PeopleCounter left, PeopleCounter right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(PeopleCounter left, PeopleCounter right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(PeopleCounter left, PeopleCounter right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(PeopleCounter left, PeopleCounter right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(PeopleCounter left, PeopleCounter right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is PeopleCounter && this.Equals((MonnitApplicationBase) (obj as PeopleCounter));
  }

  public override bool Equals(MonnitApplicationBase right)
  {
    return right is PeopleCounter && this.OccupancyCount == (right as PeopleCounter).OccupancyCount;
  }

  public override bool NotEqual(MonnitApplicationBase right)
  {
    return right is PeopleCounter && this.OccupancyCount != (right as PeopleCounter).OccupancyCount;
  }

  public override bool LessThan(MonnitApplicationBase right)
  {
    return right is PeopleCounter && this.OccupancyCount < (right as PeopleCounter).OccupancyCount;
  }

  public override bool LessThanEqual(MonnitApplicationBase right)
  {
    return right is PeopleCounter && this.OccupancyCount <= (right as PeopleCounter).OccupancyCount;
  }

  public override bool GreaterThan(MonnitApplicationBase right)
  {
    return right is PeopleCounter && this.OccupancyCount > (right as PeopleCounter).OccupancyCount;
  }

  public override bool GreaterThanEqual(MonnitApplicationBase right)
  {
    return right is PeopleCounter && this.OccupancyCount >= (right as PeopleCounter).OccupancyCount;
  }

  public static Dictionary<DateTime, PeopleCounter> PeopleCounter_AggData(
    string SensorIDs,
    DateTime FromDate,
    DateTime ToDate)
  {
    IDbCommand dbCommand = Database.GetDBCommand(nameof (PeopleCounter_AggData));
    dbCommand.CommandType = CommandType.StoredProcedure;
    dbCommand.Parameters.Add((object) Database.GetParameter("@SensorIDs", (object) SensorIDs));
    dbCommand.Parameters.Add((object) Database.GetParameter("@FromDate", (object) FromDate));
    dbCommand.Parameters.Add((object) Database.GetParameter("@ToDate", (object) ToDate));
    DataTable table = Database.ExecuteQuery(dbCommand).Tables[0];
    Dictionary<DateTime, PeopleCounter> dictionary = new Dictionary<DateTime, PeopleCounter>();
    string[] strArray = SensorIDs.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    foreach (DataRow row in (InternalDataCollectionBase) table.Rows)
    {
      DateTime dateTime = row["MessageDate"].ToDateTime();
      PeopleCounter peopleCounter = (PeopleCounter) MonnitApplicationBase.LoadMonnitApplication("", row["Data"].ToString(), 77L, strArray[0].ToLong());
      dictionary.Add(dateTime, peopleCounter);
    }
    return dictionary;
  }
}
