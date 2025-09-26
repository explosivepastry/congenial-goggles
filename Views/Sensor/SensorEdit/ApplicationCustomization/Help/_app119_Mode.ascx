<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Mode","Mode")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|There are 3 Possible Modes:","There are 3 Possible Modes:")%>
    </div>
    <div class="word-def" >
       1) <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|One Axel trigger – Imediately reports to gateway after a  pulse is registered.","One Axel trigger – Imediately reports to gateway after a  pulse is registered.")%>
    </div><br/>
    <div class="word-def ">
       2) <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Two Axel Trigger – Imediately reports to gateway when 2 counts are detected within the Detection Window.","Two Axel Trigger – Imediately reports to gateway when 2 counts are detected within the Detection Window.")%>
    </div><br/>
    <div class="word-def ">
       3) <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Two Axel count – Records a count when 2 pulses are dectected within the gateway window. When Heartbeat elapses the sensor reports the count to the gateway then resets the count to 0.","Two Axel count – Records a count when 2 pulses are dectected within the gateway window. When Heartbeat elapses the sensor reports the count to the gateway then resets the count to 0.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Detection Window","Detection Window")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|When in a \"2 Axel\" mode, this is the window of time in which both pulses have to happen in order for the pulses to be counted.","When in a \"2 Axel\" mode, this is the window of time in which both pulses have to happen in order for the pulses to be counted.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Re-Arm Time","Re-Arm Time")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|The amount of time in seconds that a sensor will ignore pulses after regiestering a count","The amount of time in seconds that a sensor will ignore pulses after regiestering a count")%>
        <hr />
    </div>
</div>