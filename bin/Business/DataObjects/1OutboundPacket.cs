// Decompiled with JetBrains decompiler
// Type: Monnit.Data.OutboundPacket
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class OutboundPacket
{
  [DBMethod("OutboundPacket_Insert")]
  [DBMethodBody(DBMS.SqlServer, "\r\nINSERT INTO dbo.[OutboundPacket] ([Encrypted], [CSNetID], [APNID], [Version], [Power], [Response], [MessageCount], [Message], [SentDate], [More], [Sequence],[ProcessingTime], [Day], [OutboundPacketGUID], [OutboundPacketID])\r\nVALUES(@Encrypted, NULL, @APNID, @Version, @Power, @Response, @MessageCount, @Message, @SentDate, @More, @Sequence, @ProcessingTime, @Day, @OutboundPacketGUID, NULL)\r\n")]
  internal class Insert : BaseDBMethod
  {
    [DBMethodParam("OutboundPacketGUID", typeof (Guid))]
    public Guid OutboundPacketGUID { get; private set; }

    [DBMethodParam("Encrypted", typeof (bool))]
    public bool Encrypted { get; private set; }

    [DBMethodParam("APNID", typeof (long))]
    public long APNID { get; private set; }

    [DBMethodParam("Version", typeof (int))]
    public int Version { get; private set; }

    [DBMethodParam("Power", typeof (int))]
    public int Power { get; private set; }

    [DBMethodParam("Response", typeof (int))]
    public int Response { get; private set; }

    [DBMethodParam("MessageCount", typeof (int))]
    public int MessageCount { get; private set; }

    [DBMethodParam("Message", typeof (byte))]
    public byte[] Message { get; private set; }

    [DBMethodParam("SentDate", typeof (DateTime))]
    public DateTime SentDate { get; private set; }

    [DBMethodParam("More", typeof (bool))]
    public bool More { get; private set; }

    [DBMethodParam("Sequence", typeof (int))]
    public int Sequence { get; private set; }

    [DBMethodParam("ProcessingTime", typeof (TimeSpan))]
    public TimeSpan ProcessingTime { get; private set; }

    [DBMethodParam("Day", typeof (int))]
    public int Day { get; private set; }

    public Insert(
      Guid outboundPacketGUID,
      bool encrypted,
      long aPNID,
      int version,
      int power,
      int response,
      int messageCount,
      byte[] message,
      DateTime sentDate,
      bool more,
      int sequence,
      TimeSpan processingTime,
      int day)
    {
      this.OutboundPacketGUID = outboundPacketGUID;
      this.Encrypted = encrypted;
      this.APNID = aPNID;
      this.Version = version;
      this.Power = power;
      this.Response = response;
      this.MessageCount = messageCount;
      this.Message = message;
      this.SentDate = sentDate;
      this.More = more;
      this.Sequence = sequence;
      this.ProcessingTime = processingTime;
      this.Day = day;
      this.Execute();
    }
  }

  [DBMethod("OutboundPacket_LoadByNetworkAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT top 100\r\n  ip.*\r\nFROM dbo.[OutboundPacket] ip WITH (NOLOCK)\r\nINNER JOIN dbo.[Gateway] g ON g.GatewayID = ip.APNID\r\nWHERE g.CSNetID = @CSNetID\r\n  AND ip.SentDate BETWEEN @FromDate AND @ToDate\r\nORDER BY\r\n  ip.OutboundPacketID DESC;\r\n")]
  internal class LoadByNetworkAndDateRange : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public List<Monnit.OutboundPacket> Result { get; private set; }

    public LoadByNetworkAndDateRange(long cSNetID, DateTime fromDate, DateTime toDate)
    {
      this.CSNetID = cSNetID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.OutboundPacket>(this.ToDataTable());
    }
  }

  [DBMethod("OutboundPacket_LoadByAPNID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 100\r\n  *\r\nFROM dbo.[OutboundPacket] WITH (NOLOCK)\r\nWHERE APNID = @GatewayID\r\nORDER BY\r\n  OutboundPacketID DESC;\r\n")]
  internal class LoadByAPNID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.OutboundPacket> Result { get; private set; }

    public LoadByAPNID(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.OutboundPacket>(this.ToDataTable());
    }
  }

  [DBMethod("OutboundPacket_Filter")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP (@Count) \r\n  * \r\nFROM dbo.[OutboundPacket] WITH (NOLOCK)    \r\nWHERE [SentDate] BETWEEN @FromDate AND @ToDate\r\n  AND (CONVERT(VARCHAR(MAX), [Message], 1)   LIKE '%'+@Msgsrch+'%' OR @Msgsrch is NULL)\r\n  AND ([Response] = @Response  OR @Response  IS NULL)\r\n  AND ([APNID]    = @GatewayID OR @GatewayID IS NULL)\r\nORDER BY [SentDate] DESC;\r\n")]
  internal class Filter : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    [DBMethodParam("Msgsrch", typeof (string))]
    public string Msgsrch { get; private set; }

    [DBMethodParam("Count", typeof (int))]
    public int Count { get; private set; }

    [DBMethodParam("Response", typeof (int))]
    public int Response { get; private set; }

    public List<Monnit.OutboundPacket> Result { get; private set; }

    public Filter(
      long? gatewayID,
      string msgsrch,
      DateTime fromDate,
      DateTime toDate,
      int count,
      int? response)
    {
      this.GatewayID = gatewayID ?? long.MinValue;
      this.Response = response ?? int.MinValue;
      this.Msgsrch = msgsrch;
      this.Count = count;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.OutboundPacket>(this.ToDataTable());
    }
  }
}
