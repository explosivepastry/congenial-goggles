<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%		
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_UseWithRepeater.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model);

	//Profile Specific
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_002/_MinThreshold.ascx", Model); //original also pulls from profile 002
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_002/_MaxThreshold.ascx", Model); //original also pulls from profile 002
%>
<% 	
	//Profile Specific Hidden
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_002/_Hysteresis_Hidden.ascx", Model); //original also pulls from profile 002
%>

<% 	
    //Hidden Defaults
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_MeasurementsPerTransmission.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_ActiveStateInterval.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_TimeOfDay.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_MeasurementsPerTransmission.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_SyncronizeOffset.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_Recovery.ascx", Model);
%>
