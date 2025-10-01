// Decompiled with JetBrains decompiler
// Type: Monnit.Data.InboundPacket
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit.Data;

internal class InboundPacket
{
  [DBMethod("InboundPacket_Insert")]
  [DBMethodBody(DBMS.SqlServer, "\r\nINSERT INTO dbo.[InboundPacket] ([CSNetID], [APNID], [Version], [Power], [Response], [MessageCount], [Message], [ReceivedDate], [More], [Sequence], [EndPoints], [Day], [InboundPacketGUID], [InboundPacketID])\r\nVALUES(@CSNetID, @APNID, @Version, @Power, @Response, @MessageCount, @Message, @ReceivedDate, @More, @Sequence, @EndPoints, @Day, @InboundPacketGUID, null);\r\n")]
  internal class Insert : BaseDBMethod
  {
    [DBMethodParam("InboundPacketGUID", typeof (Guid))]
    public Guid InboundPacketGUID { get; private set; }

    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

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

    [DBMethodParam("ReceivedDate", typeof (DateTime))]
    public DateTime ReceivedDate { get; private set; }

    [DBMethodParam("More", typeof (bool))]
    public bool More { get; private set; }

    [DBMethodParam("Sequence", typeof (int))]
    public int Sequence { get; private set; }

    [DBMethodParam("EndPoints", typeof (string))]
    public string EndPoints { get; private set; }

    [DBMethodParam("Day", typeof (int))]
    public int Day { get; private set; }

    public Insert(
      Guid inboundPacketGUID,
      long cSNetID,
      long aPNID,
      int version,
      int power,
      int response,
      int messageCount,
      byte[] message,
      DateTime receivedDate,
      bool more,
      int sequence,
      string endPoints,
      int day)
    {
      this.InboundPacketGUID = inboundPacketGUID;
      this.CSNetID = cSNetID;
      this.APNID = aPNID;
      this.Version = version;
      this.Power = power;
      this.Response = response;
      this.MessageCount = messageCount;
      this.Message = message;
      this.ReceivedDate = receivedDate;
      this.More = more;
      this.Sequence = sequence;
      this.EndPoints = endPoints;
      this.Day = day;
      this.Execute();
    }
  }

  [DBMethod("InboundPacket_LoadByNetworkAndDateRange")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 100\r\n  ip.*\r\nFROM dbo.[InboundPacket] ip WITH (NOLOCK)\r\nINNER JOIN dbo.[Gateway] g ON g.GatewayID = ip.APNID\r\nWHERE g.CSNetID = @CSNetID\r\n  AND ip.ReceivedDate BETWEEN @FromDate AND @ToDate\r\nORDER BY\r\n  ip.InboundPacketID DESC;\r\n")]
  internal class LoadByNetworkAndDateRange : BaseDBMethod
  {
    [DBMethodParam("CSNetID", typeof (long))]
    public long CSNetID { get; private set; }

    [DBMethodParam("FromDate", typeof (DateTime))]
    public DateTime FromDate { get; private set; }

    [DBMethodParam("ToDate", typeof (DateTime))]
    public DateTime ToDate { get; private set; }

    public List<Monnit.InboundPacket> Result { get; private set; }

    public LoadByNetworkAndDateRange(long cSNetID, DateTime fromDate, DateTime toDate)
    {
      this.CSNetID = cSNetID;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.InboundPacket>(this.ToDataTable());
    }
  }

  [DBMethod("InboundPacket_LoadByAPNID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT TOP 100\r\n  *\r\nFROM dbo.[InboundPacket] WITH (NOLOCK)\r\nWHERE APNID = @GatewayID\r\nORDER BY\r\n  InboundPacketID DESC;\r\n")]
  internal class LoadByAPNID : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.InboundPacket> Result { get; private set; }

    public LoadByAPNID(long gatewayID)
    {
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.InboundPacket>(this.ToDataTable());
    }
  }

