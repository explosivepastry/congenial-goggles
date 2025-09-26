<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Maintain Relay Position","Maintain Relay Position")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Maintain Relay Position","Determines if the relays will maintain their current position.\r\nIf \"on\", the sensor will maintain the relays in their current position until it receives a command or factory reset.  Default position will only be applied with factory reset event.\r\nIf \"off\", the sensor will set the Relays to their default position on:")%>
        <br />
        <strong><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Maintain Relay Position","Configuration Change")%></strong>
        <br />
        <strong><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Maintain Relay Position","Signal Connection Loss")%></strong>
        <br />
        <strong><%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Maintain Relay Position","Factory Reset")%></strong>
        <hr />
    </div>
</div>
