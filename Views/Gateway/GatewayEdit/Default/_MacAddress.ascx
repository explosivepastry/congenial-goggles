<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Gateway/_MacAddress|MAC Address","MAC Address")%>
    </div>
    <div class="col sensorEditFormInput" style="font-size:1.1em;">
        <%if (!string.IsNullOrEmpty(Model.MacAddress))
          { %>
        <%: Model.MacAddress.Length == 12 ? Model.MacAddress.Insert(10, ":").Insert(8, ":").Insert(6, ":").Insert(4, ":").Insert(2, ":") : Model.MacAddress%>
        <%} %>
    </div>
</div>

