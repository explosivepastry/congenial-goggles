<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool isFahr = MultiStageThermostat.IsFahrenheit(Model.SensorID);

    int systemType = MultiStageThermostat.GetSystemType(Model);
    int heatCoolMode = MultiStageThermostat.GetHeatCoolMode(Model);
    double occupiedSetpoint = Math.Round(MultiStageThermostat.GetOccupiedSetPoint(Model), MidpointRounding.ToEven);
    int tempDelta = Math.Round(MultiStageThermostat.GetTempDelta(Model), 0).ToInt();
    double occupiedTimeout = MultiStageThermostat.GetOccupiedTimeout(Model);
    double unoccupiedCoolingSetpoint = MultiStageThermostat.GetUnoccupiedCoolingSetpoint(Model);
    double unoccupiedHeatingSetpoint = MultiStageThermostat.GetUnoccupiedHeatingSetpoint(Model);

    if (isFahr)
    {
        occupiedSetpoint = Math.Round(MultiStageThermostat.GetOccupiedSetPoint(Model).ToFahrenheit(), MidpointRounding.ToEven);
        unoccupiedCoolingSetpoint = unoccupiedCoolingSetpoint.ToFahrenheit();
        unoccupiedHeatingSetpoint = unoccupiedHeatingSetpoint.ToFahrenheit();
        tempDelta = Math.Round((MultiStageThermostat.GetTempDelta(Model) * 1.8), 0).ToInt();
    }
