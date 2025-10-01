// Decompiled with JetBrains decompiler
// Type: Monnit.DataUseList
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("DataUseList")]
public class DataUseList : BaseDBObject
{
  private long _DataUseList = long.MinValue;
  private long _GatewayID = long.MinValue;
  public string _CellID = string.Empty;
  public string _TimeZoneIDString = string.Empty;
  public string _Carrier = string.Empty;
  public string _Name = string.Empty;
  public string _Status = string.Empty;
  public int _eMonnitStatus = int.MinValue;

  [DBProp("DataUseListID", IsPrimaryKey = true)]
  public long DataUseListID
  {
    get => this._DataUseList;
    set => this._DataUseList = value;
  }

  [DBProp("GatewayID", AllowNull = true)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("CellID", MaxLength = 3000)]
  public string CellID
  {
    get => this._CellID;
    set => this._CellID = value;
  }

  [DBProp("TimeZoneIDString", MaxLength = 300)]
  public string TimeZoneIDString
  {
    get => this._TimeZoneIDString;
    set => this._TimeZoneIDString = value;
  }

  [DBProp("Carrier", MaxLength = 300)]
  public string Carrier
  {
    get => this._Carrier;
    set => this._Carrier = value;
  }

  [DBProp("Name", MaxLength = 300)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Status", MaxLength = 300)]
  public string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [DBProp("eMonnitStatus", MaxLength = 300)]
  public int eMonnitStatus
  {
    get => this._eMonnitStatus;
    set => this._eMonnitStatus = value;
  }

  public static List<DataUseList> LoadAllByCarrier(string carrier, bool forLogging)
  {
    return new Monnit.Data.DataUseList.LoadAllByCarrier(carrier, forLogging).Result;
  }

  public static void GenerateNew()
  {
    Monnit.Data.DataUseList.PopulateNew populateNew = new Monnit.Data.DataUseList.PopulateNew();
  }
}
