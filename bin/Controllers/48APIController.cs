// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIValidationError
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.API;

public class APIValidationError
{
  public APIValidationError()
  {
  }

  public APIValidationError(string parameter, string error)
  {
    this.Parameter = parameter;
    this.Error = error;
  }

  public string Parameter { get; set; }

  public string Error { get; set; }
}
