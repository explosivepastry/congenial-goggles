// Decompiled with JetBrains decompiler
// Type: Monnit.Data.APIKey
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class APIKey
{
  [DBMethod("APIKey_LoadByKeyValue")]
  [DBMethodBody(DBMS.SqlServer, "\r\n \r\n  SELECT\r\n    *\r\n  FROM dbo.[APIKey]\r\n  WHERE KeyValue = @KeyValue;\r\n")]
  internal class LoadByKeyValue : BaseDBMethod
  {
    [DBMethodParam("KeyValue", typeof (string))]
    public string KeyValue { get; private set; }

    public List<Monnit.APIKey> Result { get; private set; }

    public LoadByKeyValue(string keyvalue)
    {
      this.KeyValue = keyvalue;
      this.Result = BaseDBObject.Load<Monnit.APIKey>(this.ToDataTable());
    }
  }
}
