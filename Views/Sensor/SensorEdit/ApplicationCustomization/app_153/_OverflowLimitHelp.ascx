<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Overflow Limit (seconds)","Overflow Limit (seconds)")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|If the aware flag was set on the previous reported data point, the sensor will wait this amount of time before assessing the Overflow Count again.","If the aware flag was set on the previous reported data point, the sensor will wait this amount of time before assessing the Overflow Count again.")%>
        <hr />
    </div>
</div>