<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: CalibrationCertificateList</b><br />
    List all callibration certificates assigned to a given sensor<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>SensorID:</td>
            <td>Integer</td>
            <td>Identifier of sensor</td>
        </tr>
        <tr>
            <td>ShowDeleted:</td>
            <td>Bool</td>
            <td>Include deleted certificates</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/AssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=2&sensorID=101&checkDigit=AAAAAA" target="_blank">https://<%:Request.Url.Host %>/xml/AssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=2&amp;sensorID=101&amp;checkDigit=AAAAAA</a>--%>
     <input type="button" id="btn_TryAPI_CalibrationCertificateList" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
            $('#btn_TryAPI_CalibrationCertificateList').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "CalibrationCertificateList",
                    "params": [
						{ "name": "SensorID", "type": "Integer", "description": "Identifier of sensor on your account", "optional": false },
                        { "name": "ShowDeleted", "type": "bool", "description": "Hide certificates previously assigned to sensor", "optional": true },]
				};								
				APITest(json);
			});
		});
    </script>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px; white-space: pre-wrap" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CalibrationCertificateList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APICalibrationCertificateList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APICalibrationCertificate CalibrationCertificateID=&quot;1234&quot; CreatedByUserID=&quot;1234&quot; SensorID=&quot;1234&quot; DateCreated=&quot;9/22/2022 8:22:50 PM&quot; DateCertified=&quot;3/7/2021 12:00:00 AM&quot; CertificationValidUntil=&quot;10/31/2022 12:00:00 AM&quot; CalibrationNumber=&quot;No Certification ID Provided&quot; CalibrationFacilityID=&quot;1&quot; CertificationType=&quot;1234&quot; DeletedByUserID=&quot;-9223372036854775808&quot; DeletedDate=&quot;1/1/0001 12:00:00 AM&quot; /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APICalibrationCertificateList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>