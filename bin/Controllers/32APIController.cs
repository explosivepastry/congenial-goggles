// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIAdvancedNotificationParameter
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIAdvancedNotificationParameter
{
  public APIAdvancedNotificationParameter()
  {
  }

  public APIAdvancedNotificationParameter(AdvancedNotificationParameter parameter)
  {
    this.AdvancedNotificationParameterID = parameter.AdvancedNotificationParameterID;
    this.ParameterDescription = parameter.Description;
    this.ParameterType = parameter.ParameterType == "Monnit.Sensor" ? "SensorID" : parameter.ParameterType;
    this.Required = parameter.Required;
  }

  public long AdvancedNotificationParameterID { get; set; }

  public string ParameterType { get; set; }

  public bool Required { get; set; }

  public string ParameterDescription { get; set; }
}
