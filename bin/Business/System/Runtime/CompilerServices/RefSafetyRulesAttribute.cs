// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RefSafetyRulesAttribute
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using Microsoft.CodeAnalysis;
using System.Runtime.InteropServices;

#nullable disable
namespace System.Runtime.CompilerServices;

[CompilerGenerated]
[Embedded]
[AttributeUsage(AttributeTargets.Module, AllowMultiple = false, Inherited = false)]
internal sealed class RefSafetyRulesAttribute : Attribute
{
  public readonly int Version;

  public RefSafetyRulesAttribute([In] int obj0) => this.Version = obj0;
}
