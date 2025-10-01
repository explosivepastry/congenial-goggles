// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CustomModels.AWSCallbackModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Newtonsoft.Json;

#nullable disable
namespace iMonnit.Models.CustomModels;

public class AWSCallbackModel
{
  public string Type { get; set; }

  public string MessageId { get; set; }

  public string TopicArn { get; set; }

  public string Subject { get; set; }

  public string Message { get; set; }

  public AWSEmailCallbackModel ParsedMessage
  {
    get => JsonConvert.DeserializeObject<AWSEmailCallbackModel>(this.Message);
  }
}
