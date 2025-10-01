// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.NotificationRecipientDevice
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class NotificationRecipientDevice : BaseDBObject
{
  private List<Sensor> _ControlUnitList = new List<Sensor>();
  private List<Sensor> _LocalAlertList = new List<Sensor>();
  private List<Sensor> _ThermostatList = new List<Sensor>();
  private List<Sensor> _ResetAccList = new List<Sensor>();

  [DBProp("ControlUnitList")]
  public List<Sensor> ControlUnitList
  {
    get => this._ControlUnitList;
    set => this._ControlUnitList = value;
  }

  [DBProp("LocalAlertList")]
  public List<Sensor> LocalAlertList
  {
    get => this._LocalAlertList;
    set => this._LocalAlertList = value;
  }

  [DBProp("ThermostatList")]
  public List<Sensor> ThermostatList
  {
    get => this._ThermostatList;
    set => this._ThermostatList = value;
  }

  [DBProp("ResetAccList")]
  public List<Sensor> ResetAccList
  {
    get => this._ResetAccList;
    set => this._ResetAccList = value;
  }

  public static NotificationRecipientDevice LoadByAccountID(long accountID)
  {
    return new Data.NotificationRecipientDevice.LoadByAccountID(accountID).Result;
  }
}
