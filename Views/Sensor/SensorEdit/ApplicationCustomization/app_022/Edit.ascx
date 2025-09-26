<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>



<%--Basic Users--%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorName.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_UseWithRepeater.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_HeartBeat.ascx", Model); %>



<% Account acc = Account.Load(Model.AccountID);
    if (acc.CurrentSubscription.AccountSubscriptionType.Can("sensor_advanced_edit") && MonnitSession.CustomerCan("Sensor_Advanced_Configuration"))
    {
        Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_IsSensorActive.ascx", Model);
        Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_ActiveStateInterval.ascx", Model);
        Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_TimeOfDay.ascx", Model);

        if (Monnit.VersionHelper.IsVersion_1_0(Model))
        {
            //Don't render the Profile settings for version 1.0 Sensors
        }
        else
        {
            Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_MeasurementsPerTransmission.ascx", Model);
            Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_022/_MinThreshold.ascx", Model);
            Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_022/_MaxThreshold.ascx", Model);
            Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_022/_Hysteresis.ascx", Model);
            Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_022/_PowerOption.ascx", Model);
            if (Model.SensorTypeID != 4)
            {
                Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SyncronizeOffset.ascx", Model);
            }
        }

        //Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_Recovery.ascx", Model);

    }%>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SensorPrint.ascx", Model); %>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_WifiSensor.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_PoESensor.ascx", Model);%>
<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_LTESensor.ascx", Model);%>


<div class="clearfix"></div>
<div class="ln_solid"></div>

<%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveButtons.ascx", Model);%>