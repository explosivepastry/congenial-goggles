<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    /*
    ********************************************************************************
    
    This is not Coomplete , Started  then decided that user doesnt need to be able
    to set Aware state based on Temp for this sensor
    
    ********************************************************************************
    */

    if (!Monnit.VersionHelper.IsVersion_1_0(Model))
    {

        string TempMin = "";
        string TempMax = "";
        string TempHyst = "";

        TempMax = AirSpeed.TemperatureMaxThreshForUI(Model);
        TempMin = AirSpeed.TemperatureMinThreshForUI(Model);
        TempHyst = AirSpeed.TemperatureHystForUI(Model);

%>
<h5>Temperature Aware State</h5>

<%--Temp Min--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MinimumThreshold_Manual" id="MinimumThreshold_Manual" value="<%=(TempMin) %>" />
        <a id="minThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Calibration4)%>
    </div>
</div>

<%--Temp Max--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Above","Above")%> <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="MaximumThreshold_Manual" id="MaximumThreshold_Manual" value="<%=(TempMax) %>" />
        <a id="maxThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<%--Temp Hyst--%>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature Aware State Buffer","Temperature Aware State Buffer")%> <%: Html.Label(Temperature.IsFahrenheit(Model.SensorID)?"°F":"°C") %>
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Hysteresis_Manual" id="Hysteresis_Manual" value="<%=(TempHyst) %>" />
        <a id="hystThreshNum" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<%

    long TempDefaultMin = -40;
    long TempDefaultMax = 85;

    if (Temperature.IsFahrenheit(Model.SensorID))
    {
        TempDefaultMin = TempDefaultMin.ToDouble().ToFahrenheit().ToLong();
        TempDefaultMax = TempDefaultMax.ToDouble().ToFahrenheit().ToLong();
    }

%>

<script>
    var minVal = <%:(TempDefaultMin)%>;
    var maxVal = <%:(TempDefaultMax)%>;
    const label = '<%:Temperature.IsFahrenheit(Model.SensorID) ? "Fahrenheit" : "Celsius" %>';
    $(function () {
                <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(minVal, maxVal, 10);
        createSpinnerModal("minThreshNum", label, "MinimumThreshold_Manual", arrayForSpinner);
        createSpinnerModal("maxThreshNum", label, "MaximumThreshold_Manual", arrayForSpinner);

         <%}%>

        $("#MinimumThreshold_Manual").addClass('editField editFieldSmall');
        $("#MaximumThreshold_Manual").addClass('editField editFieldSmall');

        $("#MinimumThreshold_Manual").change(function () {
            if (isANumber($("#MinimumThreshold_Manual").val())) {
                if ($("#MinimumThreshold_Manual").val() < <%:(TempDefaultMin)%>)
                    $("#MinimumThreshold_Manual").val(<%:(TempDefaultMin)%>);

                if ($("#MinimumThreshold_Manual").val() > <%:(TempDefaultMax)%>)
                    $("#MinimumThreshold_Manual").val(<%:(TempDefaultMax)%>);
            } else
                $('#MinimumThreshold_Manual').val(<%: TempMin%>);
        });

        $("#MaximumThreshold_Manual").change(function () {
            if (isANumber($("#MaximumThreshold_Manual").val())) {
                if ($("#MaximumThreshold_Manual").val() < <%:(TempDefaultMin)%>)
                    $("#MaximumThreshold_Manual").val(<%:(TempDefaultMin)%>);

                if ($("#MaximumThreshold_Manual").val() > <%:(TempDefaultMax)%>)
                    $("#MaximumThreshold_Manual").val(<%:(TempDefaultMax)%>);
            }
            else
                $('#MaximumThreshold_Manual').val(<%: TempMax%>);
        });
    });


</script>
<%} %>