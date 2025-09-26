<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Communication Mode","Communication Mode")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Button Press - Commmunication Mode","The time allowed for the sensor to communicate with the gateway before another button press can send data. The time allowed depends upon the sensor's use case. Selecting Site Survey sets the delay to three seconds. The Service Button sets the delay to 20 seconds. The LED will flash red when the sensor doesn't communicate with the gateway and green when it does. Data is only sent every 20 seconds when a button is pressed- no matter how many times the button is pressed during that 20 seconds.")%>
        <hr />
    </div>
</div>


