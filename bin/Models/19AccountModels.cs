// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.StoreLinkGuidModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;

#nullable disable
namespace iMonnit.Models;

public class StoreLinkGuidModel
{
  public long AccountID { get; set; }

  public Guid LocalGuid { get; set; }

  public DateTime LocalExpirationDate { get; set; }
}
