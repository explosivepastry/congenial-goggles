// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ChangeUserAPIModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class ChangeUserAPIModel
{
  [Required]
  [DisplayName("UserName")]
  public string UserName { get; set; }

  [Required]
  [DisplayName("New Username")]
  public string NewUsername { get; set; }
}
