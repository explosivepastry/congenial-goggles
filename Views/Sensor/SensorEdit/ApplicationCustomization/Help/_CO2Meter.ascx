<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Instantaneous Threshold","CO2 Instantaneous Threshold")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default value: 10000 ppm.  Instantaneous CO2 values above this level will cause the sensor to become aware.vvvv", "Default value: 10000 ppm.  Instantaneous CO2 values above this level will cause the sensor to become aware.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Instantaneous Buffer","CO2 Instantaneous Buffer")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when assessments are very close to a threshold. The Buffer can be a maximum of 50% of the Threshold.", "A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when assessments are very close to a threshold. The Buffer can be a maximum of 50% of the Threshold.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Time Weighted Average Threshold","CO2 Time Weighted Average Threshold")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default value: 10000 ppm.  TWA CO2 values above this level will cause the sensor to become aware.", "Default value: 10000 ppm.  TWA CO2 values above this level will cause the sensor to become aware.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CO2 Time Weighted Average Buffer","CO2 Time Weighted Average Buffer")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when assessments are very close to a threshold. The Buffer can be a maximum of 50% of the Threshold.", "A buffer to prevent the sensor from bouncing between Standard Operation and Aware State when assessments are very close to a threshold. The Buffer can be a maximum of 50% of the Threshold.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Measurement Interval","Measurement Interval")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default value: 1 minute.  How often the sensor makes a measurment.", "Default value: 1 minute.  How often the sensor makes a measurment.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enable Auto Calibration","Enable Auto Calibration")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This setting enables autocalibration.", "This setting enables autocalibration.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Auto Calibration Interval","Auto Calibration Interval")%>
    </div>
     <div class="word-def" >
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default value: 3 days.  A span of time over which the sensor tracks the minimum instantaneous CO2 value. At the end of this interval the sensor calculates the CO2 offset needed  to make this minimum value equal to 400 ppm. Once set  this CO2 offset is applied to all instantaneous CO2 readings. The CO2 offset is save to nonvolatile memory and persists if the sensor is power cycled.", "Default value: 3 days.  A span of time over which the sensor tracks the minimum instantaneous CO2 value. At the end of this interval the sensor calculates the CO2 offset needed  to make this minimum value equal to 400 ppm. Once set  this CO2 offset is applied to all instantaneous CO2 readings. The CO2 offset is save to nonvolatile memory and persists if the sensor is power cycled.")%> 
        <hr />
    </div>
</div>


