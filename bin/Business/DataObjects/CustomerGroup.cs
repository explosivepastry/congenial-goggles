// Decompiled with JetBrains decompiler
// Type: Monnit.CustomerGroup
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("CustomerGroup")]
public class CustomerGroup : BaseDBObject
{
  private long _CustomerGroupID = long.MinValue;
  private long _AccountID = long.MinValue;
  private string _Name = string.Empty;

  [DBProp("CustomerGroupID", IsPrimaryKey = true)]
  public long CustomerGroupID
  {
    get => this._CustomerGroupID;
    set => this._CustomerGroupID = value;
  }

  [DBForeignKey("Account", "AccountID")]
  [DBProp("AccountID", AllowNull = false)]
  public long AccountID
  {
    get => this._AccountID;
    set => this._AccountID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/, AllowNull = false)]
  public string Name
  {
    get => this._Name;
    set => this._Name = value;
  }

  public bool RemoveCustomer(long customerID, eNotificationType notiType)
  {
    try
    {
      foreach (CustomerGroupLink customerGroupLink in CustomerGroupLink.LoadByCustomerIDandGroupID(customerID, this.CustomerGroupID))
      {
        if (customerGroupLink.NotificationType == notiType)
        {
          DataTable dataTable = new DataTable();
          dataTable = CustomerGroupLink.DeleteByCustomerGroupLinkID(customerGroupLink.CustomerGroupLinkID);
        }
      }
      return true;
    }
    catch
    {
      return false;
    }
  }

  public bool AddCustomer(long customerID, int delayMinutes, eNotificationType notiType)
  {
    try
    {
      CustomerGroupLink customerGroupLink = CustomerGroupLink.LoadByCustomerIDandGroupID(customerID, this.CustomerGroupID).Where<CustomerGroupLink>((System.Func<CustomerGroupLink, bool>) (m => m.CustomerID == customerID && m.NotificationType == notiType)).FirstOrDefault<CustomerGroupLink>();
      if (customerGroupLink == null)
      {
        customerGroupLink = new CustomerGroupLink();
        customerGroupLink.CustomerID = customerID;
        customerGroupLink.CustomerGroupID = this.CustomerGroupID;
        customerGroupLink.NotificationType = notiType;
      }
      customerGroupLink.DelayMinutes = delayMinutes;
      customerGroupLink.Save();
      return true;
    }
    catch (Exception ex)
    {
      ex.Log($"CustomerGroup.AddCustomer/ | customerID = {customerID}, delayMinutes = {delayMinutes}, eNotificationType = {notiType}");
      return false;
    }
  }

  public override void Delete()
  {
    try
    {
      if (CustomerGroup.DeleteByCustomerGroupID(this.CustomerGroupID).Rows[0][0].ToInt() == 0)
        throw new Exception("Error Deleting Group.");
    }
    catch (Exception ex)
    {
      ex.Log("CustomerGroup.Delete | CustomerGroup Failed to Delete GroupID: " + this.CustomerGroupID.ToString());
    }
  }

  public static DataTable DeleteByCustomerGroupID(long customerGroupID)
  {
    return new Monnit.Data.CustomerGroup.DeleteByCustomerGroupID(customerGroupID).Result;
  }

  public static List<CustomerGroup> LoadByAccountID(long accountID)
  {
    return new Monnit.Data.CustomerGroup.LoadByAccountID(accountID).Result;
  }

  public static CustomerGroup Load(long id) => BaseDBObject.Load<CustomerGroup>(id);
}
