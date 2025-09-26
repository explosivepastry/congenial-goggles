<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();%>
<div class="col-12 ps-0 mb-4" style="margin-bottom: 2px;">
    <div class="col-12 view-btns_container mb-4">
        <div class="view-btns deviceView_btn_row shadow-sm rounded">
            <a id="indexLink" href="/Overview/SensorIndex/<%:Model.CSNetID %>#<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-default btn-lg btn-fill"><%=Html.GetThemedSVG("arrowLeft") %></div>
            </a>
            <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Control", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {%>
            <a title="Control" id="tabControl" href="/Overview/Control/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("Control")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("repeat") %><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Control","Control") %></span></div>
            </a>
            <%}%>
            <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Notify", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {%>
            <a title="Data" id="a2" href="Overview/DeviceNotify/<%:Model.SensorID %>" class=" btn-<%:Request.RawUrl.Contains("DeviceNotify")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill">
                <div class="deviceView_btn_row__device"><i class="fa fa-database"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Data","Data") %></span></div>
            </a>
            <%}%>
            <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Terminal", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {%>
            <a title="Terminal" id="tabTerminal" href="Overview/SensorTerminal/<%:Model.SensorID %>" class=" btn-<%:Request.RawUrl.Contains("SensorTerminal")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill">
                <div class="deviceView_btn_row__device"><i class="fa fa-terminal"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Terminal","Terminal") %></span></div>
            </a>
            <%}%>
            <%if (MonnitSession.CustomerCan("Sensor_View_Chart"))
                { %>
            <a title="Details" id="tabChart" href="/Overview/SensorChart/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class=" btn-<%:Request.RawUrl.Contains("SensorChart")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill "><%=Html.GetThemedSVG("details") %><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Details","Details") %></span></div>
            </a>
            <% } %>
            <%if (MonnitSession.CustomerCan("Sensor_View_History"))
                { %>
            <a title="Readings" id="tabHistory" href="/Overview/SensorHome/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorHome")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("list") %><span class="extra"><%: Html.TranslateTag("Readings","Readings") %></span></div>
            </a>
            <% } %>
            <%if (MonnitApplicationBase.HasSensorFile(Model) && MonnitSession.CustomerCan("Sensor_View_History"))
                {%>
            <a title="File List" id="tabFile" href="/Overview/SensorFileList/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorFileList")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("page") %><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|File List","File List") %></span></div>
            </a>
            <%}%>
            <%if (MonnitSession.CustomerCan("Sensor_View_Notifications"))
                { %>
            <a title="Rules" id="tabNotification" href="/Overview/SensorNotification/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorNotification")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill">
                    <%=Html.GetThemedSVG("rules") %>
                    <span class="extra"><%: Html.TranslateTag("Rules") %></span>
                </div>
            </a>
            <% } %>
            <%if (MonnitSession.CustomerCan("Sensor_Edit"))
                { %>
            <a title="Settings" id="tabEdit" href="/Overview/SensorEdit/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class=" btn-<%:(Request.RawUrl.Contains("SensorEdit") || Request.RawUrl.Contains("InterfaceEdit"))? "active-fill" : "secondaryToggle" %> btn-lg btn-fill"><%=Html.GetThemedSVG("settings") %><span class="extra"><%: Html.TranslateTag("Settings","Settings") %></span></div>
            </a>
            <% } %>
            <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Calibrate", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {%>
            <%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
                { %>
            <%if (MonnitSession.CustomerCan("Sensor_Calibrate"))
                { %>
            <a title="Calibrate" id="tabCalibrate" href="/Overview/SensorCalibrate/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class=" btn-<%:Request.RawUrl.Contains("SensorCalibrate")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill"><i class="fa fa-wrench"></i><span class="extra"><%: Html.TranslateTag("Calibrate","Calibrate") %></span></div>
            </a>
            <%}%>
            <%}
                else // Sensor Certificate
                {%>
            <a title="Certificate" id="tabCertificate" href="/Overview/SensorCertificate/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class=" btn-<%:Request.RawUrl.Contains("SensorCertificate")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill">
                    <svg xmlns="http://www.w3.org/2000/svg" width="16" height="20" viewBox="0 0 16 24">
                        <path id="iconmonstr-certificate-6" d="M11.25,9.541,9,7.359l.929-.929L11.25,7.683l2.821-2.892.929.93Zm7.676-3.819c-.482,1.41-.484,1.139,0,2.555A1.38,1.38,0,0,1,19,8.722a1.411,1.411,0,0,1-.615,1.156c-1.256.87-1.09.651-1.562,2.067a1.481,1.481,0,0,1-1.415.99h0c-1.549-.005-1.28-.088-2.528.789a1.532,1.532,0,0,1-1.753,0c-1.249-.878-.98-.794-2.528-.789h0a1.478,1.478,0,0,1-1.413-.99c-.473-1.417-.311-1.2-1.562-2.067A1.41,1.41,0,0,1,5,8.722a1.369,1.369,0,0,1,.074-.444c.483-1.411.484-1.139,0-2.555A1.38,1.38,0,0,1,5,5.278a1.411,1.411,0,0,1,.616-1.157C6.867,3.253,6.7,3.473,7.178,2.054a1.479,1.479,0,0,1,1.413-.99h0c1.545,0,1.271.1,2.528-.79a1.539,1.539,0,0,1,1.753,0c1.248.878.98.8,2.528.79h0a1.48,1.48,0,0,1,1.415.99c.473,1.416.307,1.2,1.562,2.067A1.409,1.409,0,0,1,19,5.277a1.375,1.375,0,0,1-.074.445ZM16.75,7A4.75,4.75,0,1,0,12,11.75,4.75,4.75,0,0,0,16.75,7ZM9.365,14.931a7.017,7.017,0,0,1-1.873-.213A11.949,11.949,0,0,1,4,22.31a5.093,5.093,0,0,0,2.833-.56A7.612,7.612,0,0,1,7.6,24a15.825,15.825,0,0,0,3.734-8.071A4.341,4.341,0,0,1,9.365,14.931Zm5.27,0a4.359,4.359,0,0,1-1.968,1A15.856,15.856,0,0,0,16.4,24a7.556,7.556,0,0,1,.767-2.25A5.1,5.1,0,0,0,20,22.31a11.974,11.974,0,0,1-3.479-7.595A6.99,6.99,0,0,1,14.635,14.931Z" transform="translate(-4)" class="<%:Request.RawUrl.Contains("SensorCertificate")? "icon-fill" : "icon-fill-grey" %>" />
                    </svg>
                    <span class="extra" style="padding-top: 10px;">
                        <%: Html.TranslateTag("Overview/SensorLink|Certificate","Certificate") %>
                    </span>
                </div>
            </a>
            <%}%>
            <%}%>
            <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("SensorEdit\\ApplicationCustomization\\app_{0}\\Scale", Model.ApplicationID.ToString("D3")), "Sensor", MonnitSession.CurrentTheme.Theme))
                {%>
            <%if (MonnitSession.CustomerCan("Sensor_Edit"))
                { %>
            <a title="Scale" id="tabScale" href="/Overview/SensorScale/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorScale")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill"><i class="fa fa-balance-scale"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Scale","Scale") %></span></div>
            </a>
            <%} %>
            <%} %>
            <%if (MonnitViewEngine.CheckPartialViewExists(ViewContext, string.Format("EquipmentInfo"), "Sensor", MonnitSession.CurrentTheme.Theme))
                {%>
            <a title="Equipment" id="tabEquipment" href="/Overview/SensorEquipmentInfo/<%:Model.SensorID %>" class="deviceView_btn_row__device">
                <div class="btn-<%:Request.RawUrl.Contains("SensorEquipmentInfo")? "active-fill" : "secondaryToggle" %> btn-lg btn-fill"><i class="fa fa-wrench"></i><span class="extra"><%: Html.TranslateTag("Overview/SensorLink|Equipment","Equipment") %></span></div>
            </a>
            <%}%>
        </div>
    </div>
    <div class="col-12 device_detailsRow">
        <div class="col-md-6 col-12 ps-0 device_detailsRow__card">
            <div class="x_panel shadow-sm rounded">
                <div class="card_container__top ">
                    <div class="card_container__top__title">
                        <div title="<%=Model.ApplicationID %>" class="hidden-xs">
                            <%=Html.GetThemedSVG("sensor") %>
                            <%: Html.TranslateTag("Sensor","Sensor")%>:
                            &nbsp;
                        </div>
                        <span style="font-size: 14px; font-weight: 500;"><%= Model.SensorName %></span>

                    </div>
                </div>
                <div class="x_content">
                    <div class="card__container__body">
                        <div class="col-12 card_container__body__content">
                            <div class="col-12" style="display: flex; justify-content: space-between; align-items: flex-start;">
                                <div class="" style="display: flex; flex-direction: column;">
                                    <%if (Model.LastCommunicationDate.AddMinutes(Model.MinimumCommunicationFrequency) > DateTime.UtcNow)
                                        {
                                            if (Model.LastDataMessage != null)
                                            {
                                                if (!Model.IsPoESensor)
                                                {
                                                    if (!Model.IsWiFiSensor)
                                                    { %>
                                    <div style="dfac">
                                        <b class="hidden-xs"><%: Html.TranslateTag("Gateway", "Gateway")%>:</b>
                                        <a style="color: #007FEB;" href="/Overview/GatewayHome/<%:Model.LastDataMessage.GatewayID%>">
                                            <%:Model.LastDataMessage.GatewayID > 0 ? Model.LastDataMessage.GatewayID.ToString() : ""%>
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
                                    }
                                    else
                                    {
                                        Gateway gate = Gateway.Load(Model.LastDataMessage.GatewayID);%>
                                    <b class="hidden-xs">MacAddress:  </b><%: gate.MacAddress%>
                                    <%}
                                            }
                                        }

                                    }%>
                                    <div class="dfac">
                                        <b class="hidden-xs"><%: Html.TranslateTag("Network","Network")%>:</b>
                                        <a style="color: #007FEB;" href="/Network/Edit/<%=Model.CSNetID %>">&nbsp;<%= Model.Network == null ? "" : Model.Network.Name.ToStringSafe() %></a>
                                    </div>

                                </div>
                                <div class="dfjcsbac">
                                    <div id="fourth_element_to_target">
                                        <div class="dfac">


                                            <%if (new Version(Model.FirmwareVersion) >= new Version("25.45.0.0") || Model.SensorPrintActive)
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



                                            <%if (Model.LastDataMessage != null)
                                                {
                                                    if (!Model.IsPoESensor)
                                                    {
                                                        int Percent = DataMessage.GetSignalStrengthPercent(Model.GenerationType, Model.SensorTypeID, Model.LastDataMessage.SignalStrength);
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
                                            <div class="card-signal-icon icon icon-signal<%:signal %>"></div>
                                            <%}
                                            else
                                            {%>
                                            <div class="card-signal-icon icon icon-ethernet-b"></div>
                                            <%} %>
                                            <%} %>
                                        </div>
                                    </div>
                                    <div style="min-width: 70px;">
                                        <div style="text-align: center;" id="fifth_element_to_target">
                                            <%DataMessage message = Model.LastDataMessage;
                                                string battLevel = "";
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

                                                    if (Model.PowerSourceID == 3 || message.Voltage == 0)
                                                    {
                                                        battType = "-line";
                                                        battLevel = "";
                                                    }
                                                    else if (Model.PowerSourceID == 4)
                                                    {
                                                        battType = "-volt";
                                                        battLevel = "";
                                                    }
                                                    else if (Model.PowerSourceID == 1 || Model.PowerSourceID == 14)
                                                        battType = "-cc";
                                                    else if (Model.PowerSourceID == 2 || Model.PowerSourceID == 8 || Model.PowerSourceID == 10 || Model.PowerSourceID == 13 || Model.PowerSourceID == 15 || Model.PowerSourceID == 17 || Model.PowerSourceID == 19)
                                                        battType = "-aa";
                                                    else if (Model.PowerSourceID == 6 || Model.PowerSourceID == 7 || Model.PowerSourceID == 9 || Model.PowerSourceID == 16 || Model.PowerSourceID == 18)
                                                        battType = "-ss";
                                                    else
                                                        battType = "-gateway";
                                                }%>
                                            <div class="dfjcsbac">
                                                <div class="card-battery-icon col-xs-6 battIcon  icon icon-battery<%:battType %><%:battLevel %><%:battModifier %>" title="Battery: <%=message == null ? "" : message.Battery.ToString()  %>%, Voltage: <%=message == null ? "" : message.Voltage.ToStringSafe() %> V">


                                                    <%:(battType == "volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", message.Voltage) : "" %>
                                                </div>
                                                <%if (Model.isPaused())
                                                    { %>
                                                <div class="powertour-tooltip powertour-hook" id="hook-eight">
                                                    <div title="Paused" class="col-xs-6 pendingEditIcon pausesvg" style="padding-top: 5px;">
                                                        <svg xmlns="http://www.w3.org/2000/svg" width="25" height="25" viewBox="0 0 20 20">
                                                            <path id="ic_pause_circle_filled_24px" d="M12,2A10,10,0,1,0,22,12,10,10,0,0,0,12,2ZM11,16H9V8h2Zm4,0H13V8h2Z" transform="translate(-2 -2)" />
                                                        </svg>
                                                    </div>
                                                </div>
                                                <%} %>
                                                <%if (!Model.CanUpdate)
                                                    { %>
                                                <div class="powertour-tooltip powertour-hook" id="hook-eight">
                                                    <div title="Settings Update Pending" class="col-xs-6 pendingEditIcon pendingsvg" style="padding-top: 5px">
                                                        <%=Html.GetThemedSVG("Pending_Update") %>
                                                    </div>
                                                </div>
                                                <%} %>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div>
                                <div id="second_element_to_target">
                                    <div class="divCellCenter holder holder<%:Model.Status.ToString() %>">
                                        <div class="eventIcon_container">
                                            <div class="sensor eventIcon eventIconStatus sensorIcon sensorStatus<%:Model.Status.ToString() %>">
                                            </div>
                                            <%=Html.GetThemedSVG("app" + Model.ApplicationID) %>
                                        </div>
                                    </div>
                                </div>
                                <div>
                                    <div class="powertour-hook" id="hook-four">
                                        <div class="glance-text" id="third_element_to_target">
                                            <%if (Model.LastDataMessage != null)
                                                { %>
                                            <div class="glance-reading" style="padding-left: 5px;"><%= Model.LastDataMessage.DisplayData%></div>
                                            <%} %>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 1em">
                                <div class="dfac" style="background-color: white; flex-wrap: wrap;">
                                    <div class="glance-lastDate" style="margin-right: 5px;"><%: Html.TranslateTag("Last Message","Last Message") %> : <%: Model.LastDataMessage == null ?   Html.TranslateTag("Unavailable","Unavailable") : (Model.LastCommunicationDate.ToElapsedMinutes() > 120 ? Model.LastCommunicationDate.OVToLocalDateTimeShort() : (Model.LastCommunicationDate.ToElapsedMinutes()  < 0 ? Html.TranslateTag("Unavailable","Unavailable") :  Model.LastCommunicationDate.ToElapsedMinutes().ToString() + " " + Html.TranslateTag("Minutes ago","Minutes ago")) ) %></div>
                                    <%: Html.TranslateTag("Overview/SensorHome|Next Check-In","Next Check-in") %>:
                                    <% if (Model.LastCommunicationDate < DateTime.UtcNow.AddMinutes(5) && Model.NextCommunicationDate > DateTime.UtcNow)
                                        {
                                            if (Model.NextCommunicationDate != DateTime.MinValue)
                                            { %>
                                    <span style="margin-right: 5px;" class="title2"><%: Model.NextCommunicationDate.OVToLocalDateShort()%></span>
                                    <span class="title2"><%: Model.NextCommunicationDate.OVToLocalTimeShort()%></span>
                                    <%}
                                   else
                                   {%>  <span class="title2"><%: Html.TranslateTag("Unavailable","Unavailable") %></span> <%}
                                               }
                                               else
                                               { %>
                                    <span class="title2"><%: Html.TranslateTag("Unavailable","Unavailable") %></span>
                                    <% } %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%if (!MonnitSession.CurrentCustomer.Account.HideData)
            { %>
        <div class="col-md-6 col-12 device_detailsRow__card <%:Request.Path.StartsWith("/Overview/SensorChart") ? " " : "disp-none" %> detailsReadings_card">
            <div class="x_panel device_detailsRow__card__inner">
                <div class="x_title x_card_title">
                    <i class="fa fa-list-ul main-page-icon-color"></i>
                    &nbsp;
                    <%: Html.TranslateTag("Readings", "Readings")%>
                </div>
                <div class="x_body verticalScroll" style="max-height: 160px; overflow-y: scroll;">
                    <%if (Request.Path.StartsWith("/Overview/SensorChart"))
                        {%>

                    <%Html.RenderPartial("SensorHistoryListSmall", Model); %>

                    <%} %>
                </div>
            </div>
        </div>
        <%} %>
    </div>
</div>

<script>
    $('.btn-secondaryToggle').hover(
        function () { $(this).addClass('active-hover-fill') },
        function () { $(this).removeClass('active-hover-fill') }
    )
</script>
