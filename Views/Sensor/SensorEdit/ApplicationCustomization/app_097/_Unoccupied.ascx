<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    bool isFahr = Thermostat.IsFahrenheit(Model.SensorID);

    //min thresh
    double coolingThreshold = Thermostat.GetCoolingThreshold(Model);
    double heatingThreshold = Thermostat.GetHeatingThreshold(Model);
    double coolingBuffer = Thermostat.GetCoolingBuffer(Model);
    double heatingBuffer = Thermostat.GetHeatingBuffer(Model);

    int UnoccupiedTempBuffer = Thermostat.GetUnoccupiedBuffer(Model.SensorID).ToInt(); // change later
    string tempUnitOfMeasure = Temperature.IsFahrenheit(Model.SensorID) ? "°F":"°C";

    if (isFahr)
    {
        coolingThreshold = coolingThreshold.ToFahrenheit();
        heatingThreshold = heatingThreshold.ToFahrenheit();
        coolingBuffer = coolingBuffer.ToFahrenheit();
        heatingBuffer = heatingBuffer.ToFahrenheit();

    }
%>


<p class="useAwareState">Unoccupied Settings</p>


<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature Deadpoint","Temperature Deadpoint")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select" onchange="bufferDropDown(this.value);" id="TempBuffer" name="TempBuffer" <%= !Model.CanUpdate ? "disabled" : ""%>>
            <option disabled>Please Select a Buffer</option>
            <option value="2" <%= UnoccupiedTempBuffer == 2 ? "selected='selected'":"" %>>2 degrees</option>
            <option value="3" <%= UnoccupiedTempBuffer == 3 ? "selected='selected'":"" %>>3 degrees</option>
            <option value="4" <%= UnoccupiedTempBuffer == 4 ? "selected='selected'":"" %>>4 degrees</option>
            <option value="5" <%= UnoccupiedTempBuffer == 5 ? "selected='selected'":"" %>>5 degrees</option>
            <option value="6" <%= UnoccupiedTempBuffer == 6 ? "selected='selected'":"" %>>6 degrees</option>
            <option value="7" <%= UnoccupiedTempBuffer == 7 ? "selected='selected'":"" %>>7 degrees</option>
            <option value="8" <%= UnoccupiedTempBuffer == 8 ? "selected='selected'":"" %>>8 degrees</option>
            <option value="9" <%= UnoccupiedTempBuffer == 9 ? "selected='selected'":"" %>>9 degrees</option>
            <option value="10" <%= UnoccupiedTempBuffer == 10 ? "selected='selected'":"" %>>10 degrees</option>
        </select>

    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Turn Heat on at:","Turn Heat on at:")%>
    </div>
    <div class="col sensorEditFormInput">
        <input <%= !Model.CanUpdate ? "disabled" : ""%> type="text" class="form-control" value="<%= (heatingThreshold).ToString("0.#")%>" name="HeatingThreshold" id="HeatingThreshold" />
        <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
        <a id="HTNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
        <input type="hidden" value="<%=heatingBuffer%>" name="HeatingBuffer" id="HeatingBuffer" />
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Turn Cool on at:","Turn Cool on at:")%>
    </div>
    <div class="col sensorEditFormInput">
        <input <%= !Model.CanUpdate ? "disabled" : ""%> type="text" class="form-control" value="<%= (coolingThreshold).ToString("0.#")%>" name="CoolingThreshold" id="CoolingThreshold" />
        <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
        <a id="CTNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
        <input type="hidden" value="<%=coolingBuffer%>" name="CoolingBuffer" id="CoolingBuffer" />
    </div>
</div>



<script type="text/javascript">

    var SliderMin = <%:(isFahr ? (10.0).ToFahrenheit():10.0)%>;
    var SliderMax = <%:(isFahr ? (40.0).ToFahrenheit():40.0)%>;
    var unitOfMeasureString1 = "<%=tempUnitOfMeasure%>";

    //MobiScroll
    $(function () {
          <% if (Model.CanUpdate)
    { %>

        let arrayForSpinner = arrayBuilder(SliderMin, SliderMax, 1);

        createSpinnerModal("HTNum", unitOfMeasureString1, "HeatingThreshold", arrayForSpinner);
        createSpinnerModal("CTNum", unitOfMeasureString1, "CoolingThreshold", arrayForSpinner);

        <%}%>


        $("#HeatingThreshold").change(function () {
            if (isANumber($("#HeatingThreshold").val())) {

                var heatingThreshold = Number($("#HeatingThreshold").val());
                var buffer = Number($('#TempBuffer').val());

                if (heatingThreshold < SliderMin) {

                    $("#HeatingThreshold").val(SliderMin);
                    $("#CoolingThreshold").change();
                }

                var coolingThreshold = Number($("#CoolingThreshold").val());
                if (heatingThreshold + (buffer * 2) > coolingThreshold) {
                    heatingThreshold = coolingThreshold - (buffer * 2);
                    $("#HeatingThreshold").val(heatingThreshold);
                }
            }
            else {
                $('#HeatingThreshold').val(SliderMin);
            }


            $("#HeatingBuffer").val(heatingThreshold + buffer);


        });

        $("#CoolingThreshold").change(function () {
            if (isANumber($("#CoolingThreshold").val())) {

                var coolingThreshold = Number($("#CoolingThreshold").val());
                var buffer = Number($('#TempBuffer').val());

                if (coolingThreshold > SliderMax) {
                    $("#CoolingThreshold").val(SliderMax);
                    $("#HeatingThreshold").change();
                }

                var heatingThreshold = Number($("#HeatingThreshold").val());

                if (coolingThreshold - (buffer * 2) < heatingThreshold) {
                    coolingThreshold = heatingThreshold + (buffer * 2);
                    $("#CoolingThreshold").val(coolingThreshold);
                }
            }
            else {
                $('#CoolingThreshold').val(SliderMax);
            }

            $("#CoolingBuffer").val(coolingThreshold - buffer);

        });
    });

    var lastTempBuffer = <%=UnoccupiedTempBuffer%>;
    function bufferDropDown(inputbuffer) {
        if ((Number($('#HeatingThreshold').val()) + Number(inputbuffer * 2)) > Number($('#CoolingThreshold').val())) {
            showSimpleMessageModal("<%=Html.TranslateTag("Invalid Temperature selection")%>");
            $("#TempBuffer").val(lastTempBuffer);
        }
        else {
            lastTempBuffer = inputbuffer;
            $("#HeatingThreshold").change();
            //$("#CoolingThreshold").change();
        }
    }

</script>
