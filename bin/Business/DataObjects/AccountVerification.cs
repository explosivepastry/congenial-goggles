// Decompiled with JetBrains decompiler
// Type: Monnit.AccountVerification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("AccountVerification")]
public class AccountVerification : BaseDBObject
{
  private long _AccountVerificationID = long.MinValue;
  private string _EmailAddress = string.Empty;
  private string _Password = string.Empty;
  private string _Name = string.Empty;
  private string _LocationName = string.Empty;
  private string _IanaTimeZone = string.Empty;
  private string _VerificationCode = string.Empty;
  private bool _EULA = false;
  private bool _SendVerificationEmail = false;
  private DateTime _CreateDate = DateTime.UtcNow;
  private byte[] _Password2 = (byte[]) null;
  private byte[] _Salt = (byte[]) null;
  private int _WorkFactor = 0;

  [DBProp("AccountVerificationID", IsPrimaryKey = true)]
  public long AccountVerificationID
  {
    get => this._AccountVerificationID;
    set => this._AccountVerificationID = value;
  }

  [DBProp("EmailAddress", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string EmailAddress
  {
    get => this._EmailAddress;
    set => this._EmailAddress = value;
  }

  [DBProp("Password", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Password
  {
    get => this._Password;
    set
    {
      if (value == null)
        this._Password = string.Empty;
      else
        this._Password = value;
    }
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true, International = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("LocationName", MaxLength = 255 /*0xFF*/, AllowNull = true, International = true)]
  public string LocationName
  {
    get => this._LocationName;
    set => this._LocationName = value;
  }

  [DBProp("IanaTimeZone", MaxLength = 1000, AllowNull = true)]
  public string IanaTimeZone
  {
    get => this._IanaTimeZone;
    set => this._IanaTimeZone = value;
  }

  [DBProp("VerificationCode", MaxLength = 500, AllowNull = false)]
  public string VerificationCode
  {
    get => this._VerificationCode;
    set => this._VerificationCode = value;
  }

  [DBProp("EULA")]
  public bool EULA
  {
    get => this._EULA;
    set => this._EULA = value;
  }

  [DBProp("SendVerificationEmail")]
  public bool SendVerificationEmail
  {
    get => this._SendVerificationEmail;
    set => this._SendVerificationEmail = value;
  }

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("Password2", AllowNull = true, MaxLength = 255 /*0xFF*/)]
  public byte[] Password2
  {
    get => this._Password2;
    set => this._Password2 = value;
  }

  [DBProp("Salt", AllowNull = true, MaxLength = 25)]
  public byte[] Salt
  {
    get => this._Salt;
    set => this._Salt = value;
  }

  [DBProp("WorkFactor", AllowNull = false, DefaultValue = 0)]
  public int WorkFactor
  {
    get => this._WorkFactor;
    set => this._WorkFactor = value;
  }

  public static AccountVerification LoadByCode(string VerificationCode)
  {
    return new Monnit.Data.AccountVerification.LoadByCode(VerificationCode).Result;
  }

  public static AccountVerification LoadByEmailAddress(string VerificationCode)
  {
    return new Monnit.Data.AccountVerification.LoadByEmailAddress(VerificationCode).Result;
  }

  public override void Save()
  {
    this.Password = "";
    if (this.Password2 == null)
      this.Password2 = new byte[1];
    if (this.Salt == null)
      this.Salt = new byte[1];
    base.Save();
  }
}
