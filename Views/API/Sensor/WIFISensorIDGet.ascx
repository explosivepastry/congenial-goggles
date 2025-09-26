<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WIFISensorIDGet</b><br />
    Returns the gateway object.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>MAC:</td>
            <td>string</td>
            <td>Unique identifier of the sensor at the gateway level</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/WIFISensorIDGet/Z3Vlc3Q6cGFzc3dvcmQ=?mac=00:11:22:33:44:55" target="_blank">https://<%:Request.Url.Host %>/xml/WIFISensorIDGet/Z3Vlc3Q6cGFzc3dvcmQ=?mac=00:11:22:33:44:55</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;WIFISensorIDGet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:int"&gt;101&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>