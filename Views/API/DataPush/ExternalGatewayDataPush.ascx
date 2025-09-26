<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>
    ExternalGatewayDataPush</h3>
<div class="methodDiv">
    <b>Method: ExternalGatewayDataPush</b><br />
    Returns settings used to send data to external source.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>gatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ExternalGatewayDataPush/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=003420" target="_blank">https://<%:Request.Url.Host %>/xml/ExternalGatewayDataPush/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=003420</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalGatewayDataPush&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIGatewayDataPushConfiguration GatewayID="8585" ExternalID="None" ConnectionInfo="http://www.mydomain.com/{1}" verb="get" ConnectionBody="" LastResult="" HeaderType=""/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>