// Decompiled with JetBrains decompiler
// Type: Monnit.IANATimeZone
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("IANATimeZone")]
public class IANATimeZone : BaseDBObject
{
  private long _IanaTimeZoneID = long.MinValue;
  private string _IanaIDString = string.Empty;
  private string _Territory = string.Empty;
  private string _TimeZoneIDString = string.Empty;
  private long _TimeZoneID = long.MinValue;

  [DBProp("IanaTimeZoneID", IsPrimaryKey = true)]
  public long IanaTimeZoneID
  {
    get => this._IanaTimeZoneID;
    set => this._IanaTimeZoneID = value;
  }

  [DBProp("IanaIDString", MaxLength = 1000, AllowNull = false)]
  public string IanaIDString
  {
    get => this._IanaIDString;
    set => this._IanaIDString = value;
  }

  [DBProp("Territory", MaxLength = 30, AllowNull = false)]
  public string Territory
  {
    get => this._Territory;
    set => this._Territory = value;
  }

  [DBProp("TimeZoneIDString", MaxLength = 300, AllowNull = false)]
  public string TimeZoneIDString
  {
    get => this._TimeZoneIDString;
    set => this._TimeZoneIDString = value;
  }

  [DBProp("TimeZoneID")]
  public long TimeZoneID
  {
    get => this._TimeZoneID;
    set => this._TimeZoneID = value;
  }

  public static Dictionary<string, IANATimeZone> LoadAll()
  {
    string key = nameof (IANATimeZone);
    Dictionary<string, IANATimeZone> dictionary = TimedCache.RetrieveObject<Dictionary<string, IANATimeZone>>(key);
    if (dictionary == null)
    {
      dictionary = new Dictionary<string, IANATimeZone>();
      List<IANATimeZone> ianaTimeZoneList = BaseDBObject.LoadAll<IANATimeZone>();
      if (ianaTimeZoneList != null)
      {
        foreach (IANATimeZone ianaTimeZone in ianaTimeZoneList)
        {
          if (!dictionary.ContainsKey(ianaTimeZone.IanaIDString))
            dictionary.Add(ianaTimeZone.IanaIDString, ianaTimeZone);
        }
        TimedCache.AddObjectToCach(key, (object) dictionary, new TimeSpan(6, 0, 0));
      }
    }
    return dictionary;
  }

  public static IANATimeZone Find(string IANATimeZone)
  {
    try
    {
      return IANATimeZone.LoadAll()[IANATimeZone];
    }
    catch (Exception ex)
    {
      ex.Log("IANATimeZone.Find/ | IANATimeZone = " + IANATimeZone);
      return (IANATimeZone) null;
    }
  }
}
