// Decompiled with JetBrains decompiler
// Type: Monnit.ReportParameter
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ReportParameter")]
public class ReportParameter : BaseDBObject
{
  private long _ReportParameterID = long.MinValue;
  private long _ReportQueryID = long.MinValue;
  private long _ReportParameterTypeID = long.MinValue;
  private string _ParamName = string.Empty;
  private string _LabelText = string.Empty;
  private string _HelpText = string.Empty;
  private string _DefaultValue = string.Empty;
  private string _PredefinedValues = string.Empty;
  private int _SortOrder = 0;
  private ReportParameterType _Type = (ReportParameterType) null;

  [DBProp("ReportParameterID", IsPrimaryKey = true)]
  public long ReportParameterID
  {
    get => this._ReportParameterID;
    set => this._ReportParameterID = value;
  }

  [DBProp("ReportQueryID")]
  [DBForeignKey("ReportQuery", "ReportQueryID")]
  public long ReportQueryID
  {
    get => this._ReportQueryID;
    set => this._ReportQueryID = value;
  }

  [DBProp("ReportParameterTypeID")]
  [DBForeignKey("ReportParameterType", "ReportParameterTypeID")]
  public long ReportParameterTypeID
  {
    get => this._ReportParameterTypeID;
    set => this._ReportParameterTypeID = value;
  }

  [DBProp("ParamName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ParamName
  {
    get => this._ParamName;
    set => this._ParamName = value;
  }

  [DBProp("LabelText", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string LabelText
  {
    get => this._LabelText;
    set => this._LabelText = value;
  }

  [DBProp("HelpText", MaxLength = 2000, AllowNull = true)]
  public string HelpText
  {
    get => this._HelpText;
    set => this._HelpText = value;
  }

  [DBProp("DefaultValue", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [DBProp("PredefinedValues", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string PredefinedValues
  {
    get => this._PredefinedValues;
    set => this._PredefinedValues = value;
  }

  [DBProp("SortOrder")]
  public int SortOrder
  {
    get => this._SortOrder;
    set => this._SortOrder = value;
  }

  public ReportParameterType Type
  {
    get
    {
      if (this._Type == null)
        this._Type = ReportParameterType.Load(this.ReportParameterTypeID);
      return this._Type;
    }
  }

  public object GetValue(ReportSchedule report)
  {
    return this.GetValue(report.FindParameterValue(this.ReportParameterID));
  }

  public object GetValue(string value)
  {
    string lower = this.Type.ValueType.ToLower();
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(lower))
    {
      case 398550328:
        if (lower == "string")
          goto default;
        goto default;
      case 1623908912:
        if (lower == "bit")
          break;
        goto default;
      case 1710517951:
        if (lower == "boolean")
          break;
        goto default;
      case 2515107422:
        if (lower == "int")
        {
          try
          {
            return (object) Convert.ToInt32(value);
          }
          catch
          {
            goto label_23;
          }
        }
        else
          goto default;
      case 2699759368:
        if (lower == "double")
        {
          try
          {
            return (object) Convert.ToDouble(value);
          }
          catch
          {
            goto label_23;
          }
        }
        else
          goto default;
      case 3270303571:
        if (lower == "long")
        {
          try
          {
            return (object) Convert.ToInt64(value);
          }
          catch
          {
            goto label_23;
          }
        }
        else
          goto default;
      case 3365180733:
        if (lower == "bool")
          break;
        goto default;
      case 3437915536:
        if (lower == "datetime")
        {
          try
          {
            return (object) Convert.ToDateTime(value);
          }
          catch
          {
            goto label_23;
          }
        }
        else
          goto default;
      default:
        return (object) value;
    }
    try
    {
      return value == "1" || value.ToLower() == "true" || value.ToLower() == "yes" || value.ToLower() == "on" ? (object) true : (object) false;
    }
    catch
    {
    }
label_23:
    return (object) null;
  }

  public static ReportParameter Load(long id) => BaseDBObject.Load<ReportParameter>(id);

  public static List<ReportParameter> LoadByReportQuery(long reportQueryID)
  {
    return BaseDBObject.LoadByForeignKey<ReportParameter>("ReportQueryID", (object) reportQueryID);
  }
}
