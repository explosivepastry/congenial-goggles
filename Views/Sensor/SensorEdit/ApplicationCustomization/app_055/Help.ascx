<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_UseWithRepeater.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_HeartBeat.ascx", Model); %>
<%--<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_CTSize.ascx", Model); %>--%>


<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
    {
        if (Monnit.VersionHelper.IsVersion_1_0(Model))
        {
            //Don't render the Profile settings for version 1.0 Sensors
        }
        else
        {

            Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_ActiveStateInterval.ascx", Model);
            Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_TimeOfDay.ascx", Model);

            if (Model.MonnitApplication.IsTriggerProfile == eApplicationProfileType.Interval)
            {
                Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_MeasurementsPerTransmission.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_MinThreshold.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_MaxThreshold.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Hysteresis.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);
            }
            else //eApplicationProfileType.Trigger
            {
                Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_EventDetection.ascx", Model);
                Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_RearmTime.ascx", Model);
            }
        }
    }
    //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Recovery.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_WifiSensor.ascx", Model); %>



<div class="clearfix"></div>
