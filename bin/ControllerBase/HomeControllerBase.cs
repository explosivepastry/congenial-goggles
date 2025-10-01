// Decompiled with JetBrains decompiler
// Type: iMonnit.ControllerBase.HomeControllerBase
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Monnit;
using System;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.ControllerBase;

public class HomeControllerBase : ThemeController
{
  protected List<AccountThemeReleaseNoteModel> CheckReleaseNotes(Version codeVersion)
  {
    List<AccountThemeReleaseNoteModel> releaseNoteModelList = new List<AccountThemeReleaseNoteModel>();
    if (MonnitSession.CurrentCustomer != null && !MonnitSession.UserIsCustomerProxied && (MonnitSession.CurrentCustomer.LastViewedReleaseNote(MonnitSession.CurrentCustomer.CustomerID) == null || new Version(MonnitSession.LastReleaseNoteViewed.Version) < codeVersion))
    {
      List<ReleaseNote> releaseNoteList = ReleaseNote.LoadActiveReleaseNotesByDateAndVersion(codeVersion, DateTime.Now.AddYears(-1));
      DateTime createDate = MonnitSession.CurrentCustomer.CreateDate;
      foreach (ReleaseNote releaseNote in releaseNoteList)
      {
        if (releaseNote.ReleaseDate >= createDate && !MonnitSession.CurrentCustomer.HasSeenReleaseNote(releaseNote))
        {
          MonnitSession.CurrentCustomer.AcknowledgeReleaseNote(releaseNote.ReleaseNoteID, MonnitSession.CurrentCustomer.CustomerID, false);
          releaseNoteModelList.Add(new AccountThemeReleaseNoteModel(releaseNote, MonnitSession.CurrentTheme.AccountThemeID));
        }
      }
    }
    return releaseNoteModelList;
  }

  protected void PrimaryContactNotifications()
  {
    if (MonnitSession.CurrentCustomer == null || MonnitSession.CurrentCustomer.Account == null || MonnitSession.CurrentCustomer.Account.PrimaryContactID != MonnitSession.CurrentCustomer.CustomerID || !(this.Request.UrlReferrer != (Uri) null) || !this.Request.UrlReferrer.LocalPath.ToLower().Contains("logon"))
      return;
    List<CSNet> csNetList = CSNet.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    foreach (CSNet csNet in csNetList)
    {
      if (!csNet.SendNotifications && csNet.AlertNotificationsAreOff)
      {
        this.TempData["networks"] = (object) csNetList;
        this.ViewData["showAlertNotification"] = (object) true;
      }
    }
    DateTime now;
    int num;
    if (MonnitSession.CurrentCustomer.Account.IsPremium)
    {
      DateTime premiumValidUntil = MonnitSession.CurrentCustomer.Account.PremiumValidUntil;
      now = DateTime.Now;
      DateTime dateTime = now.AddDays(30.0);
      if (premiumValidUntil < dateTime)
      {
        num = 1;
        goto label_15;
      }
    }
    if (!MonnitSession.CurrentCustomer.Account.IsPremium)
    {
      DateTime premiumValidUntil = MonnitSession.CurrentCustomer.Account.PremiumValidUntil;
      now = DateTime.Now;
      DateTime dateTime = now.AddDays(-15.0);
      num = premiumValidUntil > dateTime ? 1 : 0;
    }
    else
      num = 0;
label_15:
    if (num != 0)
    {
      this.ViewData["showAlertNotification"] = (object) false;
      this.ViewData["ExpiredNotification"] = (object) true;
    }
  }

  public static void Unsubscribe(string address, string reason)
  {
    try
    {
      if (UnsubscribedNotificationEmail.EmailIsUnsubscribed(address))
        return;
      new UnsubscribedNotificationEmail()
      {
        EmailAddress = address,
        OptOutDate = DateTime.UtcNow,
        Reason = reason
      }.Save();
      UnsubscribedNotificationEmail.ClearCachedList();
    }
    catch (Exception ex)
    {
      ex.Log("HomeBase.Unsubscribe ");
    }
  }
}
