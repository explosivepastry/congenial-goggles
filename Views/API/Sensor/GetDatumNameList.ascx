<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GetDatumNameList</b><br />
    Returns the list of DatumNames that belongs to a sensor.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/GetDatumNameList/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599" target="_blank">https://<%:Request.Url.Host %>/xml/GetDatumNameList/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599</a>--%>
     <input type="button" id="btn_TryAPI_GetDatumNameList" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GetDatumNameList').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GetDatumNameList",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;GetDatumNameList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDatumNameList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDatumName SensorID="002599" Index="0" Name="Probe1" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDatumName SensorID="002599" Index="1" Name="Probe2" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDatumName SensorID="002599" Index="2" Name="Probe3" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDatumName SensorID="002599" Index="3" Name="Probe4" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDatumNameList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
