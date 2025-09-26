<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: ExternalDataPushSet</b><br />
    Sets the configuration for data push to 3rd party server<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>connectionInfo:</td>
            <td>String</td>
            <td>Url of 3rd party server and defined data to send (Url Encoded)</td>
        </tr>
        <tr>
            <td>externalID:</td>
            <td>String (optional)</td>
            <td>Identifier to send to 3rd party server</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ExternalDataPushSet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599&externalID=aQ231b&connectionInfo=<%=HttpContext.Current.Server.UrlEncode("http://mydomain.com/ReceiveSensorData?Sensor={0}&Data={2}")%>" target="_blank">https://<%=Request.Url.Host %>/xml/ExternalDataPushSet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599&amp;externalID=aQ231b&amp;connectionInfo=<%=HttpContext.Current.Server.UrlEncode("http://mydomain.com/ReceiveSensorData?Sensor={0}&Data={2}") %>"</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalDataPushSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>