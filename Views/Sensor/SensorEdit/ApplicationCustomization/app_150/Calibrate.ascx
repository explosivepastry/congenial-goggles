<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    //purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
    Response.Cache.SetValidUntilExpires(false);
    Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetNoStore();

    TempData["CanCalibrate"] = true;
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);

    double humOffset = Motion_RH_WaterDetect.GetHumidityOffset(Model);
    double tempOffset = Motion_RH_WaterDetect.GetTemperatureOffset(Model);

    if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>



<%if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
    { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>



    <%  if (Model.CanUpdate && Model.LastDataMessage != null)
        {%>



    <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Humidity","Humidity")%></h2>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" name="observed" id="observed" readonly="readonly" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.GetPlotValues(Model.SensorID)[2] : "" %>" />%
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Humidity Offset","Humidity Offset")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" name="HumOffset" id="HumOffset" value="<%:(humOffset).ToString("0.00") %>" required />%
        </div>
    </div>

    <div style="clear: both;"></div>
    <br />
    <%

        bool isF = Motion_RH_WaterDetect.IsFahrenheit(Model.SensorID);

        string tempVal = Motion_RH_WaterDetect.Deserialize(Model.FirmwareVersion.ToString(), Model.LastDataMessage.Data.ToString()).Temperature.ToString();

        if (isF)
        {
            tempVal = tempVal.ToDouble().ToFahrenheit().ToString("0.00");
            tempOffset = (tempOffset * 1.8);
        }
    %>



    <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Temperature","Temperature")%></h2>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Observed Sensor Reading: (From sensor)","Observed Sensor Reading: (From sensor)")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" name="observedTemp" id="observedTemp" readonly="readonly" value="<%: Model.LastDataMessage != null ? tempVal.ToDouble().ToString() : "" %>" />
            <%:Monnit.Motion_RH_WaterDetect.IsFahrenheit(Model.SensorID)? "F" : "C" %>
        </div>
    </div>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Temperature Offset","Temperature Offset")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" id="TempOffset" name="TempOffset" type="text" value="<%:tempOffset.ToString("0.00")  %>" required />
            <%:isF? "F" : "C" %>
        </div>
    </div>

    <br />

    <script>


        $(function () {


            $("#TempOffset").change(function () {
                if (!isANumber($("#TempOffset").val())) {
                    $("#TempOffset").val(<%: tempOffset%>);
                } else {

                    if ($("#TempOffset").val() > 10)
                        $("#TempOffset").val(10);

                    if ($("#TempOffset").val() < -10)
                        $("#TempOffset").val(-10);

                }
            });


            $("#HumOffset").change(function () {
                if (!isANumber($("#HumOffset").val())) {
                    $("#HumOffset").val(0);
                } else {

                    if ($("#HumOffset").val() > 10)
                        $("#HumOffset").val(10);

                    if ($("#HumOffset").val() < -10)
                        $("#HumOffset").val(-10);

                }
            });


        });

    </script>

    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>


<%}
    else
    {
%>
<div class="formBody" style="font-weight: bold">
    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|Calibration for this sensor is not available for edit until pending transaction is complete.","Calibration for this sensor is not available for edit until pending transaction is complete.")%>
</div>
<div class="buttons">&nbsp; </div>
<%
    }
%>


<%}
    else
    {%>
<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_150|This sensor has been pre-calibrated and certified by","This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate","View Calibration Certificate")%></a>
    </div>
</div>
<%}%>


<%	}%>

