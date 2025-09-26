<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Concentration Threshold PPM","Concentration Threshold PPM")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO Meter - Concentration Threshold", "Any assesments above this value will cause the device to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Concentration Buffer PPM","Concentration Buffer PPM")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO Meter - Concentration Buffer PPM", "After exceeding a threshold and entering the Aware State, the Normal State will not resume until after the measurement value is an Aware State Buffer value awawy from either threshold.")%> 
    </div>
         <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO Meter - Concentration Buffer PPM", "For example, if the Concentration Threshold is set to 900 PPM and the buffer is 100 PPM, once the sensor assesses 900 PPM or greater, it will remain in an Aware State until dropping to 890 PPM.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time Weighted Average Threshold","Time Weighted Average Threshold")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO Meter - Time Weighted Average Threshold", "Any assesments above this value will cause the device to enter the Aware State.")%> 
         <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scale","Scale")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO Meter - Scale", "Change the unit of measurment from Fahrenheit to Celsius or vice versa.")%> 
         <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Calibration","Calibration")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO Meter - Calibration", "Modify the temperature calibration")%> 
         <hr />
    </div>
</div>







