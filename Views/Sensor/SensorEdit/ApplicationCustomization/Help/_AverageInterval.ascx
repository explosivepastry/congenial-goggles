<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Averaging Interval","Averaging Interval")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|User defined value that determines how often the sensor will make an assessment. if you enter in 5 then the sensor will make a measurement every 5 seconds and on the Heartbeat it will deliver an average of all the measurements over that interval.",
        "User defined value that determines how often the sensor will make an assessment. if you enter in 5 then the sensor will make a measurement every 5 seconds and on the Heartbeat it will deliver an average of all the measurements over that interval.")%>
        <hr />
    </div>
</div>


