// Decompiled with JetBrains decompiler
// Type: Monnit.PowerSource
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("PowerSource")]
public class PowerSource : BaseDBObject
{
  private long _PowerSourceID = long.MinValue;
  private string _Name = string.Empty;
  private double _VoltageForZeroPercent = (double) int.MinValue;
  private double _VoltageForTenPercent = (double) int.MinValue;
  private double _VoltageForTwentyFivePercent = (double) int.MinValue;
  private double _VoltageForFiftyPercent = (double) int.MinValue;
  private double _VoltageForSeventyFivePercent = (double) int.MinValue;
  private double _VoltageForOneHundredPercent = (double) int.MinValue;
  private int _MinimumRecommendedHeartbeat;

  [DBProp("PowerSourceID", IsPrimaryKey = true)]
  public long PowerSourceID
  {
    get => this._PowerSourceID;
    set => this._PowerSourceID = value;
  }

  [DBProp("Name", AllowNull = false, MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  [DBProp("VoltageForZeroPercent")]
  public double VoltageForZeroPercent
  {
    get => this._VoltageForZeroPercent;
    set => this._VoltageForZeroPercent = value;
  }

  [DBProp("VoltageForTenPercent")]
  public double VoltageForTenPercent
  {
    get => this._VoltageForTenPercent;
    set => this._VoltageForTenPercent = value;
  }

  [DBProp("VoltageForTwentyFivePercent")]
  public double VoltageForTwentyFivePercent
  {
    get => this._VoltageForTwentyFivePercent;
    set => this._VoltageForTwentyFivePercent = value;
  }

  [DBProp("VoltageForFiftyPercent")]
  public double VoltageForFiftyPercent
  {
    get => this._VoltageForFiftyPercent;
    set => this._VoltageForFiftyPercent = value;
  }

  [DBProp("VoltageForSeventyFivePercent")]
  public double VoltageForSeventyFivePercent
  {
    get => this._VoltageForSeventyFivePercent;
    set => this._VoltageForSeventyFivePercent = value;
  }

  [DBProp("VoltageForOneHundredPercent")]
  public double VoltageForOneHundredPercent
  {
    get => this._VoltageForOneHundredPercent;
    set => this._VoltageForOneHundredPercent = value;
  }

  [DBProp("MinimumRecommendedHeartbeat", DefaultValue = 60)]
  public int MinimumRecommendedHeartbeat
  {
    get
    {
      if (this._MinimumRecommendedHeartbeat < 0)
        this._MinimumRecommendedHeartbeat = 60;
      return this._MinimumRecommendedHeartbeat;
    }
    set => this._MinimumRecommendedHeartbeat = value;
  }

  public double Percent(double voltage)
  {
    voltage = (double) ((voltage * 1000.0).ToInt() & (int) short.MaxValue) / 1000.0;
    if (voltage <= this.VoltageForZeroPercent)
      return 0.0;
    if (voltage > this.VoltageForZeroPercent && voltage <= this.VoltageForTenPercent)
      return voltage.LinearInterpolation(this.VoltageForZeroPercent, 0.0, this.VoltageForTenPercent, 10.0);
    if (voltage > this.VoltageForTenPercent && voltage <= this.VoltageForTwentyFivePercent)
      return voltage.LinearInterpolation(this.VoltageForTenPercent, 10.0, this.VoltageForTwentyFivePercent, 25.0);
    if (voltage > this.VoltageForTwentyFivePercent && voltage <= this.VoltageForFiftyPercent)
      return voltage.LinearInterpolation(this.VoltageForTwentyFivePercent, 25.0, this.VoltageForFiftyPercent, 50.0);
    if (voltage > this.VoltageForFiftyPercent && voltage <= this.VoltageForSeventyFivePercent)
      return voltage.LinearInterpolation(this.VoltageForFiftyPercent, 50.0, this.VoltageForSeventyFivePercent, 75.0);
    return voltage > this.VoltageForSeventyFivePercent && voltage <= this.VoltageForOneHundredPercent ? voltage.LinearInterpolation(this.VoltageForSeventyFivePercent, 75.0, this.VoltageForOneHundredPercent, 100.0) : 100.0;
  }

  public static PowerSource Load(long ID)
  {
    string key = $"PowerSource_{ID}";
    PowerSource powerSource = TimedCache.RetrieveObject<PowerSource>(key);
    if (powerSource == null)
    {
      powerSource = BaseDBObject.Load<PowerSource>(ID);
      if (powerSource != null)
        TimedCache.AddObjectToCach(key, (object) powerSource, new TimeSpan(6, 0, 0));
    }
    return powerSource;
  }

  public static List<PowerSource> LoadAll()
  {
    string key = "PowerSourceList";
    List<PowerSource> powerSourceList = TimedCache.RetrieveObject<List<PowerSource>>(key);
    if (powerSourceList == null)
    {
      powerSourceList = BaseDBObject.LoadAll<PowerSource>();
      if (powerSourceList != null)
        TimedCache.AddObjectToCach(key, (object) powerSourceList, new TimeSpan(6, 0, 0));
    }
    return powerSourceList;
  }
}
