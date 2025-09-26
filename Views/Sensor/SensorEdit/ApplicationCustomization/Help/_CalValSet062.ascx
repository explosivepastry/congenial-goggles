<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Minor Interval","Minor Interval")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of miliseconds that represents the sub-sample period.", "Number of miliseconds that represents the sub-sample period.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Maximum Reading","Maximum Reading")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of miliseconds that represents the sub-sample period", "Number of miliseconds that represents the sub-sample period.")%> 
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Trip point","Trip point")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of pulsed in every Minor Interval needed to qualify occupancy.", "Number of pulsed in every Minor Interval needed to qualify occupancy.")%> 
        <hr />
    </div>
</div>


