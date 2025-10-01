// Decompiled with JetBrains decompiler
// Type: Monnit.Validation
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("Validation")]
public class Validation : BaseDBObject
{
  private long _ValidationID = long.MinValue;
  private long _CustomerID = long.MinValue;
  private string _Type = string.Empty;
  private string _Token = string.Empty;
  private string _TypeValue = string.Empty;
  private DateTime _ExpirationDate = DateTime.MinValue;
  private DateTime _OptInDate = DateTime.MinValue;

  [DBProp("ValidationID", IsPrimaryKey = true)]
  public long ValidationID
  {
    get => this._ValidationID;
    set => this._ValidationID = value;
  }

  [DBForeignKey("Customer", "CustomerID")]
  [DBProp("CustomerID")]
  public long CustomerID
  {
    get => this._CustomerID;
    set => this._CustomerID = value;
  }

  [DBProp("Type", MaxLength = 20)]
  public string Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  [DBProp("TypeValue", MaxLength = 100)]
  public string TypeValue
  {
    get => this._TypeValue;
    set => this._TypeValue = value;
  }

  [DBProp("Token", MaxLength = 20, AllowNull = true)]
  public string Token
  {
    get => this._Token;
    set => this._Token = value;
  }

  [DBProp("ExpirationDate", AllowNull = true)]
  public DateTime ExpirationDate
  {
    get => this._ExpirationDate;
    set => this._ExpirationDate = value;
  }

  [DBProp("OptInDate", AllowNull = true)]
  public DateTime OptInDate
  {
    get => this._OptInDate;
    set => this._OptInDate = value;
  }

  public static Validation Load(long ID) => BaseDBObject.Load<Validation>(ID);

  public static List<Validation> LoadByCustomerID(long id)
  {
    return BaseDBObject.LoadByForeignKey<Validation>("CustomerID", (object) id);
  }

  public static Validation LoadByTokenKey(string token) => new Validation.LoadByToken(token).Result;

  [DBMethod("Validation_LoadByToken")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[Validation] WHERE Token = @Token;\r\n")]
  internal class LoadByToken : BaseDBMethod
  {
    [DBMethodParam("Token", typeof (string))]
    public string Token { get; private set; }

    public Validation Result { get; private set; }

    public LoadByToken(string username)
    {
      this.Token = username;
      this.Result = BaseDBObject.Load<Validation>(this.ToDataTable()).FirstOrDefault<Validation>();
    }
  }
}
