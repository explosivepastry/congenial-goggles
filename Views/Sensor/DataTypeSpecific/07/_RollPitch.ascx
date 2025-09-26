<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
      
    int pitchMin = Tilt.GetPitchMin(Model);
    int pitchMax = Tilt.GetPitchMax(Model);
    int rollMin = Tilt.GetRollMin(Model);
    int rollMax = Tilt.GetRollMax(Model);
    string hyst = Tilt.HystForUI(Model);
    
%>
<tr>
    <td>
        <h3>Aware State</h3>
    </td>
    <td></td>
    <td></td>
</tr>
<tr>
    <td>Pitch Threshold</td>
    <td>Below: <%: Html.TextBox("pitchMin_Manual", pitchMin, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>°&nbsp;&nbsp;or&nbsp;&nbsp;
        Above: <%: Html.TextBox("pitchMax_Manual", pitchMax, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>°
    </td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="pitchThreshold_Slider"></div>
    </td>
</tr>

<tr>
    <td>Roll Threshold</td>
    <td>Below: <%: Html.TextBox("rollMin_Manual", rollMin, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>°&nbsp;&nbsp;or&nbsp;&nbsp;
        Above: <%: Html.TextBox("rollMax_Manual", rollMax, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>°
    </td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="rollThreshold_Slider"></div>
    </td>
</tr>
<tr>
    <td>Aware State Buffer</td>
    <td>
        <%: Html.TextBox("Hysteresis_Manual", hyst, (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>°
    </td>
</tr>
<script type="text/javascript">
    //pitch

    $("#pitchMin_Manual").addClass('editField editFieldSmall aSettings__input_input');
    $("#pitchMax_Manual").addClass('editField editFieldSmall aSettings__input_input');
    $("#rollMin_Manual").addClass('editField editFieldSmall aSettings__input_input');
    $("#rollMax_Manual").addClass('editField editFieldSmall aSettings__input_input');
    $("#Hysteresis_Manual").addClass('editField editFieldSmall aSettings__input_input');

    $('#Hysteresis_Manual').change(function () {
        if (isANumber($("#Hysteresis_Manual").val())) {
            if ($('#Hysteresis_Manual').val() < 0)
                $('#Hysteresis_Manual').val(0);

            if ($('#Hysteresis_Manual').val() > 5)
                $('#Hysteresis_Manual').val(5);
        }
        else {
            $('#Hysteresis_Manual').val(<%: hyst%>);
        }

    });

    $('#pitchThreshold_Slider').slider({
        range: true,
        values: [<%:pitchMin%>,<%:pitchMax%>],
        min: -180,
        max: 180,
                            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#pitchMin_Manual').val(ui.values[0]);
            $('#pitchMax_Manual').val(ui.values[1]);
        }
    });

   
    $("#pitchMin_Manual").change(function () {
        if (isANumber($("#pitchMin_Manual").val())) {
            if ($("#pitchMin_Manual").val() < -180)
                $("#pitchMin_Manual").val(-180);
            if ($("#pitchMin_Manual").val() > 180)
                $("#pitchMin_Manual").val(180);
            $('#pitchThreshold_Slider').slider("option", "values", [$("#pitchMin_Manual").val(), $("#pitchMax_Manual").val()]);
           
        }
        else {
            $("#pitchMin_Manual").val(<%: pitchMin%>);
            $('#pitchThreshold_Slider').slider("option", "values", [$("#pitchMin_Manual").val(), $("#pitchMax_Manual").val()]);
        }
    });
    $("#pitchMax_Manual").change(function () {
        if (isANumber($("#pitchMax_Manual").val())) {
            if ($("#pitchMax_Manual").val() < -180)
                $("#pitchMax_Manual").val(-180);
            if ($("#pitchMax_Manual").val() > 180)
                $("#pitchMax_Manual").val(180);
            $('#pitchThreshold_Slider').slider("option", "values", [$("#pitchMin_Manual").val(), $("#pitchMax_Manual").val()]);
        }
        else {
            $("#pitchMax_Manual").val(<%: pitchMax%>);
            $('#pitchThreshold_Slider').slider("option", "values", [$("#pitchMin_Manual").val(), $("#pitchMax_Manual").val()]);
        }
    });
    //roll
    $('#rollThreshold_Slider').slider({
        range: true,
        values: [<%:rollMin%>,<%:rollMax%>],
        min: -180,
        max: 180,
                            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#rollMin_Manual').val(ui.values[0]);
            $('#rollMax_Manual').val(ui.values[1]);
        }
    });


    $("#rollMin_Manual").change(function () {
        if (isANumber($("#rollMin_Manual").val())) {
            if ($("#rollMin_Manual").val() < -180)
                $("#rollMin_Manual").val(-180);
            if ($("#rollMin_Manual").val() > 180)
                $("#rollMin_Manual").val(180);
            $('#rollThreshold_Slider').slider("option", "values", [$("#rollMin_Manual").val(), $("#rollMax_Manual").val()]);
        }
        else {
            $("#rollMin_Manual").val(<%: rollMin%>);
            $('#rollThreshold_Slider').slider("option", "values", [$("#rollMin_Manual").val(), $("#rollMax_Manual").val()]);
        }
    });

    $("#rollMax_Manual").change(function () {
        if (isANumber($("#rollMax_Manual").val())) {
            if ($("#rollMax_Manual").val() < -180)
                $("#rollMax_Manual").val(-180);
            if ($("#rollMax_Manual").val() > 180)
                $("#rollMax_Manual").val(180);
            $('#rollThreshold_Slider').slider("option", "values", [$("#rollMin_Manual").val(), $("#rollMax_Manual").val()]);
        }
        else {
            $("#rollMax_Manual").val(<%: rollMax%>);
            $('#rollThreshold_Slider').slider("option", "values", [$("#rollMin_Manual").val(), $("#rollMax_Manual").val()]);
        }
    });
    $("#rollMin_Manual").addClass("aSettings__input_input");

</script>
