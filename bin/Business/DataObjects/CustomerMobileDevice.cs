// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerMobileDevice
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerMobileDevice")]
public class CustomerMobileDevice : BaseDBObject
{
  private long _CustomerMobileDeviceID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private bool _IsPrimary = false;
  private bool _SendToDevice = false;
  private string _MobileDeviceType = string.Empty;
  private string _MobileDeviceName = string.Empty;
  private string _MobileDisplayName = string.Empty;
  private string _MobileDeviceId = string.Empty;
  private DateTime _CreateDate = DateTime.UtcNow;
  private DateTime _LastModifyDate = DateTime.MinValue;

  [DBProp("CustomerMobileDeviceID", IsPrimaryKey = true)]
  public long CustomerMobileDeviceID
  {
    get => this._CustomerMobileDeviceID;
    set => this._CustomerMobileDeviceID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("IsPrimary", AllowNull = true)]
  public bool IsPrimary
  {
    get => this._IsPrimary;
    set => this._IsPrimary = value;
  }

  [DBProp("SendToDevice", AllowNull = true)]
  public bool SendToDevice
  {
    get => this._SendToDevice;
    set => this._SendToDevice = value;
  }

  [DBProp("MobileDeviceType", MaxLength = 50)]
  public string MobileDeviceType
  {
    get => this._MobileDeviceType;
    set
    {
      if (value == null)
        this._MobileDeviceType = string.Empty;
      else
        this._MobileDeviceType = value;
    }
  }

  [DBProp("MobileDeviceName", MaxLength = 255 /*0xFF*/)]
  public string MobileDeviceName
  {
    get => this._MobileDeviceName;
    set
    {
      if (value == null)
        this._MobileDeviceName = string.Empty;
      else
        this._MobileDeviceName = value;
    }
  }

  [DBProp("MobileDisplayName", MaxLength = 255 /*0xFF*/)]
  public string MobileDisplayName
  {
    get => this._MobileDisplayName;
    set
    {
      if (value == null)
        this._MobileDisplayName = string.Empty;
      else
        this._MobileDisplayName = value;
    }
  }

  [DBProp("MobileDeviceId", MaxLength = 255 /*0xFF*/)]
  public string MobileDeviceId
  {
    get => this._MobileDeviceId;
    set
    {
      if (value == null)
        this._MobileDeviceId = string.Empty;
      else
        this._MobileDeviceId = value;
    }
  }

  [DBProp("CreateDate", AllowNull = false)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("LastModifyDate", AllowNull = false)]
  public DateTime LastModifyDate
  {
    get => this._LastModifyDate;
    set => this._LastModifyDate = value;
  }

  public static List<CustomerMobileDevice> LoadAll()
  {
    return BaseDBObject.LoadAll<CustomerMobileDevice>();
  }

  public static CustomerMobileDevice Load(long id) => BaseDBObject.Load<CustomerMobileDevice>(id);

  public static List<CustomerMobileDevice> LoadByCustomerID(long id)
  {
    return BaseDBObject.LoadByForeignKey<CustomerMobileDevice>("CustomerID", (object) id);
  }
}
