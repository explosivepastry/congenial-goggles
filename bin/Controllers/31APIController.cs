// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIAdvancedNotification
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIAdvancedNotification
{
  public APIAdvancedNotification()
  {
  }

  public APIAdvancedNotification(AdvancedNotification AdvNoti)
  {
    this.AdvancedNotificationID = AdvNoti.AdvancedNotificationID;
    this.Name = AdvNoti.Name;
    this.AllowsSensors = AdvNoti.HasSensorList;
    this.AllowsGateways = AdvNoti.HasGatewayList;
  }

  public long AdvancedNotificationID { get; set; }

  public string Name { get; set; }

  public bool AllowsSensors { get; set; }

  public bool AllowsGateways { get; set; }
}
