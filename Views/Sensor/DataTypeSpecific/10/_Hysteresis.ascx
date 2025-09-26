<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Hyst = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
        string unitsforhyst = Ultrasonic.GetUnits(Model.SensorID).ToString();
        double minhyst = 0;
        double maxhyst = 0;
        double step = 1;
        string abbrv = Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(Model.SensorID));
       
        if (unitsforhyst == "Inch"||unitsforhyst == "Yard"||unitsforhyst == "Foot")
        {
            minhyst = 0;
            maxhyst = 3.9;
            step = .1;
        }

        if (unitsforhyst == "Millimeter" || unitsforhyst == "Centimeter" || unitsforhyst == "Meter")
        {
            minhyst = 0;
            maxhyst = 100;
            step = 1;
        }   
%>

<tr>
    <td style="width: 250px;">Aware State Buffer</td>
    <td>
        <%: Html.TextBox("Hysteresis_Manual", Hyst, (Dictionary<string,object>)ViewData["HtmlAttributes"])%> <%: abbrv == "mm"|| abbrv == "cm"|| abbrv == "M" ? "mm": "in" %>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.  For example, if a Maximum Threshold is set to 90° and a Aware State Buffer of 1°, then once the sensor takes an assessment of 90.0° and enters the Aware State it will remain in the Aware State until the temperature readings drop to 89.0°.  Similarly, at the Minimum Threshold the temperature will have to rise a degree above the threshold to return to Standard Operation." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="Hysteresis_Slider"></div>
    </td>
</tr>
<script type="text/javascript">
            
    $('#Hysteresis_Slider').slider({
        value: <%:Hyst%>,
                min: <%:minhyst%>,
                max: <%:(maxhyst)%>,
                step:<%:(step)%>,
                <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
                slide: function (event, ui) {
                    //update the amount by fetching the value in the value_array at index ui.value
                    $('#Hysteresis_Manual').val(ui.value);
                }
            });
    $("#Hysteresis_Manual").addClass('editField editFieldSmall');
    $("#Hysteresis_Manual").change(function () {
        if(isANumber($("#Hysteresis_Manual").val())){
            if ($("#Hysteresis_Manual").val() < <%:minhyst%>)
                $("#Hysteresis_Manual").val(<%:minhyst%>);
            if ($("#Hysteresis_Manual").val() >maxhyst)
                $("#Hysteresis_Manual").val(maxhyst)
            $('#Hysteresis_Slider').slider("value", $("#Hysteresis_Manual").val());
        }
        else
        {
            $("#Hysteresis_Manual").val(<%: Hyst%>);
            $('#Hysteresis_Slider').slider("value", $("#Hysteresis_Manual").val());
        }
            });
</script>
<%} %>