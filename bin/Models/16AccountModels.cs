// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CustomAccountSetupModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;

#nullable disable
namespace iMonnit.Models;

public class CustomAccountSetupModel
{
  [DBProp("DivisionAccountID")]
  public long DivisionAccountID { get; set; }

  [DBProp("DistrictAccountID")]
  public long DistrictAccountID { get; set; }

  [DBProp("AccountName")]
  public string AccountName { get; set; }

  [DBProp("GatewayID")]
  public string GatewayID { get; set; }

  [DBProp("GatewayCode")]
  public string GatewayCode { get; set; }

  [DBProp("SensorID1")]
  public long SensorID1 { get; set; }

  [DBProp("SensorCode1")]
  public string SensorCode1 { get; set; }

  [DBProp("SensorType1")]
  public string SensorType1 { get; set; }

  [DBProp("SensorName1")]
  public string SensorName1 { get; set; }

  [DBProp("SensorID2")]
  public long? SensorID2 { get; set; }

  [DBProp("SensorCode2")]
  public string SensorCode2 { get; set; }

  [DBProp("SensorType2")]
  public string SensorType2 { get; set; }

  [DBProp("SensorName2")]
  public string SensorName2 { get; set; }

  [DBProp("SensorID3")]
  public long? SensorID3 { get; set; }

  [DBProp("SensorCode3")]
  public string SensorCode3 { get; set; }

  [DBProp("SensorType3")]
  public string SensorType3 { get; set; }

  [DBProp("SensorName3")]
  public string SensorName3 { get; set; }

  [DBProp("SensorID4")]
  public long? SensorID4 { get; set; }

  [DBProp("SensorCode4")]
  public string SensorCode4 { get; set; }

  [DBProp("SensorType4")]
  public string SensorType4 { get; set; }

  [DBProp("SensorName4")]
  public string SensorName4 { get; set; }

  [DBProp("AccountType")]
  public string AccountType { get; set; }
}
