// Decompiled with JetBrains decompiler
// Type: SKUFirmware
// Assembly: iMonnit, Version=4.0.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 8D8B7007-62F0-412B-AC82-92244CE5EA6C
// Assembly location: C:\inetpub\wwwroot\Enterprise\bin\iMonnit.dll

using iMonnit;
using iMonnit.Models;
using Monnit;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

#nullable disable
public class SKUFirmware
{
  public long FirmwareID { get; set; }

  public string Version { get; set; }

  public string SKU { get; set; }

  public string FlashOnlyFirmware { get; set; }

  public static SKUFirmware LatestFirmware(string SKU)
  {
    string key = "FirmwareBySKU_" + SKU;
    Dictionary<string, SKUFirmware> dictionary = TimedCache.RetrieveObject<Dictionary<string, SKUFirmware>>(key);
    if (dictionary == null)
    {
      dictionary = new Dictionary<string, SKUFirmware>();
      TimedCache.AddObjectToCach(key, (object) dictionary, new TimeSpan(1, 0, 0));
    }
    if (!dictionary.ContainsKey(SKU))
    {
      SKUFirmware skuFirmware1 = new SKUFirmware();
      SKUFirmware skuFirmware2 = !MonnitSession.IsEnterprise ? SKUFirmware.LatestFirmwareFromMEA(SKU) : SKUFirmware.LatestEncryptedFirmware(SKU);
      try
      {
        if (skuFirmware2 == null)
          return (SKUFirmware) null;
        dictionary.Add(SKU, skuFirmware2);
      }
      catch (Exception ex)
      {
        ex.Log("SKUFirmware.LatestFirmware (Existing Key in Dictionary) ");
      }
    }
    return dictionary[SKU];
  }

  private static SKUFirmware LatestEncryptedFirmware(string SKU)
  {
    try
    {
      IEnumerable<XElement> xelements = XDocument.Load($"{ConfigData.AppSettings("LookUpHost")}xml/GetFirmwareList?sku={SKU}").Root.Elements((XName) "Result").Elements<XElement>((XName) "APIFirmwareList").Elements<XElement>((XName) "APIFirmware");
      List<FirmwareDataModel> firmwareDataModelList = new List<FirmwareDataModel>();
      string str = "";
      foreach (XElement xelement in xelements)
      {
        FirmwareDataModel firmwareDataModel = new FirmwareDataModel(xelement.Attribute((XName) "FirmwareID").Value.ToLong(), xelement.Attribute((XName) "Name").Value, xelement.Attribute((XName) "Version").Value, xelement.Attribute((XName) "SizeInBytes").Value.ToInt(), xelement.Attribute((XName) "IsDeleted").Value.ToBool(), xelement.Attribute((XName) "Status").Value, xelement.Attribute((XName) "DeviceTypeID").Value.ToLong(), xelement.Attribute((XName) "FirmwareBaseID").Value.ToLong(), xelement.Attribute((XName) "RadioBandID").Value.ToLong());
        switch (firmwareDataModel.Status.ToLower())
        {
          case "in production":
            firmwareDataModelList.Add(firmwareDataModel);
            break;
          case "productionflashonly":
            str = $"{str}{firmwareDataModel.Version}|";
            break;
        }
      }
      if (firmwareDataModelList.Count == 0)
        return (SKUFirmware) null;
      return new SKUFirmware()
      {
        SKU = SKU.ToUpper(),
        Version = firmwareDataModelList[0].Version,
        FirmwareID = firmwareDataModelList[0].FirmwareID,
        FlashOnlyFirmware = str
      };
    }
    catch (Exception ex)
    {
      ex.Log("Load Firmware Failed ");
    }
    return (SKUFirmware) null;
  }

  private static SKUFirmware LatestFirmwareFromMEA(string SKU)
  {
    DateTime utcNow = DateTime.UtcNow;
    string uri = "";
    string str1 = ConfigData.AppSettings("MeaApiLogging");
    bool flag = !string.IsNullOrWhiteSpace(str1) && bool.Parse(str1);
    try
    {
      uri = $"{ConfigData.AppSettings("MEA_API_Location")}xml/GetFirmwareList?sku={SKU}";
      XDocument xdocument = XDocument.Load(uri);
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-LatestFirmwareFromMEA",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = xdocument.ToString(),
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = false
        }.Save();
      }
      IEnumerable<XElement> xelements = xdocument.Root.Elements((XName) "Result").Elements<XElement>((XName) "APIFirmwareList").Elements<XElement>((XName) "APIFirmware");
      List<FirmwareDataModel> firmwareDataModelList = new List<FirmwareDataModel>();
      string str2 = "";
      foreach (XElement xelement in xelements)
      {
        FirmwareDataModel firmwareDataModel = new FirmwareDataModel(xelement.Attribute((XName) "FirmwareID").Value.ToLong(), xelement.Attribute((XName) "Name").Value, xelement.Attribute((XName) "Version").Value, xelement.Attribute((XName) "SizeInBytes").Value.ToInt(), xelement.Attribute((XName) "IsDeleted").Value.ToBool(), xelement.Attribute((XName) "Status").Value, xelement.Attribute((XName) "DeviceTypeID").Value.ToLong(), xelement.Attribute((XName) "FirmwareBaseID").Value.ToLong(), xelement.Attribute((XName) "RadioBandID").Value.ToLong());
        switch (firmwareDataModel.Status.ToLower())
        {
          case "in production":
            firmwareDataModelList.Add(firmwareDataModel);
            break;
          case "productionflashonly":
            str2 = $"{str2}{firmwareDataModel.Version}|";
            break;
        }
      }
      if (firmwareDataModelList.Count == 0)
        return (SKUFirmware) null;
      return new SKUFirmware()
      {
        SKU = SKU.ToUpper(),
        Version = firmwareDataModelList[0].Version,
        FirmwareID = firmwareDataModelList[0].FirmwareID,
        FlashOnlyFirmware = str2
      };
    }
    catch (Exception ex)
    {
      ex.Log("Load Firmware Failed ");
      if (flag)
      {
        TimeSpan timeSpan = DateTime.UtcNow - utcNow;
        new MeaApiLog()
        {
          MethodName = "MEA-LatestFirmwareFromMEA",
          MachineName = Environment.MachineName,
          RequestBody = uri,
          ResponseBody = ex.Message,
          CreateDate = utcNow,
          SecondsElapsed = timeSpan.TotalSeconds.ToInt(),
          IsException = true
        }.Save();
      }
    }
    return (SKUFirmware) null;
  }
}
