<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%if (!Model.IsWiFiSensor && new Version(Model.FirmwareVersion) < new Version("2.0.0.0"))
  {%>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Use this sensor through a repeater","Use this sensor through a repeater")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default not checked. Check this box if sensor will be used in conjuction with a repeater.","Default not checked. Check this box if sensor will be used in conjuction with a repeater.") %>
        <hr />
    </div>
</div>
<%} %>


