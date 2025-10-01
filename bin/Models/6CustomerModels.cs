// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CustomerFavoriteModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace iMonnit.Models;

public class CustomerFavoriteModel
{
  public CustomerFavorite CustomerFavorite { get; set; }

  public Sensor Sensor { get; set; }

  public Gateway Gateway { get; set; }

  public VisualMap VisualMap { get; set; }

  public Notification Rule { get; set; }

  public ReportSchedule ReportSchedule { get; set; }

  public Account Location { get; set; }

  public static List<CustomerFavoriteModel> LoadSensors(long customerID, long accountID)
  {
    return new CustomerFavoriteModel.LoadSensorsProc(customerID, accountID).Result;
  }

  public static List<CustomerFavoriteModel> LoadGateways(long customerID, long accountID)
  {
    return new CustomerFavoriteModel.LoadGatewaysProc(customerID, accountID).Result;
  }

  public static List<CustomerFavoriteModel> LoadVisualMaps(long customerID, long accountID)
  {
    return new CustomerFavoriteModel.LoadVisualMapsProc(customerID, accountID).Result;
  }

  public static List<CustomerFavoriteModel> LoadNotifications(long customerID, long accountID)
  {
    return new CustomerFavoriteModel.LoadNotificationsProc(customerID, accountID).Result;
  }

  public static List<CustomerFavoriteModel> LoadReportSchedules(long customerID, long accountID)
  {
    return new CustomerFavoriteModel.LoadReportSchedulesProc(customerID, accountID).Result;
  }

  public static List<CustomerFavoriteModel> LoadLocations(long customerID, long accountID)
  {
    return new CustomerFavoriteModel.LoadLocationsProc(customerID, accountID).Result;
  }

  [DBMethod("CustomerFavoriteModel_LoadSensors")]
  [DBMethodBody(DBMS.SqlServer, "\r\n        SELECT\r\n          *\r\n        FROM dbo.[CustomerFavorite] cf WITH (NOLOCK)\r\n\t        INNER JOIN dbo.[Sensor] s WITH (NOLOCK) ON s.SensorID = cf.SensorID\r\n        WHERE\tcf.CustomerID = @CustomerID \r\n\t        AND cf.AccountID = @AccountID\r\n\t        AND cf.SensorID IS NOT NULL\r\n        ORDER BY cf.OrderNum;\r\n        ")]
  private class LoadSensorsProc : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<CustomerFavoriteModel> Result { get; private set; }

