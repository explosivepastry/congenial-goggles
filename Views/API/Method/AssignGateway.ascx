<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AssignGateway</b><br />
    Assigns gateway to the specified network<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NetworkID:</td>
            <td>Integer</td>
            <td>Identifier of network on your account</td>
        </tr>
        <tr>
            <td>GatewayID:</td>
            <td>Integer</td>
            <td>Identifier of gateway to move</td>
        </tr>
        <tr>
            <td>CheckDigit:</td>
            <td>String</td>
            <td>Check digit to prevent unauthorized movement of gateways</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/AssignGateway/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=2&gatewayID=100&checkDigit=AAAAAA" target="_blank">https://<%=Request.Url.Host %>/xml/AssignGateway/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=2&amp;gatewayID=100&amp;checkDigit=AAAAAA</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;AssignGateway&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>