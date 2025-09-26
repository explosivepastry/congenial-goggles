<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Gateway>>" %>


<%


    foreach (Gateway gate in Model)
    {
        if (gate.GatewayTypeID == 10 || gate.GatewayTypeID == 11 || gate.GatewayTypeID == 35 || gate.GatewayTypeID == 36)//don't show wifi gateways here
            continue;

        GatewayMessage lastGatewayMessage = GatewayMessage.LoadLastByGateway(gate.GatewayID);

        string imagePath = Html.GetThemedContent("/images/good.png");
        if (gate.LastCommunicationDate == DateTime.MinValue)
            imagePath = Html.GetThemedContent("/images/sleeping.png");
        else if (gate.ReportInterval != double.MinValue && gate.LastCommunicationDate.AddMinutes(gate.ReportInterval * 2 + 1) < DateTime.UtcNow)//Missed more than one heartbeat + one minute to take drift into account
            imagePath = Html.GetThemedContent("/images/Alert.png");

        string imagePathPower = Html.GetThemedContent("/Images/Battery/Line.png");
        if (gate.CurrentPower != 0 && gate.CurrentPower != 1)
        {
            if (gate.Battery <= 0)
                imagePathPower = Html.GetThemedContent("/Images/Battery/Battery-0.png");
            else if (gate.Battery <= 10)
                imagePathPower = Html.GetThemedContent("/Images/Battery/Battery-10.png");
            else if (gate.Battery <= 25)
                imagePathPower = Html.GetThemedContent("/Images/Battery/Battery-25.png");
            else if (gate.Battery <= 50)
                imagePathPower = Html.GetThemedContent("/Images/Battery/Battery-50.png");
            else if (gate.Battery <= 75)
                imagePathPower = Html.GetThemedContent("/Images/Battery/Battery-75.png");
            else if (gate.Battery <= 100)
                imagePathPower = Html.GetThemedContent("/Images/Battery/Battery-100.png");
        }
        %>

<a class="turn-on-check-card" style="width:20rem;" onclick="toggleGateway(<%:gate.GatewayID%>,<%=gate.GatewayTypeID %>);">
           <div class="icon gwicon "> <%=Html.GetThemedSVGForGateway(gate.GatewayTypeID) %></div>

<%--<div class="check-card-icon" style="width: 35px; height: 35px; margin: 0 5px;" title="<%=gate.GatewayType.Name %>">
<span class="sensor icon <%:gatewayTypeIcon %>"></span>
</div>--%>

            <div class="" style="margin-left:10px;">
                <span title="<%=gate.SKU %>"><%=gate.Name.Length >= 33 ? gate.Name.Substring(0, 33).Insert(33, "...") : gate.Name%></span>
                <br />
                <span title="<%:Html.TranslateTag("Network/GatewaysToUpdateRefresh|Current APN Firmware", "Current APN Firmware")%>: <%=gate.APNFirmwareVersion %>"><%:Html.TranslateTag("Network/GatewaysToUpdateRefresh|Update APN To", "Update APN To")%>: <%=MonnitSession.LatestVersion(gate.SKU, true).ToStringSafe()  %></span><br />
                <span title="<%:Html.TranslateTag("Network/GatewaysToUpdateRefresh|Current Gateway Firmware", "Current Gateway Firmware")%>: <%=gate.GatewayFirmwareVersion %>"><%:Html.TranslateTag("Network/GatewaysToUpdateRefresh|Update To", "Update To")%>: <%= MonnitSession.LatestVersion(gate.SKU,false).ToStringSafe() %></span>
            </div>

<%--  <div class="dfjcfe dfac gatewaySignal sigIcon" style="height: 50%;">
</div>--%>
         
                <div class="check-network-check ListBorder ListBorderNotActive notiGateway<%:gate.GatewayID%>">
                       <%=Html.GetThemedSVG("circle-check") %>
                </div>
                <input hidden class="updateChk" type="checkbox" name="<%=gate.GatewayID %>" id="update_<%=gate.GatewayID %>_<%=gate.GatewayTypeID %>" />
</a>
<%} %>

<script type="text/javascript">
    $('#filteredGateways').html('<%:ViewBag.FilteredGateways%>');
    $('#totalGateways').html('<%:ViewBag.TotalGateways%>');
</script>