    public LoadSensorsProc(long customerID, long accountID)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.Result = new List<CustomerFavoriteModel>();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Sensor sensor = new Sensor();
        sensor.Load(row);
        CustomerFavorite customerFavorite = new CustomerFavorite();
        customerFavorite.Load(row);
        this.Result.Add(new CustomerFavoriteModel()
        {
          CustomerFavorite = customerFavorite,
          Sensor = sensor
        });
      }
    }
  }

  [DBMethod("CustomerFavoriteModel_LoadGateways")]
  [DBMethodBody(DBMS.SqlServer, "\r\n        SELECT\r\n          *\r\n        FROM dbo.[CustomerFavorite] cf WITH (NOLOCK)\r\n        \tINNER JOIN dbo.[Gateway] g WITH (NOLOCK) ON g.GatewayID = cf.GatewayID\r\n        WHERE cf.CustomerID = @CustomerID AND cf.GatewayID IS NOT NULL\r\n        ORDER BY cf.OrderNum;\r\n        ")]
  private class LoadGatewaysProc : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<CustomerFavoriteModel> Result { get; private set; }

    public LoadGatewaysProc(long customerID, long accountID)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.Result = new List<CustomerFavoriteModel>();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Gateway gateway = new Gateway();
        gateway.Load(row);
        CustomerFavorite customerFavorite = new CustomerFavorite();
        customerFavorite.Load(row);
        this.Result.Add(new CustomerFavoriteModel()
        {
          CustomerFavorite = customerFavorite,
          Gateway = gateway
        });
      }
    }
  }

  [DBMethod("CustomerFavoriteModel_LoadVisualMaps")]
  [DBMethodBody(DBMS.SqlServer, "\r\n        SELECT\r\n          *\r\n        FROM dbo.[CustomerFavorite] cf \r\n\t        WITH (NOLOCK)\r\n        INNER JOIN dbo.[VisualMap] vm  \r\n\t        WITH (NOLOCK) \r\n\t        ON vm.VisualMapID = cf.VisualMapID\r\n        WHERE\tcf.CustomerID = @CustomerID \r\n\t        AND cf.AccountID = @AccountID\r\n\t        AND cf.VisualMapID IS NOT NULL\r\n        ORDER BY cf.OrderNum;\r\n        ")]
  private class LoadVisualMapsProc : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<CustomerFavoriteModel> Result { get; private set; }

    public LoadVisualMapsProc(long customerID, long accountID)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.Result = new List<CustomerFavoriteModel>();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        VisualMap visualMap = new VisualMap();
        visualMap.Load(row);
        CustomerFavorite customerFavorite = new CustomerFavorite();
        customerFavorite.Load(row);
        this.Result.Add(new CustomerFavoriteModel()
        {
          CustomerFavorite = customerFavorite,
          VisualMap = visualMap
        });
      }
    }
  }

  [DBMethod("CustomerFavoriteModel_LoadNotifications")]
  [DBMethodBody(DBMS.SqlServer, "\r\n        SELECT\r\n          *\r\n        FROM dbo.[CustomerFavorite] cf WITH (NOLOCK)\r\n        \tINNER JOIN dbo.[Notification] n WITH (NOLOCK) ON n.NotificationID = cf.NotificationID\r\n        WHERE cf.CustomerID = @CustomerID AND cf.NotificationID IS NOT NULL\r\n        ORDER BY cf.OrderNum;\r\n        ")]
  private class LoadNotificationsProc : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<CustomerFavoriteModel> Result { get; private set; }

    public LoadNotificationsProc(long customerID, long accountID)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.Result = new List<CustomerFavoriteModel>();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Notification notification = new Notification();
        notification.Load(row);
        CustomerFavorite customerFavorite = new CustomerFavorite();
        customerFavorite.Load(row);
        this.Result.Add(new CustomerFavoriteModel()
        {
          CustomerFavorite = customerFavorite,
          Rule = notification
        });
      }
    }
  }

  [DBMethod("CustomerFavoriteModel_LoadReportSchedules")]
  [DBMethodBody(DBMS.SqlServer, "\r\n        SELECT\r\n          *\r\n        FROM dbo.[CustomerFavorite] cf WITH (NOLOCK)\r\n        INNER JOIN dbo.[ReportSchedule] rs \r\n\t        WITH (NOLOCK) \r\n\t        ON rs.ReportScheduleID = cf.ReportScheduleID\r\n        WHERE\tcf.CustomerID = @CustomerID \r\n\t        AND cf.ReportScheduleID IS NOT NULL\r\n        ORDER BY cf.OrderNum;\r\n        ")]
  private class LoadReportSchedulesProc : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<CustomerFavoriteModel> Result { get; private set; }

    public LoadReportSchedulesProc(long customerID, long accountID)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.Result = new List<CustomerFavoriteModel>();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        ReportSchedule reportSchedule = new ReportSchedule();
        reportSchedule.Load(row);
        CustomerFavorite customerFavorite = new CustomerFavorite();
        customerFavorite.Load(row);
        this.Result.Add(new CustomerFavoriteModel()
        {
          CustomerFavorite = customerFavorite,
          ReportSchedule = reportSchedule
        });
      }
    }
  }

  [DBMethod("CustomerFavoriteModel_LoadLocations")]
  [DBMethodBody(DBMS.SqlServer, "\r\n        SELECT\r\n          *\r\n        FROM dbo.[CustomerFavorite] cf WITH (NOLOCK)\r\n        INNER JOIN dbo.[Location] a \r\n\t        WITH (NOLOCK) \r\n\t        ON cf.LocationID = a.AccountID\r\n        WHERE cf.CustomerID = @CustomerID\r\n\t        AND cf.AccountID = @AccountID\r\n\t        AND cf.LocationID IS NOT NULL\r\n        ORDER BY cf.OrderNum;\r\n        ")]
  private class LoadLocationsProc : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public List<CustomerFavoriteModel> Result { get; private set; }

    public LoadLocationsProc(long customerID, long accountID)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.Result = new List<CustomerFavoriteModel>();
      DataTable dataTable = this.ToDataTable();
      if (dataTable == null || dataTable.Rows.Count <= 0)
        return;
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        Account account = new Account();
        account.Load(row);
        account.AccountID = row["LocationID"].ToLong();
        CustomerFavorite customerFavorite = new CustomerFavorite();
        customerFavorite.Load(row);
        this.Result.Add(new CustomerFavoriteModel()
        {
          CustomerFavorite = customerFavorite,
          Location = account
        });
      }
    }
  }
}
