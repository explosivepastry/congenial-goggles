<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<table>
    <tr>
        <td style="width: 125px; font-weight: bold;"><%:Html.TranslateTag ("Sensor Profile")%></td>
        <td style="width: 250px"><%: Model.MonnitApplication.ApplicationName %></td>
    </tr>
    
    <tr>
        <td style="font-weight: bold;"><%:Html.TranslateTag ("Sensor Type")%>
        </td>
        <td>
            <%: Model.SensorType.Name %>
        </td>
    </tr>

    <tr>
        <td style="font-weight: bold;"><%:Html.TranslateTag ("Power Source")%>
        </td>
        <td>
            <%: Model.PowerSource.Name%>
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold;"><%:Html.TranslateTag ("Firmware Version")%>
        </td>
        <td>
            <%: Model.FirmwareVersion %>
        </td>
    </tr>
    <tr>
        <td style="font-weight: bold;"><%:Html.TranslateTag ("Radio Band")%>
        </td>
        <td>
            <%: Model.RadioBand %>
        </td>
    </tr>
    
</table>
<%Html.RenderPartial("DeviceInfoSensorCustom",Model); %>