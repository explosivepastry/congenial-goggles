<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool isF = MultiStageThermostat.IsFahrenheit(Model.SensorID);
    bool EnableLoadBalancingHeater = MultiStageThermostat.GetEnableLoadBalancingHeater(Model);
    bool EnableLoadBalancingCooler = MultiStageThermostat.GetEnableLoadBalancingCooler(Model);
    int tempDelta = Math.Round(MultiStageThermostat.GetTempDelta(Model), 0).ToInt();
    int ReversingValve = MultiStageThermostat.GetReversingValve(Model);
    double Stage2CoolingActivationTime = MultiStageThermostat.GetStage2CoolingActivationTime(Model);
    double Stage2CoolingActivationThreshold = MultiStageThermostat.GetStage2CoolingActivationThreshold(Model);
    double Stage2HeatingActivationTime = MultiStageThermostat.GetStage2HeatingActivationTime(Model);
    double Stage2HeatingActivationThreshold = MultiStageThermostat.GetStage2HeatingActivationThreshold(Model);
    double Stage3HeatingActivationTime = MultiStageThermostat.GetStage3HeatingActivationTime(Model);
    double Stage3HeatingActivationThreshold = MultiStageThermostat.GetStage3HeatingActivationThreshold(Model);
    double Stage4HeatingActivationTime = MultiStageThermostat.GetStage4HeatingActivationTime(Model);
    double Stage4HeatingActivationThreshold = MultiStageThermostat.GetStage4HeatingActivationThreshold(Model);

    if (isF)
    {
        tempDelta = Math.Round(MultiStageThermostat.GetTempDelta(Model) * 1.8, 0).ToInt();
        Stage2CoolingActivationThreshold = Math.Round(Stage2CoolingActivationThreshold * 1.8, 0).ToInt();
        Stage2HeatingActivationThreshold = Math.Round(Stage2HeatingActivationThreshold * 1.8, 0).ToInt();
        Stage3HeatingActivationThreshold = Math.Round(Stage3HeatingActivationThreshold * 1.8, 0).ToInt();
        Stage4HeatingActivationThreshold = Math.Round(Stage4HeatingActivationThreshold * 1.8, 0).ToInt();
    }

%>

