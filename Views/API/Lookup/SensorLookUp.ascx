<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorLookUp</b><br />
    Returns the Sensor meta data.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>CheckDigit:</td>
            <td>String</td>
            <td>Check digit to prevent unauthorized access to sensors</td>
        </tr>
    </table>

    <h4>Example</h4>
   <%-- <a href="/xml/SensorLookUp?sensorID=1234&CheckDigit=XXXXXX" target="_blank">https://<%:Request.Url.Host %>/xml/SensorLookUp?sensorID=1234&amp;CheckDigit=XXXXXX</a>--%>
    <input type="button" id="btn_TryAPI_SensorLookUp" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorLookUp').click(function () {
				var json =
				{
					"auth": false,					
					"api": "SensorLookUp",
					"params": [
						{ "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
						{ "name": "CheckDigit", "type": "String", "description": "Check digit to prevent unauthorized access to sensors", "optional": false }
					]
				};								
				APITest(json);
			});
		});
	</script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorLookUp&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorMetaData ApplicationID="2" SensorID="1234" FirmwareVersion="2.5.4.3" PowerSourceID="1" RadioBand="900 MHz" Calibration1="0" Calibration2="100000" Calibration3="560000" Calibration4="10000" GenerationType="Gen1" /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
