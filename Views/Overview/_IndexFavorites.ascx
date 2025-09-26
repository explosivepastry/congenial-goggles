<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%
    string removeFavoriteAlertText = Html.TranslateTag("Remove from favorites?", "Remove from favorites?");
    string addFavoriteAlertText = Html.TranslateTag("Add to favorites?", "Add to favorites?");
    List<CSNet> networks = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(MonnitSession.CurrentCustomer.AccountID);
%>



<%--<div class="favoritesGrid">--%>
<style>
    /*.parent{
  position: relative;
  width: 300px;
  height: 150px;
  border: 1px solid salmon;
}*/

    .parent .primary-link {
        display: block;
        margin-bottom: 2em;
    }

        /* 
  expands the clickable area of the main link 
  to fill the parent container, because it's the nearest
  ancestor with "position:relative"
*/
        .parent .primary-link::before {
            content: '';
            position: absolute;
            inset: 0;
        }

    .parent a:hover {
        color: green;
    }

    /* bring other links "forward" to make them clickable */
    .parent .secondary-link {
        position: relative;
        z-index: 1;
        /*padding: 10px;*/
        /*background: lightgreen;*/
    }

</style>
<div class="d-flex w-100 headerCard " style="align-items: center; gap: 10px; margin-bottom: .5rem;">
    <div class="top-icon"><%:Html.GetThemedSVG("heart-fav") %></div>
    <span style="width: 100%">Favorites</span>
</div>



