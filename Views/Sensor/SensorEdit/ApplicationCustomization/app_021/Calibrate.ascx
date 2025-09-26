<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    TempData["CanCalibrate"] = true;

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>


<%  if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <div class="row sensorEditForm">
        <div class="col-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_021|Make sure the Light sensor is in a constant light condition then enter the actual lux. When this button is available again the calibration cycle is complete.","Make sure the Light sensor is in a constant light condition then enter the actual lux. When this button is available again the calibration cycle is complete.")%>
            <hr />
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_021|Actual LUX Reading:","Actual LUX Reading:")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Html.TextBox("actual")%>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_021|Filter Type:","Filter Type:")%>
        </div>
        <div class="col sensorEditFormInput">
            <select name="filter" class="form-select">
                <option value="1">Standard</option>
            </select>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_021|Ambient Temperature:","Ambient Temperature:")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Html.TextBox("tempTarget")%>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_021|Temperature Scale:","Temperature Scale:")%>
        </div>
        <div class="col-sm-9 col-12 ">
            <input class="form-check-input" type="radio" name="ForC" id="Radio1" value="f" checked="checked" />&nbsp;<label for="f"><%: Html.TranslateTag("Fahrenheit","Fahrenheit")%></label>&nbsp;&nbsp;
            <input class="form-check-input" type="radio" name="ForC" id="Radio2" value="c" />&nbsp;<label for="c"><%: Html.TranslateTag("Celsius","Celsius")%></label>
        </div>
    </div>

    <div class="clear"></div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>

<%}
    else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow)) 
    {%>
<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by","This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate","View Calibration Certificate")%></a>
    </div>
</div>
<%}%>


<script>
    $('#tempTarget').addClass('form-control')
</script>

