// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.DisplayMessageModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class DisplayMessageModel : BaseDBObject
{
  public DisplayMessageModel()
  {
  }

  public DisplayMessageModel(string title, string text)
  {
    this.Title = title;
    this.Text = text;
  }

  [Required]
  [DBProp("Title")]
  public string Title { get; set; }

  [DBProp("Text")]
  public string Text { get; set; }
}
