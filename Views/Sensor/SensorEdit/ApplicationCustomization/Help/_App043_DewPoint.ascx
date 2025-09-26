<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Show Moisture Point","Show Moisture Point")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Humidity - Show Moisture Point","Displays the Altitude field for adjustable moisture weight.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Show Dew-point","Show Dew-point")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Humidity - Show Dew point","Displays the dew point to the displayed data.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Show Heat Index","Show Heat Index")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Humidity - Show Heat Index","Displays the human-perceived equivalent temperature based on the temperature and relative humidity (RH).")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Show Wet Bulb","Show Wet Bulb")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Humidity - Show Wet Bulb","Displays the temperature as if the thermometer is covered in a wet cloth.")%>
        <hr />
    </div>
</div>

<%--<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Scale","Scale")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Humidity - Scale","This sensor supports changing the unit of measurement from Fahrenheit to Celsius or vice versa.")%>
        <hr />
    </div>
</div>--%>
