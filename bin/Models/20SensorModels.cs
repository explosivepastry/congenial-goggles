// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.DeviceStatusModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace iMonnit.Models;

public class DeviceStatusModel : BaseDBObject
{
  [DBProp("Type")]
  public string Type { get; set; }

  [DBProp("Active")]
  public int Active { get; set; }

  [DBProp("Total")]
  public int Total { get; set; }

  [DBProp("Alerting")]
  public int Alerting { get; set; }

  public static List<DeviceStatusModel> DeviceStatusModel_LoadByAccountID(long AccountID)
  {
    return new Data.DeviceStatusModel.LoadByAccountID(AccountID).Result;
  }
}
