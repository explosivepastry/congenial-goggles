// Decompiled with JetBrains decompiler
// Type: Monnit.CassandraLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("CassandraLog")]
public class CassandraLog : BaseDBObject
{
  private Guid _CassandraLogGUID = Guid.Empty;
  private long _AccountID = long.MinValue;
  private DateTime _TimeStamp = DateTime.MinValue;
  private string _Method = string.Empty;
  private int _Rows = int.MinValue;

  [DBProp("CassandraLogGUID", IsGuidPrimaryKey = true)]
  public Guid CassandraLogGUID
  {
    get => this._CassandraLogGUID;
    set => this._CassandraLogGUID = value;
  }

  [DBProp("AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("TimeStamp")]
  public DateTime TimeStamp
  {
    get => this._TimeStamp;
    set => this._TimeStamp = value;
  }

  [DBProp("Method", MaxLength = 300)]
  public string Method
  {
    get => this._Method;
    set => this._Method = value;
  }

  [DBProp("Rows")]
  public int Rows
  {
    get => this._Rows;
    set => this._Rows = value;
  }

  public static void LogAuditData(long accountID, string method, int rows, DateTime timeStamp)
  {
    timeStamp = timeStamp;
    try
    {
      new CassandraLog()
      {
        AccountID = accountID,
        Method = method,
        TimeStamp = timeStamp,
        Rows = rows
      }.Save();
    }
    catch (Exception ex)
    {
      ex.Log($"CassandraLog Failed method: {method} AccountID: {accountID.ToString()}");
    }
  }
}
