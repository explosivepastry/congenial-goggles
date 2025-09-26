<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%		
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model);

	//Profile Specific
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_094/_MinThreshold.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_094/_MaxThreshold.ascx", Model);
%>
<% 	
    //Profile Specific Hidden
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_094/_OffsetAcc_Hidden.ascx", Model);
%>

<% 	
    //Hidden Defaults
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_IsSensorActive.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_ActiveStateInterval.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_TimeOfDay.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_MeasurementsPerTransmission.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_SyncronizeOffset.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_Recovery.ascx", Model);
%>
