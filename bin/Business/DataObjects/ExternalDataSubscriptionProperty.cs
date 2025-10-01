// Decompiled with JetBrains decompiler
// Type: Monnit.ExternalDataSubscriptionProperty
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ExternalDataSubscriptionProperty")]
public class ExternalDataSubscriptionProperty : BaseDBObject
{
  private long _ExternalDataSubscriptionPropertyID = long.MinValue;
  private long _ExternalDataSubscriptionID = long.MinValue;
  private string _Name = string.Empty;
  private string _DisplayName = string.Empty;
  private eExternalDataSubscriptionType _eExternalDataSubscriptionType;
  private string _StringValue = string.Empty;
  private string _StringValue2 = string.Empty;
  private byte[] _BinaryValue = (byte[]) null;

  [DBProp("ExternalDataSubscriptionPropertyID", IsPrimaryKey = true)]
  public long ExternalDataSubscriptionPropertyID
  {
    get => this._ExternalDataSubscriptionPropertyID;
    set => this._ExternalDataSubscriptionPropertyID = value;
  }

  [DBProp("ExternalDataSubscriptionID")]
  [DBForeignKey("ExternalDataSubscription", "ExternalDataSubscriptionID")]
  public long ExternalDataSubscriptionID
  {
    get => this._ExternalDataSubscriptionID;
    set => this._ExternalDataSubscriptionID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("DisplayName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string DisplayName
  {
    get => this._DisplayName;
    set => this._DisplayName = value;
  }

  [DBProp("eExternalDataSubscriptionType")]
  public eExternalDataSubscriptionType eExternalDataSubscriptionType
  {
    get => this._eExternalDataSubscriptionType;
    set => this._eExternalDataSubscriptionType = value;
  }

  [DBProp("StringValue", MaxLength = 2000, AllowNull = true)]
  public string StringValue
  {
    get => this._StringValue;
    set => this._StringValue = value;
  }

  [DBProp("StringValue2", MaxLength = 2000, AllowNull = true)]
  public string StringValue2
  {
    get => this._StringValue2;
    set => this._StringValue2 = value;
  }

  [DBProp("BinaryValue")]
  public byte[] BinaryValue
  {
    get => this._BinaryValue == null ? new byte[0] : this._BinaryValue;
    set => this._BinaryValue = value;
  }

  public static ExternalDataSubscriptionProperty Load(long id)
  {
    return BaseDBObject.Load<ExternalDataSubscriptionProperty>(id);
  }

  public static List<ExternalDataSubscriptionProperty> LoadByExternalDataSubscriptionID(
    long externalDataSubscriptionID)
  {
    return BaseDBObject.LoadByForeignKey<ExternalDataSubscriptionProperty>("ExternalDataSubscriptionID", (object) externalDataSubscriptionID);
  }
}
