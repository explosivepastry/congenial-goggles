// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APICableMetaData
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using System;

#nullable disable
namespace iMonnit.API;

public class APICableMetaData
{
  public APICableMetaData()
  {
  }

  public APICableMetaData(Cable cable)
  {
    this.CableID = cable.CableID;
    this.CreateDate = cable.CreateDate;
    this.SKU = cable.SKU;
    this.ApplicationID = cable.ApplicationID;
    this.CableMinorRevision = cable.CableMinorRevision;
    this.CableMajorRevision = cable.CableMajorRevision;
  }

  public long CableID { get; set; }

  public DateTime CreateDate { get; set; }

  public string SKU { get; set; }

  public long ApplicationID { get; set; }

  public int CableMinorRevision { get; set; }

  public int CableMajorRevision { get; set; }
}
