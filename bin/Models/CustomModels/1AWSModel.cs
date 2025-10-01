// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CustomModels.AWSEmailCallbackModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.Models.CustomModels;

public class AWSEmailCallbackModel
{
  public string eventType { get; set; }

  public AWSEmailCallbackBounceModel bounce { get; set; }

  public AWSEmailCallbackComplaintModel complaint { get; set; }

  public AWSEmailCallbackDeliveryModel delivery { get; set; }

  public AWSEmailCallbackMailModel mail { get; set; }
}
