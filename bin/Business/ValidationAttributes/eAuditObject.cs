// Decompiled with JetBrains decompiler
// Type: Monnit.eAuditObject
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

#nullable disable
namespace Monnit;

public enum eAuditObject
{
  Sensor = 1,
  Gateway = 2,
  NotificationTriggered = 3,
  Notification = 4,
  Network = 5,
  Customer = 6,
  Account = 7,
  Recipient = 8,
  SystemAction = 9,
  Schedule = 10, // 0x0000000A
  Permission = 11, // 0x0000000B
  Webhook = 12, // 0x0000000C
  OneviewLog = 13, // 0x0000000D
  CustomerGroup = 14, // 0x0000000E
  Credit = 15, // 0x0000000F
  DataMessageNote = 16, // 0x00000010
  NotificationNote = 17, // 0x00000011
  Map = 18, // 0x00000012
}
