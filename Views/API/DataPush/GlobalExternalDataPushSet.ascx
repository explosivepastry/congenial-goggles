<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GlobalExternalDataPushSet</b><br />
    Sets the configuration for pushing all data to a 3rd party server<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountid:</td>
            <td>Integer</td>
            <td>Unique identifier of your account</td>
        </tr>
        <tr>
            <td>sensorUrl:</td>
            <td>String</td>
            <td>Url of 3rd party server for sensor data. If this value contains an & character, it must be replaced by its urlencoding, %26. Depending on the context, you may also have to escape a % using %25</td>
        </tr>
        <tr>
            <td>gatewayUrl:</td>
            <td>String</td>
            <td>Url of 3rd party server for gateway data. If this value contains an & character, it must be replaced by its urlencoding, %26. Depending on the context, you may also have to escape a % using %25</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/GlobalExternalDataPushSet/Z3Vlc3Q6cGFzc3dvcmQ=?accountid=65432&sensorUrl=<%=HttpContext.Current.Server.UrlEncode("http://mydomain.com/ReceiveSensorData")%>&gatewayUrl=http://mydomain.com/ReceiveGatewayData" target="_blank">https://<%=Request.Url.Host %>/xml/GlobalExternalDataPushSet/Z3Vlc3Q6cGFzc3dvcmQ=?accountid=65432&amp;sensorUrl=<%=HttpContext.Current.Server.UrlEncode("http://mydomain.com/ReceiveSensorData") %>&gatewayUrl=http://mydomain.com/ReceiveGatewayData</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;GlobalExternalDataPushSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>