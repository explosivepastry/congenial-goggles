// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountVerification
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class AccountVerification
{
  [DBMethod("AccountVerification_LoadByCode")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[AccountVerification]\r\nWHERE VerificationCode = @VerificationCode;\r\n")]
  internal class LoadByCode : BaseDBMethod
  {
    [DBMethodParam("VerificationCode", typeof (string))]
    public string VerificationCode { get; private set; }

    public Monnit.AccountVerification Result { get; private set; }

    public LoadByCode(string verificationCode)
    {
      this.VerificationCode = verificationCode;
      this.Result = BaseDBObject.Load<Monnit.AccountVerification>(this.ToDataTable()).FirstOrDefault<Monnit.AccountVerification>();
    }
  }

  [DBMethod("AccountVerification_LoadByEmailAddress")]
  [DBMethodBody(DBMS.SqlServer, "\r\n\u200B\r\nDELETE AccountVerification WHERE CreateDate < DATEADD(Day, -1, GETUTCDATE())\r\n\r\nSELECT\r\n  *\r\nFROM dbo.[AccountVerification]\r\nWHERE EmailAddress = @EmailAddress;\r\n")]
  internal class LoadByEmailAddress : BaseDBMethod
  {
    [DBMethodParam("EmailAddress", typeof (string))]
    public string EmailAddress { get; private set; }

    public Monnit.AccountVerification Result { get; private set; }

    public LoadByEmailAddress(string emailAddress)
    {
      this.EmailAddress = emailAddress;
      this.Result = BaseDBObject.Load<Monnit.AccountVerification>(this.ToDataTable()).FirstOrDefault<Monnit.AccountVerification>();
    }
  }
}
