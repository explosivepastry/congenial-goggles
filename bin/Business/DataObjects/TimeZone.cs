// Decompiled with JetBrains decompiler
// Type: Monnit.TimeZone
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("TimeZone")]
public class TimeZone : BaseDBObject
{
  private long _TimeZoneID = long.MinValue;
  private string _TimeZoneIDString = string.Empty;
  private string _DisplayName = string.Empty;
  private bool _SupportsDST;
  private string _Region = string.Empty;
  private TimeZoneInfo _Info;
  private static List<TimeZone> _TimeZones;

  [DBProp("TimeZoneID", IsPrimaryKey = true)]
  public long TimeZoneID
  {
    get => this._TimeZoneID;
    set => this._TimeZoneID = value;
  }

  [DBProp("TimeZoneIDString", MaxLength = 100, AllowNull = false)]
  public string TimeZoneIDString
  {
    get => this._TimeZoneIDString;
    set => this._TimeZoneIDString = value;
  }

  [DBProp("DisplayName", MaxLength = 100, AllowNull = false)]
  public string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  [DBProp("SupportsDST", AllowNull = false)]
  public bool SupportsDTS
  {
    get => this._SupportsDST;
    set => this._SupportsDST = value;
  }

  [DBProp("Region", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Region
  {
    get => this._Region;
    set => this._Region = value;
  }

  public static TimeZone Load(long ID)
  {
    return TimeZone.LoadAll().Find((Predicate<TimeZone>) (tz => tz.TimeZoneID == ID));
  }

  public string CurrentTimeWithName
  {
    get
    {
      return $"{this.DisplayName} | Current Time: {TimeZone.GetLocalTime(DateTime.UtcNow, this.Info).ToShortTimeString()}";
    }
  }

  public static TimeZone Default
  {
    get
    {
      TimeZone timeZone = TimedCache.RetrieveObject<TimeZone>("TimeZone_Default");
      if (timeZone == null)
      {
        Account account = Account.Load(ConfigData.AppSettings("AdminAccountID").ToLong());
        if (account != null)
          timeZone = TimeZone.Load(account.TimeZoneID);
        if (timeZone == null)
          timeZone = TimeZone.LoadAll().FirstOrDefault<TimeZone>();
        TimedCache.AddObjectToCach("TimeZone_Default", (object) timeZone);
      }
      return timeZone;
    }
  }

  public TimeZoneInfo Info
  {
    get
    {
      if (this._Info == null)
        this._Info = TimeZoneInfo.FindSystemTimeZoneById(this.TimeZoneIDString);
      return this._Info;
    }
  }

  public static List<TimeZone> LoadAll()
  {
    if (TimeZone._TimeZones == null)
      TimeZone._TimeZones = BaseDBObject.LoadAll<TimeZone>();
    return TimeZone._TimeZones;
  }

  public static List<TimeZone> LoadByRegion(string region)
  {
    return new List<TimeZone>(TimeZone.LoadAll().Where<TimeZone>((Func<TimeZone, bool>) (zone => zone.Region == region)));
  }

  public static List<string> LoadRegions()
  {
    return new List<string>((IEnumerable<string>) new HashSet<string>(TimeZone.LoadAll().Select<TimeZone, string>((Func<TimeZone, string>) (zone => zone.Region))).ToArray<string>());
  }

  public static DateTime GetLocalTime(DateTime UTCTime, TimeZoneInfo myTimeZoneInfo)
  {
    try
    {
      return TimeZoneInfo.ConvertTimeFromUtc(UTCTime, myTimeZoneInfo);
    }
    catch (TimeZoneNotFoundException ex)
    {
      return UTCTime;
    }
    catch (InvalidTimeZoneException ex)
    {
      return UTCTime;
    }
  }

  public static DateTime GetLocalTimeById(DateTime UTCTime, long myTimeZoneId)
  {
    TimeZone timeZone = TimeZone.Load(myTimeZoneId) ?? TimeZone.Default;
    return TimeZone.GetLocalTime(UTCTime, timeZone.Info);
  }

  public static DateTime GetUTCFromLocal(DateTime LocalTime, TimeZoneInfo myTimeZoneInfo)
  {
    try
    {
      return TimeZoneInfo.ConvertTimeToUtc(LocalTime, myTimeZoneInfo);
    }
    catch (TimeZoneNotFoundException ex)
    {
      string str = $"{$" ExceptionType=[TimeZoneNotFoundException] LocalTime=[{LocalTime.ToStringSafe()}]"}, myTimeZoneInfo=[{(myTimeZoneInfo == null ? "null" : "id=" + myTimeZoneInfo.ToStringSafe())}] ";
      ex.Log("GetUTCFromLocal()");
      return LocalTime;
    }
    catch (InvalidTimeZoneException ex)
    {
      ex.Log("TimeZone.GetUTCFromLocal(): " + $"{$" ExceptionType=[InvalidTimeZoneException] LocalTime=[{LocalTime.ToStringSafe()}]"}, myTimeZoneInfo=[{(myTimeZoneInfo == null ? "null" : "id=" + myTimeZoneInfo.ToStringSafe())}] ");
      return LocalTime;
    }
    catch (Exception ex)
    {
      ex.Log("TimeZone.GetUTCFromLocal(): " + $"{$" ExceptionType=[Generic] LocalTime=[{LocalTime.ToStringSafe()}]"}, myTimeZoneInfo=[{(myTimeZoneInfo == null ? "null" : "id=" + myTimeZoneInfo.ToStringSafe())}] ");
      return LocalTime;
    }
  }

  public static DateTime GetUTCFromLocalById(DateTime LocalTime, long myTimeZoneId)
  {
    TimeZone timeZone = TimeZone.Load(myTimeZoneId);
    return TimeZone.GetUTCFromLocal(LocalTime, timeZone.Info);
  }

  public static TimeSpan GetCurrentLocalOffset(TimeZoneInfo myTimeZoneInfo)
  {
    return myTimeZoneInfo.GetUtcOffset(DateTime.Now.Date);
  }

  public static TimeSpan GetCurrentLocalOffsetById(long myTimeZoneId)
  {
    return TimeZone.GetCurrentLocalOffset(TimeZone.Load(myTimeZoneId).Info);
  }

  public static bool IsValidTimeZone(long timeZoneID) => TimeZone.Load(timeZoneID) != null;
}
