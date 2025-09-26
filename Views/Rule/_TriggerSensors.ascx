<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<SensorNoficationModel>>" %>

<%="" %>
<%
    List<SensorNoficationModel> sensorlist = (List<SensorNoficationModel>)Model;

    string notiBadgeValue = string.Empty;
    long notificationID = ViewBag.NotiID;
    bool isApplicationTypeRule = false;

    if (notificationID != long.MinValue)
    {
        Notification noti = Notification.Load(notificationID);
        notiBadgeValue = noti.Scale;
        isApplicationTypeRule = noti.NotificationClass == eNotificationClass.Application;
    }
%>


<%
    Notification notiWhole = Notification.Load(notificationID);
    if (ViewBag.AdvancedNoti != null) { }
    AdvancedNotification Advanced = ViewBag.AdvancedNoti;
    string badgeValue = "";
    var sortedModelBySelected = Model.OrderByDescending(item => item.Notify).ToList();
    foreach (SensorNoficationModel item in sortedModelBySelected)
    {
        if (isApplicationTypeRule)
            badgeValue = item.Sensor.ApplicationID == 22 ? Monnit.ZeroToTwentyMilliamp.ScaleBadgeText(item.Sensor.SensorID) : "";
%>
<a id="sensorCardId_<%:item.Sensor.SensorID%>" <%:item.Notify ? "Selected":""%> class="activate-card-holder">
    <div class=" active-card-contents" style="height: 100%;">
        <%if (isApplicationTypeRule)
            { %>
        <div title="Make primary sensor" id="coloredCertIconId_<%:item.Sensor.SensorID%>" class="<%:item.Sensor.SensorID == notiWhole.SensorID ? "coloredCertIcon": "unselectedCertIcon"%> fullHeight" onclick="togglePrimarySensor('coloredCertIconId_<%:item.Sensor.SensorID%>', <%:item.DatumIndex%>)"><%=Html.GetThemedSVG("certificate") %></div>
        <%} %>
        <div class=" activate-icon 1111">
            <div class="icon-color iconMap" style="width: 30px; margin-left: 5px; margin-right: 10px;">
                <%=Html.GetThemedSVG("app" + item.Sensor.ApplicationID) %>
            </div>
        </div>
        <div class="activate-name">
            <strong><%:System.Web.HttpUtility.HtmlDecode(item.Sensor.SensorName) %></strong>

            <%
                string name = item.Sensor.GetDatumName(item.DatumIndex);
                int test = item.DatumIndex;
                if (item.NotificationClass == eNotificationClass.Application || (item.NotificationClass == eNotificationClass.Advanced && Advanced != null && Advanced.UseDatums))
                {
                    if (name != item.Sensor.SensorName)
                    {
            %>

            <span style="font-size: 0.8em;"><%: name %></span>
            <span style="font-size: 0.8em;"><%: test %></span>
            <%}
                }
                else
                {%>

            <%} %>

            <%if (!string.IsNullOrEmpty(badgeValue))
                { %>
            <div title="<%=badgeValue == notiBadgeValue ? "Matches Scale" : "Does Not Match Scale" %>" class="badge <%= badgeValue == notiBadgeValue ? "badgeSelected" : ""%>" id="sensorBadge">
                <%= badgeValue%>
            </div>
            <%} %>
        </div>
        <div title="Apply rule to sensor" class="ListBorder<%:item.Notify ? "Active":"NotActive" %> notiSensor<%:item.Sensor.SensorID%>_<%:item.DatumIndex%> circle__status gridPanel-sensor" onclick="toggleSensor(<%:item.Sensor.SensorID%>, <%:item.DatumIndex %>);">
            <%=Html.GetThemedSVG("circle-check") %>
        </div>
    </div>
</a>
<%} %>


<script type="text/javascript">

    var refreshRequest = null;
    $(document).ready(function () {

        $('#filterdSensors').html('<%:sensorlist.Count%>');
        $('#totalSensors').html('<%:ViewBag.TriggerSensors.Count%>');

    });

    var togglePrimarySensor = (idOfCardElement, datumIndex) => {
        var cardToUpdates = document.querySelectorAll(`#${idOfCardElement}`);
        var previousPrimarySensors = document.querySelectorAll(".coloredCertIcon");
        var sensorIdToMakePrimary = idOfCardElement.replace("coloredCertIconId_", "");
        var ruleIdToUpdate = "<%= notificationID %>";
        var url = "/Rule/RulePrimarySensorEdit/";
        $.post(url, { sensorId: sensorIdToMakePrimary, ruleId: ruleIdToUpdate, datumIndex: datumIndex })
            .done(function (data) {
                if (typeof data == 'object') {
                    toastBuilder("Primary Sensor Updated!", "Success");
                    showBaseUnitsMessage("CompareValue", "scale", data);
                    if (previousPrimarySensors) {
                        previousPrimarySensors.forEach(sensor => {
                            sensor.classList.remove("coloredCertIcon");
                        })
                    }
                    cardToUpdates.forEach(card => {
                        card.classList.add("coloredCertIcon");
                    })
                } else {
                    toastBuilder(data);
                }
            })
            .fail(function (xhr, status, error) {
                console.error("Request failed with status:", status);
                console.error("Error:", error);
                toastBuilder("Oops! Unable to set primary sensor, please refresh your page. If this error continues, contact support.");
            });
    };

</script>

<style>
    .fullHeight {
        height: 100%;
        cursor: pointer;
    }

    .unselectedCertIcon svg {
        fill: grey;
        transition: fill 0.3s ease;
    }

    .coloredCertIcon svg {
        fill: var(--options-icon-color, darkblue);
        transition: fill 0.3s ease;
    }
</style>
