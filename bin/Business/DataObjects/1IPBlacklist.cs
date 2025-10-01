// Decompiled with JetBrains decompiler
// Type: Monnit.Data.IPBlackList
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class IPBlackList
{
  [DBMethod("IPBlackList_LoadByIP")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDELETE IPBlackList where FirstFailedAttempt < DATEADD(MINUTE, -5, GETUTCDATE())\r\n\r\nSELECT * FROM IPBlackList WHERE IPAddress = @IPAddress;\r\n")]
  internal class LoadByIP : BaseDBMethod
  {
    [DBMethodParam("IPAddress", typeof (string))]
    public string IPAddress { get; private set; }

    [DBMethodParam("AttemptPeriod", typeof (int))]
    public int AttemptPeriod { get; private set; }

    public List<IPBlacklist> Result { get; private set; }

    public LoadByIP(string iPAddress, int attemptPeriod)
    {
      this.IPAddress = iPAddress;
      this.AttemptPeriod = attemptPeriod;
      this.Result = BaseDBObject.Load<IPBlacklist>(this.ToDataTable());
    }
  }
}
