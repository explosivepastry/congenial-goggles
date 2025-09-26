<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.Notification>>" %>

    <table border="0" style="margin: 10px">

    <%//int j = 0;
      foreach (Notification item in Model.OrderBy(n=>n.Name)) {      
    %>
        <tr id="NotificationList<%:item.NotificationID %>">
            <td width="20">
                <input type="checkbox" id="notificationID_<%:item.NotificationID %>" />
            </td>
            <td>
                <label for="notificationID_<%:item.NotificationID %>"><img src="<%: Html.GetThemedContent(string.Format("/images/notification/class-{0}.png",item.NotificationClass))%>" class="typeIcon" alt="<%:item.NotificationClass %> Notification" /></label>
            </td>
            <td>
                <label for="notificationID_<%:item.NotificationID %>"><%: item.Name%></label>
            </td>
        </tr>
    <% } %>
    </table>