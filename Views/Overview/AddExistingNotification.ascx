<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>



<%

    List<Notification> list = ViewBag.ExistingNotificaitons as List<Notification>;
    if (list == null || list.Count < 1)
    {%>
<div class="col-12">

    <b><a href="/Rule/ChooseType" class="btn btn-primary user-dets">
       <%=Html.GetThemedSVG("add") %>
        <%: Html.TranslateTag("Overview/AddExistingNotification|Create a new rule") %></a></b>
</div>
<%}
    else
    {
        foreach (var item in list)
        {
%>

<div class="gridPanel viewNotificationDetails d-flex align-items-center <%:item.NotificationID %> col-12">

    <div class="col-3 col-md-2" style="padding-top: 5px;">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off", "Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2 notiStatus<%:item.NotificationID%>" type="checkbox" onchange="SetStatus(<%:item.NotificationID%>,<%: item.IsActive ? "false" : "true" %>);" name="IsActive" id="IsActive" <%= item.IsActive ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On", "On")%></label>
        </div>
    </div>

    <div class="bold col-5 col-md-6" style="padding-top: 5px; padding-left: 10px;">
        <a href="/Rule/History/<%=item.NotificationID %>"><%= item.Name%></a>
    </div>

    <div class="col-3" style="padding-top: 5px;">
        <%if (item.LastSent > DateTime.MinValue)
            { %>
        <strong><%: Html.TranslateTag("Overview/AddExistingNotification|Last Sent", "Last Sent") %>:</strong>
        <br />
        <%: item.LastSent.OVToLocalDateTimeShort()%>
        <%}
            else
            { %>

        <%} %>
    </div>

    <div class="col-1 text-end" style="padding-top: 5px;">
        <%if (MonnitSession.CustomerCan("Notification_Edit"))
            { %>

        <a href="Remove" onclick="removeNotification(<%:item.NotificationID %>); return false;" class="icon icon-remove"></a>

        <%} %>
    </div>
</div>
<% }
} %>

<script type="text/javascript">

    function SetStatus(notiID, setActive) {


        $.post("/Rule/ToggleRule/", { id: notiID, active: setActive }, function (data) {
            if (data != "Success") {
                console.log(data);
                showSimpleMessageModal("<%=Html.TranslateTag("Oops! That didn't work, please refresh your page. If this error continues, contact support.")%>");
            }
        });

    }

</script>
