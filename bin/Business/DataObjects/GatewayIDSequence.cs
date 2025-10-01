// Decompiled with JetBrains decompiler
// Type: Monnit.GatewayIDSequence
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("GatewayIDSequence")]
public class GatewayIDSequence : BaseDBObject
{
  private long _GatewayIDSequenceID = long.MinValue;
  private DateTime _CreatedOnDate = DateTime.UtcNow;

  [DBProp("GatewayIDSequenceID", IsPrimaryKey = true)]
  public long GatewayIDSequenceID
  {
    get => this._GatewayIDSequenceID;
    set => this._GatewayIDSequenceID = value;
  }

  [DBProp("CreatedOnDate")]
  public DateTime CreatedOnDate
  {
    get => this._CreatedOnDate;
    set => this._CreatedOnDate = value;
  }

  public static GatewayIDSequence Load(long id) => BaseDBObject.Load<GatewayIDSequence>(id);
}
