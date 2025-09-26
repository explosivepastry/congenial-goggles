<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Gateway>" %>

<div class="row sensorEditForm">
	<div class="col-12 col-md-3">
		<%: Html.TranslateTag("Gateway/_GatewayName|Gateway Name","Gateway Name")%>
	</div>
	<div class="col sensorEditFormInput">
		<input class="form-control" type="text" id="Name" name="Name" value="<%= Model.Name%>" />
		<%: Html.ValidationMessageFor(model => model.Name)%>
	</div>
</div>

