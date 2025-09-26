<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%		
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model);

	//Profile Specific
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_101/_EnterAwareState.ascx", Model);
%>
<% 	
	//Profile Specific Hidden

	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_101/_ReportImmediately_Hidden.ascx", Model); //Check line 9
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_101/_Sensitivity_Hidden.ascx", Model);
%>

<% 	
    //Hidden Defaults
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_ActiveStateInterval.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_RearmTime.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_TimeOfDay.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_SyncronizeOffset.ascx", Model);
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_Recovery.ascx", Model);
%>
