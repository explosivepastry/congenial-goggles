<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("MAC Address","MAC Address")%>
    </div>
    <div class="col sensorEditFormInput" style="font-size:1.1em;">
        <%if (!string.IsNullOrEmpty(Model.MacAddress))
          { 
		  string MacAddress = Model.MacAddress.Split('|')[0];
		  %>
        <%: MacAddress.Length == 12 ? MacAddress.Insert(10, ":").Insert(8, ":").Insert(6, ":").Insert(4, ":").Insert(2, ":") : MacAddress%>
        <%} %>
    </div>
</div>

