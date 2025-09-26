<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

	TempData["CanCalibrate"] = true;

	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<% 
	//purgeclassic
	Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
	Response.Cache.SetValidUntilExpires(false);
	Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
	Response.Cache.SetCacheability(HttpCacheability.NoCache);
	Response.Cache.SetNoStore();
%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
	

	<%: Html.ValidationSummary(false)%>
	<%: Html.Hidden("id", Model.SensorID)%>

	<%if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	  { %>

	<% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

	<%} %>

	<%  if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow) && Model.LastDataMessage != null)
		{%>

	<input type="hidden" name="last" value="<%:Model.LastDataMessage.DataMessageGUID %>" />


	<div class="form-group">
		<div class="bold col-md-3 col-sm-3 col-xs-12">
			<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_122|Last Sensor Reading:(must be greater than 5 to calibrate)", "Last Sensor Reading:(must be greater than 5 to calibrate)")%>
		</div>
		<div class="col-md-9 col-sm-9 col-xs-12 mdBox">
			<input name="observed" id="Text1" readonly="readonly" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>" />
			<%: VoltageMeter500VAC.GetLabel(Model.SensorID)  %>
		</div>
	</div>
	<div class="clear"></div>
	<br />

	<div class="form-group">
		<div class="bold col-md-3 col-sm-3 col-xs-12 calOptions">
			<%: Html.TranslateTag("Sensor/ApplicationCustomization/app_122|Actual Input: (External reference value)", "Actual Input: (External reference value)")%>
		</div>
		<div class="col-md-9 col-sm-9 col-xs-12 mdBox calOptions">
			<input name="actual" id="Text2" value="<%: Model.LastDataMessage != null ? Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.000") : "" %>" />
			<%: VoltageMeter500VAC.GetLabel(Model.SensorID)  %>
		</div>
	</div>
	<div class="clear"></div>
	<br />

	<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%} %>