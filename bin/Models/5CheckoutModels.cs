// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.PurchaseLinkStoreModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class PurchaseLinkStoreModel
{
  public List<PaymentInfoModel> PaymentInfoModelList { get; set; }

  public List<ProductInfoModel> ProductList { get; set; }

  public Account account { get; set; }

  public int Quantity { get; set; }
}
