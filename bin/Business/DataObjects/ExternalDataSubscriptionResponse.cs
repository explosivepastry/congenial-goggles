// Decompiled with JetBrains decompiler
// Type: Monnit.ExternalDataSubscriptionResponse
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Net;

#nullable disable
namespace Monnit;

[DBClass("ExternalDataSubscriptionResponse")]
public class ExternalDataSubscriptionResponse : BaseDBObject
{
  private long _ExternalDataSubscriptionResponseID = long.MinValue;
  private long _ExternalDataSubscriptionAttemptID = long.MinValue;
  private string _status = string.Empty;
  private HttpStatusCode _StatusCode = HttpStatusCode.RequestTimeout;
  private DateTime _DateTime = DateTime.UtcNow;
  private string _Headers = string.Empty;
  private string _response = string.Empty;
  private bool _ExceptionOccurred = false;
  private string _ExceptionData = string.Empty;
  private int _ResponseTime = int.MinValue;
  private string _ServerIP = ExternalDataSubscriptionResponse.getIP();

  [DBProp("ExternalDataSubscriptionResponseID", IsPrimaryKey = true)]
  public long ExternalDataSubscriptionResponseID
  {
    get => this._ExternalDataSubscriptionResponseID;
    set => this._ExternalDataSubscriptionResponseID = value;
  }

  [DBProp("ExternalDataSubscriptionAttemptID")]
  [DBForeignKey("ExternalDataSubscriptionAttempt", "ExternalDataSubscriptionAttemptID")]
  public long ExternalDataSubscriptionAttemptID
  {
    get => this._ExternalDataSubscriptionAttemptID;
    set => this._ExternalDataSubscriptionAttemptID = value;
  }

  [DBProp("Status", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Status
  {
    get => this._status;
    set => this._status = value;
  }

  [DBProp("StatusCode")]
  public HttpStatusCode StatusCode
  {
    get => this._StatusCode;
    set => this._StatusCode = value;
  }

  public string RawCode => this.StatusCode.ToInt().ToString();

  [DBProp("DateTime")]
  public DateTime DateTime
  {
    get => this._DateTime;
    set => this._DateTime = value;
  }

  [DBProp("Headers", MaxLength = 2000, AllowNull = true)]
  public string Headers
  {
    get => this._Headers;
    set => this._Headers = value;
  }

  [DBProp("Response", MaxLength = 8000, AllowNull = true)]
  public string Response
  {
    get => this._response;
    set => this._response = value;
  }

  [DBProp("ExceptionOccurred")]
  public bool ExceptionOccurred
  {
    get => this._ExceptionOccurred;
    set => this._ExceptionOccurred = value;
  }

  [DBProp("ExceptionData", DefaultValue = null, MaxLength = 2000)]
  public string ExceptionData
  {
    get => this._ExceptionData;
    set => this._ExceptionData = value;
  }

  [DBProp("ResponseTime")]
  public int ResponseTime
  {
    get => this._ResponseTime;
    set => this._ResponseTime = value;
  }

  [DBProp("ServerIP")]
  public string ServerIP
  {
    get => this._ServerIP;
    set => this._ServerIP = value;
  }

  public DateTime ResponseDateLocalTime(long TimeZoneID)
  {
    return TimeZone.GetLocalTimeById(this.DateTime, TimeZoneID);
  }

  public static ExternalDataSubscriptionResponse Load(long id)
  {
    return BaseDBObject.Load<ExternalDataSubscriptionResponse>(id);
  }

  public static List<ExternalDataSubscriptionResponse> LoadByAttemptID(long id)
  {
    return BaseDBObject.LoadByForeignKey<ExternalDataSubscriptionResponse>("ExternalDataSubscriptionAttemptID", (object) id);
  }

  private static string getIP()
  {
    string ip = "?";
    foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
    {
      if (address.AddressFamily.ToString() == "InterNetwork")
        ip = address.ToString();
    }
    return ip;
  }
}
