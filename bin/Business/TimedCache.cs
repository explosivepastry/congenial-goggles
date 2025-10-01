// Decompiled with JetBrains decompiler
// Type: Monnit.TimedCache
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class TimedCache
{
  protected static Dictionary<string, TimeCacheObject> Cache = new Dictionary<string, TimeCacheObject>();

  public static void AddObjectToCach(string key, object obj)
  {
    TimedCache.AddObjectToCach(key, obj, new TimeSpan(0, 30, 0));
  }

  public static void AddObjectToCach(string key, object obj, TimeSpan cacheLength)
  {
    TimedCache.RemoveObject(key);
    try
    {
      lock (TimedCache.Cache)
        TimedCache.Cache.Add(key, new TimeCacheObject(obj, cacheLength));
    }
    catch (Exception ex)
    {
      if (ex.ToString().Contains("An item with the same key has already been added"))
        return;
      ex.Log($"TimedCache.AddObjectToCache: key='{key}'  object='{obj.ToString()}'");
    }
  }

  public static void AddObjectToCach(string key, object obj, DateTime expirationDate)
  {
    if (!(expirationDate > DateTime.Now))
      return;
    TimedCache.AddObjectToCach(key, obj, expirationDate.Subtract(DateTime.Now));
  }

  public static bool ContainsKey(string key)
  {
    TimeCacheObject timeCacheObject;
    if (TimedCache.Cache.TryGetValue(key, out timeCacheObject))
    {
      if (timeCacheObject.IsValid)
        return true;
      TimedCache.RemoveObject(key);
    }
    return false;
  }

  public static DateTime ValidUntill(string key)
  {
    TimeCacheObject timeCacheObject;
    if (TimedCache.Cache.TryGetValue(key, out timeCacheObject))
    {
      if (timeCacheObject.IsValid)
        return timeCacheObject.ValidUntill;
      TimedCache.RemoveObject(key);
    }
    return DateTime.MinValue;
  }

  public static T RetrieveObject<T>(string key)
  {
    TimeCacheObject timeCacheObject;
    if (!string.IsNullOrEmpty(key) && TimedCache.Cache.TryGetValue(key, out timeCacheObject))
    {
      if (timeCacheObject != null && timeCacheObject.IsValid && timeCacheObject.Obj is T)
        return (T) timeCacheObject.Obj;
      TimedCache.RemoveObject(key);
    }
    return default (T);
  }

  public static void RemoveObject(string key)
  {
    lock (TimedCache.Cache)
    {
      if (!TimedCache.Cache.ContainsKey(key))
        return;
      TimedCache.Cache.Remove(key);
    }
  }

  public static void RollTime(string key)
  {
    if (!TimedCache.Cache.ContainsKey(key))
      return;
    TimedCache.Cache[key].Roll();
  }

  public static void ClearAll()
  {
    lock (TimedCache.Cache)
      TimedCache.Cache.Clear();
  }

  public static DateTime LastCacheReset
  {
    get
    {
      try
      {
        ConfigData configData = ConfigData.Find(nameof (LastCacheReset));
        if (configData != null)
          configData = ConfigData.Load(configData.ConfigDataID);
        return configData == null ? DateTime.MinValue : configData.Value.ToDateTime();
      }
      catch
      {
        return DateTime.MinValue;
      }
    }
    set
    {
      ConfigData configData = ConfigData.Find(nameof (LastCacheReset));
      if (configData == null)
        configData = new ConfigData()
        {
          Key = nameof (LastCacheReset)
        };
      configData.Value = value.ToString();
      configData.Save();
    }
  }
}
