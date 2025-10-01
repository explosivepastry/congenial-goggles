// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.SensorApplicationModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class SensorApplicationModel
{
  public long SensorID { get; set; }

  public long NetworkID { get; set; }

  [Required]
  public string SensorName { get; set; }

  [Required(ErrorMessage = "Application is Required")]
  public long SensorApplicationID { get; set; }

  public string OtherSensorApplicationName { get; set; }
}
