<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Minor Interval","Minor Interval")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of miliseconds that represents the sub-sample period. Range is 20 to 30000", "Number of miliseconds that represents the sub-sample period. Range is 20 to 30000")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Maximum Reading","Maximum Reading")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of miliseconds that represents the sub-sample period. Range is 100 to 250", "Number of miliseconds that represents the sub-sample period. Range is 100 to 250")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity","Sensitivity")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of pulsed in every Minor Interval needed to qualify occupancy. Range 1 to 10", "Number of pulsed in every Minor Interval needed to qualify occupancy. Range 1 to 10")%> 
        <hr />
    </div>
</div>


