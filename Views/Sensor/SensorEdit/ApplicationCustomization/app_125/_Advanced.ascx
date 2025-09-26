<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%

    //calval3
    int fanControl = MultiStageThermostat.GetFanControl(Model);
    int fanOnPeriod = MultiStageThermostat.GetFanOnPeriod(Model);
    int fanOnInterval = MultiStageThermostat.GetFanOnInterval(Model);

    //calval4
    int fanstarttimeforheater = MultiStageThermostat.GetFanStartTimeForHeater(Model);
    int fanstoptimeforheater = MultiStageThermostat.GetFanStopTimeForHeater(Model);
    int fanstartdelayforcooler = MultiStageThermostat.GetFanStartDelayForCooler(Model);
    int fanstopdelayforcooler = MultiStageThermostat.GetFanStopDelayForCooler(Model);

    bool awareWhenOccupied = MultiStageThermostat.GetAwareWhenOccupied(Model);
    bool awareOnStateChange = MultiStageThermostat.GetAwareOnStateChange(Model);

%>


<div class="row sensorEditForm useAwareState">
    <div class="col-12 col-md-3">
        <p style="margin-bottom: 0;">Advanced Settings</p>
    </div>
    <div class="col sensorEditFormInput standard" id="hyst3">
        <a style="color: var(--options-icon-color); font-size: 0.75rem; font-style: italic;" href="#" onclick="showAdvanced(true);return false;"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Show Advanced Settings","Show Advanced Settings")%>
        </a>
    </div>
    <div class="col sensorEditFormInput advanced-quick-exit" id="Div1">
        <a style="color: var(--options-icon-color); font-size: 0.75rem; font-style: italic;" href="#" onclick="showAdvanced(false);return false;">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Hide Advanced Settings","Hide Advanced Settings")%>
        </a>
    </div>
</div>

