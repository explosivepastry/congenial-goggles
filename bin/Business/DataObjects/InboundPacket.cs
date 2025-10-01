// Decompiled with JetBrains decompiler
// Type: Monnit.InboundPacket
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("InboundPacket")]
public class InboundPacket : BaseDBObject
{
  private long _InboundPacketID = long.MinValue;
  private long _CSNetID = long.MinValue;
  private long _APNID = long.MinValue;
  private int _Version = int.MinValue;
  private int _Power = int.MinValue;
  private int _Response = int.MinValue;
  private int _MessageCount = int.MinValue;
  private byte[] _Message = (byte[]) null;
  private DateTime _ReceivedDate = DateTime.MinValue;
  private bool _More = false;
  private int _Sequence = 0;
  private string _EndPoints = string.Empty;
  private Guid _InboundPacketGUID = Guid.Empty;

  [DBProp("InboundPacketID", IsPrimaryKey = true)]
  public long InboundPacketID
  {
    get => this._InboundPacketID;
    set => this._InboundPacketID = value;
  }

  [DBProp("CSNetID")]
  public long CSNetID
  {
    get => this._CSNetID;
    set => this._CSNetID = value;
  }

  public long Security
  {
    get => this._CSNetID;
    set => this._CSNetID = value;
  }

  [DBProp("APNID", AllowNull = false)]
  public long APNID
  {
    get => this._APNID;
    set => this._APNID = value;
  }

  [DBProp("Version")]
  public int Version
  {
    get => this._Version;
    set => this._Version = value;
  }

  [DBProp("Power")]
  public int Power
  {
    get => this._Power;
    set => this._Power = value;
  }

  [DBProp("Response")]
  public int Response
  {
    get => this._Response;
    set => this._Response = value;
  }

  [DBProp("MessageCount")]
  public int MessageCount
  {
    get => this._MessageCount;
    set => this._MessageCount = value;
  }

  [DBProp("Message", AllowNull = true)]
  public byte[] Message
  {
    get => this._Message;
    set => this._Message = value;
  }

  [DBProp("ReceivedDate", AllowNull = false)]
  public DateTime ReceivedDate
  {
    get => this._ReceivedDate;
    set => this._ReceivedDate = value;
  }

  [DBProp("More")]
  public bool More
  {
    get => this._More;
    set => this._More = value;
  }

  [DBProp("Sequence")]
  public int Sequence
  {
    get => this._Sequence;
    set => this._Sequence = value;
  }

