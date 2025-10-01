// Decompiled with JetBrains decompiler
// Type: Monnit.CassandraMappings
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using Cassandra.Mapping;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Monnit;

public class CassandraMappings : Mappings
{
  public CassandraMappings()
  {
    this.For<DataMessage>().TableName("DataMessageBySensorYear").PartitionKey((Expression<Func<DataMessage, object>>) (u => (object) u.SensorID));
    this.For<GatewayMessage>().TableName("GatewayMessageByGatewayYearMonth").PartitionKey((Expression<Func<GatewayMessage, object>>) (u => (object) u.GatewayID));
    this.For<InboundPacket>().TableName("InboundPacket").PartitionKey((Expression<Func<InboundPacket, object>>) (u => (object) u.APNID));
    this.For<OutboundPacket>().TableName("OutboundPacket").PartitionKey((Expression<Func<OutboundPacket, object>>) (u => (object) u.APNID)).Column<TimeSpan>((Expression<Func<OutboundPacket, TimeSpan>>) (u => u.ProcessingTime));
  }
}
