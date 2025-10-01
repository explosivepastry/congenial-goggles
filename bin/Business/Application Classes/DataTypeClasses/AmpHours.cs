// Decompiled with JetBrains decompiler
// Type: Monnit.Application_Classes.DataTypeClasses.AmpHours
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Application_Classes.DataTypeClasses;

public class AmpHours : DataTypeBase
{
  public double value;

  public override eDatumType datatype => eDatumType.AmpHours;

  public override object compvalue => (object) this.value;

  public AmpHours(double a) => this.value = a;

  public static void SetHighValue(long sensorID, double highValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
      {
        sensorAttribute.Value = highValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "HighValue",
        Value = highValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static double GetHighValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "HighValue")
        return Convert.ToDouble(sensorAttribute.Value);
    }
    return 20.0;
  }

  public static double GetLowValue(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
        return Convert.ToDouble(sensorAttribute.Value);
    }
    return 0.0;
  }

  public static void SetLowValue(long sensorID, double lowValue)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "LowValue")
      {
        sensorAttribute.Value = lowValue.ToString();
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = "LowValue",
        Value = lowValue.ToString(),
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static string GetLabel(long sensorID)
  {
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == "Label")
        return sensorAttribute.Value;
    }
    return "Ah";
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

  public static void SetShowAdvCal(long sensorID, string showAdvCal)
  {
    bool flag = false;
    foreach (SensorAttribute sensorAttribute in SensorAttribute.LoadBySensorID(sensorID))
    {
      if (sensorAttribute.Name == nameof (showAdvCal))
      {
        sensorAttribute.Value = showAdvCal;
        sensorAttribute.Save();
        flag = true;
      }
    }
    if (!flag)
      new SensorAttribute()
      {
        Name = nameof (showAdvCal),
        Value = showAdvCal,
        SensorID = sensorID
      }.Save();
    SensorAttribute.ResetAttributeList(sensorID);
  }

  public static NotificationScaleInfoModel GetScalingInfo(long i)
  {
    return i <= 0L ? AmpHours.getScales()[(int) (-1L * i)] : MonnitApplicationBase.GetScalingInfo(Sensor.Load(i));
  }

  public static List<UnitConversion> GetScales(Sensor sens)
  {
    return new List<UnitConversion>()
    {
      new UnitConversion(1.0, 0.0, nameof (AmpHours), "", "")
    };
  }

  public static string ShouldShowEqualSelectOption() => "true";

  public static List<NotificationScaleInfoModel> getScales()
  {
    return new List<NotificationScaleInfoModel>()
    {
      new NotificationScaleInfoModel((Sensor) null, 0.0, 1.0, 0.0, 1.0, 0.0, nameof (AmpHours), nameof (AmpHours))
    };
  }
}
