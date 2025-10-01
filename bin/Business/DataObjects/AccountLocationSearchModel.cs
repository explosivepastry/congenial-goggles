// Decompiled with JetBrains decompiler
// Type: Monnit.AccountLocationSearchModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class AccountLocationSearchModel : BaseDBObject
{
  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("AccountNumber")]
  public string AccountNumber { get; set; }

  [DBProp("SensorCount")]
  public int SensorCount { get; set; }

  [DBProp("OfflineCount")]
  public int OfflineCount { get; set; }

  [DBProp("SensorAwareCounts")]
  public int SensorAwareCounts { get; set; }

  [DBProp("SensorLowBattery")]
  public int SensorLowBattery { get; set; }

  [DBProp("AlertCounts")]
  public int AlertCounts { get; set; }

  [DBProp("Hardware")]
  public int Hardware { get; set; }

  [DBProp("GatewayCount")]
  public int GatewayCount { get; set; }

  [DBProp("GatewayOffline")]
  public int GatewayOffline { get; set; }

  [DBProp("SubAccounts")]
  public int SubAccounts { get; set; }

  public int DevicesAlerting => this.AlertCounts;

  public int DevicesWarning => this.SensorLowBattery + this.Hardware;

  public int DevicesOffline => this.OfflineCount + this.GatewayOffline;

  public int DevicesOK
  {
    get => this.DeviceCount - this.DevicesAlerting - this.DevicesWarning - this.DevicesOffline;
  }

  public int DeviceCount => this.SensorCount + this.GatewayCount;

  public static Tuple<List<AccountLocationSearchModel>, AccountLocationOverviewHeaderModel> LocationSearch(
    long accountID,
    string searchTerm)
  {
    return new Monnit.Data.Account.LocationSearch(accountID, searchTerm).Result;
  }

  public static Tuple<List<AccountLocationSearchModel>, AccountLocationOverviewHeaderModel> LocationSearchCanteen(
    long accountID,
    string searchTerm)
  {
    return new Monnit.Data.Account.LocationSearchCanteen(accountID, searchTerm).Result;
  }
}
