// Decompiled with JetBrains decompiler
// Type: iMonnit.ControllerBase.CSNetControllerBase
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Monnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace iMonnit.ControllerBase;

public class CSNetControllerBase : ThemeController
{
  public static List<CSNet> GetListOfNetworksUserCanSee(long? id)
  {
    List<CSNet> networksUserCanSee = new List<CSNet>();
    if (MonnitSession.CurrentCustomer != null)
    {
      long accountID = id ?? MonnitSession.CurrentCustomer.AccountID;
      networksUserCanSee = CSNet.LoadByAccountID(accountID).Where<CSNet>((Func<CSNet, bool>) (network => MonnitSession.CurrentCustomer.CanSeeNetwork(network.CSNetID) || MonnitSession.CurrentCustomer.AccountID != accountID)).OrderBy<CSNet, string>((Func<CSNet, string>) (network => network.Name)).ToList<CSNet>();
    }
    return networksUserCanSee;
  }

  public static Gateway LookUpWifiGateway(AssignObjectModel model, Sensor sens)
  {
    return Gateway.LoadBySensorID(sens.SensorID) ?? XDocument.Load(string.Format("{2}/xml/LookUpWifiGateway?SensorID={0}&checkDigit={1}", (object) sens.SensorID, (object) model.Code, (object) ConfigData.FindValue("LookUpHost"))).Descendants((XName) "APILookUpGateway").Select<XElement, Gateway>((Func<XElement, Gateway>) (g => new Gateway()
    {
      GatewayID = g.Attribute((XName) "GatewayID").Value.ToLong(),
      Name = g.Attribute((XName) "GatewayName").Value,
      RadioBand = g.Attribute((XName) "RadioBand").Value,
      APNFirmwareVersion = g.Attribute((XName) "APNFirmwareVersion").Value,
      GatewayFirmwareVersion = g.Attribute((XName) "GatewayFirmwareVersion").Value,
      GatewayTypeID = g.Attribute((XName) "GatewayTypeID").Value.ToLong(),
      MacAddress = g.Attribute((XName) "MacAddress").Value,
      CSNetID = model.NetworkID,
      SensorID = g.Attribute((XName) "SensorID").Value.ToLong(),
      GenerationType = g.Attribute((XName) "GenerationType").Value,
      PowerSourceID = g.Attribute((XName) "PowerSourceID") == null ? 3L : g.Attribute((XName) "PowerSourceID").Value.ToLong(),
      SKU = g.Attribute((XName) "SKU").Value
    })).FirstOrDefault<Gateway>();
  }

  public static Gateway LookUpGateway(long accountID, long gatewayID, string checkDigit)
  {
    return CSNetControllerBase.LookUpGateway(new AssignObjectModel()
    {
      AccountID = accountID,
      ObjectID = gatewayID,
      Code = checkDigit
    });
  }

  public static Gateway LookUpGateway(AssignObjectModel model)
  {
    return Gateway.Load(model.ObjectID) ?? XDocument.Load(string.Format("{2}/xml/LookUpGateway?GatewayID={0}&checkDigit={1}", (object) model.ObjectID, (object) model.Code, (object) ConfigData.FindValue("LookUpHost"))).Descendants((XName) "APILookUpGateway").Select<XElement, Gateway>((Func<XElement, Gateway>) (g => new Gateway()
    {
      GatewayID = model.ObjectID,
      Name = g.Attribute((XName) "GatewayName").Value,
      RadioBand = g.Attribute((XName) "RadioBand").Value,
      APNFirmwareVersion = g.Attribute((XName) "APNFirmwareVersion").Value,
      GatewayFirmwareVersion = g.Attribute((XName) "GatewayFirmwareVersion").Value,
      GatewayTypeID = g.Attribute((XName) "GatewayTypeID").Value.ToLong(),
      MacAddress = g.Attribute((XName) "MacAddress").Value,
      CSNetID = model.NetworkID,
      SensorID = g.Attribute((XName) "SensorID").Value.ToLong(),
      GenerationType = g.Attribute((XName) "GenerationType").Value,
      PowerSourceID = g.Attribute((XName) "PowerSourceID") == null ? 3L : g.Attribute((XName) "PowerSourceID").Value.ToLong(),
      SKU = g.Attribute((XName) "SKU") != null ? g.Attribute((XName) "SKU").Value : ""
    })).FirstOrDefault<Gateway>();
  }

