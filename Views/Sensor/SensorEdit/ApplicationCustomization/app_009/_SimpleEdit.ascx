<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_UseCaseOptions.ascx", Model);	
%>

	<%//Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_009/_ScaleOptions.ascx", Model);%> <!-- check causes error --> 
<div id="HiddenUntilUseCaseSelected">
	<div id="ValuesThatGetRefreshed"><%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_009/_SimpleEdit_Refresh.ascx", Model);%></div>
	<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_AllInOne_DevicePages.ascx", Model);%><!-- These views are for PoE, LTE, and MOWI fields -->
	<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_SaveButtons.ascx", Model);%>
</div>