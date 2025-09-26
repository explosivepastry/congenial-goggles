<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

	TempData["CanCalibrate"] = true;
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);

	if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{
		  if (HumiditySHT25.UseInternalCalibration(Model))
		  { 
			Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/InternalCalibrate.ascx", Model);
		  }
		  else
		  { 
			 Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/AttributeCalibrate.ascx", Model);
		  }
	}%>

