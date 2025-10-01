// Decompiled with JetBrains decompiler
// Type: Monnit.Data.AccountSubscriptionType
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class AccountSubscriptionType
{
  [DBMethod("AccountSubscriptionType_LoadByKeyType")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  *\r\nFROM dbo.[AccountSubscriptionType]\r\nWHERE KeyType = @KeyType;\r\n")]
  internal class LoadByKeyType : BaseDBMethod
  {
    [DBMethodParam("KeyType", typeof (string))]
    public string KeyType { get; private set; }

    public Monnit.AccountSubscriptionType Result { get; private set; }

    public LoadByKeyType(string keyType)
    {
      this.KeyType = keyType;
      this.Result = BaseDBObject.Load<Monnit.AccountSubscriptionType>(this.ToDataTable()).FirstOrDefault<Monnit.AccountSubscriptionType>();
    }
  }
}
