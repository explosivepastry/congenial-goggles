// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CustomerLinkRequest
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class CustomerLinkRequest
{
  [DBMethod("CustomerLinkRequest_LoadByEmail")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[CustomerLinkRequest]\r\nWHERE EmailAddress = @Email\r\n  AND IsDeleted    = 0;")]
  internal class LoadByEmail : BaseDBMethod
  {
    [DBMethodParam("Email", typeof (string))]
    public string Email { get; private set; }

    public List<Monnit.CustomerLinkRequest> Result { get; private set; }

    public LoadByEmail(string email)
    {
      this.Email = email;
      this.Result = BaseDBObject.Load<Monnit.CustomerLinkRequest>(this.ToDataTable());
    }
  }

  [DBMethod("CustomerLinkRequest_LoadActiveByLinkCode")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[CustomerLinkRequest]\r\nWHERE LinkCode  = @LinkCode\r\n  AND IsDeleted = 0\r\nORDER BY CreateDate DESC;\r\n")]
  internal class LoadActiveByLinkCode : BaseDBMethod
  {
    [DBMethodParam("LinkCode", typeof (string))]
    public string LinkCode { get; private set; }

    public Monnit.CustomerLinkRequest Result { get; private set; }

    public LoadActiveByLinkCode(string linkCode)
    {
      this.LinkCode = linkCode;
      this.Result = BaseDBObject.Load<Monnit.CustomerLinkRequest>(this.ToDataTable()).FirstOrDefault<Monnit.CustomerLinkRequest>();
    }
  }
}
