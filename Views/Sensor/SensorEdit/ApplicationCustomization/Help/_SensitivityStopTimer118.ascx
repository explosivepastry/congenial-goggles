<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity","Sensitivity")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This value determines the level of sensitivity to trigger the sensor into the moving state.","This value determines the level of sensitivity to trigger the sensor into the moving state.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Stop Timer","Stop Timer")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Off when 0, any other value will (seconds) cause the sensor to report when It determines it is no longer moving after the give value.","Off when 0, any other value will (seconds) cause the sensor to report when It determines it is no longer moving after the give value.")%>
        <hr />
    </div>
</div>