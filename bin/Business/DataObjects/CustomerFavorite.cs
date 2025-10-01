// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerFavorite
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("CustomerFavorite")]
public class CustomerFavorite : BaseDBObject
{
  private long _CustomerFavoriteID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _OrderNum = long.MinValue;
  private long _CustomerID = long.MinValue;
  private long _SensorID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private long _VisualMapID = long.MinValue;
  private long _NotificationID = long.MinValue;
  private long _ReportScheduleID = long.MinValue;
  private long _LocationID = long.MinValue;

  [DBProp("CustomerFavoriteID", IsPrimaryKey = true)]
  public long CustomerFavoriteID
  {
    get => this._CustomerFavoriteID;
    set => this._CustomerFavoriteID = value;
  }

  [DBProp("AccountID", AllowNull = false)]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("OrderNum", AllowNull = true)]
  public long OrderNum
  {
    get => this._OrderNum;
    set => this._OrderNum = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("SensorID", AllowNull = true)]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("GatewayID", AllowNull = true)]
  [DBForeignKey("Gateway", "GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("VisualMapID", AllowNull = true)]
  [DBForeignKey("VisualMap", "VisualMapID")]
  public long VisualMapID
  {
    get => this._VisualMapID;
    set => this._VisualMapID = value;
  }

  [DBProp("NotificationID", AllowNull = true)]
  [DBForeignKey("Notification", "NotificationID")]
  public long NotificationID
  {
    get => this._NotificationID;
    set => this._NotificationID = value;
  }

  [DBProp("ReportScheduleID", AllowNull = true)]
  [DBForeignKey("ReportSchedule", "ReportScheduleID")]
  public long ReportScheduleID
  {
    get => this._ReportScheduleID;
    set => this._ReportScheduleID = value;
  }

  [DBProp("LocationID", AllowNull = true)]
  [DBForeignKey("Account", "AccountID")]
  public long LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  public static CustomerFavorite Load(long ID) => BaseDBObject.Load<CustomerFavorite>(ID);

  public static List<CustomerFavorite> LoadByCustomerIDAndAccountID(long customerID, long accountID)
  {
    return new Monnit.Data.CustomerFavorite.LoadByCustomerIDAndAccountID(customerID, accountID).Result;
  }

  public static CustomerFavorite LoadFavoriteSensor(long customerID, long accountID, long sensorID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomerFavorite>(new string[3]
    {
      "CustomerID",
      "AccountID",
      "SensorID"
    }, (object[]) new string[3]
    {
      customerID.ToString(),
      accountID.ToString(),
      sensorID.ToString()
    }).FirstOrDefault<CustomerFavorite>();
  }

  public static CustomerFavorite LoadFavoriteGateway(
    long customerID,
    long accountID,
    long gatewayID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomerFavorite>(new string[3]
    {
      "CustomerID",
      "AccountID",
      "GatewayID"
    }, (object[]) new string[3]
    {
      customerID.ToString(),
      accountID.ToString(),
      gatewayID.ToString()
    }).FirstOrDefault<CustomerFavorite>();
  }

  public static CustomerFavorite LoadFavoriteMap(long customerID, long accountID, long visualMapID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomerFavorite>(new string[3]
    {
      "CustomerID",
      "AccountID",
      "VisualMapID"
    }, (object[]) new string[3]
    {
      customerID.ToString(),
      accountID.ToString(),
      visualMapID.ToString()
    }).FirstOrDefault<CustomerFavorite>();
  }

  public static CustomerFavorite LoadFavoriteNotification(
    long customerID,
    long accountID,
    long notificationID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomerFavorite>(new string[3]
    {
      "CustomerID",
      "AccountID",
      "NotificationID"
    }, (object[]) new string[3]
    {
      customerID.ToString(),
      accountID.ToString(),
      notificationID.ToString()
    }).FirstOrDefault<CustomerFavorite>();
  }

  public static CustomerFavorite LoadFavoriteReport(
    long customerID,
    long accountID,
    long reportScheduleID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomerFavorite>(new string[3]
    {
      "CustomerID",
      "AccountID",
      "ReportScheduleID"
    }, (object[]) new string[3]
    {
      customerID.ToString(),
      accountID.ToString(),
      reportScheduleID.ToString()
    }).FirstOrDefault<CustomerFavorite>();
  }

  public static CustomerFavorite LoadFavoriteLocation(
    long customerID,
    long accountID,
    long locationID)
  {
    return BaseDBObject.LoadByForeignKeys<CustomerFavorite>(new string[3]
    {
      "CustomerID",
      "AccountID",
      "LocationID"
    }, (object[]) new string[3]
    {
      customerID.ToString(),
      accountID.ToString(),
      locationID.ToString()
    }).FirstOrDefault<CustomerFavorite>();
  }

  public static List<CustomerFavorite> LoadByGatewayID(long gatewayID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerFavorite>("GatewayID", (object) gatewayID);
  }

  public static List<CustomerFavorite> LoadBySensorID(long sensorID)
  {
    return BaseDBObject.LoadByForeignKey<CustomerFavorite>("SensorID", (object) sensorID);
  }
}
