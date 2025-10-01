// Decompiled with JetBrains decompiler
// Type: Monnit.AzureTempFile
// Assembly: Business, Version=2.5.0.6, Culture=neutral, PublicKeyToken=null
// MVID: ADEA1334-3F26-4050-9B0F-973EB7B9AD1C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\Business.dll

using Azure;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

#nullable disable
namespace Monnit;

public class AzureTempFile
{
  private static string Container = "temp";
  private BlobClient _BlockBlob;

  public string FileName => this.DiskName.Substring(this.DiskName.LastIndexOf('/') + 1);

  public string Folder => this._BlockBlob.GetParentBlobContainerClient().Name;

  public string DiskName => this._BlockBlob.Name;

  public long Size => this._BlockBlob.GetProperties().Value.ContentLength;

  public string HttpsLink => this.GetLink(true);

  public string IconLink => AzureTempFile.FindIconLink(this.HttpsLink);

  public AzureTempFile(BlobClient blockBlob) => this._BlockBlob = blockBlob;

  public AzureTempFile(string diskPath)
  {
    this._BlockBlob = AzureTempFile.GetCloudFileContainer().GetBlobClient(diskPath);
  }

  private string GetLink(bool https)
  {
    return $"http{(https ? (object) "s" : (object) "")}://{ConfigData.AppSettings("AzureStorageURL", "")}/{AzureTempFile.Container}/{this.DiskName}";
  }

  public void DownloadToStream(Stream outputStream) => this._BlockBlob.DownloadTo(outputStream);

  private static BlobContainerClient GetCloudFileContainer()
  {
    BlobContainerClient blobContainerClient = new BlobServiceClient(ConfigData.Find("StorageConnectionString").Value).GetBlobContainerClient(AzureTempFile.Container);
    if (!(bool) blobContainerClient.Exists())
      blobContainerClient.CreateIfNotExists(PublicAccessType.Blob);
    return blobContainerClient;
  }

  public static string FindIconLink(string url)
  {
    int startIndex = url.LastIndexOf('.');
    if (startIndex < 0)
      return "/Content/images/docTypes/unknown.png";
    switch (url.Substring(startIndex).ToLower())
    {
      case ".bmp":
      case ".gif":
      case ".jpeg":
      case ".jpg":
      case ".png":
      case ".tiff":
        return url;
      case ".csv":
      case ".xls":
      case ".xlsx":
        return "/Content/images/docTypes/xls.png";
      case ".doc":
      case ".docx":
        return "/Content/images/docTypes/doc.png";
      case ".pdf":
        return "/Content/images/docTypes/pdf.png";
      default:
        return "/Content/images/docTypes/unknown.png";
    }
  }

  public static AzureTempFile UploadFile(byte[] fileStream, string fileName, string folder)
  {
    if (string.IsNullOrEmpty(fileName) || fileStream == null)
      return (AzureTempFile) null;
    try
    {
      fileName = fileName.Replace("/", "_").Replace("\\", "_");
      string blobName = $"{folder}/{fileName}";
      if (string.IsNullOrEmpty(folder))
        blobName = fileName;
      BlobClient blobClient1 = AzureTempFile.GetCloudFileContainer().GetBlobClient(blobName);
      string lower = Path.GetExtension(fileName).ToLower();
      string str = "";
      switch (lower)
      {
        case ".jpg":
          str = "image/jpeg";
          break;
        case ".png":
          str = "image/png";
          break;
        case ".pdf":
          str = "application/pdf";
          break;
      }
      BlobClient blobClient2 = blobClient1;
      MemoryStream content = new MemoryStream(fileStream);
      BlobHttpHeaders httpHeaders = new BlobHttpHeaders();
      httpHeaders.ContentType = str;
      AccessTier? accessTier = new AccessTier?();
      StorageTransferOptions transferOptions = new StorageTransferOptions();
      CancellationToken cancellationToken = new CancellationToken();
      blobClient2.Upload((Stream) content, httpHeaders, accessTier: accessTier, transferOptions: transferOptions, cancellationToken: cancellationToken);
      try
      {
        blobClient1.SetMetadata((IDictionary<string, string>) new Dictionary<string, string>()
        {
          {
            "FileName",
            WebUtility.HtmlEncode(fileName)
          }
        });
      }
      catch (RequestFailedException ex)
      {
        ex.Log($"AzureTempFile.UploadFile.SetMetadata[RequestFailedException]| fileName = {fileName}, folder = {folder}");
      }
      catch (Exception ex)
      {
        ex.Log($"AzureTempFile.UploadFile.SetMetadata| fileName = {fileName}, folder = {folder}");
      }
      return new AzureTempFile(blobClient1);
    }
    catch (Exception ex)
    {
      ex.Log($"AzureTempFile.UploadFile| fileName = {fileName}, folder = {folder}");
      return (AzureTempFile) null;
    }
  }

  public static bool DeleteFile(string folder, string fileName)
  {
    try
    {
      BlobContainerClient cloudFileContainer = AzureTempFile.GetCloudFileContainer();
      fileName = fileName.Replace("/", "_").Replace("\\", "_");
      cloudFileContainer.GetBlobClient(folder + fileName).DeleteIfExists();
      return true;
    }
    catch (Exception ex)
    {
      ex.Log($"AzureTempFile.DeleteFile() | path = {folder}{fileName}");
      return false;
    }
  }

  public static List<AzureTempFile> AllFiles()
  {
    BlobContainerClient container = AzureTempFile.GetCloudFileContainer();
    return container.GetBlobsByHierarchy().Where<BlobHierarchyItem>((Func<BlobHierarchyItem, bool>) (b => b.IsBlob)).Select<BlobHierarchyItem, AzureTempFile>((Func<BlobHierarchyItem, AzureTempFile>) (b => new AzureTempFile(container.GetBlobClient(b.Blob.Name)))).ToList<AzureTempFile>();
  }

  public static List<AzureTempFile> Files(string folder = null, string searchFileName = null)
  {
    BlobContainerClient container = AzureTempFile.GetCloudFileContainer();
    if (!string.IsNullOrWhiteSpace(folder) && !folder.EndsWith("/"))
      folder += "/";
    return container.GetBlobsByHierarchy(delimiter: "/", prefix: folder).Where<BlobHierarchyItem>((Func<BlobHierarchyItem, bool>) (b =>
    {
      if (!b.IsBlob)
        return false;
      return !string.IsNullOrWhiteSpace(searchFileName) ? b.Blob.Name.ToLower().Contains(searchFileName.ToLower()) : b.IsBlob;
    })).Select<BlobHierarchyItem, AzureTempFile>((Func<BlobHierarchyItem, AzureTempFile>) (b => new AzureTempFile(container.GetBlobClient(b.Blob.Name)))).ToList<AzureTempFile>();
  }

  public static List<string> Folders(string folder = null)
  {
    BlobContainerClient cloudFileContainer = AzureTempFile.GetCloudFileContainer();
    if (!string.IsNullOrWhiteSpace(folder) && !folder.EndsWith("/"))
      folder += "/";
    return cloudFileContainer.GetBlobsByHierarchy(delimiter: "/", prefix: folder).Where<BlobHierarchyItem>((Func<BlobHierarchyItem, bool>) (b => !b.IsBlob)).Select<BlobHierarchyItem, string>((Func<BlobHierarchyItem, string>) (b => b.Prefix)).ToList<string>();
  }
}
