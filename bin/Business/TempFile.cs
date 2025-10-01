// Decompiled with JetBrains decompiler
// Type: Monnit.TempFile
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using RedefineImpossible;
using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

#nullable disable
namespace Monnit;

public class TempFile
{
  public string FileName { get; set; }

  public string Extenstion { get; set; }

  public byte[] DocData { get; set; }

  public TempFile(string fileName, string ext, byte[] docData)
  {
    this.FileName = fileName;
    this.Extenstion = ext;
    this.DocData = docData;
  }

  public Attachment ToAttachmentFile()
  {
    MemoryStream contentStream = (MemoryStream) null;
    try
    {
      ContentType contentType1 = new ContentType();
      contentStream = new MemoryStream(this.DocData);
      ContentType contentType2;
      switch (this.Extenstion)
      {
        case ".pdf":
          contentType2 = new ContentType("application/pdf");
          break;
        case ".csv":
          contentType2 = new ContentType("text/plain");
          break;
        case ".xls":
          contentType2 = new ContentType("application/vnd.ms-excel");
          break;
        case ".xlsx":
          contentType2 = new ContentType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
          break;
        default:
          contentType2 = new ContentType(this.Extenstion);
          break;
      }
      return new Attachment((Stream) contentStream, contentType2)
      {
        ContentDisposition = {
          FileName = this.FileName + this.Extenstion
        }
      };
    }
    catch (Exception ex)
    {
      contentStream?.Dispose();
      ExceptionLog.Log(ex);
      throw ex;
    }
  }
}
