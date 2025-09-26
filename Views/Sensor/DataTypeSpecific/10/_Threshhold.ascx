<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);

        long DefaultMin = 0;
        long DefaultMax = 0;
        
        MonnitApplicationBase.DefaultThresholds(Model, out DefaultMin, out DefaultMax);
        string unit = Ultrasonic.GetUnits(Model.SensorID).ToString();
        string label = Ultrasonic.GetUnits(Model.SensorID).ToString();
        if (label == "Inch")
        {
            label = "Inches";
        }
        else if(label == "Foot")
        {
            label = "Feet";
        }
        else
        {
            label = label + "s";
        }
        double minthresh = 0;
        double maxthresh = 0;
        double minUIVal = 0;
        double maxUIVal = 0;
        double step = 1;
        

        if (unit == "Centimeter")
        {
            minthresh = (DefaultMin * .1);
            maxthresh = (DefaultMax * .1);
            minUIVal = (Min.ToDouble() * .1);
            maxUIVal = (Max.ToDouble() * .1);
            step = 1;
        }

        if (unit == "Meter")
        {
            minthresh = (DefaultMin * .001);
            maxthresh = (DefaultMax * .001);
            minUIVal = (Min.ToDouble() * .001);
            maxUIVal = (Max.ToDouble() * .001);
            step = .1;
        }

        if (unit == "Inch")
        {
            minthresh = (DefaultMin * 0.0393701);
            maxthresh = (DefaultMax * 0.0393701);
            minUIVal = (Min.ToDouble() * .0393701);
            maxUIVal = (Max.ToDouble() * .0393701);
            step = 1;
        }

        if (unit == "Foot")
        {
            minthresh = (DefaultMin * .00328084);
            maxthresh = (DefaultMax * .00328084);
            minUIVal = (Min.ToDouble() * .00328084);
            maxUIVal = (Max.ToDouble() * .00328084);
            step = .1;
        }

        if (unit == "Yard")
        {
            minthresh = (DefaultMin * .00109361);
            maxthresh = (DefaultMax * .00109361);
            minUIVal = (Min.ToDouble() * .00109361);
            maxUIVal = (Max.ToDouble() * .00109361);
            step = .1;
        }

        if (unit == "Millimeter")
        {
            minthresh = DefaultMin;
            maxthresh = DefaultMax;
            minUIVal = Min.ToDouble();
            maxUIVal = Max.ToDouble();
            step = 10;
        }   
    
%>


<tr>
    <td style="width: 250px;">Use Aware State</td>
    <td>Below: <%: Html.TextBox("MinimumThreshold_Manual", Min, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>&nbsp;&nbsp;or&nbsp;&nbsp;
                            Above: <%: Html.TextBox("MaximumThreshold_Manual", Max, (Dictionary<string,object>)ViewData["HtmlAttributes"])%> <%:label %>
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
<%
       
                                
%>
<script type="text/javascript">
    

    $('#Threshold_Slider').slider({
        range: true,
        values: [<%:Min%>,<%:Max%>],
        min: <%:minthresh%>,
        max: <%:maxthresh%>,
        step:<%:step%>,
        <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#MinimumThreshold_Manual').val(ui.values[0]);
            $('#MaximumThreshold_Manual').val(ui.values[1]);
              
                
        }
              
    });
    
    

    $("#MinimumThreshold_Manual").addClass('editField editFieldMedium');
    $("#MaximumThreshold_Manual").addClass('editField editFieldMedium');
    $("#MinimumThreshold_Manual").change(function () {
        if (isANumber($("#MinimumThreshold_Manual").val())) {
            if ($("#MinimumThreshold_Manual").val() < <%:minthresh%>)
                $("#MinimumThreshold_Manual").val(<%:minthresh%>);
            if ($("#MinimumThreshold_Manual").val() > <%:(maxthresh)%>)
                $("#MinimumThreshold_Manual").val(<%:(maxthresh)%>);
            $('#Threshold_Slider').slider("option", "values", [$("#MinimumThreshold_Manual").val(),$("#MaximumThreshold_Manual").val()]);
        }
        else
        {
            $("#MinimumThreshold_Manual").val(<%:minUIVal%>);
            $('#Threshold_Slider').slider("option", "values", [$("#MinimumThreshold_Manual").val(),$("#MaximumThreshold_Manual").val()]);
        }

    });
    $("#MaximumThreshold_Manual").change(function () {
        if (isANumber($("#MaximumThreshold_Manual").val())) {
        if ($("#MaximumThreshold_Manual").val() < <%:minthresh%>)
            $("#MaximumThreshold_Manual").val(<%:minthresh%>);
        if ($("#MaximumThreshold_Manual").val() > <%:maxthresh%>)
            $("#MaximumThreshold_Manual").val(<%:maxthresh%>);
        $('#Threshold_Slider').slider("option", "values", [$("#MinimumThreshold_Manual").val(),$("#MaximumThreshold_Manual").val()]);
    }
    else
    {
            $("#MaximumThreshold_Manual").val(<%:maxUIVal%>);
    $('#Threshold_Slider').slider("option", "values", [$("#MinimumThreshold_Manual").val(),$("#MaximumThreshold_Manual").val()]);
    }
    });
</script>
<%} %>