<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Display Mode","Display Mode")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sets the display mode of the data. Ability to set thresholds available only for the displayed values", "Sets the display mode of the data. Ability to set thresholds available only for the displayed values")%> 
        <hr />
    </div>
</div>


