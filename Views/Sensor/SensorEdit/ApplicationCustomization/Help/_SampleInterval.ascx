<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sample Interval","Sample Interval")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|How often the sensor checks for a vehicle. If set to less than 25 ms the sample interval become Assessments per Heartbeat.", "How often the sensor checks for a vehicle. If set to less than 25 ms the sample interval become Assessments per Heartbeat.")%> 
        <hr />
    </div>
</div>


