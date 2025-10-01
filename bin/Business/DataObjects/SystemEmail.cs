// Decompiled with JetBrains decompiler
// Type: Monnit.SystemEmail
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SystemEmail")]
public class SystemEmail : BaseDBObject
{
  private long _SystemEmailID = long.MinValue;
  private long _SystemEmailContentID = long.MinValue;
  private DateTime _CreateDate = DateTime.MinValue;
  private long _CustomerID = long.MinValue;
  private string _ToAddress = string.Empty;
  private int _RetryCount = 0;
  private bool _DoRetry = false;
  private string _Status = string.Empty;
  private DateTime _ProcessDate = DateTime.MinValue;
  private DateTime _SendDate = DateTime.MinValue;

  [DBProp("SystemEmailID", IsPrimaryKey = true)]
  public long SystemEmailID
  {
    get => this._SystemEmailID;
    set => this._SystemEmailID = value;
  }

  [DBProp("SystemEmailContentID")]
  [DBForeignKey("SystemEmailContent", "SystemEmailContentID")]
  public long SystemEmailContentID
  {
    get => this._SystemEmailContentID;
    set => this._SystemEmailContentID = value;
  }

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("CustomerID", AllowNull = true)]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("ToAddress", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ToAddress
  {
    get => this._ToAddress;
    set => this._ToAddress = value;
  }

  [DBProp("RetryCount", DefaultValue = 0)]
  public int RetryCount
  {
    get => this._RetryCount;
    set => this._RetryCount = value;
  }

  [DBProp("DoRetry", DefaultValue = false)]
  public bool DoRetry
  {
    get => this._DoRetry;
    set => this._DoRetry = value;
  }

  [DBProp("Status", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [DBProp("ProcessDate")]
  public DateTime ProcessDate
  {
    get => this._ProcessDate;
    set => this._ProcessDate = value;
  }

  [DBProp("SendDate")]
  public DateTime SendDate
  {
    get => this._SendDate;
    set => this._SendDate = value;
  }

  public static SystemEmail Load(long id) => BaseDBObject.Load<SystemEmail>(id);

  public static List<SystemEmail> LoadReady(int limit) => new Monnit.Data.SystemEmail.LoadReady(limit).Result;
}
