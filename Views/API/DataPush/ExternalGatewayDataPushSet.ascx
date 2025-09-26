<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv" >
    <b>Method: ExternalGatewayDataPushSet</b><br />
    Sets the configuration for data push to 3rd party server<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>gatewayID:</td>
            <td>Integer</td>
            <td>Unique identifier of the gateway</td>
        </tr>
        <tr>
            <td>connectionInfo:</td>
            <td>String</td>
            <td>Url of 3rd party server and defined data to send (Url Encoded). If this value contains an & character, <br /> it must be replaced by its urlencoding, %26. Depending on the context, you may also have to <br  />escape a % using %25</td>
        </tr>
        <tr>
            <td>externalID:</td>
            <td>String (optional)</td>
            <td>Identifier to send to 3rd party server</td>
        </tr>
        <tr>
            <td>verb:</td>
            <td>String (optional)</td>
            <td>The type of send used. Must be either 'get' or 'post' without apostrophes.</td>
        </tr>
        <tr>
            <td>connectionBody:</td>
            <td>String (required for post)</td>
            <td>Body of an html post</td>
        </tr>
        <tr>
            <td>ContentHeaderType:</td>
            <td>String (required for post)</td>
            <td>Header type. Can be 'raw', 'application/json', or 'application/x-www-form-urlencoded'</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ExternalGatewayDataPushSet/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=003420&externalID=aQ231b&verb=get&connectionInfo=<%:HttpContext.Current.Server.UrlEncode("http://mydomain.com/ReceiveGatewayData?Gateway={0}&MessageType={2}")%>" target="_blank">https://<%:Request.Url.Host %>/xml/ExternalGatewayDataPushSet/Z3Vlc3Q6cGFzc3dvcmQ=?gatewayID=003420&amp;externalID=aQ231b&amp;verb=get&amp; connectionInfo=<%:HttpContext.Current.Server.UrlEncode("http://mydomain.com/ReceiveGatewayData?Gateway={0}&MessageType={2}") %>"</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalGatewayDataPushSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>