<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity","Sensitivity")%>
    </div>
     <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Default value 3. 1 is the most sensitive to motion, 4 is the least sensitive.",
        "Default value 3. 1 is the most sensitive to motion, 4 is the least sensitive.")%>
        <hr />
    </div>
</div>


