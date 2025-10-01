// Decompiled with JetBrains decompiler
// Type: Monnit.AuthenticationLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AuthenticationLog")]
public class AuthenticationLog : BaseDBObject
{
  private long _AuthenticationLogID = long.MinValue;
  private string _UserName = string.Empty;
  private long _CustomerID = long.MinValue;
  private DateTime _LogDate = DateTime.MinValue;
  private bool _Success = false;
  private string _Application = string.Empty;
  private string _IpAddress = string.Empty;
  private Customer _Customer = (Customer) null;

  [DBProp("AuthenticationLogID", IsPrimaryKey = true)]
  public long AuthenticationLogID
  {
    get => this._AuthenticationLogID;
    set => this._AuthenticationLogID = value;
  }

  [DBProp("UserName", AllowNull = false)]
  public string UserName
  {
    get => this._UserName;
    set => this._UserName = value;
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("CustomerID", AllowNull = true)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("LogDate", AllowNull = false)]
  public DateTime LogDate
  {
    get => this._LogDate;
    set => this._LogDate = value;
  }

  [DBProp("Success", AllowNull = false)]
  public bool Success
  {
    get => this._Success;
    set => this._Success = value;
  }

  [DBProp("Application", AllowNull = true)]
  public string Application
  {
    get => this._Application;
    set => this._Application = value;
  }

  [DBProp("IpAddress", AllowNull = true)]
  public string IpAddress
  {
    get => this._IpAddress;
    set => this._IpAddress = value;
  }

  public Customer Customer
  {
    get
    {
      if (this._Customer == null && this._CustomerID > long.MinValue)
        this._Customer = Customer.Load(this.CustomerID);
      return this._Customer;
    }
  }

  public static AuthenticationLog Load(long ID) => BaseDBObject.Load<AuthenticationLog>(ID);

  public static void Log(
    string username,
    long customerID,
    bool success,
    string application,
    string ipAddress)
  {
    try
    {
      new AuthenticationLog()
      {
        UserName = username,
        CustomerID = customerID,
        LogDate = DateTime.UtcNow,
        Success = success,
        Application = application,
        IpAddress = ipAddress
      }.Save();
    }
    catch (Exception ex)
    {
      ExceptionLog.Log(ex);
    }
  }

  public static List<AuthenticationLog> LoadByAccountAndDateRange(
    long accountID,
    DateTime fromDate,
    DateTime toDate)
  {
    return new Monnit.Data.AuthenticationLog.LoadByAccountAndDateRange(accountID, fromDate, toDate).Result;
  }
}
