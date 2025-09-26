<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<% 
    //check remove this section?
    string VibrationAwareThreshold = "";
    string VibrationSensitivityThreshold = "";

    string WindowFunction = "";
    string VibrationMode = "";
    string AccelerometerRange = "";
    string MeasurementInterval = "";
    string VibrationHysteresis = "";

    VibrationAwareThreshold = AdvancedVibration2.GetVibrationAwareThreshold(Model).ToString();
    VibrationSensitivityThreshold = AdvancedVibration2.GetVibrationSensitivityThreshold(Model).ToString();

    WindowFunction = AdvancedVibration2.GetWindowFunction(Model).ToString();
    VibrationMode = AdvancedVibration2.GetVibrationMode(Model).ToString();
    AccelerometerRange = AdvancedVibration2.GetAccelerometerRange(Model).ToString();
    MeasurementInterval = AdvancedVibration2.GetMeasurementInterval(Model).ToString("0.##");
    VibrationHysteresis = AdvancedVibration2.GetVibrationHysteresis(Model).ToString();

%>


<select hidden <%=Model.CanUpdate ? "" : "disabled" %> id="VibrationMode_Manual" name="VibrationMode_Manual" class="form-select">
    <option <%: VibrationMode == "0"? "selected":"" %> value="0">Velocity RMS</option>
    <option <%: VibrationMode == "1"? "selected":"" %> value="1">Acceleration RMS</option>
    <option <%: VibrationMode == "2"? "selected":"" %> value="2">Acceleration Peak</option>
    <option <%: VibrationMode == "4"? "selected":"" %> value="4">Displacement</option>
</select>


<input hidden class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationAwareThreshold_Manual" id="VibrationAwareThreshold_Manual" value="<%=VibrationAwareThreshold %>" />
<%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>

<input hidden class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationHysteresis_Manual" id="VibrationHysteresis_Manual" value="<%=VibrationHysteresis %>" />
<%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>

<input hidden class="form-control" type="number" step="any" <%=Model.CanUpdate ? "" : "disabled" %> name="VibrationSensitivityThreshold_Manual" id="VibrationSensitivityThreshold_Manual" value="<%=VibrationSensitivityThreshold %>" />
<%: Html.ValidationMessageFor(model => model.Calibration3)%>


<select hidden <%=Model.CanUpdate ? "" : "disabled" %> id="WindowFunction_Manual" name="WindowFunction_Manual" class="editField editFieldMedium tzSelect">
    <option <%: WindowFunction == "0"? "selected":"" %> value="0">Rect</option>
    <option <%: WindowFunction == "1"? "selected":"" %> value="1">Flat Top</option>
    <option <%: WindowFunction == "2"? "selected":"" %> value="2">Hanning</option>
</select>


<select hidden <%=Model.CanUpdate ? "" : "disabled" %> id="AccelerometerRange_Manual" name="AccelerometerRange_Manual" class="editField editFieldMedium tzSelect">
    <option <%: AccelerometerRange == "0"? "selected":"" %> value="0">8 g</option>
    <option <%: AccelerometerRange == "1"? "selected":"" %> value="1">16 g</option>
    <option <%: AccelerometerRange == "2"? "selected":"" %> value="2">32 g</option>
</select>


<input hidden class="form-control" id="MeasurementInterval_Manual" <%=Model.CanUpdate ? "" : "disabled" %> name="MeasurementInterval_Manual" type="number" value="<%=MeasurementInterval %>">
<%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
