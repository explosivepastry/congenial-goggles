<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% if (!Model.GenerationType.Contains("2") && new Version(Model.FirmwareVersion) >= new Version("1.2.009") && Model.SensorTypeID != 4) //This checks to see if Generation is Gen2// Not supported for Gen2/Alta until further notice 6/27/2017
    {%>
<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensor is on", "Sensor is on")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The time of day the sensor is actively working. No communication will be sent while sensor is hibernating.", "The time of day the sensor is actively working. No communication will be sent while sensor is hibernating.")%>
        <hr />
    </div>
</div>
<%} %>