<div class="advanced">
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware when Occupied","Aware when Occupied")%>
            </div>
            <div class="col sensorEditFormInput">
                <div class="form-check form-switch d-flex align-items-center ps-0">
                    <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="awareWhenOccupiedChk" id="awareWhenOccupiedChk" <%=awareWhenOccupied ? "checked" : "" %>>
                    <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                </div>
                <div style="display: none;"><%: Html.TextBox("AwareWhenOccupied",awareWhenOccupied.ToInt(), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
            </div>
        </div>

        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware on State Change","Aware on State Change")%>
            </div>
            <div class="col sensorEditFormInput">
                <div class="form-check form-switch d-flex align-items-center ps-0">
                    <label class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
                    <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="awareOnStateChangeChk" id="awareOnStateChangeChk" <%=awareOnStateChange ? "checked" : "" %>>
                    <label class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
                </div>
                <div style="display: none;"><%: Html.TextBox("AwareOnStateChange",awareOnStateChange.ToInt(), (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
            </div>
        </div>

        <div class="useAwareState">
            <%: Html.Label(Html.TranslateTag("Fan Settings","Fan Settings"))%>
        </div>
        <div class="row sensorEditForm">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Control Mode","Control Mode")%>
            </div>
            <div class="col sensorEditFormInput" id="Div3">
                <select id="FanControl" name="FanControl" <%= !Model.CanUpdate ? "disabled" : ""%> class="form-select">
                    <option value="0" <%= fanControl == 0 ?"selected='selected'":"" %>>Auto</option>
                    <option value="1" <%= fanControl == 1 ?"selected='selected'":"" %>>Auto + Periodic</option>
                    <option value="2" <%= fanControl == 2 ?"selected='selected'":"" %>>On</option>
                    <option value="3" <%= fanControl == 3 ?"selected='selected'":"" %>>Active Fan Control</option>
                    <option value="4" <%= fanControl == 4 ?"selected='selected'":"" %>>Off</option>
                </select>
            </div>
        </div>

        <div class="row sensorEditForm fanAuto">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Period","Fan Period")%> (<%: Html.Label("Minutes") %>)
            </div>
            <div class="col sensorEditFormInput" id="Div4">
                <input class="form-control" type="number" id="FanOnPeriod" name="FanOnPeriod" <%= !Model.CanUpdate ? "disabled" : ""%> value="<%=(fanOnPeriod)%>" />
                <a id="FanOnPeriodNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            </div>
        </div>

        <div class="row sensorEditForm fanAuto">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Interval","Fan Interval")%> (<%: Html.Label("Minutes") %>)
            </div>
            <div class="col sensorEditFormInput" id="Div5">
                <input class="form-control" type="number" id="FanOnInterval" name="FanOnInterval" <%= !Model.CanUpdate ? "disabled" : ""%> value="<%=(fanOnInterval)%>" />
                <a id="FanOnIntervalNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            </div>
        </div>

        <div class="row sensorEditForm fanOn">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Start Time: Heat","Fan Start Time: Heat")%> (<%: Html.Label("Seconds") %>)
            </div>
            <div class="col sensorEditFormInput" id="Div6">
                <input class="form-control" type="number" name="FanStartTimeForHeater" id="FanStartTimeForHeater" value="<%=fanstarttimeforheater %>" <%= !Model.CanUpdate ? "disabled" : ""%> />
                <a id="FanStartTimeForHeaterNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            </div>
        </div>
        <div class="row sensorEditForm fanOn">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Stop Time: Heat","Fan Stop Time: Heat")%> (<%: Html.Label("Seconds") %>)
            </div>
            <div class="col sensorEditFormInput" id="Div7">
                <input class="form-control" type="number" name="Fanstoptimeforheater" id="FanStopTimeForHeater" value="<%=fanstoptimeforheater %>" <%= !Model.CanUpdate ? "disabled" : ""%> />
                <a id="FanStopTimeForHeaterNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            </div>
        </div>
        <div class="row sensorEditForm fanOn">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Start Time: Cool","Fan Start Time: Cool")%> (<%: Html.Label("Seconds") %>)
            </div>
            <div class="col sensorEditFormInput" id="Div8">
                <input class="form-control" type="number" name="FanStartDelayForCooler" id="FanStartDelayForCooler" value="<%=fanstartdelayforcooler %>" <%= !Model.CanUpdate ? "disabled" : ""%> />
                <a id="FanStartDelayForCoolerNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            </div>
        </div>
        <div class="row sensorEditForm fanOn">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Stop Time: Cool","Fan Stop Time: Cool")%> (<%: Html.Label("Seconds") %>)
            </div>
            <div class="col sensorEditFormInput" id="Div9">
                <input class="form-control" type="number" name="FanStopDelayForCooler" id="FanStopDelayForCooler" value="<%=fanstopdelayforcooler %>" <%= !Model.CanUpdate ? "disabled" : ""%> />
                <a id="FanStopDelayForCoolerNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
            </div>
        </div>
    </div>

<script type="text/javascript">

    

    $(document).ready(function () {

               <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(0, 240, 5);
        createSpinnerModal("FanOnPeriodNum", "Minutes", "FanOnPeriod", arrayForSpinner);
        const arrayForSpinner1 = arrayBuilder(0, 720, 5);
        createSpinnerModal("FanOnIntervalNum", "Minutes", "FanOnInterval", arrayForSpinner1);
        const arrayForSpinner2 = arrayBuilder(-300, 300, 5);
        createSpinnerModal("FanStartTimeForHeaterNum", "Seconds", "FanStartTimeForHeater", arrayForSpinner2);
        createSpinnerModal("FanStopTimeForHeaterNum", "Seconds", "FanStopTimeForHeater", arrayForSpinner2);
        createSpinnerModal("FanStartDelayForCoolerNum", "Seconds", "FanStartDelayForCooler", arrayForSpinner2);
        createSpinnerModal("FanStopDelayForCoolerNum", "Seconds", "FanStopDelayForCooler", arrayForSpinner2);

    <%}%>

        $('#FanOnInterval').change(function () {

            if (parseInt($('#FanOnInterval').val()) <= parseInt($('#FanOnPeriod').val())) {
                if (parseInt($('#FanOnInterval').val()) != 0) {
                    $('#FanOnPeriod').val(parseInt($('#FanOnInterval').val()) - 5);
                }
                else {
                    $('#FanOnInterval').val(1);
                    $('#FanOnPeriod').val(0);
                }
            }
        });

        $('#FanOnPeriod').change(function () {

            if (parseInt($('#FanOnInterval').val()) == parseInt($('#FanOnPeriod').val()) && parseInt($('#FanOnInterval').val()) != 0)
                $('#FanOnPeriod').val(parseInt($('#FanOnInterval').val()) - 5);
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

        $('#FanStartTimeForHeater').change(function () {
            if (isANumber($("#FanStartTimeForHeater").val())) {
                if ($("#FanStartTimeForHeater").val() < -300)
                    $("#FanStartTimeForHeater").val(-300);
                if ($("#FanStartTimeForHeater").val() > 300)
                    $("#FanStartTimeForHeater").val(300);
            } else {

                $("#FanStartTimeForHeater").val(<%: fanstarttimeforheater%>);
            }
        });

        $('#FanStopTimeForHeater').change(function () {
            if (isANumber($("#FanStopTimeForHeater").val())) {
                if ($("#FanStopTimeForHeater").val() < -300)
                    $("#FanStopTimeForHeater").val(-300);
                if ($("#FanStopTimeForHeater").val() > 300)
                    $("#FanStopTimeForHeater").val(300);
            } else {

                $("#FanStopTimeForHeater").val(<%: fanstoptimeforheater%>);
            }
        });

        $('#FanStartDelayForCooler').change(function () {
            if (isANumber($("#FanStartDelayForCooler").val())) {
                if ($("#FanStartDelayForCooler").val() < -300)
                    $("#FanStartDelayForCooler").val(-300);
                if ($("#FanStartDelayForCooler").val() > 300)
                    $("#FanStartDelayForCooler").val(300);
            } else {

                $("#FanStartDelayForCooler").val(<%: fanstartdelayforcooler%>);
            }
        });

        $('#FanStopDelayForCooler').change(function () {
            if (isANumber($("#FanStopDelayForCooler").val())) {
                if ($("#FanStopDelayForCooler").val() < -300)
                    $("#FanStopDelayForCooler").val(-300);
                if ($("#FanStopDelayForCooler").val() > 300)
                    $("#FanStopDelayForCooler").val(300);
            } else {

                $("#FanStopDelayForCooler").val(<%: fanstopdelayforcooler%>);
            }
        });


        $('#awareWhenOccupiedChk').change(function () {
            if ($('#awareWhenOccupiedChk').prop('checked')) {
                $('#AwareWhenOccupied').val(1);
            } else {
                $('#AwareWhenOccupied').val(0);
            }
        });

        $('#awareOnStateChangeChk').change(function () {
            if ($('#awareOnStateChangeChk').prop('checked')) {
                $('#AwareOnStateChange').val(1);
            } else {
                $('#AwareOnStateChange').val(0);
            }
        });

    });

    function showAdvanced(adv) {
     
        if (adv) {
            
            $('.advanced-quick-exit').show();
            $('.advanced').fadeIn();
            $('.standard').hide();

            var hide = $('#FanControl').val();
            $(".fanOn").fadeOut();
            $(".fanAuto").fadeOut();

            if (hide == 1) {
                $(".fanAuto").fadeIn();
            }
            else if (hide == 3) {
                $(".fanOn").fadeIn();
            }
        }
        else {
            $('.standard').show();
            $('.advanced').fadeOut();
            $('.advanced-quick-exit').hide();
        }
    }

</script>
