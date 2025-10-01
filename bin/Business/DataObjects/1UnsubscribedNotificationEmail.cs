// Decompiled with JetBrains decompiler
// Type: Monnit.Data.UnsubscribedNotificationEmail
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class UnsubscribedNotificationEmail
{
  [DBMethod("UnsubscribedNotificationEmail_LoadByEmail")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[UnsubscribedNotificationEmail]\r\nWHERE EmailAddress = @EmailAddress;\r\n")]
  internal class LoadByEmail : BaseDBMethod
  {
    [DBMethodParam("EmailAddress", typeof (string))]
    public string EmailAddress { get; private set; }

    public List<Monnit.UnsubscribedNotificationEmail> Result { get; private set; }

    public LoadByEmail(string emailAddress)
    {
      this.EmailAddress = emailAddress;
      this.Result = BaseDBObject.Load<Monnit.UnsubscribedNotificationEmail>(this.ToDataTable());
    }
  }
}
