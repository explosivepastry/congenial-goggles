// Decompiled with JetBrains decompiler
// Type: Monnit.SamlEndpoint
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SamlEndpoint")]
public class SamlEndpoint : BaseDBObject
{
  private long _SamlEndpointID = long.MinValue;
  private string _Name = string.Empty;
  private string _EndpointURL = string.Empty;
  private string _Certificate = string.Empty;

  [DBProp("SamlEndpointID", IsPrimaryKey = true)]
  public long SamlEndpointID
  {
    get => this._SamlEndpointID;
    set => this._SamlEndpointID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("EndpointURL", MaxLength = 1000, AllowNull = false)]
  public string EndpointURL
  {
    get => this._EndpointURL;
    set => this._EndpointURL = value;
  }

  [DBProp("Certificate", MaxLength = 8000, AllowNull = false)]
  public string Certificate
  {
    get => this._Certificate;
    set => this._Certificate = value;
  }

  public static List<SamlEndpoint> All => BaseDBObject.LoadAll<SamlEndpoint>();

  public static SamlEndpoint Load(long id) => BaseDBObject.Load<SamlEndpoint>(id);
}
