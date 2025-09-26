<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: CalibrationFacilityList</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Lookup to Find CalibrationFacilityID<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>None</td>
            <td>&emsp;</td>
            <td>&emsp;</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/AssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=2&sensorID=101&checkDigit=AAAAAA" target="_blank">https://<%:Request.Url.Host %>/xml/AssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=2&amp;sensorID=101&amp;checkDigit=AAAAAA</a>--%>
     <input type="button" id="btn_TryAPI_CalibrationFacilityList" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
            $('#btn_TryAPI_CalibrationFacilityList').click(function () {
				var json =
				{
					"auth": false,					
                    "api": "CalibrationFacilityList",
                    "params": [
                        /*{ "name": "SensorID", "type": "Integer", "description": "Identifier of sensor on your account", "optional": false },*/
                        ]
				};								
				APITest(json);
			});
		});
    </script>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px; white-space: pre-wrap" >
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;SensorRestAPI xmlns:xsi=&quot;http://www.w3.org/2001/XMLSchema-instance&quot; xmlns:xsd=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
&nbsp;&nbsp;&lt;Method&gt;CalibrationFacilityList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:collection&quot;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APICalibrationFacilityList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APICalibrationFacility CalibrationFacilityID=&quot;1&quot; Name=&quot;CalLabCo&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APICalibrationFacility CalibrationFacilityID=&quot;2&quot; Name=&quot;KWJ Engineering&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APICalibrationFacility CalibrationFacilityID=&quot;3&quot; Name=&quot;Laboratory Testing&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APICalibrationFacility CalibrationFacilityID=&quot;4&quot; Name=&quot;Custom Facility&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APICalibrationFacilityList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>