<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Pulse Edge Detection","Pulse Edge Detection")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Determines whether pulses are recoreded at the positive edge of the pulse or the negative edge of the pulse.","Determines whether pulses are recoreded at the positive edge of the pulse or the negative edge of the pulse.")%>
        <hr />
    </div>
</div>