%>

    <p class="useAwareState">General Settings</p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("System Type","System Type")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="SystemType" class="form-select" name="SystemType" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <option value="0" <%= systemType == 0 ? "selected='selected'":"" %>><%=Html.TranslateTag("Not Defined","Not Defined")%></option>
            <option value="1" <%= systemType == 1 ? "selected='selected'":"" %>><%=Html.TranslateTag("Conventional 1 Stage Heating","Conventional 1 Stage Heating")%></option>
            <option value="2" <%= systemType == 2 ? "selected='selected'":"" %>><%=Html.TranslateTag("Conventional 2 Stage Heating","Conventional 2 Stage Heating")%></option>
            <option value="3" <%= systemType == 3 ? "selected='selected'":"" %>><%=Html.TranslateTag("Conventional 3 Stage Heating","Conventional 3 Stage Heating")%></option>
            <option value="4" <%= systemType == 4 ? "selected='selected'":"" %>><%=Html.TranslateTag("Conventional 1 Stage Heating, 1 Stage Cooling","Conventional 1 Stage Heating, 1 Stage Cooling")%></option>
            <option value="5" <%= systemType == 5 ? "selected='selected'":"" %>><%=Html.TranslateTag("Conventional 2 Stage Heating, 1 Stage Cooling","Conventional 2 Stage Heating, 1 Stage Cooling")%></option>
            <option value="6" <%= systemType == 6 ? "selected='selected'":"" %>><%=Html.TranslateTag("Conventional 2 Stage Heating, 2 Stage Cooling","Conventional 2 Stage Heating, 2 Stage Cooling")%></option>
            <option value="7" <%= systemType == 7 ? "selected='selected'":"" %>><%=Html.TranslateTag("Conventional 3 Stage Heating, 2 Stage Cooling","Conventional 3 Stage Heating, 2 Stage Cooling")%></option>
            <option value="8" <%= systemType == 8 ? "selected='selected'":"" %>><%=Html.TranslateTag("Conventional 1 Stage Heating, 2 Stage Cooling","Conventional 1 Stage Heating, 2 Stage Cooling")%></option>
            <option value="9" <%= systemType == 9 ? "selected='selected'":"" %>><%=Html.TranslateTag("1 Stage Heat Pump","1 Stage Heat Pump")%></option>
            <option value="10" <%= systemType == 10 ? "selected='selected'":"" %>><%=Html.TranslateTag("1 Stage Heat Pump with Aux Heat","1 Stage Heat Pump with Aux Heat")%></option>
            <option value="11" <%= systemType == 11 ? "selected='selected'":"" %>><%=Html.TranslateTag("1 Stage Heat Pump with Aux Heat and Emergency Heat","1 Stage Heat Pump with Aux Heat and Emergency Heat")%></option>
            <option value="12" <%= systemType == 12 ? "selected='selected'":"" %>><%=Html.TranslateTag("2 Stage Heat Pump","2 Stage Heat Pump")%></option>
            <option value="13" <%= systemType == 13 ? "selected='selected'":"" %>><%=Html.TranslateTag("2 Stage Heat Pump with Aux Heat","2 Stage Heat Pump with Aux Heat")%></option>
            <option value="14" <%= systemType == 14 ? "selected='selected'":"" %>><%=Html.TranslateTag("Dual Fuel - 1 Stage heat Pump, 1 Stage Heat","Dual Fuel - 1 Stage heat Pump, 1 Stage Heat")%></option>
            <option value="15" <%= systemType == 15 ? "selected='selected'":"" %>><%=Html.TranslateTag("Dual Fuel - 2 Stage Heat Pump, 1 Stage Heat","Dual Fuel - 2 Stage Heat Pump, 1 Stage Heat")%></option>
            <option value="16" <%= systemType == 16 ? "selected='selected'":"" %>><%=Html.TranslateTag("Dual Fuel - 2 Stage Heat Pump, 2 Stage Heat","Dual Fuel - 2 Stage Heat Pump, 2 Stage Heat")%></option>
        </select>

    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Heat/Cool Mode","Heat/Cool Mode")%>
    </div>
    <div class="col sensorEditFormInput">
         <select class="form-select" id="HeatCoolMode" name="HeatCoolMode" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <option class="heatcoolmodeOff" value="0" <%= heatCoolMode == 0 ?"selected='selected'":"" %>><%=Html.TranslateTag("Off","Off")%></option>
            <option class="heatcoolmodeHeat heatcoolmodeCool" value="1" <%= heatCoolMode == 1 ?"selected='selected'":"" %>><%=Html.TranslateTag("Auto","Auto")%></option>
            <option class="heatcoolmodeHeat" value="2" <%= heatCoolMode == 2 ?"selected='selected'":"" %>><%=Html.TranslateTag("Heat Only","Heat Only")%></option>
            <option class="heatcoolmodeCool" value="3" <%= heatCoolMode == 3 ?"selected='selected'":"" %>><%=Html.TranslateTag("Cool Only","Cool Only")%></option>
            <option class="heatcoolmodeEmergency" value="4" <%= heatCoolMode == 4 ?"selected='selected'":"" %>><%=Html.TranslateTag("Emergency Heat","Emergency Heat")%></option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature buffer","Temperature Buffer")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID) ? "°F" :" °C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select" id="TempDelta" name="TempDelta" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <%if (isFahr)
                { %>
            <option value="2" <%= tempDelta == 2 ? "selected='selected'" : "" %>><%=Html.TranslateTag("2 Degrees", "2 Degrees") %></option>
            <option value="4" <%= tempDelta == 4 ? "selected='selected'" : "" %>><%=Html.TranslateTag("4 Degrees", "4 Degrees")  %></option>
            <option value="6" <%= tempDelta == 6 ? "selected='selected'" : "" %>><%=Html.TranslateTag("6 Degrees", "6 Degrees")  %></option>
            <option value="8" <%= tempDelta == 8 ? "selected='selected'" : "" %>><%=Html.TranslateTag("8 Degrees", "8 Degrees") %></option>
            <option value="10" <%= tempDelta == 10 ? "selected='selected'" : "" %>><%=Html.TranslateTag("10 Degrees", "10 Degrees") %></option>
            <%}
                else
                { %>
            <option value="1" <%= tempDelta == 1 ? "selected='selected'" : "" %>><%=Html.TranslateTag("1 Degree","1 Degree") %></option>
            <option value="2" <%= tempDelta == 2 ? "selected='selected'" : "" %>><%=Html.TranslateTag("2 Degrees","2 Degrees") %></option>
            <option value="3" <%= tempDelta == 3 ? "selected='selected'" : "" %>><%=Html.TranslateTag("3 Degrees","3 Degrees") %></option>
            <option value="4" <%= tempDelta == 4 ? "selected='selected'" : "" %>><%=Html.TranslateTag("4 Degrees","4 Degrees") %></option>
            <option value="5" <%= tempDelta == 5 ? "selected='selected'" : "" %>><%=Html.TranslateTag("5 Degrees","5 Degrees") %></option>
            <%} %>
        </select>

    </div>
</div>

<p class="useAwareState">
    <%: Html.Label(Html.TranslateTag("Occupied","Occupied"))%>
