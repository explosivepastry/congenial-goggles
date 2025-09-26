<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
      <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Poll Interval","Poll Interval")%>
    </div>
     <div class="word-def">
       <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Control - Poll Interval", "The frequency the device contacts the gateway to ask if control messages await action. It doesn't create a data message.")%>
        <hr />
    </div>
</div>