<%
    if (MonnitSession.CurrentCustomerFavorites.AllFavoritesForCurrentCustomerAccountID().Count == 0)
    {
%>
<div class="favorites-msg">
    <h6><%:Html.TranslateTag("Create a snapshot of items you want to keep your eye on.") %></h6>

    <%:Html.TranslateTag(" Everywhere you see a heart icon,") %>
    <span class="add-a-fav" style="width: 20px; cursor: none"><%=Html.GetThemedSVG("heart-empty") %></span>
    <%:Html.TranslateTag("  click it to add that item—like sensors, rules, gateways, and maps—to Favorites as a customizable link. Select up to 25 Favorites to see on your home dashboard and arrange them in order of importance.") %>
</div>
<%
    }
    else
    {
%>


<!------------------ Fav Sensors   ----------------------->

<div class="fav-container " style="background: white;">




    <%List<CustomerFavoriteModel> favoriteSensors = MonnitSession.CurrentCustomerFavorites.Sensors.OrderBy(x => x.CustomerFavorite.OrderNum).ToList(); ;
        if (favoriteSensors.Count >= 1)
        {%>
    <div class="fav-sensor-list accordion" style="padding: 3px 24px;">
        <%:Html.GetThemedSVG("circle-sensor-blu") %>
        <span>Sensors</span>
    </div>

    <div class="container-S panel" style="background: none!important;">
        <% for (int i = 0; i < favoriteSensors.Count; i++)
            {
                long custFavID = favoriteSensors[i].CustomerFavorite.CustomerFavoriteID;
                Sensor sensor = favoriteSensors[i].Sensor;
                MvcHtmlString SignalIcon = new MvcHtmlString("");
                MvcHtmlString BatteryIcon = new MvcHtmlString("");

                DataMessage message = sensor.LastDataMessage;
                if (message != null)
                {
                    // Signal Icon
                    int Percent = DataMessage.GetSignalStrengthPercent(sensor.GenerationType, sensor.SensorTypeID, message.SignalStrength);
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

                    // Battery Icon
                    if (sensor.PowerSourceID == 3 || message.Voltage == 0)
                    {
                        BatteryIcon = Html.GetThemedSVG("plugsensor1");
                    }
                    else if (sensor.PowerSourceID == 4)
                    {
                        BatteryIcon = new MvcHtmlString("<div style='font-size: 25px; color: #2d4780;'>" + message.Voltage + " volts</div><div>&nbsp;</div>");
                    }
                    else
                    {
                        if (message.Battery <= 0)
                            BatteryIcon = Html.GetThemedSVG("bat-dead");
                        else if (message.Battery <= 10)
                            BatteryIcon = Html.GetThemedSVG("bat-low");
                        else if (message.Battery <= 25)
                            BatteryIcon = Html.GetThemedSVG("bat-low");
                        else if (message.Battery <= 50)
                            BatteryIcon = Html.GetThemedSVG("bat-half");
                        else if (message.Battery <= 75)
                            BatteryIcon = Html.GetThemedSVG("bat-full-ish");
                        else
                            BatteryIcon = Html.GetThemedSVG("bat-ful");
                    }
                }%>

        <div class="boxS small-list-card parent" data-index="<%=favoriteSensors[i].CustomerFavorite.OrderNum %>" data-id="<%=custFavID%>" data-objid="<%=sensor.SensorID %>" style="justify-content: start;">
            <a href="/Overview/SensorChart/<%= sensor.SensorID %>" class="primary-link"></a>
            <div class="corp-status change-rule-status " style="height: 100%;">
                <div class="sensor corp-status sensorIcon sensorStatus<%=sensor.Status %>"></div>
            </div>
            <div class="home-inside-data">
                <div class="home-icon-card"><%=Html.GetThemedSVG("app" + sensor.ApplicationID) %></div>
                <div class="home-name-card" title="Click to View Sensor Details/Chart - [SensorID: <%=sensor.SensorID %>]" data-shorty="<%:sensor.Name%>"><%=sensor.Name %> </div>
                <%--                                <div class="home-condition-card"><%=sensor.LastDataMessage != null ? sensor.LastDataMessage.DisplayData : ""%></div>--%>
                <div class="home-end-card" style="align-items: center;">
                    <div class="home-icon-card"><%=SignalIcon %></div>
                    <div class="home-icon-card-bat"><%=BatteryIcon %></div>

                    <%--<a class="home-icon-card"><%:Html.GetThemedSVG("menu") %></a>--%>
                    <div class="home-icon-card dropdown menu-hover" style="padding: 5px 6px 5px 0; z-index: 1;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                        <%=Html.GetThemedSVG("menu") %>
                    </div>

                    <ul class="dropdown-menu ddm" data-bs-auto-close="true">
                        <%if (MonnitSession.CustomerCan("Sensor_View_History"))
                            {%>
                        <li>
                            <a class="dropdown-item menu_dropdown_item secondary-link"
                                href="/Overview/SensorChart/<%=sensor.SensorID %>">
                                <span><%: Html.TranslateTag("Details","Details")%></span>
                                <%=Html.GetThemedSVG("details") %>
                            </a>
                        </li>
                        <%}   /*if  Sensor_View_History*/

                            if (MonnitSession.CustomerCan("Sensor_View_History"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item secondary-link"
                                href="/Overview/SensorHome/<%=sensor.SensorID %>">
                                <span><%: Html.TranslateTag("Readings","Readings")%></span>
                                <%=Html.GetThemedSVG("list") %>
                            </a>
                        </li>
                        <%}    /*if  Sensor_View_History*/

                            if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item secondary-link"
                                href="/Overview/SensorNotification/<%=sensor.SensorID %>">
                                <span><%: Html.TranslateTag("Rules","Rules")%></span>
                                <%=Html.GetThemedSVG("rules") %>
                            </a>
                        </li>
                        <%}   /*if  Sensor_View_Notifications*/

                            if (MonnitSession.CustomerCan("Sensor_Edit"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item secondary-link"
                                href="/Overview/SensorEdit/<%=sensor.SensorID %>">
                                <span><%: Html.TranslateTag("Settings","Settings")%></span>
                                <%=Html.GetThemedSVG("settings") %>
                            </a>
                        </li>
                        <%}   /*if  Sensor_Edit*/%>

                        <li>
                            <a class="dropdown-item menu_dropdown_item favoriteItem" data-id="<%=sensor.SensorID %>" data-fav="true"
                                onclick="removeFavorite(this, '<%=removeFavoriteAlertText%>','sensor')">
                                <span>Unfavorite</span>
                                <span class="heart-n-menu">
                                    <%=Html.GetThemedSVG("heart-beat") %>
                                </span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <%} %>
    </div>
    <% }%>


    <!------------------  Fav Gateways  ----------------------->

    <%List<CustomerFavoriteModel> favoriteGateways = MonnitSession.CurrentCustomerFavorites.Gateways.OrderBy(x => x.CustomerFavorite.OrderNum).ToList(); ;
        if (favoriteGateways.Count >= 1)
        {%>
    <div class="fav-sensor-list  accordion" style="padding: 3px 24px;">
        <%:Html.GetThemedSVG("circle-gateway") %>
        <span>Gateways</span>
    </div>

    <div class="container-G panel" style="background: none!important;">

        <% for (int i = 0; i < favoriteGateways.Count; i++)
            {
                long custFavID = favoriteGateways[i].CustomerFavorite.CustomerFavoriteID;
                Gateway gateway = favoriteGateways[i].Gateway;%>

        <div class="boxG small-list-card parent" data-index="<%=favoriteGateways[i].CustomerFavorite.OrderNum %>" data-id="<%=custFavID%>" data-objid="<%=gateway.GatewayID%>" style="justify-content: start; width: 200px;">
            <a href="/Overview/GatewayHome/<%= gateway.GatewayID %>" class="primary-link"></a>
            <div class="corp-status change-rule-status" style="height: 100%;">
                <div class="sensor corp-status sensorIcon sensorStatus<%=gateway.Status %>"></div>
            </div>
            <div class="d-flex" style="flex-direction: column; margin-left: 10px;">
                <div class="home-inside-data">
                    <div class="home-icon-card"><%=Html.GetThemedSVGForGateway(gateway.GatewayTypeID)%></div>
                    <div class="home-name-card" title="Click to View Gateway History: <%=gateway.Name%>"><%=gateway.Name %></div>
                </div>
                <div class="glance-reading">
                    <div class="glance-lastDate" style="font-size: 10px;"><%:Html.GetThemedSVG("clock") %> <%=gateway.LastCommunicationDate.OVToLocalDateTimeShort() %> </div>
                </div>
            </div>
            <div class="home-end-card">
                <%--<a class="home-icon-card menuEnd" ><%:Html.GetThemedSVG("menu") %></a>--%>
                <div class="home-icon-card menuEnd dropdown menu-hover" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                    <%=Html.GetThemedSVG("menu") %>
                </div>
                <ul class="dropdown-menu ddm" aria-labelledby="dropdownMenuButton" data-bs-auto-close="true">
                    <li>
                        <a class="dropdown-item menu_dropdown_item secondary-link"
                            href="/Overview/GatewayHome/<%=gateway.GatewayID %>">
                            <span><%: Html.TranslateTag("Messages","Messages")%></span>
                            <%=Html.GetThemedSVG("messages") %>
                        </a>
                    </li>

                    <li>
                        <a class="dropdown-item menu_dropdown_item secondary-link"
                            href="/Overview/GatewayNotification/<%=gateway.GatewayID %>">
                            <span><%: Html.TranslateTag("Rules","Rules")%></span>
                            <%=Html.GetThemedSVG("rules") %>
                        </a>
                    </li>

                    <%if (MonnitSession.CustomerCan("Network_Edit_Gateway_Settings"))
                        {%>
                    <li>
                        <a class="dropdown-item menu_dropdown_item secondary-link"
                            href="/Overview/GatewayEdit/<%=gateway.GatewayID %>">
                            <span><%: Html.TranslateTag("Settings","Settings")%></span>
                            <%=Html.GetThemedSVG("settings") %>
                        </a>
                    </li>
                    <%} %>

                    <li>
                        <a class="dropdown-item menu_dropdown_item secondary-link"
                            href="/Overview/GatewaySensorList/<%=gateway.GatewayID %>">
                            <span><%: Html.TranslateTag("Sensors","Sensors")%></span>
                            <%=Html.GetThemedSVG("sensor") %>
                        </a>
                    </li>

                    <li>
                        <a class="dropdown-item menu_dropdown_item favoriteItem" data-id="<%=gateway.GatewayID %>" data-fav="true"
                            onclick="removeFavorite(this, '<%=removeFavoriteAlertText%>','gateway')">
                            <span>Unfavorite</span>
                            <span class="heart-n-menu">
                                <%=Html.GetThemedSVG("heart-beat") %>
                            </span>
                        </a>
                    </li>

                </ul>
            </div>
        </div>
        <%} %>
    </div>
    <%}%>
    <!------------------  Fav Maps   ----------------------->

    <%List<CustomerFavoriteModel> favoriteMaps = MonnitSession.CurrentCustomerFavorites.VisualMaps.OrderBy(x => x.CustomerFavorite.OrderNum).ToList(); ;
        if (favoriteMaps.Count >= 1)
        {%>

    <div class="fav-sensor-list  accordion" style="padding: 3px 24px;">
        <%:Html.GetThemedSVG("circle-maps") %>
        <span>Maps</span>
    </div>

    <div class="containerMap panel" style="background: none!important">
        <% for (int i = 0; i < favoriteMaps.Count; i++)
            {
                long custFavID = favoriteMaps[i].CustomerFavorite.CustomerFavoriteID;
                VisualMap map = favoriteMaps[i].VisualMap;%>

        <div class="boxMap small-list-card parent" data-index="<%=favoriteMaps[i].CustomerFavorite.OrderNum %>" data-id="<%=custFavID%>" data-objid="<%=map.VisualMapID%>" style="height: 53px; justify-content: start; width: 200px;">
            <a href="/Map/ViewMap/<%= map.VisualMapID %>" class="primary-link"></a>
            <div class="corp-status change-rule-status" style="height: 100%;">
                <div class="sensor corp-status sensorIcon sensorStatusOK"></div>
            </div>
            <div class="home-inside-data">
                <div class="home-icon-card"><%=Html.GetThemedSVG(map.MapType == eMapType.GpsMap ? "gps-pin" : "static-map") %></div>
                <div class="home-name-card" title="Click to View Map: <%=map.Name%> [MapID: <%=map.VisualMapID%>]"><%=map.Name %></div>
                <div class="home-end-card">
                    <%--<a class="home-icon-card"><%:Html.GetThemedSVG("menu") %></a>--%>
                    <span class="menu-hover dropdown" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false" style="margin-left: -10px;">
                        <%=Html.GetThemedSVG("menu") %>
                    </span>
                    <div class="dropdown-menu ddm" data-bs-auto-close="true">

                        <ul class="ps-0 mb-0">
                            <li>
                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/ViewMap/<%:map.VisualMapID%>'">
                                    <%: Html.TranslateTag("View","View")%>
                                    <%=Html.GetThemedSVG("view") %>
                                </div>
                            </li>
                            <li>
                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/EditMap/<%:map.VisualMapID %>'">
                                    <%: Html.TranslateTag("Edit","Edit")%>
                                    <span>
                                        <%=Html.GetThemedSVG("edit") %>
                                    </span>
                                </div>
                            </li>
                            <li>
                                <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Map/DevicesToShow/<%:map.VisualMapID %>'">
                                    <%: Html.TranslateTag("Devices","Devices")%>
                                    <span>
                                        <%=Html.GetThemedSVG("gps-pin") %>
                                    </span>
                                </div>
                            </li>
                            <%-- <hr class="my-0">--%>
                            <li>
                                <a class="dropdown-item menu_dropdown_item favoriteItem" data-id="<%=map.VisualMapID %>" data-fav="true"
                                    onclick="removeFavorite(this, '<%=removeFavoriteAlertText%>','visualmap')">
                                    <span>Unfavorite</span>
                                    <span class="heart-n-menu">
                                        <%=Html.GetThemedSVG("heart-beat") %>
                                    </span>
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <%} %>
    </div>
    <%}%>
    <!------------------  Fav Notifications   ----------------------->

    <%List<CustomerFavoriteModel> favoriteNotifications = MonnitSession.CurrentCustomerFavorites.Rules.OrderBy(x => x.CustomerFavorite.OrderNum).ToList(); ;
        if (favoriteNotifications.Count >= 1)
        { %>
    <div class="fav-sensor-list accordion" style="padding: 3px 24px;">
        <%:Html.GetThemedSVG("circle-rules") %>
        <span>Rules</span>
    </div>

    <div class="containerRule panel" style="background: none!important">
        <% for (int i = 0; i < favoriteNotifications.Count; i++)
            {
                long custFavID = favoriteNotifications[i].CustomerFavorite.CustomerFavoriteID;
                Notification notification = favoriteNotifications[i].Rule;

                string svgIcon = "";
                switch (notification.NotificationClass)
                {
                    case eNotificationClass.Application:
                        svgIcon = Html.GetThemedSVG("sensor").ToString();
                        break;
                    case eNotificationClass.Low_Battery:
                        svgIcon = Html.GetThemedSVG("lowBattery").ToString();
                        break;
                    case eNotificationClass.Advanced:
                        svgIcon = Html.GetThemedSVG("gears").ToString();
                        break;
                    case eNotificationClass.Inactivity:
                        svgIcon = Html.GetThemedSVG("hourglass").ToString();
                        break;
                    case eNotificationClass.Timed:
                        svgIcon = Html.GetThemedSVG("clock").ToString();
                        break;
                }
                string backgroundOverwrite = "";
                string classOverwrite = "";
                string respond = "";
                string ruleHref = "";
                string ruleHrefTwo = "";
                string ruleTitle = "";
                string ruleStatus = "Ready";
                MvcHtmlString alertingIcon = new MvcHtmlString("");

                NotificationTriggered notificationTriggered = MonnitSession.OverviewHomeModel.NotificationsTriggered.Where(nt => nt.NotificationID == notification.NotificationID).FirstOrDefault();
                if (!notification.IsActive)
                {
                    ruleStatus = "Disabled";
                }
                else if (notificationTriggered != null)
                {
                    if (notificationTriggered.AcknowledgedTime == DateTime.MinValue)
                    {
                        backgroundOverwrite = "background:#ffcaca;";
                        alertingIcon = Html.GetThemedSVG("notifications");
                        classOverwrite = "Acknowledge";
                        respond = "AckAllButton(this);return false;";
                        ruleHref = "/Notification/AcknowledgeActiveNotifications";
                        ruleHrefTwo = "&AckMethod=Browser_MainList";
                        ruleTitle = "Click to disarm alarming rule ";
                        ruleStatus = "Alerting";
                    }
                    else if (notificationTriggered.resetTime == DateTime.MinValue)
                    {
                        backgroundOverwrite = "background-color:#ffedd4;";
                        backgroundOverwrite = "background-color:#ffedd4;";
                        alertingIcon = Html.GetThemedSVG("reset");
                        classOverwrite = "Reset";
                        respond = "ResetAllPending(this);return false;";
                        ruleHref = "/Notification/ResetPendingConditions";
                        ruleTitle = "Condition pending reset. Click to force reset.";
                        ruleStatus = "Acknowledged";
                    }
                    else if (notification.LastSent > DateTime.MinValue)
                    {
                        ruleStatus = "Resolved";
                    }
                }
        %>
        <div class="boxRule small-list-card parent" data-index="<%=favoriteNotifications[i].CustomerFavorite.OrderNum %>" data-id="<%=custFavID%>" data-objid="<%=notification.NotificationID%>" style="height: 53px; justify-content: start; width: 200px;">
            <a href="/Rule/History/<%= notification.NotificationID %>" class="primary-link"></a>
            <div class="corp-status change-rule-status" style="height: 100%;">
                <div class="sensor corp-status sensorIcon <%=notification.IsActive ? "sensorStatusOK" : "sensorStatusInactive"%>"></div>
            </div>
            <div class="home-inside-data">
                <div class="home-icon-card"><%=svgIcon %></div>
                <div class="home-name-card" title="Click to View Notification: <%=notification.Name %> [<%=notification.NotificationID%>]"><%=notification.Name %></div>
                <%--           <div class="home-condition-card">
                                    <%: Html.TranslateTag("Events/Details|Last Sent","Last Sent")%>:
                                    <%if (notification.LastSent > DateTime.MinValue)
                                      { %>
                                        <%:notification.LastSent.OVToLocalDateTimeShort()%>
                                    <%} %>
                                    <br />
                                    <%:Html.Partial("~/Views/Rule/_RuleConditionAsSentenceMain.ascx", notification) %>
                                </div>--%>
                <div class="home-end-card">
                    <%--<a class="home-icon-card"><%:Html.GetThemedSVG("menu") %></a>--%>
                    <div class="menu-hover dropdown" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                        <%=Html.GetThemedSVG("menu") %>
                    </div>
                    <ul class="dropdown-menu ddm">
                        <% bool showtest = true;
                            if (notificationTriggered != null)
                            {
                                if (ruleStatus == "Alerting")
                                {
                                    showtest = false;%>
                        <li style="<%: backgroundOverwrite %>">
                            <a title="<%: Html.TranslateTag("Shared/TopBarEventList|Click to disarm alerting rule")%>" class="dropdown-item menu_dropdown_item"
                                href="/Notification/AcknowledgeActiveNotifications?notificationID=<%:notification.NotificationID%>&userID=<%:MonnitSession.CurrentCustomer.CustomerID%>
                                                                &AckMethod=Browser_MainList"
                                onclick="AckAllButton(this);return false;">
                                <span style="font-weight: bold;"><%: Html.TranslateTag("Acknowledge")%></span>
                                <i style="color: #ff4d2d; margin-right: 0px; font-size: 1.2em; font-weight: bold;" class="fa fa-bell-o ackBellListPage"></i>
                            </a>
                        </li>
                        <%}
                            else if (ruleStatus == "Acknowledged"
                                    && notification.IsActive == true)
                            {
                                showtest = false;%>
                        <li style="<%: backgroundOverwrite %>">
                            <a title="<%: Html.TranslateTag("Acknowledged","Acknowledged")%> <%:notificationTriggered.AcknowledgedTime.ToShortDateString() %>, 
                                                                <%: Html.TranslateTag("Condition pending reset. Click to force reset.","Condition pending reset. Click to force reset.")%>"
                                class="dropdown-item menu_dropdown_item"
                                href="/Notification/ResetPendingConditions?notificationID=<%:notification.NotificationID%>&userID=<%:MonnitSession.CurrentCustomer.CustomerID%>"
                                onclick="ResetAllPending(this);return false;">
                                <span style="font-weight: bold;"><%: Html.TranslateTag("Reset","Reset")%></span>
                                <%=Html.GetThemedSVG("reset") %>
                            </a>
                        </li>
                        <%}
                            }
                            if (notification.NotificationRecipients.Count > 0 && showtest)
                            {%>
                        <li>
                            <a class="dropdown-item menu_dropdown_item notitest" title="Send Test" id="notiTest" onclick="SendTest('<%=notification.NotificationID %>');">
                                <span><%: Html.TranslateTag("Send Test","Send Test")%></span>
                                <%=Html.GetThemedSVG("sendTest") %>
                            </a>
                        </li>
                        <span id="testMessage_<%=notification.NotificationID %>" style="color: red; font-weight: bold; font-size: 0.8em;"></span>
                        <hr style="margin-top: 5px; margin-bottom: 5px;" />
                        <%}
                            if (MonnitSession.CustomerCan("Notification_Disable_Network"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item" data-id="<%:notification.NotificationID%>" onclick="toggleRuleStatus(this);">
                                <span id="toggleText_<%:notification.NotificationID%>"><%:notification.IsActive ?  Html.TranslateTag("Disable") :Html.TranslateTag("Enable")%></span>
                                <%=Html.GetThemedSVG("disable") %>
                            </a>
                        </li>
                        <%} %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item secondary-link" href="/Rule/History/<%:notification.NotificationID%>">
                                <span><%: Html.TranslateTag("History","History")%></span>
                                <%=Html.GetThemedSVG("list") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item secondary-link" href="/Rule/Triggers/<%:notification.NotificationID%>">
                                <span><%: Html.TranslateTag("Conditions","Conditions")%></span>
                                <%=Html.GetThemedSVG("conditions") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item secondary-link" href="/Rule/ChooseTaskToEdit/<%:notification.NotificationID%>">
                                <span><%: Html.TranslateTag("Tasks","Tasks")%></span>
                                <%=Html.GetThemedSVG("tasks") %>
                            </a>
                        </li>
                        <li>
                            <a class="dropdown-item menu_dropdown_item secondary-link" href="/Rule/Calendar/<%:notification.NotificationID%>">
                                <span><%: Html.TranslateTag("Schedule","Schedule")%></span>
                                <%=Html.GetThemedSVG("schedule") %>
                            </a>
                        </li>
                        <%if (MonnitSession.CustomerCan("Notification_Edit"))
                            { %>
                        <li>
                            <a class="dropdown-item menu_dropdown_item favoriteItem" data-id="<%=notification.NotificationID %>" data-fav="true"
                                onclick="removeFavorite(this, '<%=removeFavoriteAlertText%>', 'notification')">
                                <span><%: Html.TranslateTag("Unfavorite","Unfavorite")%></span>
                                <span class="heart-n-menu">
                                    <%=Html.GetThemedSVG("heart-beat") %>
                                </span>
                            </a>
                        </li>
                        <%} %>
                    </ul>
                </div>
            </div>
        </div>
        <%} %>
    </div>
    <%}%>
    <!------------------   Fav Reports    ----------------------->

    <%List<CustomerFavoriteModel> favoriteReports = MonnitSession.CurrentCustomerFavorites.ReportSchedules.OrderBy(x => x.CustomerFavorite.OrderNum).ToList(); ;
        if (favoriteReports.Count >= 1)
        { %>
    <div class="fav-sensor-list accordion" style="padding: 3px 24px;">
        <%:Html.GetThemedSVG("circle-reports") %>
        <span>Reports</span>
    </div>

    <div class="containerReport panel" style="background: none!important">

        <% for (int i = 0; i < favoriteReports.Count; i++)
            {
                long custFavID = favoriteReports[i].CustomerFavorite.CustomerFavoriteID;
                ReportSchedule report = favoriteReports[i].ReportSchedule;%>

        <div class="boxReport small-list-card parent" data-index="<%=favoriteReports[i].CustomerFavorite.OrderNum %>" data-id="<%=custFavID%>" data-objid="<%=report.ReportScheduleID%>" style="justify-content: start; width: 200px; align-items: center;">
            <a href="/Export/ReportHistory/<%= report.ReportScheduleID %>" class="primary-link"></a>
            <div class="corp-status change-rule-status" style="height: 100%;">
                <div class="sensor corp-status sensorIcon <%=report.IsActive ? "sensorStatusOK" : "sensorStatusInactive" %>"></div>
            </div>
            <div class="d-flex" style="flex-direction: column; margin-left: 10px;">
                <div class="home-inside-data ">
                    <div class="home-icon-card"><%=Html.GetThemedSVG("book") %></div>
                    <div class="home-name-card" title="Click to View Report History: <%:report.Name%> [<%:report.ReportScheduleID%>]" data-shorty="<%:report.Name%>"><%=report.Name %></div>
                </div>
                <div class="glance-reading">
                    <div class="glance-lastDate" style="font-size: 10px;">
                        <%: Html.TranslateTag("Export/ReportDetails|Last Run Date:","Last Run")%>: <%:report.LastRunDate.OVToLocalDateTimeShort() %>
                    </div>
                </div>
            </div>
            <div class="home-end-card">
                <%--<a class="home-icon-card"><%:Html.GetThemedSVG("menu") %></a>--%>

                <span class="menu-hover dropdown" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                    <%=Html.GetThemedSVG("menu") %>
                </span>
                <div class="dropdown-menu ddm">
                    <ul class="ps-0 mb-0">
                        <li>
                            <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Export/ReportHistory/<%:report.ReportScheduleID %>'">
                                <%: Html.TranslateTag("Export/ReportDetails|View","View")%>
                                <span><%=Html.GetThemedSVG("view") %></span>
                            </div>
                        </li>
                        <li>
                            <div class="dropdown-item menu_dropdown_icons_items " onclick="event.preventDefault(); window.location='/Export/ReportEdit/<%:report.ReportScheduleID %>'">
                                <%: Html.TranslateTag("Export/ReportDetails|Edit","Edit")%>
                                <span><%=Html.GetThemedSVG("edit") %></span>
                            </div>
                        </li>
                        <li>
                            <div class="dropdown-item menu_dropdown_icons_items" onclick="event.preventDefault(); window.location='/Export/ReportRecipient/<%:report.ReportScheduleID %>'">
                                <%: Html.TranslateTag("Export/ReportDetails|Recipients","Recipients")%>
                                <span><%=Html.GetThemedSVG("recipients") %></span>
                            </div>
                        </li>
                        <li>
                            <div class="dropdown-item menu_dropdown_icons_items" data-id="<%:report.ReportScheduleID%>" onclick="toggleReportStatus(this);">
                                <span id="toggleText_<%:report.ReportScheduleID%>"><%:report.IsActive ?  Html.TranslateTag("Disable") :Html.TranslateTag("Enable")%></span>
                                <div class="corp-status" <%:report.IsActive ? "sensorStatusOK" : "sensorStatusInactive"%>>
                                    <%=Html.GetThemedSVG("disable") %>
                                </div>
                            </div>
                        </li>
                        <%--  <hr class="my-0">--%>
                        <li>
                            <a class="dropdown-item menu_dropdown_item favoriteItem" data-id="<%=report.ReportScheduleID %>" data-fav="true"
                                onclick="removeFavorite(this, '<%=removeFavoriteAlertText%>', 'reportschedule')">
                                <span>Unfavorite</span>
                                <span class="heart-n-menu">
                                    <%=Html.GetThemedSVG("heart-beat") %>
                                </span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <%} %>
    </div>
    <%}%>

    <!------------------  Fav Locations  ----------------------->

    <%List<CustomerFavoriteModel> favoriteLocations = MonnitSession.CurrentCustomerFavorites.Locations.OrderBy(x => x.CustomerFavorite.OrderNum).ToList();

        if (favoriteLocations.Count >= 1)
        { %>
    <div class="fav-sensor-list accordion" style="padding: 3px 24px;">
        <%:Html.GetThemedSVG("circle-blu-locations") %>
        <span>Locations</span>
    </div>

    <div class="containerLocation panel" style="background: none!important">
        <% Tuple<List<AccountLocationSearchModel>, AccountLocationOverviewHeaderModel> Result = AccountLocationSearchModel.LocationSearch(MonnitSession.CurrentCustomer.AccountID, null);
            for (int i = 0; i < favoriteLocations.Count; i++)
            {
                long custFavID = favoriteLocations[i].CustomerFavorite.CustomerFavoriteID;
                //long orderNum = favoriteLocations[i].CustomerFavorite.OrderNum;
                Account location = favoriteLocations[i].Location;
                AccountLocationSearchModel accountLocationSearchModel = Result != null && Result.Item1 != null ? Result.Item1.Where(x => x.AccountID == location.AccountID).FirstOrDefault() : null;
                if (accountLocationSearchModel != null)
                {
                    //string corpPartialView = accountLocationSearchModel.SubAccounts > 0 ? "~/Views/Settings/CorporateCards.ascx" : "~/Views/Settings/CorporateLeafCards.ascx";
                    string sensorStatus = "sensorStatus";
                    if (accountLocationSearchModel.DevicesAlerting > 0)
                        sensorStatus += "Alert";
                    else if (accountLocationSearchModel.DevicesWarning > 0)
                        sensorStatus += "Warning";
                    else if (accountLocationSearchModel.DevicesOffline > 0)
                        sensorStatus += "Offline";
                    else
                        sensorStatus += "OK";

                    bool isCorporateCard = accountLocationSearchModel.SubAccounts > 0;
                    string onclickMethod = isCorporateCard ? "viewLocationsQuick" : "viewAccountQuick";
        %>

        <div class="boxLocation small-list-card parent" data-index="<%=favoriteLocations[i].CustomerFavorite.OrderNum %>" data-id="<%=custFavID%>" data-objid="<%=location.AccountID%>" data-accountid="<%=accountLocationSearchModel.AccountID %>" data-destination="/Overview/SensorIndex/" style="justify-content: start; /*width: 220px; height: 69px; */ align-items: center;">
            <a href="/Account/ProxySubAccount/<%= location.AccountID %>" class="primary-link"></a>
            <div class="corp-status change-rule-status" style="height: 100%;">
                <div class="sensor corp-status sensorIcon <%=sensorStatus %>"></div>
            </div>
            <div class="d-flex" style="flex-direction: column; margin-left: 10px;">
                <div class="home-inside-data ">
                    <div class="home-icon-card"></div>
                    <div class="home-name-card" title="Click to Add Sensor to Location Name: <%=accountLocationSearchModel.AccountNumber %>" data-shorty="<%:accountLocationSearchModel.AccountNumber%>" style="font-weight: bold;"><%=accountLocationSearchModel.AccountNumber%></div>
                </div>
                <div class="searchCardDiv " style="width: 8rem;">
                    <div class="corp-top-title">
                        <div class="corp-grid-boxes" style="width: 169px; height: 55px; margin-left: 17px;">
                            <%if (isCorporateCard)
                                {%>
                            <div class="corp-box corp-alert-fav">
                                <%=Html.GetThemedSVG("db-location") %><div class="db-number"><%=accountLocationSearchModel.SubAccounts %></div>
                            </div>
                            <div class="corp-box corp-alert-fav">
                                <%=Html.GetThemedSVG("db-alert") %><div class="db-number"><%=accountLocationSearchModel.DevicesAlerting %></div>
                            </div>
                            <div class="corp-box corp-alert-fav">
                                <%=Html.GetThemedSVG("db-low-battery") %><div class="db-number"><%=accountLocationSearchModel.DevicesWarning %></div>
                            </div>
                            <div class="corp-box corp-alert-fav">
                                <%=Html.GetThemedSVG("db-wifi-off") %><div class="db-number"><%=accountLocationSearchModel.DevicesOffline %></div>
                            </div>
                            <%}
                                else
                                {%>
                            <div class="corp-circle-fav leaf-alert" style="width: 90%">
                                <%=Html.GetThemedSVG("single-alert-red") %><div class="db-leaf-number"><%=accountLocationSearchModel.DevicesAlerting %></div>
                            </div>
                            <div class="corp-circle-fav leaf-battery" style="width: 90%">
                                <%=Html.GetThemedSVG("single-batlow-y") %><div class="db-leaf-number"><%=accountLocationSearchModel.DevicesWarning %></div>
                            </div>
                            <div class="corp-circle-fav leaf-wifi" style="width: 90%">
                                <%=Html.GetThemedSVG("single-wifioff") %><div class="db-leaf-number"><%=accountLocationSearchModel.DevicesOffline %></div>
                            </div>
                            <div class="corp-circle-fav leaf-check" style="width: 90%">
                                <%=Html.GetThemedSVG("single-check") %><div class="db-leaf-number"><%=accountLocationSearchModel.DevicesOK %></div>
                            </div>
                            <%} %>
                        </div>
                    </div>
                </div>
            </div>
            <div class="home-end-card">
                <span class="home-icon-card menuEnd dropdown menu-hover" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                    <%=Html.GetThemedSVG("menu") %>
                </span>
                <ul class="dropdown-menu ddm" aria-labelledby="dropdownMenuButton">
                    <li>
                        <div class="dropdown-item menu_dropdown_icons_items" data-accountid="<%=accountLocationSearchModel.AccountID %>"
                            data-href="/Account/ProxySubAccount/" onclick="<%=onclickMethod%>(this); return false;">
                            <%: Html.TranslateTag("Overview/Index|Proxy","Proxy")%>
                            <span class="index-menu-ico" style="width: 20px"><%=Html.GetThemedSVG("login") %></span>
                        </div>
                    </li>

                    <%if (MonnitSession.CustomerCan("Account_View"))
                        {%>
                    <li>
                        <div class="dropdown-item menu_dropdown_icons_items" onclick="proxySubAccountAndRedirect(this); return false;"
                            data-accountid="<%=accountLocationSearchModel.AccountID %>" data-destination="/Overview/SensorIndex/"
                            title="<%: Html.TranslateTag("Overview/Index|Account Sensors","Account Sensors")%>">
                            <%: Html.TranslateTag("Overview/Index|Sensors","Sensors")%>
                            <span class="index-menu-ico" style="width: 24px"><%=Html.GetThemedSVG("sensor") %></span>
                        </div>
                    </li>
                    <li>
                        <div class="dropdown-item menu_dropdown_icons_items " onclick="proxySubAccountAndRedirect(this); return false;"
                            data-accountid="<%=accountLocationSearchModel.AccountID %>" data-destination="/Rule/Index/"
                            title="<%:Html.TranslateTag("Overview/Index|Account AlertCount","Account AlertCount")%>">
                            <%: Html.TranslateTag("Overview/Index|Rules","Rules")%>
                            <span class="index-menu-ico"><%=Html.GetThemedSVG("rules") %></span>
                        </div>
                    </li>
                    <%} %>

                    <li>
                        <a class="dropdown-item menu_dropdown_icons_items" onclick="proxySubAccountAndRedirect(this); return false;"
                            data-accountid="<%=accountLocationSearchModel.AccountID %>" data-destination="/Settings/AccountUserList/">
                            <%: Html.TranslateTag("Overview/Index|Users","Users")%>
                            <span class="index-menu-ico"><%=Html.GetThemedSVG("recipients") %></span>
                        </a>
                    </li>
                    <li>
                        <a class="dropdown-item menu_dropdown_icons_items" onclick="proxySubAccountAndRedirect(this); return false;"
                            data-accountid="<%=accountLocationSearchModel.AccountID %>" data-destination="/Settings/AccountEdit/">
                            <%: Html.TranslateTag("Overview/Index|Settings","Settings")%>
                            <span class="index-menu-ico"><%=Html.GetThemedSVG("user-settings") %></span>
                        </a>
                    </li>

                    <%if (accountLocationSearchModel.SubAccounts == 0 && MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CustomerCan("Navigation_View_Administration") || MonnitSession.CustomerCan("Can_Create_Locations"))
                        { %>
                    <li>
                        <a class="dropdown-item menu_dropdown_icons_items" onclick="proxySubAccountAndRedirect(this); return false;"
                            data-accountid="<%=accountLocationSearchModel.AccountID %>" data-destination="/Settings/CreateLocationAccount/"
                            title="<%: Html.TranslateTag("Overview/Index|Add Location","Add Location")%>">
                            <%: Html.TranslateTag("Overview/Index|Add Location","Add Location")%>
                            <span class="index-menu-ico"><%=Html.GetThemedSVG("gps-pin") %></span>
                        </a>
                    </li>
                    <%}

                        if (MonnitSession.IsCurrentCustomerMonnitAdmin || MonnitSession.CurrentCustomer.CanAssignParent(accountLocationSearchModel.AccountID) /*|| MonnitSession.CurrentCustomer.Account.IsReseller*/)
                        {%>
                    <li>
                        <a class="dropdown-item menu_dropdown_icons_items" onclick="proxySubAccountAndRedirect(this); return false;"
                            data-accountid="<%=accountLocationSearchModel.AccountID %>" data-destination="/Settings/AssignParentSearch/"
                            title="<%: Html.TranslateTag("Overview/Index|Assign Parent","Assign Parent")%>">
                                <%: Html.TranslateTag("Overview/Index|Assign Parent","Assign Parent")%>
                                <span class="index-menu-ico"><%=Html.GetThemedSVG("add") %></span>
                        </a>
                    </li>
                    <%}

                        if (MonnitSession.CustomerCan("Account_View"))
                        {
                            if (MonnitSession.IsCurrentCustomerMonnitAdmin && MonnitSession.CustomerCan("Account_Set_Premium"))
                            { %>
                    <li>
                        <a class="dropdown-item menu_dropdown_icons_items" onclick="proxySubAccountAndRedirect(this); return false;"
                            data-accountid="<%=accountLocationSearchModel.AccountID %>" data-destination="/Settings/AdminSubscriptionDetails/"
                            title="<%: Html.TranslateTag("Overview/Index|Account Subscriptions","Account Subscriptions")%>">
                            <%: Html.TranslateTag("Overview/Index|Subscriptions","Subscriptions")%>
                            <span class="index-menu-ico"><%=Html.GetThemedSVG("subscription") %></span>
                        </a>
                    </li>
                    <%}
                        } %>
                    <li>
                        <a class="dropdown-item menu_dropdown_item favoriteItem" data-id="<%=accountLocationSearchModel.AccountID %>" data-fav="true"
                            onclick="removeFavorite(this, '<%=removeFavoriteAlertText%>','location')">
                            <span><%: Html.TranslateTag("Unfavorite","Unfavorite")%></span>
                            <span class="heart-n-menu">
                                <%=Html.GetThemedSVG("heart-beat") %>
                            </span>
                        </a>
                    </li>
                </ul>
            </div>
            <%--<%:Html.Partial(corpPartialView, accountLocationSearchModel) %>--%>
        </div>
        <%}
            }%>
    </div>
    <%}%>
</div>



<%
    }
%>
<%--</div>--%>

<script>
    <%= ExtensionMethods.LabelPartialIfDebug("_IndexFavorites.ascx") %>

    $(document).ready(function () {


        var acc = document.getElementsByClassName("accordion");

        window.addEventListener('resize', function () {
            if (window.innerWidth <= 769) {
                for (let i = 0; i < acc.length; i++) {
                    acc[i].addEventListener("click", function () {
                        this.classList.toggle("active");
                        var panel = this.nextElementSibling;
                        if (panel.style.maxHeight) {
                            panel.style.maxHeight = null;
                            panel.style.overflow = "scroll";
                        } else {
                            panel.style.maxHeight = "fit-content";
                            panel.style.overflow = "";
                        }
                    });
                    if (!acc[i].classList.contains('active')) {
                        acc[i].click();
                    }
                }
            }
        });

        if (window.innerWidth <= 769) {
            for (let i = 0; i < acc.length; i++) {
                acc[i].addEventListener("click", function () {
                    this.classList.toggle("active");
                    var panel = this.nextElementSibling;
                    if (panel.style.maxHeight) {
                        panel.style.maxHeight = null;
                        panel.style.overflow = "scroll";
                    } else {
                        panel.style.maxHeight = "fit-content";
                        panel.style.overflow = "";
                    }
                });
                acc[i].click();
            }
        }

        $('.boxLocation').on('contextmenu', function (e) {
            event.preventDefault();
        })

        $('.panel .small-list-card').click(function (e) {
            event.preventDefault();
            var objClasses = this.className;
            var objID = $(this).attr('data-objid');

            if (objClasses.includes("boxS")) {
                location.href = '/Overview/SensorChart/' + objID;
            }
            else if (objClasses.includes("boxG")) {
                location.href = '/Overview/GatewayHome/' + objID;
            }
            else if (objClasses.includes("boxMap")) {
                location.href = '/Map/ViewMap/' + objID;
            }
            else if (objClasses.includes("boxRule")) {
                location.href = '/Rule/History/' + objID;
            }
            else if (objClasses.includes("boxReport")) {
                location.href = '/Export/ReportHistory/' + objID;
            }
            else if (objClasses.includes("boxLocation")) {
                proxySubAccountAndRedirect(this);
            }

        });

        $('.home-icon-card, .dropdown, .menu-hover').click(function (e) {
            e.stopPropagation();
            $('ul.dropdown-menu.ddm.show').removeClass('show'); // prevent more than 1 being displayed at the same time
            //$(this).closest('.dropdown-menu').toggle();
        });


        $('.panel').sortable({
            start: handleSortStart,
            stop: handleSortStop,
            update: handleSortUpdate
        });




    });
    function handleSortStart(e, ui) {
        ui.item.data('idx', ui.item.index());
    }

    function handleSortUpdate(e, ui) {
        if (ui.item.data('idx') == ui.item.index()) {
            //console.log("error\nerror\nerror\nerror\nerror\nerror\nerror\nerror\nerror\nerror");
            //console.log("update called but card hasn't moved!");
            //console.log(ui.item.data('idx') + ' == ' + ui.item.index());
        }

        e.stopPropagation(); // stops the browser from redirecting.

        var children = $(this).children();

        var data = $.map(children, function (c, i) { var x = $(c); var r = x.data('id') + '_' + i; return r; });
        var res = data.join('|');

        $.post('/Overview/UpdateFavoritesOrder/', { favOrders: res },
            function (data) {
                if (data == "Success") {
                    $.each(children, function (i, c) {
                        var x = $(c);
                        x.data('index', i);
                        x.attr('data-index', i);
                    });
                } else {
                    window.location.reload();
                }
            }
        );
    }



    function handleSortStop(e, ui) {

        //console.log(ui.item.data('id') + ': ' + ui.item.data('idx') + ' => ' + ui.item.index());
        //console.log(ui.item.data('id') + ': ' + ui.item.data('index') + ' => ' + ui.item.index());

        var children = $(this).children();
        //console.log($.map(children, function (c, i) { var x = $(c); var r = x.data('id') + ': ' + x.data('index') + ' => ' + x.index(); return r; }));
        //console.log($.map(children, function (c, i) { var x = $(c); var r = x.data('id') + ': ' + x.attr('data-index') + ' => ' + x.index(); return r; }));
        /*var data = $.map(children, function (c, i) { var x = $(c); return { favId: x.data('id'), oldOrder: x.data('index'), newOrder: i }; });*/
        /*var data = $.map(children, function (c, i) { var x = $(c); return { 'favId_' + x.data('id') : i }; });*/
        //var data = $.map(children, function (c, i) { var x = $(c); return { i }; });
        //var data = $.map(children, function (c, i) { var x = $(c); var r = x.data('id') + '_' + i; return r; });
        //var res = data.join('|');
        //var data = $.map(children, function (c, i) { var x = $(c); return { favId: x.data('id'), newOrder: i }; });
        //var res = JSON.stringify(data);

        //console.log("handleSortStop");
        //console.log(data);
        e.stopPropagation(); // stops the browser from redirecting.

        //$.post('/Overview/UpdateFavoritesOrder/', { favOrders: res },
        //    function (data) {
        //        if (data == "Success") {
        //            $.each(children, function (i, c) {
        //                var x = $(c);
        //                x.data('index', i);
        //                x.attr('data-index', i);
        //            });
        //        } else {
        //            window.location.reload();
        //        }
        //    }
        //);

        //$.ajax({
        //    contentType: 'application/json; charset=utf-8',
        //    dataType: 'json',
        //    processData: false,
        //    type: 'POST',
        //    url: '/Overview/UpdateFavoritesOrder/',
        //    data: { favOrders: res },
        //    success: function () {
        //        $.each(children, function (i, c) { var x = $(c); x.data('index', i); });
        //    },
        //    failure: function (response) {
        //        showSimpleMessageModal(data);
        //    }
        //}); 

    }

    //function handleSortUpdate(e, ui) {


    //    var data = $.map($(this).children(), function (c, i) { var x = $(c); return { favId: x.data('id'), oldOrder: x.data('index'), newOrder: i }; })
    //    console.log("handleSortUpdate");
    //    console.log(data);
    //    e.stopPropagation(); // stops the browser from redirecting.

    //}

</script>

<style>
    /*.small-list-card
    .home-name-card, 
    .corp-alert-fav,
    .leaf-alert
    {
        cursor: pointer;        
    }*/

    /*    .small-list-card {
        cursor: grab;
    }*/

    /*    .small-list-card:active {
        cursor: grabbing;
    }*/

    /*    .home-inside-data {
        cursor: pointer;
    }*/

    /*    .home-inside-data:active {
        cursor: grabbing;
    }*/

    .boxLocation {
        width: 220px;
        height: 69px;
    }
</style>

<!-- END: FavContainer -->
