<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_UseWithRepeater.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_HeartBeat.ascx", Model); %>

<% if (MonnitSession.AccountCan("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
    {%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_ActiveStateInterval.ascx", Model);%> 
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_TimeOfDay.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_app130_DataMode.ascx", Model);%>
<%--<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_app130_DeltaMode.ascx", Model);%>--%>


<%--<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Pitch130.ascx", Model);%>--%>
<%--<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_RearmTime.ascx", Model);%>--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);%>
<%} %>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_WifiSensor.ascx", Model); %>

<div class="clearfix"></div>
