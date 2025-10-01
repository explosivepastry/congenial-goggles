// Decompiled with JetBrains decompiler
// Type: Monnit.SMSCarrier
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("SMSCarrier")]
public class SMSCarrier : BaseDBObject
{
  private long _SMSCarrierID = long.MinValue;
  private string _SMSCarrierName = string.Empty;
  private string _SMSFormatString = string.Empty;
  private int _Rank = int.MinValue;
  private int _ExpectedPhoneDigits = int.MinValue;
  private long _AccountThemeID = long.MinValue;
  private bool _AuthenticationInTemplateBody = false;

  [DBProp("SMSCarrierID", IsPrimaryKey = true)]
  public long SMSCarrierID
  {
    get => this._SMSCarrierID;
    set => this._SMSCarrierID = value;
  }

  [DBProp("SMSCarrierName", MaxLength = 255 /*0xFF*/)]
  public string SMSCarrierName
  {
    get => this._SMSCarrierName;
    set
    {
      if (value == null)
        this._SMSCarrierName = string.Empty;
      else
        this._SMSCarrierName = value;
    }
  }

  [DBProp("SMSFormatString", MaxLength = 255 /*0xFF*/)]
  public string SMSFormatString
  {
    get => this._SMSFormatString;
    set
    {
      if (value == null)
        this._SMSFormatString = string.Empty;
      else
        this._SMSFormatString = value;
    }
  }

  [DBProp("Rank")]
  public int Rank
  {
    get => this._Rank;
    set => this._Rank = value;
  }

  [DBProp("ExpectedPhoneDigits")]
  public int ExpectedPhoneDigits
  {
    get => this._ExpectedPhoneDigits;
    set => this._ExpectedPhoneDigits = value;
  }

  [DBForeignKey("AccountTheme", "AccountThemeID")]
  [DBProp("AccountThemeID", AllowNull = true)]
  public long AccountThemeID
  {
    get => this._AccountThemeID;
    set => this._AccountThemeID = value;
  }

  [DBProp("AuthenticationInTemplateBody")]
  public bool AuthenticationInTemplateBody
  {
    get => this._AuthenticationInTemplateBody;
    set => this._AuthenticationInTemplateBody = value;
  }

  public static SMSCarrier Load(long ID)
  {
    return SMSCarrier.LoadAll().FirstOrDefault<SMSCarrier>((Func<SMSCarrier, bool>) (obj => obj.SMSCarrierID == ID));
  }

  public static List<SMSCarrier> LoadAll()
  {
    string key = "SMSCarrierList";
    List<SMSCarrier> smsCarrierList = TimedCache.RetrieveObject<List<SMSCarrier>>(key);
    if (smsCarrierList == null)
    {
      smsCarrierList = BaseDBObject.LoadAll<SMSCarrier>();
      if (smsCarrierList != null)
        TimedCache.AddObjectToCach(key, (object) smsCarrierList, new TimeSpan(6, 0, 0));
    }
    return smsCarrierList;
  }

  public string SMSAddress(string number)
  {
    string str = number.RemoveNonNumeric();
    return str != null && str != string.Empty && str.Length == this.ExpectedPhoneDigits ? string.Format(this.SMSFormatString, (object) str) : string.Empty;
  }
}
