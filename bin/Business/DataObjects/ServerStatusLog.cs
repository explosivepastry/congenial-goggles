// Decompiled with JetBrains decompiler
// Type: Monnit.ServerStatusLog
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;

#nullable disable
namespace Monnit;

[DBClass("ServerStatusLog")]
public class ServerStatusLog : BaseDBObject
{
  private long _ServerStatusLogID = long.MinValue;
  private DateTime _LogDate = DateTime.MinValue;
  private string _ServerName = string.Empty;
  private int _TCPClients = int.MinValue;
  private int _UDPClients = int.MinValue;
  private double _CPU = double.MinValue;
  private int _ConnectionPool = int.MinValue;
  private int _ProcessThreads = int.MinValue;
  private string _Description = string.Empty;
  private bool _EventTriggered = false;
  private long _DeviceID = long.MinValue;

  [DBProp("ServerStatusLogID", IsPrimaryKey = true)]
  public long ServerStatusLogID
  {
    get => this._ServerStatusLogID;
    set => this._ServerStatusLogID = value;
  }

  [DBProp("LogDate")]
  public DateTime LogDate
  {
    get => this._LogDate;
    set => this._LogDate = value;
  }

  [DBProp("ServerName", MaxLength = 255 /*0xFF*/, AllowNull = true)]
  public string ServerName
  {
    get => this._ServerName;
    set => this._ServerName = value;
  }

  [DBProp("TCPClients")]
  public int TCPClients
  {
    get => this._TCPClients;
    set => this._TCPClients = value;
  }

  [DBProp("UDPClients")]
  public int UDPClients
  {
    get => this._UDPClients;
    set => this._UDPClients = value;
  }

  [DBProp("CPU")]
  public double CPU
  {
    get => this._CPU;
    set => this._CPU = value;
  }

  [DBProp("ConnectionPool")]
  public int ConnectionPool
  {
    get => this._ConnectionPool;
    set => this._ConnectionPool = value;
  }

  [DBProp("ProcessThreads")]
  public int ProcessThreads
  {
    get => this._ProcessThreads;
    set => this._ProcessThreads = value;
  }

  [DBProp("Description", MaxLength = 2000, AllowNull = true)]
  public string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [DBProp("EventTriggered")]
  public bool EventTriggered
  {
    get => this._EventTriggered;
    set => this._EventTriggered = value;
  }

  [DBProp("DeviceID")]
  public long DeviceID
  {
    get => this._DeviceID;
    set => this._DeviceID = value;
  }
}
