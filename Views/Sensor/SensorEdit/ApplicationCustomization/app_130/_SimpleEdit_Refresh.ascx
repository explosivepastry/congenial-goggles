<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%	
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_UseWithRepeater.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model);

	//Profile Specific
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_130/_Pitch_Simple.ascx", Model);
%>
<% 	
	//Profile Specifi Hidden	
%>

<!-- Check to see if this should be added with logic -->
<%if (TriggeredTilt.UseAppVersion2Settings(Model))
    { %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_130/_Version2Settings_Simple.ascx", Model);%>
<%} %>

<% 	
	//Hidden Defaults
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_ActiveStateInterval.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_TimeOfDay.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_SyncronizeOffset.ascx", Model);
	//Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Hidden/_Recovery.ascx", Model);
%>
