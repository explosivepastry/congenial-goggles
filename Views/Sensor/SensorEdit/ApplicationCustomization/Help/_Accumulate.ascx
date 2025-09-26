<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Accumulate","Accumulate")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default: Off","Default: Off")%>
        <br />
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Standard opperation reports the number of pulses detected between heartbeats.","Standard opperation reports the number of pulses detected between heartbeats.")%>
        <hr />
    </div>

        <%if(Model.ApplicationID == 109  || Model.ApplicationID == 129 || Model.ApplicationID == 137) { %> 
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Display Data","Display Data")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Provides options for how the data will be shown.","Provides options for how the data will be shown.")%>
        <hr />
    </div>
   <% }%>

    <%if(Model.ApplicationID != 129  && Model.ApplicationID != 137 && Model.ApplicationID != 109) { %> 

    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Pulse Edge Detection","Pulse Edge Detection")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Determines whether pulses are recorded at the positive edge of the pulse or the negative edge of the pulse.","Determines whether pulses are recorded at the positive edge of the pulse or the negative edge of the pulse.")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Stabilization Delay","Stabilization Delay")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Miliseconds to wait for confirming positive reading after pulse initially detected. (Debouncing) Recommended 50 percent of minimum pulse width of input signal.","Miliseconds to wait for confirming positive reading after pulse initially detected. (Debouncing) Recommended 50 percent of minimum pulse width of input signal.")%>
        <hr />
    </div>
   <% }%>
</div>


