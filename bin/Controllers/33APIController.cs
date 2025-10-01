// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIAdvancedNotificationParameterOption
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.API;

public class APIAdvancedNotificationParameterOption
{
  public APIAdvancedNotificationParameterOption()
  {
  }

  public APIAdvancedNotificationParameterOption(AdvancedNotificationParameterOption option)
  {
    this.Description = option.Display;
    this.ValueToEnter = option.Value;
  }

  public string Description { get; set; }

  public string ValueToEnter { get; set; }
}
