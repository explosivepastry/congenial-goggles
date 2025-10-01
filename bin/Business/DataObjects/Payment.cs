// Decompiled with JetBrains decompiler
// Type: Monnit.Payment
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("Payment")]
public class Payment : BaseDBObject
{
  private long _PaymentID = long.MinValue;
  private long _OrderID = long.MinValue;
  private string _CardNumber = string.Empty;
  private double _PaymentAmount = double.MinValue;
  private DateTime _PaymentDate = DateTime.MinValue;
  private string _ResponseCode = string.Empty;
  private string _CapturedResponseCode = string.Empty;
  private ePaymentType _PaymentType = ePaymentType.Charge;
  private bool _Approved = false;
  private string _TransactionID = string.Empty;
  private string _AuthCode = string.Empty;
  private bool _Captured = false;
  private string _Message = string.Empty;

  [DBProp("PaymentID", IsPrimaryKey = true)]
  public long PaymentID
  {
    get => this._PaymentID;
    set => this._PaymentID = value;
  }

  [DBForeignKey("OnlineOrder", "OrderID")]
  [DBProp("OrderID", AllowNull = false)]
  public long OrderID
  {
    get => this._OrderID;
    set => this._OrderID = value;
  }

  [DBProp("CardNumber", MaxLength = 255 /*0xFF*/)]
  public string CardNumber
  {
    get => this._CardNumber;
    set
    {
      if (value == null)
        this._CardNumber = string.Empty;
      else
        this._CardNumber = value;
    }
  }

  [DBProp("PaymentAmount", AllowNull = false)]
  public double PaymentAmount
  {
    get => this._PaymentAmount;
    set => this._PaymentAmount = value;
  }

  [DBProp("PaymentDate", AllowNull = false)]
  public DateTime PaymentDate
  {
    get => this._PaymentDate;
    set => this._PaymentDate = value;
  }

  [DBProp("ResponseCode", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ResponseCode
  {
    get => this._ResponseCode;
    set => this._ResponseCode = value;
  }

  [DBProp("CapturedResponseCode", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CapturedResponseCode
  {
    get => this._CapturedResponseCode;
    set => this._CapturedResponseCode = value;
  }

  [DBProp("PaymentType", AllowNull = false)]
  public ePaymentType PaymentType
  {
    get => this._PaymentType;
    set => this._PaymentType = value;
  }

  [DBProp("Approved")]
  public bool Approved
  {
    get => this._Approved;
    set => this._Approved = value;
  }

  [DBProp("TransactionID", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string TransactionID
  {
    get => this._TransactionID;
    set => this._TransactionID = value;
  }

  [DBProp("AuthCode", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string AuthCode
  {
    get => this._AuthCode;
    set => this._AuthCode = value;
  }

  [DBProp("Captured")]
  public bool Captured
  {
    get => this._Captured;
    set => this._Captured = value;
  }

  [DBProp("Message", MaxLength = 2000, AllowNull = true)]
  public string Message
  {
    get => this._Message;
    set => this._Message = value;
  }

  public static Payment Load(long ID) => BaseDBObject.Load<Payment>(ID);

  public static List<Payment> LoadByOrderID(long orderID)
  {
    return BaseDBObject.LoadByForeignKey<Payment>("OrderID", (object) orderID);
  }
}
