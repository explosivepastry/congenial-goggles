<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Duty Threshold","Duty Threshold")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Values above this threshold will contribute to the average current/power. Ex: If the value is set to 10 amps or Watts and the current is below 10 amps or watts the entire heartbeat the average current or power will indicated 0. If above this.","Values above this threshold will contribute to the average current/power. Ex: If the value is set to 10 amps or Watts and the current is below 10 amps or watts the entire heartbeat the average current or power will indicated 0. If above this.")%>
        <hr />
    </div>
</div>
