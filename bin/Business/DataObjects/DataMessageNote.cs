// Decompiled with JetBrains decompiler
// Type: Monnit.DataMessageNote
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("DataMessageNote")]
public class DataMessageNote : BaseDBObject
{
  private long _DataMessageNoteID = long.MinValue;
  private Guid _DataMessageGUID = Guid.Empty;
  private long _SensorID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private DateTime _NoteDate = DateTime.MinValue;
  private string _Note = string.Empty;
  private DateTime _MessageDate = DateTime.MinValue;

  [DBProp("DataMessageNoteID", IsPrimaryKey = true)]
  public long DataMessageNoteID
  {
    get => this._DataMessageNoteID;
    set => this._DataMessageNoteID = value;
  }

  [DBProp("DataMessageGUID")]
  public Guid DataMessageGUID
  {
    get => this._DataMessageGUID;
    set => this._DataMessageGUID = value;
  }

  [DBProp("SensorID")]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("NoteDate")]
  public DateTime NoteDate
  {
    get => this._NoteDate;
    set => this._NoteDate = value;
  }

  [DBProp("Note", MaxLength = 2000, AllowNull = true, International = true)]
  public string Note
  {
    get => this._Note;
    set => this._Note = value;
  }

  [DBProp("MessageDate")]
  public DateTime MessageDate
  {
    get => this._MessageDate;
    set => this._MessageDate = value;
  }

  public static DataMessageNote Load(long id) => BaseDBObject.Load<DataMessageNote>(id);

  public static List<DataMessageNote> LoadByDataMessageGUID(Guid guid)
  {
    return BaseDBObject.LoadByForeignKey<DataMessageNote>("DataMessageGUID", (object) guid);
  }

  public static List<DataMessageNote> LoadBySensorAndDateRange(
    long sensorID,
    DateTime fromDate,
    DateTime toDate)
  {
    return new Monnit.Data.DataMessageNote.LoadBySensorAndDateRange(sensorID, fromDate, toDate).Result;
  }
}
