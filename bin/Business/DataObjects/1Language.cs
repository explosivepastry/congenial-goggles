// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Language
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class Language
{
  [DBMethod("Language_LoadActive")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT \r\n  *\r\nFROM dbo.[Language]\r\nWHERE IsActive = 1;\r\n")]
  internal class LoadActive : BaseDBMethod
  {
    public List<Monnit.Language> Result { get; private set; }

    public LoadActive() => this.Result = BaseDBObject.Load<Monnit.Language>(this.ToDataTable());
  }
}
