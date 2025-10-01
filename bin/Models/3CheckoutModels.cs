// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.StoreAccountInfoModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.ComponentModel;

#nullable disable
namespace iMonnit.Models;

public class StoreAccountInfoModel
{
  [DisplayName("AccountID")]
  public long AccountID { get; set; }

  [DisplayName("User Name")]
  public string UserName { get; set; }

  [DisplayName("Store LinkGuid")]
  public Guid StoreLinkGuid { get; set; }

  [DisplayName("Store UserID")]
  public long StoreUserID { get; set; }
}
