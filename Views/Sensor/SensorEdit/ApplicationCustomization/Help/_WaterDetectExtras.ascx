<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enter Aware State when water is","Enter Aware State when water is")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Water Detect - Aware State","Sets if the sensor will enter the Aware State when water is Present, Absent, or when there is a State Change.")%>
        <hr />
    </div>

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Time to Re-Arm","Time to Re-Arm")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Water Detect - Time to Re-Arm","The time in seconds after a triggering event that the sensor will wait before recognizing additional triggering events.")%>
        <hr />
    </div>
</div>
