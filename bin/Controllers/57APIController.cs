// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIWebHookGet
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIWebHookGet
{
  public APIWebHookGet()
  {
  }

  public APIWebHookGet(ExternalDataSubscription exdata)
  {
    this.ConnectionInfo = exdata.ConnectionInfo1;
    this.BrokenCount = exdata.BrokenCount;
    this.LastResult = string.IsNullOrEmpty(exdata.LastResult) ? "Not Configured" : exdata.LastResult;
    this.SendingEnabled = exdata.DoSend;
    this.RetriesEnabled = exdata.DoRetry;
    this.SendWithoutDataMessage = exdata.SendWithoutDataMessage;
    this.AuthenticationEnabled = !string.IsNullOrEmpty(exdata.Username);
    this.SendRawData = exdata.SendRawData;
  }

  public string ConnectionInfo { get; set; }

  public int BrokenCount { get; set; }

  public string LastResult { get; set; }

  public bool SendingEnabled { get; set; }

  public bool RetriesEnabled { get; set; }

  public bool SendWithoutDataMessage { get; set; }

  public bool AuthenticationEnabled { get; set; }

  public bool SendRawData { get; set; }
}
