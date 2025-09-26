<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Below","Below")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meter 20mA - Minimum Threshold","Assessments below this value will cause the meter to enter the Aware State")%>
        <hr />
    </div>
</div>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Above","Above")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meter 20mA - Maximum Threshold","Assessments above this value will cause the sensor to enter the Aware State.")%>
        <hr />
    </div>
</div>


<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Hysteresis.ascx", Model);%>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("View Power Options","View Power Options")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meter 20mA - View Power Options","Toggle to Show the Power Options.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Power Options","Power Options")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meter 20mA - Power Options","Set Power Option to Digital High, Digital Low, or Amplifier SP9 Active Low.")%>
        <hr />
    </div>
</div>


<%--<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Scale","Scale")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meter 20mA - Scale","Set 4 mA value, 20 mA value, and Label.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Calibration","Calibration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meter 20mA - Calibration","This meter supports modifying the current calibration.")%>
        <hr />
    </div>
</div>--%>
