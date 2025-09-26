<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Minimum Dwell Time Threshold","Minimum Dwell Time Threshold")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Dwell Times below this period will not be reported.", "Dwell Times below this period will not be reported.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Detection Temperature","Detection Temperature")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The sensor can only detect the surface temperature of objects between the Minimum and Maximum Detection Temperatures. NOTE: Keep in mind that even though the core temperatur of a healthy person is around 98.6 F (37 C) skin temperature is typically less. Clothing will also affect and typically reduce the surface temperature.", "The sensor can only detect the surface temperature of objects between the Minimum and Maximum Detection Temperatures. NOTE: Keep in mind that even though the core temperatur of a healthy person is around 98.6 F (37 C) skin temperature is typically less. Clothing will also affect and typically reduce the surface temperature.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Min Warm Body Size","Min Warm Body Size")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Any warm body producing fewer than this number of detection pixels in the sensor array will be disgarded and ignored. Do not recommend using 1 pixel because a stray reading on a single pixels can cause less consistent results. The area of the pixel as it projects outward from the sensor face is approximately (distance/4) squared. Maximum Warm Body Size: Any warm body producing more than this number of detection pixels in the sensor array will cause the sensor to split the detection pixels into two or more groups less than the Maximum Warm Body Size. The area of the pixel as it projects outward from the sensor face is approximately (distance/4) squared.", "Any warm body producing fewer than this number of detection pixels in the sensor array will be disgarded and ignored. Do not recommend using 1 pixel because a stray reading on a single pixels can cause less consistent results. The area of the pixel as it projects outward from the sensor face is approximately (distance/4) squared. Maximum Warm Body Size: Any warm body producing more than this number of detection pixels in the sensor array will cause the sensor to split the detection pixels into two or more groups less than the Maximum Warm Body Size. The area of the pixel as it projects outward from the sensor face is approximately (distance/4) squared.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Reaverage Period","Reaverage Period")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Amount of time it takes to reaverage the background temparature to a particular temperature value. Also used to prohibit reaveraging if a warm body is detected. If a warm body is detected the sensor will stop averaging its backround temperature for this period of time. If the sensor still detects a warm body after this period it will reaverage the background temperature assuming this warm body is part of the background.", "Amount of time it takes to reaverage the background temparature to a particular temperature value. Also used to prohibit reaveraging if a warm body is detected. If a warm body is detected the sensor will stop averaging its backround temperature for this period of time. If the sensor still detects a warm body after this period it will reaverage the background temperature assuming this warm body is part of the background.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Differential Temperature","Differential Temperature")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The sensor only detects an object if the temperature difference between the object and the background is greater than Differential Temperature.", "The sensor only detects an object if the temperature difference between the object and the background is greater than Differential Temperature.")%> 
        <hr />
    </div>
</div>


