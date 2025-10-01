// Decompiled with JetBrains decompiler
// Type: Monnit.Data.MonnitUtil
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;

#nullable disable
namespace Monnit.Data;

internal class MonnitUtil
{
  [DBMethod("MonnitUtil_CheckDBConnection")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT 1;\r\n")]
  [DBMethodBody(DBMS.SQLite, "SELECT 1")]
  internal class CheckDBConnection : BaseDBMethod
  {
    public bool Result { get; private set; }

    public CheckDBConnection()
    {
      try
      {
        this.Result = this.ToScalarValue<bool>();
      }
      catch
      {
        this.Result = false;
      }
    }
  }
}
