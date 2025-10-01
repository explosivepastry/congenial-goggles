// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.RefreshGatewayModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.Models;

public class RefreshGatewayModel
{
  public RefreshGatewayModel(long gatewayID)
  {
    this.GatewayID = gatewayID;
    this.StatusImageUrl = "";
    this.Date = "";
    this.SignalImageUrl = "";
    this.PowerImageUrl = "";
    this.NotificationPaused = false;
    this.IsDirty = false;
  }

  public long GatewayID { get; set; }

  public string StatusImageUrl { get; set; }

  public string Date { get; set; }

  public string SignalImageUrl { get; set; }

  public string PowerImageUrl { get; set; }

  public bool NotificationPaused { get; set; }

  public bool IsDirty { get; set; }
}
