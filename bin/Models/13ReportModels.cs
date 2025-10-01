// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.DatabaseDetails
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using details;
using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class DatabaseDetails : BaseDBObject
{
  private string _AccountNumber = string.Empty;
  private long _ExternalDataSubscriptionID = long.MinValue;
  private string _Status = string.Empty;
  private int _Counts = int.MinValue;
  private long _GatewayID = long.MinValue;
  private double _ReportInterval = 0.0;
  private long _NotificationID = long.MinValue;
  private string _SMTP = string.Empty;
  private string _Message = string.Empty;
  private string _StackTrace = string.Empty;
  private DateTime _ExceptionDate = DateTime.Now;

  [DBProp("AccountNumber", MaxLength = 255 /*0xFF*/)]
  public string AccountNumber
  {
    get => this._AccountNumber;
    set
    {
      if (value == null)
        this._AccountNumber = string.Empty;
      else
        this._AccountNumber = value;
    }
  }

  [DBProp("ExternalDataSubscriptionID")]
  public long ExternalDataSubscriptionID
  {
    get => this._ExternalDataSubscriptionID;
    set => this._ExternalDataSubscriptionID = value;
  }

  [DBProp("Status", MaxLength = 255 /*0xFF*/)]
  public string Status
  {
    get => this._Status;
    set
    {
      if (value == null)
        this._Status = string.Empty;
      else
        this._Status = value;
    }
  }

  [DBProp("Counts")]
  public int Counts
  {
    get => this._Counts;
    set => this._Counts = value;
  }

  [DBProp("GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("ReportInterval")]
  public double ReportInterval
  {
    get => this._ReportInterval;
    set => this._ReportInterval = value;
  }

  [DBProp("NotificationID", MaxLength = 255 /*0xFF*/)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("SMTP", MaxLength = 255 /*0xFF*/)]
  public string SMTP
  {
    get => this._SMTP;
    set
    {
      if (value == null)
        this._SMTP = string.Empty;
      else
        this._SMTP = value;
    }
  }

  [DBProp("Message", MaxLength = 255 /*0xFF*/)]
  public string Message
  {
    get => this._Message;
    set
    {
      if (value == null)
        this._Message = string.Empty;
      else
        this._Message = value;
    }
  }

  [DBProp("StackTrace", MaxLength = 255 /*0xFF*/)]
  public string StackTrace
  {
    get => this._StackTrace;
    set
    {
      if (value == null)
        this._StackTrace = string.Empty;
      else
        this._StackTrace = value;
    }
  }

  [DBProp("ExceptionDate", MaxLength = 255 /*0xFF*/)]
  public DateTime ExceptionDate
  {
    get => this._ExceptionDate;
    set => this._ExceptionDate = value;
  }

  public static List<DatabaseDetails> CriticalDetails(
    string tableName,
    string status,
    DateTime fromDate,
    DateTime toDate)
  {
    return new CriticalDetailsModel.CriticalDetails(tableName, status, fromDate, toDate).Result;
  }
}
