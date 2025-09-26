<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EquipmentTypeSensor>" %>

<script src="<%: Url.Content("~/Scripts/jquery-1.8.2.min.js") %>"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>"></script>

<form action="/Equipment/AddEquipmentTypeSensor" method="post">
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>EquipmentTypeSensor</legend>

		<input type="hidden" name="EquipmentTypeID" value="<%:ViewBag.TypeID%>" />

        <div class="editor-label">
            <%: Html.LabelFor(model => model.ApplicationID) %>
        </div>
        <div class="editor-field">
            <select id="ApplicationID" name="ApplicationID">
				<%foreach (Monnit.MonnitApplication type in Monnit.MonnitApplication.LoadAll()) {%>
					<option value="<%:type.ApplicationID%>"><%:type.ApplicationName%></option>						
				<%}%>
			</select>
            <%: Html.ValidationMessageFor(model => model.ApplicationID) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.Name) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.Name) %>
            <%: Html.ValidationMessageFor(model => model.Name) %>
        </div>

        <div class="editor-label">
            <%: Html.LabelFor(model => model.IsRequired) %>
        </div>
        <div class="editor-field">
            <%: Html.EditorFor(model => model.IsRequired) %>
            <%: Html.ValidationMessageFor(model => model.IsRequired) %>
        </div>

		<input class="editor-label" type="submit" value="Add" />
    </fieldset>
</form>