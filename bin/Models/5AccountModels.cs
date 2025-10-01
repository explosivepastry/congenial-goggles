// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.UpdateEulas
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.ComponentModel;

#nullable disable
namespace iMonnit.Models;

public class UpdateEulas
{
  public DateTime _EULAUpdateDate = DateTime.MinValue;

  [DisplayName("EULA Update")]
  public bool EULAUpdate { get; set; }

  public DateTime EULAUpdateDate
  {
    get => this._EULAUpdateDate;
    set => this._EULAUpdateDate = value;
  }
}
