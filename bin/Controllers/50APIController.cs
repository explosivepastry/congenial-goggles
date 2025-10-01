// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIGatewayDataPushConfiguration
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIGatewayDataPushConfiguration
{
  public APIGatewayDataPushConfiguration()
  {
  }

  public APIGatewayDataPushConfiguration(ExternalDataSubscription sub)
  {
    if (sub != null)
    {
      this.ExternalID = sub.ExternalID;
      this.ConnectionInfo = sub.ConnectionInfo1;
      this.verb = sub.verb;
      if (this.verb == "post")
      {
        this.ConnectionBody = sub.ConnectionInfo2;
        this.HeaderType = sub.ContentHeaderType;
      }
      else
      {
        this.ConnectionBody = "";
        this.HeaderType = "";
      }
      this.LastResult = sub.LastResult;
    }
    else
    {
      this.ExternalID = "";
      this.ConnectionInfo = "";
      this.verb = "";
      this.ConnectionBody = "";
      this.LastResult = "";
      this.HeaderType = "";
    }
  }

  public long GatewayID { get; set; }

  public string ExternalID { get; set; }

  public string ConnectionInfo { get; set; }

  public string verb { get; set; }

  public string ConnectionBody { get; set; }

  public string LastResult { get; set; }

  public string HeaderType { get; set; }
}
