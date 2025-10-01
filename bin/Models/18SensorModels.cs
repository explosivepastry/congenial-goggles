// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ApplicationTypeShort
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class ApplicationTypeShort : BaseDBObject
{
  private long _MonnitApplicationID = long.MinValue;
  private string _ApplicationName = string.Empty;

  [DBProp("ApplicationID")]
  public long MonnitApplicationID
  {
    get => this._MonnitApplicationID;
    set => this._MonnitApplicationID = value;
  }

  public long ApplicationID
  {
    get => this.MonnitApplicationID;
    set => this.MonnitApplicationID = value;
  }

  [DBProp("ApplicationName", MaxLength = 255 /*0xFF*/)]
  public string ApplicationName
  {
    get => this._ApplicationName;
    set
    {
      if (value == null)
        this._ApplicationName = string.Empty;
      else
        this._ApplicationName = value;
    }
  }

  public static List<ApplicationTypeShort> LoadAllByAccountID(long accountID)
  {
    return new Data.ApplicationTypeShort.LoadAllByAccountID(accountID).Result;
  }
}
