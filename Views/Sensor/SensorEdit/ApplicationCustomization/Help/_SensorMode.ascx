<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensor Mode","Sensor Mode")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|If AC RMS is selected, you can calibrate. Otherwise no calibration is available for the other options.","If AC RMS is selected, you can calibrate. Otherwise no calibration is available for the other options.")%>
        <hr />
    </div>
</div>


