<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Buffer","Aware State Buffer")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Prevents the meter from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.","Prevents the meter from bouncing between Standard Operation and Aware State when the assessments are very close to a threshold.")%>
        <br />
        <br />
        <b><%:Html.TranslateTag("Sensor/ApplicationCustomization/Help|For Example:", "For Example:")%></b>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|If the Above Threshold is set to 90 mV/V and the buffer is set to 1 mV/V, then once the meter reads 90 mV/V and enters the Aware State it will remain in the Aware State until the readings drop to 89 mV/V. Similarly, at the Below Threshold, the reading will have to rise 1 mV/V above the threshold to return to Standard Operation.","If the Above Threshold is set to 90 mV/V and the buffer is set to 1 mV/V, then once the meter reads 90 mV/V and enters the Aware State it will remain in the Aware State until the readings drop to 89 mV/V. Similarly, at the Below Threshold, the reading will have to rise 1 mV/V above the threshold to return to Standard Operation.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Gain","Gain")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Gain controls the range that can be measured.  As the gain increases the range of the measurement decreases.", "Gain controls the range that can be measured.  As the gain increases the range of the measurement decreases.")%>
        <br />
        <br />
        <b><%:Html.TranslateTag("Sensor/ApplicationCustomization/Help|For Example:", "For Example:")%></b>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|When the gain is 1x, the meter can measure -1000mV/V to 1000mV/V. When the gain is set to 2x, the meter can only measure up to -500mV/V to 500mV/V. If it takes a measurement above or below the measurement range, the meter will report an out of range error.", "When the gain is 1x, the meter can measure -1000mV/V to 1000mV/V. When the gain is set to 2x, the meter can only measure up to -500mV/V to 500mV/V. If it takes a measurement above or below the measurement range, the meter will report an out of range error.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Noise Rejection","Noise Rejection")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enables and disables the power noise rejection filters.","Enables and disables the power noise rejection filters.")%>
        <hr />
    </div>
</div>
