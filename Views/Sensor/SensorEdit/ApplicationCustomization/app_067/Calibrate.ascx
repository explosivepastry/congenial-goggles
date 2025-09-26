<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
    
<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    TempData["CanCalibrate"] = true;

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<% 

    string unitsforhyst = Ultrasonic.GetUnits(Model.SensorID).ToString();
%>


<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>

<%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    { %>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

<%} %>

<%
    if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    { %>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" name="observed" id="observed" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0") : "" %>" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>


    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_067|Measured Distance Reading", "Measured Distance Reading")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Model.LastDataMessage != null ? (Model.LastDataMessage.AppBase.Datums[0].data.ToDouble() == double.MinValue ? "" : Model.LastDataMessage.AppBase.Datums[0].data.ToDouble().ToString("0.0#")) + " " + Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(Model.SensorID)) : "Unavailable" %>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_067|Actual Distance Reading", "Actual Distance Reading")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Html.TextBox("actual", (Dictionary<string, object>)ViewData["HtmlAttributes"])%> <%: Ultrasonic.AbreviatedMesaurement(Ultrasonic.GetUnits(Model.SensorID)) %>
            <%: Html.Hidden("DistanceUnits", unitsforhyst) %>
        </div>
    </div>


    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%} %>

<%string unit = Ultrasonic.GetUnits(Model.SensorID).ToString();

  double minthresh = 20;
  double maxthresh = 400;


  if (unit == "Meter")
  {

      minthresh = (minthresh * 0.01);
      maxthresh = (maxthresh * 0.01);
  }

  if (unit == "Inch")
  {
      minthresh = (minthresh * 0.393701);
      maxthresh = (maxthresh * 0.393701);
  }

  if (unit == "Feet")
  {
      minthresh = (minthresh * 0.0328084);
      maxthresh = (maxthresh * 0.0328084);
  }

  if (unit == "Yard")
  {
      minthresh = (minthresh * 0.0109361);
      maxthresh = (maxthresh * 0.0109361);
  }
  
%>

<script>
    $(document).ready(function () {


        $('#actual').change(function (e) {
            e.preventDefault();
            var actual = parseFloat($('#actual').val())


            var lowVal = parseFloat(<%=minthresh%>);
            var highVal = parseFloat(<%=maxthresh%>);




            if (actual < lowVal) {
                $('#actual').val(lowVal.toFixed(2))
            }
            if (actual > highVal) {
                $('#actual').val(highVal.toFixed(2))
            }


        });

    });

</script>

<%} 
else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow)){ %>
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
