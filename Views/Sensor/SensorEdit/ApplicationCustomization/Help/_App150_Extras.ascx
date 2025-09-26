<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity","Sensitivity")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Temp Water - Sensitivity","Sensitivity of the PIR Sensor. Sensor will quickly and consistently detect a full sized adult(5'8\", 170 Lbs) moving across the sensor face facing perpendicular to the sensor at the given range. The sensor can detect beyond the given range but it may be inconsistent or only trigger if there is a more significant infrared change (multiple adults moving across the sensor face, single adult moving across the sensor face while facing the sensor and jumping up and down, adult with more exposed skin area, etc.). The ability of the sensor to detect an object is directly related to the ability of the sensor to see a rapid change in the background temperature. Coats, hats, gloves, or other insulative clothing will reduce the sensors ability to detect the subject.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Temperature Offset Calibration","Temperature Offset Calibration")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Temp Water - Temperature Offset Calibration","Adjusts temperature readings by the Offset Value. Example: Offset of 2 C will change a 20 C reading to a 22 C reading.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Humidity Offset Calibration","Humidity Offset Calibration")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Motion Temp Water - Humidity Offset Calibration","Adjusts the humidity readings by the Offset Value. Example: Offset of 2% will change a 20% reading to a 22% reading. Has no effect if humidity hardware option not installed.")%>
        <hr />
    </div>
</div>