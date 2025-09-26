<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Poll Rate","Poll Rate")%> (Minutes)
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|How often the Device communitcates with the gateway looking for new down messages.","How often the device communitcates with the gateway looking for new down messages.")%>
        <hr />
    </div>
</div>