  [DBMethod("InboundPacket_SearchInboundPacket")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[InboundPacket]\r\nWHERE CONVERT(VARCHAR(MAX),[Message],1) LIKE '%'+@msgsrch+'%';\r\n")]
  internal class SearchInboundPacket : BaseDBMethod
  {
    [DBMethodParam("msgsrch", typeof (string))]
    public string msgsrch { get; private set; }

    public List<Monnit.InboundPacket> Result { get; private set; }

    public SearchInboundPacket(string Msgsrch)
    {
      this.msgsrch = Msgsrch;
      this.Result = BaseDBObject.Load<Monnit.InboundPacket>(this.ToDataTable());
    }
  }

  [DBMethod("InboundPacket_SearchInboundPacketWithGatewayID")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSELECT\r\n  *\r\nFROM dbo.[InboundPacket]\r\nWHERE APNID = @GatewayID\r\n  AND CONVERT(VARCHAR(MAX),[Message],1) LIKE '%'+@msgsrch+'%';\r\n")]
  internal class SearchInboundPacketWithGatewayID : BaseDBMethod
  {
    [DBMethodParam("msgsrch", typeof (string))]
    public string msgsrch { get; private set; }

    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    public List<Monnit.InboundPacket> Result { get; private set; }

    public SearchInboundPacketWithGatewayID(string Msgsrch, long gatewayID)
    {
      this.msgsrch = Msgsrch;
      this.GatewayID = gatewayID;
      this.Result = BaseDBObject.Load<Monnit.InboundPacket>(this.ToDataTable());
    }
  }

  [DBMethod("InboundPacket_Filter")]
  [DBMethodBody(DBMS.SqlServer, "\r\nSET NOCOUNT ON;\r\n\r\n\r\n--DECLARE @sensorHex VARCHAR(30)\r\n\r\n--IF @SensorID IS NOT NULL\r\n--BEGIN\r\n\r\n--  SELECT \r\n--    @sensorHex =\r\n--      SUBSTRING(CONVERT(VARCHAR(30), convert(varbinary, @sensorid, 2),2), 15, 2)\r\n--    + SUBSTRING(CONVERT(VARCHAR(30), convert(varbinary, @sensorid, 2),2), 13, 2)\r\n--    + SUBSTRING(CONVERT(VARCHAR(30), convert(varbinary, @sensorid, 2),2), 11, 2)\r\n--    + SUBSTRING(CONVERT(VARCHAR(30), convert(varbinary, @sensorid, 2),2), 09, 2)\r\n\r\n--END\r\n\r\n--SELECT top(@Count)\r\n--  * \r\n--FROM dbo.[InboundPacket] WITH(NOLOCK)    \r\n--WHERE ReceivedDate BETWEEN @FromDate AND @ToDate\r\n--  --AND (CONVERT(VARCHAR(MAX),[Message],1) LIKE '%'+@Msgsrch+'%' OR @Msgsrch IS NULL)\r\n--  --AND (CONVERT(VARCHAR(MAX),[Message],1) LIKE '%C5______'+@sensorhex+'%' OR @sensorhex IS NULL)\r\n--  AND (Response = @Response OR @Response IS NULL)\r\n--  AND (APNID = @GatewayID OR @GatewayID IS null)\r\n--ORDER BY ReceivedDate DESC;\r\n\r\nIF @ToDate > GETUTCDATE()\r\nSET @ToDate = GETUTCDATE()\r\n\r\nDECLARE @SQL VARCHAR(8000)\r\nDECLARE @Day varchar(1)\r\nDECLARE @FromDate2 DATETIME = CONVERT(DATETIME, CONVERT(Date, @ToDate), 120)\r\nDECLARE @RowCount INT = 0\r\n\r\n\r\nSET @Day = datepart(dw, @FromDate2) - 1\r\n\r\n--SELECT @FromDate2, @FromDate, @ToDate, @Day\r\n\r\nIF @FromDate <= DATEADD(DAY, -7, GETUTCDATE())\r\nSET @FromDate = DATEADD(DAY, -7, GETUTCDATE())\r\n\r\n\r\nCREATE TABLE #Packet\r\n(\r\n  InboundPacketID BIGINT,\r\n  InboundPacketGUID UNIQUEIDENTIFIER,\r\n  CSNetID\tBIGINT,\r\n  APNID BIGINT,\r\n  Version INT,\r\n  Power\tINT,\r\n  Response INT,\r\n  MessageCount\tINT,\r\n  Message\tVARBINARY(MAX),\r\n  ReceivedDate DATETIME,\r\n  More\tBIT,\r\n  Sequence INT,\r\n  EndPoints VARCHAR(255),\r\n  Day INT\r\n)\r\n\r\n\r\n--DECLARE @sensorHex VARCHAR(30)\r\n\r\n--IF @SensorID IS NOT NULL\r\n--BEGIN\r\n\r\n--  SELECT \r\n--    @sensorHex =\r\n--      SUBSTRING(CONVERT(VARCHAR(30), convert(varbinary, @sensorid, 2),2), 15, 2)\r\n--    + SUBSTRING(CONVERT(VARCHAR(30), convert(varbinary, @sensorid, 2),2), 13, 2)\r\n--    + SUBSTRING(CONVERT(VARCHAR(30), convert(varbinary, @sensorid, 2),2), 11, 2)\r\n--    + SUBSTRING(CONVERT(VARCHAR(30), convert(varbinary, @sensorid, 2),2), 09, 2)\r\n\r\n--END\r\n\r\nDECLARE @LoopStop BIGINT = CONVERT(BIGINT, FORMAT(DATEPART(YEAR, @FromDate), '####') + FORMAT(DATEPART(MONTH, @FromDate), '0#') + FORMAT(DATEPART(DAY, @FromDate), '0#'));\r\n\r\nWHILE @RowCount < @Count AND CONVERT(BIGINT, FORMAT(DATEPART(YEAR, @FromDate2), '####') + FORMAT(DATEPART(MONTH, @FromDate2), '0#') + FORMAT(DATEPART(DAY, @FromDate2), '0#')) >= @LoopStop\r\nBEGIN\r\n\r\n    SET @SQL = \r\n    'SELECT top(' +CONVERT(VARCHAR(30), @Count -  @RowCount) + ')\r\n      * \r\n    FROM [GatewayPackets].dbo.[InboundPacket0'+@day+'] WITH(NOLOCK)    \r\n    WHERE ReceivedDate BETWEEN ''' + CONVERT(VARCHAR(30), @FromDate, 120) + ''' AND ''' + CONVERT(VARCHAR(30), @ToDate, 120) + '''\r\n      --AND (CONVERT(VARCHAR(MAX),[Message],1) LIKE ''%''+@Msgsrch+''%'' OR @Msgsrch IS NULL)\r\n      --AND (CONVERT(VARCHAR(MAX),[Message],1) LIKE ''%C5______''+@sensorhex+''%'' OR @sensorhex IS NULL)\r\n      AND (Response = ' + ISNULL(CONVERT(VARCHAR(30), @Response), 'Response') + ')\r\n      AND (APNID = ' + ISNULL( CONVERT(VARCHAR(30),@GatewayID), 'APNID') +')\r\n    ORDER BY ReceivedDate DESC;'\r\n\r\n\r\n    PRINT (@SQL)\r\n\r\n    INSERT INTO #Packet\r\n    EXEC (@SQL)\r\n\r\n    SET @RowCount = @@RowCount + @RowCount\r\n    SET @FromDate2 = DATEADD(DAY, -1, @FromDate2)\r\n    SET @Day = DATEPART(dw,  @FromDate2) - 1\r\n\r\nEND\r\n\r\nSELECT DISTINCT\r\n*\r\nFROM #Packet\r\nORDER BY ReceivedDate DESC\r\n\r\nSET NOCOUNT OFF;\r\n")]
  internal class Filter : BaseDBMethod
  {
    [DBMethodParam("GatewayID", typeof (long))]
    public long GatewayID { get; private set; }

    [DBMethodParam("SensorID", typeof (long))]
    public long SensorID { get; private set; }

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

    public List<Monnit.InboundPacket> Result { get; private set; }

    public Filter(long? gatewayID, DateTime fromDate, DateTime toDate, int count, int? response)
    {
      this.GatewayID = gatewayID ?? long.MinValue;
      this.SensorID = long.MinValue;
      this.Response = response ?? int.MinValue;
      this.Msgsrch = (string) null;
      this.Count = count;
      this.FromDate = fromDate;
      this.ToDate = toDate;
      this.Result = BaseDBObject.Load<Monnit.InboundPacket>(this.ToDataTable());
    }
  }
}
