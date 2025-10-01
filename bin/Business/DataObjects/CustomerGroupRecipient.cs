// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerGroupRecipient
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerGroupRecipient")]
public class CustomerGroupRecipient : BaseDBObject
{
  private long _CustomerGroupRecipientID = long.MinValue;
  private long _NotificationID = long.MinValue;
  private DateTime _LastSentTime = DateTime.MinValue;
  private long _CustomerGroupLinkID = long.MinValue;

  [DBProp("CustomerGroupRecipientID", IsPrimaryKey = true)]
  public long CustomerGroupRecipientID
  {
    get => this._CustomerGroupRecipientID;
    set => this._CustomerGroupRecipientID = value;
  }

  [DBProp("NotificationID")]
  [DBForeignKey("Notification", "NotificationID")]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("LastSentTime")]
  public DateTime LastSentTime
  {
    get => this._LastSentTime;
    set => this._LastSentTime = value;
  }

  [DBProp("CustomerGroupLinkID")]
  [DBForeignKey("CustomerGroupLink", "CustomerGroupLinkID")]
  public long CustomerGroupLinkID
  {
    get => this._CustomerGroupLinkID;
    set => this._CustomerGroupLinkID = value;
  }

  public static CustomerGroupRecipient Load(long id)
  {
    return BaseDBObject.Load<CustomerGroupRecipient>(id);
  }

  public static List<CustomerGroupRecipient> LoadByCustomerGroupLinkID(long customerGroupLinkID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerGroupRecipient>("CustomerGroupLinkID", (object) customerGroupLinkID);
  }
}
