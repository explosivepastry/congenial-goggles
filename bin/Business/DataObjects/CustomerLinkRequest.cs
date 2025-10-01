// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerLinkRequest
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerLinkRequest")]
public class CustomerLinkRequest : BaseDBObject
{
  private long _CustomerLinkRequestID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private string _EmailAddress = string.Empty;
  private string _LinkCode = string.Empty;
  private DateTime _CreateDate = DateTime.MinValue;
  private DateTime _ActivationDate = DateTime.MinValue;
  private bool _IsDeleted = false;

  [DBProp("CustomerLinkRequestID", IsPrimaryKey = true)]
  public long CustomerLinkRequestID
  {
    get => this._CustomerLinkRequestID;
    set => this._CustomerLinkRequestID = value;
  }

  [DBProp("CustomerID", AllowNull = false)]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("EmailAddress", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string EmailAddress
  {
    get => this._EmailAddress;
    set => this._EmailAddress = value;
  }

  [DBProp("LinkCode", MaxLength = 10, AllowNull = false)]
  public string LinkCode
  {
    get => this._LinkCode;
    set => this._LinkCode = value;
  }

  [DBProp("CreateDate", AllowNull = false)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("ActivationDate", AllowNull = true)]
  public DateTime ActivationDate
  {
    get => this._ActivationDate;
    set => this._ActivationDate = value;
  }

  [DBProp("IsDeleted", DefaultValue = false, AllowNull = false)]
  public bool IsDeleted
  {
    get => this._IsDeleted;
    set => this._IsDeleted = value;
  }

  public static CustomerLinkRequest LoadActiveByLinkCode(string linkCode)
  {
    return new Monnit.Data.CustomerLinkRequest.LoadActiveByLinkCode(linkCode).Result;
  }

  public static List<CustomerLinkRequest> LoadByEmail(string email)
  {
    return new Monnit.Data.CustomerLinkRequest.LoadByEmail(email).Result;
  }

  public static CustomerLinkRequest Load(long id) => BaseDBObject.Load<CustomerLinkRequest>(id);
}
