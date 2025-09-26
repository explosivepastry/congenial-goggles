<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool isFahr = Thermostat.IsFahrenheit(Model.SensorID);


    double occupiedCoolingThresh = Thermostat.GetOccupiedCoolingThreshold(Model);
    double occupiedHeatingThresh = Thermostat.GetOccupiedHeatingThreshold(Model);
    double occupiedcoolingBuffer = Thermostat.GetOccupiedCoolingBuffer(Model);
    double occupiedheatingBuffer = Thermostat.GetOccupiedHeatingBuffer(Model);

    int OccupiedTempBuffer = Thermostat.GetOccupiedBuffer(Model.SensorID).ToInt();

    // calval2
    int occupiedTimeout = Thermostat.GetOccupiedTimeout(Model);

    if (isFahr)
    {
        occupiedCoolingThresh = occupiedCoolingThresh.ToFahrenheit();
        occupiedHeatingThresh = occupiedHeatingThresh.ToFahrenheit();
        occupiedcoolingBuffer = occupiedcoolingBuffer.ToFahrenheit();
        occupiedheatingBuffer = occupiedheatingBuffer.ToFahrenheit();

    }

   string tempUnitOfMeasure = Temperature.IsFahrenheit(Model.SensorID) ? "°F":"°C";
%>

<p class="useAwareState">Occupied Settings</p>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Set Occupied on Motion for","Set Occupied on Motion for")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="OccupiedTimeout" class="form-select" name="OccupiedTimeout" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <%-- <option value="0">Please Select a Timeout</option>--%>
            <option value="5" <%= occupiedTimeout == 5 ?"selected='selected'":"" %>>5 minutes</option>
            <option value="10" <%= occupiedTimeout == 10 ?"selected='selected'":"" %>>10 minutes</option>
            <option value="15" <%= occupiedTimeout == 15 ?"selected='selected'":"" %>>15 minutes</option>
            <option value="30" <%= occupiedTimeout == 30 ?"selected='selected'":"" %>>30 minutes</option>
            <option value="60" <%= occupiedTimeout == 60 ?"selected='selected'":"" %>>1 hour</option>
            <option value="90" <%= occupiedTimeout == 90 ?"selected='selected'":"" %>>1 hour and 30 minutes</option>
            <option value="120" <%= occupiedTimeout == 120 ?"selected='selected'":"" %>>2 hours</option>
            <option value="240" <%= occupiedTimeout == 240 ?"selected='selected'":"" %>>4 hours</option>
            <option value="480" <%= occupiedTimeout == 480 ?"selected='selected'":"" %>>8 hours</option>
            <option value="720" <%= occupiedTimeout == 720 ?"selected='selected'":"" %>>12 hours</option>
        </select>

    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature Deadpoint","Temperature Deadpoint")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select" onchange="OccupiedbufferDropDown(this.value);" id="OccupiedTempBuffer" name="OccupiedTempBuffer" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <option disabled>Please Select a Buffer</option>
            <option value="2" <%= OccupiedTempBuffer == 2 ?"selected='selected'":"" %>>2 degrees</option>
            <option value="3" <%= OccupiedTempBuffer == 3 ?"selected='selected'":"" %>>3 degrees</option>
            <option value="4" <%= OccupiedTempBuffer == 4 ?"selected='selected'":"" %>>4 degrees</option>
            <option value="5" <%= OccupiedTempBuffer == 5 ?"selected='selected'":"" %>>5 degrees</option>
            <option value="6" <%= OccupiedTempBuffer == 6 ?"selected='selected'":"" %>>6 degrees</option>
            <option value="7" <%= OccupiedTempBuffer == 7 ?"selected='selected'":"" %>>7 degrees</option>
            <option value="8" <%= OccupiedTempBuffer == 8 ?"selected='selected'":"" %>>8 degrees</option>
            <option value="9" <%= OccupiedTempBuffer == 9 ?"selected='selected'":"" %>>9 degrees</option>
            <option value="10" <%= OccupiedTempBuffer == 10 ?"selected='selected'":"" %>>10 degrees</option>
        </select>

    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Turn Heat on at:","Turn Heat on at:")%>
    </div>
    <div class="col sensorEditFormInput">
        <input <%= !Model.CanUpdate ? "disabled" : ""%> type="text" class="form-control" value="<%= (occupiedHeatingThresh).ToString("0.#")%>" name="OccupiedHeatingThreshold" id="OccupiedHeatingThreshold" />
        <%: Html.Label(tempUnitOfMeasure) %>
        <a id="OHTNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
        <input type="hidden" value="<%= occupiedheatingBuffer.ToString("0.#")%>" name="OccupiedHeatingBuffer" id="OccupiedHeatingBuffer" />
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Turn Cool on at:","Turn Cool on at:")%>
    </div>
    <div class="col sensorEditFormInput">
        <input <%= !Model.CanUpdate ? "disabled" : ""%> type="text" class="form-control" value="<%= occupiedCoolingThresh.ToString("0.#")%>" name="OccupiedCoolingThreshold" id="OccupiedCoolingThreshold" />
        <%: Html.Label(tempUnitOfMeasure) %>
        <a id="OCTNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
        <input type="hidden" value="<%= occupiedcoolingBuffer.ToString("0.#")%>" name="OccupiedCoolingBuffer" id="OccupiedCoolingBuffer" />
    </div>
