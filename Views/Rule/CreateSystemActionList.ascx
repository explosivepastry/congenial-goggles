<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<Monnit.NotificationRecipient>>" %>

<% 
    foreach (NotificationRecipient recipient in Model)
    {%>
<!-- System Action -->
<div class=" csa-card ">
    <%if (recipient.NotificationType == eNotificationType.SystemAction)
        {
            string notiName = "";

            if (recipient.SerializedRecipientProperties.ToLong() == -1)
                notiName = "{Target Self}";
            else
                notiName = Notification.Load(recipient.SerializedRecipientProperties.ToLong()).Name;



    %>

    <div class="csa-action">
        <%=ActionToExecute.Load(recipient.ActionToExecuteID).Name %>
        <div style="color: red; font-size: 0.8em">
            <%= recipient.DelayMinutes == 0 ? "No" : recipient.DelayMinutes > 60 ? (recipient.DelayMinutes / 60) + Html.TranslateTag("Hour", " Hour") : recipient.DelayMinutes + Html.TranslateTag("Minutes", " Minutes")%>
            <%: Html.TranslateTag("Rule/CreateSystemActionList|Delay", "Delay")%>
        </div>
    </div>

    <div class="csa-name"><%=notiName %></div>

    <%}
        else if (recipient.NotificationType == eNotificationType.ResetAccumulator)
        {%>

    <div class="csa-action">
        <%= Html.TranslateTag("Reset Accumulator") %>
        <div style="color: red; font-size: 0.8em">
            <%= recipient.DelayMinutes == 0 ? "No" : recipient.DelayMinutes > 60 ? (recipient.DelayMinutes / 60) + Html.TranslateTag("Hour", " Hour") : recipient.DelayMinutes + Html.TranslateTag("Minutes", " Minutes")%>
            <%: Html.TranslateTag("Rule/CreateSystemActionList|Delay", "Delay")%>
        </div>
    </div>

        <div class="csa-name"><%: Html.TranslateTag(recipient.DeviceToNotify.SensorName) %></div>
    <%}
        else if (recipient.NotificationType == eNotificationType.HTTP)
        {%>

    <div class="csa-action">
        <%= Html.TranslateTag("Rule Webhook") %>
        <div style="color: red; font-size: 0.8em">
            <%= recipient.DelayMinutes == 0 ? "No" : recipient.DelayMinutes > 60 ? (recipient.DelayMinutes / 60) + Html.TranslateTag("Hour", " Hour") : recipient.DelayMinutes + Html.TranslateTag("Minutes", " Minutes")%>
            <%: Html.TranslateTag("Rule/CreateSystemActionList|Delay", "Delay")%>
        </div>
    </div>

    <%}

        else
        {%>
    <div style="font-size: 13px; font-weight: bold;">
        <%= Html.TranslateTag("Unknown Notification Type") %>
    </div>
    <%} %>

    <div class="csa-list-footer">
        <svg xmlns="http://www.w3.org/2000/svg" width="14" height="18" viewBox="0 0 14 18" title="<%: Html.TranslateTag("Delete","Delete")%>" onclick="removeSystemAction('<%= recipient.NotificationType%>', '<%= recipient.SerializedRecipientProperties%>', '<%= recipient.DelayMinutes%>', '<%= recipient.ActionToExecuteID%>');" class="fa fa-trash " style="vertical-align: top; cursor: pointer;">
            <path id="ic_delete_24px" d="M6,19a2.006,2.006,0,0,0,2,2h8a2.006,2.006,0,0,0,2-2V7H6ZM19,4H15.5l-1-1h-5l-1,1H5V6H19Z" transform="translate(-5 -3)" fill="#ff4d2d"></path>
        </svg>
    </div>
</div>

<!-- End System Action -->
<%} %>