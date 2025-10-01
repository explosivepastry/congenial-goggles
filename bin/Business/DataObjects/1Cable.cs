// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Cable
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit.Data;

internal class Cable
{
  [DBMethod("Cable_ForceInsert")]
  [DBMethodBody(DBMS.SqlServer, "\r\n  --Error Handling Params\r\n  DECLARE @ErrorNum                     INT,              -- Remember when Multiple Error Log Catch statements, to Move these 3 lines to top of Procedure\r\n          @ErrorProcedure               NVARCHAR(50),     -- Remember when Multiple Error Log Catch statements, to Move these 3 lines to top of Procedure\r\n          @ErrorSysMsg                  NVARCHAR(MAX);    -- Remember when Multiple Error Log Catch statements, to Move these 3 lines to top of Procedure\r\n\r\n\tBEGIN TRAN\r\n\tBEGIN TRY\r\n\t\r\n\t\t--SET IDENTITY_INSERT dbo.Cable ON;\r\n\t\tINSERT INTO \r\n\t\t\tdbo.Cable (CableID,CreateDate,SKU,ApplicationID,CableMinorRevision,CableMajorRevision)\r\n\t\tVALUES \r\n\t\t\t(@CableID,@CreateDate,@SKU,@ApplicationID,@CableMinorRevision,@CableMajorRevision)\r\n\t\t--SET IDENTITY_INSERT dbo.Cable  OFF;\r\n\t\t\r\n\tEND TRY\r\n\tBEGIN CATCH\r\n\t\r\n\t  SET @ErrorNum = ERROR_NUMBER();\r\n\t  SET @ErrorProcedure = ERROR_PROCEDURE();\r\n\t  SET @ErrorSysMsg = ERROR_MESSAGE();\r\n\r\n\t\tROLLBACK TRAN\r\n\t\t\r\n\t  RAISERROR (@ErrorSysMsg, 11, 1);\r\n\r\n\tEND CATCH\r\n\r\n\tIF @@TRANCOUNT > 0\r\n\t   COMMIT TRAN\r\n")]
  [DBMethodBody(DBMS.SQLite, "INSERT INTO Cable (CableID, CreateDate, SKU, ApplicationID, CableMinorRevision, CableMajorRevision) \r\n\t                                    values (@CableID,@CreateDate,@SKU,@ApplicationID,'@CableMinorRevision',@CableMajorRevision')")]
  internal class ForceInsert : BaseDBMethod
  {
    [DBMethodParam("CableID", typeof (long))]
    public long CableID { get; private set; }

    [DBMethodParam("CreateDate", typeof (DateTime))]
    public DateTime CreateDate { get; private set; }

    [DBMethodParam("SKU", typeof (string))]
    public string SKU { get; private set; }

    [DBMethodParam("ApplicationID", typeof (long))]
    public long ApplicationID { get; private set; }

    [DBMethodParam("CableMinorRevision", typeof (int))]
    public int CableMinorRevision { get; private set; }

    [DBMethodParam("CableMajorRevision", typeof (int))]
    public int CableMajorRevision { get; private set; }

    public ForceInsert(Monnit.Cable cable)
    {
      this.CableID = cable.CableID;
      this.CreateDate = cable.CreateDate;
      this.SKU = cable.SKU;
      this.ApplicationID = cable.ApplicationID;
      this.CableMinorRevision = cable.CableMinorRevision;
      this.CableMajorRevision = cable.CableMajorRevision;
      this.Execute();
    }
  }
}
