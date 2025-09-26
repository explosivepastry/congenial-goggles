<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.EquipmentSensor>" %>

<script src="<%: Url.Content("~/Scripts/jquery-1.8.2.min.js") %>"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
<script src="<%: Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js") %>"></script>

<form action="/Equipment/AddEquipmentSensors" method="post">
    <% Html.RenderPartial("~/Views/Shared/_AntiForgeryToken.ascx"); %>
    <%: Html.ValidationSummary(true) %>

    <fieldset>
        <legend>Equipment Sensors</legend>

		<input type="hidden" name="EquipmentProfileID" value="<%:ViewBag.ProfileID %>" />

		<%--<div class="editor-field">
			<%List<EquipmentTypeSensor> sensors = EquipmentTypeSensor.LoadByEquipmentType(ViewBag.TypeID);
			foreach (EquipmentTypeSensor type in sensors) { 
				if(type.IsRequired) {%>
					<span data-id="<%:type.EquipmentTypeSensorID%>"><b>Required</b> - <%:type.Name%></span>
				<%} else { %>
					<span data-id="<%:type.EquipmentTypeSensorID%>"><%:type.Name%></span>
				<%} %>
				<select id="SensorID" name="SensorID">
					<%foreach (Sensor sensor in Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID)) { 
						if(type.ApplicationID == sensor.ApplicationID) {%>
							<option value="<%:sensor.SensorID%>"><%:sensor.SensorName%></option>
					<%}}%>
				</select>
				<br />
			<%}%>
        </div>--%>
        <div class="editor-label">
            Equipment Type Sensor
        </div>
		<div class="editor-field">
			<select id="EquipmentTypeSensorID" name="EquipmentTypeSensorID">
				<%List<EquipmentSensor> existingSensors = EquipmentSensor.LoadByEquipmentProfile(ViewBag.ProfileID);
				List<EquipmentTypeSensor> sensors = EquipmentTypeSensor.LoadByEquipmentType(ViewBag.TypeID);

				foreach (EquipmentSensor item in existingSensors) {
					if(sensors.Exists(x => x.EquipmentTypeSensorID == item.EquipmentTypeSensorID)) {
						EquipmentTypeSensor removeSens = sensors.Find(x => x.EquipmentTypeSensorID == item.EquipmentTypeSensorID);
						sensors.Remove(removeSens);
					}
				}
				
				foreach (EquipmentTypeSensor type in sensors) { %>
					<option value="<%:type.EquipmentTypeSensorID%>"><%:type.Name%></option>
				<%}%>
			</select>
            <%:Html.ValidationMessageFor(model => model.EquipmentTypeSensorID)%>
        </div>
		
        <div class="editor-label">
            Sensor
        </div>
		<div class="editor-field">
			<select id="SensorID" name="SensorID">
				<%List<Sensor> accountSensors = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
				
				foreach (EquipmentSensor item in existingSensors) {
					if(accountSensors.Exists(x => x.SensorID == item.SensorID)) {
						Sensor removeSens = accountSensors.Find(x => x.SensorID == item.SensorID);
						accountSensors.Remove(removeSens);
					}
				}
	  
				foreach (Sensor sensor in accountSensors) { %>
					<option value="<%:sensor.SensorID%>"><%:sensor.SensorName%></option>
				<%}%>
			</select>
            <%:Html.ValidationMessageFor(model => model.SensorID)%>
        </div>

		<input class="editor-label" type="submit" value="Create" />
    </fieldset>
</form>