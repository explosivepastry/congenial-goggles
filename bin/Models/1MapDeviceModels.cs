// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.HistoryPinModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.Models;

public class HistoryPinModel
{
  public LocationMessage FirstMessage { get; set; }

  public LocationMessage LastMessage { get; set; }

  public string Status { get; set; }

  public int Reports { get; set; }
}
