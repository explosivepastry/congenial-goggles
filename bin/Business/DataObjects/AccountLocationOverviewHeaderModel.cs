// Decompiled with JetBrains decompiler
// Type: Monnit.AccountLocationOverviewHeaderModel
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit;

public class AccountLocationOverviewHeaderModel : BaseDBObject
{
  private int _offlineCount;
  private int _warningCount;
  private int _alertCount;
  private int _locationCount;

  [DBProp("Offline")]
  public int OfflineCount
  {
    get => this._offlineCount < 0 ? 0 : this._offlineCount;
    private set => this._offlineCount = value;
  }

  [DBProp("Warning")]
  public int WarningCount
  {
    get => this._warningCount < 0 ? 0 : this._warningCount;
    private set => this._warningCount = value;
  }

  [DBProp("Alert")]
  public int AlertCount
  {
    get => this._alertCount < 0 ? 0 : this._alertCount;
    private set => this._alertCount = value;
  }

  [DBProp("Locations")]
  public int LocationCount
  {
    get => this._locationCount < 0 ? 0 : this._locationCount;
    private set => this._locationCount = value;
  }
}
