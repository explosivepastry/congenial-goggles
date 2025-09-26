<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Overflow Count","Overflow Count")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of pulses in a heartbeat to trigger aware state.","Number of pulses in a heartbeat to trigger aware state.")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Aware State Overflow Count","Aware State Overflow Count")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of pulses in a heartbeat to continue the aware state.","Number of pulses in a heartbeat to continue the aware state.")%>
        <hr />
    </div>
</div>