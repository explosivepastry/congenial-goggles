// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.MissedSensorMessages
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.Data;

#nullable disable
namespace iMonnit.Models;

public class MissedSensorMessages
{
  public MissedSensorMessages(DataRow dr)
  {
    this.ExpectedCheckIn = Monnit.TimeZone.GetLocalTimeById(Convert.ToDateTime(dr["MissedMessageDate"]), MonnitSession.CurrentCustomer.Account.TimeZoneID);
    this.Minutes = Convert.ToInt32(dr["MinutesBetween"]);
    this.Days = 0;
    this.Hours = 0;
    if (this.Minutes > 1440)
    {
      this.Days = this.Minutes / 1440;
      this.Minutes %= 1440;
    }
    if (this.Minutes <= 60)
      return;
    this.Hours = this.Minutes / 60;
    this.Minutes %= 60;
  }

  public DateTime ExpectedCheckIn { get; set; }

  public int Days { get; set; }

  public int Hours { get; set; }

  public int Minutes { get; set; }
}
