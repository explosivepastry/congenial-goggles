<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Negative Change","Negative Change")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The percent change from the last reported resistance value that causes the sensor to go aware and report.  Entering a 0 in the % field prevents changes in the indicated direction from making the sensor go aware.  Resolution .1%.",
        "The percent change from the last reported resistance value that causes the sensor to go aware and report.  Entering a 0 in the % field prevents changes in the indicated direction from making the sensor go aware.  Resolution .1%.")%>
        <hr />
    </div>
        <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Positive Change","Positive Change")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The percent change from the last reported resistance value that causes the sensor to go aware and report.  Entering a 0 in the % field prevents changes in the indicated direction from making the sensor go aware.  Resolution .1%.",
        "The percent change from the last reported resistance value that causes the sensor to go aware and report.  Entering a 0 in the % field prevents changes in the indicated direction from making the sensor go aware.  Resolution .1%.")%>
        <hr />
    </div>
</div>


