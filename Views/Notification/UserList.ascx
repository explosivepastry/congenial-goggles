<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<PotentialNotificationRecipient>>" %>

<table border="0" style="margin: 10px;">
    <%
          
        List<NotificationRecipient> RecipientList = ViewBag.NotificationRecipients;
        foreach (PotentialNotificationRecipient cust in Model)//.OrderBy(p => RecipientList.FirstOrDefault(l => l.CustomerToNotifyID == p.CustomerID) == null).ThenBy(p => p.FullName))
        {
    %>

    <tr class="recipient<%:cust.CustomerID %>">
        <td>
            <%= cust.FullName%> <%if (cust.AccountID != MonnitSession.CurrentCustomer.AccountID)
                                               { %> (<%=cust.CompanyName %>) <%} %>
        </td>

        <td>
            <%var EmailReceipient = (RecipientList.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && (m.NotificationType == eNotificationType.Email || m.NotificationType == eNotificationType.Both); }).FirstOrDefault());
              var emailActive = EmailReceipient != null;%>

            <a href="RemoveEmail" title="<%:cust.NotificationEmail %>" onclick="toggleRecipient($(this)); return false;" data-customerid="<%: cust.CustomerID %>" data-type="Email" class="nr<%:cust.CustomerID %> icon icon-mail active " style="display: <%:emailActive ? "inline" : "none"%>;"></a>
            <a href="AddEmail" title="<%:cust.NotificationEmail %>" onclick="toggleRecipient($(this)); return false;" data-customerid="<%: cust.CustomerID %>" data-type="Email" class="nr<%:cust.CustomerID %> icon icon-mail inactive" style="display: <%:!emailActive ? "inline" : "none"%>;"></a>
            <% if (EmailReceipient != null && MonnitSession.AccountCan("email_notification"))
               { %>
            <select onchange="setDelay($(this));return false;" data-recipientid="<%:EmailReceipient.NotificationRecipientID%>" style="width: 100px;">

                <option value="0" <%:  EmailReceipient.DelayMinutes == 0?"selected":""%>>No Delay</option>
                <option value="1" <%:  EmailReceipient.DelayMinutes == 1?"selected":""%>>1 min</option>
                <option value="2" <%:  EmailReceipient.DelayMinutes == 2?"selected":""%>>2 min</option>
                <option value="5" <%:  EmailReceipient.DelayMinutes == 5?"selected":""%>>5 min</option>
                <option value="10" <%:  EmailReceipient.DelayMinutes == 10?"selected":""%>>10 min</option>
                <option value="15" <%:  EmailReceipient.DelayMinutes == 15?"selected":""%>>15 min</option>
                <option value="20" <%:  EmailReceipient.DelayMinutes == 20?"selected":""%>>20 min</option>
                <option value="25" <%:  EmailReceipient.DelayMinutes == 25?"selected":""%>>25 min</option>
                <option value="30" <%:  EmailReceipient.DelayMinutes == 30?"selected":""%>>30 min</option>
                <option value="35" <%:  EmailReceipient.DelayMinutes == 35?"selected":""%>>35 min</option>
                <option value="40" <%:  EmailReceipient.DelayMinutes == 40?"selected":""%>>40 min</option>
                <option value="45" <%:  EmailReceipient.DelayMinutes == 45?"selected":""%>>45 min</option>
                <option value="50" <%:  EmailReceipient.DelayMinutes == 50?"selected":""%>>50 min</option>
                <option value="55" <%:  EmailReceipient.DelayMinutes == 55?"selected":""%>>55 min</option>
                <option value="60" <%:  EmailReceipient.DelayMinutes == 60?"selected":""%>>1 hour</option>
                <option value="120" <%:  EmailReceipient.DelayMinutes == 120?"selected":""%>>2 hours</option>
                <option value="180" <%:  EmailReceipient.DelayMinutes == 180?"selected":""%>>3 hours</option>
                <option value="240" <%:  EmailReceipient.DelayMinutes == 240?"selected":""%>>4 hours</option>
            </select>
            <% } %>
        </td>
        <td>
            <%if (!string.IsNullOrEmpty(cust.NotificationPhone) && cust.SendSensorNotificationToText)
              {
                  var smsReceipient = (RecipientList.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && (m.NotificationType == eNotificationType.SMS || m.NotificationType == eNotificationType.Both); }).FirstOrDefault());
                  var smsActive = smsReceipient != null; %>
            <a href="RemoveSMS" title="<%:cust.NotificationPhone%>" onclick="toggleRecipient($(this)); return false;" data-customerid="<%: cust.CustomerID %>" data-type="SMS" class="nr<%:cust.CustomerID %> icon icon-chat-empty active" style="display: <%:smsActive ? "inline" : "none"%>;"></a>
            <a href="AddSMS" title="<%:cust.NotificationPhone %>" onclick="toggleRecipient($(this)); return false;" data-customerid="<%: cust.CustomerID %>" data-type="SMS" class="nr<%:cust.CustomerID %> icon icon-chat-empty inactive" style="display: <%:!smsActive ? "inline" : "none"%>;"></a>
            <% if (smsReceipient != null && MonnitSession.AccountCan("text_notification"))
               { %>
            <select onchange="setDelay($(this));return false;" data-recipientid="<%:smsReceipient.NotificationRecipientID%>" style="width: 100px;">

                <option value="0" <%:  smsReceipient.DelayMinutes == 0?"selected":""%>>No Delay</option>
                <option value="1" <%:  smsReceipient.DelayMinutes == 1?"selected":""%>>1 min</option>
                <option value="2" <%:  smsReceipient.DelayMinutes == 2?"selected":""%>>2 min</option>
                <option value="5" <%:  smsReceipient.DelayMinutes == 5?"selected":""%>>5 min</option>
                <option value="10" <%:  smsReceipient.DelayMinutes == 10?"selected":""%>>10 min</option>
                <option value="15" <%:  smsReceipient.DelayMinutes == 15?"selected":""%>>15 min</option>
                <option value="20" <%:  smsReceipient.DelayMinutes == 20?"selected":""%>>20 min</option>
                <option value="25" <%:  smsReceipient.DelayMinutes == 25?"selected":""%>>25 min</option>
                <option value="30" <%:  smsReceipient.DelayMinutes == 30?"selected":""%>>30 min</option>
                <option value="35" <%:  smsReceipient.DelayMinutes == 35?"selected":""%>>35 min</option>
                <option value="40" <%:  smsReceipient.DelayMinutes == 40?"selected":""%>>40 min</option>
                <option value="45" <%:  smsReceipient.DelayMinutes == 45?"selected":""%>>45 min</option>
                <option value="50" <%:  smsReceipient.DelayMinutes == 50?"selected":""%>>50 min</option>
                <option value="55" <%:  smsReceipient.DelayMinutes == 55?"selected":""%>>55 min</option>
                <option value="60" <%:  smsReceipient.DelayMinutes == 60?"selected":""%>>1 hour</option>
                <option value="120" <%:  smsReceipient.DelayMinutes == 120?"selected":""%>>2 hours</option>
                <option value="180" <%:  smsReceipient.DelayMinutes == 180?"selected":""%>>3 hours</option>
                <option value="240" <%:  smsReceipient.DelayMinutes == 240?"selected":""%>>4 hours</option>
            </select>
            <% } %>
            <%}
              else
              {
                  foreach (NotificationRecipient nr in RecipientList.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && m.NotificationType == eNotificationType.SMS; }))
                  {
                      nr.Delete();
                  }
              }%>
        </td>
        <td>
            <%if (!string.IsNullOrEmpty(cust.NotificationPhone2) && cust.SendSensorNotificationToVoice && !string.IsNullOrEmpty(MonnitSession.CurrentTheme.FromPhone))
              {
                  var VoiceReceipient = (RecipientList.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && m.NotificationType == eNotificationType.Phone; }).FirstOrDefault());
                  var voiceActive = VoiceReceipient != null;%>
            <a href="RemovePhone" title="<%:cust.NotificationPhone2 %>" onclick="toggleRecipient($(this)); return false;" data-customerid="<%: cust.CustomerID %>" data-type="Phone" class="nr<%:cust.CustomerID %> icon icon-voice active" style="display: <%:voiceActive ? "inline" : "none"%>;"></a>
            <a href="AddPhone" title="<%:cust.NotificationPhone2 %>" onclick="toggleRecipient($(this)); return false;" data-customerid="<%: cust.CustomerID %>" data-type="Phone" class="nr<%:cust.CustomerID %> icon icon-voice inactive" style="display: <%:!voiceActive ? "inline" : "none"%>;"></a>
            <% if (VoiceReceipient != null && MonnitSession.AccountCan("voice_notification"))
               { %>
            <select onchange="setDelay($(this));return false;" data-recipientid="<%:VoiceReceipient.NotificationRecipientID%>" style="width: 100px;">

                <option value="0" <%:  VoiceReceipient.DelayMinutes == 0?"selected":""%>>No Delay</option>
                <option value="1" <%:  VoiceReceipient.DelayMinutes == 1?"selected":""%>>1 min</option>
                <option value="2" <%:  VoiceReceipient.DelayMinutes == 2?"selected":""%>>2 min</option>
                <option value="5" <%:  VoiceReceipient.DelayMinutes == 5?"selected":""%>>5 min</option>
                <option value="10" <%:  VoiceReceipient.DelayMinutes == 10?"selected":""%>>10 min</option>
                <option value="15" <%:  VoiceReceipient.DelayMinutes == 15?"selected":""%>>15 min</option>
                <option value="20" <%:  VoiceReceipient.DelayMinutes == 20?"selected":""%>>20 min</option>
                <option value="25" <%:  VoiceReceipient.DelayMinutes == 25?"selected":""%>>25 min</option>
                <option value="30" <%:  VoiceReceipient.DelayMinutes == 30?"selected":""%>>30 min</option>
                <option value="35" <%:  VoiceReceipient.DelayMinutes == 35?"selected":""%>>35 min</option>
                <option value="40" <%:  VoiceReceipient.DelayMinutes == 40?"selected":""%>>40 min</option>
                <option value="45" <%:  VoiceReceipient.DelayMinutes == 45?"selected":""%>>45 min</option>
                <option value="50" <%:  VoiceReceipient.DelayMinutes == 50?"selected":""%>>50 min</option>
                <option value="55" <%:  VoiceReceipient.DelayMinutes == 55?"selected":""%>>55 min</option>
                <option value="60" <%:  VoiceReceipient.DelayMinutes == 60?"selected":""%>>1 hour</option>
                <option value="120" <%:  VoiceReceipient.DelayMinutes == 120?"selected":""%>>2 hours</option>
                <option value="180" <%:  VoiceReceipient.DelayMinutes == 180?"selected":""%>>3 hours</option>
                <option value="240" <%:  VoiceReceipient.DelayMinutes == 240?"selected":""%>>4 hours</option>
            </select>
            <% } %>
            <%}
              else
              {
                  foreach (NotificationRecipient nr in RecipientList.Where(m => { return m.CustomerToNotifyID == cust.CustomerID && m.NotificationType == eNotificationType.Phone; }))
                  {
                      nr.Delete();
                  }
              }%>
        </td>
        <%--Add Icons for iOS Device and Android Device notifications here--%>
    </tr>

    <%}%>
</table>
