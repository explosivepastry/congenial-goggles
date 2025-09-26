<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<div class="formtitle">
    System Actions

</div>
<div id="MainList">
    <table width="100%">
        <tr>
            <th></th>
            <th style="width: 120px">Action</th>
            <th>Target</th>
            <th></th>
            <th></th>
            <th>Delay Minutes</th>
            <th>Remove</th>
            <th></th>
        </tr>
        <% List<NotificationRecipient> recipientList = Model.NotificationRecipients.Where(mod => mod.ActionToExecuteID > 0).ToList();
            foreach (NotificationRecipient nr in recipientList)
            { %>
        <tr>
            <td></td>
            <td><%=ActionToExecute.Load(nr.ActionToExecuteID).Name %></td>
            <td><%=Notification.Load(nr.SerializedRecipientProperties.ToLong()).Name %></td>
            <td></td>
            <td></td>
            <td>
                <%NotificationRecipient Recipient = NotificationRecipient.Load(nr.NotificationRecipientID); %>
                <select onchange="setDelay($(this));return false;" data-recipientid="<%=Recipient.NotificationRecipientID%>" style="width: 100px;">

                    <option value="0" <%=  Recipient.DelayMinutes == 0?"selected":""%>>No Delay</option>
                    <option value="1" <%=  Recipient.DelayMinutes == 1?"selected":""%>>1 Minute</option>
                    <option value="2" <%=  Recipient.DelayMinutes == 2?"selected":""%>>2 Minutes</option>
                    <option value="5" <%=  Recipient.DelayMinutes == 5?"selected":""%>>5 Minutes</option>
                    <option value="10" <%= Recipient.DelayMinutes == 10?"selected":""%>>10 Minutes</option>
                    <option value="15" <%= Recipient.DelayMinutes == 15?"selected":""%>>15 Minutes</option>
                    <option value="20" <%= Recipient.DelayMinutes == 20?"selected":""%>>20 Minutes</option>
                    <option value="25" <%= Recipient.DelayMinutes == 25?"selected":""%>>25 Minutes</option>
                    <option value="30" <%= Recipient.DelayMinutes == 30?"selected":""%>>30 Minutes</option>
                    <option value="35" <%= Recipient.DelayMinutes == 35?"selected":""%>>35 Minutes</option>
                    <option value="40" <%= Recipient.DelayMinutes == 40?"selected":""%>>40 Minutes</option>
                    <option value="45" <%= Recipient.DelayMinutes == 45?"selected":""%>>45 Minutes</option>
                    <option value="50" <%= Recipient.DelayMinutes == 50?"selected":""%>>50 Minutes</option>
                    <option value="55" <%= Recipient.DelayMinutes == 55?"selected":""%>>55 Minutes</option>
                </select>

            </td>

            <td><a href="/Notification/ActionDelete/<%=nr.NotificationRecipientID %>" class="delete">
                <img src="<%= Html.GetThemedContent("/images/notification/trash.png")%>" class="deleteIcon" alt="delete" /></a></td>
            <td></td>
        </tr>
        <%} %>
    </table>

</div>
<form id="ActionForm">
    <div style="margin-top: 20px; margin-bottom: 10px;">
        <div class="formtitle">
            <div>
                <label for="Action">Action to be done </label>


                <select name="action" id="Action" style="width: 200px">
                    <%foreach (ActionToExecute action in ActionToExecute.LoadAll())
                        { %>
                    <option value="<%=action.ActionToExecuteID %>"><%=action.Description %></option>
                    <%} %>
                </select>

                <label for="Notification">Notification </label>

                <select id="ActionTarget" name="ActionTarget">
                    <% foreach (Notification item in (ViewBag.NotificationList))
                        {%>
                    <option value="<%=item.NotificationID%>" <%= Model.NotificationID == item.NotificationID ? "selected" : ""%>><%=item.Name %></option>
                    <%} %>
                </select>

                <input type="button" value="Add" class="bluebutton" id="AddButton" />

            </div>
        </div>

    </div>
</form>


<script type="text/javascript">

    $(document).ready(function () {
        $('#AddButton').click(function () {
            var body = $('#ActionForm').serialize();
            var href = "/Notification/SystemAction/<%=Model.NotificationID%>";
            $.post(href, body, function (data) {
                if (data == "Failed") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed to set action")%>");
                }
                else
                    var tabContainer = $('.tabContainer').tabs();
                var active = tabContainer.tabs('option', 'active');
                tabContainer.tabs('load', active);
            });
        });
    });


    $('.delete').click(function (e) {
        e.preventDefault();
        var clickedRow = $(this);

        if (confirm('Are you sure you want to delete this action?')) {
            $.get(clickedRow.attr('href'), function (data) {
                if (data == "Success")
                    var tabContainer = $('.tabContainer').tabs();
                var active = tabContainer.tabs('option', 'active');
                tabContainer.tabs('load', active);
            });

        }
        e.stopImmediatePropagation();
    });

    function setDelay(Delayinput) {
        Delayinput = $(Delayinput);
        var recipientID = Delayinput.data("recipientid");
        var delayMinutes = Delayinput.val();
        var url = "/Notification/SetDelayMinutes/";

        var params = "notificationRecipientID=" + recipientID;

        params += "&delayMinutes=" + delayMinutes;


        $.post(url, params, function (data) {
            if (data != 'Success') {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });
    }





</script>
