// Decompiled with JetBrains decompiler
// Type: Monnit.CSNet
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CSNet")]
public class CSNet : BaseDBObject
{
  private long _CSNetID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _Name = string.Empty;
  private bool _SendNotifications = false;
  private bool _AlertNotificationsAreOff = true;
  private bool _HoldingOnlyNetwork = false;
  private DateTime _ExternalAccessUntil = DateTime.MinValue;
  private List<Gateway> _Gateways = (List<Gateway>) null;
  private List<Sensor> _Sensors = (List<Sensor>) null;
  private List<Cable> _Cables = (List<Cable>) null;

  [DBProp("CSNetID", IsPrimaryKey = true)]
  public long CSNetID
  {
    get => this._CSNetID;
    set => this._CSNetID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, International = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value.ToStringSafe();
  }

  [DBProp("SendNotifications", AllowNull = true)]
  public bool SendNotifications
  {
    get => this._SendNotifications;
    set => this._SendNotifications = value;
  }

  [DBProp("AlertNotificationsAreOff", AllowNull = false, DefaultValue = 1)]
  public bool AlertNotificationsAreOff
  {
    get => this._AlertNotificationsAreOff;
    set => this._AlertNotificationsAreOff = value;
  }

  [DBProp("HoldingOnlyNetwork")]
  public bool HoldingOnlyNetwork
  {
    get => this._HoldingOnlyNetwork;
    set => this._HoldingOnlyNetwork = value;
  }

  [DBProp("ExternalAccessUntil", AllowNull = true)]
  public DateTime ExternalAccessUntil
  {
    get => this._ExternalAccessUntil;
    set => this._ExternalAccessUntil = value;
  }

  public List<Gateway> Gateways
  {
    get
    {
      if (this._Gateways == null)
        this._Gateways = Gateway.LoadByCSNetID(this.CSNetID);
      return this._Gateways;
    }
  }

  public List<Sensor> Sensors
  {
    get
    {
      if (this._Sensors == null)
        this._Sensors = Sensor.LoadByCsNetID(this.CSNetID);
      return this._Sensors;
    }
  }

  public List<Cable> Cables
  {
    get
    {
      if (this._Cables == null)
      {
        this._Cables = new List<Cable>();
        foreach (Sensor sensor in this.Sensors)
        {
          if (sensor.IsCableEnabled && sensor.CableID > 0L)
            this._Cables.Add(Cable.Load(sensor.CableID));
        }
      }
      return this._Cables;
    }
  }

  public static CSNet Load(long ID)
  {
    CSNet csNet = BaseDBObject.Load<CSNet>(ID);
    if (csNet == null)
    {
      List<Sensor> sensorList = BaseDBObject.Load<Sensor>(Sensor.LoadTableByCsNetID(ID));
      if (sensorList.Count > 0)
      {
        Account account = Account.Load(sensorList[0].AccountID);
        csNet = new CSNet();
        csNet.CSNetID = ID;
        csNet.AccountID = account.AccountID;
        csNet.Name = $"{account.CompanyName}-{ID}";
        csNet.Save();
      }
    }
    return csNet;
  }

  public static List<CSNet> LoadByAccountID(long accountID)
  {
    return BaseDBObject.LoadByForeignKey<CSNet>("AccountID", (object) accountID);
  }

  public static void SetGatewaysUrgentTrafficFlag(long csNetID)
  {
    Monnit.Data.CSNet.SetGatewaysUrgentTrafficFlag urgentTrafficFlag = new Monnit.Data.CSNet.SetGatewaysUrgentTrafficFlag(csNetID);
  }

  public static bool RequiresUrgentProcessing(long csNetID)
  {
    return new Monnit.Data.CSNet.RequiresUrgentProcessing(csNetID).Result;
  }

  public static List<long> IgnoreCSNetIDList() => new Monnit.Data.CSNet.IgnoreCSNetIDList().Result;

  public static object LoadByAccountID(object id) => throw new NotImplementedException();
}
