// Decompiled with JetBrains decompiler
// Type: Monnit.ZipcodeInfo
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("ZipcodeInfo")]
public class ZipcodeInfo : BaseDBObject
{
  private long _ZipcodeInfoID = long.MinValue;
  private string _Zipcode = string.Empty;
  private string _StateAbrev = string.Empty;
  private string _City = string.Empty;
  private string _State = string.Empty;
  private double _Latitude = double.MinValue;
  private double _Longitude = double.MinValue;

  [DBProp("ZipcodeInfoID", IsPrimaryKey = true)]
  public long ZipcodeInfoID
  {
    get => this._ZipcodeInfoID;
    set => this._ZipcodeInfoID = value;
  }

  [DBProp("Zipcode", AllowNull = false, MaxLength = 10)]
  public string Zipcode
  {
    get => this._Zipcode;
    set => this._Zipcode = value;
  }

  [DBProp("StateAbrev", AllowNull = false, MaxLength = 10)]
  public string StateAbrev
  {
    get => this._StateAbrev;
    set => this._StateAbrev = value;
  }

  [DBProp("City", AllowNull = false, MaxLength = 200)]
  public string City
  {
    get => this._City;
    set => this._City = value;
  }

  [DBProp("State", AllowNull = false, MaxLength = 200)]
  public string State
  {
    get => this._State;
    set => this._State = value;
  }

  [DBProp("Latitude", AllowNull = false)]
  public double Latitude
  {
    get => this._Latitude;
    set => this._Latitude = value;
  }

  [DBProp("Longitude", AllowNull = false)]
  public double Longitude
  {
    get => this._Longitude;
    set => this._Longitude = value;
  }

  public static ZipcodeInfo Load(long ID) => BaseDBObject.Load<ZipcodeInfo>(ID);

  public static ZipcodeInfo Find(string zipCode) => new Monnit.Data.ZipcodeInfo.Find(zipCode).Result;

  public double CalculateDistance(string zipCode)
  {
    ZipcodeInfo zipCodeB = ZipcodeInfo.Find(zipCode);
    return zipCodeB == null ? double.MinValue : ZipcodeInfo.CalculateDistance(this, zipCodeB);
  }

  public static double CalculateDistance(string zipCodeA, string zipCodeB)
  {
    ZipcodeInfo zipCodeA1 = ZipcodeInfo.Find(zipCodeA);
    ZipcodeInfo zipCodeB1 = ZipcodeInfo.Find(zipCodeB);
    return zipCodeA1 == null || zipCodeB1 == null ? double.MinValue : ZipcodeInfo.CalculateDistance(zipCodeA1, zipCodeB1);
  }

  public static double CalculateDistance(ZipcodeInfo zipCodeA, ZipcodeInfo zipCodeB)
  {
    return ZipcodeInfo.CalculateDistance(zipCodeA.Latitude, zipCodeA.Longitude, zipCodeB.Latitude, zipCodeB.Longitude);
  }

  private static double CalculateDistance(double latA, double longA, double latB, double longB)
  {
    return ZipcodeInfo.RadianToDegree(Math.Acos(Math.Sin(ZipcodeInfo.DegreeToRadian(latA)) * Math.Sin(ZipcodeInfo.DegreeToRadian(latB)) + Math.Cos(ZipcodeInfo.DegreeToRadian(latA)) * Math.Cos(ZipcodeInfo.DegreeToRadian(latB)) * Math.Cos(ZipcodeInfo.DegreeToRadian(longA - longB)))) * 69.09;
  }

  private static double DegreeToRadian(double angle) => Math.PI * angle / 180.0;

  private static double RadianToDegree(double angle) => angle * (180.0 / Math.PI);
}
