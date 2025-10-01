// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.GatewayTestingModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class GatewayTestingModel
{
  public List<GatewayTestingMessage> Messages { get; set; }

  public static GatewayTestingModel LoadByGatewayID(
    long gatewayID,
    DateTime fromDate,
    DateTime toDate)
  {
    return new Data.GatewayTestingModel.LoadByGatewayID(gatewayID, fromDate, toDate).Result;
  }
}
