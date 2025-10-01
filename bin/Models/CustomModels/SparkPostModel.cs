// Decompiled with JetBrains decompiler
// Type: iMonnit.Models.CustomModels.SparkPostModel
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

#nullable disable
namespace iMonnit.Models.CustomModels;

public class SparkPostModel
{
  public string type { get; set; }

  public string sending_ip { get; set; }

  public string subject { get; set; }

  public string raw_rcpt_to { get; set; }

  public SparkPostMetaModel rcpt_meta { get; set; }
}
