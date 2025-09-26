<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>
    ExternalGatewayDataPushList</h3>
<div class="methodDiv">
    <b>Method: ExternalGatewayDataPushList</b><br />
     Returns a list of settings used to send data to external source.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID:</td>
            <td>Integer</td>
            <td>Unique identifier of all External Data Pushes that belong to the specific account</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ExternalGatewayDataPushList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101" target="_blank">https://<%:Request.Url.Host %>/xml/ExternalGatewayDataPushList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalGatewayDataPushList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;&lt;APIGatewayDataPushConfiguration GatewayID="3420" ExternalID="aQ231b" ConnectionInfo="http://mydomain.com/ReceiveGatewayData?Gateway={0}&MessageType={2}" LastResult="Message Processed" /&gt;&lt;/Result&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;&lt;APIGatewayDataPushConfiguration GatewayID="3421" ExternalID="aQ231b" ConnectionInfo="http://mydomain.com/ReceiveGatewayData?Gateway={0}&MessageType={2}" LastResult="Message Processed" /&gt;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>