<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="form-group">
	<div class="bold col-md-3 col-sm-3 col-xs-12">
		<%: Html.TranslateTag("Sensor Name","Sensor Name")%>
	</div>
	<div class="col-md-9 col-sm-9 col-xs-12 lgBox">
		<input class="aSettings__input_input" type="text" <%=Model.CanUpdate ? "" : "disabled" %> id="SensorName" name="SensorName" value="<%= Model.SensorName%>" style="width: 50%;" />
		<%: Html.ValidationMessageFor(model => model.SensorName)%>
	</div>
</div>