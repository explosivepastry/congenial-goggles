// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.MapDeviceModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using RedefineImpossible;

#nullable disable
namespace iMonnit.Models;

public class MapDeviceModel : BaseDBObject
{
  private long _DeviceID = long.MinValue;
  private long _VisualMapID = long.MinValue;
  private string _Name = string.Empty;
  private long _ApplicationID = long.MinValue;
  private long _GatewayTypeID = long.MinValue;

  [DBProp("DeviceID")]
  public long DeviceID
  {
    get => this._DeviceID;
    set => this._DeviceID = value;
  }

  [DBProp("VisualMapID")]
  public long VisualMapID
  {
    get => this._VisualMapID;
    set => this._VisualMapID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  [DBProp("ApplicationID")]
  public long ApplicationID
  {
    get => this._ApplicationID;
    set => this._ApplicationID = value;
  }

  [DBProp("GatewayTypeID")]
  public long GatewayTypeID
  {
    get => this._GatewayTypeID;
    set => this._GatewayTypeID = value;
  }
}
