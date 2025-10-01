// Decompiled with JetBrains decompiler
// Type: Monnit.VisualMap
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

#nullable disable
namespace Monnit;

[DBClass("VisualMap")]
public class VisualMap : BaseDBObject
{
  private long _VisualMapID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _Name = string.Empty;
  private int _Width = int.MinValue;
  private int _Height = int.MinValue;
  private byte[] _Image = new byte[0];
  private eMapType _MapType = eMapType.StaticMap;
  private Dictionary<long, VisualMapSensor> _PlacedSensors;
  private Dictionary<long, VisualMapGateway> _PlacedGateways;

  [DBProp("VisualMapID", IsPrimaryKey = true)]
  public long VisualMapID
  {
    get => this._VisualMapID;
    set => this._VisualMapID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [Required]
  [MinLength(10, ErrorMessage = "Name cannot be blank")]
  [DBProp("Name", AllowNull = false, MaxLength = 255 /*0xFF*/, International = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [DBProp("Width", AllowNull = true)]
  public int Width
  {
    get => this._Width;
    set => this._Width = value;
  }

  [DBProp("Height", AllowNull = true)]
  public int Height
  {
    get => this._Height;
    set => this._Height = value;
  }

  [DBProp("Image", AllowNull = true)]
  public byte[] Image
  {
    get => this._Image;
    set => this._Image = value;
  }

  [DBProp("MapType", AllowNull = false, DefaultValue = 1)]
  public eMapType MapType
  {
    get => this._MapType;
    set => this._MapType = value;
  }

  public Dictionary<long, VisualMapSensor> PlacedSensors
  {
    get
    {
      if (this._PlacedSensors == null)
      {
        this._PlacedSensors = new Dictionary<long, VisualMapSensor>();
        foreach (VisualMapSensor visualMapSensor in VisualMapSensor.LoadByVisualMapID(this.VisualMapID))
        {
          if (this._PlacedSensors.ContainsKey(visualMapSensor.SensorID))
            visualMapSensor.Delete();
          else
            this._PlacedSensors.Add(visualMapSensor.SensorID, visualMapSensor);
        }
      }
      return this._PlacedSensors;
    }
  }

  public Dictionary<long, VisualMapGateway> PlacedGateways
  {
    get
    {
      if (this._PlacedGateways == null)
      {
        this._PlacedGateways = new Dictionary<long, VisualMapGateway>();
        foreach (VisualMapGateway visualMapGateway in VisualMapGateway.LoadByVisualMapID(this.VisualMapID))
        {
          if (this._PlacedGateways.ContainsKey(visualMapGateway.GatewayID))
            visualMapGateway.Delete();
          else
            this._PlacedGateways.Add(visualMapGateway.GatewayID, visualMapGateway);
        }
      }
      return this._PlacedGateways;
    }
  }

  public Bitmap Bitmap
  {
    get => VisualMap.FromBytes(this.Image);
    set
    {
      int width = value.Width;
      int height = value.Height;
      int num = (int) Math.Pow(2.0, (double) ((int) Math.Ceiling(Math.Log((double) Math.Max(width, height), 2.0)) + 1));
      if (width > height)
      {
        this.Width = num;
        this.Height = num * height / width;
      }
      else
      {
        this.Width = num * width / height;
        this.Height = num;
      }
      this.Image = VisualMap.FromBitmap(value);
    }
  }

  public static byte[] FromBitmap(Bitmap source)
  {
    MemoryStream memoryStream = new MemoryStream();
    source.Save((Stream) memoryStream, ImageFormat.Bmp);
    return memoryStream.ToArray();
  }

  public static Bitmap FromBytes(byte[] bytes)
  {
    using (MemoryStream memoryStream = new MemoryStream(bytes))
      return new Bitmap((Stream) memoryStream);
  }

  public static VisualMap Load(long visualMapID) => BaseDBObject.Load<VisualMap>(visualMapID);

  public override void Delete()
  {
    foreach (long key in this.PlacedSensors.Keys)
      this.PlacedSensors[key].Delete();
    base.Delete();
  }

  public static void DeleteByMapID(long visualMapID)
  {
    Monnit.Data.VisualMap.VisualMapDelete visualMapDelete = new Monnit.Data.VisualMap.VisualMapDelete(visualMapID);
  }

  public static List<VisualMap> LoadByAccountID(long accountID)
  {
    return new Monnit.Data.VisualMap.LoadByAccountID(accountID).Result;
  }

  public static DataTable LoadGatewaysByAccountID(
    long accountID,
    long visualMapID,
    bool isGPSUnlocked,
    long csNetID,
    string name)
  {
    return new Monnit.Data.VisualMap.LoadGateways(accountID, visualMapID, isGPSUnlocked, csNetID, name).Result;
  }
}
