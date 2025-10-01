// Decompiled with JetBrains decompiler
// Type: Monnit.Data.Preference
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;
using System.Data;

#nullable disable
namespace Monnit.Data;

internal class Preference
{
  [DBMethod("Preference_LoadPreferences")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDECLARE @Accounts TABLE\r\n(\r\n  ID INT IDENTITY(1,1),\r\n  Item INT\r\n);\r\n\r\nIF @AccountThemeID IS NULL\r\nBEGIN\r\n\r\n    INSERT INTO @Accounts (Item)\r\n    SELECT\r\n      Item = CONVERT(int,  i.item)\r\n    FROM dbo.[Account] a WITH (NOLOCK)\r\n    CROSS APPLY dbo.Split(AccountIDtree, '*') i\r\n    WHERE AccountID = @AccountID;\r\n\r\n    SELECT TOP 1 \r\n      @AccountThemeID = AccountThemeID \r\n    FROM @Accounts a\r\n    INNER JOIN dbo.[AccountTheme] t ON a.Item = t.AccountID\r\n    WHERE t.IsActive = 1\r\n    ORDER BY a.Id DESC;\r\n\r\nEND\r\n\r\n/*Priority of values returned: \r\n  Customer     - If AccountThemePreferenceTypeLink exists for accountthemeid and CustomerCanOverride = 1\r\n  Account      - If AccountThemePreferenceTypeLink exists for accountthemeid and AccountCanOverride  = 1 and above value is null\r\n  AccountTheme - If AccountThemePreferenceTypeLink exists for accountthemeid and above values are null\r\n  MonnitDefault- If AccountThemePreferenceTypeLink does not exist for accountthemeid or above values are null\r\n*/\r\nSELECT\r\n  [TypeName] = pt.[Name],\r\n  [Value]    = ISNULL(ISNULL(ISNULL(cp.[Value], ap.[value]), a.[DefaultValue]), pt.[DefaultValue])\r\nFROM dbo.[PreferenceType] pt                     WITH (NOLOCK)\r\nLEFT JOIN dbo.[AccountThemePreferenceTypeLink] a WITH (NOLOCK) ON pt.[PreferenceTypeID] = a.[PreferenceTypeID] AND a.[AccountThemeID] = @AccountThemeID\r\nLEFT JOIN dbo.[Preference] ap                    WITH (NOLOCK) ON ap.[AccountID]  = @AccountID AND a.[PreferenceTypeID] = ap.[PreferenceTypeID] AND a.[AccountCanOverride] = 1\r\nLEFT JOIN dbo.[Preference] cp                    WITH (NOLOCK) ON cp.[CustomerID] = @CustomerID AND a.[PreferenceTypeID] = cp.[PreferenceTypeID] AND a.[CustomerCanOverride] = 1\r\norder by pt.PreferenceTypeID;\r\n")]
  internal class LoadPreferences : BaseDBMethod
  {
    [DBMethodParam("AccountThemeID", typeof (long))]
    public long AccountThemeID { get; private set; }

    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("CustomerID", typeof (long))]
    public long CustomerID { get; private set; }

    public Dictionary<string, string> Result { get; private set; }

    public LoadPreferences(long accountThemeID, long accountID, long customerID)
    {
      this.AccountThemeID = accountThemeID;
      this.AccountID = accountID;
      this.CustomerID = customerID;
      DataTable dataTable = this.ToDataTable();
      this.Result = new Dictionary<string, string>();
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        if (!this.Result.ContainsKey((string) row["TypeName"]))
          this.Result.Add((string) row["TypeName"], (string) row["Value"]);
      }
    }
  }
}
