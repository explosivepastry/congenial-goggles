<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.NotificationSchedule>" %>


<tr class="activeDay ActiveAllDay ActiveAllDay<%:Model.DayofWeek.ToString() %>">
    <td style="width: 212px; padding-left: 25px;" class="editor-label"><%:Model.DayofWeek.ToString() %>:</td>
    <td class="dropdownWidth ">


    <%: Html.DropDownList(Model.DayofWeek.ToString() + "Schedule.NotificationDaySchedule", Model.NotificationDaySchedule)%> 
        <%: Html.ValidationMessage(Model.DayofWeek.ToString() + "Schedule.NotificationDaySchedule") %>
         
    </td>
    <td></td>
</tr>

<tr  class="activeTime Before Between  Before_and_After <%: Model.DayofWeek.ToString() %>">
    <td style="text-align: right;"></td>
    <td>
        <div  class="Before Before_and_After  <%: Model.DayofWeek.ToString() %>" style="display: none;">Before</div>
        <%: Html.DropDownList(Model.DayofWeek.ToString()+"StartTimeHour", (SelectList)ViewData[Model.DayofWeek.ToString()+"StartHours"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:
                        <%: Html.DropDownList(Model.DayofWeek.ToString()+"StartTimeMinute", (SelectList)ViewData[Model.DayofWeek.ToString()+"StartMinutes"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.DropDownList(Model.DayofWeek.ToString()+"StartTimeAM", (SelectList)ViewData[Model.DayofWeek.ToString()+"StartAM"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>

    </td>
    <td></td>
</tr>


<tr  class=" Before_and_After After Between  <%: Model.DayofWeek.ToString() %> ">
    <td></td>
    <td colspan="2" ><span class="Between Before_and_After <%: Model.DayofWeek.ToString() %>">and</span> <span class="After Before_and_After <%: Model.DayofWeek.ToString() %>" style="display: none;">After</span></td>
</tr>


<tr class="activeTime Between Before_and_After After <%: Model.DayofWeek.ToString() %>">
    <td style="text-align: right;"></td>
    <td>
        
        <%: Html.DropDownList(Model.DayofWeek.ToString()+"EndTimeHour", (SelectList)ViewData[Model.DayofWeek.ToString()+"EndHours"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:
                        <%: Html.DropDownList(Model.DayofWeek.ToString()+"EndTimeMinute", (SelectList)ViewData[Model.DayofWeek.ToString()+"EndMinutes"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.DropDownList(Model.DayofWeek.ToString()+"EndTimeAM", (SelectList)ViewData[Model.DayofWeek.ToString()+"EndAM"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    </td>
    <td></td>
</tr>

<script type="text/javascript">


    $(function () {
        Switch($('#<%: Model.DayofWeek.ToString()%>Schedule\\.NotificationDaySchedule').val(), '<%: Model.DayofWeek.ToString()%>');
        if($('#AlwaysSend').not('checked')){
            Switch($('#<%: Model.DayofWeek.ToString()%>Schedule\\.NotificationDaySchedule').val(), '<%: Model.DayofWeek.ToString()%>');
        }
        $('#<%: Model.DayofWeek.ToString()%>Schedule\\.NotificationDaySchedule').live('change', function () {
            Switch($(this).val(), '<%: Model.DayofWeek.ToString()%>');
        });

    });


</script>
