// Decompiled with JetBrains decompiler
// Type: Monnit.PopupNoticeRecord
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Monnit;

[DBClass("PopupNoticeRecord")]
public class PopupNoticeRecord : BaseDBObject
{
  private long _PopupNoticeRecordID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private long _AccountID = long.MinValue;
  private ePopupNoticeType _PopupNoticeType;
  private DateTime _DateLastSeen = DateTime.MinValue;
  private string _SKU;
  private string _FirmwareVersionToIgnore;

  [DBProp("PopupNoticeRecordID", IsPrimaryKey = true)]
  public long PopupNoticeRecordID
  {
    get => this._PopupNoticeRecordID;
    set => this._PopupNoticeRecordID = value;
  }

  [DBProp("CustomerID", AllowNull = false)]
  [DBForeignKey("Customer", "CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("ePopupNoticeType", AllowNull = false)]
  public ePopupNoticeType PopupNoticeType
  {
    get => this._PopupNoticeType;
    set => this._PopupNoticeType = value;
  }

  [DBProp("DateLastSeen", AllowNull = true)]
  public DateTime DateLastSeen
  {
    get => this._DateLastSeen;
    set => this._DateLastSeen = value;
  }

  [DBProp("SKU", AllowNull = true)]
  public string SKU
  {
    get => this._SKU;
    set => this._SKU = value;
  }

  [DBProp("FirmwareVersionToIgnore", AllowNull = true)]
  public string FirmwareVersionToIgnore
  {
    get => this._FirmwareVersionToIgnore;
    set => this._FirmwareVersionToIgnore = value;
  }

  private static string GetColumnNameString(List<string> columnNames, string columnName)
  {
    string str = columnNames.Where<string>((Func<string, bool>) (c => c.Equals(columnName))).FirstOrDefault<string>();
    return !string.IsNullOrEmpty(str) ? str : throw new Exception($"PopupNoticeRecord column name [{columnName}] not found");
  }

  public static PopupNoticeRecord LoadPopupNoticeRecordByCustomerIDAccountIDAndType(
    long customerID,
    long accountID,
    ePopupNoticeType popupNoticeType)
  {
    List<string> list = ((IEnumerable<MemberInfo>) typeof (PopupNoticeRecord).GetMembers()).SelectMany<MemberInfo, object>((Func<MemberInfo, IEnumerable<object>>) (m => (IEnumerable<object>) m.GetCustomAttributes(typeof (DBPropAttribute), true))).Select<object, string>((Func<object, string>) (x => ((DBPropAttribute) x).ColumnName)).ToList<string>();
    PopupNoticeRecord popupNoticeRecord = BaseDBObject.LoadByForeignKeys<PopupNoticeRecord>(new string[3]
    {
      PopupNoticeRecord.GetColumnNameString(list, "CustomerID"),
      PopupNoticeRecord.GetColumnNameString(list, "AccountID"),
      PopupNoticeRecord.GetColumnNameString(list, "ePopupNoticeType")
    }, new object[3]
    {
      (object) customerID,
      (object) accountID,
      (object) popupNoticeType
    }).Where<PopupNoticeRecord>((Func<PopupNoticeRecord, bool>) (r => string.IsNullOrEmpty(r.SKU) && string.IsNullOrEmpty(r.FirmwareVersionToIgnore))).FirstOrDefault<PopupNoticeRecord>();
    if (popupNoticeRecord == null)
    {
      popupNoticeRecord = new PopupNoticeRecord();
      popupNoticeRecord.CustomerID = customerID;
      popupNoticeRecord.AccountID = accountID;
      popupNoticeRecord.PopupNoticeType = popupNoticeType;
    }
    return popupNoticeRecord;
  }

  public static PopupNoticeRecord LoadPopupNoticeRecordByCustomerIDAccountIDTypeSKUAndFirmwareVersion(
    long customerID,
    long accountID,
    ePopupNoticeType popupNoticeType,
    string SKU,
    string FirmwareVersion)
  {
    List<string> list = ((IEnumerable<MemberInfo>) typeof (PopupNoticeRecord).GetMembers()).SelectMany<MemberInfo, object>((Func<MemberInfo, IEnumerable<object>>) (m => (IEnumerable<object>) m.GetCustomAttributes(typeof (DBPropAttribute), true))).Select<object, string>((Func<object, string>) (x => ((DBPropAttribute) x).ColumnName)).ToList<string>();
    PopupNoticeRecord popupNoticeRecord = BaseDBObject.LoadByForeignKeys<PopupNoticeRecord>(new string[5]
    {
      PopupNoticeRecord.GetColumnNameString(list, "CustomerID"),
      PopupNoticeRecord.GetColumnNameString(list, "AccountID"),
      PopupNoticeRecord.GetColumnNameString(list, "ePopupNoticeType"),
      PopupNoticeRecord.GetColumnNameString(list, nameof (SKU)),
      PopupNoticeRecord.GetColumnNameString(list, "FirmwareVersionToIgnore")
    }, new object[5]
    {
      (object) customerID,
      (object) accountID,
      (object) popupNoticeType,
      (object) SKU,
      (object) FirmwareVersion
    }).FirstOrDefault<PopupNoticeRecord>();
    if (popupNoticeRecord == null)
    {
      popupNoticeRecord = new PopupNoticeRecord();
      popupNoticeRecord.CustomerID = customerID;
      popupNoticeRecord.AccountID = accountID;
      popupNoticeRecord.PopupNoticeType = popupNoticeType;
      popupNoticeRecord.SKU = SKU;
      popupNoticeRecord.FirmwareVersionToIgnore = FirmwareVersion;
    }
    return popupNoticeRecord;
  }

  public static List<PopupNoticeRecord> LoadPopupNoticeRecordByCustomerIDAndAccountID(
    long customerID,
    long accountID)
  {
    return BaseDBObject.LoadByForeignKeys<PopupNoticeRecord>(new string[2]
    {
      "CustomerID",
      "AccountID"
    }, new object[2]
    {
      (object) customerID,
      (object) accountID
    });
  }

  public static PopupNoticeRecord Load(long id) => BaseDBObject.Load<PopupNoticeRecord>(id);

  public static List<PopupNoticeRecord> LoadIgnoredFirmwareVersions(
    long customerID,
    long accountID,
    ePopupNoticeType popupNoticeType)
  {
    return new Monnit.Data.PopupNoticeRecord.LoadIgnoredFirmwareVersions(customerID, accountID, popupNoticeType).Result;
  }
}
