<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<GatewayNoficationModel>>" %>

<%List<GatewayNoficationModel> gatewaylist = (List<GatewayNoficationModel>)Model; %>

<div style="margin-top: 0px;">
    <div>
        <%foreach (GatewayNoficationModel item in Model)
            { %>

        <% string gatewayTypeIcon = "icon-ethernet-b";
            switch (item.Gateway.GatewayTypeID)
            {
                case 1:   //USB
                case 2:   //USB Service
                case 28:  //USB Pro Service
                    gatewayTypeIcon = "icon-usb-b";
                    break;
                case 3:   //Ethernet_1.x
                case 4:   //Ethernet_Gateway_2.5
                case 5:   //Ethernet_For_Enterprise_2.4
                case 6:   //Ethernet_Beta_2.5.1
                case 7:   //Ethernet Gateway
                case 31:  //EGW2 w MQTT 
                case 33:  //EGW 4
                case 35:  //PoE Sensor
                    gatewayTypeIcon = "icon-ethernet-b";
                    break;
                case 8:   //iMetrik Cell OpenSIM
                case 9:   //OLD_iMetrik_A
                case 13:  //OLD_iMetrik_B
                case 14:  //OLD_iMetrik_2_For_Enterprise
                case 19:  //iMetrik Cell Canada SIM
                case 20:  //OLD_iMetrik_Beta
                case 21:  //Ion Cell Gateway
                    gatewayTypeIcon = "icon-iMetrik";
                    break;
                case 10:  //Wifi_Sensor_Beta
                case 11:  //Wifi_Sensor
                    gatewayTypeIcon = "icon-wi-fi";
                    break;
                case 17:  //CGW2 Verizon
                case 18:  //CGW2 Dev
                case 22:  //CGW3 US Cellular
                case 23:  //CGW3 Sprint A
                case 24:  //CGW3 GPRS Int
                case 25:  //CGW3 3g North America
                case 26:  //CGW3 3g International
                case 27:  //CGW3 3g URB
                case 30:  //ELTE 
                case 32:  //LTE Gateway
                case 36:  //LTE_Sensor
                    gatewayTypeIcon = "icon-cellular";
                    break;
                case 12:  //Serial Interface Board
                case 15:  //Serial Modbus Gateway
                default:
                    break;
            } %>

        <a class="activate-card-holder" style="cursor: pointer;" onclick="toggleGateway(<%:item.Gateway.GatewayID%>);">

            <div class=" active-card-contents" style="height:100%;">
                <div class="hidden-xs  activate-icon">
              
                         <span class="sensor-icon " style="width:28px; height:28px; display:flex;">   <%=Html.GetThemedSVGForGateway(item.Gateway.GatewayTypeID) %></span>

                </div>
                <div class="activate-name">
                    <strong><%:item.Gateway.Name %></strong>
                </div>
                <div class=" ListBorder<%:item.Notify ? "Active":"NotActive" %> notiGateway<%:item.Gateway.GatewayID%> circle__status gridPanel-sensor">

                    <%=Html.GetThemedSVG("circle-check") %>
                </div>

            </div>
        </a>
        <%} %>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $('#filterdGateways').html('<%:gatewaylist.Count%>');
        $('#totalGateways').html('<%:ViewBag.TriggerGateways.Count%>');
    });		
</script>
