<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Power Option","Power Option")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Select Power Option","Select Power Option")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Delay in milliseconds","Delay in milliseconds")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|The amount of time delayed (default 0)","The amount of time delayed (default 0)")%>
        <hr />
    </div>
</div>


