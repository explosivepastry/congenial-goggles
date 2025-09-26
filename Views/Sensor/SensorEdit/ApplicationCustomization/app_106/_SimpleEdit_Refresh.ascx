<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%--<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_002/_ScaleOptions.ascx", Model);%>--%>

<%		

	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model);

	//Profile Specific
	
%>
<% 	
	//Profile Specific Hidden
 	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_106/_Threshold_Hidden.ascx", Model);
%>
<% 	
	//Hidden Defaults
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_ActiveStateInterval.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_TimeOfDay.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_MeasurementsPerTransmission.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_SyncronizeOffset.ascx", Model);
	//Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_Recovery.ascx", Model);
%>
