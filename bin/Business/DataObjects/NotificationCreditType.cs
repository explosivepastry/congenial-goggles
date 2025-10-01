// Decompiled with JetBrains decompiler
// Type: Monnit.NotificationCreditType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("NotificationCreditType")]
public class NotificationCreditType : BaseDBObject
{
  private long _NotificationCreditTypeID = long.MinValue;
  private string _Name = string.Empty;
  private string _Description = string.Empty;
  private int _Rank = int.MinValue;
  private eCreditClassification _CreditClassification = eCreditClassification.Notification;

  [DBProp("NotificationCreditTypeID", IsPrimaryKey = true)]
  public long NotificationCreditTypeID
  {
    get => this._NotificationCreditTypeID;
    set => this._NotificationCreditTypeID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Description", MaxLength = 2000, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [DBProp("Rank")]
  public int Rank
  {
    get => this._Rank;
    set => this._Rank = value;
  }

  [DBProp("CreditClassification", AllowNull = false)]
  public eCreditClassification CreditClassification
  {
    get => this._CreditClassification;
    set => this._CreditClassification = value;
  }

  public static NotificationCreditType Load(long id)
  {
    NotificationCreditType notificationCreditType = NotificationCreditType.LoadAll().FirstOrDefault<NotificationCreditType>((Func<NotificationCreditType, bool>) (t => t.NotificationCreditTypeID == id));
    if (notificationCreditType == null)
    {
      notificationCreditType = BaseDBObject.Load<NotificationCreditType>(id);
      if (notificationCreditType != null)
        NotificationCreditType.LoadAll().Add(notificationCreditType);
    }
    return notificationCreditType;
  }

  public static List<NotificationCreditType> LoadByClassification(
    eCreditClassification creditClassification)
  {
    return NotificationCreditType.LoadAll().Where<NotificationCreditType>((Func<NotificationCreditType, bool>) (c => c.CreditClassification == creditClassification)).ToList<NotificationCreditType>();
  }

  public static List<NotificationCreditType> LoadAll()
  {
    string key = "NotificationCreditTypeList";
    List<NotificationCreditType> notificationCreditTypeList = TimedCache.RetrieveObject<List<NotificationCreditType>>(key);
    if (notificationCreditTypeList == null)
    {
      notificationCreditTypeList = BaseDBObject.LoadAll<NotificationCreditType>();
      if (notificationCreditTypeList != null)
        TimedCache.AddObjectToCach(key, (object) notificationCreditTypeList, new TimeSpan(2, 0, 0));
    }
    return notificationCreditTypeList;
  }
}
