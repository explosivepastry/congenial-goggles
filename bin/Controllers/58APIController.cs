// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIWebHookProperty
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIWebHookProperty
{
  public APIWebHookProperty()
  {
  }

  public APIWebHookProperty(ExternalDataSubscriptionProperty prop)
  {
    this.WebHookPropertyID = prop.ExternalDataSubscriptionPropertyID;
    this.Type = prop.Name;
    this.StringValue = $"{prop.StringValue}={prop.StringValue2}";
    this.Key = prop.StringValue;
    this.Value = prop.StringValue2;
  }

  public long WebHookPropertyID { get; set; }

  public string Type { get; set; }

  public string StringValue { get; set; }

  public string Key { get; set; }

  public string Value { get; set; }
}
