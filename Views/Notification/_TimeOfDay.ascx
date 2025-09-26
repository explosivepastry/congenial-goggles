<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>


<style>
    .ActiveAllDay .dropdownWidth select {
        width:195px;
        margin-top: 0px;
    }
</style>

<%if (ViewData["ShowSchedule"].ToBool())
  {%>
<tr class="AlwaysSend">
    <td width="238px">Schedule Notification Times</td>
    <td width="268px">
        <%: Html.CheckBox("AlwaysSend",Model.AlwaysSend, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Schedules notifications for the entire week." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<%if (ViewData["ShowTimeOfDay"].ToBool())
  {%>
        <% Html.RenderPartial("TimeOfDayFolder/_NotificationSchedule", Model.MondaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_NotificationSchedule", Model.TuesdaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_NotificationSchedule", Model.WednesdaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_NotificationSchedule", Model.ThursdaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_NotificationSchedule", Model.FridaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_NotificationSchedule", Model.SaturdaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_NotificationSchedule", Model.SundaySchedule);%>
<% } %>


<% } %>

