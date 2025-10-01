// Decompiled with JetBrains decompiler
// Type: Monnit.AccountLocationOverviewCustomModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class AccountLocationOverviewCustomModel : BaseDBObject
{
  [DBProp("RowID")]
  public int RowID { get; set; }

  [DBProp("AccountID")]
  public long AccountID { get; set; }

  [DBProp("AccountNumber")]
  public string AccountNumber { get; set; }

  [DBProp("RetailAccountID")]
  public long RetailAccountID { get; set; }

  [DBProp("SensorCount")]
  public int SensorCount { get; set; }

  [DBProp("OnlineCount")]
  public int OnlineCount { get; set; }

  [DBProp("OfflineCount")]
  public int OfflineCount { get; set; }

  [DBProp("SensorLowSig")]
  public int SensorLowSig { get; set; }

  [DBProp("SensorLowBatt")]
  public int SensorLowBatt { get; set; }

  [DBProp("AlertCounts")]
  public int AlertCounts { get; set; }

  [DBProp("AwareCounts")]
  public int AwareCounts { get; set; }

  [DBProp("GatewayCount")]
  public int GatewayCount { get; set; }

  [DBProp("GatewayOnline")]
  public int GatewayOnline { get; set; }

  [DBProp("GatewayOffline")]
  public int GatewayOffline { get; set; }

  [DBProp("SubAccountLevel")]
  public int SubAccountLevel { get; set; }

  [DBProp("SubAccountCount")]
  public int SubAccountCount { get; set; }

  [DBProp("AccountLevelIssues")]
  public int AccountLevelIssues { get; set; }

  [DBProp("RetailLevelIssues")]
  public int RetailLevelIssues { get; set; }

  [DBProp("NotNormal")]
  public bool NotNormal { get; set; }

  [DBProp("PremiereMonths")]
  public bool PremiereMonths { get; set; }

  public static List<AccountLocationOverviewCustomModel> LocationOverview(long accountID)
  {
    return new Monnit.Data.Account.LocationOverviewCustom(accountID).Result;
  }
}
