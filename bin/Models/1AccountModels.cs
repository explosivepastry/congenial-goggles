// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.AdminResetPasswordModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

[PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "The new password and confirmation password do not match.")]
public class AdminResetPasswordModel
{
  [Required]
  [DataType(DataType.Password)]
  [DisplayName("New Password")]
  public string NewPassword { get; set; }

  [Required]
  [DataType(DataType.Password)]
  [DisplayName("Confirm New Password")]
  public string ConfirmPassword { get; set; }

  public long CustomerID { get; set; }
}
