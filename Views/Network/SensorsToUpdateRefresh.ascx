<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Sensor>>" %>

<%foreach (Sensor sens in Model)
    {
        string battType = "";

        int battery = 0;
        if (sens.LastDataMessage != null)
        {
            battery = sens.LastDataMessage.Battery;

            if (sens.PowerSourceID == 1 || sens.PowerSourceID == 14)
                battType = "-cc";
            else if (sens.PowerSourceID == 2 || sens.PowerSourceID == 8 || sens.PowerSourceID == 10 || sens.PowerSourceID == 13 || sens.PowerSourceID == 15 || sens.PowerSourceID == 17 || sens.PowerSourceID == 19)
                battType = "-aa";
            else if (sens.PowerSourceID == 6 || sens.PowerSourceID == 7 || sens.PowerSourceID == 9 || sens.PowerSourceID == 16 || sens.PowerSourceID == 18)
                battType = "-ss";
            else
                battType = "-gateway";
        }

        //string Warning = "";
        List<string> Warnings = new List<string>();
        int Percent = 0;

        %>

<a class="turn-on-check-card" style="width:22rem; flex-direction:column" onclick="toggleSensor(<%:sens.SensorID%>,<%=sens.ApplicationID %>);">
    <div id="card-wrapper-w-warn">
      <div class="sensor-icon"   title="<%=sens.MonnitApplication.ApplicationName %>">
       <%=Html.GetThemedSVG("app" + sens.ApplicationID) %>
            </div>

            <div class="check-card-name" >
                <span  title="<%=sens.SKU %>"><%:sens.SensorName.Length >= 33 ? sens.SensorName.Substring(0, 33).Insert(33, "...") : sens.SensorName%></span>
                <br />
                <span title="<%:Html.TranslateTag("Current Version", "Current Version")%>: <%=sens.FirmwareVersion %>"><%:Html.TranslateTag("Update To", "Update To")%>: <%=ViewBag.LatestVersions[sens.SKU] %></span>

                <%if (sens.LastDataMessage != null)
                    {
                        Percent = DataMessage.GetSignalStrengthPercent(sens.GenerationType, sens.SensorTypeID, sens.LastDataMessage.SignalStrength);
                        if (battery < 50)
                        {
                            //Warning = Html.TranslateTag("Network/SensorToUpdate|BatteryWarning", "You may want to replace your batteries before updating firmware on this device");
                            Warnings.Add(Html.TranslateTag("Network/SensorToUpdate|BatteryWarning", "You may want to replace your batteries before updating firmware on this device"));
                        }
                        if (Percent < 70)
                        {
                            //if (!string.IsNullOrEmpty(Warning))
                            //{
                            //    Warning += "\r\n";
                            //}
                            //Warning += Html.TranslateTag("Network/SensorToUpdate|SignalWarning", "Improve device signal strength for reliable firmware updates");
                            Warnings.Add(Html.TranslateTag("Network/SensorToUpdate|SignalWarning", "Improve device signal strength for reliable firmware updates"));
                        }

                    } %>

            </div>

            <div class="check-card-icon d-flex" style="align-items:center; margin-left:auto;">
                    <% MvcHtmlString SignalIcon = new MvcHtmlString("");
                        if (sens.LastDataMessage != null)
                        {
                            if (sens.IsPoESensor)
                            {%>

                <div class="icon icon-ethernet-b"></div>
                <%}
                    else
                    {
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
                    }  %>
                <% }%>
                <div  title="<%: Html.TranslateTag("Signal","Signal")%>": <%=Percent %>% <%=Percent < 70 ? Html.TranslateTag("SensorOTA|Improve device signal strength for reliable firmware updates.") : "" %>" >
                   <%=SignalIcon %>
                    </div>
             <%---------------------------- 
                          Battery Icon🔻🔋
                 ----------------------------%>

                              <%   MvcHtmlString PowerIcon = new MvcHtmlString("");
                                  if (sens.LastDataMessage != null)
                                  {
                                      if (sens.PowerSourceID == 3 || sens.LastDataMessage.Voltage == 0)
                                      {
                                          PowerIcon = Html.GetThemedSVG("plug");
                                      }
                                      else if (sens.PowerSourceID == 4)
                                      {
                        %><div style='font-size: 25px; color: #2d4780;'><%:sens.LastDataMessage.Voltage %> volts</div>
                        <div>&nbsp;</div>
                        <%
                                }
                                else
                                {
                                    if (sens.LastDataMessage.Battery <= 0)
                                        PowerIcon = Html.GetThemedSVG("bat-dead");
                                    else if (sens.LastDataMessage.Battery <= 10)
                                        PowerIcon = Html.GetThemedSVG("bat-low");
                                    else if (sens.LastDataMessage.Battery <= 25)
                                        PowerIcon = Html.GetThemedSVG("bat-low");
                                    else if (sens.LastDataMessage.Battery <= 50)
                                        PowerIcon = Html.GetThemedSVG("bat-half");
                                    else if (sens.LastDataMessage.Battery <= 75)
                                        PowerIcon = Html.GetThemedSVG("bat-full-ish");
                                    else
                                        PowerIcon = Html.GetThemedSVG("bat-ful");
                                }

                            }%>
                <div  title="<%: Html.TranslateTag("Battery","Battery")%>": <%=battery %>% <%=battery < 50 ? Html.TranslateTag("SensorOTA|You may want to replace your batteries before updating firmware on this device."): "" %>" >
                        <%=PowerIcon %>
                        <%:(battType == "volt") ? string.Format("<div style='font-size:25px; color:#2d4780;'>{0} volts</div><div>&nbsp;</div>", sens.LastDataMessage.Voltage) : "" %>
                 </div>

            </div>
          
                <div class="check-network-check ListBorder ListBorderNotActive notiSensor<%:sens.SensorID%>">
                <%=Html.GetThemedSVG("circle-check") %>
                </div>
                <input hidden class="updateChk" type="checkbox" name="<%=sens.SensorID %>" id="update_<%=sens.SensorID %>_<%=sens.ApplicationID %>" />
        </div>
<% foreach (string warning in Warnings)
    { %>
    <span class="check-card-icon color-help" title="<%= warning %>">
        <%= Html.GetThemedSVG("alert") %>
       &nbsp;<%= warning %>
    </span>
<% } %>

 </a>
<%} %>

<style type="text/css">
    .color-help {
        fill: var(--help-highlight-color);
        border: solid 1px var(--help-highlight-color);
        border-radius: 0.5rem;
        padding: 0.25rem;
        margin:0.5rem;
    }

        .color-help svg {
            width: 20px;
            height: 20px;
        }

    #card-wrapper-w-warn {
        display: flex;
        width: 100%;
        height: 100%;
        align-items: center;
    }
</style>

<script type="text/javascript">
    $('#filteredSensors').html('<%:ViewBag.FilteredSensors%>');
    $('#totalSensors').html('<%:ViewBag.TotalSensors%>');
</script>

