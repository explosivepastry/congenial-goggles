<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<iMonnit.Models.SensorGroupSensorModel>>" %>

<%			
    CSNet network = CSNet.Load(MonnitSession.SensorListFilters.CSNetID);
    string networkname = (network == null) ? "All Networks" : network.Name;

    foreach (var item in Model)
    {
        DataMessage message = item.DataMessage;
%>

<div style="height: 80px;" class="gridPanel shadow-sm rounded eventsList back<%:item.Sensor.Status.ToString() %>" id="SensorHome<%: item.Sensor.SensorID %>">
    <div style="height: inherit; width: 100%;">
        <div style="height: inherit;" class="viewSensorDetails eventsList__tr innerCard-holder">
            <a style="height: inherit; width: 100%; display: flex;"
                href="/Overview/SensorChart/<%=item.Sensor.SensorID %>">
                <div class="innerCard-holder__icon">
                    <div class="divCellCenter holder holderInactive" style="height: 100%; display: flex; flex-direction: column; justify-content: space-around; align-items: center;">
                        <div style="min-width: 50px; padding: 1px; width: 100%; margin-left: 4px; box-shadow: none; font-size: 11px; height: auto;"
                            class="sensor sensorIcon sensorStatus<%:item.Sensor.Status.ToString() %> eventIconStatus eventIcon font<%:item.Sensor.Status.ToString() %>">
                            <%:item.Sensor.Status.ToString() %>
                        </div>

                        <div style="width: 30px; height: 30px; margin-left: 5px;">
                            <%=Html.GetThemedSVG("app" + item.Sensor.ApplicationID) %>
                        </div>
                    </div>
                </div>

                <div class="col-7 dffdc" style="align-items: flex-start; justify-content: space-evenly; height: inherit;">
                    <div class=" noPad">
                        <div class="glance-text">
                            <div class="glance-name"><%= item.Sensor.SensorName%></div>
                        </div>
                    </div>

                    <div class=" noPad">
                        <div class="glance-text">
                            <div class="glance-reading" style="max-height: 12px; overflow: hidden;" title="<%: item.Sensor.LastDataMessage == null ? "" : item.Sensor.LastDataMessage.DisplayData%>"><%: item.Sensor.LastDataMessage == null ? "" : item.Sensor.LastDataMessage.DisplayData%></div>
                        </div>
                    </div>

                    <div class=" noPad" style="display: grid; grid-template-columns: 9fr 1fr;">
                        <div class="glance-text">
                            <div class="glance-lastDate"><%: Html.TranslateTag("Last Message","") %>  <%: item.Sensor.LastDataMessage == null ?  Html.TranslateTag("Unavailable","Unavailable") : (item.Sensor.LastCommunicationDate.ToElapsedMinutes() > 120 ? item.Sensor.LastCommunicationDate.OVToLocalDateTimeShort() : item.Sensor.LastCommunicationDate.ToElapsedMinutes().ToString()  + " " + Html.TranslateTag("Minutes ago","Minutes ago") ) %></div>
                        </div>
                        <span <%=!item.Sensor.CanUpdate ? "style='display:flex; justify-content:center; align-items:center;'" : "style='display:none;'"%> title="Settings Update Pending" class="showPendingIcon pendingEditList pendingsvg"><%=Html.GetThemedSVG("Pending_Update")%></span>
                    </div>
                </div>
            </a>

            <%string battLevel = "";
                string battType = "";
                string battModifier = "";
                if (message != null)
                {
                    if (message.Battery <= 0)
                    {
                        battLevel = "-0";
                        battModifier = " batteryCritical batteryLow";
                    }
                    else if (message.Battery <= 10)
                    {
                        battLevel = "-1";
                        battModifier = " batteryCritical batteryLow";
                    }
                    else if (message.Battery <= 25)
                    {
                        battLevel = "-2";
                    }
                    else if (message.Battery <= 50)
                        battLevel = "-3";
                    else if (message.Battery <= 75)
                        battLevel = "-4";
                    else
                        battLevel = "-5";

                    if (item.Sensor.PowerSourceID == 3 || message.Voltage == 0)
                    {
                        battType = "-line";
                        battLevel = "";
                    }
                    else if (item.Sensor.PowerSourceID == 4)
                    {
                        battType = "-volt";
                        battLevel = "";
                    }
                    else if (item.Sensor.PowerSourceID == 1 || item.Sensor.PowerSourceID == 14)
                        battType = "-cc";
                    else if (item.Sensor.PowerSourceID == 2 || item.Sensor.PowerSourceID == 8 || item.Sensor.PowerSourceID == 10 || item.Sensor.PowerSourceID == 13 || item.Sensor.PowerSourceID == 15 || item.Sensor.PowerSourceID == 17 || item.Sensor.PowerSourceID == 19)
                        battType = "-aa";
                    else if (item.Sensor.PowerSourceID == 6 || item.Sensor.PowerSourceID == 7 || item.Sensor.PowerSourceID == 9 || item.Sensor.PowerSourceID == 16 || item.Sensor.PowerSourceID == 18)
                        battType = "-ss";
                    else
                        battType = "-gateway";

                }%>

            <div class="col-4 AlignTop" style="display: flex; flex-direction: column; align-items: flex-end; width: 0%;">
                <div class="dfjcfe dfac gatewaySignal sigIcon" style="height: 50%;">
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
                    
                    <%if (message != null)
                        {
                            if (item.Sensor.IsPoESensor)
                            {%>
                    <div class="icon icon-ethernet-b"></div>
                    <%}
                        else
                        {
                            int Percent = DataMessage.GetSignalStrengthPercent(item.Sensor.GenerationType, item.Sensor.SensorTypeID, message.SignalStrength);
                            string signal = "";

                            if (Percent <= 0)
                                signal = "-0";
                            else if (Percent <= 10)
                                signal = "-1";
                            else if (Percent <= 25)
                                signal = "-2";
                            else if (Percent <= 50)
                                signal = "-3";
                            else if (Percent <= 75)
                                signal = "-4";
                            else
                                signal = "-5";
                    %>
                    <div class="icon iconSignal icon-signal<%:signal %>" style="font-size: 0.7em; margin-top: 5px;"></div>
                    <%}

                    }%>
                    <div style="margin-right: 3px; margin-top: 5px;" class="battIcon icon icon-battery<%:battType %><%:battLevel %><%:battModifier %>">
                        <%:(battType == "volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", message.Voltage) : "" %>
                    </div>
                </div>
                <div style="height: 50%" class="dfjcfe">
                    <div class="dropleft">
                        <div style="padding: 5px 15px;" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">
                            <%=Html.GetThemedSVG("menu") %>
                        </div>
                        <ul class="dropdown-menu py-0" aria-labelledby="dropdownMenuButton" >
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Overview/SensorChart/<%=item.Sensor.SensorID %>">
                                    <span><%: Html.TranslateTag("Details","Details")%></span>
                                    <%=Html.GetThemedSVG("details") %>
                                </a>
                            </li>
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Overview/SensorHome/<%=item.Sensor.SensorID %>">
                                    <span><%: Html.TranslateTag("Readings","Readings")%></span>
                                    <%=Html.GetThemedSVG("list") %>
                                </a>
                            </li>
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Overview/SensorNotification/<%=item.Sensor.SensorID %>">
                                    <span><%: Html.TranslateTag("Rules","Rules")%></span>
                                    <%=Html.GetThemedSVG("rules") %>
                                </a>
                            </li>
                            <%if (MonnitSession.CustomerCan("Sensor_Edit"))
                                { %>
                            <li>
                                <a class="dropdown-item menu_dropdown_item"
                                    href="/Overview/SensorEdit/<%=item.Sensor.SensorID %>">
                                    <span><%: Html.TranslateTag("Settings", "Settings")%></span>
                                    <%=Html.GetThemedSVG("settings") %>
                                </a>
                            </li>
                            <%} %>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<% } %>


<script>

    $(document).ready(function () {
        $('#networkSelect').change(function () {
            window.location.href = $(this).val();
        });
        $('#filterdSensors').html('<%:Model.Count%>');
        $('#totalSensors').html('<%:ViewBag.TotalSensors%>');
        $('#networkname').html('<%:networkname.Replace("&#39;","'")%>');
    });
    var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';


</script>
