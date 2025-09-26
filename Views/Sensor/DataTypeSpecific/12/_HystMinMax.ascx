<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    
    double tempHyst = Gas_CO.TemperatureHysteresis(Model);
    double tempMin = Gas_CO.TemperatureMinimumThreshold(Model);
    double tempMax = Gas_CO.TemperatureMaximumThreshold(Model);

    if (Gas_CO.IsFahrenheit(Model.SensorID))
    {
        tempHyst = tempHyst * 9 / 5;
        tempMin = tempMin.ToFahrenheit();
        tempMax = tempMax.ToFahrenheit();
    }

    int conHyst = Gas_CO.ConcentrationHysteresis(Model);
    int conMin = Gas_CO.ConcentrationMinimumThreshold(Model);
    int conMax = Gas_CO.ConcentrationMaximumThreshold(Model);

    int twaHyst = Gas_CO.AverageHysteresis(Model);
    int twaMin = Gas_CO.AverageMinimumThreshold(Model);
    int twaMax = Gas_CO.AverageMaximumThreshold(Model);

   
    
%>
<%-- 
<tr class="Thres34 Thres34_All">
    <td>Use Aware State When Temperature </td>
    <td>Below: <%: Html.TextBox("tempMinimumThreshold_Manual", tempMin, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>°&nbsp;&nbsp;or&nbsp;&nbsp;
        Above: <%: Html.TextBox("tempMaximumThreshold_Manual", tempMax, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>°
    </td>
</tr>
<tr class="Thres34 Thres34_All">
    <td></td>
    <td colspan="2">
        <div id="tempThreshold_Slider"></div>
    </td>
</tr>
<tr class="Thres34 Thres34_All">
    <td>Temperature Aware State Buffer</td>
    <td>
        <%: Html.TextBox("tempHysteresis_Manual", tempHyst, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>°
    </td>
</tr>
--%>

<tr class="Thres34 Thres34_All Thres34_Concentration">
    <td>Use Aware State When Concentration</td>
    <td>Below: <%: Html.TextBox("conMinimumThreshold_Manual", conMin, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>ppm&nbsp;&nbsp;or&nbsp;&nbsp;
        Above: <%: Html.TextBox("conMaximumThreshold_Manual", conMax, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>ppm
    </td>
</tr>
<tr class="Thres34 Thres34_All Thres34_Concentration">
    <td></td>
    <td colspan="2">
        <div id="conThreshold_Slider"></div>
    </td>
</tr>
<tr class="Thres34 Thres34_All Thres34_Concentration">
    <td>Concentration Aware State Buffer</td>
    <td>
        <%: Html.TextBox("conHysteresis_Manual", conHyst, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>ppm
    </td>
</tr>

<tr class="Thres34 Thres34_All Thres34_Time_Weighted_Average">
    <td>Use Aware State When Time Weighted Average</td>
    <td>Below: <%: Html.TextBox("twaMinimumThreshold_Manual", twaMin, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>ppm&nbsp;&nbsp;or&nbsp;&nbsp;
        Above: <%: Html.TextBox("twaMaximumThreshold_Manual", twaMax, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>ppm
    </td>
</tr>
<tr class="Thres34 Thres34_All Thres34_Time_Weighted_Average">
    <td></td>
    <td colspan="2">
        <div id="twaThreshold_Slider"></div>
    </td>
</tr>
<tr class="Thres34 Thres34_All Thres34_Time_Weighted_Average">
    <td>Time Weighted Average Aware State Buffer</td>
    <td>
        <%: Html.TextBox("twaHysteresis_Manual", twaHyst, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>ppm
    </td>
</tr>
<%
   
    var tempDefaultMin = Gas_CO.TemperatureDefaultMinThreshold / 10.0;
    var tempDefaultMax = Gas_CO.TemperatureDefaultMaxThreshold / 10.0;
    var twaDefaultMin = Gas_CO.AverageDefaultMinThreshold;
    var twaDefaultMax = Gas_CO.AverageDefaultMaxThreshold;
    var conDefaultMin = Gas_CO.ConcentrationDefaultMinThreshold;
    var conDefaultMax = Gas_CO.ConcentrationDefaultMaxThreshold;

    if (Gas_CO.IsFahrenheit(Model.SensorID))
    {
        var tdmin = tempDefaultMin.ToDouble().ToFahrenheit().ToInt();
        var tdmax = tempDefaultMax.ToDouble().ToFahrenheit().ToInt();
        tempDefaultMax = (Int16)tdmax;
        tempDefaultMin = (Int16)tdmin;
    }
                                
%>
<script type="text/javascript">
    $('#tempThreshold_Slider').slider({
        range: true,
        values: [<%:tempMin%>,<%:tempMax%>],
        min: <%:tempDefaultMin%>,
        max: <%:tempDefaultMax%>,
                            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#tempMinimumThreshold_Manual').val(ui.values[0]);
            $('#tempMaximumThreshold_Manual').val(ui.values[1]);
        }
    });
    $("#tempMinimumThreshold_Manual").addClass('editField editFieldSmall');
    $("#tempMaximumThreshold_Manual").addClass('editField editFieldSmall');
    $("#tempHysteresis_Manual").addClass('editField editFieldSmall');

    $("#tempHysteresis_Manual").change(function () {
        if (isANumber($("#tempHysteresis_Manual").val())){
            if($("#tempHysteresis_Manual").val() <0)
                $("#tempHysteresis_Manual").val(0);
            if($("#tempHysteresis_Manual").val()>5)
                $("#tempHysteresis_Manual").val(5);     
        }
        else
            $("#tempHysteresis_Manual").val(<%: conHyst%>);
    });

    $("#tempMinimumThreshold_Manual").change(function () {
        if (isANumber($("#tempMinimumThreshold_Manual").val())){
            if ($("#tempMinimumThreshold_Manual").val() < <%:(tempDefaultMin)%>)
                $("#tempMinimumThreshold_Manual").val(<%:(tempDefaultMin)%>);
            if ($("#tempMinimumThreshold_Manual").val() > <%:(tempDefaultMax)%>)
                $("#tempMinimumThreshold_Manual").val(<%:(tempDefaultMax)%>);
            $('#tempThreshold_Slider').slider("option", "values", [$("#tempMinimumThreshold_Manual").val(),$("#tempMaximumThreshold_Manual").val()]);
           
        }
        else
        {
            $("#tempMinimumThreshold_Manual").val(<%:(tempDefaultMin)%>);
            $('#tempThreshold_Slider').slider("option", "values", [$("#tempMinimumThreshold_Manual").val(),$("#tempMaximumThreshold_Manual").val()]);
        }
    });
    $("#tempMaximumThreshold_Manual").change(function () {
        if ($("#tempMaximumThreshold_Manual").val() < <%:(tempDefaultMin)%>)
            $("#tempMaximumThreshold_Manual").val(<%:(tempDefaultMin)%>);
        if ($("#tempMaximumThreshold_Manual").val() > <%:(tempDefaultMax)%>)
            $("#tempMaximumThreshold_Manual").val(<%:(tempDefaultMax)%>);
        $('#tempThreshold_Slider').slider("option", "values", [$("#tempMinimumThreshold_Manual").val(),$("#tempMaximumThreshold_Manual").val()]);
    });


    $('#conThreshold_Slider').slider({
        range: true,
        values: [<%:conMin%>,<%:conMax%>],
        min: <%:conDefaultMin%>,
        max: <%:conDefaultMax%>,
                            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#conMinimumThreshold_Manual').val(ui.values[0]);
            $('#conMaximumThreshold_Manual').val(ui.values[1]);
        }
    });
    $("#conMinimumThreshold_Manual").addClass('editField editFieldSmall');
    $("#conMaximumThreshold_Manual").addClass('editField editFieldSmall');
    $("#conHysteresis_Manual").addClass('editField editFieldSmall');

    $("#conHysteresis_Manual").change(function () {
        if (isANumber($("#conHysteresis_Manual").val())){
            if($("#conHysteresis_Manual").val() <0)
                $("#conHysteresis_Manual").val(0);
            if($("#conHysteresis_Manual").val()>5)
                $("#conHysteresis_Manual").val(5);     
        }
        else
            $("#conHysteresis_Manual").val(<%: conHyst%>);
    });

$("#conMinimumThreshold_Manual").change(function () {
    if (isANumber($("#conMinimumThreshold_Manual").val())){
        if ($("#MinimumThreshold_Manual").val() < <%:(conDefaultMin)%>)
                                  $("#conMinimumThreshold_Manual").val(<%:(conDefaultMin)%>);
            if ($("#conMinimumThreshold_Manual").val() > <%:(conDefaultMax)%>)
                  $("#conMinimumThreshold_Manual").val(<%:(conDefaultMax)%>);
        $('#conThreshold_Slider').slider("option", "values", [$("#conMinimumThreshold_Manual").val(),$("#conMaximumThreshold_Manual").val()]);
        }
        else
        { 
            $("#conMinimumThreshold_Manual").val(<%: conMin%>);
        $('#conThreshold_Slider').slider("option", "values", [$("#conMinimumThreshold_Manual").val(),$("#conMaximumThreshold_Manual").val()]);
        }
    });

    $("#conMaximumThreshold_Manual").change(function () {
        if (isANumber($("#conMaximumThreshold_Manual").val())){
            if ($("#conMaximumThreshold_Manual").val() < <%:(conDefaultMin)%>)
                                    $("#conMaximumThreshold_Manual").val(<%:(conDefaultMin)%>);
            if ($("#conMaximumThreshold_Manual").val() > <%:(conDefaultMax)%>)
                        $("#conMaximumThreshold_Manual").val(<%:(conDefaultMax)%>);
            $('#conThreshold_Slider').slider("option", "values", [$("#conMinimumThreshold_Manual").val(),$("#conMaximumThreshold_Manual").val()]);
        }
        else
        {
            $("#conMaximumThreshold_Manual").val(<%: conMax%>);
            $('#conThreshold_Slider').slider("option", "values", [$("#conMinimumThreshold_Manual").val(),$("#conMaximumThreshold_Manual").val()]);
        }
    });


    $('#twaThreshold_Slider').slider({
        range: true,
        values: [<%:twaMin%>,<%:twaMax%>],
          min: <%:twaDefaultMin%>,
          max: <%:twaDefaultMax%>,
                            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
          slide: function (event, ui) {
              //update the amount by fetching the value in the value_array at index ui.value
              $('#twaMinimumThreshold_Manual').val(ui.values[0]);
              $('#twaMaximumThreshold_Manual').val(ui.values[1]);
          }
      });
      $("#twaMinimumThreshold_Manual").addClass('editField editFieldSmall');
      $("#twaMaximumThreshold_Manual").addClass('editField editFieldSmall');
      $("#twaHysteresis_Manual").addClass('editField editFieldSmall');

      $("#twaHysteresis_Manual").change(function () {
          if (isANumber($("#twaHysteresis_Manual").val())){
              if($("#twaHysteresis_Manual").val() <0)
                  $("#twaHysteresis_Manual").val(0);
              if($("#twaHysteresis_Manual").val()>5)
                  $("#twaHysteresis_Manual").val(5);     
          }
          else
              $("#twaHysteresis_Manual").val(<%: conHyst%>);
       });

$("#twaMinimumThreshold_Manual").change(function () {
    if (isANumber($("#twaMinimumThreshold_Manual").val())){
        if ($("#twaMinimumThreshold_Manual").val() < <%:(twaDefaultMin)%>)
                                    $("#twaMinimumThreshold_Manual").val(<%:twaDefaultMin%>);
            if ($("#twaMinimumThreshold_Manual").val() > <%:(twaDefaultMax)%>)
                  $("#twaMinimumThreshold_Manual").val(<%:(twaDefaultMax)%>);
        $('#twaThreshold_Slider').slider("option", "values", [$("#twaMinimumThreshold_Manual").val(),$("#twaMaximumThreshold_Manual").val()]);
        }
        else
        {
            $("#twaMinimumThreshold_Manual").val(<%: twaMin%>);
        $('#twaThreshold_Slider').slider("option", "values", [$("#twaMinimumThreshold_Manual").val(),$("#twaMaximumThreshold_Manual").val()]);
        }
    });

    $("#twaMaximumThreshold_Manual").change(function () {
        if (isANumber($("#twaMaximumThreshold_Manual").val())){
            if ($("#twaMaximumThreshold_Manual").val() < <%:(twaDefaultMin)%>)
                                    $("#twaMaximumThreshold_Manual").val(<%:(twaDefaultMin)%>);
            if ($("#twaMaximumThreshold_Manual").val() > <%:(twaDefaultMax)%>)
                        $("#twaMaximumThreshold_Manual").val(<%:(twaDefaultMax)%>);
            $('#twaThreshold_Slider').slider("option", "values", [$("#twaMinimumThreshold_Manual").val(),$("#twaMaximumThreshold_Manual").val()]);
        }
        else
        {
            $("#twaMaximumThreshold_Manual").val(<%: twaMax%>);
            $('#twaThreshold_Slider').slider("option", "values", [$("#twaMinimumThreshold_Manual").val(),$("#twaMaximumThreshold_Manual").val()]);
        }
    });
</script>

