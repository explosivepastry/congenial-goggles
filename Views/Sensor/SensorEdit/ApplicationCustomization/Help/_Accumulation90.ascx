<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Accumulate","Accumulate")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default: Off","Default: Off")%>
        <br />
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Standard operation reports the number of pulses detected between heartbeats.","Standard opperation reports the number of pulses detected between heartbeats.")%>
        <br />
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Accumulate on will accumulate the readings heartbeat after heartbeat.","Accumulate on will accumulate the readings heartbeat after heartbeat.")%>
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Accumulation rolls over at 18446744073709551615","Accumulation rolls over at 18446744073709551615")%>
        <hr />
    </div>
</div>


