// Decompiled with JetBrains decompiler
// Type: Monnit.EmailTemplate
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

#nullable disable
namespace Monnit;

[MetadataType(typeof (EmailTemplateMetadata))]
[DBClass("EmailTemplate")]
public class EmailTemplate : BaseDBObject
{
  private long _EmailTemplateID = long.MinValue;
  private string _Name = string.Empty;
  private string _Subject = string.Empty;
  private string _Template = string.Empty;
  private long _AccountID = long.MinValue;
  private string _Flags = string.Empty;

  [DBProp("EmailTemplateID", IsPrimaryKey = true)]
  public long EmailTemplateID
  {
    get => this._EmailTemplateID;
    set => this._EmailTemplateID = value;
  }

  [Required]
  [DBProp("Name", AllowNull = false, MaxLength = 200)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  [Required]
  [DBProp("Subject", AllowNull = false, MaxLength = 200)]
  public string Subject
  {
    get => this._Subject;
    set => this._Subject = value;
  }

  [Required]
  [AllowHtml]
  [DBProp("Template", AllowNull = false, MaxLength = 2147483647 /*0x7FFFFFFF*/)]
  public string Template
  {
    get => this._Template;
    set => this._Template = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = true)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Flags", AllowNull = false, MaxLength = 2000)]
  public string Flags
  {
    get => this._Flags;
    set => this._Flags = value;
  }

  public static EmailTemplate Load(long ID) => BaseDBObject.Load<EmailTemplate>(ID);

  public string MailMergeOld(string Content)
  {
    return this.Template.Replace("{Content}", Content).Replace("{OptOutAddress}", "");
  }

  public string MailMerge(string Content, string optOutAddress)
  {
    return this.Template.Replace("{Content}", Content).Replace("{OptOutAddress}", optOutAddress);
  }

  public bool ContainsFlag(eEmailTemplateFlag flag)
  {
    string flags = this.Flags;
    char[] chArray = new char[1]{ '|' };
    foreach (string str in flags.Split(chArray))
    {
      if (str == flag.ToString())
        return true;
    }
    return false;
  }

  public void AddFlag(eEmailTemplateFlag flag)
  {
    if (this.ContainsFlag(flag))
      return;
    this.Flags = $"{this.Flags}|{flag}";
  }

  public void RemoveFlag(eEmailTemplateFlag flag)
  {
    this.Flags.Replace(flag.ToString(), "");
    this.Flags.Replace("||", "|");
  }

  public static List<EmailTemplate> LoadByAccountID(long accountID)
  {
    string key = $"EmailTemplates_{accountID}";
    List<EmailTemplate> emailTemplateList = TimedCache.RetrieveObject<List<EmailTemplate>>(key);
    if (emailTemplateList == null)
    {
      emailTemplateList = BaseDBObject.LoadByForeignKey<EmailTemplate>("AccountID", (object) accountID);
      TimedCache.AddObjectToCach(key, (object) emailTemplateList);
    }
    return emailTemplateList;
  }

  public static void ClearCache(long accountID)
  {
    TimedCache.RemoveObject($"EmailTemplates_{accountID}");
  }

  public static EmailTemplate LoadBest(Account account, eEmailTemplateFlag flag)
  {
    EmailTemplate emailTemplate = EmailTemplate.LoadByAccountAndFlag(account.AccountID, flag);
    Account account1 = account;
    if (emailTemplate == null)
    {
      do
      {
        emailTemplate = EmailTemplate.LoadByAccountAndFlag(account1.RetailAccountID, flag);
        account1 = Account.Load(account1.RetailAccountID);
      }
      while (emailTemplate == null && account1 != null && account1.RetailAccountID != account1.AccountID);
    }
    return emailTemplate;
  }

  public static EmailTemplate LoadByAccountAndFlag(long accountID, eEmailTemplateFlag flag)
  {
    EmailTemplate emailTemplate1 = (EmailTemplate) null;
    List<EmailTemplate> emailTemplateList = EmailTemplate.LoadByAccountID(accountID);
    if (emailTemplateList.Count > 0)
    {
      int num1 = ((IEnumerable<string>) Enum.GetNames(typeof (eEmailTemplateFlag))).Count<string>();
      foreach (EmailTemplate emailTemplate2 in emailTemplateList)
      {
        if (emailTemplate2.ContainsFlag(flag))
        {
          int num2 = ((IEnumerable<string>) emailTemplate2.Flags.Split('|')).Count<string>();
          if (emailTemplate1 == null || num2 < num1)
          {
            num1 = num2;
            emailTemplate1 = emailTemplate2;
          }
        }
      }
      if (emailTemplate1 == null)
      {
        foreach (EmailTemplate emailTemplate3 in emailTemplateList)
        {
          if (emailTemplate3.ContainsFlag(eEmailTemplateFlag.Generic))
          {
            emailTemplate1 = emailTemplate3;
            break;
          }
        }
      }
    }
    return emailTemplate1;
  }
}
