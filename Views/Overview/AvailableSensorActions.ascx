<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<AvailableNotificationBySensorModel>>" %>
<%
    Sensor sensor = ViewData["Sensor"] as Monnit.Sensor;

    //int DatumIndexFilter = ViewData["DatumIndexFilter"].ToInt();
    //string AssignedFilter = ViewData["AssignedFilter"].ToStringSafe();
    int totalRules = ViewBag.TotalRules;
    int filteredRules = ViewBag.FilteredRules;
%>

<% 	
    foreach (AvailableNotificationBySensorModel item in Model)
    {

        string svgTemp = "";
        switch (item.Notification.NotificationClass)
        {
            case eNotificationClass.Application:
                svgTemp = Html.GetThemedSVG("sensor").ToString();
                break;
            case eNotificationClass.Low_Battery:
                svgTemp = Html.GetThemedSVG("lowBattery").ToString();
                break;
            case eNotificationClass.Advanced:
                svgTemp = Html.GetThemedSVG("gears").ToString();
                break;
            case eNotificationClass.Inactivity:
                svgTemp = Html.GetThemedSVG("hourglass").ToString();
                break;
            case eNotificationClass.Timed:
                svgTemp = Html.GetThemedSVG("clock").ToString();
                break;
        }


        foreach (AvailableNotificationBySensorDetailsModel detail in item.DetailsList)
        {
            //bool ShowDatumFilter = DatumIndexFilter < 0 || DatumIndexFilter == detail.DatumIndex;
            //bool ShowAssignedFilter = string.IsNullOrEmpty(AssignedFilter) || detail.SensorNotificationID > 0 == AssignedFilter.ToBool();
            //
            //if (ShowDatumFilter && ShowAssignedFilter)
            //{
%>


<div class=" small-list-card">
    <div class=" existing-rule-data" style="width: 100%; align-items: center;">
        <div class="svgTemp"><%=svgTemp %></div>
        <div class="triggerDevice__name trigger-name" style="width: 150px; align-items: start;">
            <strong style="font-size: 13px"><%=System.Web.HttpUtility.HtmlDecode(item.Notification.Name) %></strong>
            <%if (item.Notification.NotificationClass == eNotificationClass.Application && sensor != null)
                { %>
            <br />
            <span style="font-size: 13px"><%:sensor.GetDatumName(detail.DatumIndex) %></span>
            <%} %>
        </div>
        <div class="toggleNoti " data-notiid="<%:item.Notification.NotificationID%>" data-dindex="<%:detail.DatumIndex%>">
            <div class=" ListBorder<%:detail.SensorNotificationID > 0 ? "Active" : "NotActive"%> notiSensor<%:detail.SensorNotificationID%> sensorID<%:sensor.SensorID%> datumIndex<%:detail.DatumIndex%> circle__status gridPanel-sensor">
                <%=Html.GetThemedSVG("circle-check") %>
            </div>
        </div>
        <div class="menu-hover menu-fav" data-bs-toggle="dropdown" data-bs-auto-close="true" aria-expanded="false">

            <%=Html.GetThemedSVG("menu") %>
        </div>
        <ul class="dropdown-menu ddm" style="padding: 0;">
            <%bool showtest = true;
                if (item.Notification.NotificationID > 0 && showtest)
                {%>
            <li>
                <a class="dropdown-item menu_dropdown_item notitest" title="Send Test" style="cursor: pointer;" id="notiTest" onclick="SendTest('<%=item.Notification.NotificationID %>');">
                    <span><%: Html.TranslateTag("Send Test","Send Test")%></span>
                    <%=Html.GetThemedSVG("sendTest") %>
                </a>
            </li>
            <span id="testMessage_<%=item.Notification.NotificationID %>" style="color: red; font-weight: bold; font-size: 0.8em;"></span>
            <hr style="margin-top: 5px; margin-bottom: 5px;" />
            <%} %>


            <li>
                <a class="dropdown-item menu_dropdown_item" href="/Rule/History/<%:item.Notification.NotificationID%>">
                    <span><%: Html.TranslateTag("History","History")%></span>
                    <%=Html.GetThemedSVG("list") %>
                </a>
            </li>
            <li>
                <a class="dropdown-item menu_dropdown_item" href="/Rule/Triggers/<%:item.Notification.NotificationID%>">
                    <span><%: Html.TranslateTag("Conditions")%></span>
                    <%=Html.GetThemedSVG("conditions") %>
                </a>
            </li>
            <li>
                <a class="dropdown-item menu_dropdown_item" href="/Rule/ChooseTaskToEdit/<%:item.Notification.NotificationID%>">
                    <span><%: Html.TranslateTag("Tasks")%></span>
                    <%=Html.GetThemedSVG("tasks") %>
                </a>
            </li>
            <li>
                <a class="dropdown-item menu_dropdown_item" href="/Rule/Calendar/<%:item.Notification.NotificationID%>">
                    <span><%: Html.TranslateTag("Schedule","Schedule")%></span>
                    <%=Html.GetThemedSVG("schedule") %>
                </a>
            </li>
            <%if (MonnitSession.CustomerCan("Notification_Edit"))
                { %>
            <hr />
            <li>
                <a class="dropdown-item menu_dropdown_item" onclick="deleteConfirmation(<%=item.Notification.NotificationID %>)" id="list">
                    <span>
                        <%: Html.TranslateTag("Events/Triggers|Delete Rule")%> 
                    </span>
                    <%=Html.GetThemedSVG("delete") %>
                </a>
            </li>
            <%} %>
        </ul>


    </div>
</div>

<%
            //}
        }
    }
