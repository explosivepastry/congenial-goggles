<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    TempData["CanCalibrate"] = true;

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);


    if (new Version(Model.FirmwareVersion) > new Version("2.0.0.0") || Model.SensorTypeID == 4)//Post 2.0 
		//TempData["CanCalibrate"] = false;    

        if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        {%>

<div class="row sensorEditForm">
	<div class="col-12 col-md-3">
		<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_055|Last Sensor Reading:", "Last Sensor Reading:")%>
	</div>
	<div class="col sensorEditFormInput">
		<input name="observed" id="Text1" class="form-control" readonly="readonly" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>" />
	</div>
</div>
<div class="clear"></div>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
	<input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

	<%: Html.ValidationSummary(false)%>
	<%: Html.Hidden("id", Model.SensorID)%>
	<%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{ 
			Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);
		} %>
	<div class="row sensorEditForm">
		<div class="col-12 col-md-3 calOptions">
			<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_055|Actual Input: (External reference value)", "Actual Input: (External reference value)")%>
		</div>
		<div class="col sensorEditFormInput calOptions">
			<input name="actual" id="Text2" class="form-control" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>" />
		</div>
	</div>

	<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}%>