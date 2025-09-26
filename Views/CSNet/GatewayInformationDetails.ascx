<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<%: Html.ValidationSummary(false)%>

    <%if(Model != null){ %>
        <table>
            <tr>
                 <td style="width: 125px; font-weight:bold;">Gateway ID</td>
                 <td style="width: 250px;"><%: Model.GatewayID %></td>
            </tr>
            <tr>
                 <td style="font-weight:bold;">Gateway Name</td>
                 <td><%: Model.Name %> </td>
            </tr>
            <tr>
                 <td style="font-weight:bold;">Gateway Type</td>
                 <td><%: Model.GatewayType.Name %></td>
            </tr>
            <tr>
                <td style="font-weight:bold;">Radio Band</td>
                <td><%: Model.RadioBand %></td>
            </tr>
            <tr>
                 <td style="font-weight:bold;">APNFirmware Version</td>
                 <td><%: Model.APNFirmwareVersion %></td>
            </tr>
            <tr>
                 <td style="font-weight:bold;">Gateway Firmware Version</td>
                 <td><%: Model.GatewayFirmwareVersion %></td>
            </tr>
            <% if (Model.PowerSource != null) {%>
            <tr>
                 <td style="font-weight:bold;">Power Source</td>
                 <td><%: Model.PowerSource.Name %> </td>
            </tr>
            <% }
               if (!string.IsNullOrWhiteSpace(Model.MacAddress)) {%>
            <tr>
                <td style="font-weight:bold;">MacAddress</td>
                <td><%: Model.MacAddress %></td>
            </tr>
            <%} %>
        </table>
<%}else{%>
    <img src="/Content/images/gateway-label.png" style="margin-left: -40px; " alt="Gateway ID and Security Code Location" title="Gateway ID and Security Code Location" />
<%}%>
