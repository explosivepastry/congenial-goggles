// Decompiled with JetBrains decompiler
// Type: Monnit.FirmwareCache
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("FirmwareCache")]
public class FirmwareCache : BaseDBObject
{
  private long _FirmwareCacheID = long.MinValue;
  private long _GatewayID = long.MinValue;
  private long _FirmwareID = long.MinValue;
  private string _SKU = string.Empty;
  private bool _IsGatewayBase = false;
  private byte[] _Encrypted = (byte[]) null;
  private long _OriginalCRC = long.MinValue;
  private int _OriginalLength = int.MinValue;
  private DateTime _CacheDate = DateTime.UtcNow;

  [DBProp("FirmwareCacheID", IsPrimaryKey = true)]
  public long FirmwareCacheID
  {
    get => this._FirmwareCacheID;
    set => this._FirmwareCacheID = value;
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

  [DBProp("SKU", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string SKU
  {
    get => this._SKU;
    set => this._SKU = value;
  }

  [DBProp("IsGatewayBase")]
  public bool IsGatewayBase
  {
    get => this._IsGatewayBase;
    set => this._IsGatewayBase = value;
  }

  [DBProp("Encrypted")]
  public byte[] Encrypted
  {
    get => this._Encrypted;
    set => this._Encrypted = value;
  }

  [DBProp("OriginalCRC")]
  public long OriginalCRC
  {
    get => this._OriginalCRC;
    set => this._OriginalCRC = value;
  }

  [DBProp("OriginalLength")]
  public int OriginalLength
  {
    get => this._OriginalLength;
    set => this._OriginalLength = value;
  }

  [DBProp("CacheDate")]
  public DateTime CacheDate
  {
    get => this._CacheDate;
    set => this._CacheDate = value;
  }

  public static FirmwareCache Load(long id) => BaseDBObject.Load<FirmwareCache>(id);

  public static List<FirmwareCache> LoadByGatewayID(long gatewayID)
  {
    return BaseDBObject.LoadByForeignKey<FirmwareCache>("GatewayID", (object) gatewayID);
  }

  public static FirmwareCache Find(long gatewayID, string sku, bool isGatewayBase)
  {
    return new Monnit.Data.FirmwareCache.FindBySKU(gatewayID, sku, isGatewayBase).Result;
  }

  public static FirmwareCache Find(long gatewayID, long firmwareID)
  {
    return new Monnit.Data.FirmwareCache.FindByFirmware(gatewayID, firmwareID).Result;
  }

  public static void ClearForGateway(long gatewayID)
  {
    Monnit.Data.FirmwareCache.ClearForGateway clearForGateway = new Monnit.Data.FirmwareCache.ClearForGateway(gatewayID);
  }
}
