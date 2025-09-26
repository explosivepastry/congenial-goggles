<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time to Re-Arm (seconds)","Time to Re-Arm (seconds)")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The time in seconds after a triggering event that the sensor will wait before re-arming itself.","The time in seconds after a triggering event that the sensor will wait before re-arming itself.")%>
        <hr />
    </div>
</div>


