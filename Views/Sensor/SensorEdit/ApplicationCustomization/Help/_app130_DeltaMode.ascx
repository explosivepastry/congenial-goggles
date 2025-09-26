<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Aware Mode","Aware Mode")%>
    </div>
    <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Tilt Detection - Aware Mode","The detected activity that causes the sensor to enter its Aware State: On Change, Not Down, Not Up, Stuck, or None.")%>
        <hr />
    </div>
</div>


