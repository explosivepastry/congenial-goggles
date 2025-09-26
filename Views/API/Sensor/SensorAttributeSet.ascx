<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorAttributeSet</b><br />
    Creates/Updates sensor attribute.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>SensorID:</td>
            <td>interger</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>Name:</td>
            <td>string</td>
            <td>Name of the attribute</td>
        </tr>
        <tr>
            <td>Value:</td>
            <td>string</td>
            <td>Value for the attribute</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SensorAttributeSet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&name=CorF&value=C" target="_blank">https://<%:Request.Url.Host %>/xml/SensorAttributeSet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;name=CorF&amp;value=C</a>--%>
    <input type="button" id="btn_TryAPI_SensorAttributeSet" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorAttributeSet').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorAttributeSet",
					"params": [
                        { "name": "SensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
                        { "name": "Name", "type": "String", "description": "Name of the attribute", "optional": false },
						{ "name": "Value", "type": "String", "description": "Value for the attribute", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;SensorAttributeSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&lt;APISensorAttribute SensorID="101" Name="CorF" Value="C" /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>