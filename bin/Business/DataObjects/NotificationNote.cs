// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationNote
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("NotificationNote")]
public class NotificationNote : BaseDBObject
{
  private long _NotificationNoteID = long.MinValue;
  private long _NotificationTriggeredID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private DateTime _CreateDate = DateTime.MinValue;
  private string _Note = string.Empty;

  [DBProp("NotificationNoteID", IsPrimaryKey = true)]
  public long NotificationNoteID
  {
    get => this._NotificationNoteID;
    set => this._NotificationNoteID = value;
  }

  [DBProp("NotificationTriggeredID", AllowNull = false)]
  [DBForeignKey("NotificationTriggered", "NotificationTriggeredID")]
  public long NotificationTriggeredID
  {
    get => this._NotificationTriggeredID;
    set => this._NotificationTriggeredID = value;
  }

  [DBProp("CustomerID", AllowNull = false)]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("CreateDate", AllowNull = false)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("Note", MaxLength = 2000, AllowNull = false, International = true)]
  public string Note
  {
    get => this._Note;
    set => this._Note = value;
  }

  public static NotificationNote Load(long id) => BaseDBObject.Load<NotificationNote>(id);

  public static List<NotificationNote> LoadByNotificationTriggeredID(long id)
  {
    return BaseDBObject.LoadByForeignKey<NotificationNote>("NotificationTriggeredID", (object) id);
  }
}
