<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|AdvancedTitle","Advanced Settings")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|AdvancedDescription","Allows for more advanced editing of thermostat.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|MotionTitle","Trigger On Motion")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|MotionDescription","If turned on signals aware on movement and will force a data point to the server.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|TimeoutTitle","Fan Control")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|TimeoutDescription","Various control states for the fan. Auto: HVAC system not the thermostat control the fan. Auto+Periodic: Enable configuration to turn the fan of for a period of time at the beinning of a larger period of time. Meant to be used in conjunction with Fan On Period and Fan Auto Period configuration.  If a cooling or heating event occurs, the Perodic Fan event time is reset.  Active Fan Control: The thermostat actively controls when the fan turns on/off when the heating/cooling is started and stopped. Meant to be used in conjuction with the various Fan Start Time configurations.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|PeriodTitle","Fan Period")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|PeriodDescription","Amount of time the fan is forced on during the Fan Interval. Ex: Fan On Period = 10 minutes, Fan Interval = 120 minutes. At the beginning of the 120 minute Fan Interval the fan will be forced on for 10 minutes. After 10 minutes the fan will turn off. 120 minutes after the fan was forced on the fan will be forced on again. Fan On Period must be less than Fan Auto Period.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|IntervalTitle","Fan Interval")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|IntervalDescription.","Interval in which Fan On Period operates. Fan Interval must be greater than Fan On Period.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|HeaterStartTitle","Fan Start Time for Heater")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|HeaterStartDescription","Fan starts this amount of time after the heater starts.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|HeaterStopTitle","Fan Stop Time for Heater")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|HeaterStopDescription","Fan stops this amount of time after the heater stops.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|CoolerStartTitle","Fan Start Delay for Cooler")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|CoolerStartDescription","Fan starts this amount of time after the cooler starts.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|CoolerStopTitle","Fan Stop Delay for Cooler")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Advanced97|CoolerStopDescription.","Fan stops this amount of time after the cooler starts.")%>
        <hr />
    </div>
</div>


