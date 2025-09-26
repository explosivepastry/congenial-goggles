<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Additional97|AdditionalTitle","Additional Features and Options")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Additional97|AdditionalDescription","Further settings,such as Calibration and Scale, may appear on their own page.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Additional97|ScaleTitle","Scale")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Additional97|ScaleDescription","Show temperature values in Fahrenheit or Celsius.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Additional97|CalibrationTitle","Calibration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Additional97|CalibrationDescription","Calibrate Fan Override, Occupancy Override, Unoccupancy Override, Temperature Offset, and Humidity Offset.")%>
        <hr />
    </div>
</div>