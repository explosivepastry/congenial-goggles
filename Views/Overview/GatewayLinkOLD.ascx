<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Gateway>" %>

<div class="col-md-12 col-xs-12">
    <div class="view-btns powertour-hook" id="hook-one">
        <a id="indexlink" href="/Overview/GatewayIndex/" class="btn btn-default btn-grey btn-lg btn-fill"><i class="fa fa-arrow-left"></i></a>

        <a id="tabHistory" href="/Overview/GatewayHome/<%:Model.GatewayID %>" class="btn btn-<%:Request.RawUrl.Contains("GatewayHome")? "primary" : "grey" %> btn-lg btn-fill"><%=Html.GetThemedSVG("list") %><span class="extra">&nbsp;<%: Html.TranslateTag("History","History") %></span></a>

        <a id="tabNotification" href="/Overview/GatewayNotification/<%:Model.GatewayID %>" class="btn btn-<%:Request.RawUrl.Contains("GatewayNotification")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-bullhorn"></i><span class="extra">&nbsp;<%: Html.TranslateTag("Actions","Actions") %></span></a>

        <%GatewayType gatewaytype = GatewayType.Load(Model.GatewayTypeID);

          if (gatewaytype.SupportsDataUsage)
          {
              if (gatewaytype.GatewayTypeID != 30 || (gatewaytype.GatewayTypeID == 30 && (((Model.SystemOptions & 0x04) > 0))))
              { %>
        <a id="tabDataUsage" href="/Overview/GatewayDataUsage/<%:Model.GatewayID %>" class="btn btn-<%:Request.RawUrl.Contains("GatewayDataUsage")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-database"></i><span class="extra">&nbsp;<%: Html.TranslateTag("Overview/GatewayLink|Data Usage","Data Usage") %></span></a>
        <% }
          } %>
        <%if (MonnitSession.CustomerCan("Network_Edit_Gateway_Settings"))
          { %><a id="tabEdit" href="/Overview/GatewayEdit/<%:Model.GatewayID %>" class="btn btn-<%:Request.RawUrl.Contains("GatewayEdit")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-cog"></i> <span class="extra">&nbsp;<%: Html.TranslateTag("Settings","Settings") %></span></a>
        <% } %>
        <a id="tabSensorList" href="/Overview/GatewaySensorList/<%:Model.GatewayID %>" class="btn btn-<%:Request.RawUrl.Contains("GatewaySensorList")? "primary" : "grey" %> btn-lg btn-fill"><i class="fa fa-file-o"></i><span class="extra">&nbsp;<%: Html.TranslateTag("Overview/GatewayLink|Sensor List","Sensor List") %></span></a>

    </div>
</div>

<%
    string gatewayTypeIcon = "icon-ethernet-b";
    switch (Model.GatewayTypeID)
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


<div class="col-md-12 col-xs-12">
    <div class="x_panel">
        <div class="x_title">
            
            <div class="powertour-hook" id="hook-two">
                <h2 class="first_element_to_target" style="background-color: white !important; overflow: unset;">
                    <img src="../../Content/images/icongateway_darkgrey.png" width="20px" height="auto" style="padding-bottom: 7px;">&nbsp;<b title="<%=Model.GatewayTypeID %>" class="hidden-xs"><%: Html.TranslateTag("Gateway","Gateway")%>:&nbsp;</b><%= Model.Name %>

                    <br />
                    <img src="../../Content/images/iconmonstr-sitemap-21-240-darkgrey.png" width="20px" height="auto" style="padding-bottom: 7px;">&nbsp;<b class="hidden-xs"><%: Html.TranslateTag("Network","Network")%>:&nbsp;</b> <a></a><a href="/Network/Edit/<%=Model.CSNetID %>"><%= CSNet.Load(Model.CSNetID).Name %></a>
                </h2>
            </div>

            
            <div class="nav navbar-right panel_toolbox">
                <!-- help button -->
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <div class="divCellCenter holder holder<%:Model.Status.ToString() %>">
                <a href="/Overview/GatewayHome/<%=Model.GatewayID %>">
                    <div id="hook-three" class="powertour-hook sensor icon <%:gatewayTypeIcon %> sensorIcon gatewayStatus<%:Model.Status.ToString() %>">
                    </div>
                </a>
            </div>

            <div style="margin-top: 1em">
                <%if (!Model.isEnterpriseHost)
                  { %>
                <div class="powertour-hook" id="hook-four">
                    <div style="background-color: white;" class="glance-lastDate" style="font-size: 0.8em"><%: Html.TranslateTag("Last Message","Last Message")%> : <%: GatewayMessage.LoadLastByGateway(Model.GatewayID) == null ?   Html.TranslateTag("Unavailable","Unavailable") : (Model.LastCommunicationDate.ToElapsedMinutes() > 120 ? Model.LastCommunicationDate.OVToLocalDateTimeShort() : Model.LastCommunicationDate.ToElapsedMinutes().ToString() + " " +  Html.TranslateTag("Minutes ago","Minutes ago") ) %></div>
                    <div style="background-color: white">
                        <span title="Operating Channel:<%: (Model.LastKnownChannel < 0) ? "Unknown" : ""+ Model.LastKnownChannel%>"><%: Html.TranslateTag("Overview/GatewayLink|Next Check-in","Next Check-in")%>:</span>
                        <%if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5) && Model.LastCommunicationDate > DateTime.MinValue)
                          {
                              double ReportInterval = Model.ReportInterval;

                              if (ReportInterval <= 0)
                                  ReportInterval = Model.GatewayType.DefaultReportInterval;
                              DateTime NextCommunicationDate = Model.LastCommunicationDate.AddMinutes(ReportInterval);
                              while (NextCommunicationDate < DateTime.UtcNow)
                                  NextCommunicationDate = NextCommunicationDate.AddMinutes(ReportInterval);

                              if (NextCommunicationDate != DateTime.MinValue)
                              {%>
                        <span style="font-size: 16px;" class="title2"><%: NextCommunicationDate.OVToLocalDateShort() %> -</span>
                        <span style="font-size: 16px;" class="title2"><%: NextCommunicationDate.OVToLocalTimeShort() %></span>
                        <%} %>

                        <% } %>
                        
                    </div>
                </div>
                
                <%} %>
                <% if (Model.isEnterpriseHost)
                   { %>
                <div>
                    <span><%: Html.TranslateTag("Overview/GatewayLink|Server","Server")%> -</span>
                    <span style="font-size: 16px;" class="title2"><%: Model.ServerHostAddress %>:<%: Model.Port %></span>
                </div>
                <%} %>
            </div>

        </div>
    </div>
</div>
