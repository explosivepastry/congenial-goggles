// Decompiled with JetBrains decompiler
// Type: Monnit.AdvancedNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("AdvancedNotification")]
public class AdvancedNotification : BaseDBObject
{
  private long _AdvancedNotificationID = long.MinValue;
  private string _Name = string.Empty;
  private string _Sql = string.Empty;
  private long _AccountID = long.MinValue;
  private bool _AvailableGlobaly = false;
  private string _ReturnType = string.Empty;
  private string _ReadingFormat = string.Empty;
  public eAdvancedNotificationType _AdvancedNotificationType = eAdvancedNotificationType.Everyone;
  private string _HelpText = string.Empty;
  private bool _HasSensorList = true;
  private bool _HasGatewayList = true;
  private bool _UseDatums = false;
  private eDatumType _eDatumType = eDatumType.Error;
  private string _DeviceTypeFilter = string.Empty;

  [DBProp("AdvancedNotificationID", IsPrimaryKey = true)]
  public long AdvancedNotificationID
  {
    get => this._AdvancedNotificationID;
    set => this._AdvancedNotificationID = value;
  }

  [DBProp("Name", MaxLength = 200, AllowNull = false)]
  public string Name
  {
    get => this._Name;
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  [DBProp("Sql", MaxLength = 8000, AllowNull = false)]
  public string Sql
  {
    get => this._Sql;
    set
    {
      if (value == null)
        this._Sql = string.Empty;
      else
        this._Sql = value;
    }
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = true)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("AvailableGlobaly", AllowNull = false)]
  public bool AvailableGlobaly
  {
    get => this._AvailableGlobaly;
    set => this._AvailableGlobaly = value;
  }

  [DBProp("ReturnType", MaxLength = 200, AllowNull = false)]
  public string ReturnType
  {
    get => this._ReturnType;
    set
    {
      if (value == null)
        this._ReturnType = string.Empty;
      else
        this._ReturnType = value;
    }
  }

  [DBProp("ReadingFormat", MaxLength = 200, AllowNull = false)]
  public string ReadingFormat
  {
    get => this._ReadingFormat;
    set
    {
      if (value == null)
        this._ReadingFormat = string.Empty;
      else
        this._ReadingFormat = value;
    }
  }

  [DBProp("eAdvancedNotificationType")]
  public eAdvancedNotificationType AdvancedNotificationType
  {
    get => this._AdvancedNotificationType;
    set => this._AdvancedNotificationType = value;
  }

  [DBProp("HelpText", MaxLength = 2147483647 /*0x7FFFFFFF*/, AllowNull = true)]
  public string HelpText
  {
    get => this._HelpText;
    set => this._HelpText = value;
  }

  [DBProp("HasSensorList", AllowNull = true)]
  public bool HasSensorList
  {
    get => this._HasSensorList;
    set => this._HasSensorList = value;
  }

  [DBProp("HasGatewayList", AllowNull = true)]
  public bool HasGatewayList
  {
    get => this._HasGatewayList;
    set => this._HasGatewayList = value;
  }

  [DBProp("UseDatums", AllowNull = false, DefaultValue = false)]
  public bool UseDatums
  {
    get => this._UseDatums;
    set => this._UseDatums = value;
  }

  [DBProp("eDatumType", AllowNull = true)]
  public eDatumType eDatumType
  {
    get => this._eDatumType;
    set => this._eDatumType = value;
  }

