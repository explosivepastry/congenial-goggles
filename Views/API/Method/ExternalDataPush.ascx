<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: ExternalDataPush</b><br />
    Returns settings used to send data to external source.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ExternalDataPush/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599" target="_blank">https://<%=Request.Url.Host %>/xml/ExternalDataPush/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalDataPush&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;&lt;APIDataPushConfiguration SensorID="101" ExternalID="aQ231b" ConnectionInfo="http://mydomain.com/ReceiveSensorData?Sensor={0}&Data={2}" LastResult="Message Processed" /&gt;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>