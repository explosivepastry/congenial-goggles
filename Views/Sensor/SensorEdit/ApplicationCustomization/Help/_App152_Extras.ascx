<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware Below Threshold","Aware Below Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LN2 - Aware Below Threshold","Levels below this value will cause the sensor to go aware and report immediately.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware Above Threshold","Aware Above Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LN2 - Aware Above Threshold","Levels above this value will cause the sensor to go aware and report immediately.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tank Height","Tank Height")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LN2 - Tank Height","This height should be the internal tank height or more specifically the length of sensor that is exposed to liquid in the tank. When updated and saved, this value updates the 100% value configuration on the sensor and willa adjust readings that take place after this configuration is picked up by the sensor. Ex: Setting this value to 50cm on a 100cm sensor will set the 100% point to what was previously the 50% point of the sensor. So if your tank was full before setting this value the readings would go from 50% to 100%. NOTE: Updating this value overrides the Span Calibration value. Performing Span Calibration does not.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tank Volume","Tank Volume")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LN2 - Tank Volume","This value is used to convert the percentage produced by the sensor to a volumetric value")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Data Presentation","Data Presentation")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|LN2 - Data Presentation","If the associated box is checked that data will be displayed in history. At least one box must be checked. Custom scale is configurable in the Scale tab.")%>
        <hr />
    </div>
</div>