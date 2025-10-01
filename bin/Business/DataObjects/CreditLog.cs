// Decompiled with JetBrains decompiler
// Type: Monnit.CreditLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("CreditLog")]
public class CreditLog : BaseDBObject
{
  private long _CreditLogID = long.MinValue;
  private long _AccountID = long.MinValue;
  private long _SensorID = long.MinValue;
  private DateTime _InsertDate = DateTime.MinValue;
  private DateTime _MessageDate = DateTime.MinValue;
  private short _Hour0 = 0;
  private short _Consumed0 = 0;
  private short _Hour1 = 0;
  private short _Consumed1 = 0;
  private short _Hour2 = 0;
  private short _Consumed2 = 0;
  private short _Hour3 = 0;
  private short _Consumed3 = 0;
  private short _Hour4 = 0;
  private short _Consumed4 = 0;
  private short _Hour5 = 0;
  private short _Consumed5 = 0;
  private short _Hour6 = 0;
  private short _Consumed6 = 0;
  private short _Hour7 = 0;
  private short _Consumed7 = 0;
  private short _Hour8 = 0;
  private short _Consumed8 = 0;
  private short _Hour9 = 0;
  private short _Consumed9 = 0;
  private short _Hour10 = 0;
  private short _Consumed10 = 0;
  private short _Hour11 = 0;
  private short _Consumed11 = 0;
  private short _Hour12 = 0;
  private short _Consumed12 = 0;
  private short _Hour13 = 0;
  private short _Consumed13 = 0;
  private short _Hour14 = 0;
  private short _Consumed14 = 0;
  private short _Hour15 = 0;
  private short _Consumed15 = 0;
  private short _Hour16 = 0;
  private short _Consumed16 = 0;
  private short _Hour17 = 0;
  private short _Consumed17 = 0;
  private short _Hour18 = 0;
  private short _Consumed18 = 0;
  private short _Hour19 = 0;
  private short _Consumed19 = 0;
  private short _Hour20 = 0;
  private short _Consumed20 = 0;
  private short _Hour21 = 0;
  private short _Consumed21 = 0;
  private short _Hour22 = 0;
  private short _Consumed22 = 0;
  private short _Hour23 = 0;
  private short _Consumed23 = 0;
  private int _Total = 0;

  [DBProp("CreditLogID", IsPrimaryKey = true)]
  public long CreditLogID
  {
    get => this._CreditLogID;
    set => this._CreditLogID = value;
  }

