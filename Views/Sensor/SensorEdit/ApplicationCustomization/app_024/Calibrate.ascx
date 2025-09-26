<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>

<%
	if (TempData["CanCalibrate"].ToBool())
	{  %>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{ %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <% Monnit.DataMessage flexMessage = Model.LastDataMessage;
		if (Model.CanUpdate && flexMessage != null)
		{

			string Display = flexMessage.DisplayData;
			if (Display.Length > 30)
				Display = Display.Substring(0, 30);

    %>
    <%: Html.Hidden("messageId", flexMessage.DataMessageGUID)%>
    <div class="form-group">
        <div class="bold col-md-9 col-sm-9 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|To calibrate the flex sensor:", "To calibrate the flex sensor:")%>
            <br />

            <ol>
                <li><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|Position the sensor with flex element in application with sensor at full or maximum bend", "Position the sensor with flex element in application with sensor at full or maximum bend")%></li>
                <li><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|Wait for one or two sensor readings to come in so the system has good sample for maximum bend", "Wait for one or two sensor readings to come in so the system has good sample for maximum bend")%></li>
                <li><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|Choose Maximum Bend from the drop down list below and press Calibrate", "Choose Maximum Bend from the drop down list below and press Calibrate")%></li>
                <li><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|Position the sensor with flex element in application with sensor at rest or minimum bend", "Position the sensor with flex element in application with sensor at rest or minimum bend")%></li>
                <li><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|Wait for one or two sensor readings to come in so the system has good sample for minimum bend", "Wait for one or two sensor readings to come in so the system has good sample for minimum bend")%></li>
                <li><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|Choose Minimum Bend from the drop down list below and press Calibrate", "Choose Minimum Bend from the drop down list below and press Calibrate")%></li>
                <li><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|Calibration complete.  Sensor will now show readings between Minimum Bend 0% and Maximum Bend 100%", "Calibration complete.  Sensor will now show readings between Minimum Bend 0% and Maximum Bend 100%")%></li>
            </ol>
        </div>
        <hr />
    </div>
    <div class="clear"></div>

    <div class="form-group">
        <div class="bold col-md-9 col-sm-9 col-xs-12">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_024|The last reading was recorded at", "The last reading was recorded at")%>  &nbsp;<%:Display%>
            <hr />
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_021|This will be considered", "This will be considered")%>
        </div>
        <div class="col sensorEditFormInput">
            <select name="limit" class="form-select">
                <option value="3">Maximum Bend</option>
                <option value="4">Minimum Bend</option>
            </select>
        </div>
    </div>
    <script>
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';
        //MobiScroll
        //$(function () {
        //    $('#limit').mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        minWidth: 200
        //    });
        //});
    </script>

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
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by", "This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate", "View Calibration Certificate")%></a>
    </div>
</div>
<%}
    }%>



