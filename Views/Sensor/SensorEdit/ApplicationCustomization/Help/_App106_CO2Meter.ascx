<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Instantaneous Threshold","CO2 Instantaneous Threshold")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Meter - Instantaneous Threshold", "Instantaneous CO2 values above this level will cause the device to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Instantaneous Buffer","CO2 Instantaneous Buffer")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Meter - Instantaneous Buffer", "A buffer value between measurements corresponding to a device in an Aware State and those corresponding to a device in Normal State. This stops the device from cycling between states when measuring near the maximum or the minimum limits.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Time Weighted Average Threshold PPM","CO2 Time Weighted Average Threshold PPM")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Meter - Time Weighted Avg Threshold", "Time Weighted Average CO2 values above this level will cause the device to enter the Aware State.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Time Weighted Average Buffer PPM","CO2 Time Weighted Average Buffer PPM")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Meter - Time Weighted Average Buffer", "Afer exceeding a threshold and entering the Aware State, the Normal State will not resume until after the measurement value is an Aware State Buffer value away from either threshold.")%> 
    </div>
         <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Meter - Time Weighted Average Buffer", "For example, if the CO2 Time Weighted Average Threshold is set to 1000 PPM and the buffer is 50 PPM, then once the sensor assesses 1000 PPM or greater, it will remain in an Aware State until dropping to 950 PPM.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Meter - Measurement Interval", "How often the device measures.")%> 
        <hr />
    </div>
</div>

<%--<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enable Auto Calibration","Enable Auto Calibration")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Meter - Auto Calibration", "Instigates autocalibration.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Auto Calibration Interval Days","Auto Calibration Interval Days")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Meter - Calibration Interval Days", "When the sensor tracks the minimum instantaneous CO2 value. At the end of this interval, the sensor calulates the CO2 offset needed to make this minimum value equal to 400 PPM. Once set, this CO2 offset is applied to all instantaneous CO2 readings. The CO2 offset is saved to non-volatile memory and persists if the sensor is power cycled. The default value is three days.")%> 
        <hr />
    </div>
</div>--%>


