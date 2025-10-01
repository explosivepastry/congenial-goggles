// Decompiled with JetBrains decompiler
// Type: Monnit.ScheduledTasks
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.Collections.Generic;

#nullable disable
namespace Monnit;

[DBClass("ScheduledTasks")]
public class ScheduledTasks : BaseDBObject
{
  private long _ScheduledTaskID = long.MinValue;
  private string _Name = string.Empty;
  private string _Description = string.Empty;
  private TimeSpan _Frequency = TimeSpan.MinValue;
  private DateTime _LastRun = DateTime.MinValue;
  private bool _IsActive = false;
  private string _AssemblyName = string.Empty;
  private string _ClassName = string.Empty;
  private int _RunOnMinute = int.MinValue;

  [DBProp("ScheduledTaskID", IsPrimaryKey = true)]
  public long ScheduledTaskID
  {
    get => this._ScheduledTaskID;
    set => this._ScheduledTaskID = value;
  }

  [DBProp("Name", MaxLength = 255 /*0xFF*/)]
  public string Name
  {
    get => this._Name;
    set
    {
      if (value == null)
        this._Name = string.Empty;
      else
        this._Name = value;
    }
  }

  [DBProp("Description", MaxLength = 2000)]
  public string Description
  {
    get => this._Description;
    set
    {
      if (value == null)
        this._Description = string.Empty;
      else
        this._Description = value;
    }
  }

  [DBProp("Frequency")]
  public TimeSpan Frequency
  {
    get => this._Frequency;
    set => this._Frequency = value;
  }

  [DBProp("LastRun")]
  public DateTime LastRun
  {
    get => this._LastRun;
    set => this._LastRun = value;
  }

  [DBProp("IsActive")]
  public bool IsActive
  {
    get => this._IsActive;
    set => this._IsActive = value;
  }

  [DBProp("AssemblyName", MaxLength = 255 /*0xFF*/)]
  public string AssemblyName
  {
    get => this._AssemblyName;
    set
    {
      if (value == null)
        this._AssemblyName = string.Empty;
      else
        this._AssemblyName = value;
    }
  }

  [DBProp("ClassName", MaxLength = 255 /*0xFF*/)]
  public string ClassName
  {
    get => this._ClassName;
    set
    {
      if (value == null)
        this._ClassName = string.Empty;
      else
        this._ClassName = value;
    }
  }

  [DBProp("RunOnMinute")]
  public int RunOnMinute
  {
    get => this._RunOnMinute;
    set => this._RunOnMinute = value;
  }

  public bool Ready() => this.IsActive && DateTime.UtcNow.Subtract(this.LastRun) > this.Frequency;

  public void MarkAsRan()
  {
    this.LastRun = DateTime.UtcNow;
    if (this.Frequency >= new TimeSpan(1, 0, 0) && this.RunOnMinute >= 0 && this.RunOnMinute < 59)
    {
      DateTime lastRun = this.LastRun;
      if (lastRun.Minute >= this.RunOnMinute)
      {
        lastRun = this.LastRun;
        this.LastRun = lastRun.AddMinutes((double) (this.RunOnMinute - this.LastRun.Minute));
      }
      else
      {
        lastRun = this.LastRun;
        this.LastRun = lastRun.AddMinutes((double) (this.RunOnMinute - this.LastRun.Minute - 60));
      }
    }
    this.Save();
  }

  public static ScheduledTasks Load(long ID) => BaseDBObject.Load<ScheduledTasks>(ID);

  public static List<ScheduledTasks> LoadAll() => BaseDBObject.LoadAll<ScheduledTasks>();
}
