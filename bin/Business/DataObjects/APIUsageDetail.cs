// Decompiled with JetBrains decompiler
// Type: Monnit.APIUsageDetail
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("APIUsageDetail")]
public class APIUsageDetail : BaseDBObject
{
  private Guid _APIUsageDetailGUID = Guid.Empty;
  private int _Day = int.MinValue;
  private DateTime _Date = DateTime.MinValue;
  private string _Method = string.Empty;
  private string _QueryString = string.Empty;
  private long _CustomerID = long.MinValue;
  private string _AuthenticationResult = string.Empty;

  [DBProp("APIUsageDetailGUID", IsGuidPrimaryKey = true)]
  public Guid APIUsageDetailGUID
  {
    get => this._APIUsageDetailGUID;
    set => this._APIUsageDetailGUID = value;
  }

  [DBProp("Day", AllowNull = false)]
  public int Day
  {
    get => this._Day;
    set => this._Day = value;
  }

  [DBProp("Date")]
  public DateTime Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [DBProp("Method", MaxLength = 500, AllowNull = true)]
  public string Method
  {
    get => this._Method;
    set => this._Method = value;
  }

  [DBProp("QueryString", MaxLength = 4000, AllowNull = true)]
  public string QueryString
  {
    get => this._QueryString;
    set => this._QueryString = value;
  }

  [DBProp("CustomerID", AllowNull = true)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("AuthenticationResult", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string AuthenticationResult
  {
    get => this._AuthenticationResult;
    set => this._AuthenticationResult = value;
  }

  public static APIUsageDetail Load(Guid ID) => BaseDBObject.Load<APIUsageDetail>(ID);

  public static void Insert(
    string method,
    string queryString,
    long customerID,
    string authenticationResult)
  {
    Monnit.Data.APIUsageDetail.Insert insert = new Monnit.Data.APIUsageDetail.Insert(method, queryString, customerID, authenticationResult);
  }

  public static List<APIUsageDetail> LoadAllUsagesByDateAndCustomerID(
    DateTime from,
    DateTime to,
    long custID,
    int limit)
  {
    return (List<APIUsageDetail>) null;
  }

  public static List<APIUsageDetail> LoadAllUsagesByDateAndAccountID(
    DateTime from,
    DateTime to,
    long acctID,
    int limit)
  {
    return (List<APIUsageDetail>) null;
  }
}
