<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Notification>" %>

<style type="text/css">
    .ActiveAllDay .dropdownWidth select {
        width: 195px;
        margin-top: 0px;
    }
</style>

<%if (ViewData["ShowSchedule"].ToBool())
  {%>
    <tr class="AlwaysSend dffdc">
        <td><%: Html.TranslateTag("Rule/_TimeOfDay|Rule Schedule Time","Rule Schedule Time")%></td>
        <td  class="schedule-time-toggle">
           <div class="form-check form-switch d-flex ps-2">
                <label class="form-check-label" ><%: Html.TranslateTag("Schedule","Schedule")%></label>
                <input class="form-check-input mx-2" type="checkbox" name="AlwaysSend" id="AlwaysSend" <%= Model.AlwaysSend ? "checked" : "" %>>
                <label class="form-check-label" ><%: Html.TranslateTag("Always","Always")%></label>
            </div>
        </td>
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

