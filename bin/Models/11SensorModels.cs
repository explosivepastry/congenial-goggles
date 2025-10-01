// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.MultiChartSensorDataModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;
using System.Linq;

#nullable disable
namespace iMonnit.Models;

public class MultiChartSensorDataModel
{
  public MultiChartSensorDataModel()
  {
  }

  public MultiChartSensorDataModel(
    SensorGroupSensor groupSensor,
    string messageString,
    string label)
  {
    this.GroupSensor = groupSensor;
    this.MessageString = messageString;
    if (string.IsNullOrEmpty(label))
    {
      MonnitApplicationBase.ProfileLabelForScale(groupSensor.Sensor, out label);
      SensorAttribute sensorAttribute = SensorAttribute.LoadBySensorID(groupSensor.SensorID).FirstOrDefault<SensorAttribute>((Func<SensorAttribute, bool>) (sa => sa.Name == nameof (Label)));
      if (sensorAttribute != null)
        label = sensorAttribute.Value;
    }
    this.Label = label;
  }

  public SensorGroupSensor GroupSensor { get; set; }

  public string MessageString { get; set; }

  public string Label { get; set; }
}
