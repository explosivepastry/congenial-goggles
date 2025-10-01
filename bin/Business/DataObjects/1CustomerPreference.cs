// Decompiled with JetBrains decompiler
// Type: Monnit.Data.CustomerPreference
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class CustomerPreference
{
  [DBMethod("CustomerPreference_LoadByCustomerIDandGroup")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF @CustomerID IS NOT NULL\r\nBEGIN\r\n\r\n    SELECT\r\n      *\r\n    FROM dbo.[CustomerPreference] WITH (NOLOCK)\r\n    WHERE CustomerID      = @CustomerID\r\n      AND PreferenceGroup = @Group;\r\n\r\nEND ELSE\r\nBEGIN\r\n\r\n    SELECT\r\n      *\r\n    FROM dbo.[CustomerPreference] WITH (NOLOCK)\r\n    WHERE CustomerID IS NULL\r\n      AND PreferenceGroup = @Group;\r\n\r\nEND;\r\n")]
  internal class LoadByCustomerIDandGroup : BaseDBMethod
  {
    [DBMethodParam("Group", typeof (string))]
    public string Group { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public List<Monnit.CustomerPreference> Result { get; private set; }

    public LoadByCustomerIDandGroup(long customerID, string group)
    {
      this.Group = group;
      this.CustomerID = customerID;
      this.Result = BaseDBObject.Load<Monnit.CustomerPreference>(this.ToDataTable());
    }
  }

  [DBMethod("CustomerPreference_LoadByCustomerIDAndGroupAndName")]
  [DBMethodBody(DBMS.SqlServer, "\r\nIF @CustomerID IS NOT NULL\r\nBEGIN\r\n\r\n    SELECT TOP 1 \r\n      * \r\n    FROM dbo.[CustomerPreference] WITH (NOLOCK)\r\n    WHERE [CustomerID]      = @CustomerID \r\n      AND [PreferenceGroup] = @Group \r\n      AND [Name]            = @Name;\r\n\r\nEND ELSE\r\nBEGIN\r\n\r\n    SELECT TOP 1 \r\n      * \r\n    FROM dbo.[CustomerPreference] WITH (NOLOCK)\r\n    WHERE [CustomerID] IS NULL \r\n      AND [PreferenceGroup] = @Group \r\n      AND [Name]            = @Name;\r\n\r\nEND;\r\n")]
  internal class LoadByCustomerIDAndGroupAndName : BaseDBMethod
  {
    [DBMethodParam("Name", typeof (string))]
    public string Name { get; private set; }

    [DBMethodParam("Group", typeof (string))]
    public string Group { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public Monnit.CustomerPreference Result { get; private set; }

    public LoadByCustomerIDAndGroupAndName(long customerID, string group, string name)
    {
      this.Name = name;
      this.Group = group;
      this.CustomerID = customerID;
      this.Result = BaseDBObject.Load<Monnit.CustomerPreference>(this.ToDataTable()).FirstOrDefault<Monnit.CustomerPreference>();
    }
  }
}
