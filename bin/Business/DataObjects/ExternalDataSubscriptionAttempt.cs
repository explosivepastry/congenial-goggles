// Decompiled with JetBrains decompiler
// Type: Monnit.ExternalDataSubscriptionAttempt
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ExternalDataSubscriptionAttempt")]
public class ExternalDataSubscriptionAttempt : BaseDBObject
{
  private long _ExternalDataSubscriptionAttemptID = long.MinValue;
  private long _ExternalDataSubscriptionID = long.MinValue;
  private Guid _DataMessageGUID = Guid.Empty;
  private Guid _GatewayMessageGUID = Guid.Empty;
  private string _url = string.Empty;
  private string _Verb = string.Empty;
  private string _body = string.Empty;
  private long _SensorID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private eExternalDataSubscriptionStatus _Status = eExternalDataSubscriptionStatus.New;
  private int _AttemptCount = 0;
  private DateTime _CreateDate = DateTime.UtcNow;
  private bool _DoRetry = true;
  private DateTime _ProcessDate = DateTime.UtcNow;
  private long _NotificationRecordedID = long.MinValue;
  private List<ExternalDataSubscriptionProperty> _Properties = (List<ExternalDataSubscriptionProperty>) null;
  private List<ExternalDataSubscriptionResponse> _responses;

  [DBProp("ExternalDataSubscriptionAttemptID", IsPrimaryKey = true)]
  public long ExternalDataSubscriptionAttemptID
  {
    get => this._ExternalDataSubscriptionAttemptID;
    set => this._ExternalDataSubscriptionAttemptID = value;
  }

  [DBProp("ExternalDataSubscriptionID")]
  [DBForeignKey("ExternalDataSubscription", "ExternalDataSubscriptionID")]
  public long ExternalDataSubscriptionID
  {
    get => this._ExternalDataSubscriptionID;
    set => this._ExternalDataSubscriptionID = value;
  }

  [DBProp("DataMessageGUID")]
  public Guid DataMessageGUID
  {
    get => this._DataMessageGUID;
    set => this._DataMessageGUID = value;
  }

  [DBProp("GatewayMessageGUID")]
  public Guid GatewayMessageGUID
  {
    get => this._GatewayMessageGUID;
    set => this._GatewayMessageGUID = value;
  }

  [DBProp("Url", MaxLength = 500, AllowNull = true)]
  public string Url
  {
    get => this._url;
    set => this._url = value;
  }

  [DBProp("Verb", MaxLength = 2000, AllowNull = true)]
  public string Verb
  {
    get => this._Verb;
    set => this._Verb = value;
  }

  [DBProp("body", AllowNull = true, MaxLength = 2147483647 /*0x7FFFFFFF*/)]
  public string body
  {
    get => this._body;
    set => this._body = value;
  }

  [DBProp("SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("Status")]
  public eExternalDataSubscriptionStatus Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [DBProp("AttemptCount")]
  public int AttemptCount
  {
    get => this._AttemptCount;
    set => this._AttemptCount = value;
  }

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("DoRetry", AllowNull = false, DefaultValue = true)]
  public bool DoRetry
  {
    get => this._DoRetry;
    set => this._DoRetry = value;
  }

  [DBProp("ProcessDate")]
  public DateTime ProcessDate
  {
    get => this._ProcessDate;
    set => this._ProcessDate = value;
  }

  [DBProp("NotificationRecordedID")]
  [DBForeignKey("NotificationRecorded", "NotificationRecordedID")]
  public long NotificationRecordedID
  {
    get => this._NotificationRecordedID;
    set => this._NotificationRecordedID = value;
  }

  public List<ExternalDataSubscriptionProperty> Properties
  {
    get
    {
      if (this._Properties == null)
        this._Properties = ExternalDataSubscriptionProperty.LoadByExternalDataSubscriptionID(this.ExternalDataSubscriptionID);
      return this._Properties;
    }
    set => this._Properties = value;
  }

  public static ExternalDataSubscriptionAttempt Load(long id)
  {
    return BaseDBObject.Load<ExternalDataSubscriptionAttempt>(id);
  }

  public List<ExternalDataSubscriptionResponse> Responses
  {
    get
    {
      if (this._responses == null)
        this._responses = ExternalDataSubscriptionResponse.LoadByAttemptID(this.ExternalDataSubscriptionAttemptID);
      return this._responses;
    }
  }

  public static List<ExternalDataSubscriptionAttempt> LoadReady(int limit)
  {
    return new Monnit.Data.ExternalDataSubscriptionAttempt.LoadReady(limit).Result;
  }

  public static List<ExternalDataSubscriptionAttempt> LoadBySubscriptionAndDate(
    long subscriptionID,
    DateTime fromDate,
    DateTime toDate,
    long paginationAttemptID,
    int limit)
  {
    return new Monnit.Data.ExternalDataSubscriptionAttempt.LoadBySubscriptionAndDate(subscriptionID, fromDate, toDate, paginationAttemptID, limit).Result;
  }

  public static List<ExternalDataSubscriptionAttempt> LoadErrorsBySubscriptionAndDate(
    long subscriptionID,
    DateTime fromDate,
    DateTime toDate,
    long paginationAttemptID,
    int limit)
  {
    return new Monnit.Data.ExternalDataSubscriptionAttempt.LoadErrorsBySubscriptionAndDate(subscriptionID, fromDate, toDate, paginationAttemptID, limit).Result;
  }
}
