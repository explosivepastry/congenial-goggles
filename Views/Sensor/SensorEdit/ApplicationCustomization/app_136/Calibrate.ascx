<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    if (!string.IsNullOrEmpty(ViewBag.ErrorMessage))
    { %>
<div class="formBody" style="color: red;"><%:ViewBag.ErrorMessage %></div>
<%} %>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    {%>
<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>
    <%  if (Model.CanUpdate)
        {
            if (Model.LastDataMessage == null)
            {%>
    <div class="formBody" style="font-weight: bold">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Calibration for this sensor is not available until it has checked in at least one time.", "Calibration for this sensor is not available until it has checked in at least one time.")%> 
    </div>
    <div class="buttons">&nbsp; </div>

    <% }
        else
        {

            string tempVal = LCD_Temperature.Deserialize(Model.FirmwareVersion.ToString(), Model.LastDataMessage.Data.ToString()).Temp.ToString();

            string displayTempVal = tempVal;

            if (Monnit.LCD_Temperature.IsFahrenheit(Model.SensorID))
                displayTempVal = displayTempVal.ToDouble().ToFahrenheit().ToString("0.00");
    %>


    <div class="">
        <h2><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Temperature", "Temperature")%></h2>
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Observed Sensor Reading: (From sensor)", "Observed Sensor Reading: (From sensor)")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">

                <input type="hidden" name="observed" value="<%: Model.LastDataMessage != null ? tempVal.ToDouble().ToString() : "" %>" /> 
                <input style="text-align: center; width:70px;" name="observedDisplay" id="observedDisplay" readonly="readonly" value="<%: Model.LastDataMessage != null ? displayTempVal.ToDouble().ToString() : "" %>" />
                <%:Monnit.HandheldFoodProbe.IsFahrenheit(Model.SensorID) ? "°F" : "°C" %>
            </div>
        </div>
        <div style="clear: both;"></div>
        <br />
        <div class="form-group">
            <div class="bold col-md-3 col-sm-3 col-xs-12">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_043|Actual reading is", "Actual reading is")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <input type="number" style="text-align: center;width:70px;" name="actual" id="actual" value="<%: Model.LastDataMessage != null ? displayTempVal.ToDouble().ToString() : "" %>" required />
                <%:Monnit.HandheldFoodProbe.IsFahrenheit(Model.SensorID) ? "°F" : "°C" %>
            </div>
        </div>
        <div style="clear: both;"></div>
        <div class=" mdBox">
            <input type="hidden" name="tempScale" value="<%:Monnit.HandheldFoodProbe.IsFahrenheit(Model.SensorID) ? "F" : "C" %>" />
        </div>
    </div>



    <script>
        $(document).ready(function () {


        });
    </script>

    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%
        }
    }%>
<% else
    {%>
<div class="formBody" style="font-weight: bold">
    <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration is not available till data is present.","Calibration is not available till data is present.")%>
</div>
<div class="buttons">&nbsp; </div>
<%}%>

<%}
    else
    {%>
<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by","This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%= new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate","View Calibration Certificate")%></a>
    </div>
</div>
<%}%>
