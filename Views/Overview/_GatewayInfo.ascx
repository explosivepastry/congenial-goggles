<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Gateway>" %>

<% 
    GatewayMessage message = GatewayMessage.LoadLastByGateway(Model.GatewayID);
    List<CSNet> accNetworks = CSNet.LoadByAccountID(Model.AccountID);

    bool isFavorite = MonnitSession.IsGatewayFavorite(Model.GatewayID);
    string removeFavoriteAlertText = Html.TranslateTag("Remove from favorites?", "Remove from favorites?");
    string addFavoriteAlertText = Html.TranslateTag("Add to favorites?", "Add to favorites?");

%>

<div class=" col-12 col-lg-6 fullOnSm" style="display: flex; height: inherit;">
    <div class="rule-card_container w-100 111" style="height: inherit; margin-top: 0;">
        <div class="card_container__top ">
            <div class="card_container__top__title docTitle">
                <div style="display: flex; align-items: baseline;">
                    <div title="<%=Model.GatewayTypeID %>" class="hidden-xs">
                        <%=Html.GetThemedSVG("gateway") %>
                        <span style="font-size: 14px; font-weight: 700;"><%= Model.Name %></span>
                    </div>
                </div>
            </div>

            <div class="ruleSelectOptionsTop" style="cursor: pointer">
                <span title="<%:isFavorite ? "Unfavorite Gateway" : "Favorite Gateway" %>" id="favoriteItem" data-id="<%=Model.GatewayID %>" <%=isFavorite ? "data-fav=true " : "data-fav=false "%>
                    onclick="favoriteItemClickEvent(this, '<%=removeFavoriteAlertText%>', '<%=addFavoriteAlertText%>', 'gateway')">
                    <%:Html.GetThemedSVG("heart-beat") %>                
                </span>
                <%if (MonnitSession.CustomerCan("Network_Edit"))
                    { %>
                <a onclick="removeGateway('<%:Model.GatewayID %>'); return false;" title="<%: Html.TranslateTag("Remove", "Remove")%>">
                    <%=Html.GetThemedSVG("delete") %></a>
                <%} %>
            </div>
        </div>
        <div class="x_content">
            <div class="card__container__body">

                <div class="rule-on-off" style="height: auto;">
                    <!--Color -->
                    <div class="corp-status  gatewayStatus<%:Model.Status.ToString() %> " style="height: auto;">
                    </div>
                    <!-- -->

                    <!-- Container -->
                    <div class="mainSensorCardInside" style="margin: 10px;">
                        <%if (accNetworks != null && accNetworks.Count > 1)
                            {%>
                        <div class="mainSensorCardInside">
                            <div>
                                <b class="hidden-xs"><%: Html.TranslateTag("Network","Network")%>:</b>
                                <a style="color: #007FEB;" href="/Network/Edit/<%=Model.CSNetID %>">&nbsp;<%= CSNet.Load(Model.CSNetID).Name %></a>
                            </div>
                        </div>
                        <%} %>

                        <div class="dfjcsbac" style="flex-wrap: wrap;">
                            <div id="fourth_element_to_target"></div>

                            <div class="sigBatUp" style="margin-top: 10px;">
                                <div id="second_element_to_target " style="width: 100%;">
                                    <div class=" holder holder<%:Model.Status.ToString() %>">

                                        <div class=" d-flex w-100 gw-icon-gwhome">
                                            <%=Html.GetThemedSVGForGateway(Model.GatewayTypeID) %>
                                            <div id=" third_element_to_target" class="d-flex">
                                            </div>
                                        </div>

                                    </div>
                                </div>

                                <div id="fifth_element_to_target" style="text-align: center; display: flex;">

                                    <%MvcHtmlString SignalIcon = new MvcHtmlString("");
                                        // SignalStrength
                                        if (message != null)
                                        {
                                            double Percent = Math.Round(Model.CurrentSignalStrength.ToDouble().LinearInterpolation(0,0,28,100));
                                            if (Percent > 100)
                                            {
                                                Percent = 100;
                                            }

                                            bool cellGateway =  Model.GatewayType.GatewayTypeID == 8 ||Model.GatewayType.GatewayTypeID == 9  || Model.GatewayType.GatewayTypeID == 13 || Model.GatewayType.GatewayTypeID == 14 || Model.GatewayType.GatewayTypeID == 17 || Model.GatewayType.GatewayTypeID == 18 || Model.GatewayType.GatewayTypeID == 19 || Model.GatewayType.GatewayTypeID == 20 || Model.GatewayType.GatewayTypeID == 21 || Model.GatewayType.GatewayTypeID == 23 || Model.GatewayType.GatewayTypeID == 24 ||  Model.GatewayType.GatewayTypeID == 25 || Model.GatewayType.GatewayTypeID == 26 || Model.GatewayType.GatewayTypeID == 27 || Model.GatewayType.GatewayTypeID == 30 || Model.GatewayType.GatewayTypeID == 32;

                                            if (cellGateway)
                                            {
                                                string signal = "";

                                                if (Percent == 0)
                                                {
                                                    SignalIcon = Html.GetThemedSVG("signal-none");

                                                }
                                                else if (Percent <= 10)
                                                {
                                                    SignalIcon = Html.GetThemedSVG("signal-1");

                                                }
                                                else if (Percent <= 25)
                                                {
                                                    SignalIcon = Html.GetThemedSVG("signal-2");

                                                }
                                                else if (Percent <= 50)
                                                {
                                                    SignalIcon = Html.GetThemedSVG("signal-3");

                                                }
                                                else if (Percent <= 75)
                                                {
                                                    SignalIcon = Html.GetThemedSVG("signal-4");

                                                }
                                                else
                                                {
                                                    SignalIcon = Html.GetThemedSVG("signal-5");

                                                }
                                    %>
                                        <div class="" title="Signal: <%=message == null ? "" : Percent.ToString()  %>%">
                                            <%=SignalIcon %>
                                        </div>
                                    <%}
                                        else
                                        {%>
                                    <div style="display: none" class="card-signal-icon icon icon-ethernet-b"></div>
                                    <%}
                                        }%>

                                    <% MvcHtmlString PowerIcon = Html.GetThemedSVG("plugsensor1");

                                        if
                                            (message != null)
                                        {
                                            if (message.Battery <= 0)
                                            {

                                                PowerIcon = Html.GetThemedSVG("bat-dead");
                                            }
                                            else if (message.Battery <= 10)
                                            {

                                                PowerIcon = Html.GetThemedSVG("bat-low");
                                            }
                                            else if (message.Battery <= 25)
                                            {

                                                PowerIcon = Html.GetThemedSVG("bat-low");
                                            }
                                            else if (message.Battery <= 50)
                                            {
                                                PowerIcon = Html.GetThemedSVG("bat-half");

                                            }
                                            else if (message.Battery <= 75)
                                            {
                                                PowerIcon = Html.GetThemedSVG("bat-full-ish");

                                            }
                                            else if (message.Battery > 75 && message.Battery <= 100)
                                            {
                                                PowerIcon = Html.GetThemedSVG("bat-ful");
                                            }
                                            else if (message.Battery > 100)
                                            {
                                                PowerIcon = Html.GetThemedSVG("plugsensor1");
                                            }
                                        }%>

                                    <div class="dfjcsbac">
                                        <div class="" title="Battery: <%=message == null ? "" : message.Battery.ToString()  %>%<%--, Voltage: <%=message == null ? "" : message.Voltage.ToStringSafe() %> V"--%>">
                                            <%=PowerIcon %>
                                        </div>

                                        <%if (Model.isPaused())
                                            { %>
                                        <div class="powertour-tooltip powertour-hook" id="hook-eight">
                                            <div title="<%: Html.TranslateTag("Paused","Paused")%>" class="col-xs-6 pendingEditIcon pausesvg" style="padding-top: 5px;">
                                                <%=Html.GetThemedSVG("pause") %>
                                            </div>
                                        </div>
                                        <%} %>
                                    </div>



                                </div>
                                <!-- fitth element -->
                            </div>



                            <div style="margin-top: 1em">
                                <div class="dfac dateSensorCard">
                                    <%if (!Model.isEnterpriseHost)
                                        { %>
                                    <div class="glance-lastDate" style="margin-right: 5px;">
                                        <%: Html.TranslateTag("Last Message") %> : 
                                    <%: message == null ? Html.TranslateTag("Unavailable","Unavailable") :
                                                (Model.LastCommunicationDate.ToElapsedMinutes() > 120 ? Model.LastCommunicationDate.OVToLocalDateTimeShort() :
                                                (Model.LastCommunicationDate.ToElapsedMinutes() < 0 ? Html.TranslateTag("Unavailable", "Unavailable") :
                                                Model.LastCommunicationDate.ToElapsedMinutes().ToString() + " " + Html.TranslateTag("Minutes ago", "Minutes ago"))) %>
                                    </div>

                                    <div>
                                        <span title="Operating Channel:<%: (Model.LastKnownChannel < 0) ? "Unknown" : "" + Model.LastKnownChannel%>">
                                            <%: Html.TranslateTag("Next Check-in") %>:
                                        </span>
                                        <% if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5) && Model.NextCommunicationDate > DateTime.UtcNow)
                                            {
                                                if (Model.NextCommunicationDate != DateTime.MinValue)
                                                { %>
                                        <span style="margin-right: 5px;" class="title2"><%: Model.NextCommunicationDate.OVToLocalDateShort()%></span>
                                        <span class="title2"><%: Model.NextCommunicationDate.OVToLocalTimeShort()%></span>
                                        <%}
                                            else
                                            {%>
                                        <span class="title2"><%: Html.TranslateTag("Unavailable","Unavailable")%></span> <%}
                                                   }
                                                   else
                                                   { %>
                                        <span class="title2"><%: Html.TranslateTag("Unavailable","Unavailable")%></span>
                                        <% } %>
                                    </div>
                                    <%
                                        }
                                        else
                                        {  %>
                                    <div class="glance-lastDate" style="background: white;">
                                        <span><%: Html.TranslateTag("Overview/GatewayLink|Server","Server")%> :</span>
                                        <span class="title2"><%: Model.ServerHostAddress %>:<%: Model.Port %></span>
                                    </div>
                                    <% } %>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- -->



                </div>
            </div>
        </div>

    </div>
</div>
<style>
    .gw-icon-gwhome > svg {
        width: 38px !important;
        /*     height: 32px !important;*/
        fill: #515356;
    }

    .gw-icon-gwhome {
        height: 40px !important;
    }
</style>

<script>
    $(function () {
        isFavoriteItemCheck(<%:isFavorite ? "true" : "false" %>);
    });

    function removeGateway(item) {
        let values = {};
        values.url = `/CSNet/Remove/${item}`;
        values.text = "<%=Html.TranslateTag("Are you sure you want to remove this gateway from the network? This will also Reform the Gateway.","Are you sure you want to remove this gateway from the network? This will also Reform the Gateway.")%>";
        values.redirect = "/Overview/GatewayIndex";
        openConfirm(values);
    }
</script>
