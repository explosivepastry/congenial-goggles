<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%

    //calval3
    int fanControl = Thermostat.GetFanControl(Model);
    int fanOnPeriod = Thermostat.GetFanOnPeriod(Model);
    int fanOnInterval = Thermostat.GetFanOnInterval(Model);

    //calval4
    int fanstarttimeforheater = Thermostat.GetFanStartTimeForHeater(Model);
    int fanstoptimeforheater = Thermostat.GetFanStopTimeForHeater(Model);
    int fanstartdelayforcooler = Thermostat.GetFanStartDelayForCooler(Model);
    int fanstopdelayforcooler = Thermostat.GetFanStopDelayForCooler(Model);

    //hyst
    int awareStateTrigger = Thermostat.GetAwareStateTriggers(Model);

%>


<div class="row sensorEditForm useAwareState">
    <div class="col-12 col-md-3">
        <p style="margin-bottom:0;">Advanced Settings</p>
    </div>
    <div class="col sensorEditFormInput standard" id="hyst3">
        <a style="color: var(--options-icon-color); font-size: 0.75rem; font-style: italic;" href="#" onclick="showAdvanced(true);return false;"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Show Advanced Settings","Show Advanced Settings")%>
        </a>
    </div>
        <div class="col sensorEditFormInput advanced" id="Div1">
        <a style="color: var(--options-icon-color); font-size: 0.75rem; font-style: italic;" href="#" onclick="showAdvanced(false);return false;">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Hide Advanced Settings","Hide Advanced Settings")%>
        </a>
    </div>
</div>

<div class="row sensorEditForm advanced">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Trigger On Motion","Trigger On Motion")%>
    </div>
    <div class="col sensorEditFormInput" id="Div2">
        <div class="form-check form-switch d-flex align-items-center ps-0  1111111">
            <label id="advancedOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="advancedOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input value="<%=(awareStateTrigger > 0) ? "checked" : "" %>" class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=!Model.CanUpdate ? "disabled" : "" %> name="AwareStateTrigger_Chk" id="AwareStateTrigger_Chk" <%=(awareStateTrigger > 0) ? "checked" : "" %> onclick="advancedToggle()">
        </div>
        <%: Html.Hidden("AwareStateTrigger",  (awareStateTrigger), (Dictionary<string,object>)ViewData["HtmlAttributes"])%>
    </div>
</div>


<div class="row sensorEditForm advanced">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Control","Fan Control")%>
    </div>
    <div class="col sensorEditFormInput" id="Div3">
        <select id="FanControl" class="form-select" name="FanControl" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <option value="">Please Select a Timeout</option>
            <option value="0" <%= fanControl == 0 ?"selected='selected'":"" %>>Auto</option>
            <option value="1" <%= fanControl == 1 ?"selected='selected'":"" %>>Auto + Periodic</option>
            <option value="2" <%= fanControl == 2 ?"selected='selected'":"" %>>On</option>
            <option value="3" <%= fanControl == 3 ?"selected='selected'":"" %>>Active Fan Control</option>
        </select>
    </div>
</div>

<div class="row sensorEditForm fanAuto advanced">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Period","Fan Period")%>
    </div>
    <div class="col sensorEditFormInput" id="Div4">
        <input type="text" class="form-control" id="FanOnPeriod" name="FanOnPeriod" <%= !Model.CanUpdate ? "disabled" : ""%> value="<%=(fanOnPeriod)%>" />
        <%: Html.Label("Minutes") %>
    </div>
</div>

<div class="row sensorEditForm fanAuto advanced">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Interval","Fan Interval")%>
    </div>
    <div class="col sensorEditFormInput" id="Div5">
        <input type="text" class="form-control" id="FanOnInterval" name="FanOnInterval" <%= !Model.CanUpdate ? "disabled" : ""%> value="<%=(fanOnInterval)%>" />
        <%: Html.Label("Minutes") %>
    </div>
</div>

<div class="row sensorEditForm fanOn advanced">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Start Time For Heater","Fan Start Time For Heater")%>
    </div>
    <div class="col sensorEditFormInput" id="Div6">
        <input type="number" class="form-control" name="FanStartTimeForHeater" id="FanStartTimeForHeater" value="<%=fanstarttimeforheater %>" <%= !Model.CanUpdate ? "disabled" : ""%> />seconds
    </div>
</div>
<div class="row sensorEditForm fanOn advanced">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Stop Time For Heater","Fan Stop Time For Heater")%>
    </div>
    <div class="col sensorEditFormInput" id="Div7">
        <input type="number" class="form-control" name="fanstoptimeforheater" id="fanstoptimeforheater" value="<%=fanstoptimeforheater %>" <%= !Model.CanUpdate ? "disabled" : ""%> />seconds
    </div>
</div>
<div class="row sensorEditForm fanOn advanced">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Start Delay For Cooler","Fan Start Delay For Cooler")%>
    </div>
    <div class="col sensorEditFormInput" id="Div8">
        <input type="number" class="form-control" name="FanStartDelayForCooler" id="FanStartDelayForCooler" value="<%=fanstartdelayforcooler %>" <%= !Model.CanUpdate ? "disabled" : ""%> />seconds
    </div>
</div>
<div class="row sensorEditForm fanOn advanced">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Stop Delay For Cooler","Fan Stop Delay For Cooler")%>
    </div>
    <div class="col sensorEditFormInput" id="Div9">
        <input type="number" class="form-control" name="FanStopDelayForCooler" id="FanStopDelayForCooler" value="<%=fanstopdelayforcooler %>" <%= !Model.CanUpdate ? "disabled" : ""%> />seconds
    </div>
</div>

<script type="text/javascript">


    $(document).ready(function () {

        $(".advanced").hide();

        $('#FanOnPeriod').change(function () {

            if (parseInt($('#FanOnInterval').val()) == parseInt($('#FanOnPeriod').val()) && parseInt($('#FanOnInterval').val()) != 0)
                $('#FanOnPeriod').val(parseInt($('#FanOnInterval').val()) - 5);

            $('#FanPeriod_Slider').slider("option", "value", $('#FanOnPeriod').val());
        });

        $('#FanControl').change(function () {
            $(".fanOn").hide();
            $(".fanAuto").hide();

            if ($('#FanControl').val() == 1) {
                $(".fanAuto").show();
            }
            else if ($('#FanControl').val() == 3) {
                $(".fanOn").show();
            }

        });

        $('#AwareStateTrigger_Chk').change(function () {
            if ($(this).prop('checked'))
                $('#AwareStateTrigger').val(1);
            else
                $('#AwareStateTrigger').val(0);
        });
    });



    function showAdvanced(adv) {

        if (adv) {
            $('.advanced').show();
            $('.standard').hide();


            var hide = $('#FanControl').val();
            $(".fanOn").hide();
            $(".fanAuto").hide();

            if (hide == 1) {
                $(".fanAuto").show();
            }
            else if (hide == 3) {
                $(".fanOn").show();
            }
        }
        else {
            $('.standard').show();
            $('.advanced').hide();
        }
    }

    let trig = document.getElementById("AwareStateTrigger_Chk");
    let addvOff = document.getElementById("advancedOff");
    let addvOn = document.getElementById("advancedOn");


    function advancedToggle() {
        if (trig.checked === 1) {
            addvOff.style.display = "none";
            addvOn.style.display = "block";
        } else {
            addvOn.style.display = "none";
            addvOff.style.display = "block";
        }
    };
 advancedToggle()

   

</script>
