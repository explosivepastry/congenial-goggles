<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Delay Milliseconds","Delay Milliseconds")%>
    </div>
     <div class="word-def" >
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|The Amount of Time delayed default 0.","The Amount of Time delayed default 0.")%>
        <hr />
    </div>
</div>
