<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Minor Interval","Minor Interval")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Time in seconds representing the amount of time is tallied for either no motion or motion variables.","Time in seconds representing the amount of time is tallied for either no motion or motion variables.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Maximum Reading","Maximum Reading")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scalar multiplied with the percentage of the tallied motion time.","Scalar multiplied with the percentage of the tallied motion time.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity","Sensitivity")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Number of pulsed in every Minor Interval needed to qualify occupancy.","Number of pulsed in every Minor Interval needed to qualify occupancy.")%>
        <hr />
    </div>
</div>


