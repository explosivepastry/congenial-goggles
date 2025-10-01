// Decompiled with JetBrains decompiler
// Type: Monnit.OTARequest
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("OTARequest")]
public class OTARequest : BaseDBObject
{
  private long _OTARequestID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private long _FirmwareID = long.MinValue;
  private string _Version = string.Empty;
  private string _SKU = string.Empty;
  private DateTime _CreateDate = DateTime.MinValue;
  private long _CreatedByID = long.MinValue;
  private long _ApplicationID = long.MinValue;
  private eOTAStatus _Status;
  private List<OTARequestSensor> _OTARequestSensors = (List<OTARequestSensor>) null;

  [DBProp("OTARequestID", IsPrimaryKey = true)]
  public long OTARequestID
  {
    get => this._OTARequestID;
    set => this._OTARequestID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("GatewayID")]
  [DBForeignKey("Gateway", "GatewayID")]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("FirmwareID")]
  public long FirmwareID
  {
    get => this._FirmwareID;
    set => this._FirmwareID = value;
  }

  [DBProp("Version", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Version
  {
    get => this._Version;
    set => this._Version = value;
  }

  [DBProp("SKU", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SKU
  {
    get => this._SKU;
    set => this._SKU = value;
  }

  [DBProp("CreateDate")]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("CreatedByID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [DBProp("ApplicationID")]
  [DBForeignKey("Application", "ApplicationID")]
  public long ApplicationID
  {
    get => this._ApplicationID;
    set => this._ApplicationID = value;
  }

  [DBProp("Status", AllowNull = false, DefaultValue = 4)]
  public eOTAStatus Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  public List<OTARequestSensor> OTARequestSensors
  {
    get
    {
      if (this._OTARequestSensors == null)
        this._OTARequestSensors = OTARequestSensor.LoadByOTARequestID(this.OTARequestID);
      return this._OTARequestSensors;
    }
  }

  public static OTARequest Load(long id) => BaseDBObject.Load<OTARequest>(id);

  public static List<OTARequest> LoadActiveByAccountID(long accountID)
  {
    return Monnit.Data.OTARequest.LoadActiveByAccountID.Exec(accountID);
  }

  public static List<OTARequest> LoadActiveByGatewayID(long gatewayID)
  {
    return Monnit.Data.OTARequest.LoadActiveByGatewayID.Exec(gatewayID);
  }

  public static List<Sensor> LoadSensorsToUpdate(long accountID, string minVersion)
  {
    return Monnit.Data.OTARequest.LoadSensorsToUpdate.Exec(accountID, minVersion);
  }
}
