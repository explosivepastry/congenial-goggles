// Decompiled with JetBrains decompiler
// Type: Monnit.AccountThemeStyle
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("AccountThemeStyle")]
public class AccountThemeStyle : BaseDBObject
{
  private long _AccountThemeStyleID = long.MinValue;
  private long _AccountThemeStyleGroupID = long.MinValue;
  private long _AccountThemeStyleTypeID = long.MinValue;
  private string _Value = string.Empty;
  private byte[] _BinaryValue = (byte[]) null;

  [DBProp("AccountThemeStyleID", IsPrimaryKey = true)]
  public long AccountThemeStyleID
  {
    get => this._AccountThemeStyleID;
    set => this._AccountThemeStyleID = value;
  }

  [DBProp("AccountThemeStyleGroupID")]
  [DBForeignKey("AccountThemeStyleGroup", "AccountThemeStyleGroupID")]
  public long AccountThemeStyleGroupID
  {
    get => this._AccountThemeStyleGroupID;
    set => this._AccountThemeStyleGroupID = value;
  }

  [DBProp("AccountThemeStyleTypeID")]
  [DBForeignKey("AccountThemeStyleType", "AccountThemeStyleTypeID")]
  public long AccountThemeStyleTypeID
  {
    get => this._AccountThemeStyleTypeID;
    set => this._AccountThemeStyleTypeID = value;
  }

  [DBProp("Value", MaxLength = 500)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  [DBProp("BinaryValue", AllowNull = true)]
  public byte[] BinaryValue
  {
    get => this._BinaryValue == null ? new byte[0] : this._BinaryValue;
    set => this._BinaryValue = value;
  }

  public AccountThemeStyleType Type => AccountThemeStyleType.Load(this.AccountThemeStyleTypeID);

  public static AccountThemeStyle Load(long id) => BaseDBObject.Load<AccountThemeStyle>(id);

  public static List<AccountThemeStyle> LoadAll()
  {
    return BaseDBObject.LoadAll<AccountThemeStyle>().ToList<AccountThemeStyle>();
  }

  public static List<AccountThemeStyle> LoadByAccountThemeStyleGroupID(long accountThemeStyleGroupID)
  {
    return BaseDBObject.LoadByForeignKey<AccountThemeStyle>("AccountThemeStyleGroupID", (object) accountThemeStyleGroupID);
  }
}
