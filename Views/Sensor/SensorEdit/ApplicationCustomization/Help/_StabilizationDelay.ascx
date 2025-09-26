<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Stabilization Delay","Stabilization Delay")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The Amount of Time delayed. Default is 750 milliseconds.",
        "The Amount of Time delayed. Default is 750 milliseconds.")%>
        <hr />
    </div>
</div>


