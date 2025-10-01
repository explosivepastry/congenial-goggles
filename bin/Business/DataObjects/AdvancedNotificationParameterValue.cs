// Decompiled with JetBrains decompiler
// Type: Monnit.AdvancedNotificationParameterValue
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit;

[DBClass("AdvancedNotificationParameterValue")]
public class AdvancedNotificationParameterValue : BaseDBObject
{
  private long _AdvancedNotificationParameterValueID = long.MinValue;
  private long _AdvancedNotificationParameterID = long.MinValue;
  private long _NotificationID = long.MinValue;
  private string _ParameterValue = string.Empty;

  [DBProp("AdvancedNotificationParameterValueID", IsPrimaryKey = true)]
  public long AdvancedNotificationParameterValueID
  {
    get => this._AdvancedNotificationParameterValueID;
    set => this._AdvancedNotificationParameterValueID = value;
  }

  [DBForeignKey("AdvancedNotificationParameter", "AdvancedNotificationParameterID")]
  [DBProp("AdvancedNotificationParameterID", AllowNull = false)]
  public long AdvancedNotificationParameterID
  {
    get => this._AdvancedNotificationParameterID;
    set => this._AdvancedNotificationParameterID = value;
  }

  [DBForeignKey("Notification", "NotificationID")]
  [DBProp("NotificationID", AllowNull = true)]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("ParameterValue", MaxLength = 2000, AllowNull = true)]
  public string ParameterValue
  {
    get => this._ParameterValue;
    set
    {
      if (value == null)
        this._ParameterValue = string.Empty;
      else
        this._ParameterValue = value;
    }
  }

  public AdvancedNotificationParameterValue()
  {
  }

  public AdvancedNotificationParameterValue(
    long advancedNotificationParameterID,
    long notificationID,
    string parameterValue)
  {
    this.AdvancedNotificationParameterID = advancedNotificationParameterID;
    this.NotificationID = notificationID;
    this.ParameterValue = parameterValue;
  }

  public static AdvancedNotificationParameterValue Load(long ID)
  {
    return BaseDBObject.Load<AdvancedNotificationParameterValue>(ID);
  }

  private long AccountID
  {
    get
    {
      try
      {
        return Notification.Load(this.NotificationID).AccountID;
      }
      catch (Exception ex)
      {
        ex.Log($"AdvancedNotificationParameterValue.AccountID/ | NotificationID = {this.NotificationID}");
        return long.MinValue;
      }
    }
  }

  public object Value(string value, AdvancedNotificationParameter parameter)
  {
    try
    {
      switch (parameter.Type.ToString())
      {
        case "Monnit.CSNet":
          long ID1 = long.Parse(value);
          return this.AccountID == CSNet.Load(ID1).AccountID ? (object) ID1 : (object) null;
        case "Monnit.Gateway":
          long ID2 = long.Parse(value);
          return this.AccountID == CSNet.Load(Gateway.Load(ID2).CSNetID).AccountID ? (object) ID2 : (object) null;
        case "Monnit.Sensor":
          long ID3 = long.Parse(value);
          return this.AccountID == CSNet.Load(Sensor.Load(ID3).CSNetID).AccountID ? (object) ID3 : (object) null;
        case "System.Boolean":
        case "System.SByte":
          return (object) bool.Parse(value);
        case "System.DateTime":
          return (object) DateTime.Parse(value);
        case "System.Double":
          return (object) double.Parse(value);
        case "System.Int32":
        case "System.UInt32":
          return (object) int.Parse(value);
        case "System.Int64":
        case "System.UInt64":
          return (object) long.Parse(value);
        case "System.TimeSpan":
          double result1;
          if (double.TryParse(value, out result1))
            return (object) (new DateTime(1900, 1, 1) + new TimeSpan(0, Math.Floor(result1).ToInt(), Math.Round((result1 - Math.Floor(result1)) * 60.0, 0).ToInt()));
          DateTime result2;
          return DateTime.TryParse(value, out result2) ? (object) result2 : (object) DateTime.MinValue;
        case "eCompareType":
          return Enum.Parse(typeof (eCompareType), value);
        default:
          return (object) value;
      }
    }
    catch
    {
      return (object) null;
    }
  }

  public IDbDataParameter DBParameter
  {
    get
    {
      AdvancedNotificationParameter parameter = AdvancedNotificationParameter.Load(this.AdvancedNotificationParameterID);
      return Database.GetParameter(parameter.ParameterName, this.Value(this.ParameterValue, parameter));
    }
  }

  public static List<AdvancedNotificationParameterValue> LoadByNotificationID(long notificationID)
  {
    return BaseDBObject.LoadByForeignKey<AdvancedNotificationParameterValue>("NotificationID", (object) notificationID);
  }
}
