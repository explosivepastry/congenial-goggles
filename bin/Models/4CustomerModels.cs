// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CustomerContactInfoModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class CustomerContactInfoModel
{
  public Customer Customer { get; set; }

  public List<CustomerInformation> CustomerInformationList { get; set; }
}
