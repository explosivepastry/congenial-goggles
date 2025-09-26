<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Monnit.CSNet>" %>

<%

    string attachment = "attachment; filename=NetworkDevicesMetaData_" + Model.CSNetID + ".xml";
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.ClearHeaders();
    HttpContext.Current.Response.ClearContent();
    HttpContext.Current.Response.AddHeader("content-disposition", attachment);
    HttpContext.Current.Response.ContentType = "application/octet-stream";
    HttpContext.Current.Response.AddHeader("Pragma", "public");


    List<iMonnit.API.APILookUpSensor> sensList = new List<iMonnit.API.APILookUpSensor>();
    List<iMonnit.API.APILookUpCable> cableList = new List<iMonnit.API.APILookUpCable>();
    List<iMonnit.API.APILookUpGateway> gateList = new List<iMonnit.API.APILookUpGateway>();

    foreach (Sensor sensor in Model.Sensors)
    {
        sensList.Add(new iMonnit.API.APILookUpSensor(sensor));
    }

    foreach (Cable cable in Model.Cables)
    {
        cableList.Add(new iMonnit.API.APILookUpCable(cable));
    }

    foreach (Gateway gateway in Model.Gateways)
    {
        gateList.Add(new iMonnit.API.APILookUpGateway(gateway));
    }

    XElement sensElement = MonnitController.CreateXML(sensList);
    XElement cableElement = MonnitController.CreateXML(cableList);
    XElement gateElement = MonnitController.CreateXML(gateList);
    XElement result = new XElement("Result", sensElement,cableElement, gateElement);

    HttpContext.Current.Response.Write(result);

    HttpContext.Current.Response.End();
%>