// Decompiled with JetBrains decompiler
// Type: Monnit.eNotificationDaySchedule
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System.ComponentModel;

#nullable disable
namespace Monnit;

public enum eNotificationDaySchedule
{
  [Description("All Day")] All_Day,
  [Description("Off")] Off,
  [Description("Between")] Between,
  [Description("Before & After")] Before_and_After,
  [Description("Before")] Before,
  [Description("After")] After,
}
