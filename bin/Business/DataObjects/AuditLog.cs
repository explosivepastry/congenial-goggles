// Decompiled with JetBrains decompiler
// Type: Monnit.AuditLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("AuditLog")]
public class AuditLog : BaseDBObject
{
  private Guid _AuditLogGUID = Guid.Empty;
  private long _CustomerID = long.MinValue;
  private DateTime _TimeStamp = DateTime.MinValue;
  private long _ObjectID = long.MinValue;
  private eAuditObject _AuditObject;
  private eAuditAction _AuditAction;
  private string _Record = string.Empty;
  private long _AccountID = long.MinValue;
  private string _ActionDescription = string.Empty;

  [DBProp("AuditLogGUID", IsGuidPrimaryKey = true)]
  public Guid AuditLogGUID
  {
    get => this._AuditLogGUID;
    set => this._AuditLogGUID = value;
  }

  [DBProp("CustomerID")]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("TimeStamp")]
  public DateTime TimeStamp
  {
    get => this._TimeStamp;
    set => this._TimeStamp = value;
  }

  [DBProp("ObjectID")]
  public long ObjectID
  {
    get => this._ObjectID;
    set => this._ObjectID = value;
  }

  [DBProp("AuditObject")]
  public eAuditObject AuditObject
  {
    get => this._AuditObject;
    set => this._AuditObject = value;
  }

  [DBProp("AuditAction")]
  public eAuditAction AuditAction
  {
    get => this._AuditAction;
    set => this._AuditAction = value;
  }

  [DBProp("Record", MaxLength = 8000, AllowNull = true)]
  public string Record
  {
    get => this._Record;
    set => this._Record = value;
  }

  [DBProp("AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("ActionDescription", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ActionDescription
  {
    get => this._ActionDescription;
    set => this._ActionDescription = value;
  }

  public static void LogAuditData(
    long customerID,
    long objectID,
    eAuditAction action,
    eAuditObject Auditobj,
    string record,
    long accountID,
    string descriptionOfAction)
  {
    try
    {
      new AuditLog()
      {
        AccountID = accountID,
        ActionDescription = descriptionOfAction,
        AuditAction = action,
        AuditObject = Auditobj,
        ObjectID = objectID,
        TimeStamp = DateTime.UtcNow,
        CustomerID = customerID,
        Record = record
      }.Save();
    }
    catch (Exception ex)
    {
      ex.Log($"AuditLog Failed action: {action.ToString()} Object: {Auditobj.ToString()} CustomerID: {customerID.ToString()}");
    }
  }

  public static AuditLog Load(Guid Guid) => BaseDBObject.Load<AuditLog>(Guid);

  public static List<AuditLog> LoadAllByAccountIDAndMonthRange(long accountID)
  {
    return new Monnit.Data.AuditLog.LoadAllByAccountIDAndMonthRange(accountID).Result;
  }
}
