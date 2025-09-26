<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NotificationTriggered>>" %>


<div>
    <div class="formtitle">
        Triggered Notifications
        <span style="float: right;">Alerting : <%=Model.Where(c=>c.AcknowledgedBy == long.MinValue).Count() %>
        Pending : <%= Model.Where(c=>c.AcknowledgedTime != DateTime.MinValue && c.resetTime == DateTime.MinValue).Count() %>
            
        </span>
    </div>

    <table width="100%">
        <tr>
            <th></th>
            <th style="width: 120px">Triggered Date</th>
            <th>Last Reading Date</th>
            <th>Reading</th>
            <th></th>
            <th>Acknowledge</th>
            <th>Reset</th>
            <th></th>
        </tr>

        <%foreach (NotificationTriggered trig in Model)
          { %>
        <tr>

            <td></td>
            <td><%=trig.StartTime.ToLocalDateTimeShort() %></td>
            <td><%=trig.ReadingDate.ToLocalDateTimeShort() %></td>
            <td><%=trig.Reading %></td>
            <td></td>
            <% string acknowledgedByName = string.Empty;
               if (trig.AcknowledgedBy == long.MinValue)
               {
                   acknowledgedByName = string.IsNullOrEmpty(trig.AcknowledgeMethod) ? "System_Auto" : trig.AcknowledgeMethod;
               }
               else
               {
                   acknowledgedByName = Customer.Load(trig.AcknowledgedBy).FullName;
               }

               if (trig.AcknowledgedTime == DateTime.MinValue)
               {%>
            <td style="padding-left: 40px;">
                <a href="/Notification/AcknowledgeTriggeredNotification?triggeredID=<%=trig.NotificationTriggeredID%>&AckMethod=Browser_AlertDetails" onclick="AckButton(this);return false">
                    <img src="<%: Html.GetThemedContent("/images/acknowledge.png")%>" /></a>
            </td>
            <%}
                      else
                      { %>
            <td style="text-align: left;">
                <%= acknowledgedByName%><br />

                <%: trig.AcknowledgedTime.ToLocalDateTimeShort() %>
            </td>
            <%} %>
            <td>
                <a style="margin-right: 20%;" href="/Notification/FullReset?triggeredID=<%=trig.NotificationTriggeredID%>&AckMethod=Browser_FullReset" onclick="AckButton(this);return false;">
                    <img title="Click to force reset notification" src="<%: Html.GetThemedContent("/images/reset-alert.png")%>" /></a>
            </td>
            <td></td>
        </tr>
        <%} %>
    </table>

</div>
<script>

    $(function () {
        $('.status').tipTip();
    });



    function AckButton(anchor) {
        var href = $(anchor).attr('href');
        if (confirm('Are you sure you want to acknowledge this Notification?')) {
            $.post(href, function (data) {
                if (data == "Success") {
                    window.location.href = window.location.href;

                }
                else if (data == "Unauthorized") {
                    showSimpleMessageModal("<%=Html.TranslateTag("Unauthorized: User does not have permission to acknowledge notifications")%>");
                }
                else
                    showSimpleMessageModal("<%=Html.TranslateTag("Failed to acknowledge notification")%>");
            });
        }
    }

</script>
