<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Signal Test Duration","Signal Test Duration")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Signal Test Duration","Determines the duration of a signal test in seconds from the time you press the Signal Test button. This also controls the number of intermediate results averaged throughout the test.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Signal Reliablity Level","Signal Reliablity Level")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Signal Reliablity Level","Select the PASS/POOR Threshold.")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Signal Reliablity Level","Mission-Critical: Optimizes deliverability over range.")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Signal Reliablity Level","Strong: Balances deliverability and range.")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Signal Reliablity Level","Functional: Optimizes range over deliverability.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Auto Shutoff Time","Auto Shutoff Time")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Auto Shutoff Time","Value in minutes that determines how long the device will stay on after the last button press or signal test completion. After this time, the device will shut down to save battery life.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Show Advanced Dat","Show Advanced Data")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Show Advanced Data","Toggle to show more data. If set to ON, additional advanced data will be displayed:")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Show Advanced Data","Average Signal Margin (in decibels)")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Site Survey - Show Advanced Data","Signal Quality presents the number of packets sent and received.")%>
        <hr />
    </div>
</div>
