// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APICalibrationFacility
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APICalibrationFacility
{
  public APICalibrationFacility()
  {
  }

  public APICalibrationFacility(CalibrationFacility facility)
  {
    this.Name = facility.Name;
    this.CalibrationFacilityID = facility.CalibrationFacilityID;
  }

  public long CalibrationFacilityID { get; set; }

  public string Name { get; set; }
}
