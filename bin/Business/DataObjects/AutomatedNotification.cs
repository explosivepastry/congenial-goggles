// Decompiled with JetBrains decompiler
// Type: Monnit.AutomatedNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("AutomatedNotification")]
public class AutomatedNotification : BaseDBObject
{
  private long _AutomatedNotificationID = long.MinValue;
  private DateTime _LastProcessDate = DateTime.MinValue;
  private int _ProcessFrequency = 720;
  private DateTime _TriggerDate = DateTime.MinValue;
  private bool _AutoReset = true;
  private long _NotificationID = long.MinValue;
  private string _Description = string.Empty;
  private long _ExternalID = long.MinValue;

  [DBProp("AutomatedNotificationID", IsPrimaryKey = true)]
  public long AutomatedNotificationID
  {
    get => this._AutomatedNotificationID;
    set => this._AutomatedNotificationID = value;
  }

  [DBProp("LastProcessDate", AllowNull = true)]
  public DateTime LastProcessDate
  {
    get => this._LastProcessDate;
    set => this._LastProcessDate = value;
  }

  [DBProp("ProcessFrequency")]
  public int ProcessFrequency
  {
    get => this._ProcessFrequency;
    set => this._ProcessFrequency = value;
  }

  [DBProp("TriggerDate", AllowNull = true)]
  public DateTime TriggerDate
  {
    get => this._TriggerDate;
    set => this._TriggerDate = value;
  }

  [DBProp("AutoReset")]
  public bool AutoReset
  {
    get => this._AutoReset;
    set => this._AutoReset = value;
  }

  [DBProp("NotificationID")]
  [DBForeignKey("Notification", "NotificationID")]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("Description", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [DBProp("ExternalID", AllowNull = true)]
  public long ExternalID
  {
    get => this._ExternalID;
    set => this._ExternalID = value;
  }

  public static AutomatedNotification LoadByExternalIDAndNotificationID(
    long externalID,
    long notificationID)
  {
    return BaseDBObject.LoadByForeignKeys<AutomatedNotification>(new string[2]
    {
      "ExternalID",
      "NotificationID"
    }, new object[2]
    {
      (object) externalID,
      (object) notificationID
    }).FirstOrDefault<AutomatedNotification>();
  }

  public static AutomatedNotification Load(long id) => BaseDBObject.Load<AutomatedNotification>(id);
}