  [DBProp("AccountID", AllowNull = false)]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("SensorID", AllowNull = false)]
  [DBForeignKey("Sensor", "SensorID")]
  public long SensorID
  {
    get => this._SensorID;
    set => this._SensorID = value;
  }

  [DBProp("InsertDate", AllowNull = false)]
  public DateTime InsertDate
  {
    get => this._InsertDate;
    set => this._InsertDate = value;
  }

  [DBProp("MessageDate", AllowNull = false)]
  public DateTime MessageDate
  {
    get => this._MessageDate;
    set => this._MessageDate = value;
  }

  [DBProp("Hour0")]
  public short Hour0
  {
    get => this._Hour0;
    set => this._Hour0 = value;
  }

  [DBProp("Consumed0")]
  public short Consumed0
  {
    get => this._Consumed0;
    set => this._Consumed0 = value;
  }

  [DBProp("Hour1")]
  public short Hour1
  {
    get => this._Hour1;
    set => this._Hour1 = value;
  }

  [DBProp("Consumed1")]
  public short Consumed1
  {
    get => this._Consumed1;
    set => this._Consumed1 = value;
  }

  [DBProp("Hour2")]
  public short Hour2
  {
    get => this._Hour2;
    set => this._Hour2 = value;
  }

  [DBProp("Consumed2")]
  public short Consumed2
  {
    get => this._Consumed2;
    set => this._Consumed2 = value;
  }

  [DBProp("Hour3")]
  public short Hour3
  {
    get => this._Hour3;
    set => this._Hour3 = value;
  }

  [DBProp("Consumed3")]
  public short Consumed3
  {
    get => this._Consumed3;
    set => this._Consumed3 = value;
  }

  [DBProp("Hour4")]
  public short Hour4
  {
    get => this._Hour4;
    set => this._Hour4 = value;
  }

  [DBProp("Consumed4")]
  public short Consumed4
  {
    get => this._Consumed4;
    set => this._Consumed4 = value;
  }

  [DBProp("Hour5")]
  public short Hour5
  {
    get => this._Hour5;
    set => this._Hour5 = value;
  }

  [DBProp("Consumed5")]
  public short Consumed5
  {
    get => this._Consumed5;
    set => this._Consumed5 = value;
  }

  [DBProp("Hour6")]
  public short Hour6
  {
    get => this._Hour6;
    set => this._Hour6 = value;
  }

  [DBProp("Consumed6")]
  public short Consumed6
  {
    get => this._Consumed6;
    set => this._Consumed6 = value;
  }

  [DBProp("Hour7")]
  public short Hour7
  {
    get => this._Hour7;
    set => this._Hour7 = value;
  }

  [DBProp("Consumed7")]
  public short Consumed7
  {
    get => this._Consumed7;
    set => this._Consumed7 = value;
  }

  [DBProp("Hour8")]
  public short Hour8
  {
    get => this._Hour8;
    set => this._Hour8 = value;
  }

  [DBProp("Consumed8")]
  public short Consumed8
  {
    get => this._Consumed8;
    set => this._Consumed8 = value;
  }

  [DBProp("Hour9")]
  public short Hour9
  {
    get => this._Hour9;
    set => this._Hour9 = value;
  }

  [DBProp("Consumed9")]
  public short Consumed9
  {
    get => this._Consumed9;
    set => this._Consumed9 = value;
  }

  [DBProp("Hour10")]
  public short Hour10
  {
    get => this._Hour10;
    set => this._Hour10 = value;
  }

  [DBProp("Consumed10")]
  public short Consumed10
  {
    get => this._Consumed10;
    set => this._Consumed10 = value;
  }

  [DBProp("Hour11")]
  public short Hour11
  {
    get => this._Hour11;
    set => this._Hour11 = value;
  }

  [DBProp("Consumed11")]
  public short Consumed11
  {
    get => this._Consumed11;
    set => this._Consumed11 = value;
  }

  [DBProp("Hour12")]
  public short Hour12
  {
    get => this._Hour12;
    set => this._Hour12 = value;
  }

  [DBProp("Consumed12")]
  public short Consumed12
  {
    get => this._Consumed12;
    set => this._Consumed12 = value;
  }

  [DBProp("Hour13")]
  public short Hour13
  {
    get => this._Hour13;
    set => this._Hour13 = value;
  }

  [DBProp("Consumed13")]
  public short Consumed13
  {
    get => this._Consumed13;
    set => this._Consumed13 = value;
  }

  [DBProp("Hour14")]
  public short Hour14
  {
    get => this._Hour14;
    set => this._Hour14 = value;
  }

  [DBProp("Consumed14")]
  public short Consumed14
  {
    get => this._Consumed14;
    set => this._Consumed14 = value;
  }

  [DBProp("Hour15")]
  public short Hour15
  {
    get => this._Hour15;
    set => this._Hour15 = value;
  }

  [DBProp("Consumed15")]
  public short Consumed15
  {
    get => this._Consumed15;
    set => this._Consumed15 = value;
  }

  [DBProp("Hour16")]
  public short Hour16
  {
    get => this._Hour16;
    set => this._Hour16 = value;
  }

  [DBProp("Consumed16")]
  public short Consumed16
  {
    get => this._Consumed16;
    set => this._Consumed16 = value;
  }

  [DBProp("Hour17")]
  public short Hour17
  {
    get => this._Hour17;
    set => this._Hour17 = value;
  }

  [DBProp("Consumed17")]
  public short Consumed17
  {
    get => this._Consumed17;
    set => this._Consumed17 = value;
  }

  [DBProp("Hour18")]
  public short Hour18
  {
    get => this._Hour18;
    set => this._Hour18 = value;
  }

  [DBProp("Consumed18")]
  public short Consumed18
  {
    get => this._Consumed18;
    set => this._Consumed18 = value;
  }

  [DBProp("Hour19")]
  public short Hour19
  {
    get => this._Hour19;
    set => this._Hour19 = value;
  }

  [DBProp("Consumed19")]
  public short Consumed19
  {
    get => this._Consumed19;
    set => this._Consumed19 = value;
  }

  [DBProp("Hour20")]
  public short Hour20
  {
    get => this._Hour20;
    set => this._Hour20 = value;
  }

  [DBProp("Consumed20")]
  public short Consumed20
  {
    get => this._Consumed20;
    set => this._Consumed20 = value;
  }

  [DBProp("Hour21")]
  public short Hour21
  {
    get => this._Hour21;
    set => this._Hour21 = value;
  }

  [DBProp("Consumed21")]
  public short Consumed21
  {
    get => this._Consumed21;
    set => this._Consumed21 = value;
  }

  [DBProp("Hour22")]
  public short Hour22
  {
    get => this._Hour22;
    set => this._Hour22 = value;
  }

  [DBProp("Consumed22")]
  public short Consumed22
  {
    get => this._Consumed22;
    set => this._Consumed22 = value;
  }

  [DBProp("Hour23")]
  public short Hour23
  {
    get => this._Hour23;
    set => this._Hour23 = value;
  }

  [DBProp("Consumed23")]
  public short Consumed23
  {
    get => this._Consumed23;
    set => this._Consumed23 = value;
  }

  [DBProp("Total")]
  public int Total
  {
    get => this._Total;
    set => this._Total = value;
  }

  public static CreditLog Load(long id) => BaseDBObject.Load<CreditLog>(id);
}
