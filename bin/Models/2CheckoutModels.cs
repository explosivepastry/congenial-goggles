// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.StoreLoginModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class StoreLoginModel
{
  public Account account { get; set; }

  [DisplayName("User Name")]
  public string UserName { get; set; }

  [DataType(DataType.Password)]
  [DisplayName("Password")]
  public string Password { get; set; }

  [DisplayName("Email Address")]
  public string EmailAddress { get; set; }
}
