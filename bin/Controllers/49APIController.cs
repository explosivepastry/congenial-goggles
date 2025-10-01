// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIDataPushConfiguration
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIDataPushConfiguration
{
  public APIDataPushConfiguration()
  {
  }

  public APIDataPushConfiguration(ExternalDataSubscription sub)
  {
    if (sub != null)
    {
      this.ExternalID = sub.ExternalID;
      this.ConnectionInfo = sub.ConnectionInfo1;
      this.verb = sub.verb;
      this.LastResult = sub.LastResult;
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

  public long SensorID { get; set; }

  public string ExternalID { get; set; }

  public string ConnectionInfo { get; set; }

  public string verb { get; set; }

  public string ConnectionBody { get; set; }

  public string LastResult { get; set; }

  public string HeaderType { get; set; }
}
