<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|H2S Instantaneous Threshold","H2S Instantaneous Threshold")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|H2S Gas - Instantaneous Threshold", "Instantaneous H2S values above this level will cause the device to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|H2S Time Weighted Average Threshold","H2S Time Weighted Average Threshold")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|H2S Meter - Time Weighted Avg Threshold", "Time Weighted Average H2S values above this level will cause the device to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|H2S Time Weighted Average Buffer","H2S Time Weighted Average Buffer")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|H2S Meter - Time Weighted Average Buffer", "Afer exceeding a threshold and entering the Aware State, the Normal State will not resume until after the measurement value is an Aware State Buffer value away from either threshold.")%> 
    </div>
         <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|H2S Meter - Time Weighted Average Buffer", "For example, if the H2S Time Weighted Average Threshold is set to 50 PPM and the buffer is 1 PPM, then once the sensor assesses 50 PPM, it will remain in an Aware State until dropping to 45c PPM.")%> 
        <hr />
    </div>
</div>

