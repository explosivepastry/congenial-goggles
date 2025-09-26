<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.NotificationSchedule>" %>

<tr class="activeDay ActiveAllDay ActiveAllDay<%:Model.DayofWeek.ToString() %>" style="display: flex; flex-direction: column;">
    <td style="width: 150px; padding-left: 25px;" class="editor-label"><%: Html.TranslateTag(Model.DayofWeek.ToString()) %>:</td>
    <td class="dropdownWidth">
        
        <select id="<%=Model.DayofWeek.ToString() %>Schedule.NotificationDaySchedule" name="<%=Model.DayofWeek.ToString() %>Schedule.NotificationDaySchedule" class="form-select mgn-tb-10">
            <option <%=Model.NotificationDaySchedule == eNotificationDaySchedule.All_Day ? "selected='selected'" : ""%> value="All_Day"><%: Html.TranslateTag("All Day","All Day")%></option>
            <option <%=Model.NotificationDaySchedule == eNotificationDaySchedule.Off ? "selected='selected'" : ""%> value="Off"><%: Html.TranslateTag("Off","Off")%></option>
            <option <%=Model.NotificationDaySchedule == eNotificationDaySchedule.Between ? "selected='selected'" : ""%> value="Between"><%: Html.TranslateTag("Between","Between")%></option>
            <option <%=Model.NotificationDaySchedule == eNotificationDaySchedule.Before_and_After ? "selected='selected'" : ""%> value="Before_and_After"><%: Html.TranslateTag("Before and After","Before and After")%></option>
            <option <%=Model.NotificationDaySchedule == eNotificationDaySchedule.Before ? "selected='selected'" : ""%> value="Before"><%: Html.TranslateTag("Before","Before")%></option>
            <option <%=Model.NotificationDaySchedule == eNotificationDaySchedule.After ? "selected='selected'" : ""%> value="After"><%: Html.TranslateTag("After","After")%></option>
        </select>
        
        <%: Html.ValidationMessage(Model.DayofWeek.ToString() + "Schedule.NotificationDaySchedule") %>

    </td>
    <td>
  </td>
</tr>

<tr class="activeTime Before Between Before_and_After <%: Model.DayofWeek.ToString() %>">
    <td class="ps-sm-5">
        <div class="Before Before_and_After  <%: Model.DayofWeek.ToString() %>" style="display: none;"><%: Html.TranslateTag("Before","Before")%></div>
        <div class="d-flex align-items-center">
            <%: Html.DropDownList(Model.DayofWeek.ToString()+"StartTimeHour", (SelectList)ViewData[Model.DayofWeek.ToString()+"StartHours"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:
            <%: Html.DropDownList(Model.DayofWeek.ToString()+"StartTimeMinute", (SelectList)ViewData[Model.DayofWeek.ToString()+"StartMinutes"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            <%: Html.DropDownList(Model.DayofWeek.ToString()+"StartTimeAM", (SelectList)ViewData[Model.DayofWeek.ToString()+"StartAM"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        </div>
    </td>
    <td></td>
</tr>

<tr class="Before_and_After After Between <%: Model.DayofWeek.ToString() %> ">
    <td colspan="2" class="ps-sm-5"><span class="Between Before_and_After <%: Model.DayofWeek.ToString() %>"><%: Html.TranslateTag("and","and")%></span> <span class="After Before_and_After <%: Model.DayofWeek.ToString() %>" style="display: none;"><%: Html.TranslateTag("After","After")%></span></td>
</tr>

<tr class="activeTime Between Before_and_After After <%: Model.DayofWeek.ToString() %>">
    <td class="d-flex align-items-center ms-sm-5">
        <%: Html.DropDownList(Model.DayofWeek.ToString()+"EndTimeHour", (SelectList)ViewData[Model.DayofWeek.ToString()+"EndHours"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:
        <%: Html.DropDownList(Model.DayofWeek.ToString()+"EndTimeMinute", (SelectList)ViewData[Model.DayofWeek.ToString()+"EndMinutes"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.DropDownList(Model.DayofWeek.ToString()+"EndTimeAM", (SelectList)ViewData[Model.DayofWeek.ToString()+"EndAM"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    </td>
    <td></td>
</tr>

<script type="text/javascript">
    $(function () {
        Switch($('#<%: Model.DayofWeek.ToString()%>Schedule\\.NotificationDaySchedule').val(), '<%: Model.DayofWeek.ToString()%>');

        if ($('#AlwaysSend').not('checked')) {

            Switch($('#<%: Model.DayofWeek.ToString()%>Schedule\\.NotificationDaySchedule').val(), '<%: Model.DayofWeek.ToString()%>');
        }
        $('#<%: Model.DayofWeek.ToString()%>Schedule\\.NotificationDaySchedule').on('change', function () {
            Switch($(this).val(), '<%: Model.DayofWeek.ToString()%>');
        });
    });
    $(document).ready(function () {
        $("select").addClass("form-select");
    });
    $(document).ready(function () {
        $("select").addClass("mgn-tb-10");
    });

</script>
