<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensor Sleep Allowed","Sensor Sleep Allowed")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|At every heartbeat it will take the number of measuresents per cyle then go to sleep if sleep is allowed. Otherwise, if sleep is not allowed the sensor will measure countiniously, reporting based on Measurement Time.",
      "At every heartbeat it will take the number of measuresents per cyle then go to sleep if sleep is allowed. Otherwise, if sleep is not allowed the sensor will measure countiniously, reporting based on Measurement Time.")%> 
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Measurement Time","Measurement Time")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time inbetween reported measurements","Time inbetween reported measurements.")%> 
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Measurement Reported Per Heartbeat","Measurement Reported Per Heartbeat")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of measurements reported per heartbeat if sleep is allowed.", "Number of measurements reported per heartbeat if sleep is allowed.")%> 
        <hr />
    </div>
</div>
<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Option Mode","Option Mode")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Normal includes orientation and vibration forces, High Pass Filter includes only vibration forces or rapidly changing forces. 2G limits the readings to 2G max, 4G limits readings to 4G max, and 8G limits readings to 8G max.",
      "Normal includes orientation and vibration forces, High Pass Filter includes only vibration forces or rapidly changing forces. 2G limits the readings to 2G max, 4G limits readings to 4G max, and 8G limits readings to 8G max.")%> 
        <hr />
    </div>
</div>


