<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<%--Basic Users--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_134/_HeartBeat.ascx", Model); %>


<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit"))
   {%>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_140/_ActiveReportInterval.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_140/_ActiveTemperatureDelta.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_140/_ActivePressureDelta.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_140/_ActiveCurrentDelta.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_140/_ActiveSoundThreshold.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_140/_VoltageThreshold.ascx", Model);%>


<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SyncronizeOffset.ascx", Model);%>
<%--<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_Recovery.ascx", Model);%>--%>

<%} %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorPrint.ascx", Model); %>

<div class="clearfix"></div>
<div class="ln_solid"></div>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons.ascx", Model);%>