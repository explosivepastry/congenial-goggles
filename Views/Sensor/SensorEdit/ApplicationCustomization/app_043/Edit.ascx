<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<%--Basic Users--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_UseWithRepeater.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model); %>


<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
    {%>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_ActiveStateInterval.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_TimeOfDay.ascx", Model);%>


<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_isSensorActive.ascx", Model);%>


<% 
    if (Monnit.VersionHelper.IsVersion_1_0(Model))
    {
        //Don't render the Profile settings for version 1.0 Sensors
    }
    else
    {
%>


<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_MeasurementsPerTransmission.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SyncronizeOffset.ascx", Model);%>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_MinThreshold.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_MaxThreshold.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_Hysteresis.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_MinThresholdTemp.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_MaxThresholdTemp.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_HysteresisTemp.ascx", Model);%>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_Altitude.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_ShowGPP.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_ShowDewPoint.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_ShowHeatIndex.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_043/_ShowWetBulb.ascx", Model);%>

<%
        }
    } %>

<%--<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_Recovery.ascx", Model);%>--%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorPrint.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_PoESensor.ascx", Model);%>
<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_LTESensor.ascx", Model);%>

<div class="clearfix"></div>
<div class="ln_solid"></div>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons.ascx", Model);%>