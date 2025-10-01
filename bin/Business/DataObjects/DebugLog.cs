// Decompiled with JetBrains decompiler
// Type: Monnit.DebugLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("DebugLog")]
public class DebugLog : BaseDBObject
{
  private long _DebugLogID = long.MinValue;
  private string _IdentifierType = string.Empty;
  private long _IdentifierValue = long.MinValue;
  private long _Severity = long.MinValue;
  private string _Server = string.Empty;
  private DateTime _LogDate;
  private string _CodePath = string.Empty;
  private string _Message = string.Empty;

  [DBProp("DebugLogID", IsPrimaryKey = true)]
  public long DebugLogID
  {
    get => this._DebugLogID;
    set => this._DebugLogID = value;
  }

  [DBProp("IdentifierType", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string IdentifierType
  {
    get => this._IdentifierType;
    set => this._IdentifierType = value;
  }

  [DBProp("IdentifierValue", AllowNull = true)]
  public long IdentifierValue
  {
    get => this._IdentifierValue;
    set => this._IdentifierValue = value;
  }

  [DBProp("Severity", AllowNull = true)]
  public long Severity
  {
    get => this._Severity;
    set => this._Severity = value;
  }

  [DBProp("Server", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Server
  {
    get => this._Server;
    set => this._Server = value;
  }

  [DBProp("LogDate", AllowNull = true)]
  public DateTime LogDate
  {
    get => this._LogDate;
    set => this._LogDate = value;
  }

  [DBProp("CodePath", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string CodePath
  {
    get => this._CodePath;
    set => this._CodePath = value;
  }

  [DBProp("Message", MaxLength = 2147483647 /*0x7FFFFFFF*/, AllowNull = true)]
  public string Message
  {
    get => this._Message;
    set => this._Message = value;
  }

  public static void CreateLog(string message, string codePath, int severity)
  {
    if (!ConfigData.AppSettings("EnableDebugLog").ToBool())
      return;
    DebugLog.CreateLogInternal(message, codePath, severity).Save();
  }

  public static void CreateLog(
    string message,
    string codePath,
    int severity,
    string objectType,
    long objectID)
  {
    if (!ConfigData.AppSettings("EnableDebugLog").ToBool())
      return;
    DebugLog logInternal = DebugLog.CreateLogInternal(message, codePath, severity);
    logInternal.IdentifierType = objectType;
    logInternal.IdentifierValue = objectID;
    logInternal.Save();
  }

  private static DebugLog CreateLogInternal(string message, string codePath, int severity)
  {
    return new DebugLog()
    {
      Message = message,
      CodePath = codePath,
      Severity = (long) severity,
      LogDate = DateTime.Now,
      Server = ""
    };
  }

  public static DebugLog Load(long id) => BaseDBObject.Load<DebugLog>(id);
}
