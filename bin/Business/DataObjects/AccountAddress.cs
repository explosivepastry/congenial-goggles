// Decompiled with JetBrains decompiler
// Type: Monnit.AccountAddress
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Text;

#nullable disable
namespace Monnit;

[DBClass("AccountAddress")]
public class AccountAddress : BaseDBObject
{
  private long _AccountAddressID = long.MinValue;
  private long _AccountID = long.MinValue;
  private eAccountAddressType _AccountAddressType;
  private string _ProfileName = string.Empty;
  private string _Address = string.Empty;
  private string _Address2 = string.Empty;
  private string _City = string.Empty;
  private string _State = string.Empty;
  private string _PostalCode = string.Empty;
  private string _Country = string.Empty;
  private string _Attention = string.Empty;
  private string _CardType = string.Empty;
  private string _CardNumber = string.Empty;
  private string _NameOnCard = string.Empty;
  private DateTime _ExpirationDate = DateTime.MinValue;
  private long _WeatherLocationID = long.MinValue;

  [DBProp("AccountAddressID", IsPrimaryKey = true)]
  public long AccountAddressID
  {
    get => this._AccountAddressID;
    set => this._AccountAddressID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("eAccountAddressType", AllowNull = false)]
  public eAccountAddressType AccountAddressType
  {
    get => this._AccountAddressType;
    set => this._AccountAddressType = value;
  }

  [DBProp("ProfileName", MaxLength = 255 /*0xFF*/)]
  public string ProfileName
  {
    get => this._ProfileName;
    set
    {
      if (value == null)
        this._ProfileName = string.Empty;
      else
        this._ProfileName = value;
    }
  }

  [DBProp("Address", MaxLength = 255 /*0xFF*/)]
  public string Address
  {
    get => this._Address;
    set
    {
      if (value == null)
        this._Address = string.Empty;
      else
        this._Address = value;
    }
  }

  [DBProp("Address2", MaxLength = 255 /*0xFF*/)]
  public string Address2
  {
    get => this._Address2;
    set
    {
      if (value == null)
        this._Address2 = string.Empty;
      else
        this._Address2 = value;
    }
  }

  [DBProp("City", MaxLength = 255 /*0xFF*/)]
  public string City
  {
    get => this._City;
    set
    {
      if (value == null)
        this._City = string.Empty;
      else
        this._City = value;
    }
  }

  [DBProp("State", MaxLength = 255 /*0xFF*/)]
  public string State
  {
    get => this._State;
    set
    {
      if (value == null)
        this._State = string.Empty;
      else
        this._State = value;
    }
  }

  [DBProp("PostalCode", MaxLength = 255 /*0xFF*/)]
  public string PostalCode
  {
    get => this._PostalCode;
    set
    {
      if (value == null)
        this._PostalCode = string.Empty;
      else
        this._PostalCode = value;
    }
  }

  [DBProp("Country", MaxLength = 255 /*0xFF*/)]
  public string Country
  {
    get => this._Country;
    set
    {
      if (value == null)
        this._Country = string.Empty;
      else
        this._Country = value;
    }
  }

  [DBProp("Attention", MaxLength = 255 /*0xFF*/)]
  public string Attention
  {
    get => this._Attention;
    set
    {
      if (value == null)
        this._Attention = string.Empty;
      else
        this._Attention = value;
    }
  }

  [DBProp("CardType", MaxLength = 255 /*0xFF*/)]
  public string CardType
  {
    get => this._CardType;
    set
    {
      if (value == null)
        this._CardType = string.Empty;
      else
        this._CardType = value;
    }
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

  [DBProp("NameOnCard", MaxLength = 255 /*0xFF*/)]
  public string NameOnCard
  {
    get => this._NameOnCard;
    set
    {
      if (value == null)
        this._NameOnCard = string.Empty;
      else
        this._NameOnCard = value;
    }
  }

  [DBProp("ExpirationDate")]
  public DateTime ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [DBProp("WeatherLocationID")]
  [DBForeignKey("WeatherLocation", "WeatherLocationID")]
  public long WeatherLocationID
  {
    get => this._WeatherLocationID;
    set => this._WeatherLocationID = value;
  }

  public static AccountAddress Load(long ID) => BaseDBObject.Load<AccountAddress>(ID);

  public static AccountAddress LoadFirstByType(
    long accountID,
    eAccountAddressType accountAddressType)
  {
    return new Monnit.Data.AccountAddress.LoadFirstByType(accountID, accountAddressType).Result;
  }

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendLine(this.Address);
    if (this.Address2.Trim() != string.Empty)
      stringBuilder.AppendLine(this.Address2);
    stringBuilder.AppendFormat("{0}, {1} {2} ", (object) this.City, (object) this.State, (object) this.PostalCode);
    stringBuilder.AppendLine(this.Country);
    return stringBuilder.ToString();
  }

  public string ToHtmlString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("{0}<br/>", (object) this.Address);
    if (this.Address2.Trim() != string.Empty)
      stringBuilder.AppendFormat("{0}<br/>", (object) this.Address2);
    stringBuilder.AppendFormat("{0}, {1} {2} <br/>", (object) this.City, (object) this.State, (object) this.PostalCode);
    stringBuilder.AppendFormat("{0}", (object) this.Country);
    return stringBuilder.ToString();
  }
}
