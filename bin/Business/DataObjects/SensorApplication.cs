// Decompiled with JetBrains decompiler
// Type: Monnit.SensorApplication
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("SensorApplication")]
public class SensorApplication : BaseDBObject
{
  private long _SensorApplicationID = long.MinValue;
  private long _MonnitApplicationID = long.MinValue;
  private string _Name = string.Empty;

  [DBProp("SensorApplicationID", IsPrimaryKey = true)]
  public long SensorApplicationID
  {
    get => this._SensorApplicationID;
    set => this._SensorApplicationID = value;
  }

  [DBProp("ApplicationID")]
  [DBForeignKey("Application", "ApplicationID")]
  public long MonnitApplicationID
  {
    get => this._MonnitApplicationID;
    set => this._MonnitApplicationID = value;
  }

  public long ApplicationID
  {
    get => this.MonnitApplicationID;
    set => this.MonnitApplicationID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  public static SensorApplication Load(long id) => BaseDBObject.Load<SensorApplication>(id);

  public static List<SensorApplication> LoadAll() => BaseDBObject.LoadAll<SensorApplication>();

  public static List<SensorApplication> LoadByMonnitApplication(long monnitApplicationID)
  {
    return BaseDBObject.LoadByForeignKey<SensorApplication>("ApplicationID", (object) monnitApplicationID);
  }

  public static SensorApplication Unknown(long monnitApplicationID)
  {
    return new SensorApplication()
    {
      MonnitApplicationID = monnitApplicationID,
      Name = nameof (Unknown)
    };
  }
}
