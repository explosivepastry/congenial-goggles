<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>
    ExternalGatewayDataPushRemove</h3>
<div class="methodDiv">
    <b>Method: ExternalGatewayDataPushRemove</b><br />
    Removes configuration used to send data to external source.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>gatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ExternalGatewayDataPushRemove/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=003420" target="_blank">https://<%:Request.Url.Host %>/xml/ExternalGatewayDataPushRemove/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=003420</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalGatewayDataPushRemove&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>