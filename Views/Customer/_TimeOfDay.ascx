<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Customer>" %>


<style>
    .ActiveAllDay .dropdownWidth select {
        width:195px;
        margin-top: 0px;
    }
</style>



<tr class="AlwaysSend">
    <td>Schedule Notification Times</td>
    <td>
        <%: Html.CheckBox("AlwaysSend",Model.AlwaysSend, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Schedules notifications for the entire week." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
      
        <% Html.RenderPartial("TimeOfDayFolder/_CustomerSchedule", Model.MondaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_CustomerSchedule", Model.TuesdaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_CustomerSchedule", Model.WednesdaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_CustomerSchedule", Model.ThursdaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_CustomerSchedule", Model.FridaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_CustomerSchedule", Model.SaturdaySchedule);%>
        <% Html.RenderPartial("TimeOfDayFolder/_CustomerSchedule", Model.SundaySchedule);%>




