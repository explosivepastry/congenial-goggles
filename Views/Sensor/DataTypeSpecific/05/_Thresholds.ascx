<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
    
%>


<tr>
    <td style="width: 250px;">
        <h3>Use Aware State</h3>
    </td>
</tr>
<tr>
    <td>Minimum and Maximum thresholds by percent:</td>
    <td>Below: <%: Html.TextBox("MinimumThreshold_Manual", Min, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>%
                            <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>&nbsp;&nbsp;or&nbsp;&nbsp;
                            Above: <%: Html.TextBox("MaximumThreshold_Manual", Max, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>%
                            <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Any assessments outside of these values will cause the sensor to enter the Aware State." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
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
        values: [<%:Min%>,<%:Max%>],
        min: 0,
        max: 100,
                <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#MinimumThreshold_Manual').val(ui.values[0]);
            $('#MaximumThreshold_Manual').val(ui.values[1]);
        }
    });
    $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');
    $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');
    $("#MinimumThreshold_Manual").change(function () {
        if (isANumber($("#MinimumThreshold_Manual").val())) {
            if ($("#MinimumThreshold_Manual").val() < 0)
                $("#MinimumThreshold_Manual").val(0);
            if ($("#MinimumThreshold_Manual").val() > 100)
                $("#MinimumThreshold_Manual").val(100);
            $('#Threshold_Slider').slider("option", "values", [$("#MinimumThreshold_Manual").val(), $("#MaximumThreshold_Manual").val()]);
        }
        else {
            $('#MinimumThreshold_Manual').val(<%: Min%>);
            $('#Threshold_Slider').slider("option", "values", [$("#MinimumThreshold_Manual").val(), $("#MaximumThreshold_Manual").val()]);
        }
    });

    $("#MaximumThreshold_Manual").change(function () {
        if (isANumber($("#MaximumThreshold_Manual").val())) {
            if ($("#MaximumThreshold_Manual").val() < 0)
                $("#MaximumThreshold_Manual").val(0);
            if ($("#MaximumThreshold_Manual").val() > 100)
                $("#MaximumThreshold_Manual").val(100);
            $('#Threshold_Slider').slider("option", "values", [$("#MinimumThreshold_Manual").val(), $("#MaximumThreshold_Manual").val()]);
        }
        else
        {
            $('#MaximumThreshold_Manual').val(<%: Max%>);
            $('#Threshold_Slider').slider("option", "values", [$("#MinimumThreshold_Manual").val(), $("#MaximumThreshold_Manual").val()]);
        }
    });
</script>
<%} %>