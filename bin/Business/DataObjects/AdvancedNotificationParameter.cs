// Decompiled with JetBrains decompiler
// Type: Monnit.AdvancedNotificationParameter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AdvancedNotificationParameter")]
public class AdvancedNotificationParameter : BaseDBObject
{
  private long _AdvancedNotificationParameterID = long.MinValue;
  private long _AdvancedNotificationID = long.MinValue;
  private string _ParameterName = string.Empty;
  private string _ParameterDisplayName = string.Empty;
  private string _ParameterType = string.Empty;
  private string _Description = string.Empty;
  private bool _Required = true;
  private int _DisplayOrder = 0;
  private string _Units = string.Empty;
  private List<AdvancedNotificationParameterOption> _Options;

  [DBProp("AdvancedNotificationParameterID", IsPrimaryKey = true)]
  public long AdvancedNotificationParameterID
  {
    get => this._AdvancedNotificationParameterID;
    set => this._AdvancedNotificationParameterID = value;
  }

  [DBForeignKey("AdvancedNotification", "AdvancedNotificationID")]
  [DBProp("AdvancedNotificationID", AllowNull = false)]
  public long AdvancedNotificationID
  {
    get => this._AdvancedNotificationID;
    set => this._AdvancedNotificationID = value;
  }

  [DBProp("ParameterName", MaxLength = 200, AllowNull = false)]
  public string ParameterName
  {
    get => this._ParameterName;
    set
    {
      if (value == null)
        this._ParameterName = string.Empty;
      else
        this._ParameterName = value;
    }
  }

  [DBProp("ParameterDisplayName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ParameterDisplayName
  {
    get
    {
      return string.IsNullOrEmpty(this._ParameterDisplayName) ? this.ParameterName.ToStringSafe().Replace("_", " ") : this._ParameterDisplayName;
    }
    set => this._ParameterDisplayName = value;
  }

  [DBProp("ParameterType", MaxLength = 200, AllowNull = false)]
  public string ParameterType
  {
    get => this._ParameterType;
    set
    {
      if (value == null)
        this._ParameterType = string.Empty;
      else
        this._ParameterType = value;
    }
  }

  [DBProp("Description", MaxLength = 2000)]
  public string Description
  {
    get => this._Description;
    set
    {
      if (value == null)
        this._Description = string.Empty;
      else
        this._Description = value;
    }
  }

  [DBProp("Required", DefaultValue = true, AllowNull = false)]
  public bool Required
  {
    get => this._Required;
    set => this._Required = value;
  }

  [DBProp("DisplayOrder", DefaultValue = 0)]
  public int DisplayOrder
  {
    get => this._DisplayOrder;
    set => this._DisplayOrder = value;
  }

  [DBProp("Units", MaxLength = 200, AllowNull = true)]
  public string Units
  {
    get => this._Units;
    set
    {
      if (value == null)
        this._Units = string.Empty;
      else
        this._Units = value;
    }
  }

  public Type Type => AdvancedNotification.TypeFromString(this.ParameterType);

  public List<AdvancedNotificationParameterOption> Options
  {
    get
    {
      if (this._Options == null)
        this._Options = AdvancedNotificationParameterOption.LoadByParameter(this.AdvancedNotificationParameterID);
      return this._Options;
    }
  }

  public static AdvancedNotificationParameter Load(long ID)
  {
    return BaseDBObject.Load<AdvancedNotificationParameter>(ID);
  }

  public static List<AdvancedNotificationParameter> LoadByAdvancedNotificationID(long id)
  {
    return BaseDBObject.LoadByForeignKey<AdvancedNotificationParameter>("AdvancedNotificationID", (object) id);
  }

  public bool IsValid(AdvancedNotificationParameterValue obj, string value)
  {
    return string.IsNullOrWhiteSpace(value) ? !this.Required : obj.Value(value, this) != null;
  }
}
