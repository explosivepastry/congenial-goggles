// Decompiled with JetBrains decompiler
// Type: iMonnit.API.APIWebHookFailures
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using System;

#nullable disable
namespace iMonnit.API;

public class APIWebHookFailures
{
  public APIWebHookFailures()
  {
  }

  public APIWebHookFailures(
    long attemptID,
    DateTime createDate,
    string url,
    DateTime processDate,
    string status,
    int attemptCount,
    bool retry,
    long accountID)
  {
    this.AttemptID = attemptID;
    this.CreateDate = createDate;
    this.Url = url;
    this.ProcessDate = processDate;
    this.Retry = retry;
    this.AttemptCount = attemptCount;
    this.Status = status;
    this.AccountID = accountID;
  }

  public long AttemptID { get; set; }

  public DateTime CreateDate { get; set; }

  public string Url { get; set; }

  public DateTime ProcessDate { get; set; }

  public bool Retry { get; set; }

  public int AttemptCount { get; set; }

  public string Status { get; set; }

  public long AccountID { get; set; }
}
