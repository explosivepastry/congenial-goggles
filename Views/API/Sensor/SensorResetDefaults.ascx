<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: SensorResetDefaults()</b><br />
     Removes the sensor object from the network and sets the sensor back to factory defaults.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorResetDefaults/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=001234" target="_blank">https://<%:Request.Url.Host %>/xml/SensorResetDefaults/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=001234</a>--%>
     <input type="button" id="btn_TryAPI_SensorResetDefaults" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorResetDefaults').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorResetDefaults",
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
&nbsp;&nbsp;&lt;Method&gt;SensorResetDefaults&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

