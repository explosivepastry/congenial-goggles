// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.PurchaseConfirmationModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.Models;

public class PurchaseConfirmationModel
{
  public Account Account { get; set; }

  public string ConfirmationMessage { get; set; }

  public string PurchaseType { get; set; }

  public string PurchaseProduct { get; set; }

  public DateTime PurchaseExpiration { get; set; }

  public string PurchasedItemIDs { get; set; }

  public string CardMask { get; set; }

  public double PurchasePrice { get; set; }

  public string ActivationKey { get; set; }
}
