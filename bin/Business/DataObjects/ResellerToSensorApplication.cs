// Decompiled with JetBrains decompiler
// Type: Monnit.ResellerToSensorApplication
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace Monnit;

[MetadataType(typeof (ResellerToSensorApplicationMetaData))]
[DBClass("ResellerToSensorApplication")]
public class ResellerToSensorApplication : BaseDBObject
{
  private long _ResellerToSensorApplicationID = long.MinValue;
  private long _ResellerAccountID = long.MinValue;
  private long _SensorApplicationID = long.MinValue;

  [DBProp("ResellerToSensorApplicationID", IsPrimaryKey = true)]
  public long ResellerToSensorApplicationID
  {
    get => this._ResellerToSensorApplicationID;
    set => this._ResellerToSensorApplicationID = value;
  }

  [DBProp("ResellerAccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long ResellerAccountID
  {
    get => this._ResellerAccountID;
    set => this._ResellerAccountID = value;
  }

  [DBProp("SensorApplicationID")]
  [DBForeignKey("SensorApplication", "SensorApplicationID")]
  public long SensorApplicationID
  {
    get => this._SensorApplicationID;
    set => this._SensorApplicationID = value;
  }

  public static void DeleteByResellerID(long resellerID, long monnitApplicationID)
  {
    Monnit.Data.ResellerToSensorApplication.DeleteByResellerID deleteByResellerId = new Monnit.Data.ResellerToSensorApplication.DeleteByResellerID(resellerID, monnitApplicationID);
  }

  public static List<ResellerToSensorApplication> LoadByResellerID(long resellerID)
  {
    return new Monnit.Data.ResellerToSensorApplication.LoadByResellerID(resellerID).Result;
  }
}
