<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Alarm Time","Alarm Time")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|How long the alarm is on before snoozing.","How long the alarm is on before snoozing.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Snooze Time","Snooze Time")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|How long the alarm is turned off before alarming again (a new notification will force the device back into the alarm state).",
       "How long the alarm is turned off before alarming again (a new notification will force the device back into the alarm state).")%>
        <hr />
    </div>
</div>

