<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Name","Name")%>
    </div>
     <div class="word-def" >
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|A unique name given to a device to easily identify it in many in-app views and notifications.","A unique name given to a device to easily identify it in many in-app views and notifications.")%>
        <hr />
    </div>
</div>



