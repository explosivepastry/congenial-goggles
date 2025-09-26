<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_UserName|Cellular Network Username","Cellular Network Username")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.Username)%>
        <%: Html.ValidationMessageFor(model => model.Username)%>
    </div>
</div>

