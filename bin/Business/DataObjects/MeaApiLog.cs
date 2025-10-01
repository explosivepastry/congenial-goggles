// Decompiled with JetBrains decompiler
// Type: Monnit.MeaApiLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("MeaApiLog")]
public class MeaApiLog : BaseDBObject
{
  private long _MeaApiLogID = long.MinValue;
  private string _MethodName = string.Empty;
  private string _MachineName = string.Empty;
  private string _RequestBody = string.Empty;
  private string _ResponseBody = string.Empty;
  private DateTime _CreateDate = DateTime.MinValue;
  private int _SecondsElapsed = int.MinValue;
  private bool _IsException = false;

  [DBProp("MeaApiLogID", IsPrimaryKey = true)]
  public long MeaApiLogID
  {
    get => this._MeaApiLogID;
    set => this._MeaApiLogID = value;
  }

  [DBProp("MethodName", MaxLength = 100, AllowNull = false)]
  public string MethodName
  {
    get => this._MethodName;
    set => this._MethodName = value;
  }

  [DBProp("MachineName", MaxLength = 25, AllowNull = false)]
  public string MachineName
  {
    get => this._MachineName;
    set => this._MachineName = value;
  }

  [DBProp("RequestBody", MaxLength = 2147483647 /*0x7FFFFFFF*/, AllowNull = true)]
  public string RequestBody
  {
    get => this._RequestBody;
    set => this._RequestBody = value;
  }

  [DBProp("ResponseBody", MaxLength = 2147483647 /*0x7FFFFFFF*/, AllowNull = true)]
  public string ResponseBody
  {
    get => this._ResponseBody;
    set => this._ResponseBody = value;
  }

  [DBProp("CreateDate", AllowNull = false)]
  public DateTime CreateDate
  {
    get => this._CreateDate;
    set => this._CreateDate = value;
  }

  [DBProp("SecondsElapsed", AllowNull = false)]
  public int SecondsElapsed
  {
    get => this._SecondsElapsed;
    set => this._SecondsElapsed = value;
  }

  [DBProp("IsException", AllowNull = false, DefaultValue = false)]
  public bool IsException
  {
    get => this._IsException;
    set => this._IsException = value;
  }

  public static List<MeaApiLog> All => BaseDBObject.LoadAll<MeaApiLog>();

  public static MeaApiLog Load(long id) => BaseDBObject.Load<MeaApiLog>(id);
}
