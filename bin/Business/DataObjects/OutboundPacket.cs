// Decompiled with JetBrains decompiler
// Type: Monnit.OutboundPacket
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Monnit;

[DBClass("OutboundPacket")]
public class OutboundPacket : BaseDBObject
{
  private long _OutboundPacketID = long.MinValue;
  private bool _Encrypted = false;
  private long _APNID = long.MinValue;
  private int _Version = int.MinValue;
  private int _Power = int.MinValue;
  private int _Response = int.MinValue;
  private int _MessageCount = int.MinValue;
  private byte[] _Message = (byte[]) null;
  private DateTime _SentDate = DateTime.MinValue;
  private bool _More = false;
  private int _Sequence = 0;
  private TimeSpan _ProcessingTime = TimeSpan.Zero;
  private Guid _OutboundPacketGUID = Guid.Empty;

  [DBProp("OutboundPacketID", IsPrimaryKey = true)]
  public long OutboundPacketID
  {
    get => this._OutboundPacketID;
    set => this._OutboundPacketID = value;
  }

  [DBProp("Encrypted")]
  public bool Encrypted
  {
    get => this._Encrypted;
    set => this._Encrypted = value;
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

  [DBProp("SentDate", AllowNull = false)]
  public DateTime SentDate
  {
    get => this._SentDate;
    set => this._SentDate = value;
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

  [DBProp("ProcessingTime")]
  public TimeSpan ProcessingTime
  {
    get => this._ProcessingTime;
    set => this._ProcessingTime = value;
  }

  public long ProcessingTimeInTicks
  {
    set => this.ProcessingTime = new TimeSpan(value);
  }

  [DBProp("Day", AllowNull = false)]
  public int Day
  {
    get => this._SentDate.DayOfWeek.ToInt();
    set
    {
    }
  }

  public int Year
  {
    get => this._SentDate.Year;
    set
    {
    }
  }

  [DBProp("OutboundPacketGUID", AllowNull = false)]
  public Guid OutboundPacketGUID
  {
    get => this._OutboundPacketGUID;
    set => this._OutboundPacketGUID = value;
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

  public override void Save()
  {
    if (CassandraHelper.isCassandraWriter && CassandraHelper.enableOutboundPackets)
    {
      CassandraHelper.UpsertOutboundPacket(this);
    }
    else
    {
      Guid outboundPacketGuid = this.OutboundPacketGUID;
      if (this.OutboundPacketGUID == Guid.Empty)
      {
        this.OutboundPacketGUID = Guid.NewGuid();
        Monnit.Data.OutboundPacket.Insert insert = new Monnit.Data.OutboundPacket.Insert(this.OutboundPacketGUID, this.Encrypted, this.APNID, this.Version, this.Power, this.Response, this.MessageCount, this.Message, this.SentDate, this.More, this.Sequence, this.ProcessingTime, this.Day);
      }
      else
        base.Save();
    }
  }

  public static List<OutboundPacket> LoadByFilter(
    long? gatewayID,
    string msgsrch,
    DateTime fromDate,
    DateTime toDate,
    int limit,
    int? response)
  {
    List<OutboundPacket> result = new Monnit.Data.OutboundPacket.Filter(gatewayID, msgsrch, fromDate, toDate, limit, response).Result;
    if (CassandraHelper.isCassandraReader && CassandraHelper.enableOutboundPackets)
    {
      List<OutboundPacket> collection = new List<OutboundPacket>();
      List<OutboundPacket> outboundPacketList = new List<OutboundPacket>();
      List<DateTime> list1 = fromDate.EnumerateDays(toDate).ToList<DateTime>();
      list1.Reverse();
      List<int> list2 = list1.Select<DateTime, int>((Func<DateTime, int>) (d => (int) d.DayOfWeek)).ToList<int>();
      List<string> source = new List<string>()
      {
        "day =",
        "sentdate >=",
        "sentdate <="
      };
      List<object> objectList = new List<object>()
      {
        (object) 0,
        (object) fromDate,
        (object) toDate
      };
      int num1;
      if (gatewayID.HasValue)
      {
        long? nullable = gatewayID;
        long num2 = 0;
        num1 = nullable.GetValueOrDefault() > num2 & nullable.HasValue ? 1 : 0;
      }
      else
        num1 = 0;
      if (num1 != 0)
      {
        source.Add("apnid =");
        objectList.Add((object) gatewayID);
      }
      int? nullable1 = response;
      int num3 = 0;
      if (nullable1.GetValueOrDefault() >= num3 & nullable1.HasValue)
      {
        source.Add("response =");
        objectList.Add((object) response);
      }
      string cql = $"select * from outboundpacket where {string.Join(" and ", source.Select<string, string>((Func<string, string>) (f => f + " ?")))} allow filtering";
      try
      {
        foreach (int num4 in list2)
        {
          if (limit > 0)
          {
            objectList[0] = (object) num4;
            List<OutboundPacket> list3 = CassandraHelper.mapper.FetchAsync<OutboundPacket>(cql, objectList.ToArray()).Result.OrderByDescending<OutboundPacket, DateTime>((Func<OutboundPacket, DateTime>) (x => x.SentDate)).ToList<OutboundPacket>();
            if (msgsrch != null && msgsrch != string.Empty)
              list3 = list3.Where<OutboundPacket>((Func<OutboundPacket, bool>) (x => x.Message.ToString().Contains(msgsrch))).ToList<OutboundPacket>();
            List<OutboundPacket> list4 = list3.Take<OutboundPacket>(limit).ToList<OutboundPacket>();
            limit -= list4.Count;
            collection.AddRange((IEnumerable<OutboundPacket>) list4);
          }
        }
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] InboundPacket.LoadByFilter[GatewayID: {gatewayID.ToString()}][fromDate: {fromDate.ToShortDateString()}] [toDate: {toDate.ToShortDateString()}]  ");
      }
      collection.Sort((Comparison<OutboundPacket>) ((x, y) => y.SentDate.CompareTo(x.SentDate)));
      result.AddRange((IEnumerable<OutboundPacket>) collection);
    }
    return result;
  }
}
