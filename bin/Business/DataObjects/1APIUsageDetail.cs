// Decompiled with JetBrains decompiler
// Type: Monnit.Data.APIUsageDetail
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit.Data;

internal class APIUsageDetail
{
  [DBMethod("APIUsageDetail_Insert")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @Date DATETIME,\r\n        @Config VARCHAR(5);\r\n\r\nSET @Date = GETUTCDATE();\r\nSET @Config = (SELECT value FROM dbo.[ConfigData] WITH (NOLOCK) WHERE Keyname = 'APIUsage');\r\n\r\nIF @Config = 'True'\r\nBEGIN\r\n\r\n    INSERT INTO dbo.APIUsageDetail([APIUsageDetailGUID], [Day], [Date], [Method], [QueryString], [CustomerID],[AuthenticationResult])\r\n    VALUES (NEWID(), DATEPART(DAY, @Date), @Date, @Method, @QueryString, @CustomerID,@AuthenticationResult);\r\n\r\nEND\r\n")]
  internal class Insert : BaseDBMethod
  {
    [DBMethodParam("Method", typeof (string))]
    public string Method { get; private set; }

    [DBMethodParam("QueryString", typeof (string))]
    public string QueryString { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    [DBMethodParam("AuthenticationResult", typeof (string), DefaultValue = "Success")]
    public string AuthenticationResult { get; private set; }

    public bool Result { get; private set; }

    public Insert(string method, string queryString, long customerID, string authenticationResult)
    {
      this.Method = method;
      this.QueryString = queryString;
      this.CustomerID = customerID;
      this.AuthenticationResult = authenticationResult;
      try
      {
        this.Execute();
        this.Result = true;
      }
      catch
      {
        this.Result = false;
      }
    }
  }
}
