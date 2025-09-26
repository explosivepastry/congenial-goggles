<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|CT Size","CT Size")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default value 200. The maximum current in amps this CT is designed to report.","Default value 200. The maximum current in amps this CT is designed to report.")%> 
        <hr />
    </div>
</div>


