<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Relay Title","Relay Title")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/|Set the display name of Relay.","Set the display name of Relay.")%>
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/|Default State","Default State")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The Default State is the state the relay is in on power up and also the state that the relay is in while the paired sensor is in the 'Standard Operating State'. If a sensor is paired and the sensor enters the 'Aware' mode, the relay will toggle to the non default state.",
       "The Default State is the state the relay is in on power up and also the state that the relay is in while the paired sensor is in the 'Standard Operating State'. If a sensor is paired and the sensor enters the 'Aware' mode, the relay will toggle to the non default state.")%>
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/|LED Mode","LED Mode")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help/_ControlRelay|Controls the behavior of the led.",
       "Controls the behavior of the led.")%>
        <hr />
    </div>
</div>


