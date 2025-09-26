<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.SensorGroupSensorModel>>" %>

<%="" %>

<%	
    long accID = (Model.Count > 0) ? Model[0].Sensor.AccountID : long.MinValue;
    List<CSNet> networks = iMonnit.Controllers.CSNetController.GetListOfNetworksUserCanSee(accID);
    CSNet network = CSNet.Load(MonnitSession.SensorListFilters.CSNetID);
    string networkname = (network == null) ? "All Networks" : network.Name;




    foreach (var item in Model)
    {
        bool isFavorite = MonnitSession.IsSensorFavorite(item.Sensor.SensorID);

        DataMessage message = item.DataMessage;
        List<Notification> rules = Notification.LoadBySensorID(item.Sensor.SensorID);
        MvcHtmlString WarningIcon = new MvcHtmlString("");
        string WarningLabel = ("");
        string WarningDescription = ("");
        string createRuleUrl = "/Rule/ChooseType/";
        string existingRuleUrl = "/Overview/SensorNotification/" + item.Sensor.SensorID;

        if (item.Sensor.Status == eSensorStatus.Offline)
        {
            WarningIcon = Html.GetThemedSVG("db-wifi-off");
            WarningLabel = "OFFLINE";
            WarningDescription = "Reset or check your wifi/ethernet connection";
        }
        else if (item.Sensor.Status == eSensorStatus.Alert)
        {
            WarningIcon = Html.GetThemedSVG("circle-yellow-alert");
            WarningLabel = "Rule Triggered";

            if (rules != null && rules.Count > 0)
            {
                foreach (Notification rule in rules)
                {
                    WarningDescription += "Rule Triggered: " + rule.Name;
                }
            }
        }
        else if (item.Sensor.Status == eSensorStatus.Warning && item.Sensor.LastDataMessage.Battery < 15)
        {
            WarningIcon = Html.GetThemedSVG("db-low-battery");
            WarningLabel = "Low Battery";
            WarningDescription = "Replace your batteries before lost connection";
        }
        else if (item.Sensor.Status == eSensorStatus.Warning)
        {
            WarningIcon = Html.GetThemedSVG("db-alert");
            WarningLabel = "Hardware Error";
            WarningDescription = "Physically check sensor to make sure device is not damaged";
        }
        else if (item.Sensor.Status == eSensorStatus.OK)
        {
            WarningIcon = Html.GetThemedSVG("circle-check-green");
            WarningLabel = "Good";
            WarningDescription = "Your sensor is working properly";
        }

%>

<div class="sensor_card-w-data" id="SensorHome<%: item.Sensor.SensorID %>">
    <div class="corp-status expandable sensorStatus<%:item.Sensor.Status.ToString() %>" style="height: 100%;" onclick="expandBox(<%: item.Sensor.SensorID %>)" title="<%=WarningDescription %>"></div>

    <%------------------------------------------------------------------   
                                      Expanded Box
     ---------------------------------------------------------------%>
    <div id="box_<%: item.Sensor.SensorID %>" class="help-cover ">
        <div class="warningLabel">
            <div style="width: 25px;"><%=WarningIcon %> </div>
            <p style="margin: 0; font-size: 14px;"><%=WarningLabel %></p>
        </div>

        <div class="rule-container-error">
            <div>
                <p class="sensor_damage"><%=WarningDescription %></p>
            </div>
            <div class="error-rule-contain">
                <%--   If no rules are made for this error--%>
                <%if (rules == null || (rules != null && rules.Count == 0))
                    {%>
                <a href="<%=createRuleUrl %>" class="rule-2b-notified">
                    <%: Html.TranslateTag("Overview/_SensorDetails|Create a rule to be notified","Create a rule to be notified")%>
                    <span class="rule-go-arrow" style="width: 20px;"><%=Html.GetThemedSVG("arrow-right-circle") %></span>
                </a>
                <%} %>

                <%--    if rules are made for this error--%>
                <%if (rules != null && rules.Count > 0)
                    {%>
                <a href="<%=existingRuleUrl %>" class="current-rule">
                    <%: Html.TranslateTag("Overview/_SensorDetails|Current Rules","Current Rules")%>
                    <span class="rule-go-arrow" style="width: 20px;"><%=Html.GetThemedSVG("arrow-right-circle") %></span>
                </a>

                <a href="<%=createRuleUrl %>" class="current-rule">
                    <%: Html.TranslateTag("Overview/_SensorDetails|Create another Rule","Create another Rule")%>
                    <span class="rule-go-arrow" style="width: 20px;"><%=Html.GetThemedSVG("arrow-right-circle") %></span>
                </a>
                <%}%>
            </div>
        </div>
    </div>
    <%----------------------------   End Expand box--%>



    <div style="display: flex;" id="inside_<%: item.Sensor.SensorID %>" class="inside-card ">

        <div style="height: inherit; width: 100%" class="viewSensorDetails <%=item.Sensor.SensorID %> eventsList__tr innerCard-holder">
            <a style="height: inherit; width: 100%; display: flex;" href="/Overview/SensorChart/<%=item.Sensor.SensorID %>">
                <div class="innerCard-holder__icon">
                    <div class="icon-color icon-size">
                        <%=Html.GetThemedSVG("app" + item.Sensor.ApplicationID) %>
                    </div>

                </div>

                <%--iner card holder--%>

                <div class=" dffdc adjustableWidth" style="height: 100%; justify-content: space-around;">
                    <%--     Title--%>
                    <div class=" noPad">
                        <div class="glance-text">
                            <div style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis; display: block;"
                                title="<%= item.Sensor.SensorName%>" class=" sc-title glance-name">
                                <%= item.Sensor.SensorName%>
                            </div>
                        </div>
                    </div>

                    <%--       Message--%>
                    <div class=" noPad" style="max-width: 220px;">
                        <div class="glance-text">
                            <div class="glance-reading" title="<%: item.Sensor.LastDataMessage == null ? "" : item.Sensor.LastDataMessage.DisplayData%>"><%: item.Sensor.LastDataMessage == null ? "" : item.Sensor.LastDataMessage.DisplayData%></div>
                        </div>
                    </div>



                    <div class=" noPad" style="display: grid; grid-template-columns: 9fr 1fr 1fr; align-items: center">
                        <div class="glance-text">
                            <div class="glance-lastDate"><%: Html.TranslateTag("Last Message", "") %>  <%: item.Sensor.LastDataMessage == null ? Html.TranslateTag("Unavailable", "Unavailable") : (item.Sensor.LastCommunicationDate.ToElapsedMinutes() > 120 ? item.Sensor.LastCommunicationDate.OVToLocalDateTimeShort() : item.Sensor.LastCommunicationDate.ToElapsedMinutes().ToString() + " " + Html.TranslateTag("Minutes ago", "Minutes ago")) %></div>
                        </div>
                        <%--glance text--%>
                        <div style="display: flex; gap: .25rem;">
                            <span <%=item.Sensor.isPaused() ? "style='display:flex; justify-content:center; align-items:center;'" : "style='display:none;'"%> title="<%: Html.TranslateTag("Paused","Paused")%>" class="pendingEditList pausesvg">
                                <%=Html.GetThemedSVG("pause") %>
                            </span>
                            <span <%=!item.Sensor.CanUpdate ? "style='display:flex; justify-content:center; align-items:center;'" : "style='display:none;'"%> title="<%: Html.TranslateTag("Overview/_SensorDetails|Settings Update Pending","Settings Update Pending")%>" class="showPendingIcon pendingEditList pendingsvg">
                                <%=Html.GetThemedSVG("Pending_Update")%>
                            </span>
                            <span <%=item.Sensor.TimeOfDayControl > 0 ? "style='display:flex; justify-content:center; align-items:center;'" : "style='display:none;'"%> title="<%: Html.TranslateTag("Has Sensor Schedule","Has Sensor Schedule")%>" class="pendingScheduleList">
                                <%=Html.GetThemedSVG("schedule") %>
                            </span>
                        </div>
                    </div>

                </div>

            </a><%--overview sensor chart--%>



            <div class="dfjcfe" style="height: 70%; align-items: flex-end; gap: 5px;">
                <%if (new Version(item.Sensor.FirmwareVersion) >= new Version("25.45.0.0") || item.Sensor.SensorPrintActive)
                    {
                        if (item.Sensor.SensorPrintActive && message != null)
                        {
                            if (message.IsAuthenticated)
                            {%>
                <%=Html.GetThemedSVG("printCheck") %>
                <%}
                    else
                    {%>
                <%=Html.GetThemedSVG("printFail") %>
                <%}
                        }
                    }%>


                <%--   ---------------------    Signal Icon    -----------%>

                <%  MvcHtmlString SignalIcon = new MvcHtmlString("");
                    if (message != null)
                    {
                        if (item.Sensor.IsPoESensor)
                        {%>
                <div class="ether-icon-dets "><%=Html.GetThemedSVG("ether-icon") %></div>
                <%
                    }
                    else
                    {
                        int Percent = DataMessage.GetSignalStrengthPercent(item.Sensor.GenerationType, item.Sensor.SensorTypeID, message.SignalStrength);

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

                <div class="gatewaySignal" title="Signal Strength: <%:Percent%>%" style="width: 30px; height: 30px;"><%:SignalIcon%></div>
                <%} %>
                <%} %>



                <%--   Battery Icon--%>
                <div class="battIcon" style="margin-top: 5px;" title="Battery: <%=message == null ? "" : message.Battery.ToString()  %>%, Voltage: <%=message == null ? "" : message.Voltage.ToStringSafe() %> V">

                    <% MvcHtmlString PowerIcon = new MvcHtmlString("");
                        if (message != null)
                        {
                            if (item.Sensor.PowerSourceID == 3 || message.Voltage == 0)
                            {
                                PowerIcon = Html.GetThemedSVG("plugsensor1");
                            }
                            else if (item.Sensor.PowerSourceID == 4)
                            {
                    %><div style='font-size: 25px; color: #2d4780;'><%:message.Voltage %> volts</div>
                    <div>&nbsp;</div>
                    <%
                            }
                            else
                            {
                                if (message.Battery <= 0)
                                    PowerIcon = Html.GetThemedSVG("bat-dead");
                                else if (message.Battery <= 10)
                                    PowerIcon = Html.GetThemedSVG("bat-low");
                                else if (message.Battery <= 25)
                                    PowerIcon = Html.GetThemedSVG("bat-low");
                                else if (message.Battery <= 50)
                                    PowerIcon = Html.GetThemedSVG("bat-half");
                                else if (message.Battery <= 75)
                                    PowerIcon = Html.GetThemedSVG("bat-full-ish");
                                else
                                    PowerIcon = Html.GetThemedSVG("bat-ful");
                            }
                        }%>
                    <%=PowerIcon %>
                </div>
            </div>

            <div class=" dfac " style="/*height: 100%; */ max-width: 42px;">
                <div class="dropleft dfac " style="height: 100%;">

                    <div class=" menu-hover menu-fav  dropdown  " data-bs-toggle="dropdown " data-bs-auto-close="true" aria-expanded="false">


                        <div style="width: 100px">
                            <button type="button" data-bs-toggle="dropdown" aria-expanded="false" style="border: none; background: none;">
                                <div id="favoriteItem" class="listOfFav favoriteItem liked" style="display: <%= isFavorite ? "flex" : "none"%>; align-items: start; justify-content: center;" <%=isFavorite ? "data-fav=true" : "data-fav=false"%>>
                                    <div class="listOfFav"><%= Html.GetThemedSVG("heart-beat")  %></div>
                                </div>
                                <%=Html.GetThemedSVG("menu") %>
                            </button>

                            <ul class="dropdown-menu ddm" style="padding: 0;">
                                <%if (MonnitSession.CustomerCan("Sensor_View_History"))
                                    { %><li>
                                        <a class="dropdown-item menu_dropdown_item"
                                            onclick="window.location.href='/Overview/SensorChart/<%=item.Sensor.SensorID %>';">
                                            <span><%: Html.TranslateTag("Details", "Details")%></span>
                                            <%=Html.GetThemedSVG("details") %>
                                        </a>
                                    </li>
                                <%}   /*if  Sensor_View_History*/

                                    if (MonnitSession.CustomerCan("Sensor_View_History"))
                                    { %>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        onclick="window.location.href='/Overview/SensorHome/<%=item.Sensor.SensorID %>';">
                                        <span><%: Html.TranslateTag("Readings","Readings")%></span>
                                        <%=Html.GetThemedSVG("list") %>
                                    </a>
                                </li>
                                <%}    /*if  Sensor_View_History*/

                                    if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
                                    { %>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        onclick="window.location.href='/Overview/SensorNotification/<%=item.Sensor.SensorID %>';">
                                        <span><%: Html.TranslateTag("Rules","Rules")%></span>
                                        <%=Html.GetThemedSVG("rules") %>
                            
                                    </a>
                                </li>
                                <% }   /*if  Sensor_View_Notifications*/

                                    if (MonnitSession.CustomerCan("Sensor_Edit"))
                                    { %>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        onclick="window.location.href='/Overview/SensorEdit/<%=item.Sensor.SensorID %>';">
                                        <span><%: Html.TranslateTag("Settings","Settings")%></span>
                                        <%=Html.GetThemedSVG("settings") %>
                                    </a>
                                </li>
                                <% }   /*if  Sensor_Edit*/

                                    if (MonnitSession.CustomerCan("Network_Edit"))
                                    {
                                        if (networks.Count() > 1)
                                        {%>
                                <li>
                                    <a class="dropdown-item menu_dropdown_item"
                                        onclick="window.location.href='/Setup/AssignDevice/<%=item.Sensor.AccountID %>?networkID=<%=network != null ? network.CSNetID : long.MinValue %>&DeviceToMove=<%=item.Sensor.SensorID %>:IM<%:MonnitUtil.CheckDigit(item.Sensor.SensorID)%>';">
                                        <span><%: Html.TranslateTag("Move","Move")%></span>
                                        <%=Html.GetThemedSVG("network") %>
                                    </a>
                                </li>
                                <%} %>
                                <li>
                                    <hr class="my-0" />
                                    <a class="dropdown-item menu_dropdown_item" id="list" onclick="removeSensor('<%=item.Sensor.SensorID %>'); return false;" title="Delete: <%=item.Sensor.SensorID %>">
                                        <span>
                                            <%: Html.TranslateTag("Delete","Delete")%> 
                                        </span>
                                        <%=Html.GetThemedSVG("delete") %>
                                    </a>
                                </li>
                                <% } %>    <%--networks counts--%>
                            </ul>

                        </div>
                    </div>
                </div>

            </div>




            <%--auto style--%>
        </div>
        <%--view Sensor Details--%>
    </div>
    <%--inherit 100%--%>
</div>



<%--gridpanel--%>
<% } %>   <%-- foreach model--%>

