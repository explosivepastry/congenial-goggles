<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>




<%--Basic Users--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_UseWithRepeater.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model); %>

<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
   {%>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_ActiveStateInterval.ascx", Model);%>

<div class="ln_solid"></div>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_TimeOfDay.ascx", Model);%>
<%if (new Version(Model.FirmwareVersion) < new Version("25.44.39.5")) { %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_MeasurementsPerTransmission.ascx", Model);%>
<%} %>
<%--Low Light Threshold (Min) and Saturated Light Threshold (Max)--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_139/_Threshold.ascx", Model);%> 
<div class="ln_solid"></div>
<%--Light Light Buffer--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_139/_Hysteresis.ascx", Model);%>

<%if (new Version(Model.FirmwareVersion) >= new Version("25.44.39.5")) { %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_139/_DLIStartTime.ascx", Model);%> 
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_139/_MeasurementInterval.ascx", Model);%> 
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_139/_MaximumDLIThreshold.ascx", Model);%> 
<%} %>

<%--Enable Temperature Compensation--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_139/_TemperatureCompensation.ascx", Model);%>
<%} %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorPrint.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_PoESensor.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_LTESensor.ascx", Model);%>

<div class="clearfix"></div>
<div class="ln_solid"></div>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons.ascx", Model);%>