<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_UseWithRepeater.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_HeartBeat.ascx", Model); %>

<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
   {%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_ActiveStateInterval.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_app142_Extra.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);%>

<%} %>
<%--<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Recovery.ascx", Model); %>--%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_WifiSensor.ascx", Model); %>



<div class="clearfix"></div>
