<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Monnit.Sensor>" %>

<%
    
    string attachment = "attachment; filename=SensorMetaData_" + Model.SensorID + ".xml";
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentType = "application/octet-stream";
    HttpContext.Current.Response.AddHeader("Pragma", "public");


    string address = string.Format("{2}/xml/lookupsensor?SensorID={0}&checkDigit={1}", Model.SensorID, "IM" + Monnit.MonnitUtil.CheckDigit(Model.SensorID), ConfigData.FindValue("LookUpHost"));
    XDocument xdoc = XDocument.Load(address);

    HttpContext.Current.Response.Write(xdoc.Descendants("Result").FirstOrDefault());


    HttpContext.Current.Response.End();
%>