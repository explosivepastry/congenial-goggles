// Decompiled with JetBrains decompiler
// Type: Monnit.ReportDistribution
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ReportDistribution")]
public class ReportDistribution : BaseDBObject
{
  private long _ReportDistributionID = long.MinValue;
  private long _ReportScheduleID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private Customer _Customer;

  [DBProp("ReportDistributionID", IsPrimaryKey = true)]
  public long ReportDistributionID
  {
    get => this._ReportDistributionID;
    set => this._ReportDistributionID = value;
  }

  [DBProp("ReportScheduleID")]
  [DBForeignKey("ReportSchedule", "ReportScheduleID")]
  public long ReportScheduleID
  {
    get => this._ReportScheduleID;
    set => this._ReportScheduleID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  public Customer Customer
  {
    get
    {
      if (this._Customer == null)
        this._Customer = Customer.Load(this._CustomerID);
      return this._Customer;
    }
  }

  public static ReportDistribution Load(long id) => BaseDBObject.Load<ReportDistribution>(id);

  public static List<ReportDistribution> LoadBySchedule(long reportScheduleID)
  {
    return BaseDBObject.LoadByForeignKey<ReportDistribution>("ReportScheduleID", (object) reportScheduleID);
  }
}
