// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.AssignObjectModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.ControllerBase;
using Monnit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace iMonnit.Models;

public class AssignObjectModel
{
  private List<CSNet> _Networks = (List<CSNet>) null;
  [Required]
  private string _Code = (string) null;

  public long AccountID { get; set; }

  public List<CSNet> Networks
  {
    get
    {
      if (this._Networks == null || this._Networks.Count == 0)
        this._Networks = CSNetControllerBase.GetListOfNetworksUserCanSee(new long?(MonnitSession.CurrentCustomer.AccountID));
      return this._Networks;
    }
  }

  public long NetworkID { get; set; }

  [Required]
  public long ObjectID { get; set; }

  public string Code
  {
    get => string.IsNullOrEmpty(this._Code) ? string.Empty : this._Code.ToUpper();
    set => this._Code = value;
  }
}
