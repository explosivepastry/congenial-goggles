<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Show Dew-point","Show Dew-point")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Adds the dewpoint to the data that is displayed.","Adds the dewpoint to the data that is displayed.")%>
        <hr />
    </div>
</div>