</p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Setpoint","Setpoint")%> (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID) ? "°F" :" °C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> id="OccupiedSetpoint" name="OccupiedSetpoint" value="<%=occupiedSetpoint%>" />
        <a id="OccupiedSetpointNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Occupied Timeout","Occupied Timeout")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select" id="OccupiedTimeout" name="OccupiedTimeout" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <option value="5" <%= occupiedTimeout == 5 ?"selected='selected'":"" %>>5 minutes</option>
            <option value="10" <%= occupiedTimeout == 10 ?"selected='selected'":"" %>>10 minutes</option>
            <option value="15" <%= occupiedTimeout == 15 ?"selected='selected'":"" %>>15 minutes</option>
            <option value="30" <%= occupiedTimeout == 30 ?"selected='selected'":"" %>>30 minutes</option>
            <option value="60" <%= occupiedTimeout == 60 ?"selected='selected'":"" %>>60 minutes</option>
            <option value="90" <%= occupiedTimeout == 90 ?"selected='selected'":"" %>>90 minutes</option>
            <option value="120" <%= occupiedTimeout == 120 ?"selected='selected'":"" %>>2 hours</option>
            <option value="240" <%= occupiedTimeout == 240 ?"selected='selected'":"" %>>4 hours</option>
            <option value="480" <%= occupiedTimeout == 480 ?"selected='selected'":"" %>>8 hours</option>
            <option value="720" <%= occupiedTimeout == 720 ?"selected='selected'":"" %>>12 hours</option>
        </select>

    </div>
</div>

<p class="useAwareState">
    <%: Html.Label(Html.TranslateTag("Unoccupied","Unoccupied"))%>
