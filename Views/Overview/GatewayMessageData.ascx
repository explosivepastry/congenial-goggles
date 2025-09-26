<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%
    string DataMsg = string.Empty;
    List<GatewayMessage> dm = ViewBag.dm;
    Gateway gw = (Gateway)ViewBag.Gateway;
    bool IsProtocolVersionOne = gw.GatewayTypeID == 1 || gw.GatewayTypeID == 3 || gw.GatewayTypeID == 8 || gw.GatewayTypeID == 19 || gw.GatewayTypeID == 21;

    double Percent = Math.Round(gw.CurrentSignalStrength.ToDouble().LinearInterpolation(0, 0, 28, 100));

    if (Percent > 100)
    {
        Percent = 100;
    }

    if (dm.Count > 0)
        DataMsg = dm.Last().GatewayMessageGUID.ToString();

    foreach (var item in dm)
    {%>

<!-- History -->
<tr id="dataList">
    <td>
        <%:item.ReceivedDate.OVToLocalDateTimeShort()%>
    </td>
    <td class="gatewayReading" data-guid="<%= DataMsg %>" title="<%:item.MessageType %>">
        <%if (item.MessageType == 0 || (item.MessageType == 1 && IsProtocolVersionOne))
            { %>
        <%: Html.TranslateTag("Data","Data")%>
        <% }
            else if (item.MessageType == 250 || item.MessageType == 253)
            { %>
        <%:Html.TranslateTag("Firmware Download") %>
        <% }
            else if (item.MessageType == 11)
            { %>
        <%:Html.TranslateTag("Encryption Configuration") %>
        <% }
            else if (item.MessageType == 2)
            { %>
        <%:Html.TranslateTag("Sensor List Request") %>
        <% }
            else
            { %>
        <%: Html.TranslateTag("Overview/GatewayMessageData|Gateway Management","Gateway Management")%>
        <% } %>
    </td>
    <td>
        <%if (item.MessageType == 2 && gw.CurrentSignalStrength > 0)
            {%>

        <%: Percent %>

        <%} %>
    </td>
    <td>
        <%if (item.Power == 0)
                Response.Write("Line Powered");
            else if (item.Power == 1)
                Response.Write("PoE Powered");
            else if (item.Power == 2)
                Response.Write("Charge Fault");
            else
            {
                if ((item.Power & 0x8000) == 0x8000)
                {
                    Response.Write("Charging");
                    if (item.Battery < 100)
                        Response.Write(string.Format(" {0}", item.Battery));
                }
                else
                    Response.Write(item.Battery);
            }%>
    </td>
    <td>
        <%if (item.MessageType == 0 || (item.MessageType == 1 && IsProtocolVersionOne))
            { %>
        <%:item.MessageCount %>
        <% } %>
    </td>
</tr>

<%} %>