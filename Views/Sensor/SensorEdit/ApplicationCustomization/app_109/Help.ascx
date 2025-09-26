<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_UseWithRepeater.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_HeartBeat.ascx", Model); %>


<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
   {%>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_TimeOfDay.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_ActiveStateInterval.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_MeasurementInterval.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Accumulate.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_MinThreshold.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_MaxThreshold.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Hysteresis.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_DutyThreshold.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);%>
<%} %>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_WifiSensor.ascx", Model); %>



<div class="clearfix"></div>
