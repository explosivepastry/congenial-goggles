<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Mode","Mode")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vehicle Counter - Mode","Mode is a dropdown menu that allows you to choose whether you wish the sensor to detect or count oncoming vehicles. There are three possible modes:")%>
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|One Axel Trigger","One Axel Trigger")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vehicle Counter - Mode","Immediately reports to the gateway after registering a pulse.")%>
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Two Axel Trigger","Two Axel Trigger")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vehicle Counter - Mode","Immediately reports to the gateway when two counts are detected within the Detection Window.")%>
    </div>
        <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Two Axel Count","Two Axel Count")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vehicle Counter - Mode","Records a count when two pulses are detected within the Detection Window. When the Heartbeat elapses, the sensor reports the count to the gateway and then resets the count to 0.")%>
    </div>
    <hr />
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Detection Window (seconds)","Detection Window (seconds)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vehicle Counter - Detection Window","In Two Axel Mode, this is the window of time in which both pulses have to happen to be counted.")%>
    </div>
    <hr />
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Re-Arm Time (seconds)","Re-Arm Time (seconds)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Vehicle Counter - Re-Arm Time","The time after a triggering event that the sensor will wait before recognizing additional triggering events.")%>
    </div>
    <hr />
</div>




