// Decompiled with JetBrains decompiler
// Type: Monnit.CassandraHelper
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using Cassandra;
using Cassandra.Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Monnit;

public static class CassandraHelper
{
  private static object lockSessionCheck = new object();
  private static object lockMapperCheck = new object();
  private static PreparedStatement psInsertGatewayMessage;
  private static PreparedStatement psBulkInsertDataMessage;
  private static PreparedStatement psUpsertInboundPacket;
  private static PreparedStatement psUpsertOutboundPacket;
  private static string CassandraFilePath = ConfigData.AppSettings(nameof (CassandraFilePath));
  private static string CassandraUser = ConfigData.AppSettings(nameof (CassandraUser));
  private static string CassandraPass = ConfigData.AppSettings(nameof (CassandraPass));
  private static string CassandraKeySpace = ConfigData.AppSettings(nameof (CassandraKeySpace));
  private static string CassandraSecureConnectBundleName = ConfigData.AppSettings(nameof (CassandraSecureConnectBundleName));
  private static bool _CassandraFatalError = false;

  private static string GetCassandraSecureConnectBundlePath()
  {
    string path = string.Empty;
    string empty = string.Empty;
    string connectBundlePath;
    try
    {
      if (!string.IsNullOrEmpty(CassandraHelper.CassandraFilePath) && File.Exists(CassandraHelper.CassandraFilePath))
        connectBundlePath = CassandraHelper.CassandraFilePath;
      else if (!string.IsNullOrEmpty(CassandraHelper.CassandraSecureConnectBundleName))
      {
        List<string> list = ((IEnumerable<string>) Assembly.GetExecutingAssembly().CodeBase.Replace("file:///", "").Replace("/", "\\").Split('\\')).ToList<string>();
        list.RemoveRange(list.Count - 2, 2);
        path = string.Join(Path.DirectorySeparatorChar.ToString(), (IEnumerable<string>) list);
        connectBundlePath = ((IEnumerable<string>) Directory.GetFiles(path, CassandraHelper.CassandraSecureConnectBundleName, SearchOption.AllDirectories)).FirstOrDefault<string>();
      }
      else
      {
        CassandraHelper._CassandraFatalError = true;
        throw new Exception($"[{Environment.MachineName}] CassandraFilePath and CassandraSecureConnectBundleName configs missing. Please set one or the other to resolve. Note that if both are set CassandraFilePath takes precedence. Disabling Cassandra.");
      }
      if (File.Exists(connectBundlePath))
      {
        try
        {
          using (ZipFile.Open(connectBundlePath, ZipArchiveMode.Read))
            ;
        }
        catch (Exception ex)
        {
          ex.Log($"[{Environment.MachineName}] CassandraHelper.GetCassandraSecureConnectBundlePath. Bundle was found at [{connectBundlePath}], but an error occurred while trying to open the archive");
          throw new FileLoadException($"[{Environment.MachineName}] Error loading CassandraSecureConnectBundle at [{connectBundlePath}]");
        }
      }
      else
        throw new FileNotFoundException($"[{Environment.MachineName}] CassandraHelper.GetCassandraSecureConnectBundlePath. Bundle was not found at [{connectBundlePath}]");
    }
    catch (Exception ex)
    {
      if (string.IsNullOrEmpty(CassandraHelper.CassandraFilePath))
      {
        string function = $"[{Environment.MachineName}] CassandraHelper.GetCassandraSecureConnectBundlePath(), CassandraSecureConnectBundle could not be accessed. CassandraSecureConnectBundleName=[{CassandraHelper.CassandraSecureConnectBundleName}]. Path recursively search [{path}]. Note that if both CassandraFilePath and CassandraSecureConnectBundleName configs are set CassandraFilePath takes precedence. Disabling Cassandra.";
        ex.Log(function);
      }
      else
      {
        string function = $"[{Environment.MachineName}] CassandraHelper.GetCassandraSecureConnectBundlePath(), CassandraSecureConnectBundle could not be accessed. CassandraFilePath=[{CassandraHelper.CassandraFilePath}]. Note that if both CassandraFilePath and CassandraSecureConnectBundleName configs are set CassandraFilePath takes precedence. Disabling Cassandra.";
        ex.Log(function);
      }
      CassandraHelper._CassandraFatalError = true;
      return (string) null;
    }
    return connectBundlePath;
  }

