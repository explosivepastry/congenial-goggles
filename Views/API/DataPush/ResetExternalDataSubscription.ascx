<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: ResetExternalDataSubscription</b><br />
    Resets the send count of the External Data Subscription to have it attempt to resend subscriptions again.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>SensorID:</td>
            <td>Integer (optional: must use either sensorid or gatewayid value)</td>
            <td>Unique identifier for a sensor</td>
        </tr>
        <tr>
            <td>GatewayId:</td>
            <td>Integer (optional: must use either sensorid or gatewayid value)</td>
            <td>Unique identifier for a gateway</td>
        </tr>
         <tr>
            <td>AccountID:</td>
            <td>Integer</td>
            <td>Unique identifier of all External Data pushes that belong to the specific account</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ResetExternalDataSubscription/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=228&accountID=101" target="_blank">https://<%:Request.Url.Host %>/xml/ResetExternalDataSubscription/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=228&accountID=101</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;ResetExternalDataSubscription&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>