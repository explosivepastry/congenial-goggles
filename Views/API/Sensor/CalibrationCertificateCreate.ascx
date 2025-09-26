<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: CalibrationCertificateCreate</b><br />
    Create or edit a calibration certificate.<br />

    <h4>Parameters</h4>
    <table>
        <%--          <tr>
            <td>CalibrationCertificateID: </td>
            <td>Long</td>
            <td></td>
        </tr>--%>
        <tr>
            <td>SensorID:</td>
            <td>Long</td>
            <td></td>
        </tr>
        <tr>
            <td>CableID:</td>
            <td>Long</td>
            <td></td>
        </tr>
        <tr>
            <td>DateCertified:</td>
            <td>DateTime</td>
            <td></td>
        </tr>
        <tr>
            <td>CertificationExpirationDate:</td>
            <td>DateTime</td>
            <td></td>
        </tr>
        <tr>
            <td>CalibrationFacilityID:</td>
            <td>Long</td>
            <td></td>
        </tr>
        <tr>
            <td>CalibrationNumber:</td>
            <td>String</td>
            <td></td>
        </tr>
        <%--         <tr>
            <td>CertificationType:</td>
            <td>String</td>
            <td></td>
        </tr>--%>
        <%--        <tr>
            <td>ReportInterval:</td>
            <td>double</td>
            <td></td>
        </tr>--%>
    </table>

    <h4>Example</h4>

    <input type="button" id="btn_TryAPI_CalibrationCertificateCreate" class="btn btn-primary btn-sm" value="Try this API" />
    <script>
        $(function () {
            $('#btn_TryAPI_CalibrationCertificateCreate').click(function () {
                var json =
                {
                    "auth": true,
                    "api": "CalibrationCertificateCreate",
                    "params": [
                        /*{ "name": "CalibrationCertificateID", "type": "Long", "description": "	Unique identifier of SubAccount. If omitted, Your default account will be used.", "optional": true },*/
                        { "name": "SensorID", "type": "Long", "description": "Unique identifier of the Notification. If included notification will be edited", "optional": false },
                        { "name": "DateCertified", "type": "DateTime", "description": "Name of notification", "optional": false },
                        { "name": "CertificationExpirationDate", "type": "DateTime", "description": "Text for the subject of the notification", "optional": false },
                        { "name": "CalibrationFacilityID", "type": "Long", "description": "Text for the body of the notification", "optional": false },
                        { "name": "CalibrationNumber", "type": "String", "description": "Replacement Text for the body of a SMS notification. Default text value will be used if left blank", "optional": false },
                        /*{ "name": "CertificationType", "type": "String", "description": "Replacement Text for a voice notification. Default text value will be used if left blank", "optional": false },*/
                        /*{ "name": "ReportInterval", "type": "Double", "description": "Unique identifier of the datum type (TemperatureData or WaterDetect)", "optional": false },*/
                    ]
                };
                APITest(json);
            });
        });
    </script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px; white-space: pre-wrap">
&lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot;?&gt;
&lt;SensorRestAPI xmlns:xsi=&quot;http://www.w3.org/2001/XMLSchema-instance&quot; xmlns:xsd=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
&nbsp;&nbsp;&lt;Method&gt;CalibrationCertificateCreate&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:collection&quot;&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APICalibrationCertificate CalibrationCertificateID=&quot;1234&quot; CreatedByUserID=&quot;1234&quot; SensorID=&quot;1234&quot; DateCreated=&quot;10/12/2022 5:08:14 PM&quot; DateCertified=&quot;10/12/2022 11:07:00 AM&quot; CertificationValidUntil=&quot;10/12/2024 11:07:00 AM&quot; CalibrationNumber=&quot;Test&quot; CalibrationFacilityID=&quot;1234&quot; CertificationType=&quot;&quot; DeletedByUserID=&quot;-9223372036854775808&quot; DeletedDate=&quot;1/1/0001 12:00:00 AM&quot;/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
