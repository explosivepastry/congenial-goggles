<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Monnit.Sensor>" %>

<%
    
    string attachment = "attachment; filename=WifiMetaData_" + Model.SensorID + ".xml";
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentType = "application/octet-stream";
    HttpContext.Current.Response.AddHeader("Pragma", "public");


    string sensAddress = string.Format("{2}/xml/lookupsensor?SensorID={0}&checkDigit={1}", Model.SensorID, "IM" + Monnit.MonnitUtil.CheckDigit(Model.SensorID), ConfigData.FindValue("LookUpHost"));
    XDocument sensXdoc = XDocument.Load(sensAddress);

    string wifiAddress = string.Format("{2}/xml/LookUpWifiGateway?SensorID={0}&checkDigit={1}", Model.SensorID, "IM" + Monnit.MonnitUtil.CheckDigit(Model.SensorID), ConfigData.FindValue("LookUpHost"));
    XDocument wifiXdoc = XDocument.Load(wifiAddress);

    sensXdoc.Descendants("Result").FirstOrDefault().Element("APILookUpSensor").AddAfterSelf(wifiXdoc.Descendants("Result").FirstOrDefault().Element("APILookUpGateway"));

    HttpContext.Current.Response.Write(sensXdoc.Descendants("Result").FirstOrDefault());


    HttpContext.Current.Response.End();
%>