</div>



<script type="text/javascript">

    var SliderMin = <%:(isFahr ? (10.0).ToFahrenheit():10.0)%>;
    var SliderMax = <%:(isFahr ? (40.0).ToFahrenheit():40.0)%>;
    const unitOfMeasureString = "<%=tempUnitOfMeasure%>";

    //MobiScroll
    $(function () {
          <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(SliderMin, SliderMax, 1);

        createSpinnerModal("OHTNum", unitOfMeasureString, "OccupiedHeatingThreshold", arrayForSpinner);
        createSpinnerModal("OCTNum", unitOfMeasureString, "OccupiedCoolingThreshold", arrayForSpinner);

        <%}%>


        $("#OccupiedHeatingThreshold").change(function () {
            if (isANumber($("#OccupiedHeatingThreshold").val())) {
                var occupiedHeatingThreshold = Number($("#OccupiedHeatingThreshold").val());

                var buffer = Number($('#OccupiedTempBuffer').val());

                if (occupiedHeatingThreshold < SliderMin) {
                    $("#OccupiedHeatingThreshold").val(SliderMin);
                    $("#OccupiedCoolingThreshold").change();
                }

                var occupiedCoolingThreshold = Number($("#OccupiedCoolingThreshold").val());

                if (occupiedHeatingThreshold + (buffer * 2) > occupiedCoolingThreshold) {
                    occupiedHeatingThreshold = occupiedCoolingThreshold - (buffer * 2);
                    $("#OccupiedHeatingThreshold").val(occupiedHeatingThreshold);
                }
            }
            else {
                $('#OccupiedHeatingThreshold').val(SliderMin);
            }
            $("#OccupiedHeatingBuffer").val(occupiedHeatingThreshold + buffer);


        });


        $("#OccupiedCoolingThreshold").change(function () {
            if (isANumber($("#OccupiedCoolingThreshold").val())) {
                var occupiedCoolingThreshold = Number($("#OccupiedCoolingThreshold").val());

                var buffer = Number($('#OccupiedTempBuffer').val());

                if (occupiedCoolingThreshold > SliderMax) {
                    $("#OccupiedCoolingThreshold").val(SliderMax);
                    $("#OccupiedHeatingThreshold").change();
                }

                var occupiedHeatingThreshold = Number($("#OccupiedHeatingThreshold").val());

                if (occupiedCoolingThreshold - (buffer * 2) < occupiedHeatingThreshold) {
                    occupiedCoolingThreshold = occupiedHeatingThreshold + (buffer * 2);
                    $("#OccupiedCoolingThreshold").val(occupiedCoolingThreshold);
                }
            }
            else {
                $('#OccupiedCoolingThreshold').val(SliderMax);
            }

            $("#OccupiedCoolingBuffer").val(occupiedCoolingThreshold - buffer);

        });

    });

    var lastOccupiedTempBuffer = <%=OccupiedTempBuffer%>;

    function OccupiedbufferDropDown(inputbuffer) {
        if ((Number($('#OccupiedHeatingThreshold').val()) + Number(inputbuffer * 2)) > Number($('#OccupiedCoolingThreshold').val())) {
            showSimpleMessageModal("<%=Html.TranslateTag("Invalid Temperature selection")%>");
            $("#OccupiedTempBuffer").val(lastOccupiedTempBuffer);
        }
        else {
            lastOccupiedTempBuffer = inputbuffer;
            $("#OccupiedHeatingThreshold").change();
        }


    }

</script>
