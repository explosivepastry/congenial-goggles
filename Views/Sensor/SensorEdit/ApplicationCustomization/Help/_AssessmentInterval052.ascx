<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Assessment Interval","Assessment Interval")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|How often the sensor checks to see if the there is air flow.","How often the sensor checks to see if the there is air flow.")%> 
        <hr />
    </div>
</div>