<div class="systemSpecificConfigs" id="coolingStage2">
    <div class="col-12">
        <%: Html.Label(Html.TranslateTag("2nd Stage Cooling","2nd Stage Cooling"))%>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Cooling Activation Time","Cooling Activation Time")%> (<%: Html.Label("Minutes") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Stage2CoolingActivationTime" id="Stage2CoolingActivationTime" value="<%=Stage2CoolingActivationTime %>" />
            <a id="Stage2CoolingActivationTimeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Cooling Activation Threshold","Cooling Activation Threshold")%> (<%: Html.Label(isF ?"°F":"°C") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Stage2CoolingActivationThreshold" id="Stage2CoolingActivationThreshold" value="<%=Stage2CoolingActivationThreshold %>" />
            <a id="Stage2CoolingActivationThresholdNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        </div>
    </div>

    <script>
        $(function () {
            var defaultActivationTimeMin = Number(0);
            var defaultActivationTimeMax = Number(240);
            var coolingThreshMax = Number(<%:(isF ? 18 : 10)%>);

                 <% if (Model.CanUpdate)
        { %>

            const arrayForSpinner10 = arrayBuilder(defaultActivationTimeMin, defaultActivationTimeMax, 2);
            createSpinnerModal("Stage2CoolingActivationTimeNum", "Minutes", "Stage2CoolingActivationTime", arrayForSpinner10);

            const arrayForSpinner11 = arrayBuilder(Number($('#TempDelta').val()), coolingThreshMax, 1);
            createSpinnerModal("Stage2CoolingActivationThresholdNum", "<%= isF ? "Fahrenheit" : "Celsius"%>", "Stage2CoolingActivationThreshold", arrayForSpinner11);

        <%}%>

            $("#Stage2CoolingActivationTime").change(function () {
                if (isANumber($("#Stage2CoolingActivationTime").val())) {
                    if ($("#Stage2CoolingActivationTime").val() < defaultActivationTimeMin)
                        $("#Stage2CoolingActivationTime").val(defaultActivationTimeMin);
                    if ($("#Stage2CoolingActivationTime").val() > defaultActivationTimeMax)
                        $("#Stage2CoolingActivationTime").val(defaultActivationTimeMax);
                } else {

                    $("#Stage2CoolingActivationTime").val(<%: Stage2CoolingActivationTime%>);
                }
            });

            $("#Stage2CoolingActivationThreshold").change(function () {
                if (isANumber($("#Stage2CoolingActivationThreshold").val())) {
                    if ($("#Stage2CoolingActivationThreshold").val() < Number($('#TempDelta').val()))
                        $("#Stage2CoolingActivationThreshold").val(Number($('#TempDelta').val()));
                    if ($("#Stage2CoolingActivationThreshold").val() > coolingThreshMax)
                        $("#Stage2CoolingActivationThreshold").val(coolingThreshMax);
                } else {

                    $("#Stage2CoolingActivationThreshold").val(<%: Stage2CoolingActivationThreshold%>);
                }
            });


        });
    </script>

</div>

<div class="systemSpecificConfigs" id="heatingStage2">
    <div class="col-12">
        <%: Html.Label(Html.TranslateTag("2nd Stage Heating","2nd Stage Heating"))%>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Heating Activation Time","Heating Activation Time")%> (<%: Html.Label("Minutes") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Stage2HeatingActivationTime" id="Stage2HeatingActivationTime" value="<%=Stage2HeatingActivationTime %>" />
            <a id="Stage2HeatingActivationTimeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Heating Activation Threshold","Heating Activation Threshold")%> (<%: Html.Label(isF ?"°F":"°C") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Stage2HeatingActivationThreshold" id="Stage2HeatingActivationThreshold" value="<%=Stage2HeatingActivationThreshold %>" />
            <a id="Stage2HeatingActivationThresholdNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        </div>
    </div>

    <script>


        $(function () {
            var defaultActivationTimeMin = Number(0);
            var defaultActivationTimeMax = Number(240);
            var heatingThreshMax = Number(<%:(isF ? 18 : 10)%>);

                 <% if (Model.CanUpdate)
        { %>

            const arrayForSpinner12 = arrayBuilder(defaultActivationTimeMin, defaultActivationTimeMax, 2);
            createSpinnerModal("Stage2HeatingActivationTimeNum", "Minutes", "Stage2HeatingActivationTime", arrayForSpinner12);

            const arrayForSpinner13 = arrayBuilder(Number($('#TempDelta').val()), heatingThreshMax, 1);
            createSpinnerModal("Stage2HeatingActivationThresholdNum", "<%= isF ? "Fahrenheit" : "Celsius"%>", "Stage2HeatingActivationThreshold", arrayForSpinner13);

        <%}%>

            $("#Stage2HeatingActivationTime").change(function () {
                if (isANumber($("#Stage2HeatingActivationTime").val())) {
                    if ($("#Stage2HeatingActivationTime").val() < defaultActivationTimeMin)
                        $("#Stage2HeatingActivationTime").val(defaultActivationTimeMin);
                    if ($("#Stage2HeatingActivationTime").val() > defaultActivationTimeMax)
                        $("#Stage2HeatingActivationTime").val(defaultActivationTimeMax);


                    $('#Stage3HeatingActivationTimeNum').mobiscroll('option', {
                        min: $('#Stage2HeatingActivationTime').val()
                    });

                } else {

                    $("#Stage2HeatingActivationTime").val(<%: Stage2HeatingActivationTime%>);
                }
            });

            $("#Stage2HeatingActivationThreshold").change(function () {
                if (isANumber($("#Stage2HeatingActivationThreshold").val())) {
                    if ($("#Stage2HeatingActivationThreshold").val() < Number($('#TempDelta').val()))
                        $("#Stage2HeatingActivationThreshold").val(Number($('#TempDelta').val()));
                    if ($("#Stage2HeatingActivationThreshold").val() > heatingThreshMax)
                        $("#Stage2HeatingActivationThreshold").val(heatingThreshMax);

                    $('#Stage3HeatingActivationThresholdNum').mobiscroll('option', {
                        min: $('#Stage2HeatingActivationThreshold').val()
                    });

                } else {

                    $("#Stage2HeatingActivationThreshold").val(<%: Stage2HeatingActivationThreshold%>);
                }
            });


        });
    </script>

</div>

<div class="systemSpecificConfigs" id="heatingStage3">
    <div class="col-12">
        <%: Html.Label(Html.TranslateTag("3rd Stage Heating","3rd Stage Heating"))%>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Heating Activation Time","Heating Activation Time")%> (<%: Html.Label("Minutes") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Stage3HeatingActivationTime" id="Stage3HeatingActivationTime" value="<%=Stage3HeatingActivationTime %>" />
            <a id="Stage3HeatingActivationTimeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Heating Activation Threshold","Heating Activation Threshold")%> (<%: Html.Label(isF ?"°F":"°C") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Stage3HeatingActivationThreshold" id="Stage3HeatingActivationThreshold" value="<%=Stage3HeatingActivationThreshold %>" />
            <a id="Stage3HeatingActivationThresholdNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        </div>
    </div>

    <script>


        $(function () {
            var defaultActivationTimeMin = Number(0);
            var defaultActivationTimeMax = Number(240);
            var heatingThreshMax = Number(<%:(isF ? 18 : 10)%>);

                 <% if (Model.CanUpdate)
        { %>


            const arrayForSpinner14 = arrayBuilder(Number($('#Stage2HeatingActivationTime').val()), defaultActivationTimeMax, 2);
            createSpinnerModal("Stage3HeatingActivationTimeNum", "Minutes", "Stage3HeatingActivationTime", arrayForSpinner14);

            const arrayForSpinner15 = arrayBuilder(Number($('#Stage2HeatingActivationThreshold').val()), heatingThreshMax, 1);
            createSpinnerModal("Stage3HeatingActivationThresholdNum", "<%= isF ? "Fahrenheit" : "Celsius"%>", "Stage3HeatingActivationThreshold", arrayForSpinner15);

        <%}%>

            $("#Stage3HeatingActivationTime").change(function () {
                if (isANumber($("#Stage3HeatingActivationTime").val())) {
                    if ($("#Stage3HeatingActivationTime").val() < Number($('#Stage2HeatingActivationTime').val()))
                        $("#Stage3HeatingActivationTime").val(Number($('#Stage2HeatingActivationTime').val()));
                    if ($("#Stage3HeatingActivationTime").val() > defaultActivationTimeMax)
                        $("#Stage3HeatingActivationTime").val(defaultActivationTimeMax);

                    $('#Stage4HeatingActivationTimeNum').mobiscroll('option', {
                        min: $('#Stage3HeatingActivationTime').val()
                    });

                } else {

                    $("#Stage3HeatingActivationTime").val(<%: Stage3HeatingActivationTime%>);
                }
            });

            $("#Stage3HeatingActivationThreshold").change(function () {
                if (isANumber($("#Stage3HeatingActivationThreshold").val())) {
                    if ($("#Stage3HeatingActivationThreshold").val() < Number($('#Stage2HeatingActivationThreshold').val()))
                        $("#Stage3HeatingActivationThreshold").val(Number($('#Stage2HeatingActivationThreshold').val()));
                    if ($("#Stage3HeatingActivationThreshold").val() > heatingThreshMax)
                        $("#Stage3HeatingActivationThreshold").val(heatingThreshMax);

                    $('#Stage4HeatingActivationThresholdNum').mobiscroll('option', {
                        min: $('#Stage3HeatingActivationThreshold').val()
                    });

                } else {

                    $("#Stage3HeatingActivationThreshold").val(<%: Stage3HeatingActivationThreshold%>);
                }
            });


        });
    </script>

</div>

<div class="systemSpecificConfigs" id="heatingStage4">
    <div class="col-12">
        <%: Html.Label(Html.TranslateTag("4th Stage Heating","4th Stage Heating"))%>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Heating Activation Time","Heating Activation Time")%> (<%: Html.Label("Minutes") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Stage4HeatingActivationTime" id="Stage4HeatingActivationTime" value="<%=Stage4HeatingActivationTime %>" />
            <a id="Stage4HeatingActivationTimeNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Heating Activation Threshold","Heating Activation Threshold")%> (<%: Html.Label(isF ?"°F":"°C") %>)
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Stage4HeatingActivationThreshold" id="Stage4HeatingActivationThreshold" value="<%=Stage4HeatingActivationThreshold %>" />
            <a id="Stage4HeatingActivationThresholdNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        </div>
    </div>

    <script>


        $(function () {
            var defaultActivationTimeMin = Number(0);
            var defaultActivationTimeMax = Number(240);
            var heatingThreshMax = Number(<%:(isF ? 18 : 10)%>);

                 <% if (Model.CanUpdate)
        { %>

            const arrayForSpinner = arrayBuilder(Number($('#Stage3HeatingActivationTime').val()), defaultActivationTimeMax, 2);
            createSpinnerModal("Stage4HeatingActivationTimeNum", "Minutes", "Stage4HeatingActivationTime", arrayForSpinner);

            const arrayForSpinner1 = arrayBuilder(Number($('#Stage3HeatingActivationThreshold').val()), heatingThreshMax, 1);
            createSpinnerModal("Stage4HeatingActivationThresholdNum", "<%= isF ? "Fahrenheit" : "Celsius"%>", "Stage4HeatingActivationThreshold", arrayForSpinner1);

        <%}%>

            $("#Stage4HeatingActivationTime").change(function () {
                if (isANumber($("#Stage4HeatingActivationTime").val())) {
                    if ($("#Stage4HeatingActivationTime").val() < Number($('#Stage3HeatingActivationTime').val()))
                        $("#Stage4HeatingActivationTime").val(Number($('#Stage3HeatingActivationTime').val()));
                    if ($("#Stage4HeatingActivationTime").val() > defaultActivationTimeMax)
                        $("#Stage4HeatingActivationTime").val(defaultActivationTimeMax);
                } else {

                    $("#Stage4HeatingActivationTime").val(<%: Stage4HeatingActivationTime%>);
                }
            });

            $("#Stage4HeatingActivationThreshold").change(function () {
                if (isANumber($("#Stage4HeatingActivationThreshold").val())) {
                    if ($("#Stage4HeatingActivationThreshold").val() < Number($('#Stage3HeatingActivationThreshold').val()))
                        $("#Stage4HeatingActivationThreshold").val(Number($('#Stage3HeatingActivationThreshold').val()));
                    if ($("#Stage4HeatingActivationThreshold").val() > heatingThreshMax)
                        $("#Stage4HeatingActivationThreshold").val(heatingThreshMax);
                } else {

                    $("#Stage4HeatingActivationThreshold").val(<%: Stage4HeatingActivationThreshold%>);
                }
            });


        });
    </script>

