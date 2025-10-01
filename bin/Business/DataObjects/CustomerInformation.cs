// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerInformation
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("CustomerInformation")]
public class CustomerInformation : BaseDBObject
{
  private long _CustomerInformationID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private long _CustomerInformationTypeID = long.MinValue;
  private string _Information = string.Empty;

  [DBProp("CustomerInformationID", IsPrimaryKey = true)]
  public long CustomerInformationID
  {
    get => this._CustomerInformationID;
    set => this._CustomerInformationID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("CustomerInformationTypeID")]
  [DBForeignKey("CustomerInformationType", "CustomerInformationTypeID")]
  public long CustomerInformationTypeID
  {
    get => this._CustomerInformationTypeID;
    set => this._CustomerInformationTypeID = value;
  }

  [DBProp("Information", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Information
  {
    get => this._Information;
    set => this._Information = value;
  }

  public CustomerInformationType InformationType
  {
    get => CustomerInformationType.Load(this.CustomerInformationTypeID);
  }

  public static List<CustomerInformation> LoadAll() => BaseDBObject.LoadAll<CustomerInformation>();

  public static CustomerInformation Load(long id) => BaseDBObject.Load<CustomerInformation>(id);

  public static List<CustomerInformation> LoadByCustomerID(long id)
  {
    return BaseDBObject.LoadByForeignKey<CustomerInformation>("CustomerID", (object) id);
  }
}