  [DBProp("DeviceTypeFilter", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string DeviceTypeFilter
  {
    get => this._DeviceTypeFilter;
    set => this._DeviceTypeFilter = value;
  }

  public static AdvancedNotification Load(long ID) => BaseDBObject.Load<AdvancedNotification>(ID);

  public static Type TypeFromString(string type)
  {
    switch (type.ToLower())
    {
      case "bool":
      case "boolean":
      case "system.boolean":
      case "system.sbyte":
        return typeof (bool);
      case "csnet":
      case "csnetid":
      case "network":
      case "networkid":
        return typeof (CSNet);
      case "datetime":
      case "system.datetime":
        return typeof (DateTime);
      case "double":
      case "system.double":
        return typeof (double);
      case "ecomparetype":
        return typeof (eCompareType);
      case "gateway":
      case "gatewayid":
        return typeof (Gateway);
      case "int":
      case "int32":
      case "system.int32":
      case "system.uint32":
        return typeof (int);
      case "int64":
      case "long":
      case "system.int64":
      case "system.uint64":
        return typeof (long);
      case "option":
      case "select":
        return typeof (AdvancedNotificationParameterOption);
      case "sensor":
      case "sensorid":
        return typeof (Sensor);
      case "string":
      case "system.string":
        return typeof (string);
      case "system.timespan":
      case "timespan":
        return typeof (TimeSpan);
      default:
        return typeof (string);
    }
  }

  public bool Evaluate(
    Notification objNotification,
    long sensorID,
    Guid dataMessageGUID,
    out string AdvancedNotificationString)
  {
    IDbCommand dbCommand = Database.GetDBCommand(this.Sql);
    foreach (AdvancedNotificationParameterValue notificationParameterValue in AdvancedNotificationParameterValue.LoadByNotificationID(objNotification.NotificationID))
      dbCommand.Parameters.Add((object) notificationParameterValue.DBParameter);
    dbCommand.Parameters.Add((object) Database.GetParameter("NotificationID", (object) objNotification.NotificationID));
    IDataParameter dataParameter = (IDataParameter) null;
    foreach (IDataParameter parameter in (IEnumerable) dbCommand.Parameters)
    {
      if (parameter.ParameterName.ToLower() == "sensorid")
        dataParameter = parameter;
    }
    if (sensorID > 0L)
    {
      if (dataParameter == null)
        dbCommand.Parameters.Add((object) Database.GetParameter("SensorID", (object) sensorID));
      else
        dataParameter.Value = (object) sensorID;
    }
    if (dataMessageGUID != Guid.Empty)
      dbCommand.Parameters.Add((object) Database.GetParameter("DataMessageGUID", (object) dataMessageGUID));
    DataSet dataSet = Database.ExecuteQuery(dbCommand);
    if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
    {
      DataRow row = dataSet.Tables[0].Rows[0];
      AdvancedNotificationString = string.Format(this.ReadingFormat, (object) Database.FromDBValue(row[1], AdvancedNotification.TypeFromString(this.ReturnType)).ToStringSafe());
      return row[0].ToBool();
    }
    AdvancedNotificationString = string.Empty;
    return false;
  }

  public bool CanAdd(object obj)
  {
    switch (obj)
    {
      case Sensor sensor:
        if (this.HasSensorList)
        {
          int num;
          if (!string.IsNullOrEmpty(this.DeviceTypeFilter))
            num = ((IEnumerable<string>) this.DeviceTypeFilter.Split('|')).Contains<string>(sensor.ApplicationID.ToString()) ? 1 : 0;
          else
            num = 1;
          if (num != 0)
            return true;
          break;
        }
        if (this.UseDatums && (this.eDatumType == eDatumType.Error || sensor.GetDatumTypes().Contains(this.eDatumType)))
          return true;
        break;
      case Gateway gateway:
        if (this.HasGatewayList)
        {
          int num;
          if (!string.IsNullOrEmpty(this.DeviceTypeFilter))
            num = ((IEnumerable<string>) this.DeviceTypeFilter.Split('|')).Contains<string>(gateway.GatewayTypeID.ToString()) ? 1 : 0;
          else
            num = 1;
          if (num != 0)
            return true;
          break;
        }
        break;
    }
    return false;
  }

  public static List<AdvancedNotification> LoadByAccountID(long accountID)
  {
    return new Monnit.Data.AdvancedNotification.LoadByAccountID(accountID).Result;
  }
}
