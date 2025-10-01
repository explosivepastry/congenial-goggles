// Decompiled with JetBrains decompiler
// Type: Monnit.ReportingTag
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Linq;
using System.Text;

#nullable disable
namespace Monnit;

[DBClass("ReportingTag")]
public class ReportingTag : BaseDBObject
{
  private long _ReportingTagID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _Data = string.Empty;

  [DBProp("ReportingTagID", IsPrimaryKey = true)]
  public long ReportingTagID
  {
    get => this._ReportingTagID;
    set => this._ReportingTagID = value;
  }

  [DBProp("AccountID")]
  [DBForeignKey("Account", "AccountID")]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Data", MaxLength = 8000, AllowNull = true)]
  public string Data
  {
    get => this._Data;
    set => this._Data = value;
  }

  public string[] Tags
  {
    get => this.Data.Split(new char[1]{ '|' }, StringSplitOptions.RemoveEmptyEntries);
    set => this.Data = string.Join("|", value);
  }

  public void AddTag(string value) => this.Data = $"{this.Data}|{value}";

  public void RemoveTag(string value)
  {
    StringBuilder stringBuilder = new StringBuilder();
    foreach (string tag in this.Tags)
    {
      if (!(tag.ToLower() == value.ToLower()))
        stringBuilder.AppendFormat("|{0}", (object) tag);
    }
    this.Data = stringBuilder.ToString();
  }

  public static ReportingTag Load(long id) => BaseDBObject.Load<ReportingTag>(id);

  public static ReportingTag LoadByAccount(long accountID)
  {
    ReportingTag reportingTag = BaseDBObject.LoadByForeignKey<ReportingTag>("AccountID", (object) accountID).FirstOrDefault<ReportingTag>();
    if (reportingTag == null)
      reportingTag = new ReportingTag()
      {
        AccountID = accountID,
        Data = ""
      };
    return reportingTag;
  }

  public override void Save()
  {
    this.Tags = this.Tags;
    base.Save();
  }
}
