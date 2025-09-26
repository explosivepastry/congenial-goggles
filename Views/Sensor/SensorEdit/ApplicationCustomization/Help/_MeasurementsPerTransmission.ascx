<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Assessments per Heartbeat","Assessments per Heartbeat")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|How many times between heartbeats a sensor will check its measurements against its thresholds to determine whether it will enter the Aware State.","How many times between heartbeats a sensor will check its measurements against its thresholds to determine whether it will enter the Aware State.")%>
        <%if (new Version(Model.FirmwareVersion) < new Version("2.5.2.0"))
          { %>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The number of times between Heartbeats a sensor will compare what it is sensing against configurable thresholds to determine whether it will enter the Aware State.","The number of times between Heartbeats a sensor will compare what it is sensing against configurable thresholds to determine whether it will enter the Aware State.")%>
        <%} %>
        <hr />
    </div>
</div>
