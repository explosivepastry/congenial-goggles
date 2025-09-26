<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Warmup Time","Warmup Time")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This determines how much time the device waits before taking a measurement after powering on.","This determines how much time the device waits before taking a measurement after powering on.")%>
        <hr />
    </div>
</div>
