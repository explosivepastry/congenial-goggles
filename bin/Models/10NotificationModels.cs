// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.NotifySensorDataModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.Models;

public class NotifySensorDataModel
{
  public bool isSendingSensorData { get; set; }

  public Sensor sens { get; set; }
}