<script type="text/javascript">
    <%= ExtensionMethods.LabelPartialIfDebug("_SensorDetails") %>

    var confirmRemoveSensor = "<%: Html.TranslateTag("Network/DetailList|Are you sure you want to remove this sensor from the network","Are you sure you want to remove this sensor from the network?")%>";
    function removeSensor(item) {
        let values = {};
        values.url = `/Network/RemoveSensor/${item}`;
        values.text = confirmRemoveSensor;
        openConfirm(values)
    }

    $(document).ready(function () {

        $('#filterdSensors').html('<%=Model.Count%>');
        $('#totalSensors').html('<%=ViewBag.TotalSensors%>');
        $('#networkname').html('<%:networkname.Replace("&#39;","'")%>');


    });






    function expandBox(sensorId) {
        var mainInside = document.getElementById("SensorHome" + sensorId);
        var inside = document.getElementById("inside_" + sensorId);
        var box = document.getElementById("box_" + sensorId);
        box.classList.toggle("expand");
        if (box.classList.contains("expand")) {
            mainInside.style.overflow = "hidden";
            inside.style.display = "none";
        } else {
            setTimeout(() => {
                mainInside.style.overflow = "inherit";
            }, 1200);
            inside.style.display = "block";
        }
    }

    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
    $('.dropdown').dropdown()

    $(document).ready(function () {
        $(".listOfFav svg").addClass("liked");
    });

</script>

<style>
    .adjustableWidth {
        width: 196px;
    }

    .listOfFav > .heart-beat {
        width: 12px !important;
    }

    .listOfFav > .heart-beat {
        fill: red;
    }

    .current-rule span > svg path {
        fill: var(--primary-color);
        transition: transform 0.2s ease-in-out;
        cursor: pointer;
    }

    .listOfFav {
        pointer-events: none;
    }

    .sensor_damage {
        font-size: 12px;
        display: flex;
        flex-wrap: wrap;
        line-height: 1.09;
        margin-left: 35px;
        margin-right: 0px;
        margin-bottom: 0px;
        width: 100%;
    }

    #svg_print {
        height: 12px;
        margin-right: 5px;
    }

    #iconmonstr-fingerprint-1_1_ #svg_print {
        fill: #515356;
    }

    #iconmonstr-fingerprint-15 #svg_print {
        fill: #22ae73;
    }

    #iconmonstr-fingerprint-16 #svg_print {
        fill: #ff4d2d;
    }

    .auto-style1 {
        width: 0%;
    }

    .ether-icon-dets > svg {
        width: 21px !important;
    }

    .pendingScheduleList > svg{
        fill:grey !important;
    }

    .sensor_card-w-data {
        min-height: 5rem;
        direction: ltr;
        position: relative;
        display: flex;
        width: 23rem;
        box-shadow: 0 0.125rem 0.25rem rgb(0 0 0 / 18%);
        background: #f5f5f5;
        align-items: center;
        border-radius: 0.3rem;
        margin: 0.5rem;
        padding: 0.2rem;
        transition: all 3s ease-in-out;
        /*   overflow:hidden;*/
    }

    .inside-card {
        color: black;
        align-items: center;
        /* justify-content:center;*/
        height: 100%;
        width: 100%;
    }

        .inside-card > svg {
            fill: #515356;
        }

    .help-cover {
        width: 93%;
        background: white;
        height: 90%;
        position: absolute;
        display: inline;
        z-index: 999;
        top: 3px;
        right: 4px;
        border-radius: 0 5px 5px 0;
        transform: translateX(-90%);
        pointer-events: none;
        opacity: 0;
        /*   transition: transform 1s cubic-bezier(.49,.08,.76,.96), opacity 1s ease-in-out;*/
        transition: transform 1s cubic-bezier(.17,.67,.83,.67), opacity 0.1s ease 1s;
    }

    .corp-status {
        cursor: pointer;
    }

        .corp-status:hover {
            transform: scale(.93);
            box-shadow: rgb(0 0 0 / 21%) 3px 3px 6px 0px inset, rgba(255, 255, 255, 0.5) -3px -3px 6px 1px inset;
        }

    .help-cover.expand {
        transform: translateX(0%);
        opacity: 1;
        transition: transform 1s cubic-bezier(.49,.08,.76,.96), opacity 1.5s ease-in-out;
        /*         transition: transform 1s ease-in-out, opacity 0.5s ease-in-out;*/
    }

        .help-cover.expand a {
            pointer-events: auto;
        }

    .rules-warn-icon > svg {
        width: 18px !important;
        height: 19px !important;
    }

    .rule-go-arrow > svg {
        cursor: pointer !important;
    }

    .menu-fav {
        display: flex;
        justify-content: space-around;
        flex-direction: column;
        height: 100%;
        cursor: pointer;
        padding: 5px 6px 5px 0;
    }

    @media screen and (max-width: 400px) {
        .adjustableWidth {
            width: 180px;
        }

        .sensor_card-w-data {
            margin: 0.5rem 0rem;
            padding: 0.2rem;
        }
    }
</style>
