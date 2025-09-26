<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>



<tbody>
    <tr>
    	<th style="width:20px"></th>
        <th style="width:50px"></th>
        <th style="width:50px">Type</th>
        <th style="width:180px">Notification Name</th>
        <th style="width:130px;">Last Sent</th>
        <th>Sending To</th>
        <th>Notifying On</th>
        <th style="width:90px;"></th>
        <th style="width:20px;"></th>
    </tr>
    <%
        bool alt = true;
        List<Notification> list = ViewBag.ExistingNotificaitons as List<Notification>;
        if (list.Count < 1)
        {%>
        <tr>
            <td></td>
    	    <td colspan="6"><b>Currently no notifications assigned</b></td>
            <td></td>
        </tr>
        <%}
        foreach (var item in list)
        {
            alt = !alt;
    %>
    <tr class="<%: alt ? "alt" : "" %> viewNotificationDetails <%:item.NotificationID %>">
    	<td></td>
        <td>
            <div>
                <a href="/Notification/SetActive/<%:item.NotificationID%>?active=false"  title="Notification active.  Click to disable notification" id="<%:item.NotificationID%>" class="notiStatus notiStatus<%:item.NotificationID%>" style="display:<%:item.IsActive ? "inline" : "none"%>;"><img src="<%: Html.GetThemedContent("/images/notification/power-on.png")%>" /></a>
                <a href="/Notification/SetActive/<%:item.NotificationID%>?active=true"  title="Notification inactive.  Click to enable notification" id="<%:item.NotificationID%>" class="notiStatus notiStatus<%:item.NotificationID%>" style="display:<%:!item.IsActive ? "inline" : "none"%>;"><img src="<%: Html.GetThemedContent("/images/notification/power-off.png")%>" /></a>
            </div>
        </td>
        <td>
            <div>
                <img src="<%: Html.GetThemedContent(string.Format("/images/notification/class-{0}.png",item.NotificationClass))%>" class="typeIcon tabToShow" data-tab="From" alt="<%:item.NotificationClass %>" />
            </div>
        </td>
        <td><%= item.Name%></td>
        <td>
            <%if(item.LastSent > DateTime.MinValue){ %>
            <%: String.Format("{0:g}", Monnit.TimeZone.GetLocalTimeById(item.LastSent, MonnitSession.CurrentCustomer.Account.TimeZoneID))%>
            <%} %>
        </td>
        <td>
            <div>
            <% bool hasEmail = false;
               bool hasSMS = false;
               bool hasPhone = false;
                bool hasControl = false;
                bool hasAttention = false;
                foreach (NotificationRecipient nr in item.NotificationRecipients)
                {
                    if (nr.NotificationType == eNotificationType.Email || nr.NotificationType == eNotificationType.Both) { hasEmail = true; }
                    if (nr.NotificationType == eNotificationType.SMS || nr.NotificationType == eNotificationType.Both) { hasSMS = true; }
                    if (nr.NotificationType == eNotificationType.Phone) { hasPhone = true; }
                    if (nr.NotificationType == eNotificationType.Control) { hasControl = true; }
                    if (nr.NotificationType == eNotificationType.Local_Notifier) { hasAttention = true; }
                } %>
                <%if (hasEmail) {%><img src="<%: Html.GetThemedContent("/images/notification/email-off.png")%>" class="emailIcon tabToShow" data-tab="Recipient"  alt="Email" /><%} %>
                <%if (hasSMS) {%><img src="<%: Html.GetThemedContent("/images/notification/sms-off.png")%>" class="smsIcon tabToShow" data-tab="Recipient"  alt="SMS" /><%} %>
                <%if (hasPhone) {%><img src="<%: Html.GetThemedContent("/images/notification/phone-off.png")%>" class="phoneIcon tabToShow" data-tab="Recipient"  alt="Phone" /><%} %>
                <%if (hasControl) {%><img src="<%: Html.GetThemedContent("/images/notification/control-off.png")%>" class="controlIcon tabToShow" data-tab="Control"  alt="Control" /><%} %>
                <%if (hasAttention) {%><img src="<%: Html.GetThemedContent("/images/notification/attention-off.png")%>" class="attentionIcon tabToShow" data-tab="Control" alt="Attention" /><%} %>
            </div>
        </td>

        <td>
            <%
                List<int> notifyingIndeces = new List<int>();
                if(item.NotificationClass == eNotificationClass.Application) {
                    List<SensorDatum> sensnote = SensorNotification.LoadByNotificationID(item.NotificationID);
                    Sensor sens = null;
                    if (ViewBag.Sensor != null) {
                        sens = (ViewBag.Sensor as Sensor);
                        string notifyingon = "";
                        foreach (int di in (from di in sensnote where di.sens.SensorID == sens.SensorID select di.DatumIndex))
                        {
                            notifyingon += Sensor.GetDatumName(sens.SensorID, di) + ", ";
                            notifyingIndeces.Add(di);
                        }
                        if(!String.IsNullOrEmpty(notifyingon))
                            notifyingon = notifyingon.Substring(0, notifyingon.Length - 2);
                    
                %>
                <%= notifyingon %>
            <%  }
              }
              else {
                    
                } %>
        </td>
        
        <td>
            <%if(MonnitSession.CustomerCan("Notification_Edit")){ %>
            <div style="text-align: right;">
                <%if(ViewBag.ShowNotificationEdit == true){ %>
                <a href="/Notification?notification=<%:item.NotificationID %>&tab=Edit"><img src="<%: Html.GetThemedContent("/images/notification/pencil.png")%>" class="tabToShow" data-tab="Edit" alt="Edit" title="Edit Notification" /></a>
                <%} %>
                <%if(notifyingIndeces.Count > 0) { %>
                <a href="Remove" onclick= "removeNotification(<%:item.NotificationID %>, <%: notifyingIndeces[notifyingIndeces.Count-1] %>); return false;"><img src="/Content/images/notification/remove.png" alt="Remove Notification" title="Remove Device From Notification" class="removeIcon" /></a>
                <%} else { %>
                <a href="Remove" onclick= "removeNotification(<%:item.NotificationID %>); return false;"><img src="/Content/images/notification/remove.png" alt="Remove Notification" title="Remove Device From Notification" class="removeIcon" /></a>
                <%} %>
            </div>
            <%} %>
        </td>
        
        <td></td>
                
    </tr>
    
    <% } %>
</tbody>
<script>
    setTimeout("setNotificationStatusClick();", 10);
</script>