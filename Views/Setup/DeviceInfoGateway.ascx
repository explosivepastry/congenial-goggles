<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<table>
    
    <tr>
            <td style="font-weight:bold;"><%:Html.TranslateTag ("Gateway Type")%></td>
            <td><%: Model.GatewayType.Name %></td>
    </tr>
    <% if (Model.PowerSource != null) {%>
    <tr>
            <td style="font-weight:bold;"><%:Html.TranslateTag ("Power Source")%></td>
            <td><%: Model.PowerSource.Name %> </td>
    </tr>
    <% }
        if (!string.IsNullOrWhiteSpace(Model.MacAddress)) {%>
    <tr>
        <td style="font-weight:bold;"><%:Html.TranslateTag ("MacAddress")%></td>
        <td><%: Model.MacAddress %></td>
    </tr>
    <%} %>
    <tr>
            <td style="font-weight:bold;"><%:Html.TranslateTag ("Gateway Firmware Version")%></td>
            <td><%: Model.GatewayFirmwareVersion %></td>
    </tr>
    <tr>
            <td style="font-weight:bold;"><%:Html.TranslateTag ("Radio Version")%></td>
            <td><%: Model.APNFirmwareVersion %></td>
    </tr>
    <tr>
        <td style="font-weight:bold;"><%:Html.TranslateTag ("Radio Band")%></td>
        <td><%: Model.RadioBand %></td>
    </tr>
</table>
<%Html.RenderPartial("DeviceInfoGatewayCustom",Model); %>