  [DBProp("EndPoints", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string EndPoints
  {
    get => this._EndPoints;
    set => this._EndPoints = value;
  }

  [DBProp("InboundPacketGUID", AllowNull = false)]
  public Guid InboundPacketGUID
  {
    get => this._InboundPacketGUID;
    set => this._InboundPacketGUID = value;
  }

  public byte[] Payload
  {
    get
    {
      byte[] destinationArray = new byte[this.Message.Length - 16 /*0x10*/];
      Array.Copy((Array) this.Message, 16 /*0x10*/, (Array) destinationArray, 0, destinationArray.Length);
      return destinationArray;
    }
  }

  [DBProp("Day", AllowNull = false)]
  public int Day
  {
    get => this._ReceivedDate.DayOfWeek.ToInt();
    set
    {
    }
  }

  public int Year
  {
    get => this._ReceivedDate.Year;
    set
    {
    }
  }

  public override void Save()
  {
    if (CassandraHelper.isCassandraWriter && CassandraHelper.enableInboundPackets)
    {
      CassandraHelper.UpsertInboundPacket(this);
    }
    else
    {
      Guid inboundPacketGuid = this.InboundPacketGUID;
      if (this.InboundPacketGUID == Guid.Empty)
      {
        this.InboundPacketGUID = Guid.NewGuid();
        Monnit.Data.InboundPacket.Insert insert = new Monnit.Data.InboundPacket.Insert(this.InboundPacketGUID, this.CSNetID, this.APNID, this.Version, this.Power, this.Response, this.MessageCount, this.Message, this.ReceivedDate, this.More, this.Sequence, this.EndPoints, this.Day);
      }
      else
        base.Save();
    }
  }

  public PacketCache Cache { get; set; }

  public static List<InboundPacket> LoadByGatewayIDAndDateRange(
    long? gatewayID,
    DateTime fromDate,
    DateTime toDate)
  {
    return InboundPacket.LoadByFilter(gatewayID, new long?(), fromDate, toDate, int.MaxValue, new int?());
  }

  public static List<InboundPacket> LoadByNetworkAndDateRange(
    long cSNetID,
    DateTime fromDate,
    DateTime toDate)
  {
    return InboundPacket.LoadByFilter(new long?(), new long?(cSNetID), fromDate, toDate, int.MaxValue, new int?());
  }

  public static List<InboundPacket> LoadByFilter(
    long? gatewayID,
    long? cSNetID,
    DateTime fromDate,
    DateTime toDate,
    int limit,
    int? response)
  {
    List<InboundPacket> inboundPacketList1 = (!cSNetID.HasValue ? new Monnit.Data.InboundPacket.Filter(gatewayID, fromDate, toDate, limit, response).Result : new Monnit.Data.InboundPacket.LoadByNetworkAndDateRange(cSNetID.Value, fromDate, toDate).Result) ?? new List<InboundPacket>();
    if (CassandraHelper.isCassandraReader && CassandraHelper.enableInboundPackets)
    {
      List<InboundPacket> collection = new List<InboundPacket>();
      List<InboundPacket> inboundPacketList2 = new List<InboundPacket>();
      List<DateTime> list1 = fromDate.EnumerateDays(toDate).ToList<DateTime>();
      list1.Reverse();
      List<int> list2 = list1.Select<DateTime, int>((Func<DateTime, int>) (d => (int) d.DayOfWeek)).ToList<int>();
      List<string> source = new List<string>()
      {
        "day =",
        "receiveddate >=",
        "receiveddate <="
      };
      List<object> objectList = new List<object>()
      {
        (object) 0,
        (object) fromDate,
        (object) toDate
      };
      long? nullable1;
      int num1;
      if (gatewayID.HasValue)
      {
        nullable1 = gatewayID;
        long num2 = 0;
        num1 = nullable1.GetValueOrDefault() > num2 & nullable1.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      if (num1 != 0)
      {
        source.Add("apnid =");
        objectList.Add((object) gatewayID);
      }
      int num3;
      if (cSNetID.HasValue)
      {
        nullable1 = cSNetID;
        long num4 = 0;
        num3 = nullable1.GetValueOrDefault() > num4 & nullable1.HasValue ? 1 : 0;
      }
      else
        num3 = 0;
      if (num3 != 0)
      {
        source.Add("csnetid =");
        objectList.Add((object) cSNetID);
      }
      int? nullable2 = response;
      int num5 = 0;
      if (nullable2.GetValueOrDefault() >= num5 & nullable2.HasValue)
      {
        source.Add("response =");
        objectList.Add((object) response);
      }
      string cql = $"select * from inboundpacket where {string.Join(" and ", source.Select<string, string>((Func<string, string>) (f => f + " ?")))} allow filtering";
      try
      {
        foreach (int num6 in list2)
        {
          if (limit > 0)
          {
            objectList[0] = (object) num6;
            List<InboundPacket> list3 = CassandraHelper.mapper.FetchAsync<InboundPacket>(cql, objectList.ToArray()).Result.OrderByDescending<InboundPacket, DateTime>((Func<InboundPacket, DateTime>) (x => x.ReceivedDate)).ToList<InboundPacket>();
            nullable2 = response;
            int num7 = 0;
            if (nullable2.GetValueOrDefault() >= num7 & nullable2.HasValue)
              list3 = list3.Where<InboundPacket>((Func<InboundPacket, bool>) (x =>
              {
                int response1 = x.Response;
                int? nullable3 = response;
                int valueOrDefault = nullable3.GetValueOrDefault();
                return response1 == valueOrDefault & nullable3.HasValue;
              })).ToList<InboundPacket>();
            List<InboundPacket> list4 = list3.Take<InboundPacket>(limit).ToList<InboundPacket>();
            limit -= list4.Count;
            collection.AddRange((IEnumerable<InboundPacket>) list4);
          }
        }
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] InboundPacket.LoadByFilter[GatewayID: {gatewayID.ToString()}][fromDate: {fromDate.ToShortDateString()}] [toDate: {toDate.ToShortDateString()}]  ");
      }
      collection.Sort((Comparison<InboundPacket>) ((x, y) => y.ReceivedDate.CompareTo(x.ReceivedDate)));
      inboundPacketList1.AddRange((IEnumerable<InboundPacket>) collection);
    }
    return inboundPacketList1;
  }
}
