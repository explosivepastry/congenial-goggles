// Decompiled with JetBrains decompiler
// Type: Monnit.AnnouncementPopup
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit;

public class AnnouncementPopup : BaseDBObject
{
  public Announcement AnnouncementObj { get; set; }

  public bool CustomerViewed { get; set; }

  public long Prev { get; set; }

  public long Next { get; set; }
}
