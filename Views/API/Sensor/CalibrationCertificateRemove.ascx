<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: CalibrationCertificateRemove</b><br />
     Delete a calibration certificate and remove it from sensor<br />

    <h4>Parameters</h4>
    <table>
          <tr>
            <td>CalibrationCertificateID: </td>
            <td>Long</td>
            <td></td>
        </tr>
    </table>
    
    <h4>Example</h4>

<input type="button" id="btn_TryAPI_CalibrationCertificateRemove" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_CalibrationCertificateRemove').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "CalibrationCertificateRemove",
                    "params": [
                        { "name": "CalibrationCertificateID", "type": "Long", "description": "	Unique identifier of calibration certificate.", "optional": false },
					]
				};								
				APITest(json);
			});
		});
    </script>                         
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px; white-space: pre-wrap" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
    &lt;Method&gt;CalibrationCertificateRemove&lt;/Method&gt;
    &lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
