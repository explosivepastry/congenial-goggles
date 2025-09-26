<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Temperature Delta","Active Mode Temperature Delta")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|While in active measurement mode if the temperature changes by this amount compared to the last reported value the sensor will report immedidately. Note: The sensor enters and stays in active mode when voltage is detected (\"Voltage Present\"). The sensor leaves active mode when no voltage is detected.","While in active measurement mode if the temperature changes by this amount compared to the last reported value the sensor will report immedidately. Note: The sensor enters and stays in active mode when voltage is detected (\"Voltage Present\"). The sensor leaves active mode when no voltage is detected.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Pressure Delta","Active Mode Pressure Delta")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|While in active measurement mode if the pressure changes by this amount compared to the last reported value the sensor will report immedidately. Note: The sensor enters and stays in active mode when voltage is detected (\"Voltage Present\"). The sensor leaves ","While in active measurement mode if the pressure changes by this amount compared to the last reported value the sensor will report immedidately. Note: The sensor enters and stays in active mode when voltage is detected (\"Voltage Present\"). The sensor leaves ")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Current Delta","Active Mode Current Delta")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|While in active measurement mode if any of the current readings change by this amount compared to the last reported value the sensor will report immedidately. Note: The sensor enters and stays in active mode when voltage is detected (\"Voltage Present\"). The sensor leaves active mode when no voltage is detected.","While in active measurement mode if any of the current readings change by this amount compared to the last reported value the sensor will report immedidately. Note: The sensor enters and stays in active mode when voltage is detected (\"Voltage Present\"). The sensor leaves active mode when no voltage is detected.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Active Mode Report Interval","Active Mode Report Interval")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|How often the sensor sends data in the active mode, from the time of the last reported data.   Note: Delta threshold breach may cause data report prior to this interval.","How often the sensor sends data in the active mode, from the time of the last reported data.   Note: Delta threshold breach may cause data report prior to this interval.")%>
        <hr />
    </div>
</div>

