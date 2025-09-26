<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Gateway>" %>

<%Html.RenderPartial("_DisplayMessageBanner", ""); %>
<div class="container-fluid px-0 mb-4">
    <div class="col-12 view-btns_container">
        <div class="view-btns deviceView_btn_row shadow-sm rounded">
            <a id="indexlink" href="/Overview/GatewayIndex/" class="deviceView_btn_row__device">
                <div class="btn-default btn-lg btn-fill">
                    <div class="btn-secondaryToggle btn-lg btn-fill">
                        <%=Html.GetThemedSVG("gateway") %>
                        <span class="extra"><%: Html.TranslateTag("Gateways","Gateways") %></span>
                    </div>
                </div>
            </a>

            <a id="tabHistory" href="/Overview/GatewayHome/<%:Model.GatewayID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("GatewayHome")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("list") %>
                    <span class="extra">
                        <%: Html.TranslateTag("History","History") %>
                    </span>
                </div>
            </a>

            <%if (Model.IsGPSUnlocked && !string.IsNullOrWhiteSpace(MonnitSession.CurrentTheme.PropertyValue("Maps_API_Key")) && !Context.Request.IsSensorCertMobile())
                {%>
            <a id="tabGps" href="/Overview/GatewayGPS/<%:Model.GatewayID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("GatewayGPS")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("gps-pin") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("Location","Location") %>
                    </span>
                </div>
            </a>
            <%} %>

            <%if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
                {%>
            <a id="tabNotification" href="/Overview/GatewayNotification/<%:Model.GatewayID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("GatewayNotification") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("rules") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("Rules","Rules") %>
                    </span>
                </div>
            </a>
            <%} %>

            <%GatewayType gatewaytype = GatewayType.Load(Model.GatewayTypeID);
                if (gatewaytype.SupportsDataUsage)
                {
                    if (gatewaytype.GatewayTypeID != 30 || (gatewaytype.GatewayTypeID == 30 && (((Model.SystemOptions & 0x04) > 0))))
                    { %>
            <a id="tabDataUsage" href="/Overview/GatewayDataUsage/<%:Model.GatewayID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("GatewayDataUsage") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%:Html.GetThemedSVG("data usage") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("Data Usage","Data Usage") %>
                    </span>
                </div>
            </a>
            <% }
                }
                if (gatewaytype.GatewayTypeID == 30 || (gatewaytype.GatewayTypeID == 32 || gatewaytype.GatewayTypeID == 24 || gatewaytype.GatewayTypeID == 26))
                { %>
            <a id="tabDataUsage" href="/Overview/GatewayCellDataUsage/<%:Model.GatewayID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.ToLower().Contains("gatewaycelldatausage") ? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                    <%:Html.GetThemedSVG("data usage") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("Data Usage", "Data Usage") %>
                    </span>
                </div>
            </a>
            <% } %>

            <%if (MonnitSession.CustomerCan("Network_Edit_Gateway_Settings"))
                { %><a id="tabEdit" href="/Overview/GatewayEdit/<%:Model.GatewayID %>" class="deviceView_btn_row__device">
                    <div class="btn-<%:Request.RawUrl.Contains("GatewayEdit")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill">
                        <%=Html.GetThemedSVG("settings") %>
                        <span class="extra">&nbsp;<%: Html.TranslateTag("Settings","Settings") %>
                        </span>
                    </div>
                </a>
            <% } %>

            <a id="tabSensorList" href="/Overview/GatewaySensorList/<%:Model.GatewayID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("GatewaySensorList")? "active-fill shadow mb-lg-2" : "secondaryToggle" %> btn-lg btn-fill" style="max-width: 55px;">
                    <%=Html.GetThemedSVG("sensor") %>
                    <span class="extra">&nbsp;<%: Html.TranslateTag("Sensors","Sensors") %>
                    </span>
                </div>
            </a>
        </div>
    </div>
    <div class="col-12 device_detailsRow px-0 mb-4 two-card-container" style="gap: 0">
        <%----------------------
                  GatewayCard
            ------------------------%>
        <%Html.RenderPartial("_GatewayInfo", Model); %>

        <%if (Request.Url.PathAndQuery.Contains("GatewayEdit"))
            {%>
        <%-------------------------------------
                Gateway Info Car
    ------------------------------%>
        <div class="d-flex w-100 detailsReadings_card">
            <div class="rule-card_container device_detailsRow__card marginLeftOnLgScreen" style="width: 100%; height: inherit; margin-top: 0;">
                <div class="card_container__top ">
                    <div class="card_container__top__title docTitle">
                        <div style="display: flex; align-items: baseline; width: 100%; justify-content: space-between;">
                            <div class="hidden-xs">
                                <%: Html.TranslateTag("Gateway Info", "Gateway Info")%>
                            </div>
                            <div style="font-size: 14px; font-weight: 400;"><%= Model.GatewayType.Name%></div>
                        </div>
                    </div>
                </div>


                <div class="Gate-details" style="font-size: 1em; height: 100%;">

                    <div class="pump d-flex">
                        <div class="titleGate"><strong><%:Html.TranslateTag("Gateway ID/Code") %>:</strong></div>
                        <div><% Html.RenderPartial("~/Views/Shared/DeviceIDAndCheckCode.ascx", Model.GatewayID); %></div>
                    </div>

                    <div class="pump d-flex">
                        <div class="titleGate"><strong><%: Html.TranslateTag("Gateway/Edit|RadioBand", "RadioBand")%> : </strong></div>
                        <div><%=Model.RadioBand %></div>

                    </div>

                    <div class="pump d-flex">
                        <div class="titleGate"><strong><%: Html.TranslateTag("Gateway/Edit|APN", "APN")%> :</strong>  </div>
                        <div><%=Model.APNFirmwareVersion %></div>
                    </div>

                    <div class="pump d-flex">

                        <div class="titleGate"><strong><%: Html.TranslateTag("Gateway/Edit|Firmware", "Firmware")%> :</strong>  </div>
                        <div><%: Model.GatewayFirmwareVersion %></div>
                    </div>
                    <%if (MonnitSession.CurrentTheme.Theme == "Default")
                        { %>
                    <div class="pump d-flex">
                        <div class="titleGate"><strong><%: Html.TranslateTag("Gateway/Edit|SKU", "SKU")%> :</strong>  </div>
                        <div><%: Model.SKU.ToUpper() %></div>
                    </div>
                    <%} %>
                </div>

            </div>
        </div>
        <%   }   %>

        <%
            if (Request.Url.PathAndQuery.Contains("GatewayNotification"))
            {
        %>
        <div class="rule_card_container w-100 d-flex">
            <div class="d-flex w-100">
                <%Html.RenderPartial("DeviceActionControl", Model); %>
            </div>
        </div>
        <%
            }
            else if (Request.Url.PathAndQuery.Contains("Overview/GatewayGPS"))
            {
                List<VisualMapGateway> gatewayMaps = VisualMapGateway.LoadByGatewayID(Model.GatewayID);
        %>
        <div id="mapLocations" class="rule-card_container w-100" style="height: inherit; margin-top: 0;">
            <div class="card_container__top ">
                <div class="card_container__top__title docTitle">
                    <div style="display: flex; align-items: baseline;">
                        <div class="hidden-xs">
                            <%=Html.GetThemedSVG("map") %>
                        </div>
                        <span style="font-size: 18px; font-weight: 600; margin-left: 5px;"><%: Html.TranslateTag("Maps","Maps")%></span>
                    </div>
                </div>
            </div>

            <div class="x_content verticalScroll" style="height: 116px; overflow-y: scroll;">
                <div class="card__container__body">
                    <div class="col-12 card_container__body__content" style="font-size: 1.0rem; margin-top: 0px;">
                        <%if (gatewayMaps != null && gatewayMaps.Count > 0)
                            {
                                foreach (VisualMapGateway visualMapGateway in gatewayMaps)
                                {
                                    VisualMap visualMap = VisualMap.Load(visualMapGateway.VisualMapID);%>
                        <div id="dataInfo" style="cursor: pointer;" onclick="location.href='/Map/ViewMap/<%:visualMapGateway.VisualMapID %>';">
                            <div class="col-1 col-md-1 col-sm-1">
                                <%=Html.GetThemedSVG(visualMap.MapType == eMapType.GpsMap ? "gps-pin" : "static-map") %>
                            </div>
                            <div class="col-10 col-md-10 col-sm-10">
                                <%:visualMap.Name%>
                            </div>
                        </div>

                        <div class="clearfix"></div>
                        <!-- History-->
                        <hr style="margin: 0px;" />

                        <%
                                }
                            }
                            else
                            { %>
                        <div><%: Html.TranslateTag("Overview/GatewayLink|Not yet assigned to any maps","Not yet assigned to any maps")%></div>
                        <%}%>
                    </div>
                </div>
            </div>
        </div>








        <%
            }

            if (Request.RawUrl.Contains("GatewayCellDataUsage"))

            {%>
        <%---------------------------Data Status Card -------------------------%>
        <%Html.RenderPartial("_DataPlanCard", Model);%>

        <%}
            else if(MonnitSession.CurrentTheme.Theme == "Default" && !Request.RawUrl.Contains("GatewayNotification") && !Request.RawUrl.Contains("GatewayEdit"))
            {%>
        <%---------------------------
                                  Support Card 
                        -------------------------%>
        <div class=" col-12 col-lg-6 fullOnSm" style="display: flex; height: inherit;">
            <div class="rule-card_container device_detailsRow__card marginLeftOnLgScreen" style="width: 100%; height: inherit; margin-top: 0;">
                <div class="card_container__top ">
                    <div class="card_container__top__title docTitle">
                        <div style="display: flex; align-items: baseline;">
                            <div class="hidden-xs">
                                <%: Html.TranslateTag("Support", "Support")%>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="x_content">
                    <div class="card__container__body">
                        <div class="col-12 card_container__body__content">
                            <%Html.RenderPartial("_DeviceInfoSupport", Model.SKU);%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%} %>
    </div>
</div>




<script type="text/javascript">
    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('active-hover-fill') },
        function () { $(this).removeClass('active-hover-fill') }
    )
</script>

