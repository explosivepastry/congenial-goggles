// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CustomerGroupLink
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class CustomerGroupLink
{
  [DBMethod("CustomerGroupLink_LoadByCustomerGroupID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[CustomerGroupLink]\r\nWHERE CustomerGroupID = @CustomerGroupID\r\n\r\n")]
  internal class LoadByCustomerGroupID : BaseDBMethod
  {
    [DBMethodParam("CustomerGroupID", typeof (long))]
    public long CustomerGroupID { get; private set; }

    public List<Monnit.CustomerGroupLink> Result { get; private set; }

    public LoadByCustomerGroupID(long customerGroupID)
    {
      this.CustomerGroupID = customerGroupID;
      this.Result = BaseDBObject.Load<Monnit.CustomerGroupLink>(this.ToDataTable());
    }
  }

  [DBMethod("CustomerGroupLink_DeleteByCustomerGroupLinkID")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/*\r\n  Cascading deleted\r\n    -Delete Notification Recipients\r\n    -Delete Customer Group Links\r\n*/\r\n\r\nDECLARE @RV               BIT,\r\n        @ProcName         NVARCHAR(50);\r\n        \r\n--Error Handling Params\r\nDECLARE @ErrorSysMsg      NVARCHAR(MAX);\r\n\r\nBEGIN TRAN\r\n\r\nBEGIN TRY\r\n\r\n  SET @Rv = 1 --1 = success\r\n  SET @ProcName = OBJECT_NAME(@@PROCID); --Set the procedure name for debugging\r\n\r\n  DELETE gr FROM dbo.[CustomerGroupRecipient] gr WHERE CustomerGroupLinkID = @CustomerGroupLinkID\r\n  DELETE dbo.[CustomerGroupLink] WHERE CustomerGroupLinkID = @CustomerGroupLinkID\r\n\r\nEND TRY\r\nBEGIN CATCH\r\n\r\n  SET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n  IF (@@TRANCOUNT > 0)\r\n  BEGIN\r\n    ROLLBACK TRANSACTION\r\n  END\r\n\r\n  --RAISERROR (@ErrorSysMsg, 11, 1)\r\n  SET @RV = 0\r\n\r\nEND CATCH\r\n\r\nIF (@@TRANCOUNT > 0)\r\nBEGIN\r\n  COMMIT TRANSACTION\r\nEND\r\n\r\nSELECT RV = @RV, ErrorMsg = ISNULL(@ErrorSysMsg, 'Success')")]
  internal class DeleteByCustomerGroupLinkID : BaseDBMethod
  {
    [DBMethodParam("CustomerGroupLinkID", typeof (long))]
    public long CustomerGroupLinkID { get; private set; }

    public DataTable Result { get; private set; }

    public DeleteByCustomerGroupLinkID(long customerGroupLinkID)
    {
      this.CustomerGroupLinkID = customerGroupLinkID;
      this.Result = this.ToDataTable();
    }
  }
}
