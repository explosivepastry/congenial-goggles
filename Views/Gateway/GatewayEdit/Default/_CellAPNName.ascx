<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_CellAPNName|Cellular APN Name","Cellular APN Name")%>
    </div>
    <div class="col sensorEditFormInput">
        <%: Html.TextBoxFor(model => model.CellAPNName)%>
        <%: Html.ValidationMessageFor(model => model.CellAPNName)%>
    </div>
</div>

