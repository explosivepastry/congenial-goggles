<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        
%>

<td style="width: 250px;">Use Aware State</td>
<td>Below: <%= Html.TextBox("MinimumThreshold_Manual", Min, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
           <%= Html.ValidationMessageFor(model => model.MinimumThreshold)%>&nbsp;&nbsp;or&nbsp;&nbsp;
   
     Above: <%= Html.TextBox("MaximumThreshold_Manual", Max, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
            <%= Html.ValidationMessageFor(model => model.MaximumThreshold)%>
</td>
<td>
    <img alt="help" class="helpIcon" title="Any assessments outside of these values will cause the sensor to enter the Aware State." src="../../Content/images/help.png" /></td>
</tr>
        <tr>
            <td></td>
            <td colspan="2">
                <div id="Threshold_Slider"></div>
            </td>
        </tr>


<script type="text/javascript">
    $('#Threshold_Slider').slider({
        range: true,
        values: [<%=Min%>,<%=Max%>],
                min: 0,
                max: 200,
                            <%=ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
                slide: function (event, ui) {
                    //update the amount by fetching the value in the value_array at index ui.value
                    $('#MinimumThreshold_Manual').val(ui.values[0]);
                    $('#MaximumThreshold_Manual').val(ui.values[1]);
                }
    });
    
            $("#MinimumThreshold_Manual").attr("style", "border: 0; width:25px;");
            $("#MaximumThreshold_Manual").attr("style", "border: 0; width:25px;");
            $("#MinimumThreshold_Manual").change(function () {
                if ($("#MinimumThreshold_Manual").val() < 0)
                    $("#MinimumThreshold_Manual").val('0');
                if ($("#MinimumThreshold_Manual").val() > 200)
                    $("#MinimumThreshold_Manual").val(200);
                $('#Threshold_Slider').slider("value", $("#MinimumThreshold_Manual").val());
            });
            $("#MaximumThreshold_Manual").change(function () {
                if ($("#MaximumThreshold_Manual").val() < 0)
                    $("#MaximumThreshold_Manual").val('0');
                if ($("#MaximumThreshold_Manual").val() > 200)
                    $("#MaximumThreshold_Manual").val(200);
                $('#Threshold_Slider').slider("value", $("#MaximumThreshold_Manual").val());
            });
</script>


<%} %>