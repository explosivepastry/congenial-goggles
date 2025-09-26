<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
if (!Model.GenerationType.Contains("2")) //This checks to see if Generation is Gen2// Not supported for Gen2/Alta until further notice 6/27/2017
{
    if (ViewData["ShowTimeOfDay"].ToBool())
    {%>
        <tr>
            <td>Sensor is on </td>
            <td>
               
                <%: Html.CheckBox("ActiveAllDay", Model.ActiveStartTime == Model.ActiveEndTime, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            </td>
            <td>
                <img alt="help" class="helpIcon" title="The time of day the sensor is actively working. No communication will be sent while sensor is hibernating." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
        </tr>
        <tr class="activeTime">
            <td></td>
            <td>
                <%: Html.DropDownList("ActiveStartTimeHour", (SelectList)ViewData["StartHours"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:
                <%: Html.DropDownList("ActiveStartTimeMinute", (SelectList)ViewData["StartMinutes"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                <%: Html.DropDownList("ActiveStartTimeAM", (SelectList)ViewData["StartAM"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            </td>
            <td></td>
        </tr>
        <tr class="activeTime">
            <td></td>
            <td colspan="2">and</td>
        </tr>
        <tr class="activeTime">
            <td></td>
            <td>
                <%: Html.DropDownList("ActiveEndTimeHour", (SelectList)ViewData["EndHours"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>:
                <%: Html.DropDownList("ActiveEndTimeMinute", (SelectList)ViewData["EndMinutes"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
                <%: Html.DropDownList("ActiveEndTimeAM", (SelectList)ViewData["EndAM"], (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            </td>
            <td></td>
        </tr>
  <%} 
}%>

<script type="text/javascript">
    $(document).ready(function () {

       setTimeout('$("#ActiveAllDay").iButton({ labelOn: "All Day" ,labelOff: "Between" });', 500);
        $('#ActiveAllDay').change(function () {
            if ($('#ActiveAllDay').prop('checked')) {
                //$(".activeTime :input").attr("disabled", true);
                $(".activeTime").hide();
                $('#ActiveStartTimeHour').val('12');
                $('#ActiveStartTimeMinute').val('00');
                $('#ActiveStartTimeAM').val('AM');
                $('#ActiveEndTimeHour').val('12');
                $('#ActiveEndTimeMinute').val('00');
                $('#ActiveEndTimeAM').val('AM');
            }
            else {
                //$(".activeTime :input").attr("disabled", $("#ActiveAllDay").attr("disabled"));
                $(".activeTime").show();
            }
        });
        $('#ActiveAllDay').change();
    });
</script>