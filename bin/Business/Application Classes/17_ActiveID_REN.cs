// Decompiled with JetBrains decompiler
// Type: Monnit.ActiveID_REN
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.Collections.Generic;
using System.Collections.Specialized;

#nullable disable
namespace Monnit;

public class ActiveID_REN : ActiveID
{
  public new static long MonnitApplicationID => 17;

  public new static string ApplicationName => "Repeater";

  public new static eApplicationProfileType ProfileType => eApplicationProfileType.Interval;

  public static ActiveID_REN Deserialize(string version, string serialized) => new ActiveID_REN();

  public override List<AppDatum> Datums => new List<AppDatum>();

  public static ActiveID_REN Create(byte[] sdm, int startIndex) => new ActiveID_REN();

  public new static void DefaultConfigurationSettings(Sensor sensor)
  {
    ActiveID.DefaultConfigurationSettings(sensor);
  }

  public new static Notification NotificationByApplicationID(Sensor sensor)
  {
    return new Notification()
    {
      SnoozeDuration = 60,
      CompareValue = "60",
      AccountID = sensor.AccountID,
      CompareType = eCompareType.Greater_Than,
      NotificationClass = eNotificationClass.Application,
      ApplicationID = ActiveID_REN.MonnitApplicationID,
      Version = MonnitApplicationBase.NotificationVersion
    };
  }

  public new static void SetProfileSettings(
    Sensor sensor,
    NameValueCollection collection,
    NameValueCollection viewData)
  {
    ActiveID.SetProfileSettings(sensor, collection, viewData);
  }

  public new static string HystForUI(Sensor sensor) => ActiveID.HystForUI(sensor);

  public new static string MinThreshForUI(Sensor sensor) => ActiveID.MinThreshForUI(sensor);

  public new static string MaxThreshForUI(Sensor sensor) => ActiveID.MaxThreshForUI(sensor);

  public override int GetHashCode() => base.GetHashCode();

  public static bool operator ==(ActiveID_REN left, ActiveID_REN right)
  {
    return left.Equals((MonnitApplicationBase) right);
  }

  public static bool operator !=(ActiveID_REN left, ActiveID_REN right)
  {
    return left.NotEqual((MonnitApplicationBase) right);
  }

  public static bool operator <(ActiveID_REN left, ActiveID_REN right)
  {
    return left.LessThan((MonnitApplicationBase) right);
  }

  public static bool operator <=(ActiveID_REN left, ActiveID_REN right)
  {
    return left.LessThanEqual((MonnitApplicationBase) right);
  }

  public static bool operator >(ActiveID_REN left, ActiveID_REN right)
  {
    return left.GreaterThan((MonnitApplicationBase) right);
  }

  public static bool operator >=(ActiveID_REN left, ActiveID_REN right)
  {
    return left.GreaterThanEqual((MonnitApplicationBase) right);
  }

  public override bool Equals(object obj)
  {
    return obj is ActiveID_REN && this.Equals((MonnitApplicationBase) (obj as ActiveID_REN));
  }
}
