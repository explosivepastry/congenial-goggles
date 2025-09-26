<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Gateway>" %>

<div class="row sensorEditForm httpSettings">
	<div class="col-12 col-md-3">
		<%: Html.TranslateTag("Auto Disable ","Auto Disable ")%>(<%: Html.TranslateTag("Minutes","Minutes")%>)
	</div>
	<div class="col sensorEditFormInput" >
		<select class="form-select" name="HTTPServiceTimeout" id="HTTPServiceTimeout">
			<option value="1" <%= Model.HTTPServiceTimeout < 5 ? "selected=''" : ""%>><%: Html.TranslateTag("Short (1 min)","Short (1 min)")%></option>
			<option value="5" <%= Model.HTTPServiceTimeout >= 5 && Model.HTTPServiceTimeout < 30 ? "selected=''" : ""%>><%: Html.TranslateTag("Default (5 min)","Default (5 min)")%></option>
			<option value="30" <%= Model.HTTPServiceTimeout >= 30 && Model.HTTPServiceTimeout < 1092 ? "selected=''" : ""%>><%: Html.TranslateTag("Long (30 min)","Long (30 min)")%></option>
			<option value="1092.25" <%= Model.HTTPServiceTimeout >= 1092 ? "selected=''" : ""%>><%: Html.TranslateTag("Always Available","Always Available")%></option>
		</select>
		<%: Html.ValidationMessageFor(model => model.HTTPServiceTimeout)%>
	</div>
</div>