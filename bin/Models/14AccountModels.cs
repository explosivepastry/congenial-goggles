// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.AccountThemeReleaseNoteModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.Models;

public class AccountThemeReleaseNoteModel
{
  public AccountThemeReleaseNoteModel()
  {
  }

  public AccountThemeReleaseNoteModel(ReleaseNote rn, long themeid)
  {
    this.ThemeName = AccountTheme.Load(themeid).Theme;
    this.Version = rn.Version;
    this.Description = rn.Description;
    this.Note = rn.Note;
    this.ReleaseDate = rn.ReleaseDate;
    try
    {
      this.OverriddenNote = rn.dic[themeid].OverriddenNote;
    }
    catch
    {
      this.OverriddenNote = string.Empty;
    }
    try
    {
      this.IsHidden = rn.dic[themeid].IsHidden;
    }
    catch
    {
      this.IsHidden = false;
    }
    this.ReleaseNoteID = rn.ReleaseNoteID;
  }

  public string OverriddenNote { get; set; }

  public bool IsHidden { get; set; }

  public string Version { get; set; }

  public string Description { get; set; }

  public string Note { get; set; }

  public DateTime ReleaseDate { get; set; }

  public string ThemeName { get; set; }

  public long ReleaseNoteID { get; set; }
}
