<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<div class="row sensorEditForm" id="sensName">
	<div class="col-12 col-md-3 " >
		<%: Html.TranslateTag("Sensor Name","Sensor Name")%>
	</div>
	<div class="col sensorEditFormInput" >
		<input class="form-control user-dets" type="text" <%=Model.CanUpdate ? "" : "disabled" %>  id="SensorName" name="SensorName" value="<%= Model.SensorName%>"/>
		<%: Html.ValidationMessageFor(model => model.SensorName)%>
	</div>
</div>

