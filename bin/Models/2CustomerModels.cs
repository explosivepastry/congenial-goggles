// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.LogOnForgotModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class LogOnForgotModel
{
  [Required(ErrorMessage = "Email address is required")]
  [DisplayName("Email Address")]
  [System.ComponentModel.DataAnnotations.EmailAddress(ErrorMessage = "Not a valid email address")]
  public string EmailAddress { get; set; }

  public string VerificationCode { get; set; }

  public bool SendCodePage { get; set; }
}
