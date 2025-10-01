// Decompiled with JetBrains decompiler
// Type: Monnit.Data.SensorFile
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit.Data;

internal class SensorFile
{
  [DBMethod("SensorFile_DeleteOld")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE dbo.[SensorFile] WHERE [Date] < @Date;\r\n")]
  internal class DeleteOld : BaseDBMethod
  {
    [DBMethodParam("Date", typeof (DateTime))]
    public DateTime Date { get; private set; }

    public bool Result { get; private set; }

    public DeleteOld(DateTime olderThan)
    {
      this.Date = olderThan;
      this.Execute();
    }
  }
}
