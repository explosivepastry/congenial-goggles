<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("PM2.5 Threshold","PM2.5 Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Air Quality - PM2.5 Threshold","The maximum measurement of PM2.5 particulates before triggering an Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("PM10 Threshold","PM10 Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Air Quality - PM10 Threshold","The maximum measurement of PM10 particulates before triggering an Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("PM1.0 Threshold","PM1.0 Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Air Quality - PM1.0 Threshold","The maximum measurement of PM1 particulates before triggering an Aware State.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Show Full Data Value","Show Full Data Value")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Air Quality - Show Full Data Value","Only the PM2.5 measurement is displayed if Off. If On, the PM1, PM2.5, and PM10 measurement are displayed.")%>
        <hr />
    </div>
</div>
