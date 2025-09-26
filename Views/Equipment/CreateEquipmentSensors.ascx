<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Monnit.EquipmentTypeSensor>>" %>

<%foreach (Monnit.EquipmentTypeSensor item in Model) {%>
<div style="font-size: 11pt; float: left;">
	<span><%:item.Name + ": "%></span>
	<br />

	<select class="SensorChoice" name="SensorID">
		<option value="-1" selected="selected">- -</option>
		<%List<Sensor> accountSensors = Sensor.LoadByAccountID(MonnitSession.CurrentCustomer.AccountID);
		//List<Monnit.EquipmentSensor> existingSensors = Monnit.EquipmentSensor.LoadByEquipmentTypeSensor(item.EquipmentTypeSensorID);

		// foreach (Monnit.EquipmentSensor sensor in existingSensors) {
		//	 if (accountSensors.Exists(x => x.SensorID == sensor.SensorID)) {
		//		 Sensor removeSens = accountSensors.Find(x => x.SensorID == sensor.SensorID);
		//		 accountSensors.Remove(removeSens);
		//	 }
		// }

			foreach (Sensor sensor in accountSensors) {
				if(sensor.ApplicationID == item.ApplicationID) {%>
				<option data-typeID="<%:item.EquipmentTypeSensorID%>" value="<%:sensor.SensorID%>"><%:sensor.SensorName%></option>
		<%}}%>
	</select>
</div>
<%}%>

<script type="text/javascript">
	$(function () {
		$('.SensorChoice').change(function () {
			var sensorID = $(this).val();
			var typeID = $($(this).find(':selected')[0]).attr('data-typeid');

			$.post('/Equipment/SaveEquipmentSensor/');
		});
	});
</script>