// Decompiled with JetBrains decompiler
// Type: Monnit.InactivityNotification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class InactivityNotification : Notification
{
  private long _InactiveSensorID = long.MinValue;
  private long _InactiveGatewayID = long.MinValue;
  private long _SensorNotificationID = long.MinValue;
  private long _GatewayNotificationID = long.MinValue;

  [DBProp("InactiveSensorID")]
  public long InactiveSensorID
  {
    get => this._InactiveSensorID;
    set => this._InactiveSensorID = value;
  }

  [DBProp("InactiveGatewayID")]
  public long InactiveGatewayID
  {
    get => this._InactiveGatewayID;
    set => this._InactiveGatewayID = value;
  }

  [DBProp("SensorNotificationID")]
  public long SensorNotificationID
  {
    get => this._SensorNotificationID;
    set => this._SensorNotificationID = value;
  }

  [DBProp("GatewayNotificationID")]
  public long GatewayNotificationID
  {
    get => this._GatewayNotificationID;
    set => this._GatewayNotificationID = value;
  }

  public static List<InactivityNotification> LoadNonActivity()
  {
    return new Monnit.Data.InactivityNotification.LoadNonActivity().Result;
  }
}
