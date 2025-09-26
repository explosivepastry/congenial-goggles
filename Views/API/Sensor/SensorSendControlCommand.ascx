<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorSendControlCommand</b><br />
    Sends a Control message<br />
    
    <h4>Parameters</h4>
    <table>
         <tr>
            <td>SensorID:</td>
            <td>Integer</td>
            <td>Identifier of sensor to send command to</td>
        </tr>
       <tr>
            <td>RelayIndex:</td>
            <td>Integer</td>
            <td>Identifier of which relay to use by index.(Relay1 = 0,Relay2 = 1)</td>
        </tr>
        <tr>
            <td>State:</td>
            <td>Integer</td>
            <td>Identifier of what the relay should do (toggle = 3, on = 2, off = 1)</td>
        </tr>
        <tr>
            <td>Seconds:</td>
            <td>Integer</td>
            <td>Identifier of how long after receiving the command the command should initialize</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorSendControlCommand/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=2&relayIndex=0&state=3&seconds=0" target="_blank">https://<%:Request.Url.Host %>/xml/SensorSendControlCommand/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=2&amp;relayIndex=0&amp;state=3&amp;seconds=0</a>--%>
        <input type="button" id="btn_TryAPI_SensorSendControlCommand" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorSendControlCommand').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorSendControlCommand",
					"params": [                       
						{ "name": "SensorID", "type": "Integer", "description": "Identifier of sensor to send command to", "optional": false },
						{ "name": "RelayIndex", "type": "Integer", "description": "Identifier of which relay to use by index.(Relay1 = 0,Relay2 = 1)", "optional": false },
						{ "name": "State", "type": "Integer", "description": "Identifier of what the relay should do (toggle = 3, on = 2, off = 1)", "optional": false },
						{ "name": "Seconds", "type": "Integer", "description": "Identifier of how long after receiving the command the command should initialize", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;SensorSendControlCommand&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Control Message Successfull!! Control request will process when the sensor checks in.&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>