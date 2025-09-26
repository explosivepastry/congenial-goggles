<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<%--Basic Users--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_UseWithRepeater.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model); %>


<% if (MonnitSession.AccountCan("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
   {%>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_ActiveStateInterval.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_MeasurementsPerTransmission.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_TimeOfDay.ascx", Model);%>

<%if (new Version(Model.FirmwareVersion).Revision >= 3)
  { %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_075/_InvertPitchRollAxisSelection.ascx", Model);%>
<%} %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_075/_Pitch.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_075/_Roll.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_075/_Hysteresis.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SyncronizeOffset.ascx", Model);%>
<%--<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_Recovery.ascx", Model);%>--%>
<%} %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorPrint.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_PoESensor.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_LTESensor.ascx", Model);%>

<div class="clearfix"></div>
<div class="ln_solid"></div>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons.ascx", Model);%>