<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Power Options","Power Options")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Allows the sensor to raise or ground the voltage.","Allows the sensor to raise or ground the voltage.")%>
        <hr />
    </div>
</div>
