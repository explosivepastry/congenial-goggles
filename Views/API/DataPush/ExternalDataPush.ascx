<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>
    ExternalDataPush</h3>
<div class="methodDiv">
    <b>Method: ExternalDataPush</b><br />
    Returns settings used to send data to external source.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ExternalDataPush/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599" target="_blank">https://<%:Request.Url.Host %>/xml/ExternalDataPush/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalDataPush&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataPushConfiguration SensorID="101" ExternalID="None" ConnectionInfo="http://www.mydomain.com/{1}" verb="get" ConnectionBody="" LastResult=""/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt</pre>
</div>