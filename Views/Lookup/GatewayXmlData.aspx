<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Monnit.Gateway>" %>

<%
    
    string attachment = "attachment; filename=GatewayMetaData_" + Model.GatewayID + ".xml";
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentType = "application/octet-stream";
    HttpContext.Current.Response.AddHeader("Pragma", "public");


    string address = string.Format("{2}/xml/lookupGateway?GatewayID={0}&checkDigit={1}", Model.GatewayID, "IM" + Monnit.MonnitUtil.CheckDigit(Model.GatewayID), ConfigData.FindValue("LookUpHost"));
    XDocument xdoc = XDocument.Load(address);

    HttpContext.Current.Response.Write(xdoc.Descendants("Result").FirstOrDefault());


    HttpContext.Current.Response.End();
%>