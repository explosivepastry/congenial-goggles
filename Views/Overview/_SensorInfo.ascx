<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%  bool isFavorite = MonnitSession.IsSensorFavorite(Model.SensorID);
    string removeFavoriteAlertText = Html.TranslateTag("Remove from favorites?", "Remove from favorites?");
    string addFavoriteAlertText = Html.TranslateTag("Add to favorites?", "Add to favorites?");
%>

<div class="w-100" style="display: flex; height: inherit;">
    <div class="rule-card_container w-100 min-wd-400" style="height: inherit; margin-top: 0;">
        <div class="card_container__top ">
            <div class="card_container__top__title docTitle">
                <div style="display: flex; align-items: baseline; width: 100%">
                    <div title="<%=Model.ApplicationID %>" class="hidden-xs">
                        <%=Html.GetThemedSVG("sensor") %>
                        &nbsp;
                    </div>
                    <span style="font-size: 14px; font-weight: bold; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;" title="SensorID: <%= Model.SensorID%>"><%= Model.SensorName %> </span>
                    <div class="ruleSelectOptionsTop">

                        <%if (Model.TimeOfDayControl > 0)
                            {%>
                        <div class=" powertour-tooltip powertour-hook">
                            <div title="<%: Html.TranslateTag("Sensor Schedule","Sensor Schedule")%>" class="col-xs-6">
                                <%=Html.GetThemedSVG("schedule") %>
                            </div>
                        </div>
                        <%} %>

                        <span title="<%:isFavorite ? "Unfavorite Sensor" : "Favorite Sensor"%>" id="favoriteItem" class="ps-2" data-id="<%=Model.SensorID %>" <%=isFavorite ? "data-fav=true " : "data-fav=false "%>
                            onclick="favoriteItemClickEvent(this, '<%=removeFavoriteAlertText%>', '<%=addFavoriteAlertText%>', 'sensor')">
                            <%:Html.GetThemedSVG("heart-beat") %>                
                        </span>
                        <%if (MonnitSession.CustomerCan("Network_Edit"))
                            {%>
                        <a href="/Sensor/Remove/<%=Model.SensorID%>" onclick="removeSensor(this); return false;" title="<%: Html.TranslateTag("Remove", "Remove")%>">
                            <%=Html.GetThemedSVG("delete") %>
                        </a>
                        <%} %>
                    </div>

                    <%if ((!MonnitSession.IsEnterprise && MonnitSession.CurrentTheme.Theme == "Default") && Request.Path.Contains("Overview/SensorChart"))
                        { %>
                    <button style="margin-left: 20px;" class="docsBtn btn btn-primary btn-sm">Docs</button>
                    <%} %>
                </div>
            </div>
        </div>

        <div class="x_content">
            <div class="card__container__body">

                <div class="rule-on-off" style="height: auto;">
                    <!-- Color -->
                    <div class="corp-status  sensorStatus<%:Model.Status.ToString() %> " style="margin-right: 10px; height: auto;">
                    </div>
                    <!-- -->

                    <!-- Container-->
                    <div class="mainSensorCardInside">
                        <div class="mainSensorCardInside">

                            <%if (Model.LastCommunicationDate.AddMinutes(Model.MinimumCommunicationFrequency) > DateTime.UtcNow)
                                {
                                    if (Model.LastDataMessage != null)
                                    {
                                        if (!Model.IsPoESensor)
                                        {
                                            if (!Model.IsWiFiSensor)
                                            {
                                                Gateway gateway = Gateway.Load(Model.LastDataMessage.GatewayID);%>
                            <div class="mainSensorCardInside">
                                <div style="width: 100%; display: flex;">
                                    <b class="hidden-xs"><%: Html.TranslateTag("Gateway","Gateway")%>:</b>
                                    <a style="color: #007FEB;" href="/Overview/GatewayHome/<%:Model.LastDataMessage.GatewayID%>">&nbsp;

                                        <%--                  <%:Model.LastDataMessage.GatewayID > 0 ? Model.LastDataMessage.GatewayID.ToString() : ""%>--%>
                                        <%=gateway == null ? "" : gateway.Name %>
                         
                   

                                    </a>
                                </div>

                                <%  if (Model.ParentID > 0 && Model.ParentID != Model.LastDataMessage.GatewayID)
                                    {
                                        Sensor sens = Sensor.Load(Model.ParentID);
                                        if (sens != null && sens.ApplicationID == 45)
                                        {%>
                                <b class="hidden-xs">Repeater:  </b><%:Model.ParentID > 0 ? Model.ParentID.ToString() : ""%>
                                <%}
                                    }

                                %>
                            </div>
                            <% 
                                }
                                else
                                {
                                    Gateway gate = Gateway.Load(Model.LastDataMessage.GatewayID);%>
                            <b class="hidden-xs">MacAddress:  </b><%: gate.MacAddress%>
                            <%}
                                        }
                                    }
                                }%>

                            <div>
                                <b class="hidden-xs"><%: Html.TranslateTag("Network","Network")%>:</b>
                                <a style="color: #007FEB;" href="/Network/Edit/<%=Model.CSNetID %>">&nbsp;<%= Model.Network == null ? "" : Model.Network.Name.ToStringSafe() %></a>
                            </div>
                            <%
                                if (Model.LastCommunicationDate.AddMinutes(Model.MinimumCommunicationFrequency) > DateTime.UtcNow && Model.LastDataMessage != null)
                                {
                                    Gateway gtwy = Gateway.Load(Model.LastDataMessage.GatewayID);
                                    if (gtwy != null)
                {

                                        
                                    int ss = Model.LastDataMessage.SignalStrength;
                                    int lq = Model.LastDataMessage.LinkQuality;
                                    GatewayWIFICredential wifiCred = null;
                                    if (lq >= 0 && lq < 3)
                                    {
                                        wifiCred = gtwy.WifiCredential(lq + 1);
                                    }

                                    if (wifiCred != null && !string.IsNullOrEmpty(wifiCred.SSID))
                                    {
                            %>
                            <div>
                                <b class="hidden-xs"><%: Html.TranslateTag("WiFi","WiFi")%>:</b>
                                <a style="color: #007FEB;" href="/Overview/SensorEdit/<%=Model.SensorID%>">&nbsp;<%= wifiCred.SSID.ToStringSafe() %></a>
                            </div>
                            <%
                                    }

                }
                                }
                            %>
                        </div>

                        <div class="sigBatUp" style="justify-content: space-between; padding-top: 0.5rem;">
                            <div class="eventIcon_container holder holder<%:Model.Status.ToString() %>">
                                <%=Html.GetThemedSVG("app" + Model.ApplicationID) %>
                            </div>
                            <div id=" third_element_to_target" class="d-flex">
                                <%if (Model.LastDataMessage != null)
                                    { %>
                                <div id="dataMessageEZTherm" class="senshomecard"><%= Model.LastDataMessage.DisplayData%></div>
                                <%} %>
                            </div>

                            <div id="fourth_element_to_target" class="fourthE">
                                <div class="dfae_gap">
                                    <%
                                        // SensorPrint
                                        if (new Version(Model.FirmwareVersion) >= new Version("25.45.0.0") || Model.SensorPrintActive)
                                        {
                                            if (Model.SensorPrintActive && Model.LastDataMessage != null)
                                            {
                                                if (Model.LastDataMessage.IsAuthenticated)
                                                {%>
                                    <%=Html.GetThemedSVG("printCheck") %>
                                    <%}
                                        else
                                        {%>
                                    <%=Html.GetThemedSVG("printFail") %>
                                    <%}
                                            }
                                        }%>

                                    <div class=" powertour-tooltip powertour-hook">
                                        <div title="<%: Html.TranslateTag("Paused","Paused")%>" class="col-xs-6 pendingEditIcon pausesvg" <%: Model.isPaused() ? "" : "hidden" %>>
                                            <%=Html.GetThemedSVG("pause") %>
                                        </div>
                                    </div>

                                    <div class="powertour-tooltip powertour-hook">
                                        <div title="<%: Html.TranslateTag("Overview/_ThermostatEasy|Settings Update Pending","Settings Update Pending")%>" class="col-xs-6 pendingEditIcon pendingsvg" <%: Model.CanUpdate ? "hidden" : "" %>>
                                            <%=Html.GetThemedSVG("Pending_Update") %>
                                        </div>
                                    </div>

                                    <%MvcHtmlString SignalIcon = new MvcHtmlString("");
                                        if (Model.LastDataMessage != null)
                                        {
                                            // SignalStrength
                                            if (!Model.IsPoESensor)
                                            {
                                                int Percent = DataMessage.GetSignalStrengthPercent(Model.GenerationType, Model.SensorTypeID, Model.LastDataMessage.SignalStrength);
                                                string signal = "";

                                                if (Percent <= 0)
                                                    SignalIcon = Html.GetThemedSVG("signal-none");
                                                else if (Percent <= 10)
                                                    SignalIcon = Html.GetThemedSVG("signal-1");
                                                else if (Percent <= 25)
                                                    SignalIcon = Html.GetThemedSVG("signal-2");
                                                else if (Percent <= 50)
                                                    SignalIcon = Html.GetThemedSVG("signal-3");
                                                else if (Percent <= 75)
                                                    SignalIcon = Html.GetThemedSVG("signal-4");
                                                else
                                                    SignalIcon = Html.GetThemedSVG("signal-5");
                                    %>
                                    <div title="Signal Strength: <%=Percent %>%" style="width: 30px; height: 30px; margin-top: 10px;"><%= SignalIcon %></div>
                                    <%}
                                        else
                                        {%>

                                    <div title="PoE" style="width: 30px; height: 30px; margin-top: 10px;"><%= Html.GetThemedSVG("ether-icon") %></div>
                                    <%} %>
                                    <%} %>
                                </div>
                                <div>
                                    <div id="fifth_element_to_target" style="text-align: center;">

                                        <%--         <%MvcHtmlString plugPower = new MvcHtmlString("");
                                    if(Model.PowerSourceID == 3 )
                                    {
                                        plugPower = Html.GetThemedSVG("plugsensor1");
                                    }
                                    %>--%>

                                        <% MvcHtmlString PowerIcon = new MvcHtmlString("");

                                            DataMessage message = Model.LastDataMessage;

                                            string battLevel = "";
                                            string battType = "";
                                            string battModifier = "";
                                            if (message != null)
                                            {
                                                if (message.Battery <= 0)
                                                {
                                                    battLevel = "-0";
                                                    battModifier = " batteryCritical batteryLow";
                                                    PowerIcon = Html.GetThemedSVG("bat-dead");
                                                }
                                                else if (message.Battery <= 10)
                                                {
                                                    battLevel = "-1";
                                                    battModifier = " batteryCritical batteryLow";
                                                    PowerIcon = Html.GetThemedSVG("bat-low");
                                                }
                                                else if (message.Battery <= 25)
                                                {
                                                    battLevel = "-2";
                                                    PowerIcon = Html.GetThemedSVG("bat-low");
                                                }
                                                else if (message.Battery <= 50)
                                                {
                                                    battLevel = "-3";
                                                    PowerIcon = Html.GetThemedSVG("bat-half");
                                                }
                                                else if (message.Battery <= 75)
                                                {
                                                    battLevel = "-4";
                                                    PowerIcon = Html.GetThemedSVG("bat-full-ish");
                                                }
                                                else
                                                {
                                                    battLevel = "-5";
                                                    PowerIcon = Html.GetThemedSVG("bat-ful");
                                                }

                                                if (Model.PowerSourceID == 3 || message.Voltage == 0)
                                                {
                                                    battType = "-line";
                                                    battLevel = "";
                                                    PowerIcon = Html.GetThemedSVG("plugsensor1");
                                                }
                                                else if (Model.PowerSourceID == 4)
                                                {
                                                    battType = "-volt";
                                                    battLevel = "";

                                                }
                                                else if (Model.PowerSourceID == 1 || Model.PowerSourceID == 14)
                                                    // Coin cell (CR2032)
                                                    battType = "-cc";
                                                else if (Model.PowerSourceID == 2 || Model.PowerSourceID == 8 || Model.PowerSourceID == 10 || Model.PowerSourceID == 13 || Model.PowerSourceID == 15 || Model.PowerSourceID == 17 || Model.PowerSourceID == 19)
                                                    // Single cell cylindrical dry batteries (AA, AAA, C, D)    
                                                    battType = "-aa";
                                                else if (Model.PowerSourceID == 6 || Model.PowerSourceID == 7 || Model.PowerSourceID == 9 || Model.PowerSourceID == 16 || Model.PowerSourceID == 18)
                                                    // Lithium batteries (Tadiran, R123, rechargeable, cold weather, solar)    
                                                    battType = "-ss";
                                                else
                                                    battType = "-gateway";
                                            }%>

                                        <div class="dfjcsbac">
                                            <div title="Battery: <%=message == null ? "" : message.Battery.ToString()  %>%, Voltage: <%=message == null ? "" : message.Voltage.ToStringSafe() %> V">
                                                <%=PowerIcon %>
                                                <%=(battType == "-volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", message.Voltage) : "" %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div style="margin-top: 1em">
                            <div class="dfac dateSensorCard">
                                <div class="glance-lastDate" style="margin-right: 5px;"><%: Html.TranslateTag("Last Message","Last Message") %> : <%: Model.LastDataMessage == null ?   Html.TranslateTag("Unavailable","Unavailable") : (Model.LastCommunicationDate.ToElapsedMinutes() > 120 ? Model.LastCommunicationDate.OVToLocalDateTimeShort() : (Model.LastCommunicationDate.ToElapsedMinutes()  < 0 ? Html.TranslateTag("Unavailable","Unavailable") :  Model.LastCommunicationDate.ToElapsedMinutes().ToString() + " " + Html.TranslateTag("Minutes ago","Minutes ago")) ) %></div>

                                <div class="">
                                    <%: Html.TranslateTag("Overview/SensorHome|Next Check-In","Next Check-in") %>:
                                <% if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5) && Model.NextCommunicationDate > DateTime.UtcNow)
                                    {
                                        if (Model.NextCommunicationDate != DateTime.MinValue)
                                        { %>
                                    <span style="margin-right: 5px;" class="title2"><%: Model.NextCommunicationDate.OVToLocalDateShort()%></span>
                                    <span class="title2"><%: Model.NextCommunicationDate.OVToLocalTimeShort()%></span>
                                    <%}
                                        else
                                        {%>
                                    <span class="title2"><%: Html.TranslateTag("Unavailable","Unavailable") %></span>
                                    <%}
                                        }
                                        else
                                        { %>
                                    <span class="title2"><%: Html.TranslateTag("Unavailable","Unavailable") %></span>
                                    <%}%>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <!-- -->

        <%if (MonnitSession.CustomerCan("Support_Advanced"))
            {%>
        <div>
            <br />
            <b>Start Date:</b> <%=Model.StartDate %>
        </div>
        <%} %>
    </div>
</div>

<script>
    $(function () {
        isFavoriteItemCheck(<%:isFavorite ? "true" : "false" %>);
    });

    var confirmRemoveSensor = "<%: Html.TranslateTag("Overview/_SensorInfo|Are you sure you want to remove this sensor from the network?","Are you sure you want to remove this sensor from the network?")%>";

    function removeSensor(anchor) {
        if (confirm(confirmRemoveSensor)) {
            $.get($(anchor).attr("href"), function (data) {
                if (data == "Success") {
                    window.location.href = "/Overview/SensorIndex";
                }
                else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        }
    }

    const breakUpData = () => {
        const data = document.getElementById('dataMessageEZTherm');
        if (data && data.innerHTML.includes("Temperature") && ("System Mode") && ("Fan")) {
            const dataWithAddedBreaks = data.innerHTML.replace(/,/g, '<br>').replace(/<br>/, ',');
            data.innerHTML = dataWithAddedBreaks
        };
    };
    breakUpData();
</script>
