<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Aware","Aware")%>
    </div>
     <div class="word-def">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Determines which reading of the sensor will trigger the Aware State.","Determines which reading of the sensor will trigger the Aware State.")%> 
        <hr />
    </div>
</div>


