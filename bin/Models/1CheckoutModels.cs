// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.ReceiptModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;

#nullable disable
namespace iMonnit.Models;

public class ReceiptModel
{
  public Payment Payment { get; set; }

  public OnlineOrder Order { get; set; }
}
