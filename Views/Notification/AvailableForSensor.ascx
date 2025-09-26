<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>
<%   List<Notification> Assigned = ViewBag.Assigned;
    List<Notification> Available = new List<Notification>();
    foreach (Notification noti in Notification.LoadByAccountID(Model.AccountID))
    {
        if (Assigned.Find(n => { return n.NotificationID == noti.NotificationID; }) == null)
        {
            if ((noti.NotificationClass == eNotificationClass.Application && noti.ApplicationID == Model.ApplicationID) //Notification is valid for this application
                || (noti.NotificationClass == eNotificationClass.Low_Battery && Model.PowerSource.VoltageForZeroPercent != Model.PowerSource.VoltageForOneHundredPercent) //sensor is battery powered for battery notification
                || (noti.NotificationClass == eNotificationClass.Advanced && !AdvancedNotification.Load(noti.AdvancedNotificationID).HasGatewayList))// Advanced Sensor Notification
            {
                Available.Add(noti);
            }
        }
    }
%>
<div id="blockBox">
    <div class="formtitle">Notifications</div>
    <div id="blockBoxInside">
        <table width="100%">

            <tr>
                <th colspan="2">Build your own</th>
            </tr>

            <tr>
                <td colspan="2">
                    <a id="createNew" href="/Notification/SimpleCreateSensor/<%:Model.SensorID%>">Custom <%:Model.MonnitApplication.ApplicationName %> Notification</a>
                </td>
            </tr>

            <%if (Available.Count > 0)
                { %>
            <tr>
                <th colspan="2">Existing on Account</th>
            </tr>
            <% } %>
            <% foreach (var item in Available)
                { %>
            <tr>
                <td>
                    <%: Html.CheckBox("AddNotification_" + item.NotificationID, new { @class = "ExistingNotification" })%>
                </td>
                <td>
                    <%: Html.DisplayFor(modelItem => item.Name) %>
                </td>
            </tr>
            <% } %>

            <tr>
                <th colspan="2">Preconfigured options</th>
            </tr>

            <% int index = 0;
                foreach (var item in Model.DefaultNotifications())
                {%>
            <tr>
                <td>
                    <%: Html.CheckBox("AddNotification_" + index, new { @class = "DefaultNotification" })%>
                </td>
                <td>
                    <%: Html.DisplayFor(modelItem => item.Name) %>
                </td>
            </tr>
            <% index++;
                } %>
        </table>
    </div>

    <div class="buttons">
        <input type="button" value="Add Selected" id="addExisting" class="bluebutton">
        <div class="clear"></div>
    </div>

</div>
<script>
    $(function () {
        $('#addExisting').click(function () {
            //gather notificationIDs
            var ids = new Array();
            $('.ExistingNotification:checked').each(function (index) {
                ids[index] = $(this).attr("id").replace("AddNotification_", "");
            });

            var defaultIndexes = new Array();
            $('.DefaultNotification:checked').each(function (index) {
                defaultIndexes[index] = $(this).attr("id").replace("AddNotification_", "");
            });

            jQuery.ajaxSettings.traditional = true;//lets the post of the array
            //Post to NotificationController
            $.post("/Notification/AddNotificationsToSensor/<%:Model.SensorID%>", { "notifications": ids, "defaultIndexes": defaultIndexes }, function (data) {
                if (data == "Success")
                    window.location.href = window.location.href; //"/CSNet/AssignSensorNotification/<%:Model.SensorID%>";
                else {
                    console.log(data);
                    showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
                }
            });
        });

        $('#createNew').click(function (e) {
            e.preventDefault();

            $('.notificationRight').html(`<div id="loadingGIF" class="text-center" style="display: none;">
    <div class="spinner-border text-primary" style="margin-top: 50px;" role="status">
        <span class="visually-hidden"><%: Html.TranslateTag("Loading")%>...</span>
    </div>
</div>`);
            $.get($(this).attr("href"), function (data) {
                $('.notificationRight').html(data);
            });
        });


    });
</script>
