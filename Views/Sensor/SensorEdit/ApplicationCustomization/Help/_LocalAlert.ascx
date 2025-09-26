<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable BackLight","Enable BackLight")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Controls the backlight when only the status bar is displayed.","Controls the backlight when only the status bar is displayed.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable LED Alarm","Enable LED Alarm")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Turns the LED alarm On and Off","Turns the LED alarm On and Off")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable Buzzer Alarm","Enable Buzzer Alarm")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Turns the Buzzer Alarm On and Off","Turns the Buzzer Alarm On and Off")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable Scrolling Display Alarm","Enable Scrolling Display Alarm")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Controls whether a notification message is displayed while alarming.","Controls whether a notification message is displayed while alarming.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Message Scroll Speed","Message Scroll Speed")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|How long to display a single notification, while alarming, before scrolling to the next notification.",
       "How long to display a single notification, while alarming, before scrolling to the next notification.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Timezone Offset","Timezone Offset")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|UTC Timezone offset in minutes.","UTC Timezone offset in minutes.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|LCD Contrast","LCD Contrast")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|LCD Contrast Settings: Lower is lighter, Higher is darker.","LCD Contrast Settings: Lower is lighter, Higher is darker.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Volume Control","Volume Control")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Controls the volume of the alarm buzzer.","Controls the volume of the alarm buzzer.")%>
        <hr />
    </div>
</div>
