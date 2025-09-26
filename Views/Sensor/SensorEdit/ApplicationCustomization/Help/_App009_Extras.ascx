<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enter Aware State when magnet is","Enter Aware State when magnet is")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Open/Closed - Aware State","Sets the device to detect when the magnet is removed, introduced, or enters a state change.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time to Re-Arm","Time to Re-Arm")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Open/Closed - Time to Re-Arm","The time in seconds after a triggering event that the sensor will wait before recognizing additional triggering events.")%>
        <hr />
    </div>
</div>
