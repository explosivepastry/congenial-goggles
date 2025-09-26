<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorGroupAssignSensor</b><br />
    Assigns a sensor to a sensor group, or edits its position<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>GroupID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor group</td>
        </tr>
        <tr>
            <td>position:</td>
            <td>Integer (optional)</td>
            <td>Position of the sensor in the sensor group</td>
        </tr>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/SensorGroupAssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599&groupID=10" target="_blank">https://<%:Request.Url.Host %>/xml/SensorGroupAssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599&amp;groupID=10</a>--%>
     <input type="button" id="btn_TryAPI_SensorGroupAssignSensor" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorGroupAssignSensor').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorGroupAssignSensor",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "GroupID", "type": "Integer", "description": "Unique identifier of the sensor group", "optional": false },
						{ "name": "position", "type": "Integer", "description": "Position of the sensor in the sensor group", "optional": true }
					]
				};								
				APITest(json);
			});
		});
	</script>

    <h4>Example Output</h4>
   <%if (MonnitSession.CurrentTheme.Theme == "Default")
      {  %>
    <span style="color: red;">Deprecated: MonnitApplicationID. Use ApplicationID</span>
    <%} %>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorGroupSensorList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroupSensorList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroupSensor  SensorID="259" SensorGroupID="10" Position="0" ApplicationID="59" CSNetID="-9223372036854775808" SensorName="Sensor Name one" LastCommunicationDate="1/1/2099 12:00:00 AM" NextCommunicationDate="1/1/2099 2:00:00 AM" LastDataMessageMessageGUID="00000000-0000-0000-0000-000000000000" PowerSourceID="1" Status="4" CanUpdate="True" BatteryLevel="0" SignalStrength="0" AlertsActive="False" CheckDigit="XXXX" AccountID="4" MonnitApplicationID="59" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroupSensor SensorID="411" SensorGroupID="10" Position="0" ApplicationID="11" CSNetID="1269" SensorName="Sensor Name two " LastCommunicationDate="9/14/2016 4:22:09 PM" NextCommunicationDate="1/1/0001 12:00:00 AM" LastDataMessageMessageGUID="edf2f9c7-43b6-4bb6-a915-880c2902c4b3" PowerSourceID="1" Status="1" CanUpdate="True" BatteryLevel="0" SignalStrength="0" AlertsActive="True" CheckDigit="XXXX" AccountID="1" MonnitApplicationID="11" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroupSensor SensorID="246" SensorGroupID="10" Position="0" ApplicationID="46" CSNetID="-9223372036854775808" SensorName="Sensor Name three" LastCommunicationDate="1/1/2099 12:00:00 AM" NextCommunicationDate="1/1/2099 2:00:00 AM" LastDataMessageMessageGUID="00000000-0000-0000-0000-000000000000" PowerSourceID="1" Status="4" CanUpdate="True" BatteryLevel="0" SignalStrength="0" AlertsActive="False" CheckDigit="XXXX" AccountID="4" MonnitApplicationID="46" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APISensorGroupSensorList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
