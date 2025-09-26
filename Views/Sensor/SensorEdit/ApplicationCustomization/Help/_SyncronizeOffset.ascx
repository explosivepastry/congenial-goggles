<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (!Model.IsWiFiSensor)
    { %>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Synchronize","Synchronize")%>
    </div>
    <div class="word-def">
        <span style="font-weight: bold"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Off (Default):","Off (Default):")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Off (Default): The next report will occur one Heartbeat after the last report time. This allows devices to self-optimize network traffic patterns and reduce congestion."," The next report will occur one Heartbeat after the last report time. This allows devices to self-optimize network traffic patterns and reduce congestion.")%>
        <br />
        <br />
        <span style="font-weight: bold"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|On:","On:")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|On: Aligns the next report with UTC (Global) time, regardless of the last report time. This allows multiple devices to generate reports in a synchronized manner. Aware State Transitions will still report on a threshold breach, but the following report will return to the next aligned report interval."," Aligns the next report with UTC (Global) time, regardless of the last report time. This allows multiple devices to generate reports in a synchronized manner. Aware State Transitions will still report on a threshold breach, but the following report will return to the next aligned report interval.")%>
        <br />
        <br />
        <span style="font-weight: bold"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Warning:","Warning:")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Warning: Too many synchronized reports will increase network congestion. We recommend that no more than 10 devices per gateway are synchronized."," Too many synchronized reports will increase network congestion. We recommend that no more than 10 devices per gateway are synchronized.")%>
        <br />
        <br />
        <span style="font-weight: bold"><%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Example:","Example:")%></span>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Example: The Heartbeat is configured to 60 minutes, and the device reports at 1:47 PM."," The Heartbeat is configured to 60 minutes, and the device reports at 1:47 PM.")%>
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|If Synchronization is Off, the next reports are expected at 2:47 PM, then at 3:47 PM.","If Synchronization is Off, the next reports are expected at 2:47 PM, then at 3:47 PM.")%>
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|If Synchronization is On, the next reports are expected at 2:00 PM, then 3:00 PM.","If Synchronization is On, the next reports are expected at 2:00 PM, then 3:00 PM.")%>

        <hr />
    </div>
</div>

<%}%>
