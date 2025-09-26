<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<%: Html.ValidationSummary(false)%>

<%if(Model != null){ %>
<table>
    <tr>
        <td style="width: 125px; font-weight: bold;">Sensor Profile</td>
        <td style="width: 250px"><%: Model.MonnitApplication.ApplicationName %></td>
    </tr>
    <tr>
        <td style="font-weight: bold;">Sensor ID
        </td>
        <td>
            <%: Model.SensorID %>
        </td>
    </tr>

    <tr>
        <td style="font-weight: bold;">Sensor Name
        </td>
        <td>
            <%= Model.SensorName %>
        </td>
    </tr>

    <tr>
        <td style="font-weight: bold;">Sensor Type
        </td>
        <td>
            <%: Model.SensorType.Name %>
        </td>
    </tr>

    <tr>
        <td style="font-weight: bold;">Power Source
        </td>
        <td>
            <%: Model.PowerSource.Name%>
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold;">Firmware Version
        </td>
        <td>
            <%: Model.FirmwareVersion %>
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold;">Radio Band
        </td>
        <td>
            <%: Model.RadioBand %>
        </td>
    </tr>
    <%if(Model.SensorTypeID == 4){ 
        Gateway gw = Gateway.LoadBySensorID(Model.SensorID);
        if(gw != null){
        %>
    <tr>
        <td style="font-weight: bold;">GatewayID
        </td>
        <td>
            <%: gw.GatewayID%>
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold;">Mac Address
        </td>
        <td>
            <%: gw.MacAddress%>
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold;">Gateway Firmware Version
        </td>
        <td>
            <%: gw.GatewayFirmwareVersion%>
        </td>
    </tr>
        
    <%}
    }%>
</table>
<%}else{%>
<img src="/Content/images/sensor-label.png" style="margin-left: -40px;" alt="Sensor ID and Security Code Location" title="Sensor ID and Security Code Location" />
<%}%>