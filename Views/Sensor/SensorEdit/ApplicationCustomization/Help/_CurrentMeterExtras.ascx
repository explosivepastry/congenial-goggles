<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Below","Below")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meters - Minimum Threshold","Assessments below this value will cause the meter to enter the Aware State and report data if not already in the Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Above","Above")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meters - Maximum Threshold","Assessments above this value will cause the meter to enter the Aware State and report data if not already in the Aware State.")%>
        <hr />
    </div>
</div>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_Hysteresis.ascx", Model); %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Averaging Interval","Averaging Interval")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meters - Averaging Interval","Time, in seconds, for how often the meter will assess within its Heartbeat. The measurements taken will be averaged together to calculate the Average Current reading the meter produces.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meters - Accumulate","If Off, only the Accumulated Current or Power consumed during a single Heartbeat will be reported. If On, the energy consumed during the current Heartbeat will be continually added to the previously reported measurements.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Show Full Data","Show Full Data")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Current Meters - Show Full Data","Only the Accumulated Current or Power is displayed if Off. If On, Average, Maximum, and Minimum Current measurements are also displayed.")%>
        <hr />
    </div>
</div>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Help/_SyncronizeOffset.ascx", Model);  %>