  public static Sensor LookUpSensor(long accountID, long sensorID, string checkDigit, Sensor sens)
  {
    return CSNetControllerBase.LookUpSensor(new AssignObjectModel()
    {
      AccountID = accountID,
      ObjectID = sensorID,
      Code = checkDigit
    }, sens);
  }

  protected static Sensor LookUpSensor(AssignObjectModel model, Sensor sens)
  {
    XDocument xdocument = XDocument.Load(string.Format("{2}/xml/LookUpSensor?sensorID={0}&checkDigit={1}", (object) model.ObjectID, (object) model.Code, (object) ConfigData.FindValue("LookUpHost")));
    XElement xelement = xdocument.Descendants((XName) "APILookUpSensor").FirstOrDefault<XElement>();
    if (xelement == null)
      return (Sensor) null;
    sens = xdocument.Descendants((XName) "APILookUpSensor").Select<XElement, Sensor>((Func<XElement, Sensor>) (s => new Sensor()
    {
      AccountID = MonnitSession.CurrentCustomer.AccountID,
      SensorID = model.ObjectID,
      ApplicationID = s.Attribute((XName) "ApplicationID").Value.ToLong(),
      SensorName = s.Attribute((XName) "SensorName").Value,
      SensorTypeID = s.Attribute((XName) "SensorTypeID").Value.ToLong(),
      FirmwareVersion = s.Attribute((XName) "FirmwareVersion").Value,
      PowerSourceID = s.Attribute((XName) "PowerSourceID").Value.ToLong(),
      RadioBand = s.Attribute((XName) "RadioBand").Value,
      LastCommunicationDate = new DateTime(2099, 1, 1),
      IsActive = true,
      IsSleeping = true,
      CSNetID = model.NetworkID,
      ActiveStateInterval = s.Attribute((XName) "ActiveStateInterval").Value.ToDouble(),
      ReportInterval = s.Attribute((XName) "ReportInterval").Value.ToDouble(),
      MinimumCommunicationFrequency = 1,
      CalibrationCertification = s.Attribute((XName) "CalibrationCertification").Value,
      GenerationType = s.Attribute((XName) "GenerationType").Value,
      SKU = s.Attribute((XName) "SKU") != null ? s.Attribute((XName) "SKU").Value : "",
      CableID = s.Attribute((XName) "CableID").Value.ToLong(),
      IsCableEnabled = s.Attribute((XName) "IsCableEnabled").ToBool()
    })).FirstOrDefault<Sensor>();
    sens.SetDefaults(false);
    sens.SetDefaultCalibration();
    if (xelement.Attribute((XName) "ReportInterval") != null)
    {
      sens.ReportInterval = xelement.Attribute((XName) "ReportInterval").Value.ToDouble();
      sens.MinimumCommunicationFrequency = sens.ReportInterval.ToInt() * 2 + 5;
    }
    if (xelement.Attribute((XName) "ActiveStateInterval") != null)
      sens.ActiveStateInterval = xelement.Attribute((XName) "ActiveStateInterval").Value.ToDouble();
    if (xelement.Attribute((XName) "MeasurementsPerTransmission") != null)
      sens.MeasurementsPerTransmission = xelement.Attribute((XName) "MeasurementsPerTransmission").Value.ToInt();
    if (xelement.Attribute((XName) "TransmitOffset") != null)
      sens.TransmitOffset = xelement.Attribute((XName) "TransmitOffset").Value.ToInt();
    if (xelement.Attribute((XName) "Hysteresis") != null)
      sens.Hysteresis = xelement.Attribute((XName) "Hysteresis").Value.ToLong();
    if (xelement.Attribute((XName) "MinimumThreshold") != null)
      sens.MinimumThreshold = xelement.Attribute((XName) "MinimumThreshold").Value.ToLong();
    if (xelement.Attribute((XName) "MaximumThreshold") != null)
      sens.MaximumThreshold = xelement.Attribute((XName) "MaximumThreshold").Value.ToLong();
    if (xelement.Attribute((XName) "Calibration1") != null)
      sens.Calibration1 = xelement.Attribute((XName) "Calibration1").Value.ToLong();
    if (xelement.Attribute((XName) "Calibration2") != null)
      sens.Calibration2 = xelement.Attribute((XName) "Calibration2").Value.ToLong();
    if (xelement.Attribute((XName) "Calibration3") != null)
      sens.Calibration3 = xelement.Attribute((XName) "Calibration3").Value.ToLong();
    if (xelement.Attribute((XName) "Calibration4") != null)
      sens.Calibration4 = xelement.Attribute((XName) "Calibration4").Value.ToLong();
    if (xelement.Attribute((XName) "EventDetectionType") != null)
      sens.EventDetectionType = xelement.Attribute((XName) "EventDetectionType").Value.ToInt();
    if (xelement.Attribute((XName) "EventDetectionPeriod") != null)
      sens.EventDetectionPeriod = xelement.Attribute((XName) "EventDetectionPeriod").Value.ToInt();
    if (xelement.Attribute((XName) "EventDetectionCount") != null)
      sens.EventDetectionCount = xelement.Attribute((XName) "EventDetectionCount").Value.ToInt();
    if (xelement.Attribute((XName) "RearmTime") != null)
      sens.RearmTime = xelement.Attribute((XName) "RearmTime").Value.ToInt();
    if (xelement.Attribute((XName) "BiStable") != null)
      sens.BiStable = xelement.Attribute((XName) "BiStable").Value.ToInt();
    if (xelement.Attribute((XName) "CableID") != null)
      sens.CableID = xelement.Attribute((XName) "CableID").Value.ToLong();
    if (xelement.Attribute((XName) "IsCableEnabled") != null)
      sens.IsCableEnabled = xelement.Attribute((XName) "IsCableEnabled").Value.ToBool();
    return sens;
  }

