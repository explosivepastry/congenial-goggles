<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorEquipmentGet</b><br />
    Returns the sensor object.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorEquipmentGet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599" target="_blank">https://<%:Request.Url.Host %>/xml/SensorEquipmentGet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599</a>--%>
     <input type="button" id="btn_TryAPI_SensorEquipmentGet" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorEquipmentGet').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorEquipmentGet",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
	</script>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorEquipmentGet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&lt;APISensorEquipment SensorID="xxx" Make="" Model="" SerialNumber="" SensorLocation="Meat Refrigerator" SensorDescription="Cold Temperature" Note="Notifications set at 32 degrees"&gt;
&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>