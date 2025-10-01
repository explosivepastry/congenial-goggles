// Decompiled with JetBrains decompiler
// Type: Monnit.MaintenanceWindow
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

#nullable disable
namespace Monnit;

[DBClass("MaintenanceWindow")]
public class MaintenanceWindow : BaseDBObject
{
  private long _MaintenanceWindowID = long.MinValue;
  private string _Description = string.Empty;
  private string _EmailBody = string.Empty;
  private string _SMSDescription = string.Empty;
  private DateTime _DisplayDate = DateTime.MinValue;
  private DateTime _HideDate = DateTime.MinValue;
  private long _SeverityLevel = 3;
  private DateTime _StartDate = DateTime.MinValue;

  [DBProp("MaintenanceWindowID", IsPrimaryKey = true)]
  public long MaintenanceWindowID
  {
    get => this._MaintenanceWindowID;
    set => this._MaintenanceWindowID = value;
  }

  [AllowHtml]
  [DBProp("Description", MaxLength = 8000, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [AllowHtml]
  [DBProp("EmailBody", MaxLength = 8000, AllowNull = true)]
  public string EmailBody
  {
    get => this._EmailBody;
    set => this._EmailBody = value;
  }

  [DBProp("SMSDescription", MaxLength = 160 /*0xA0*/, AllowNull = true)]
  public string SMSDescription
  {
    get => this._SMSDescription;
    set => this._SMSDescription = value;
  }

  [DBProp("DisplayDate")]
  public DateTime DisplayDate
  {
    get => this._DisplayDate;
    set => this._DisplayDate = value;
  }

  [DBProp("HideDate")]
  public DateTime HideDate
  {
    get => this._HideDate;
    set => this._HideDate = value;
  }

  [DBProp("SeverityLevel", AllowNull = true)]
  public long SeverityLevel
  {
    get => this._SeverityLevel;
    set => this._SeverityLevel = value;
  }

  [DBProp("StartDate")]
  public DateTime StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  public List<AccountThemeMaintenanceLink> AcctThemeLink
  {
    get => AccountThemeMaintenanceLink.LoadByMaintenanceID(this.MaintenanceWindowID);
  }

  public static MaintenanceWindow Load(long id) => BaseDBObject.Load<MaintenanceWindow>(id);

  public static MaintenanceWindow LoadActive()
  {
    MaintenanceWindow maintenanceWindow = TimedCache.RetrieveObject<MaintenanceWindow>("ActiveMaintenanceWindow");
    if (maintenanceWindow == null)
    {
      maintenanceWindow = Monnit.Data.MaintenanceWindow.LoadActive.Exec();
      if (maintenanceWindow == null)
        maintenanceWindow = new MaintenanceWindow()
        {
          DisplayDate = DateTime.MaxValue
        };
      TimedCache.AddObjectToCach("ActiveMaintenanceWindow", (object) maintenanceWindow, new TimeSpan(0, 15, 0));
    }
    return maintenanceWindow.DisplayDate < DateTime.MaxValue ? maintenanceWindow : (MaintenanceWindow) null;
  }

  public static List<MaintenanceWindow> LoadFuture() => Monnit.Data.MaintenanceWindow.LoadFuture.Exec();
}
