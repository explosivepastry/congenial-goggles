// Decompiled with JetBrains decompiler
// Type: Monnit.AdvancedNotificationParameterOption
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AdvancedNotificationParameterOption")]
public class AdvancedNotificationParameterOption : BaseDBObject
{
  private long _AdvancedNotificationParameterOptionID = long.MinValue;
  private long _AdvancedNotificationParameterID = long.MinValue;
  private string _Display = string.Empty;
  private string _Value = string.Empty;

  [DBProp("AdvancedNotificationParameterOptionID", IsPrimaryKey = true)]
  public long AdvancedNotificationParameterOptionID
  {
    get => this._AdvancedNotificationParameterOptionID;
    set => this._AdvancedNotificationParameterOptionID = value;
  }

  [DBProp("AdvancedNotificationParameterID")]
  [DBForeignKey("AdvancedNotificationParameter", "AdvancedNotificationParameterID")]
  public long AdvancedNotificationParameterID
  {
    get => this._AdvancedNotificationParameterID;
    set => this._AdvancedNotificationParameterID = value;
  }

  [DBProp("Display", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Display
  {
    get => this._Display;
    set => this._Display = value;
  }

  [DBProp("Value", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public static AdvancedNotificationParameterOption Load(long id)
  {
    return BaseDBObject.Load<AdvancedNotificationParameterOption>(id);
  }

  public static List<AdvancedNotificationParameterOption> LoadByParameter(
    long advancedNotificationParameterID)
  {
    return BaseDBObject.LoadByForeignKey<AdvancedNotificationParameterOption>("AdvancedNotificationParameterID", (object) advancedNotificationParameterID);
  }
}
