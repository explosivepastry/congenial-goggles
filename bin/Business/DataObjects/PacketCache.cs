// Decompiled with JetBrains decompiler
// Type: Monnit.PacketCache
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class PacketCache
{
  private DateTime _StartTime;
  public Dictionary<long, string> NotifyingOn;
  private Dictionary<long, CSNet> _Network;
  private Dictionary<long, Account> _Account;
  private Dictionary<long, Sensor> _Sensors;
  private Dictionary<long, Customer> _Customers;
  private Dictionary<long, List<Notification>> _SensorNotifications;

  public PacketCache()
  {
    this._StartTime = DateTime.UtcNow;
    this._Sensors = new Dictionary<long, Sensor>();
    this._Customers = new Dictionary<long, Customer>();
    this._SensorNotifications = new Dictionary<long, List<Notification>>();
    this.RecordedNotifications = new List<NotificationRecorded>();
    this.DataMessages = new List<DataMessage>();
    this.LocationMessages = new List<LocationMessage>();
    this.NotifyingOn = new Dictionary<long, string>();
  }

  public TimeSpan ProcessingTime => DateTime.UtcNow.Subtract(this._StartTime);

  public Gateway Gateway { get; set; }

  public GatewayMessage GatewayMessage { get; set; }

  public GatewayLocation GatewayLocation { get; set; }

  public CSNet LoadNetwork(long csNetID)
  {
    if (this._Network == null)
      this._Network = new Dictionary<long, CSNet>();
    CSNet csNet;
    if (!this._Network.TryGetValue(csNetID, out csNet))
    {
      csNet = CSNet.Load(csNetID);
      if (csNet != null)
      {
        try
        {
          this._Network.Add(csNetID, csNet);
        }
        catch
        {
        }
      }
      else if (ConfigData.AppSettings("LogInvalidSensorIDExceptions", "False").ToBool())
        ExceptionLog.Log(new Exception("InboundPacket.LoadNetwork(long accountID) failed inside of if(!_Account.TryGetValue(accountID, out Temp)) temp is null. CSNetID is: " + csNetID.ToString()));
    }
    else if (csNet == null && ConfigData.AppSettings("LogInvalidSensorIDExceptions", "False").ToBool())
      ExceptionLog.Log(new Exception("InboundPacket.LoadNetwork(long accountID) failed inside of else if (Temp is null) temp is null. CSNetID is: " + csNetID.ToString()));
    return csNet;
  }

  public Account LoadAccount(long accountID)
  {
    if (this._Account == null)
      this._Account = new Dictionary<long, Account>();
    Account account;
    if (!this._Account.TryGetValue(accountID, out account))
    {
      account = Account.Load(accountID);
      if (account != null)
      {
        try
        {
          this._Account.Add(accountID, account);
        }
        catch
        {
        }
      }
      else if (ConfigData.AppSettings("LogInvalidSensorIDExceptions", "False").ToBool())
        ExceptionLog.Log(new Exception("InboundPacket.LoadAccount(long accountID) failed inside of if(!_Account.TryGetValue(accountID, out Temp)) temp is null. AccountID is: " + accountID.ToString()));
    }
    else if (account == null && ConfigData.AppSettings("LogInvalidSensorIDExceptions", "False").ToBool())
      ExceptionLog.Log(new Exception("InboundPacket.LoadAccount(long accountID) failed inside of else if (Temp is null) temp is null. AccountID is: " + accountID.ToString()));
    return account;
  }

  public Sensor LoadSensor(long sensorID)
  {
    Sensor sensor;
    if (!this._Sensors.TryGetValue(sensorID, out sensor))
    {
      sensor = Sensor.Load(sensorID);
      if (sensor != null)
      {
        try
        {
          this._Sensors.Add(sensorID, sensor);
        }
        catch
        {
        }
      }
      else if (ConfigData.AppSettings("LogInvalidSensorIDExceptions", "False").ToBool())
        ExceptionLog.Log(new Exception($"InboundPacket.LoadSensor(long sensorID) failed inside of if(!_sensors.trygetValue(sensorID,out Temp)) temp is null. SensorID is: {sensorID.ToString()} | {Environment.StackTrace}"));
    }
    else if (sensor == null && ConfigData.AppSettings("LogInvalidSensorIDExceptions", "False").ToBool())
      ExceptionLog.Log(new Exception($"InboundPacket.LoadSensor(long sensorID) failed inside of else if (Temp is null) temp is null. SensorID is: {sensorID.ToString()} | {Environment.StackTrace}"));
    return sensor;
  }

  public void AddSensor(Sensor sensor)
  {
    if (sensor == null || this._Sensors.ContainsKey(sensor.SensorID))
      return;
    this._Sensors.Add(sensor.SensorID, sensor);
  }

  public bool ContainsSensor(long sensorID)
  {
    Sensor sensor;
    return this._Sensors.TryGetValue(sensorID, out sensor) && sensor != null;
  }

  public Customer LoadCustomer(long customerID)
  {
    Customer customer = (Customer) null;
    if (!this._Customers.TryGetValue(customerID, out customer))
    {
      customer = Customer.Load(customerID);
      if (customer != null)
        this._Customers.Add(customerID, customer);
    }
    return customer;
  }

  public List<Notification> LoadSensorNotifications(long sensorID)
  {
    List<Notification> notificationList = (List<Notification>) null;
    if (!this._SensorNotifications.TryGetValue(sensorID, out notificationList))
    {
      notificationList = Notification.LoadBySensorID(sensorID);
      if (notificationList != null)
      {
        try
        {
          this._SensorNotifications.Add(sensorID, notificationList);
        }
        catch
        {
        }
      }
    }
    return notificationList;
  }

  public List<NotificationRecorded> RecordedNotifications { get; private set; }

  public List<DataMessage> DataMessages { get; set; }

  public List<LocationMessage> LocationMessages { get; set; }
}
