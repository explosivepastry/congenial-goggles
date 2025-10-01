// Decompiled with JetBrains decompiler
// Type: Monnit.SystemEmailContent
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Linq;
using System.Web.Mvc;

#nullable disable
namespace Monnit;

[DBClass("SystemEmailContent")]
public class SystemEmailContent : BaseDBObject
{
  private long _SystemEmailContentID = long.MinValue;
  private string _Body = string.Empty;
  private string _Subject = "";
  private DateTime _ModifiedDate = DateTime.MinValue;
  private string _TemplateFlag = "Generic";

  [DBProp("SystemEmailContentID", IsPrimaryKey = true)]
  public long SystemEmailContentID
  {
    get => this._SystemEmailContentID;
    set => this._SystemEmailContentID = value;
  }

  [AllowHtml]
  [DBProp("Body", MaxLength = 8000)]
  public string Body
  {
    get => this._Body;
    set => this._Body = value;
  }

  [DBProp("Subject", AllowNull = true)]
  public string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [DBProp("ModifiedDate")]
  public DateTime ModifiedDate
  {
    get => this._ModifiedDate;
    set => this._ModifiedDate = value;
  }

  [DBProp("TemplateFlag", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string TemplateFlag
  {
    get => this._TemplateFlag;
    set => this._TemplateFlag = value;
  }

  public static SystemEmailContent LoadBySystemEmailID(long systemEmailID)
  {
    return BaseDBObject.LoadByForeignKey<SystemEmailContent>("SystemEmailID", (object) systemEmailID).FirstOrDefault<SystemEmailContent>();
  }

  public static SystemEmailContent Load(long id) => BaseDBObject.Load<SystemEmailContent>(id);
}
