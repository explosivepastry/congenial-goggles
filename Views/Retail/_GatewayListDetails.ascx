<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.Gateway>>" %>

<%if (Model.Count == 0)
    { %>
<div class="container">
    <div class="col-md-12 col-xs-12">
            <h2 style="text-align: center;"><%:Html.TranslateTag("There are no GPS capable gateways. Click below to add one.") %></h2>
            <a class="add-btn" href="/Setup/AssignDevice/<%=MonnitSession.CurrentCustomer.AccountID %>">
                <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="16" height="15.999" viewBox="0 0 16 15.999">
                    <defs>
                        <clipPath id="clip-path">
                            <rect width="16" height="15.999" fill="none" />
                        </clipPath>
                    </defs>
                    <g id="Symbol_14_32" data-name="Symbol 14 – 32" clip-path="url(#clip-path)">
                        <path id="Union_1" data-name="Union 1" d="M7,16V9H0V7H7V0H9V7h7V9H9v7Z" transform="translate(0)" fill="#fff" />
                    </g>
                </svg>
                &nbsp; &nbsp; <%: Html.TranslateTag("Add Gateway", "Add Gateway")%>
            </a>

            <br>
            <br>
            <br>
            <br>
<%--            <% Html.RenderPartial("_LonelyAstronaut"); %>--%>

    </div>
</div>
<%}
    else
    {
        string GatewayListType = ViewBag.GatewayListType;
        string ClickSvgIcon = ViewBag.ClickSvgIcon;
        if (string.IsNullOrWhiteSpace(ClickSvgIcon))
            ClickSvgIcon = "lock";

        foreach (Gateway item in Model)
        {
            if (item.GatewayTypeID == 10 || item.GatewayTypeID == 11 || item.GatewayTypeID == 35 || item.GatewayTypeID == 36)//don't show wifi gateways here
                continue;

            bool isLocked = string.IsNullOrWhiteSpace(GatewayListType) ? !item.IsUnlocked : !item.IsGPSUnlocked;
            string gatewayTypeIcon = "icon-ethernet-b";
            switch (item.GatewayTypeID)
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
                case 32:  //LTE 
                case 36:  //LTE_Sensor
                    gatewayTypeIcon = "icon-cellular";
                    break;
                case 12:  //Serial Interface Board
                case 15:  //Serial Modbus Gateway
                default:
                    break;
            }

            if (isLocked)
            { %>

<a class="d-flex" style="flex-basis: 320px;" onclick="toggleGateway(<%:item.GatewayID%>);">

    <div class="turn-on-check-card" style="width: 100%">
        <div class="check-card-icon2" style="margin-right:10px;">
         <%=Html.GetThemedSVGForGateway(item.GatewayTypeID) %> 
    
        </div>

        <div class="check-card-name">
            <label><%:item.Name%></label>
        </div>

        <div id="gateway_<%:item.GatewayID%>" class="check-card-check ListBorderNotActive notiSensor<%:item.GatewayID%>">

            <%=Html.GetThemedSVG("lockbox") %>
        </div>

        <div class="hidden-lg hidden-md hidden-sm hidden-xs">
            <input type="checkbox" id="ckb_<%:item.GatewayID%>" data-checkbox="<%:item.GatewayID%>" data-appid="<%:gatewayTypeIcon%>" class="checkbox checkbox-info" />
        </div>
    </div>
</a>


<%}
    else
    { %>

<%--/* These are locked*/--%>

<a class="d-flex" style="flex-basis: 320px;">
    <div class="turn-on-check-card" style="width: 100%">
        <div class="check-card-icon2" style="margin-right:10px;">
    <%=Html.GetThemedSVGForGateway(item.GatewayTypeID) %>
     
        </div>
        <div class="check-card-name">
            <label><%:item.Name%></label>
        </div>

        <div id="gateway_<%:item.GatewayID%>" class=" check-card-unlock notiSensor<%:item.GatewayID%>">
            <%=Html.GetThemedSVG("unlock") %>
        </div>

        <div class="hidden-lg hidden-md hidden-sm hidden-xs"></div>
    </div>
</a>

<%}
    } %>

<style>
    .ListBorderActive svg path {
        fill: #4FB848;
    }

    .triggerDevice__status #svg_print {
        fill: #fff;
        height: 30px;
        width: 30px;
    }
</style>
<%} %>

<script type="text/javascript">

    $(document).ready(function () {
        $('.checkbox').hide();
        $('#filteredGateways').html(<%:Model.Count%>);
        $('#totalGateways').html(<%:ViewBag.TotalGatewayUnlocks%>);

        $('.sortable').click(function (e) {
            e.preventDefault();

            $.get('/Overview/GatewaySortBy', { column: $(this).attr("href"), direction: $(this).attr("data-direction") }, function (data) {
                if (data == "Success")
                    refreshGatewayUnlockList();//getMain();
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }).css('font-weight', 'bold').css('text-decoration', 'none');
    });
    $("li a").each(function () {
        if (this.href == window.location.href) {
            $(this).addClass("active");
        }
    });

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
</script>
