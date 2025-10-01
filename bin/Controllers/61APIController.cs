// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIWebHookAttemptBody
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.API;

public class APIWebHookAttemptBody
{
  public APIWebHookAttemptBody()
  {
  }

  public APIWebHookAttemptBody(string body) => this.Body = body;

  public string Body { get; set; }
}
