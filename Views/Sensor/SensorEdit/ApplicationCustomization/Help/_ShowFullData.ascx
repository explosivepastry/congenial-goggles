<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Show Full Data Value","Show Full Data Value")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default: False, only shows the avg kwh, wh, or ah.","Default: False, only shows the avg kwh, wh, or ah.")%>
        <hr />
    </div>
</div>