%>

<script>
    <%-- Use sourceURL to give <script> block in partial a label in DevTools. It will appear in the same location as parent. --%>
    //# sourceURL=AvailableSensorActions.ascx

    var sendingString = '<%= Html.TranslateTag("Sending")%>';

    $(document).ready(function () {
        $('#totalRules').html(<%= totalRules %>);
        $('#filteredRules').html(<%= filteredRules %>);
    });

    $(function () {
        $(".toggleNoti").click(function (e) {
            TogggleExistingNotification($(this),<%:sensor.SensorID%>, $(this).data('notiid'), $(this).data("dindex"));
        });
    });

    function clearTestMessage(notificationID) {
        $('#testMess').click();
        $('#testMessage_' + notificationID).html("");
        $("#sendTestListPage_" + notificationID).html('<i style="color: #51535b; margin-right: 0px; font-size: 1.2em;" class="fa fa-share-square-o"></i>');
    }

    function SendTest(notificationID) {
        $("#sendTestListPage_" + notificationID).html(sendingString + "...");

        $.post('/Notification/Test/' + notificationID, function (data) {
            $('#testMessage_' + notificationID).html(data);
        });

        setTimeout(clearTestMessage(notificationID), 5000)
    }

    function TogggleExistingNotification(element, sensorID, notificationID, datumindex) {
        let checkbox = $(element).find(".sensorID" + sensorID + ".datumIndex" + datumindex);
        if (checkbox.hasClass("ListBorderActive")) {
            RemoveExistingNotification(checkbox, sensorID, notificationID, datumindex)
        }
        else {
            AddExistingNotification(checkbox, sensorID, notificationID, datumindex);
        }
    }

    function RemoveExistingNotification(border, sensorID, notificationID, datumindex) {

        let params = {};
        params.sensorID = sensorID;
        params.notificationID = notificationID;
        params.datumindex = datumindex;
        $.post("/Overview/RemoveExistingNotificationFromSensor", params, (data) => {
            if (data == "Success") {
                border.removeClass("ListBorderActive");
                border.addClass("ListBorderNotActive");
            }
            else {
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }

        });
    }

    function AddExistingNotification(border, sensorID, notificationID, datumindex) {
        let params = {};
        var url = "/Overview/AddExistingNotificationToSensor";
        params.sensorID = sensorID;
        params.notificationID = notificationID;
        params.datumindex = datumindex;
        $.post(url, params, function (data) {
            if (data == "Success") {
                border.removeClass("ListBorderNotActive");
                border.addClass("ListBorderActive");
            }
            else {
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }
</script>
