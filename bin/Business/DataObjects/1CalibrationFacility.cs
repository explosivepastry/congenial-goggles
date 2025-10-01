// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CalibrationFacility
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class CalibrationFacility
{
  [DBMethod("CalibrationFacility_LoadByName")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSElECT \r\n  TOP 1 *\r\nFROM dbo.[CalibrationFacility]\r\nWHERE [Name] = @Name;\r\n")]
  internal class LoadByName : BaseDBMethod
  {
    [DBMethodParam("Name", typeof (string))]
    public string Name { get; private set; }

    public Monnit.CalibrationFacility Result { get; private set; }

    public LoadByName(string name)
    {
      this.Name = name.ToLower();
      this.Result = BaseDBObject.Load<Monnit.CalibrationFacility>(this.ToDataTable()).FirstOrDefault<Monnit.CalibrationFacility>();
    }
  }
}
