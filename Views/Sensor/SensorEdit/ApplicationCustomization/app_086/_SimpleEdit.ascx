<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_UseCaseOptions.ascx", Model);	
%>

<div id="HiddenUntilUseCaseSelected">
	<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_002/_ScaleOptions.ascx", Model);%>
	<div id="ValuesThatGetRefreshed"><%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_086/_SimpleEdit_Refresh.ascx", Model);%></div>
	<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_AllInOne_DevicePages.ascx", Model);%><!-- These views are for PoE, LTE, and MOWI fields -->
	<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default_Simple/_SaveButtons.ascx", Model);%>
</div>