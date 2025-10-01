// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.DateNetworkModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Models;

public class DateNetworkModel
{
  [Required]
  [DisplayName("Date")]
  public DateTime Date { get; set; }

  [Required]
  [DisplayName("Network")]
  public long NetworkID { get; set; }

  public bool ShowTime { get; set; }

  public void SetTime(FormCollection collection)
  {
    int hours = collection["TimeHour"].ToInt();
    if (hours == 12)
      hours = 0;
    if (collection["TimeAM"] == "PM")
      hours += 12;
    int minutes = collection["TimeMinute"].ToInt();
    this.Date = this.Date.Add(new TimeSpan(hours, minutes, 0));
  }

  public SelectList Hours(DateTime time)
  {
    int hour = time.Hour;
    if (time.Minute >= 45)
      hour = time.AddHours(1.0).Hour;
    return new SelectList((IEnumerable) new string[12]
    {
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
      "10",
      "11",
      "12"
    }, (object) (hour > 12 ? hour - 12 : (hour == 0 ? 12 : hour)).ToString());
  }

  public SelectList Minutes(DateTime time)
  {
    string selectedValue = "00";
    if (time.Minute < 15)
      selectedValue = "15";
    else if (time.Minute < 30)
      selectedValue = "30";
    else if (time.Minute < 45)
      selectedValue = "45";
    return new SelectList((IEnumerable) new string[4]
    {
      "00",
      "15",
      "30",
      "45"
    }, (object) selectedValue);
  }

  public SelectList AM(DateTime time)
  {
    int hour = time.Hour;
    if (time.Minute >= 45)
      hour = time.AddHours(1.0).Hour;
    return new SelectList((IEnumerable) new string[2]
    {
      nameof (AM),
      "PM"
    }, hour < 12 ? (object) nameof (AM) : (object) "PM");
  }

  public SelectList Networks
  {
    get
    {
      return new SelectList((IEnumerable) CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID), "CSNetID", "Name", (object) this.NetworkID.ToString());
    }
  }

  public DateNetworkModel() => this.Date = DateTime.UtcNow;
}
