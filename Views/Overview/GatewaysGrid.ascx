<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Gateway>>" %>

<div class="container">
            <div class="row display-flex">
                <% bool ShowSignal = false; %>
                <% bool showEnterprise = false; %>

                <% 
                    bool alt = true;
                    foreach (var item in Model)
                    {
                        if (item.GatewayTypeID == 10 || item.GatewayTypeID == 11 || item.GatewayTypeID == 35 || item.GatewayTypeID == 36)//don't show    wifi gateways here
                            continue;

                        string imagePath = Html.GetThemedContent("/images/good.png");
                        string cssClass = "";
                        if (item.LastCommunicationDate == DateTime.MinValue || item.isEnterpriseHost)
                            imagePath = Html.GetThemedContent("/images/sleeping.png");
                        else if (item.ReportInterval != double.MinValue && item.LastCommunicationDate.AddMinutes(item.ReportInterval * 2 + 1) < DateTime.UtcNow)
                            //Missed more than one heartbeat + one minute to take drift into account

                            //DataMessage message = item.DataMessage;

                            alt = !alt;



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
                %>

                <div class="col-lg-3 col-md-4 col-sm-6 col-xs-12">
                    <!----------------- Link for Gateway Home ----------------->
                    <a href="/Overview/GatewayHome/<%=item.GatewayID %>">
                    <div class="gridPanel chipHover">
                        <table width="100%">
                            <tr class=" viewGatewayDetails <%:item.GatewayID %> <%:cssClass %>">
                                <td width="50">
                                    <div class="divCellCenter holder holder<%:item.Status.ToString() %>">
                                        <!----------------- Link for Gateway Home ----------------->

                                            <div class="sensor icon <%:gatewayTypeIcon %> sensorIcon sensorStatus<%:item.Status.ToString() %>">
                                                <%--<div class="status <%:item.Status.ToString() %>"></div>--%>
                                            </div>
                 
                                    </div>
                                </td>
                                <td valign="middle" style="padding: 0px;">
                                    <!----------------- Link for Gateway Home ----------------->
                                    
                                        <div class="glance-text">
                                            <div class="glance-name"><%= item.Name%></div>
                                            <%--<%if (message != null) { %>
                                <div class="glance-reading"><%: message.DisplayData%></div>
                                <%} %>--%>
                                        </div>
                                    
                                </td>





                            </tr>
                        </table>


                        <%if (ShowSignal)
                          { %>
                        <script type="text/javascript">$(function () { $('.gatewaySignal').show(); $('holdGatewayDetails > td').attr("colspan", 7); });</script>
                        <%} %>
                        <% if (showEnterprise)
                           { %>
                        <script type="text/javascript">$(function () { $('.isEnterprise').show(); $('holdGatewayDetails > td').attr("colspan", 7); });</script>
                        <%} %>
                    </div>
                        </a>
                </div>
                <% } %>
            </div>
        </div>

<script>
    $(document).ready(function () {
        $('#filterdGateways').html('<%:Model.Count%>');
       $('#totalGateways').html('<%:ViewBag.TotalGateways%>');

       $('.sortable').click(function (e) {
           e.preventDefault();

           $.get('/Overview/GatewaySortBy', { column: $(this).attr("href"), direction: $(this).attr("data-direction") }, function (data) {
               if (data == "Success")
                   getMain();
               else {
                   console.log(data);
                   showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
               }
           });
       }).css('font-weight', 'bold').css('text-decoration', 'none');


   });

</script>