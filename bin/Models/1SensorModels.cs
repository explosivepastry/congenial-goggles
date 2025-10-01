// Decompiled with JetBrains decompiler
// Type: Data.NotificationRecipientDevice
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using Monnit;
using RedefineImpossible;
using System.Data;

#nullable disable
namespace Data;

internal class NotificationRecipientDevice : BaseDBObject
{
  [DBMethod("NotificationRecipientDevice_LoadByAccountID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nINTO #Temp1\r\nFROM dbo.[Sensor] WITH (NOLOCK)\r\nWHERE [AccountID] = @AccountID\r\n  AND [MonnitApplicationID] IN (12, 13, 73, 76, 90, 93, 94, 97, 120, 125, 153)\r\n  AND [IsDeleted] != 1;\r\n\r\n/**********************************************************************\r\n\r\n  Return in four result sets: control, local alert, Thermostat, and Reset Accumulator\r\n\r\n**********************************************************************/\r\n  --Control\r\nSELECT\r\n  *\r\nFROM #Temp1 WITH (NOLOCK)\r\nWHERE [MonnitApplicationID] IN (12,76)\r\n\r\n--LocalAlert\r\nSELECT\r\n  *\r\nFROM #Temp1 WITH (NOLOCK)\r\nWHERE [MonnitApplicationID] IN (13)\r\n\r\n--Thermostat\r\nSELECT\r\n  *\r\nFROM #Temp1 WITH (NOLOCK)\r\nWHERE [MonnitApplicationID] IN (97, 125);\r\n\r\n--Reset Accumulator devices\r\nSELECT\r\n  *\r\nFROM #Temp1 WITH (NOLOCK)\r\nWHERE [MonnitApplicationID] IN (73, 90, 93, 94, 120, 153);\r\n")]
  internal class LoadByAccountID : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    public iMonnit.Models.NotificationRecipientDevice Result { get; private set; }

    public LoadByAccountID(long accountID)
    {
      this.AccountID = accountID;
      this.Result = new iMonnit.Models.NotificationRecipientDevice();
      DataSet dataSet = this.ToDataSet();
      this.Result.ControlUnitList = BaseDBObject.Load<Sensor>(dataSet.Tables[0]);
      this.Result.LocalAlertList = BaseDBObject.Load<Sensor>(dataSet.Tables[1]);
      this.Result.ThermostatList = BaseDBObject.Load<Sensor>(dataSet.Tables[2]);
      this.Result.ResetAccList = BaseDBObject.Load<Sensor>(dataSet.Tables[3]);
    }

    public static iMonnit.Models.NotificationRecipientDevice Exec(long accountID)
    {
      return new NotificationRecipientDevice.LoadByAccountID(accountID).Result;
    }
  }
}
