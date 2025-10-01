// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CreateNotificationAPIModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.Models;

public class CreateNotificationAPIModel
{
  [Required]
  [AllowHtml]
  public string Name { get; set; }

  [AllowHtml]
  public string Text { get; set; }

  [AllowHtml]
  public string smsText { get; set; }

  [AllowHtml]
  public string voiceText { get; set; }

  [AllowHtml]
  public string pushMsgText { get; set; }

  [AllowHtml]
  public string subject { get; set; }

  public int snooze { get; set; }

  public bool autoAcknowledge { get; set; }

  public bool jointSnooze { get; set; }
}
