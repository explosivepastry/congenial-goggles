<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Overflow Count","Overflow Count")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|While not in the aware state, counts above this value cause the sensor to go into the aware state and transmit immediately.","While not in the aware state, counts above this value cause the sensor to go into the aware state and transmit immediately.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Overflow Count","Aware State Overflow Count")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|While in the aware state, counts below this value transition the sensor from aware state to not aware.","While in the aware state, counts below this value transition the sensor from aware state to not aware.")%>
        <hr />
    </div>
</div>


