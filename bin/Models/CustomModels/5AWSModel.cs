// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CustomModels.AWSEmailCallbackMailModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models.CustomModels;

public class AWSEmailCallbackMailModel
{
  public string source { get; set; }

  public List<string> destination { get; set; }

  public DateTime timestamp { get; set; }

  public List<AWSEmailCallbackMailHeaderModel> headers { get; set; }
}
