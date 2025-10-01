// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.PreAggregatePageModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace iMonnit.Models;

public class PreAggregatePageModel
{
  public PreAggregatePageModel()
  {
  }

  public List<DataMessage> DataMessageList { get; set; }

  public List<PreAggregatedData> PreAggregateList { get; set; }

  public Sensor sensor { get; set; }

  public PreAggregatePageModel(DataSet ds, Sensor sens)
  {
    try
    {
      this.DataMessageList = BaseDBObject.Load<DataMessage>(ds.Tables[0]);
      this.PreAggregateList = BaseDBObject.Load<PreAggregatedData>(ds.Tables[1]);
    }
    catch
    {
      this.DataMessageList = new List<DataMessage>();
      this.PreAggregateList = new List<PreAggregatedData>();
    }
    this.sensor = sens;
  }

  public PreAggregatePageModel(List<PreAggregatedData> ds, Sensor sens)
  {
    this.DataMessageList = new List<DataMessage>();
    this.PreAggregateList = ds;
    this.sensor = sens;
  }
}