</div>

<div class="col-lg-12 col-xs-12 systemSpecificConfigs" id="loadBalancing">
    <%: Html.Label(Html.TranslateTag("Load Balancing","Load Balancing"))%>
</div>

<div class="systemSpecificConfigs" id="heatingLoadBalancing">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Heater Stage Load Balancing","Heater Stage Load Balancing")%>
    </div>
    <div class="row sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="enableLoadBalancingHeaterChk" id="enableLoadBalancingHeaterChk" <%=EnableLoadBalancingHeater ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <div style="display: none;"><%: Html.TextBox("EnableLoadBalancingHeater",EnableLoadBalancingHeater.ToInt(), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<div class="systemSpecificConfigs" id="coolingLoadBalancing">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Cooler Stage Load Balancing","Cooler Stage Load Balancing")%>
    </div>
    <div class="row sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="enableLoadBalancingCoolerChk" id="enableLoadBalancingCoolerChk" <%=EnableLoadBalancingCooler ? "checked" : "" %>>
            <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
        </div>
        <div style="display: none;"><%: Html.TextBox("EnableLoadBalancingCooler",EnableLoadBalancingCooler.ToInt(), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<div class="systemSpecificConfigs" id="reversingValveControl">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Reversing Valve Control","Reversing Valve Control")%>
    </div>
    <div class="row sensorEditFormInput">
        <select class="form-select" id="ReversingValve" name="ReversingValve" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <option value="0" <%= ReversingValve == 0 ?"selected='selected'":"" %>><%=Html.TranslateTag("Not Defined","Not Defined")%></option>
            <option value="1" <%= ReversingValve == 1 ?"selected='selected'":"" %>><%=Html.TranslateTag("Valve Energized to Cool","Valve Energized to Cool")%></option>
            <option value="2" <%= ReversingValve == 2 ?"selected='selected'":"" %>><%=Html.TranslateTag("Valve Energized to Heat","Valve Energized to Heat")%></option>
        </select>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {

        $('#enableLoadBalancingHeaterChk').change(function () {
            if ($('#enableLoadBalancingHeaterChk').prop('checked')) {
                $('#EnableLoadBalancingHeater').val(1);
            } else {
                $('#EnableLoadBalancingHeater').val(0);
            }
        });


        $('#enableLoadBalancingCoolerChk').change(function () {
            if ($('#enableLoadBalancingCoolerChk').prop('checked')) {
                $('#EnableLoadBalancingCooler').val(1);
            } else {
                $('#EnableLoadBalancingCooler').val(0);
            }
        });

    });

</script>