  public static void init()
  {
    try
    {
      if (CassandraHelper._session == null)
        CassandraHelper._session = CassandraHelper.session;
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] CassandraHelper() init()");
    }
    try
    {
      if (CassandraHelper._mapper != null)
        return;
      CassandraHelper._mapper = CassandraHelper.mapper;
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] CassandraHelper() _mapper = mapper;");
    }
  }

  public static bool isCassandraWriter
  {
    get
    {
      if (CassandraHelper._CassandraFatalError)
        return !CassandraHelper._CassandraFatalError;
      bool isCassandraWriter = false;
      try
      {
        isCassandraWriter = ConfigData.AppSettings("CassandraWriter", "False").ToBool();
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraHelper.get_isCassandraWriter");
      }
      return isCassandraWriter;
    }
  }

  public static bool isCassandraReader
  {
    get
    {
      if (CassandraHelper._CassandraFatalError)
        return !CassandraHelper._CassandraFatalError;
      bool isCassandraReader = false;
      try
      {
        isCassandraReader = ConfigData.AppSettings("CassandraReader", "False").ToBool();
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraHelper.get_isCassandraReader");
      }
      return isCassandraReader;
    }
  }

  public static bool enableGatewayMessages
  {
    get
    {
      if (CassandraHelper._CassandraFatalError)
        return !CassandraHelper._CassandraFatalError;
      bool enableGatewayMessages = false;
      try
      {
        enableGatewayMessages = ConfigData.AppSettings("CassandraEnableGatewayMessages", "False").ToBool();
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraHelper.get_enableGatewayMessages");
      }
      return enableGatewayMessages;
    }
  }

  public static bool enableDataMessages
  {
    get
    {
      if (CassandraHelper._CassandraFatalError)
        return !CassandraHelper._CassandraFatalError;
      bool enableDataMessages = false;
      try
      {
        enableDataMessages = ConfigData.AppSettings("CassandraEnableDataMessages", "False").ToBool();
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraHelper.get_enableDataMessages");
      }
      return enableDataMessages;
    }
  }

  public static bool enableInboundPackets
  {
    get
    {
      if (CassandraHelper._CassandraFatalError)
        return !CassandraHelper._CassandraFatalError;
      bool enableInboundPackets = false;
      try
      {
        enableInboundPackets = ConfigData.AppSettings("CassandraEnableInboundPackets", "False").ToBool();
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraHelper.get_enableInboundPackets");
      }
      return enableInboundPackets;
    }
  }

  public static bool enableOutboundPackets
  {
    get
    {
      if (CassandraHelper._CassandraFatalError)
        return !CassandraHelper._CassandraFatalError;
      bool enableOutboundPackets = false;
      try
      {
        enableOutboundPackets = ConfigData.AppSettings("CassandraEnableOutboundPackets", "False").ToBool();
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraHelper.get_enableOutboundPackets");
      }
      return enableOutboundPackets;
    }
  }

  public static DateTime cassandraDataMessageCutoffDate
  {
    get
    {
      string s = ConfigData.AppSettings("CassandraDataMessageCutoffDate", "");
      DateTime utcNow = DateTime.UtcNow;
      try
      {
        utcNow = DateTime.Parse(s);
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraDataMessageCutoffDate: Invalid Config");
      }
      return utcNow;
    }
  }

  public static DateTime cassandraGatewayMessageCutoffDate
  {
    get
    {
      string s = ConfigData.AppSettings("CassandraGatewayMessageCutoffDate", "");
      DateTime utcNow = DateTime.UtcNow;
      try
      {
        utcNow = DateTime.Parse(s);
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraGatewayMessageCutoffDate: Invalid Config");
      }
      return utcNow;
    }
  }

  private static ISession _session { get; set; }

  public static ISession session
  {
    get
    {
      if (CassandraHelper._session != null)
        return CassandraHelper._session;
      try
      {
        if (CassandraHelper._CassandraFatalError)
          return (ISession) null;
        string connectBundlePath = CassandraHelper.GetCassandraSecureConnectBundlePath();
        if (connectBundlePath == null)
        {
          CassandraHelper._CassandraFatalError = true;
          return (ISession) null;
        }
        lock (CassandraHelper.lockSessionCheck)
        {
          if (CassandraHelper._session != null)
            return CassandraHelper._session;
          Cluster cluster = Cluster.Builder().WithCloudSecureConnectionBundle(connectBundlePath).WithCredentials(CassandraHelper.CassandraUser, CassandraHelper.CassandraPass).Build();
          cluster.Configuration.SocketOptions.SetConnectTimeoutMillis(12000);
          CassandraHelper._session = cluster.Connect();
          CassandraHelper._session.ChangeKeyspace(CassandraHelper.CassandraKeySpace);
          CassandraHelper.psInsertGatewayMessage = CassandraHelper.session.Prepare("INSERT INTO GatewayMessageByGatewayYearMonth (GatewayMessageGUID, YearMonth, GatewayID, Power, Battery, ReceivedDate, MeetsNotificationRequirement, MessageType, MessageCount, SignalStrength) VALUES(?, ?, ?, ?, ?, ?, ?, ?, ?, ?);");
          CassandraHelper.psBulkInsertDataMessage = CassandraHelper.session.Prepare("INSERT INTO DataMessageBySensorYear (DataMessageGUID, MessageDate, SensorID, year, isaware, Battery, Data, GatewayID, InsertDate,  LinkQuality,  SignalStrength, State, Voltage, hasnote, isauthenticated, meetsnotificationrequirement, ApplicationID) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)");
          CassandraHelper.psUpsertInboundPacket = CassandraHelper.session.Prepare("INSERT INTO InboundPacket (APNID, Day, ReceivedDate, InboundPacketGUID, CSNetID, Version, Power, Response, MessageCount, Message, More, Sequence, EndPoints) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) USING TTL 604800");
          CassandraHelper.psUpsertOutboundPacket = CassandraHelper.session.Prepare("INSERT INTO OutboundPacket (APNID, Day, SentDate, OutboundPacketGUID, Version, Power, Response, MessageCount, Message, More, Sequence, Encrypted, ProcessingTimeInTicks) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?) USING TTL 604800");
          return CassandraHelper._session;
        }
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraHelper.get_session. Disabling Cassandra.");
        CassandraHelper._CassandraFatalError = true;
        return (ISession) null;
      }
    }
  }

  private static IMapper _mapper { get; set; }

  public static IMapper mapper
  {
    get
    {
      if (CassandraHelper._mapper != null)
        return CassandraHelper._mapper;
      try
      {
        if (CassandraHelper._CassandraFatalError)
          return (IMapper) null;
        lock (CassandraHelper.lockMapperCheck)
        {
          if (CassandraHelper._mapper != null)
            return CassandraHelper._mapper;
          CassandraHelper._mapper = (IMapper) new Mapper(CassandraHelper.session);
          if ((CassandraMappings) MappingConfiguration.Global.Get<CassandraMappings>() == null)
            MappingConfiguration.Global.Define<CassandraMappings>();
          return CassandraHelper._mapper;
        }
      }
      catch (Exception ex)
      {
        ex.Log($"[{Environment.MachineName}] CassandraHelper.get_mapper. Disabling Cassandra");
        CassandraHelper._CassandraFatalError = true;
        return (IMapper) null;
      }
    }
  }

  public static void BulkInsert(List<DataMessage> results)
  {
    try
    {
      BatchStatement batchStatement = new BatchStatement();
      foreach (DataMessage result in results)
        batchStatement.Add((Statement) CassandraHelper.psBulkInsertDataMessage.Bind((object) result.DataMessageGUID, (object) result.MessageDate, (object) result.SensorID, (object) result.MessageDate.Year, (object) ((result.State & 2) == 2), (object) result.Battery, (object) result.Data, (object) result.GatewayID, (object) result.InsertDate, (object) result.LinkQuality, (object) result.SignalStrength, (object) result.State, (object) result.Voltage, (object) result.HasNote, (object) result.IsAuthenticated, (object) result.MeetsNotificationRequirement, (object) (int) result.ApplicationID));
      CassandraHelper.session.ExecuteAsync((IStatement) batchStatement);
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] CassandraHelper.BulkInsert");
    }
  }

  public static void UpdateDataMessage(DataMessage dm)
  {
    DateTime now = DateTime.Now;
    try
    {
      int year = dm.MessageDate.Year;
      CassandraHelper.mapper.UpdateAsync<DataMessage>("SET HasNote = ?, MeetsNotificationRequirement = ? WHERE SensorID = ? and Year = ? and MessageDate = ? and DataMessageGuid = ?", (object) dm.HasNote, (object) dm.MeetsNotificationRequirement, (object) dm.SensorID, (object) year, (object) dm.MessageDate, (object) dm.DataMessageGUID);
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] UpdateDataMessage[sensorID: {dm.SensorID.ToString()}][messageDate: {dm.MessageDate.ToShortDateString()}] ");
    }
  }

  public static void InsertGatewayMessage(GatewayMessage gm)
  {
    try
    {
      DateTime receivedDate = gm.ReceivedDate;
      int num1 = receivedDate.Year * 100;
      receivedDate = gm.ReceivedDate;
      int month = receivedDate.Month;
      int num2 = num1 + month;
      CassandraHelper.session.ExecuteAsync((IStatement) CassandraHelper.psInsertGatewayMessage.Bind((object) gm.GatewayMessageGUID, (object) num2, (object) gm.GatewayID, (object) gm.Power, (object) gm.Battery, (object) gm.ReceivedDate, (object) gm.MeetsNotificationRequirement, (object) gm.MessageType, (object) gm.MessageCount, (object) gm.SignalStrength));
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] CassandraHelper.InsertGatewayMessage");
    }
  }

  public static void UpdateGatewayMessage(GatewayMessage gm)
  {
    try
    {
      DateTime receivedDate = gm.ReceivedDate;
      int num1 = receivedDate.Year * 100;
      receivedDate = gm.ReceivedDate;
      int month = receivedDate.Month;
      int num2 = num1 + month;
      CassandraHelper.mapper.UpdateAsync<GatewayMessage>("SET MeetsNotificationRequirement = ? WHERE GatewayID = ? and YearMonth = ? and ReceivedDate = ? and GatewayMessageGUID = ?;", (object) gm.MeetsNotificationRequirement, (object) gm.GatewayID, (object) num2, (object) gm.ReceivedDate, (object) gm.GatewayMessageGUID);
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] CassandraHelper.UpdateGatewayMessage");
    }
  }

  public static void UpsertInboundPacket(InboundPacket ibp)
  {
    Guid inboundPacketGuid = ibp.InboundPacketGUID;
    if (ibp.InboundPacketGUID == Guid.Empty)
      ibp.InboundPacketGUID = Guid.NewGuid();
    try
    {
      CassandraHelper.session.ExecuteAsync((IStatement) CassandraHelper.psUpsertInboundPacket.Bind((object) ibp.APNID, (object) ibp.Day, (object) ibp.ReceivedDate, (object) ibp.InboundPacketGUID, (object) ibp.CSNetID, (object) ibp.Version, (object) ibp.Power, (object) ibp.Response, (object) ibp.MessageCount, (object) ibp.Message, (object) ibp.More, (object) ibp.Sequence, (object) ibp.EndPoints));
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] CassandraHelper.UpdateInboundPacket");
    }
  }

  public static void UpsertOutboundPacket(OutboundPacket obp)
  {
    Guid outboundPacketGuid = obp.OutboundPacketGUID;
    if (obp.OutboundPacketGUID == Guid.Empty)
      obp.OutboundPacketGUID = Guid.NewGuid();
    try
    {
      long ticks = obp.ProcessingTime.Ticks;
      CassandraHelper.session.ExecuteAsync((IStatement) CassandraHelper.psUpsertOutboundPacket.Bind((object) obp.APNID, (object) obp.Day, (object) obp.SentDate, (object) obp.OutboundPacketGUID, (object) obp.Version, (object) obp.Power, (object) obp.Response, (object) obp.MessageCount, (object) obp.Message, (object) obp.More, (object) obp.Sequence, (object) obp.Encrypted, (object) ticks));
    }
    catch (Exception ex)
    {
      ex.Log($"[{Environment.MachineName}] CassandraHelper.UpdateOutboundPacket");
    }
  }

  public static double MillisecondsToTicks(double ms) => ms * 10000.0;

  public static double TicksToMilliseconds(long ticks) => (double) (ticks / 10000L);

  public static double NanosecondsToTicks(double ns) => ns / 100.0;

  public static long TicksToNanoseconds(long ticks) => ticks * 100L;
}
