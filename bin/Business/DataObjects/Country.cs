// Decompiled with JetBrains decompiler
// Type: Monnit.Country
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("Country")]
public class Country : BaseDBObject
{
  private long _CountryID = long.MinValue;
  private string _Name = string.Empty;
  private string _AltName = string.Empty;
  private string _ISOCode = string.Empty;
  private string _ISOCode3 = string.Empty;
  private string _CountryCode = string.Empty;
  private double _HighestVoiceRate = double.MinValue;
  private int _VoiceCost = int.MinValue;
  private double _HighestSMSRate = double.MinValue;
  private int _SMSCost = int.MinValue;
  private bool _SupportsAlphanumeric = false;
  private bool _PreRegistrationRequired = false;
  private bool _PreRegistrationCompleted = false;

  [DBProp("CountryID", IsPrimaryKey = true)]
  public long CountryID
  {
    get => this._CountryID;
    set => this._CountryID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("AltName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string AltName
  {
    get => this._AltName;
    set => this._AltName = value;
  }

  [DBProp("ISOCode", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ISOCode
  {
    get => this._ISOCode;
    set => this._ISOCode = value;
  }

  [DBProp("ISOCode3", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ISOCode3
  {
    get => this._ISOCode3;
    set => this._ISOCode3 = value;
  }

  [DBProp("CountryCode", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CountryCode
  {
    get => this._CountryCode;
    set => this._CountryCode = value;
  }

  [DBProp("HighestVoiceRate")]
  public double HighestVoiceRate
  {
    get => this._HighestVoiceRate;
    set => this._HighestVoiceRate = value;
  }

  [DBProp("VoiceCost")]
  public int VoiceCost
  {
    get => this._VoiceCost;
    set => this._VoiceCost = value;
  }

  [DBProp("HighestSMSRate")]
  public double HighestSMSRate
  {
    get => this._HighestSMSRate;
    set => this._HighestSMSRate = value;
  }

  [DBProp("SMSCost")]
  public int SMSCost
  {
    get => this._SMSCost;
    set => this._SMSCost = value;
  }

  [DBProp("SupportsAlphanumeric", AllowNull = true)]
  public bool SupportsAlphanumeric
  {
    get => this._SupportsAlphanumeric;
    set => this._SupportsAlphanumeric = value;
  }

  [DBProp("PreRegistrationRequired", AllowNull = true)]
  public bool PreRegistrationRequired
  {
    get => this._PreRegistrationRequired;
    set => this._PreRegistrationRequired = value;
  }

  [DBProp("PreRegistrationCompleted", AllowNull = true)]
  public bool PreRegistrationCompleted
  {
    get => this._PreRegistrationCompleted;
    set => this._PreRegistrationCompleted = value;
  }

  public static Country Load(long id)
  {
    Country country = Country.LoadAll().FirstOrDefault<Country>((Func<Country, bool>) (obj => obj.PrimaryKeyValue == id));
    if (country == null)
    {
      country = BaseDBObject.Load<Country>(id);
      if (country != null)
        Country.LoadAll().Add(country);
    }
    return country;
  }

  public static List<Country> LoadAll()
  {
    string key = "CountryList";
    List<Country> countryList = TimedCache.RetrieveObject<List<Country>>(key);
    if (countryList == null)
    {
      countryList = BaseDBObject.LoadAll<Country>();
      if (countryList != null)
        TimedCache.AddObjectToCach(key, (object) countryList, new TimeSpan(2, 0, 0));
    }
    return countryList;
  }

  public static Country Find(string name)
  {
    return Country.LoadAll().Where<Country>((Func<Country, bool>) (c => c.Name.ToLower() == name.ToLower())).FirstOrDefault<Country>() ?? Country.LoadAll().Where<Country>((Func<Country, bool>) (c => c.AltName.ToLower().Contains(name.ToLower()))).FirstOrDefault<Country>();
  }

  public static Country FindByISOCodeOrNumber(string code, string number)
  {
    if (!string.IsNullOrEmpty(code))
    {
      Country byCode = Country.FindByCode(code);
      if (byCode != null)
        return byCode;
    }
    return !string.IsNullOrEmpty(number) ? Country.LoadBestByPhone(number) : (Country) null;
  }

  public static Country FindByCode(string code)
  {
    switch (code.Length)
    {
      case 2:
        return Country.LoadAll().Where<Country>((Func<Country, bool>) (c => c.ISOCode.ToLower() == code.ToLower())).FirstOrDefault<Country>();
      case 3:
        return Country.LoadAll().Where<Country>((Func<Country, bool>) (c => c.ISOCode3.ToLower().Contains(code.ToLower()))).FirstOrDefault<Country>();
      default:
        return (Country) null;
    }
  }

  public static Country LoadBestByPhone(string phone)
  {
    phone = phone.RemoveNonNumeric();
    return Country.LoadAll().Where<Country>((Func<Country, bool>) (c => phone.StartsWith(c.CountryCode))).OrderByDescending<Country, int>((Func<Country, int>) (c => c.CountryCode.Length)).FirstOrDefault<Country>();
  }
}
