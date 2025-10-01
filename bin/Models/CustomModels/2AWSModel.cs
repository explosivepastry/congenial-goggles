// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CustomModels.AWSEmailCallbackBounceModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;

#nullable disable
namespace iMonnit.Models.CustomModels;

public class AWSEmailCallbackBounceModel
{
  public string bounceType { get; set; }

  public string bounceSubType { get; set; }

  public DateTime timestamp { get; set; }
}