</p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Heating Setpoint:","Heating Setpoint:")%>  (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%= !Model.CanUpdate ? "disabled" : ""%> value="<%= (unoccupiedHeatingSetpoint).ToString("0.#")%>" name="UnoccupiedHeatingSetpoint" id="UnoccupiedHeatingSetpoint" />
        <a id="UnoccupiedHeatingSetpointNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Cooling Setpoint:","Cooling Setpoint:")%>  (<%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%= !Model.CanUpdate ? "disabled" : ""%> value="<%= unoccupiedCoolingSetpoint.ToString("0.#")%>" name="UnoccupiedCoolingSetpoint" id="UnoccupiedCoolingSetpoint" />

        <a id="UnoccupiedCoolingSetpointNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">

    var SliderMin = <%:(isFahr ? 50:10.0)%>;
    var SliderMax = <%:(isFahr ? 103:37.0)%>;

    $(function () {

        $(".systemSpecificConfigs").hide();

        setSystemTypeOptions();

        <% if (Model.CanUpdate){ %>
        
        $("#TempDelta").change(function () {

            $('#Stage2HeatingActivationThreshold').change();
            $('#Stage2CoolingActivationThreshold').change();
            $('#Stage3HeatingActivationThreshold').change();
            $('#Stage4HeatingActivationThreshold').change();

            $('#Stage2HeatingActivationThresholdNum').mobiscroll('option', {
                min: $('#TempDelta').val()
            });
            $('#Stage2CoolingActivationThresholdNum').mobiscroll('option', {
                min: $('#TempDelta').val()
            });
            $('#Stage3HeatingActivationThresholdNum').mobiscroll('option', {
                min: $('#Stage2HeatingActivationThreshold').val()
            });
            $('#Stage4HeatingActivationThresholdNum').mobiscroll('option', {
                min: $('#Stage3HeatingActivationThreshold').val()
            });
        });

        $("#SystemType").change(function () {
            setSystemTypeOptions();
            $("#ReversingValve").val(0);
        });

        const arrayForSpinner = arrayBuilder(SliderMin, SliderMax, 1);
        createSpinnerModal("OccupiedSetpointNum", "<%= isFahr ? "Fahrenheit" : "Celsius"%>", "OccupiedSetpoint", arrayForSpinner);
        createSpinnerModal("UnoccupiedHeatingSetpointNum", "<%= isFahr ? "Fahrenheit" : "Celsius"%>", "UnoccupiedHeatingSetpoint", arrayForSpinner);
        createSpinnerModal("UnoccupiedCoolingSetpointNum", "<%= isFahr ? "Fahrenheit" : "Celsius"%>", "UnoccupiedCoolingSetpoint", arrayForSpinner);

        <%}%>
        $("#OccupiedSetpoint").change(function () {
            if (isANumber($("#OccupiedSetpoint").val())) {
                if ($("#OccupiedSetpoint").val() < SliderMin)
                    $("#OccupiedSetpoint").val(SliderMax);
                if ($("#OccupiedSetpoint").val() > SliderMax)
                    $("#OccupiedSetpoint").val(SliderMax);

            } else {

                $("#OccupiedSetpoint").val(<%: occupiedSetpoint%>);
            }
        });

        $("#UnoccupiedHeatingSetpoint").change(function () {
            if (isANumber($("#UnoccupiedHeatingSetpoint").val())) {
                var UnoccupiedHeatingSetpoint = Number($("#UnoccupiedHeatingSetpoint").val());

                if (UnoccupiedHeatingSetpoint < SliderMin) {
                    $("#UnoccupiedHeatingSetpoint").val(SliderMin);
                    $("#UnoccupiedCoolingSetpoint").change();
                }

                var UnoccupiedCoolingSetpoint = Number($("#UnoccupiedCoolingSetpoint").val());

                if (UnoccupiedHeatingSetpoint > UnoccupiedCoolingSetpoint) {
                    UnoccupiedHeatingSetpoint = UnoccupiedCoolingSetpoint;
                    $("#UnoccupiedHeatingSetpoint").val(UnoccupiedHeatingSetpoint);
                }

                $('#UnoccupiedCoolingSetpointNum').mobiscroll('option', {
                    min: $('#UnoccupiedHeatingSetpoint').val()
                });

            }
            else {
                $('#UnoccupiedHeatingSetpoint').val(SliderMin);
            }



        });

        $("#UnoccupiedCoolingSetpoint").change(function () {
            if (isANumber($("#UnoccupiedCoolingSetpoint").val())) {
                var UnoccupiedCoolingSetpoint = Number($("#UnoccupiedCoolingSetpoint").val());

                if (UnoccupiedCoolingSetpoint > SliderMax) {
                    $("#UnoccupiedCoolingSetpoint").val(SliderMax);
                    $("#UnoccupiedHeatingSetpoint").change();
                }

                var UnoccupiedHeatingSetpoint = Number($("#UnoccupiedHeatingSetpoint").val());

                if (UnoccupiedCoolingSetpoint < UnoccupiedHeatingSetpoint) {
                    UnoccupiedCoolingSetpoint = UnoccupiedHeatingSetpoint;
                    $("#UnoccupiedCoolingSetpoint").val(UnoccupiedCoolingSetpoint);
                }

                $('#UnoccupiedHeatingSetpointNum').mobiscroll('option', {
                    max: $('#UnoccupiedCoolingSetpoint').val()
                });
            }
            else {
                $('#UnoccupiedCoolingSetpoint').val(SliderMax);
            }
        });
    });

    function setHCModeVals(systemType) {

        var currentSelection = $('#HeatCoolMode').val();
        
        switch (Number(systemType)) {
            case 1:
            case 2:
            case 3:
                var vals = [
                    { text: '<%=Html.TranslateTag("Off","Off")%>', value: 0 },
                    { text: '<%=Html.TranslateTag("Heat Only","Heat Only")%>', value: 2 }
                  ];
                $('#HeatCoolMode').find('option').remove();
                $('#HeatCoolMode').append($("<option/>", { value: 0, text: "Off" }));
                $('#HeatCoolMode').append($("<option/>", { value: 2, text: "Heat Only" }));
                break;
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
            case 12:
            case 13:
            case 14:
            case 15:
            case 16:
                var vals = [
                    { text: '<%=Html.TranslateTag("Off","Off")%>', value: 0 },
                    { text: '<%=Html.TranslateTag("Auto","Auto")%>', value: 1 },
                    { text: '<%=Html.TranslateTag("Heat Only","Heat Only")%>', value: 2 },
                    { text: '<%=Html.TranslateTag("Cool Only","Cool Only")%>', value: 3 }
                ];
                $('#HeatCoolMode').find('option').remove();
                $('#HeatCoolMode').append($("<option/>", { value: 0, text: "Off" }));
                $('#HeatCoolMode').append($("<option/>", { value: 1, text: "Auto" }));
                $('#HeatCoolMode').append($("<option/>", { value: 2, text: "Heat Only" }));
                $('#HeatCoolMode').append($("<option/>", { value: 3, text: "Cool Only" }));
                break;
            case 11:
                var vals = [
                    { text: '<%=Html.TranslateTag("Off","Off")%>', value: 0 },
                    { text: '<%=Html.TranslateTag("Auto","Auto")%>', value: 1 },
                    { text: '<%=Html.TranslateTag("Heat Only","Heat Only")%>', value: 2 },
                    { text: '<%=Html.TranslateTag("Cool Only","Cool Only")%>', value: 3 },
                    { text: '<%=Html.TranslateTag("Emergency Heat","Emergency Heat")%>', value: 4 }
                ];
                $('#HeatCoolMode').find('option').remove();
                $('#HeatCoolMode').append($("<option/>", { value: 0, text: "Off" }));
                $('#HeatCoolMode').append($("<option/>", { value: 1, text: "Auto" }));
                $('#HeatCoolMode').append($("<option/>", { value: 2, text: "Heat Only" }));
                $('#HeatCoolMode').append($("<option/>", { value: 3, text: "Cool Only" }));
                $('#HeatCoolMode').append($("<option/>", { value: 4, text: "Emergency Heat" }));
                break;
            case 0:
            default:
                var vals = [{ text: '<%=Html.TranslateTag("Off","Off")%>',value: 0}];
                $('#HeatCoolMode').find('option').remove();
                $('#HeatCoolMode').append($("<option/>", { value: 0, text: "Off" }));
                break;
        }

    

        $('#HeatCoolMode').val(currentSelection);
        if ($('#HeatCoolMode').val() == null) {
            $('#HeatCoolMode').val(0);
        }
       
    }

    function setSystemTypeOptions() {
        $(".systemSpecificConfigs").hide(); // hide all config sections
        $(".hcModeType11").hide();

        var systemType = Number($("#SystemType").val())
        setHCModeVals(systemType)
        switch (systemType) {
            case 2:
                $("#heatingStage2").show();
                $("#heatingLoadBalancing").show();
                $("#loadBalancing").show();
                break;
            case 3:
                $("#heatingStage2").show();
                $("#heatingStage3").show();
                $("#heatingLoadBalancing").show();
                $("#loadBalancing").show();
                break;
            case 4:
                break;
            case 5:
                $("#heatingStage2").show();
                $("#heatingLoadBalancing").show();
                $("#loadBalancing").show();
                break;
            case 6:
                $("#coolingStage2").show();
                $("#heatingStage2").show();
                $("#heatingLoadBalancing").show();
                $("#coolingLoadBalancing").show();
                $("#loadBalancing").show();
                break;
            case 7:
                $("#coolingStage2").show();
                $("#heatingStage2").show();
                $("#heatingStage3").show();
                $("#heatingLoadBalancing").show();
                $("#coolingLoadBalancing").show();
                $("#loadBalancing").show();
                break;
            case 8:
                $("#coolingStage2").show();
                $("#coolingLoadBalancing").show();
                $("#loadBalancing").show();
                break;
            case 9:
                $("#reversingValveControl").show();
                break;
            case 10:
                $("#heatingStage2").show();
                $("#reversingValveControl").show();
                break;
            case 11:
                $(".hcModeType11").show();
                $("#heatingStage2").show();
                $("#reversingValveControl").show();
                $("#loadBalancing").show();
                break;
            case 12:
                $("#coolingStage2").show();
                $("#heatingStage2").show();
                $("#heatingLoadBalancing").show();
                $("#coolingLoadBalancing").show();
                $("#reversingValveControl").show();
                $("#loadBalancing").show();
                break;
            case 13:
                $("#coolingStage2").show();
                $("#heatingStage2").show();
                $("#heatingStage3").show();
                $("#heatingLoadBalancing").show();
                $("#coolingLoadBalancing").show();
                $("#reversingValveControl").show();
                $("#loadBalancing").show();
                break;
            case 14:
                $("#heatingStage2").show();
                $("#reversingValveControl").show();
                break;
            case 15:
                $("#coolingStage2").show();
                $("#heatingStage2").show();
                $("#heatingStage3").show();
                $("#heatingLoadBalancing").show();
                $("#coolingLoadBalancing").show();
                $("#reversingValveControl").show();
                $("#loadBalancing").show();
                break;
            case 16:
                $("#coolingStage2").show();
                $("#heatingStage2").show();
                $("#heatingStage3").show();
                $("#heatingStage4").show();
                $("#heatingLoadBalancing").show();
                $("#coolingLoadBalancing").show();
                $("#reversingValveControl").show();
                $("#loadBalancing").show();
                break;
            case 1:
            default:
                break;
        }

    }

</script>
