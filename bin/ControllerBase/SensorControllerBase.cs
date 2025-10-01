// Decompiled with JetBrains decompiler
// Type: iMonnit.ControllerBase.SensorControllerBase
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit.Models;
using Monnit;
using RedefineImpossible;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

#nullable disable
namespace iMonnit.ControllerBase;

public class SensorControllerBase : ThemeController
{
  public static List<Sensor> GetAccountsSensorList(long accountID, out int totalSensors)
  {
    totalSensors = 0;
    long csNetId = MonnitSession.SensorListFilters.CSNetID;
    long AppID = MonnitSession.SensorListFilters.ApplicationID;
    int Status = MonnitSession.SensorListFilters.Status;
    string Name = MonnitSession.SensorListFilters.Name;
    string name = MonnitSession.SensorListSort.Name;
    if (MonnitSession.CurrentCustomer == null)
      return new List<Sensor>();
    IEnumerable<Sensor> source1 = Sensor.LoadByAccountID(accountID).Where<Sensor>((System.Func<Sensor, bool>) (s => MonnitSession.CurrentCustomer.CanSeeSensor(s)));
    totalSensors = source1.Count<Sensor>();
    IEnumerable<Sensor> source2 = (IEnumerable<Sensor>) source1.Where<Sensor>((System.Func<Sensor, bool>) (s =>
    {
      if (s.Status.ToInt() != Status && Status != int.MinValue || s.ApplicationID != AppID && AppID != long.MinValue)
        return false;
      return s.SensorName.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<Sensor, string>((System.Func<Sensor, string>) (s => s.SensorName.Trim()));
    switch (name)
    {
      case "Type":
        source2 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<Sensor>) source2.OrderBy<Sensor, long>((System.Func<Sensor, long>) (cs => cs.ApplicationID)) : (IEnumerable<Sensor>) source2.OrderByDescending<Sensor, long>((System.Func<Sensor, long>) (cs => cs.ApplicationID));
        break;
      case "Sensor Name":
        source2 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<Sensor>) source2.OrderBy<Sensor, string>((System.Func<Sensor, string>) (sn => sn.SensorName)) : (IEnumerable<Sensor>) source2.OrderByDescending<Sensor, string>((System.Func<Sensor, string>) (sn => sn.SensorName));
        break;
      case "Signal":
        source2 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<Sensor>) source2.OrderBy<Sensor, int>((System.Func<Sensor, int>) (s => s.LastDataMessage != null ? s.LastDataMessage.SignalStrength : 0)) : (IEnumerable<Sensor>) source2.OrderByDescending<Sensor, int>((System.Func<Sensor, int>) (s => s.LastDataMessage != null ? s.LastDataMessage.SignalStrength : 0));
        break;
      case "Battery":
        source2 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<Sensor>) source2.OrderBy<Sensor, int>((System.Func<Sensor, int>) (b => b.LastDataMessage != null ? b.LastDataMessage.Battery : 0)) : (IEnumerable<Sensor>) source2.OrderByDescending<Sensor, int>((System.Func<Sensor, int>) (b => b.LastDataMessage != null ? b.LastDataMessage.Battery : 0));
        break;
      case "Data":
        source2 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<Sensor>) source2.OrderBy<Sensor, string>((System.Func<Sensor, string>) (d => d.LastDataMessage != null ? d.LastDataMessage.Data : "No data gathered.")) : (IEnumerable<Sensor>) source2.OrderByDescending<Sensor, string>((System.Func<Sensor, string>) (d => d.LastDataMessage != null ? d.LastDataMessage.Data : "No data gathered."));
        break;
      case "Last Check In":
        source2 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<Sensor>) source2.OrderBy<Sensor, DateTime>((System.Func<Sensor, DateTime>) (lci => lci.LastCommunicationDate)) : (IEnumerable<Sensor>) source2.OrderByDescending<Sensor, DateTime>((System.Func<Sensor, DateTime>) (lci => lci.LastCommunicationDate));
        break;
    }
    return source2.ToList<Sensor>();
  }

  public static List<SensorGroupSensorModel> GetSensorList()
  {
    return SensorControllerBase.GetSensorList(out int _);
  }

  public static List<SensorGroupSensorModel> GetSensorList(long accountID)
  {
    return SensorControllerBase.GetSensorList(accountID, out int _);
  }

  public static List<SensorGroupSensorModel> GetSensorList(out int totalSensors)
  {
    return SensorControllerBase.GetSensorList(MonnitSession.CurrentCustomer.AccountID, out totalSensors);
  }

  public static List<SensorGroupSensorModel> GetSensorList(long accountID, out int totalSensors)
  {
    totalSensors = 0;
    long csNetId = MonnitSession.SensorListFilters.CSNetID;
    long AppID = MonnitSession.SensorListFilters.ApplicationID;
    int Status = MonnitSession.SensorListFilters.Status;
    string Name = MonnitSession.SensorListFilters.Name;
    string name = MonnitSession.SensorListSort.Name;
    if (MonnitSession.CurrentCustomer == null)
      return new List<SensorGroupSensorModel>();
    if (csNetId == ConfigData.AppSettings("DefaultCSNetID").ToLong())
      return new List<SensorGroupSensorModel>();
    List<SensorGroupSensorModel> source1 = new List<SensorGroupSensorModel>();
    try
    {
      foreach (DataRow row in (InternalDataCollectionBase) Sensor.SensorOverView(accountID, new long?(csNetId)).Rows)
      {
        SensorGroupSensorModel groupSensorModel = new SensorGroupSensorModel();
        Sensor sensor = new Sensor();
        DataMessage dataMessage = new DataMessage();
        sensor.Load(row);
        sensor.SuppressPropertyChangedEvent = false;
        sensor.AddToCache();
        dataMessage.Load(row);
        if (dataMessage.DataMessageGUID != Guid.Empty)
        {
          dataMessage.AddToCache();
          groupSensorModel.DataMessage = dataMessage;
          sensor.LastDataMessage = dataMessage;
        }
        groupSensorModel.SensorGroupName = row["SensorGroupName"].ToStringSafe();
        groupSensorModel.SensorGroupID = row["SensorGroupID"].ToLong();
        groupSensorModel.Sensor = sensor;
        source1.Add(groupSensorModel);
      }
    }
    catch
    {
    }
    IEnumerable<SensorGroupSensorModel> source2 = source1.Where<SensorGroupSensorModel>((System.Func<SensorGroupSensorModel, bool>) (s => MonnitSession.CurrentCustomer.CanSeeSensor(s.Sensor)));
    totalSensors = source2.Count<SensorGroupSensorModel>();
    IEnumerable<SensorGroupSensorModel> source3 = (IEnumerable<SensorGroupSensorModel>) source2.Where<SensorGroupSensorModel>((System.Func<SensorGroupSensorModel, bool>) (s =>
    {
      if (s.Sensor.Status.ToInt() != Status && Status != int.MinValue || s.Sensor.ApplicationID != AppID && AppID != long.MinValue)
        return false;
      return s.Sensor.SensorName.ToLower().Contains(Name.ToLower()) || Name == "";
    })).OrderBy<SensorGroupSensorModel, string>((System.Func<SensorGroupSensorModel, string>) (s => s.Sensor.SensorName.Trim()));
    switch (name)
    {
      case "Battery":
        source3 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<SensorGroupSensorModel>) source3.OrderBy<SensorGroupSensorModel, int>((System.Func<SensorGroupSensorModel, int>) (b => b.Sensor.LastDataMessage != null ? b.Sensor.LastDataMessage.Battery : 0)) : (IEnumerable<SensorGroupSensorModel>) source3.OrderByDescending<SensorGroupSensorModel, int>((System.Func<SensorGroupSensorModel, int>) (b => b.Sensor.LastDataMessage != null ? b.Sensor.LastDataMessage.Battery : 0));
        break;
      case "Data":
        source3 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<SensorGroupSensorModel>) source3.OrderBy<SensorGroupSensorModel, string>((System.Func<SensorGroupSensorModel, string>) (d => d.Sensor.LastDataMessage != null ? d.Sensor.LastDataMessage.Data : "No data gathered.")) : (IEnumerable<SensorGroupSensorModel>) source3.OrderByDescending<SensorGroupSensorModel, string>((System.Func<SensorGroupSensorModel, string>) (d => d.Sensor.LastDataMessage != null ? d.Sensor.LastDataMessage.Data : "No data gathered."));
        break;
      case "Last Check In":
        source3 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<SensorGroupSensorModel>) source3.OrderBy<SensorGroupSensorModel, DateTime>((System.Func<SensorGroupSensorModel, DateTime>) (lci => lci.Sensor.LastCommunicationDate)) : (IEnumerable<SensorGroupSensorModel>) source3.OrderByDescending<SensorGroupSensorModel, DateTime>((System.Func<SensorGroupSensorModel, DateTime>) (lci => lci.Sensor.LastCommunicationDate));
        break;
      case "Sensor Group":
        source3 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<SensorGroupSensorModel>) source3.OrderBy<SensorGroupSensorModel, string>((System.Func<SensorGroupSensorModel, string>) (sgn => sgn.SensorGroupName)) : (IEnumerable<SensorGroupSensorModel>) source3.OrderByDescending<SensorGroupSensorModel, string>((System.Func<SensorGroupSensorModel, string>) (sgn => sgn.SensorGroupName));
        break;
      case "Sensor Name":
        source3 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<SensorGroupSensorModel>) source3.OrderBy<SensorGroupSensorModel, string>((System.Func<SensorGroupSensorModel, string>) (sn => sn.Sensor.SensorName)) : (IEnumerable<SensorGroupSensorModel>) source3.OrderByDescending<SensorGroupSensorModel, string>((System.Func<SensorGroupSensorModel, string>) (sn => sn.Sensor.SensorName));
        break;
      case "Signal":
        source3 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<SensorGroupSensorModel>) source3.OrderBy<SensorGroupSensorModel, int>((System.Func<SensorGroupSensorModel, int>) (s => s.Sensor.LastDataMessage != null ? s.Sensor.LastDataMessage.SignalStrength : 0)) : (IEnumerable<SensorGroupSensorModel>) source3.OrderByDescending<SensorGroupSensorModel, int>((System.Func<SensorGroupSensorModel, int>) (s => s.Sensor.LastDataMessage != null ? s.Sensor.LastDataMessage.SignalStrength : 0));
        break;
      case "Type":
        source3 = !(MonnitSession.SensorListSort.Direction == "Desc") ? (IEnumerable<SensorGroupSensorModel>) source3.OrderBy<SensorGroupSensorModel, long>((System.Func<SensorGroupSensorModel, long>) (cs => cs.Sensor.ApplicationID)) : (IEnumerable<SensorGroupSensorModel>) source3.OrderByDescending<SensorGroupSensorModel, long>((System.Func<SensorGroupSensorModel, long>) (cs => cs.Sensor.ApplicationID));
        break;
    }
    return source3.ToList<SensorGroupSensorModel>();
  }

  protected void AddTimeDropDowns(Sensor sensor)
  {
    if (new Version(sensor.FirmwareVersion) >= new Version("1.2.009") && sensor.SensorTypeID != 4L)
    {
      this.ViewData["ShowTimeOfDay"] = (object) true;
      TimeSpan timeSpan1 = sensor.ActiveStartTime.Add(MonnitSession.UTCOffset);
      TimeSpan timeSpan2 = sensor.ActiveEndTime.Add(MonnitSession.UTCOffset);
      if (timeSpan1 == timeSpan2)
      {
        timeSpan1 = new TimeSpan(0L);
        timeSpan2 = new TimeSpan(0L);
      }
      else
      {
        if (timeSpan1.TotalMinutes < 0.0)
          timeSpan1 = timeSpan1.Add(new TimeSpan(1, 0, 0, 0));
        if (timeSpan1.Days > 0)
          timeSpan1 = timeSpan1.Subtract(new TimeSpan(1, 0, 0, 0));
        if (timeSpan2.TotalMinutes < 0.0)
          timeSpan2 = timeSpan2.Add(new TimeSpan(1, 0, 0, 0));
        if (timeSpan2.Days > 0)
          timeSpan2 = timeSpan2.Subtract(new TimeSpan(1, 0, 0, 0));
      }
      this.ViewData["StartHours"] = (object) new SelectList((IEnumerable) new string[12]
      {
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "11",
        "12"
      }, (object) (timeSpan1.Hours > 12 ? timeSpan1.Hours - 12 : (timeSpan1.Hours == 0 ? 12 : timeSpan1.Hours)).ToString());
      ViewDataDictionary viewData1 = this.ViewData;
      string[] items1 = new string[4]
      {
        "00",
        "15",
        "30",
        "45"
      };
      int num = timeSpan1.Minutes;
      SelectList selectList1 = new SelectList((IEnumerable) items1, (object) num.ToString());
      viewData1["StartMinutes"] = (object) selectList1;
      this.ViewData["StartAM"] = (object) new SelectList((IEnumerable) new string[2]
      {
        "AM",
        "PM"
      }, timeSpan1.Hours < 12 ? (object) "AM" : (object) "PM");
      ViewDataDictionary viewData2 = this.ViewData;
      string[] items2 = new string[12]
      {
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "10",
        "11",
        "12"
      };
      num = timeSpan2.Hours > 12 ? timeSpan2.Hours - 12 : (timeSpan2.Hours == 0 ? 12 : timeSpan2.Hours);
      SelectList selectList2 = new SelectList((IEnumerable) items2, (object) num.ToString());
      viewData2["EndHours"] = (object) selectList2;
      ViewDataDictionary viewData3 = this.ViewData;
      string[] items3 = new string[4]
      {
        "00",
        "15",
        "30",
        "45"
      };
      num = timeSpan2.Minutes;
      SelectList selectList3 = new SelectList((IEnumerable) items3, (object) num.ToString());
      viewData3["EndMinutes"] = (object) selectList3;
      this.ViewData["EndAM"] = (object) new SelectList((IEnumerable) new string[2]
      {
        "AM",
        "PM"
      }, timeSpan2.Hours < 12 ? (object) "AM" : (object) "PM");
    }
    else
      this.ViewData["ShowTimeOfDay"] = (object) false;
  }

  protected TimeSpan GetStartTime(FormCollection collection)
  {
    try
    {
      int hours = collection["ActiveStartTimeHour"].ToInt();
      if (hours == 12)
        hours = 0;
      if (collection["ActiveStartTimeAM"] == "PM")
        hours += 12;
      int minutes = collection["ActiveStartTimeMinute"].ToInt();
      return new TimeSpan(hours, minutes, 0);
    }
    catch
    {
      return new TimeSpan();
    }
  }

  protected TimeSpan GetEndTime(FormCollection collection)
  {
    try
    {
      int hours = collection["ActiveEndTimeHour"].ToInt();
      if (hours == 12)
        hours = 0;
      if (collection["ActiveEndTimeAM"] == "PM")
        hours += 12;
      int minutes = collection["ActiveEndTimeMinute"].ToInt();
      return new TimeSpan(hours, minutes, 0);
    }
    catch
    {
      return new TimeSpan();
    }
  }

  protected Gateway SaveWIFISettings(Sensor sensor, FormCollection collection)
  {
    Gateway DBObject = Gateway.LoadBySensorID(sensor.SensorID);
    if (!string.IsNullOrEmpty(collection["GatewayIP"]) && collection["GatewayIP"] != "0.0.0.0")
    {
      if (collection["GatewaySubnet"] == "" || collection["GatewaySubnet"] == "0.0.0.0")
        this.ModelState.AddModelError("GatewaySubnet", "Subnet Required when not using DHCP");
      if (collection["GatewayDNS"] == "" || collection["GatewayDNS"] == "0.0.0.0")
        this.ModelState.AddModelError("GatewayDNS", "Default DNS Required when not using DHCP");
      if (DBObject.GatewayType.SupportsDefaultRouterIP && (collection["DefaultRouterIP"] == "" || collection["DefaultRouterIP"] == "0.0.0.0"))
        this.ModelState.AddModelError("DefaultRouterIP", "Default Gateway Required when not using DHCP");
      if (!collection["GatewayIP"].IsIPAddress())
        this.ModelState.AddModelError("GatewayIP", "Must be valid IP Address (ie 192.168.0.10)");
      if (!collection["GatewaySubnet"].IsIPAddress())
        this.ModelState.AddModelError("GatewaySubnet", "Must be valid Subnet Mask (ie 255.255.255.0)");
      if (!collection["GatewayDNS"].IsIPAddress())
        this.ModelState.AddModelError("GatewayDNS", "Must be valid IP Address (ie 192.168.0.1)");
      if (DBObject.GatewayType.SupportsDefaultRouterIP && !collection["DefaultRouterIP"].IsIPAddress())
        this.ModelState.AddModelError("DefaultRouterIP", "Must be valid IP Address (ie 192.168.0.1)");
      if (string.IsNullOrWhiteSpace(collection["SSID1"]))
        this.ModelState.AddModelError("SSID1", "Primary WIFI Network Required");
    }
    string str1 = collection["SSID1"];
    if (string.IsNullOrWhiteSpace(str1))
      this.ModelState.AddModelError("SSID1", "Primary WIFI Network Required");
    if (this.ModelState.IsValid)
    {
      Account account = Account.Load(sensor.AccountID);
      AuditLog.LogAuditData(MonnitSession.CurrentCustomer.CustomerID, sensor.SensorID, eAuditAction.Update, eAuditObject.Sensor, $"{{\"DeviceID\" : \"{sensor.SensorID}\", \"DeviceType\": \"{"Sensor"}\", \"Date\": \"{DateTime.UtcNow}\" }} ", account.AccountID, "Updated gateway Wifi settings");
      DBObject.LogAuditData(eAuditAction.Update, eAuditObject.Gateway, MonnitSession.CurrentCustomer.CustomerID, account.AccountID, "Saved Wifi settings");
      try
      {
        DBObject.Name = $"WiFi Gateway({collection["SensorName"]})";
        DBObject.ReportInterval = collection["ReportInterval"].ToDouble();
        DBObject.LedActiveTime = collection["LedActive"].ToStringSafe().ToLower().Contains("true") ? 1 : 0;
        if (sensor.DataLogChanged)
          DBObject.IsDirty = true;
        int num = 0;
        if (DBObject.GatewayTypeID > long.MinValue)
        {
          GatewayType gatewayType = GatewayType.Load(DBObject.GatewayTypeID);
          if (gatewayType.SupportsHostAddress && !string.IsNullOrEmpty(collection["GatewayServerHostAddress"]))
            DBObject.ServerHostAddress = collection["GatewayServerHostAddress"];
          if (gatewayType.SupportsHostPort && collection["GatewayPort"].ToInt() > int.MinValue)
            DBObject.Port = collection["GatewayPort"].ToInt();
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewayIP"]))
            DBObject.GatewayIP = collection["GatewayIP"];
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewaySubnet"]))
            DBObject.GatewaySubnet = collection["GatewaySubnet"];
          if (gatewayType.SupportsGatewayIP && !string.IsNullOrEmpty(collection["GatewayDNS"]))
            DBObject.GatewayDNS = collection["GatewayDNS"];
          if (gatewayType.SupportsDefaultRouterIP && !string.IsNullOrEmpty(collection["DefaultRouterIP"]))
            DBObject.DefaultRouterIP = collection["DefaultRouterIP"];
          GatewayWIFICredential gatewayWifiCredential1 = DBObject.WifiCredential(1);
          if (!string.IsNullOrEmpty(collection["SSID1"]))
          {
            num = 1;
            gatewayWifiCredential1.SSID = collection["SSID1"];
            gatewayWifiCredential1.WIFISecurityMode = (eWIFI_SecurityMode) Enum.Parse(typeof (eWIFI_SecurityMode), collection["WIFISecurityMode"]);
            if (!string.IsNullOrEmpty(collection["PassPhrase1"]))
              gatewayWifiCredential1.PassPhrase = MonnitSession.UseEncryption ? collection["PassPhrase1"].Encrypt() : collection["PassPhrase1"];
            if (gatewayWifiCredential1.IsDirty || gatewayWifiCredential1.GatewayWIFICredentialID == long.MinValue)
            {
              DBObject.IsDirty = true;
              gatewayWifiCredential1.IsDirty = false;
              gatewayWifiCredential1.Save();
            }
          }
          else if (gatewayWifiCredential1.GatewayWIFICredentialID > 0L)
          {
            gatewayWifiCredential1.Delete();
            DBObject.IsDirty = true;
          }
          GatewayWIFICredential gatewayWifiCredential2 = DBObject.WifiCredential(2);
          if (!string.IsNullOrWhiteSpace(collection["SSID2"]))
          {
            num = 2;
            gatewayWifiCredential2.SSID = collection["SSID2"];
            gatewayWifiCredential2.WIFISecurityMode = (eWIFI_SecurityMode) Enum.Parse(typeof (eWIFI_SecurityMode), collection["WIFISecurityMode2"]);
            if (!string.IsNullOrEmpty(collection["PassPhrase2"]))
              gatewayWifiCredential2.PassPhrase = MonnitSession.UseEncryption ? collection["PassPhrase2"].Encrypt() : collection["PassPhrase2"];
            if (gatewayWifiCredential2.IsDirty || gatewayWifiCredential2.GatewayWIFICredentialID == long.MinValue)
            {
              DBObject.IsDirty = true;
              gatewayWifiCredential2.IsDirty = false;
              gatewayWifiCredential2.Save();
            }
          }
          else if (gatewayWifiCredential2.GatewayWIFICredentialID > 0L)
          {
            gatewayWifiCredential2.Delete();
            DBObject.IsDirty = true;
          }
          GatewayWIFICredential gatewayWifiCredential3 = DBObject.WifiCredential(3);
          if (num == 2 && !string.IsNullOrWhiteSpace(collection["SSID3"]))
          {
            num = 3;
            gatewayWifiCredential3.SSID = collection["SSID3"];
            gatewayWifiCredential3.WIFISecurityMode = (eWIFI_SecurityMode) Enum.Parse(typeof (eWIFI_SecurityMode), collection["WIFISecurityMode3"]);
            if (!string.IsNullOrEmpty(collection["PassPhrase3"]))
              gatewayWifiCredential3.PassPhrase = MonnitSession.UseEncryption ? collection["PassPhrase3"].Encrypt() : collection["PassPhrase3"];
            if (gatewayWifiCredential3.IsDirty || gatewayWifiCredential3.GatewayWIFICredentialID == long.MinValue)
            {
              DBObject.IsDirty = true;
              gatewayWifiCredential3.IsDirty = false;
              gatewayWifiCredential3.Save();
            }
          }
          else if (gatewayWifiCredential3.GatewayWIFICredentialID > 0L)
          {
            gatewayWifiCredential3.Delete();
            DBObject.IsDirty = true;
          }
          if (gatewayType.SupportsSecondaryDNS)
            DBObject.GatewayDNS = string.IsNullOrEmpty(collection["GatewayDNS"]) ? gatewayType.DefaultGatewayDNS : collection["GatewayDNS"];
          if (gatewayType.SupportsSecondaryDNS)
            DBObject.SecondaryDNS = string.IsNullOrEmpty(collection["SecondaryDNS"]) ? gatewayType.DefaultSecondaryDNS : collection["SecondaryDNS"];
        }
        DBObject.WiFiNetworkCount = num;
        DBObject.Save();
      }
      catch (Exception ex)
      {
        string str2 = sensor == null ? "null" : sensor.SensorID.ToString();
        string str3 = DBObject == null ? "null" : DBObject.GatewayID.ToString();
        ex.Log($"SensorControllerBase.SaveWIFISettings | SensorID = {sensor.SensorID}, GatewayID = {str3}, SSID = {str1}");
      }
    }
    return DBObject;
  }
}
