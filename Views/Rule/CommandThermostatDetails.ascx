<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<%     List<NotificationRecipient> notificationRecipients = (List<NotificationRecipient>)ViewBag.notificationRecipients;

    NotificationRecipient thermostatRecipient = null;
    int Occupancy = 1;
    ushort Duration = 30;
    if (notificationRecipients != null)
    {
        foreach (NotificationRecipient nr in notificationRecipients.Where(m => { return m.DeviceToNotifyID == Model.SensorID; }))
        {
            if (Model.ApplicationID == 97)
            {
                Thermostat.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out Occupancy, out Duration);
            }

            if (Model.ApplicationID == 125)
            {
                MultiStageThermostat.ParseSerializedRecipientProperties(nr.SerializedRecipientProperties, out Occupancy, out Duration);
            }

            thermostatRecipient = nr;
            break;
        }
    }%>

<!-- Thermo Control Settings -->
<div class="local-alert_container">
    <div class="local-alert_card" id="card_<%=Model.SensorID %>">
        <input type="hidden" name="localAlert_<%=Model.SensorID %>" value="<%=(thermostatRecipient != null).ToString() %>" />

        <div class="local-alert-heading" style="display: <%:thermostatRecipient != null ? "none" : "flex"%>;">
            <a href="Add" title="<%: Html.TranslateTag("Rule/CommandThermostatList|Thermostat Off","Thermostat Off")%>" onclick="toggleThermostatDevice(<%:Model.SensorID %>, true); return false;" class="nrd<%:Model.SensorID %> Thermostat thermo-off" style="display: <%:thermostatRecipient == null ? "flex" : "none"%>;">
                <div class="addme">
                    <%=Html.GetThemedSVG("add") %>
                </div>
            </a>
            <div class="command-icon " style="color: white;"><%=Html.GetThemedSVG("app97") %></div>

            <span style="">&nbsp;<%=Model.SensorName %></span>

        </div>
        <div class="local-alert-heading" style="display: <%:thermostatRecipient != null ? "flex" : "none"%>;">
            <a href="Remove" title="<%: Html.TranslateTag("Rule/CommandThermostatList|Thermostat On","Thermostat On")%>" onclick="toggleThermostatDevice(<%:Model.SensorID %>,  false); return false;" class="alert-head-active nrd<%:Model.SensorID %> Thermostat" style="display: <%:thermostatRecipient != null ? "flex" : "none"%>;">
                <div class="deletesvg">
                    <%=Html.GetThemedSVG("delete") %>
                </div>
            </a>
            <div class="command-icon " style="color: white;"><%=Html.GetThemedSVG("app97") %></div>


            <span style="width: clamp(36rem, 30vw, 100%);">&nbsp;<%=Model.SensorName %></span>
            <div id="caretClose_<%:Model.SensorID %>" class="smooth" style="width: 100%" onclick="toggleThermostatDiv(<%:Model.SensorID %>,true);">
                <div class="white-carrot"><%=Html.GetThemedSVG("caret-down") %></div>
            </div>
            <div id="caretOpen_<%:Model.SensorID %>" style="display: none;" onclick="toggleThermostatDiv(<%:Model.SensorID %>,false);">
                <div class="white-carrot carrot-close"><%=Html.GetThemedSVG("caret-down") %></div>
            </div>
        </div>

        <%-- Occuapancy--%>

        <div class="thermo-alert nrd<%:Model.SensorID %> Thermostat" style="display: <%:thermostatRecipient != null ? "flex" : "none"%>;">


            <div style="width: 68px; justify-content: start; display: flex"><span><%= Html.TranslateTag("Set Mode") %></span></div>

            <div class="switchThermostat">
                <input style="display: none;" value="<%= Occupancy.ToBool() %>" <%=Occupancy.ToBool() ? "checked=\"checked\"" : "" %> type="checkbox" id="occupancy_<%:Model.SensorID %>" name="occupancy_<%:Model.SensorID %>">
                <div class="sliderThermostat round" onclick="setThermostatVal('occupancy_','<%:Model.SensorID %>');">
                    <span class="occupied"><%= Html.TranslateTag("Occupied") %></span>
                    <span class="unoccupied"><%= Html.TranslateTag("Unoccupied") %></span>
                </div>
            </div>



            <%--Duration--%>


            <div class="alert-line nrd<%:Model.SensorID %> Thermostat" style="display: <%:thermostatRecipient != null ? "flex" : "none"%>;">

                <div style="justify-content: center; display: flex; margin-right: 5px;"><span><%= Html.TranslateTag("For") %></span></div>

                <div class="thermo-time">
                    <table>
                        <tr>
                            <td class="thermo-table">
                                <input type="number" min="0" class="shortTimer minutes form-control user-dets" name="occupancyDuration_<%:Model.SensorID %>" value="<%:Duration %>" style="width: 53px" />
                                <input type="hidden" class="shortTimer seconds" value="0" />
                                <%: Html.TranslateTag("Minutes","Minutes")%>                     
                            </td>
                        </tr>
                    </table>
                </div>
            </div>



        </div>
    </div>
</div>

<!-- End Control Settings -->

