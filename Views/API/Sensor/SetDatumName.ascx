<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SetDatumName</b><br />
    Sets the datum name object.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>index:</td>
            <td>Integer</td>
            <td>unique identifier of the datum</td>
        </tr>
        <tr>
            <td>name:</td>
            <td>string</td>
            <td>New name for the datum</td>
        </tr>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/SetDatumName/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599&index=0&name=probe1" target="_blank">https://<%:Request.Url.Host %>/xml/SetDatumName/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599&index=0&name=probe1</a>--%>
     <input type="button" id="btn_TryAPI_SetDatumName" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SetDatumName').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SetDatumName",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "index", "type": "Integer", "description": "unique identifier of the datum", "optional": false },
						{ "name": "name", "type": "String", "description": "New name for the datum", "optional": false }
					]
				};								
				APITest(json);
			});
		});
	</script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto;max-width:835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SetDatumName&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
