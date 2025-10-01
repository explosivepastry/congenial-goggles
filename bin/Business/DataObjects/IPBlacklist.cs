// Decompiled with JetBrains decompiler
// Type: Monnit.IPBlacklist
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using Monnit.Data;
using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("IPBlacklist")]
public class IPBlacklist : BaseDBObject
{
  private long _IPBlackListID = long.MinValue;
  private string _IPAddress = string.Empty;
  private int _FailedAttempts = 0;
  private DateTime _FirstFailedAttempt = DateTime.UtcNow;

  [DBProp("IPBlackListID", IsPrimaryKey = true)]
  public long IPBlackListID
  {
    get => this._IPBlackListID;
    set => this._IPBlackListID = value;
  }

  [DBProp("IPAddress", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string IPAddress
  {
    get => this._IPAddress;
    set => this._IPAddress = value;
  }

  [DBProp("FailedAttempts", AllowNull = false, DefaultValue = 0)]
  public int FailedAttempts
  {
    get => this._FailedAttempts;
    set => this._FailedAttempts = value;
  }

  [DBProp("FirstFailedAttempt")]
  public DateTime FirstFailedAttempt
  {
    get => this._FirstFailedAttempt;
    set => this._FirstFailedAttempt = value;
  }

  public bool BlacklistAttempt()
  {
    int num = ConfigData.AppSettings("IPBlacklistCount", "5").ToInt();
    if (this.BlacklistTimeUntilExpiration().TotalSeconds > 0.0)
    {
      ++this.FailedAttempts;
      if (this.FailedAttempts > num * 3)
        this.FirstFailedAttempt = this.FirstFailedAttempt = DateTime.UtcNow.AddMinutes((double) (ConfigData.AppSettings("IPBlacklistTime", "5").ToInt() * 11));
      this.Save();
      if (this.FailedAttempts > num)
        return false;
    }
    else
    {
      this.FailedAttempts = 0;
      this.FirstFailedAttempt = DateTime.UtcNow;
    }
    return true;
  }

  public DateTime BlacklistExpirationTimeUTC()
  {
    return this.FirstFailedAttempt.AddMinutes((double) ConfigData.AppSettings("IPBlacklistTime", "5").ToInt());
  }

  public TimeSpan BlacklistTimeUntilExpiration()
  {
    return this.BlacklistExpirationTimeUTC().Subtract(DateTime.UtcNow);
  }

  public override string ToString()
  {
    return this.BlacklistTimeUntilExpiration().TotalSeconds > 0.0 ? this.BlacklistTimeUntilExpiration().ToString("m' Minutes 's' Seconds'") : TimeSpan.Zero.ToString("m' Minutes 's' Seconds'");
  }

  public static List<IPBlacklist> LoadByIP(string IPAddress)
  {
    return new IPBlackList.LoadByIP(IPAddress, ConfigData.AppSettings("IPBlacklistTime", "5").ToInt()).Result;
  }
}
