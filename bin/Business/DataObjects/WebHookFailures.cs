// Decompiled with JetBrains decompiler
// Type: Monnit.WebHookFailures
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class WebHookFailures : BaseDBObject
{
  private long _AttemptID = long.MinValue;
  private DateTime _CreateDate = DateTime.MinValue;
  private string _Url = string.Empty;
  private DateTime _ProcessDate = DateTime.MinValue;
  private bool _Retry = false;
  private int _AttemptCount = int.MinValue;
  private eExternalDataSubscriptionStatus _Status = eExternalDataSubscriptionStatus.New;
  private long _AccountID = long.MinValue;

  [DBProp("AttemptID")]
  public long AttemptID
  {
    get => this._AttemptID;
    set => this._AttemptID = value;
  }

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("Url", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Url
  {
    get => this._Url;
    set => this._Url = value;
  }

  [DBProp("ProcessDate")]
  public DateTime ProcessDate
  {
    get => this._ProcessDate;
    set => this._ProcessDate = value;
  }

  [DBProp("Retry")]
  public bool Retry
  {
    get => this._Retry;
    set => this._Retry = value;
  }

  [DBProp("AttemptCount")]
  public int AttemptCount
  {
    get => this._AttemptCount;
    set => this._AttemptCount = value;
  }

  [DBProp("Status")]
  public eExternalDataSubscriptionStatus Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [DBProp("AccountID", AllowNull = true)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  public static List<WebHookFailures> LoadSubAccountErrorsBySubscriptionAndDate(
    long accountID,
    DateTime fromDate,
    DateTime toDate,
    int limit)
  {
    return new Monnit.Data.WebHookFailures.LoadSubAccountErrorsBySubscriptionAndDate(accountID, fromDate, toDate, limit).Result;
  }

  public static List<WebHookFailures> WebhookFilter(
    long accountID,
    DateTime fromDate,
    DateTime toDate)
  {
    return new Monnit.Data.WebHookFailures.WebhookFilter(accountID, fromDate, toDate).Result;
  }
}
