// Decompiled with JetBrains decompiler
// Type: Monnit.ActionToExecute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("ActionToExecute")]
public class ActionToExecute : BaseDBObject
{
  private long _ActionToExecuteID = long.MinValue;
  private string _Name = string.Empty;
  private string _Description = string.Empty;

  [DBProp("ActionToExecuteID", IsPrimaryKey = true)]
  public long ActionToExecuteID
  {
    get => this._ActionToExecuteID;
    set => this._ActionToExecuteID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Description", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  public static ActionToExecute Find(string action)
  {
    return ActionToExecute.LoadAll().Where<ActionToExecute>((Func<ActionToExecute, bool>) (m => m.Name.ToLower() == action.ToLower())).FirstOrDefault<ActionToExecute>();
  }

  public static ActionToExecute Load(long id) => BaseDBObject.Load<ActionToExecute>(id);

  public static List<ActionToExecute> LoadAll() => BaseDBObject.LoadAll<ActionToExecute>();
}
