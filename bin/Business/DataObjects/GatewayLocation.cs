// Decompiled with JetBrains decompiler
// Type: Monnit.GatewayLocation
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

#nullable disable
namespace Monnit;

[DBClass("GatewayLocation")]
public class GatewayLocation : BaseDBObject
{
  private Guid _GatewayLocationGUID = Guid.Empty;
  private long _GatewayID = long.MinValue;
  private eGatewayLocationType _GatewayLocationType = eGatewayLocationType.Lat_Long;
  private byte[] _LocationData = (byte[]) null;
  private DateTime _Date = DateTime.MinValue;
  private double _Latitude = double.MinValue;
  private double _Longitude = double.MinValue;
  private double _Accuracy = double.MinValue;

  [DBProp("GatewayLocationGUID", IsGuidPrimaryKey = true)]
  public Guid GatewayLocationGUID
  {
    get => this._GatewayLocationGUID;
    set => this._GatewayLocationGUID = value;
  }

  [DBProp("GatewayID", AllowNull = false)]
  public long GatewayID
  {
    get => this._GatewayID;
    set => this._GatewayID = value;
  }

  [DBProp("GatewayLocationType")]
  public eGatewayLocationType GatewayLocationType
  {
    get => this._GatewayLocationType;
    set => this._GatewayLocationType = value;
  }

  [DBProp("LocationData")]
  public byte[] LocationData
  {
    get => this._LocationData;
    set => this._LocationData = value;
  }

  [DBProp("Date", AllowNull = false)]
  public DateTime Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [DBProp("Latitude")]
  public double Latitude
  {
    get => this._Latitude;
    set => this._Latitude = value;
  }

  [DBProp("Longitude")]
  public double Longitude
  {
    get => this._Longitude;
    set => this._Longitude = value;
  }

  [DBProp("Accuracy")]
  public double Accuracy
  {
    get => this._Accuracy;
    set => this._Accuracy = value;
  }

  public void ConvertCellGateway_v1()
  {
    XDocument xdocument = XDocument.Load(string.Format("http://cellid.labs.ericsson.net/xml/elookup?serving={0},{1},{2},{3},{4},{5},{6}&neighbor={7},{8},{9},{10},{11},{12}&neighbor={14},{15},{16},{17},{18},{19}&key={21}", (object) "G", (object) new string(Encoding.ASCII.GetChars(this.LocationData, 0, 4)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 4, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 9, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 14, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 29, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 44, 5)).Replace("\0", ""), (object) "G", (object) new string(Encoding.ASCII.GetChars(this.LocationData, 0, 4)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 4, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 9, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 19, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 34, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 44, 5)).Replace("\0", ""), (object) "G", (object) new string(Encoding.ASCII.GetChars(this.LocationData, 0, 4)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 4, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 9, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 24, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 39, 5)).Replace("\0", ""), (object) new string(Encoding.ASCII.GetChars(this.LocationData, 44, 5)).Replace("\0", ""), (object) "twXRXsqC7VJ143q8tiHDkoznCi8ko6MAUno7VlYt"));
    this.Latitude = xdocument.Descendants((XName) "latitude").Single<XElement>().Value.ToDouble();
    this.Longitude = xdocument.Descendants((XName) "longitude").Single<XElement>().Value.ToDouble();
    this.Accuracy = xdocument.Descendants((XName) "accuracy").Single<XElement>().Value.ToDouble();
  }

  public static GatewayLocation Load(long id) => BaseDBObject.Load<GatewayLocation>(id);

  public static List<GatewayLocation> LoadByGatewayID(long gatewayID)
  {
    return new Monnit.Data.GatewayLocation.LoadByGatewayID(gatewayID).Result;
  }

  public override void Save()
  {
    if (this.GatewayLocationGUID == Guid.Empty)
    {
      switch (this.GatewayLocationType)
      {
        case eGatewayLocationType.Lat_Long:
          if (this.Latitude == double.MinValue)
          {
            this.Latitude = (double) BitConverter.ToSingle(this.LocationData, 0);
            this.Longitude = (double) BitConverter.ToSingle(this.LocationData, 4);
            this.Accuracy = (double) BitConverter.ToInt32(this.LocationData, 12);
            break;
          }
          break;
        case eGatewayLocationType.CellGateway_v1:
          try
          {
            Gateway gateway = Gateway.Load(this.GatewayID);
            if (gateway != null)
            {
              char[] charArray = this.LocationData[50].ToInt().ToString().ToCharArray();
              string empty = string.Empty;
              foreach (char ch in charArray)
                empty += $"{ch}.";
              string str = empty.TrimEnd('.');
              gateway.GatewayFirmwareVersion = str;
              if (this.LocationData.Length > 51)
                gateway.CurrentSignalStrength = (int) this.LocationData[51];
              gateway.Save();
              break;
            }
            break;
          }
          catch (Exception ex)
          {
            ex.Log($"GatewayLocation.Save/ | GatewayID = {this.GatewayID}, GatewayLocationType = {this.GatewayLocationType}");
            break;
          }
      }
    }
    base.Save();
  }
}
