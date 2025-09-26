<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Failed transmissions before link mode","Failed transmissions before link mode")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The number of transmissions the sensor sends without response from a gateway before it goes to battery saving link mode.  In link mode, the sensor will scan for a new gateway and if not found will enter battery saving sleep mode.","The number of transmissions the sensor sends without response from a gateway before it goes to battery saving link mode. In link mode, the sensor will scan for a new gateway and if not found it will enter battery saving sleep.")%>  <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help| Lower numbers will allow sensors to find new gateways with fewer missed readings.  Higher numbers will enable the sensor to remain with its current gatweay in a noisy RF environment better.  (Zero will cause the sensor to never join another gatweay, to find a new gateway the battery will have to be cycled out of the sensor.)")%>
        <hr />
    </div>
</div>