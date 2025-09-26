<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<%="" %>

<%if (!String.IsNullOrEmpty(Model.CompareValue))
    {
        if (Model.NotificationByTimeID > 0)
        {
            string scheduledTime = new DateTime(2022, 1, 1, Model.NotificationByTime.ScheduledHour, Model.NotificationByTime.ScheduledMinute, 0).ToTimeFormatted(MonnitSession.CurrentCustomer.Preferences["Time Format"]);
            %>
<%= Html.TranslateTag("Condition")%>: <%: Html.TranslateTag("Trigger at ") + scheduledTime %>
<%}
    else if (Model.NotificationClass == eNotificationClass.Application)
    {
        string ViewToFind = string.Format("DataTypeSpecific\\{0}\\_ConditionDatumEventSentenceDisplay", Model.eDatumType.ToInt().ToString("D2"));
        if (MonnitViewEngine.CheckPartialViewExists(ViewContext, ViewToFind, "Sensor", MonnitSession.CurrentTheme.Theme))
            Html.RenderPartial("~/Views/Sensor/" + ViewToFind + ".ascx", Model);
        else
            Html.RenderPartial("~/Views/Sensor/DataTypeSpecific/Default/_ConditionDatumEventSentenceDisplay.ascx", Model);

    }
    else if (Model.NotificationClass == eNotificationClass.Low_Battery)
    {%>
<%= Html.TranslateTag("Condition")%>: <%= Html.TranslateTag("Battery is below ")%>  <%=Model.CompareValue %> %
<%}
    else if (Model.NotificationClass == eNotificationClass.Inactivity)
    {%>
<%= Html.TranslateTag("Condition")%>: <%: Html.TranslateTag("Inactive for greater than ")%><%=Model.CompareValue %> <%= Html.TranslateTag("minutes")%>
<%}
    else if (Model.NotificationClass == eNotificationClass.Advanced)
    {
        AdvancedNotification advNtfc = AdvancedNotification.Load(Model.AdvancedNotificationID);
%>
<%= Html.TranslateTag("Condition")%>: <%: advNtfc == null ? Html.TranslateTag("Reading meets Advanced Parameters") : advNtfc.Name %>
<%}%>

<%}%>
