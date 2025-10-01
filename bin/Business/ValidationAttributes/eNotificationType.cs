// Decompiled with JetBrains decompiler
// Type: Monnit.eNotificationType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

#nullable disable
namespace Monnit;

public enum eNotificationType
{
  Email = 1,
  SMS = 2,
  Both = 3,
  Local_Notifier = 4,
  Control = 5,
  Phone = 6,
  HTTP = 7,
  Android = 8,
  Apple = 9,
  Local_Notifier_Display = 10, // 0x0000000A
  SerialDataBridge_Terminal = 11, // 0x0000000B
  SystemAction = 12, // 0x0000000C
  Thermostat = 13, // 0x0000000D
  Group = 14, // 0x0000000E
  Push_Message = 15, // 0x0000000F
  ResetAccumulator = 16, // 0x00000010
}
