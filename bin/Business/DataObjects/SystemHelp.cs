// Decompiled with JetBrains decompiler
// Type: Monnit.SystemHelp
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SystemHelp")]
public class SystemHelp : BaseDBObject
{
  private long _SystemHelpID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private string _Type = string.Empty;
  private DateTime _CreateDate = DateTime.MinValue;

  [DBProp("SystemHelpID", IsPrimaryKey = true)]
  public long SystemHelpID
  {
    get => this._SystemHelpID;
    set => this._SystemHelpID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("Type", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  public static SystemHelp Load(long id) => BaseDBObject.Load<SystemHelp>(id);

  public static List<SystemHelp> LoadByAccount(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<SystemHelp>("AccountID", (object) accountID);
  }
}
