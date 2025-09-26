<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Unoccupied97|UnoccupiedTitle","Unoccupied Settings")%>
    </div>
     <div class="word-def">
          <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Unoccupied97|UnoccupiedDescription","Sets when to turn on and off Heating and Cooling when the building is unoccupied.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Unoccupied97|DeadpointTitle","Temperature Deadpoint")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Unoccupied97|DeadpointDescription","Sets a buffer for when the heating and cooling systems will turn off once they are on. Example: If the temperature buffer is 5°F, and the heat turns on at 65°F, the heating system will turn off at 70°F.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Unoccupied97|HeatOnTitle","Turn Heat on at")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Unoccupied97|HeatOnDescription","Temperature readings below this value will turn on the heating system.")%>
        <hr />
    </div>
</div>

<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Unoccupied97|CoolOnTitle","Turn Cool on at")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_097/Help/_Unoccupied97|CoolOnDescription","Temperature readings above this value will turn on the cooling system.")%>
        <hr />
    </div>
</div>