  public static List<Gateway> GetGatewayList() => CSNetControllerBase.GetGatewayList(out int _);

  public static List<Gateway> GetGatewayList(out int totalGateways)
  {
    totalGateways = 0;
    long CSNetID = MonnitSession.GatewayListFilters.SensorListFiltersCSNetID;
    long GatewayTypeID = MonnitSession.GatewayListFilters.GatewayTypeID;
    int Status = MonnitSession.GatewayListFilters.Status;
    string Name = MonnitSession.GatewayListFilters.Name;
    if (MonnitSession.CurrentCustomer == null)
      return new List<Gateway>();
    List<Gateway> source1 = Gateway.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
    long[] wifiSensorGateways = new long[5]
    {
      10L,
      11L,
      35L,
      36L,
      38L
    };
    int enterpriseInstallerGatewayID = 3420;
    IEnumerable<Gateway> source2 = source1.Where<Gateway>((Func<Gateway, bool>) (g => (g.CSNetID == CSNetID || CSNetID == long.MinValue) && MonnitSession.CurrentCustomer.CanSeeNetwork(g.CSNetID) && !((IEnumerable<long>) wifiSensorGateways).Contains<long>(g.GatewayTypeID) && g.GatewayID != (long) enterpriseInstallerGatewayID));
    totalGateways = source2.Count<Gateway>();
    IEnumerable<Gateway> source3 = (IEnumerable<Gateway>) source2.Where<Gateway>((Func<Gateway, bool>) (g =>
    {
      if (g.Status.ToInt() != Status && Status != int.MinValue || g.GatewayTypeID != GatewayTypeID && GatewayTypeID != long.MinValue)
        return false;
      return g.Name.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Gateway, string>((Func<Gateway, string>) (g => g.Name.Trim()));
    switch (MonnitSession.GatewayListSort.Name)
    {
      case "Type":
        source3 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source3.OrderBy<Gateway, long>((Func<Gateway, long>) (cs => cs.GatewayTypeID)) : (IEnumerable<Gateway>) source3.OrderByDescending<Gateway, long>((Func<Gateway, long>) (t => t.GatewayTypeID));
        break;
      case "Gateway Name":
        source3 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source3.OrderBy<Gateway, string>((Func<Gateway, string>) (g => g.Name)) : (IEnumerable<Gateway>) source3.OrderByDescending<Gateway, string>((Func<Gateway, string>) (g => g.Name));
        break;
      case "Signal":
        source3 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source3.OrderBy<Gateway, int>((Func<Gateway, int>) (s => s.CurrentSignalStrength)) : (IEnumerable<Gateway>) source3.OrderByDescending<Gateway, int>((Func<Gateway, int>) (s => s.CurrentSignalStrength));
        break;
      case "Last Check In":
        source3 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source3.OrderBy<Gateway, DateTime>((Func<Gateway, DateTime>) (b =>
        {
          DateTime communicationDate = b.LastCommunicationDate;
          return b.LastCommunicationDate;
        })) : (IEnumerable<Gateway>) source3.OrderByDescending<Gateway, DateTime>((Func<Gateway, DateTime>) (b =>
        {
          DateTime communicationDate = b.LastCommunicationDate;
          return b.LastCommunicationDate;
        }));
        break;
      case "IsEnterprise":
        source3 = !(MonnitSession.GatewayListSort.Direction == "Desc") ? (IEnumerable<Gateway>) source3.OrderBy<Gateway, bool>((Func<Gateway, bool>) (cs => cs.isEnterpriseHost)) : (IEnumerable<Gateway>) source3.OrderByDescending<Gateway, bool>((Func<Gateway, bool>) (t => t.isEnterpriseHost));
        break;
    }
    return source3.ToList<Gateway>();
  }

  protected void AssignNetwork(long accountID, CSNet existingNetwork)
  {
    List<Customer> customerList = Customer.LoadAllByAccount(existingNetwork.AccountID);
    CSNet DBObject = CSNet.Load(accountID);
    Account account1 = Account.Load(DBObject.AccountID);
    DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Network, MonnitSession.CurrentCustomer.CustomerID, account1.AccountID, "Assign network to new account");
    existingNetwork.AccountID = accountID;
    existingNetwork.Save();
    foreach (Sensor sensor in Sensor.LoadByCsNetID(existingNetwork.CSNetID))
    {
      sensor.AccountID = accountID;
      sensor.Save();
    }
    try
    {
      long permissionTypeId = CustomerPermissionType.Find("Network_View").CustomerPermissionTypeID;
      foreach (Customer customer in customerList)
      {
        foreach (CustomerPermission permission in customer.Permissions)
        {
          if (permission.CSNetID == existingNetwork.CSNetID && permission.CustomerPermissionTypeID == permissionTypeId)
            permission.Delete();
        }
      }
      Account account2 = Account.Load(existingNetwork.AccountID);
      foreach (Customer customer in Customer.LoadAllByAccount(existingNetwork.AccountID))
      {
        if (customer.IsAdmin || customer.CustomerID == account2.PrimaryContactID)
          new CustomerPermission()
          {
            CSNetID = existingNetwork.CSNetID,
            CustomerID = customer.CustomerID,
            Can = true,
            CustomerPermissionTypeID = permissionTypeId
          }.Save();
      }
    }
    catch (Exception ex)
    {
      string str = existingNetwork == null ? "null" : existingNetwork.CSNetID.ToString();
      ex.Log($"CSNetControllerBase.AssignNetwork | accountID = {accountID}, existingNetwork.CSNetID = {str})");
    }
  }

  protected List<Sensor> GetSensorList(long id)
  {
    return MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("Network_View"), id) || MonnitSession.IsCurrentCustomerMonnitAdmin ? Sensor.LoadByCsNetID(id).OrderBy<Sensor, string>((Func<Sensor, string>) (s => s.SensorName)).ToList<Sensor>() : new List<Sensor>();
  }
}
