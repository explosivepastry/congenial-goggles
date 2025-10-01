// Decompiled with JetBrains decompiler
// Type: Monnit.ConfigData
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Configuration;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("ConfigData")]
public class ConfigData : BaseDBObject
{
  private long _ConfigDataID = long.MinValue;
  private string _Key = string.Empty;
  private string _Value = string.Empty;

  [DBProp("ConfigDataID", IsPrimaryKey = true)]
  public long ConfigDataID
  {
    get => this._ConfigDataID;
    set => this._ConfigDataID = value;
  }

  [DBProp("KeyName", MaxLength = 255 /*0xFF*/)]
  public string Key
  {
    get => this._Key;
    set
    {
      if (value == null)
        this._Key = string.Empty;
      else
        this._Key = value;
    }
  }

  [DBProp("Value", MaxLength = 255 /*0xFF*/)]
  public string Value
  {
    get => this._Value;
    set
    {
      if (value == null)
        this._Value = string.Empty;
      else
        this._Value = value;
    }
  }

  public static ConfigData Load(long ID) => BaseDBObject.Load<ConfigData>(ID);

  private static System.Collections.Generic.List<ConfigData> List
  {
    get
    {
      System.Collections.Generic.List<ConfigData> list = TimedCache.RetrieveObject<System.Collections.Generic.List<ConfigData>>("CachedConfig_DataList");
      if (list == null)
      {
        list = ConfigData.LoadAll();
        TimedCache.AddObjectToCach("CachedConfig_DataList", (object) list, new TimeSpan(0, 2, 0));
      }
      return list;
    }
  }

  public static ConfigData Find(string key)
  {
    ConfigData configData = ConfigData.List.Where<ConfigData>((Func<ConfigData, bool>) (cd => cd.Key == key)).FirstOrDefault<ConfigData>();
    if (configData == null)
    {
      configData = new ConfigData();
      configData.Key = key;
    }
    return configData;
  }

  public static string FindValue(string key) => ConfigData.Find(key).Value;

  public static System.Collections.Generic.List<ConfigData> LoadAll()
  {
    return BaseDBObject.LoadAll<ConfigData>();
  }

  public override void Save()
  {
    if (this.ConfigDataID == long.MinValue)
      TimedCache.RemoveObject("CachedConfig_DataList");
    base.Save();
  }

  public override void Delete()
  {
    TimedCache.RemoveObject("CachedConfig_DataList");
    base.Delete();
  }

  public static string AppSettings(string key)
  {
    string appSetting = ConfigurationManager.AppSettings[key];
    if (string.IsNullOrEmpty(appSetting))
    {
      appSetting = ConfigData.FindValue(key);
      if (string.IsNullOrEmpty(appSetting))
        return "";
    }
    return appSetting;
  }

  public static string AppSettings(string key, string defaultValue)
  {
    string str = ConfigData.AppSettings(key);
    return string.IsNullOrEmpty(str) ? defaultValue : str;
  }

  public static void SetAppSettings(string key, string value)
  {
    ConfigData configData = ConfigData.Find(key);
    configData.Value = value;
    configData.Save();
    TimedCache.RemoveObject(key);
  }
}
