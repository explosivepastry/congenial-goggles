<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    int invertPitch = Tilt.GetInvertPitch(Model);
    int invertRoll = Tilt.GetInvertRoll(Model);
    
%>

<h5><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Axis & Pitch Selection","Axis & Pitch Selection")%></h5>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Principal Axis Selection","Principal Axis Selection")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select"  id="PrincipalAxisSelection" name="PrincipalAxisSelection">
            <option value="0" <%:Model.Calibration2 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Pitch rotates around Y axis, Roll rotates around X axis","Pitch rotates around Y axis, Roll rotates around X axis")%></option>
            <option value="1" <%:Model.Calibration2 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Pitch rotates around X axis, Roll rotates around Z axis","Pitch rotates around X axis, Roll rotates around Z axis")%></option>
            <option value="2" <%:Model.Calibration2 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Pitch rotates around Z axis, Roll rotates around Y axis","Pitch rotates around Z axis, Roll rotates around Y axis")%></option>
            
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Dominant Axis Selection","Dominant Axis Selection")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select"  id="DominantAxisSelection" name="DominantAxisSelection">
            <option value="0" <%:Model.Calibration4 == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Auto Select","Auto Select")%></option>
            <option value="1" <%:Model.Calibration4 == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Pitch Dominant","Pitch Dominant")%></option>
            <option value="2" <%:Model.Calibration4 == 2 ? "selected='selected'" : "" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Roll Dominant","Roll Dominant")%></option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Invert Pitch","Invert Pitch")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select"  id="InvertPitch" name="InvertPitch">
            <option value="0" <%:invertPitch == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("False","False")%></option>
            <option value="1" <%:invertPitch == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("True","True")%></option>
        </select>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_075|Invert Roll","Invert Roll")%>
    </div>
    <div class="col sensorEditFormInput">
        <select class="form-select"  id="InvertRoll" name="InvertRoll">
            <option value="0" <%:invertRoll == 0 ? "selected='selected'" : "" %>><%: Html.TranslateTag("False","False")%></option>
            <option value="1" <%:invertRoll == 1 ? "selected='selected'" : "" %>><%: Html.TranslateTag("True","True")%></option>
        </select>
    </div>
</div>
<div class="clear"></div>
<br />
