<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Report Immediately","Report Immediately")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|If Aware Reading is selected, then the time between the message detecting motion and the message saying 'no motion' is (rearm time + aware state heartbeat).\r\n  If set to All State Changes, then the time between the two messages is (rearm time + rearm time).This example assumes standard Aware on Motion state.",
        "If Aware Reading is selected, then the time between the message detecting motion and the message saying 'no motion' is (rearm time + aware state heartbeat).\r\n  If set to All State Changes, then the time between the two messages is (rearm time + rearm time).This example assumes standard Aware on Motion state..")%>
        <hr />
    </div>
</div>


