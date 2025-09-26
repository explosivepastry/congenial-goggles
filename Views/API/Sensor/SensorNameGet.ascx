<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorNameGet</b><br />
    Returns the application identifier of the sensor.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>SensorID:</td>
            <td>interger</td>
            <td>Unique identifier of the sensor</td>
        </tr>
            <tr>
            <td>CheckDigit:</td>
            <td>String (optional)</td>
            <td>Check Digit required if sensor not already on your account</td>
        </tr>
</table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorNameGet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101" target="_blank">https://<%:Request.Url.Host %>/xml/SensorNameGet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101</a>--%>
     <input type="button" id="btn_TryAPI_SensorNameGet" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorNameGet').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorNameGet",
					"params": [
                        { "name": "SensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "CheckDigit", "type": "String", "description": "Check Digit required if sensor not already on your account", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;SensorNameGet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Temperature 101&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>