<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string Min = "";
        string Max = "";

        MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Min, out Max);
        
%>
<tr>
    <td>Sensitivity Threshold</td>
    <td><%: Html.TextBox("MaximumThreshold_Manual", Max, (Dictionary<string, object>)ViewData["HtmlAttributes"])%>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Sensitivity Threshold: A vehicle is counted when magnitude goes above then below this value. " src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="Threshold_Slider"></div>
    </td>
</tr>

<script type="text/javascript">
           
        
    $('#Threshold_Slider').slider({
        range: false,
        value: <%:Max%>,
        min: 10,
        max: 3000,
                            <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            //$('#MinimumThreshold_Manual').val(ui.values[0]);
            $('#MaximumThreshold_Manual').val(ui.value);
        }
    });

    $("#MaximumThreshold_Manual").addClass("editField editFieldMedium");

    //$("#MinimumThreshold_Manual").addClass('editField editFieldSmall');
    //$("#MaximumThreshold_Manual").addClass('editField editFieldSmall');
    //$("#MinimumThreshold_Manual").change(function () {
    //    if ($("#MinimumThreshold_Manual").val() < 0)
    //        $("#MinimumThreshold_Manual").val('0');
    //    if ($("#MinimumThreshold_Manual").val() > 200)
    //        $("#MinimumThreshold_Manual").val(200);
    //    $('#Threshold_Slider').slider("value", $("#MinimumThreshold_Manual").val());
    //});
    $("#MaximumThreshold_Manual").change(function () {
        if (isANumber($("#MaximumThreshold_Manual").val())) {
            if ($("#MaximumThreshold_Manual").val() < 10)
                $("#MaximumThreshold_Manual").val(10);
            if ($("#MaximumThreshold_Manual").val() > 3000)
                $("#MaximumThreshold_Manual").val(3000);
            $('#Threshold_Slider').slider("value", $("#MaximumThreshold_Manual").val());
        }
        else
        {
            $('#MaximumThreshold_Manual').val(<%: Max%>);
            $('#Threshold_Slider').slider("value", $("#MaximumThreshold_Manual").val());
        }
    });
</script>
<%
        if (new Version(Model.FirmwareVersion) >= new Version("2.3.0.0"))
        {
            string Hyst = "";

            MonnitApplicationBase.ProfileSettingsForIntervalUI(Model, out Hyst);
%>

<tr>
    <td style="width: 250px;">Sensitivity Buffer</td>
    <td>
        <%: Html.TextBox("Hysteresis_Manual", Hyst, (Dictionary<string, object>)ViewData["HtmlAttributes"])%>%
                <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </td>
    <td>
        <img alt="help" class="helpIcon" title="Threshold Buffer: After detecting a magnitude above the Sensitivity Threshold the sensor must detect a magnitude less than Sensitivity Threshold - Threshold Buffer to count the vehicle." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
<tr>
    <td></td>
    <td colspan="2">
        <div id="Hysteresis_Slider"></div>
    </td>
</tr>

 <tr>
       <td>Aware State Threshold</td>
    <td> <%: Html.TextBox("MinimumThreshold_Manual", Min, (Dictionary<string, object>)ViewData["HtmlAttributes"])%> Vehicles
            <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
</td>
<td>
    <img alt="help" class="helpIcon" title=" Greater than or equal number of vehicles will cause sensor to go into aware state." src="<%:Html.GetThemedContent("/images/help.png")%>" /></td>
</tr>
        <tr>
            <td></td>
            <td colspan="2">
                <div id="AwareStateThreash_Slider"></div>
            </td>
        </tr>

<script type="text/javascript">

    var recovery_array = [0,1,5,10,15,30,45,50];
    function getRecoveryIndex() {
        var retval = 0;
        $.each(recovery_array, function (index, value) {
            if (value <= $("#Hysteresis_Manual").val())
                retval = index;
        });

        return retval;
    }


    $('#AwareStateThreash_Slider').slider({
        value: <%:Min%>,
         min: 0,
         max: 65535,
         step: 5,
                <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
         slide: function (event, ui) {
             //update the amount by fetching the value in the value_array at index ui.value
             $('#MinimumThreshold_Manual').val(ui.value);
         }
     });
    $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');
    $("#MinimumThreshold_Manual").change(function () {
        if (isANumber($("#MinimumThreshold_Manual").val())){
            if ($("#MinimumThreshold_Manual").val() < 0)
                $("#MinimumThreshold_Manual").val(0);
            if ($("#MinimumThreshold_Manual").val() > 65535)
                $("#MinimumThreshold_Manual").val(65535)
            $('#AwareStateThreash_Slider').slider("value", $("#MinimumThreshold_Manual").val());
         }
         else
         {
            $('#MinimumThreshold_Manual').val(<%: Hyst%>);
            $('#AwareStateThreash_Slider').slider("value", $("#MinimumThreshold_Manual").val());
        }

    });


    
    $('#Hysteresis_Slider').slider({
        value: <%:Hyst%>,
        min: 0,
        max: 50,
        step: 5,
                <%:ViewData["disabled"].ToBool() ? "disabled: true," : ""%>
        slide: function (event, ui) {
            //update the amount by fetching the value in the value_array at index ui.value
            $('#Hysteresis_Manual').val(ui.value);
        }
    });
    $("#Hysteresis_Manual").addClass('editField editFieldSmall');
    $("#Hysteresis_Manual").change(function () {
        if (isANumber($("#Hysteresis_Manual").val())){
            if ($("#Hysteresis_Manual").val() < 0)
                $("#Hysteresis_Manual").val(0);
            if ($("#Hysteresis_Manual").val() > 50)
                $("#Hysteresis_Manual").val(50)
            $('#Hysteresis_Slider').slider("value", $("#Hysteresis_Manual").val());
        }
        else
        {
            $('#Hysteresis_Manual').val(<%: Hyst%>);
                    $('#Hysteresis_Slider').slider("value", $("#Hysteresis_Manual").val());
                }

            });
</script>


<%}
    } %>