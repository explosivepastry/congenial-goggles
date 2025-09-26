<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_HeartBeat.ascx", Model); %>


<% Account acc = Account.Load(Model.AccountID);
   if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
   {%>

<%} %>




<div class="clearfix"></div>
