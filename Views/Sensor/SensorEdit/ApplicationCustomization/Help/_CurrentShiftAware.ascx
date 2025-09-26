<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Shift Aware","Current Shift Aware")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This value determines how much of a change in current is required for the sensor to go aware. For example, suppose the value in this field is 1 amp. If the transducer is measuring 3 amps then drops to 2 amps, the sensor will go aware and report.","This value determines how much of a change in current is required for the sensor to go aware. For example, suppose the value in this field is 1 amp. If the transducer is measuring 3 amps then drops to 2 amps, the sensor will go aware and report.")%> 
        <hr />
    </div>
</div>


