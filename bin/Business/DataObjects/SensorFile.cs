// Decompiled with JetBrains decompiler
// Type: Monnit.SensorFile
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

public class SensorFile : BaseDBObject
{
  private long _SensorFileID = long.MinValue;
  private long _SensorID = long.MinValue;
  private int _FileSize = int.MinValue;
  private DateTime _Date = DateTime.MinValue;
  private byte[] _Data = (byte[]) null;
  private int _ChunkSize = int.MinValue;

  [DBProp("SensorFileID", IsPrimaryKey = true)]
  public long SensorFileID
  {
    get => this._SensorFileID;
    set => this._SensorFileID = value;
  }

  [DBProp("SensorID")]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("FileSize")]
  public int FileSize
  {
    get => this._FileSize;
    set => this._FileSize = value;
  }

  [DBProp("Date")]
  public DateTime Date
  {
    get => this._Date;
    set => this._Date = value;
  }

  [DBProp("Data")]
  public byte[] Data
  {
    get
    {
      if (this._Data == null)
        this._Data = new byte[this.FileSize];
      return this._Data;
    }
    set
    {
      if (this._Data == null)
        this._Data = new byte[this.FileSize];
      Array.Copy((Array) value, (Array) this._Data, value.Length < this.FileSize ? value.Length : this.FileSize);
    }
  }

  public int ChunkSize
  {
    get => this._ChunkSize;
    set => this._ChunkSize = value;
  }

  public static SensorFile Load(long id) => BaseDBObject.Load<SensorFile>(id);

  public void AddChunk(int messageCount, byte[] chunk)
  {
    int destinationIndex = (messageCount - 1) * this.ChunkSize;
    if (destinationIndex >= this.FileSize)
      return;
    int length = this.ChunkSize == chunk.Length ? this.ChunkSize : chunk.Length;
    if (destinationIndex + length > this.FileSize)
      length = this.FileSize - destinationIndex;
    Array.Copy((Array) chunk, 0, (Array) this.Data, destinationIndex, length);
  }

  public static void DeleteOld()
  {
    int num = 45;
    string o = ConfigData.FindValue("DaysToKeepSensorFiles");
    if (!string.IsNullOrEmpty(o) && o.ToInt() > 0)
      num = o.ToInt();
    Monnit.Data.SensorFile.DeleteOld deleteOld = new Monnit.Data.SensorFile.DeleteOld(DateTime.UtcNow.AddDays((double) (num * -1)));
  }

  public static List<SensorFile> LoadBySensor(long sensorID)
  {
    return BaseDBObject.LoadByForeignKey<SensorFile>("SensorID", (object) sensorID);
  }
}
