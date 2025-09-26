<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GlobalExternalDataPush</b><br />
    Returns settings used to send all data to external source.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountid:</td>
            <td>Integer</td>
            <td>Unique identifier of your account</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/GlobalExternalDataPush/Z3Vlc3Q6cGFzc3dvcmQ=?accountid=65194" target="_blank">https://<%=Request.Url.Host %>/xml/GlobalExternalDataPush/Z3Vlc3Q6cGFzc3dvcmQ=?accountid=65194</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalDataPush&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;GlobalAPIDataPushConfiguration SensorConnectionInfo=&quot;http://www.mywebsite.com&quot; SensorConnectionBody=&quot;&quot; SensorLastResult=&quot;&quot; GatewayConnectionInfo=&quot;http://www.mywebsite.com&quot; GatewayConnectionBody=&quot;&quot; GatewayLastResult=&quot;&quot;/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt</pre>
</div>