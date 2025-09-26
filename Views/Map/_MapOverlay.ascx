<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.VisualMap>" %>

viewer.clearOverlays();
<%if(ViewData["SensorList"] != null){ %>
    <%foreach (var item in (ViewData["SensorList"] as List<Monnit.Sensor>))
        {
            if (Model != null && Model.PlacedSensors.ContainsKey(item.SensorID))
            {
                //string sensorImage = string.Format("/Content/images/{1}/app{0}.png", item.ApplicationID, item.SensorType.WitType.ToString());
                string sensorImage = Html.GetThemedSVG("app" + item.ApplicationID).ToString();
                // var image = '/Content/images/Wit/app' + sensorTypeID + '.png';
                //string sensorImage = Html.GetThemedContent(string.Format("/images/{1}/app{0}.png", item.ApplicationID, item.SensorType.WitType.ToString()));
                //if (item.ApplicationID == 53 && item.LastDataMessage != null)//Aurora Strip from NER
                //{
                //    Monnit.TemperatureMulti Readings = TemperatureMulti.Deserialize("1", item.LastDataMessage.Data);
                //    sensorImage = Html.GetThemedContent(string.Format("/images/CustomSensor/app53complete_{0}.png", TemperatureMulti.ColorSection(Readings.MaxTemp.ToCelsius())));
                //}
                VisualMapSensor vms = Model.PlacedSensors[item.SensorID];
                Response.Write(String.Format("addOverlayDevice('{0}', `{1}`, new OpenSeadragon.Point({2}, {3}), new OpenSeadragon.Point({4}, {5}), '{6} mapIcon', 'sensor');\r\n", item.SensorID, sensorImage, vms.X, vms.Y, vms.Width, vms.Height, vms.Rotation));
                //Response.Write(String.Format("addOverlaySensor('{0}', '{1}', new OpenSeadragon.Point({2}, {3}), new OpenSeadragon.Point({4}, {5}), '{6} mapIcon sensor icon icon-app{7}');\r\n", item.SensorID, sensorImage, vms.X, vms.Y, vms.Width, vms.Height, vms.Rotation, item.ApplicationID));
            }
        } %>
<%} %>
<%if(ViewData["GatewayList"] != null){ %>
    <%foreach (var item in (ViewData["GatewayList"] as List<Monnit.Gateway>))
        {
            if (Model != null && Model.PlacedGateways.ContainsKey(item.GatewayID))
            {
                //string gatewayImage = string.Format("/Content/images/Gateway/gateway_" + item.GatewayTypeID + ".png");
                string gatewayImage = Html.GetThemedSVGForGateway(item.GatewayTypeID).ToString();
                VisualMapGateway vmg = Model.PlacedGateways[item.GatewayID];
                Response.Write(String.Format("addOverlayDevice('{0}', `{1}`, new OpenSeadragon.Point({2}, {3}), new OpenSeadragon.Point({4}, {5}), '{6} mapIcon', 'gateway');\r\n", item.GatewayID, gatewayImage, vmg.X, vmg.Y, vmg.Width, vmg.Height, vmg.Rotation));
                //Response.Write(String.Format("addOverlaySensor('{0}', '{1}', new OpenSeadragon.Point({2}, {3}), new OpenSeadragon.Point({4}, {5}), '{6} mapIcon sensor icon icon-app{7}');\r\n", item.SensorID, sensorImage, vms.X, vms.Y, vms.Width, vms.Height, vms.Rotation, item.ApplicationID));
            }
        } %>
<%} %>
