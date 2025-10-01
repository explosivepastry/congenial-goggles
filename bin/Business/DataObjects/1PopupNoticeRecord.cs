// Decompiled with JetBrains decompiler
// Type: Monnit.Data.PopupNoticeRecord
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class PopupNoticeRecord
{
  [DBMethod("PopupNoticeRecord_LoadIgnoredFirmwareVersions")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n\t  [PopupNoticeRecordID]\r\n\t, [CustomerID]\r\n\t, [AccountID]\r\n\t, [ePopupNoticeType]\r\n\t, [DateLastSeen]\r\n\t, [SKU]\r\n\t, [FirmwareVersionToIgnore]\r\nFROM PopupNoticeRecord\r\nWHERE CustomerID = @CustomerID\r\n\tAND AccountID = @AccountID\r\n\tAND ePopupNoticeType = @PopupNoticeType\r\n")]
  internal class LoadIgnoredFirmwareVersions : BaseDBMethod
  {
    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("ePopupNoticeType", typeof (int))]
    public int PopupNoticeType { get; private set; }

    public List<Monnit.PopupNoticeRecord> Result { get; private set; }

    public LoadIgnoredFirmwareVersions(
      long customerID,
      long accountID,
      ePopupNoticeType popupNoticeType)
    {
      this.CustomerID = customerID;
      this.AccountID = accountID;
      this.PopupNoticeType = (int) popupNoticeType;
      this.Result = BaseDBObject.Load<Monnit.PopupNoticeRecord>(this.ToDataTable());
    }
  }
}
