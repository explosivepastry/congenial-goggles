// Decompiled with JetBrains decompiler
// Type: Monnit.Data.GatewayMessage
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit.Data;

internal class GatewayMessage
{
  [DBMethod("GatewayMessage_UPDATE")]
  [DBMethodBody(DBMS.SqlServer, "\r\nUPDATE dbo.[GatewayMessage]\r\n  SET [Power]                         = @Power,\r\n      [Battery]                       = @Battery,\r\n      [MeetsNotificationRequirement]  = @MeetsNotificationRequirement,\r\n      [MessageType]                   = @MessageType,\r\n      [MessageCount]                  = @MessageCount,\r\n      [SignalStrength]                = @SignalStrength\r\nWHERE [ReceivedDate]       = @ReceivedDate\r\n  AND [GatewayID]          = @GatewayID\r\n  AND [GatewaymessageGUID] = @GatewayMessageGUID;\r\n")]
  internal class Update : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("Power", typeof (int))]
    public int Power { get; private set; }

    [DBMethodParam("Battery", typeof (int))]
    public int Battery { get; private set; }

    [DBMethodParam("ReceivedDate", typeof (DateTime))]
    public DateTime ReceivedDate { get; private set; }

    [DBMethodParam("MeetsNotificationRequirement", typeof (bool))]
    public bool MeetsNotificationRequirement { get; private set; }

    [DBMethodParam("MessageType", typeof (int))]
    public int MessageType { get; private set; }

    [DBMethodParam("MessageCount", typeof (int))]
    public int MessageCount { get; private set; }

    [DBMethodParam("SignalStrength", typeof (int))]
    public int SignalStrength { get; private set; }

    [DBMethodParam("GatewayMessageGUID", typeof (Guid))]
    public Guid GatewayMessageGUID { get; private set; }

    public Update(
      long gatewayID,
      int power,
      int battery,
      DateTime receivedDate,
      bool meetsNotificationRequirement,
      int messageType,
      int messageCount,
      int signalStrength,
      Guid gatewayMessageGUID)
    {
      this.GatewayID = gatewayID;
      this.Power = power;
      this.Battery = battery;
      this.ReceivedDate = receivedDate;
      this.MeetsNotificationRequirement = meetsNotificationRequirement;
      this.MessageType = messageType;
      this.MessageCount = messageCount;
      this.SignalStrength = signalStrength;
      this.GatewayMessageGUID = gatewayMessageGUID;
      this.Execute();
    }
  }

  [DBMethod("GatewayMessage_LoadByGatewayAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/*********************************************************\r\n                      CHANGE LOG\r\n**********************************************************\r\n  Date          ModifiedBy        Comments\r\n  ------------  ----------------  ------------------------\r\n  7/8/2016      Brandon Young     Written with dynamic sql generation\r\n                                  Instead of pulling from View\r\n  8/31/2016     Brandon Young     Written changed back to pull from View                                  \r\n  1/12/2017     Greg Cardall      Added ReceivedDate to the filter when the GUID is passed\r\n**********************************************************\r\nDECLARE @GatewayID        bigint\r\nDECLARE @FromDate         datetime\r\nDECLARE @ToDate           datetime\r\nDECLARE @Count            int\r\nDECLARE @GatewayMessageGUID  uniqueidentifier\r\nSET @GatewayID          = 100\r\nSET @FromDate           = '2016-07-01 03:00:00'\r\nSET @ToDate             = '2016-07-08 13:00:00'\r\nSET @Count              = 500\r\nSET @GatewayMessageGUID = '704F8953-FEEB-4E3A-BE20-B541598C4053'\r\nEXEC [dbo].[GatewayMessage_LoadByGatewayAndDateRange] @GatewayID, @FromDate, @ToDate, @Count, @GatewayMessageGUID;\r\n*********************************************************/\r\n\r\nDECLARE @_GatewayID BIGINT;\r\nSET @_GatewayID = @GatewayID;\r\n\r\n--If we have a valid @GatewayMessageGUID, use it\r\nIF @GatewayMessageGUID IS NOT NULL\r\nBEGIN\r\n\r\n    SELECT TOP 1\r\n      @ToDate = ReceivedDate\r\n    FROM dbo.[GatewayMessage] WITH(NOLOCK)\r\n    WHERE ReceivedDate        > @FromDate\r\n      AND ReceivedDate        < @ToDate\r\n      AND GatewayID           = @_GatewayID\r\n      AND GatewayMessageGUID  = @GatewayMessageGUID;\r\n\t\r\nEND\r\n\t\r\nSELECT TOP(@Count)\r\n  *\r\nFROM dbo.[GatewayMessage] WITH (NOLOCK)\r\nWHERE GatewayID     = @_GatewayID\r\n  AND ReceivedDate  > @FromDate \r\n  AND ReceivedDate  < @ToDate\r\nORDER BY ReceivedDate DESC;\r\n")]
  internal class LoadByGatewayAndDateRange : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public int Count { get; private set; }

    [DBMethodParam("GatewayMessageGUID", typeof (Guid))]
    public Guid GatewayMessageGUID { get; private set; }

    public List<Monnit.GatewayMessage> Result { get; private set; }

    public LoadByGatewayAndDateRange(
      long gatewayID,
      DateTime fromDate,
      DateTime toDate,
      int count)
    {
      this.GatewayID = gatewayID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Count = count;
      this.GatewayMessageGUID = Guid.Empty;
      this.Result = BaseDBObject.Load<Monnit.GatewayMessage>(this.ToDataTable());
    }
  }

  [DBMethod("GatewayMessage_LoadByGatewayAndDateRange2")]
  [DBMethodBody(DBMS.SqlServer, "\r\n/*********************************************************\r\n                      CHANGE LOG\r\n**********************************************************\r\n  Date          ModifiedBy        Comments\r\n  ------------  ----------------  ------------------------\r\n  7/8/2016      Brandon Young     Written with dynamic sql generation\r\n                                  Instead of pulling from View\r\n  8/31/2016     Brandon Young     Written changed back to pull from View                                  \r\n  1/12/2017     Greg Cardall      Added ReceivedDate to the filter when the GUID is passed\r\n**********************************************************\r\nDECLARE @GatewayID        bigint\r\nDECLARE @FromDate         datetime\r\nDECLARE @ToDate           datetime\r\nDECLARE @Count            int\r\nDECLARE @GatewayMessageGUID  uniqueidentifier\r\nSET @GatewayID          = 100\r\nSET @FromDate           = '2016-07-01 03:00:00'\r\nSET @ToDate             = '2016-07-08 13:00:00'\r\nSET @Count              = 500\r\nSET @GatewayMessageGUID = '704F8953-FEEB-4E3A-BE20-B541598C4053'\r\nEXEC [dbo].[GatewayMessage_LoadByGatewayAndDateRange2] @GatewayID, @FromDate, @ToDate, @Count, @GatewayMessageGUID;\r\n*********************************************************/\r\n\r\nDECLARE @_GatewayID BIGINT;\r\nSET @_GatewayID = @GatewayID;\r\n\r\n--If we have a valid @GatewayMessageGUID, use it\r\nIF @GatewayMessageGUID IS NOT NULL\r\nBEGIN\r\n\r\n    SELECT TOP 1 \r\n      @ToDate = ReceivedDate \r\n    FROM dbo.[GatewayMessage] WITH(NOLOCK) \r\n    WHERE ReceivedDate       > @FromDate \r\n      AND ReceivedDate       < @ToDate\r\n      AND GatewayID          = @_GatewayID\r\n      AND GatewayMessageGUID = @GatewayMessageGUID;\r\n\r\nEND\r\n\r\nSELECT TOP(@Count)\r\n  *\r\nFROM dbo.[GatewayMessage] WITH (NOLOCK)\r\nWHERE GatewayID     = @_GatewayID\r\n  AND ReceivedDate  > @FromDate \r\n  AND ReceivedDate  < @ToDate\r\nORDER BY ReceivedDate DESC\r\nOPTION (OPTIMIZE FOR UNKNOWN);\r\n")]
  internal class LoadByGatewayAndDateRange2 : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public int Count { get; private set; }

    [DBMethodParam("GatewayMessageGUID", typeof (Guid))]
    public Guid GatewayMessageGUID { get; private set; }

    public List<Monnit.GatewayMessage> Result { get; private set; }

    public LoadByGatewayAndDateRange2(
      long gatewayID,
      DateTime fromDate,
      DateTime toDate,
      int count,
      Guid? dataMessageGUID)
    {
      this.GatewayID = gatewayID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Count = count;
      this.GatewayMessageGUID = dataMessageGUID ?? Guid.Empty;
      this.Result = BaseDBObject.Load<Monnit.GatewayMessage>(this.ToDataTable());
    }
  }

  [DBMethod("GatewayMessage_LoadLastByGateway")]
  [DBMethodBody(DBMS.SqlServer, "\r\nDeclare @LastCommunicationDate DateTime\r\n\r\nSELECT @LastCommunicationDate = ISNULL(s.LastCommunicationDate,g.LastCommunicationDate)\r\nFROM Gateway g\r\nLEFT JOIN GatewayStatus s ON s.GatewayID = g.GatewayID\r\nWHERE g.GatewayID = @GatewayID\r\n\r\nSELECT TOP 1\r\n  *\r\nFROM dbo.[GatewayMessage] gm\r\nWHERE gm.GatewayID = @GatewayID\r\nand gm.ReceivedDate = @LastCommunicationDate\r\nORDER BY\r\n  ReceivedDate DESC;\r\n")]
  [DBMethodBody(DBMS.SQLite, "Select * FROM GatewayMessage WHERE GatewayID = @GatewayID ORDER BY GAtewayMessageID DESC Limit 1;")]
  internal class LoadLastByGateway : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public Monnit.GatewayMessage Result { get; private set; }

    public LoadLastByGateway(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.GatewayMessage>(this.ToDataTable()).FirstOrDefault<Monnit.GatewayMessage>();
    }
  }

  [DBMethod("GatewayMessage_LoadRangeByNetwork")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP (@Limit)\r\n  gm.*\r\nFROM dbo.[GatewayMessage] gm WITH (NOLOCK)\r\nINNER JOIN dbo.[Gateway] g ON g.GatewayID = gm.GatewayID\r\nWHERE g.CSNetID       = @NetworkID\r\n  AND gm.ReceivedDate >= @From\r\n  AND gm.ReceivedDate <= @To\r\nORDER BY\r\n  ReceivedDate DESC;\r\n")]
  internal class LoadRangeByNetWork : BaseDBMethod
  {
    [DBMethodParam("NetworkID", typeof (long))]
    public long NetworkID { get; private set; }

    [DBMethodParam("From", typeof (DateTime))]
    public DateTime From { get; private set; }

    [DBMethodParam("To", typeof (DateTime))]
    public DateTime To { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.GatewayMessage> Result { get; private set; }

    public LoadRangeByNetWork(long networkID, DateTime from, DateTime to, int limit)
    {
      this.NetworkID = networkID;
      this.From = from;
      this.To = to;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.GatewayMessage>(this.ToDataTable());
    }
  }

  [DBMethod("GatewayMessage_LoadRangeByAccount")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP(@Limit)\r\n  gm.*\r\nFROM dbo.[GatewayMessage] gm WITH (NOLOCK)\r\nINNER JOIN dbo.[Gateway] g ON g.GatewayID = gm.GatewayID\r\nINNER JOIN dbo.[CSNet] cs ON cs.CSNetID = g.CSNetID\r\nWHERE cs.AccountID    = @AccountID \r\n  AND gm.ReceivedDate >= @From \r\n  AND gm.ReceivedDate <= @To \r\nORDER BY\r\n  ReceivedDate DESC;\r\n")]
  internal class LoadRangeByAccount : BaseDBMethod
  {
    [DBMethodParam("AccountID", typeof (long))]
    public long AccountID { get; private set; }

    [DBMethodParam("From", typeof (DateTime))]
    public DateTime From { get; private set; }

    [DBMethodParam("To", typeof (DateTime))]
    public DateTime To { get; private set; }

    [DBMethodParam("Limit", typeof (int))]
    public int Limit { get; private set; }

    public List<Monnit.GatewayMessage> Result { get; private set; }

    public LoadRangeByAccount(long accountID, DateTime from, DateTime to, int limit)
    {
      this.AccountID = accountID;
      this.From = from;
      this.To = to;
      this.Limit = limit;
      this.Result = BaseDBObject.Load<Monnit.GatewayMessage>(this.ToDataTable());
    }
  }
}
