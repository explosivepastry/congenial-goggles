// Decompiled with JetBrains decompiler
// Type: Monnit.TimeCacheObject
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using System;

#nullable disable
namespace Monnit;

public class TimeCacheObject
{
  private DateTime _Time = DateTime.Now;
  private TimeSpan _CacheLength;
  private object _Obj = (object) null;

  public object Obj => this._Obj;

  public DateTime ValidUntill => this._Time.Add(this._CacheLength);

  public bool IsValid => this.ValidUntill > DateTime.Now;

  public void Roll() => this._Time = DateTime.Now;

  public TimeCacheObject(object obj, TimeSpan cacheLength)
  {
    this._Obj = obj;
    this._CacheLength = cacheLength;
  }
}
