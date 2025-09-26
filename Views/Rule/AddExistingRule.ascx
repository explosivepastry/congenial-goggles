<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


<tbody>
    <tr>
    	<th style="width:5px"></th>
        <th style="width:50px"></th>
        <!-- <th style="width:50px">Type</th> -->
        <th style="width:180px"><%: Html.TranslateTag("Name","Name")%></th>
        <!--<th style="width:130px;">Last Sent</th>
         <th>Sending To</th> -->
        <th style="width:90px;"></th>
        
    </tr>
    <%
        bool alt = true;
        List<Notification> list = ViewBag.ExistingNotificaitons as List<Notification>;
        if (list.Count < 1)
        {%>
        <tr>
            <td></td>
    	    <td colspan="3"><b><%: Html.TranslateTag("Rule/AddExistingRule|Currently no notifications assigned","Currently no notifications assigned")%></b></td>
            
        </tr>
        <%}
        foreach (var item in list)
        {
            alt = !alt;
    %>
    <tr class="<%: alt ? "alt" : "" %> viewNotificationDetails <%:item.NotificationID %>">
    	<td></td>
<%--        <td>
            <div>
                <a href="/Notification/SetActive/<%:item.NotificationID%>?active=false" title="Notification active.  Click to disable notification" id="<%: item.NotificationID%>" class="notiStatus notiStatus<%:item.NotificationID%>" style="display:<%:item.IsActive ? "inline" : "none"%>;"><img src="<%: Html.GetThemedContent("/images/notification/power-on.png")%>" /></a>
                <a href="/Notification/SetActive/<%:item.NotificationID%>?active=true" title="Notification inactive.  Click to enable notification" id="<%: item.NotificationID%>" class="notiStatus notiStatus<%:item.NotificationID%>" style="display:<%:!item.IsActive ? "inline" : "none"%>;"><img src="<%: Html.GetThemedContent("/images/notification/power-off.png")%>" /></a>
            </div>
        </td>--%>
        <!-- <td>
            <div>
                <img src="<%: Html.GetThemedContent(string.Format("/images/notification/class-{0}.png",item.NotificationClass))%>" class="typeIcon tabToShow" data-tab="From" alt="<%:item.NotificationClass %>" />
            </div>
        </td> -->
        <td><%= item.Name%></td>
        <!--<td>
            <%if(item.LastSent > DateTime.MinValue){ %>
            <%: String.Format("{0:g}", Monnit.TimeZone.GetLocalTimeById(item.LastSent, MonnitSession.CurrentCustomer.Account.TimeZoneID))%>
            <%} %>
        </td>
         <td>
            <div>
            <% bool hasEmail = false;
                bool hasSMS = false;
                bool hasControl = false;
                bool hasAttention = false;

                foreach (NotificationRecipient nr in item.NotificationRecipients)
                {
                    if (nr.NotificationType == eNotificationType.Email || nr.NotificationType == eNotificationType.Both) { hasEmail = true; } 
                    if (nr.NotificationType == eNotificationType.SMS || nr.NotificationType == eNotificationType.Both) { hasSMS = true; }
                    if (nr.NotificationType == eNotificationType.Control) { hasControl = true; }
                    if (nr.NotificationType == eNotificationType.Local_Notifier) { hasAttention = true; }
                } %>
                <%if (hasEmail) {%><img src="<%: Html.GetThemedContent("/images/notification/email.png")%>" class="emailIcon tabToShow" data-tab="Recipient"  alt="Email" /><%} %>
                <%if (hasSMS) {%><img src="<%: Html.GetThemedContent("/images/notification/sms.png")%>" class="smsIcon tabToShow" data-tab="Recipient"  alt="SMS" /><%} %>
                <%if (hasControl) {%><img src="<%: Html.GetThemedContent("/images/notification/control.png")%>" class="controlIcon tabToShow" data-tab="Control"  alt="Control" /><%} %>
                <%if (hasAttention) {%><img src="<%: Html.GetThemedContent("/images/notification/attention.png")%>" class="attentionIcon tabToShow" data-tab="Control" alt="Attention" /><%} %>
            </div>
        </td> -->
        
         <td>
            <%if(MonnitSession.CustomerCan("Notification_Edit")){ %>
            <div style="text-align: right; min-width:90px;">
                <%--<%if(ViewBag.ShowNotificationEdit == true){ %>
                <a href="/m/Notification?notification=<%:item.NotificationID %>&tab=Edit" class="icon icon-pencil"><!-- <img src="<%: Html.GetThemedContent("/images/notification/pencil.png")%>" class="tabToShow" data-tab="Edit" alt="Edit" title="Edit Notification" />--></a>
                <%} %>--%>
                <a href="Remove" onclick="removeNotification(<%:item.NotificationID %>); return false;"  class="icon icon-remove"></a><!--<img src="/Content/images/notification/remove.png" alt="Remove Notification" title="Remove Device From Notification" class="removeIcon" /> -->
            </div>
            <%} %>
        </td>
    </tr>
  <% } %>
</tbody>