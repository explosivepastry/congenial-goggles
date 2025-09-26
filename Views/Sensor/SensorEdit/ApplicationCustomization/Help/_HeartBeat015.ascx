<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Heartbeat Interval","Heartbeat Interval")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|When the G-Force Snapshot sensor wakes up and delivers its current XYZ axis readings.","When the G-Force Snapshot sensor wakes up and delivers its current XYZ axis readings.")%>
        <hr />
    </div>
</div>


