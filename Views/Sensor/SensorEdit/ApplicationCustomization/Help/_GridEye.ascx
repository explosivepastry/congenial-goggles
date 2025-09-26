<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Max Occupancy Threshold","Max Occupancy Threshold")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Occupancy counts above this value cause the sensor to become aware. If the occupancy count is below this value at the end of a heartbeat the sensor will become non-aware.", "Occupancy counts above this value cause the sensor to become aware. If the occupancy count is below this value at the end of a heartbeat the sensor will become non-aware.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Height","Height")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Height corresponds to the height above the ground at which the Sensor is deployed. The heights overlap to allow adjustment for best device behavior. ")%> 
        <hr />
    </div>
</div>



