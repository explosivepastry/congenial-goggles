<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorSetTag()</b><br />
     sets the Notification active or inactive<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Long</td>
            <td>Unique identifier of the Sensor</td>
        </tr>
        <tr>
            <td>tag:</td>
            <td>String</td>
            <td>This is a pipe deliminated string that gives a way to group sensors</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SensorSetTag/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=250&tag=Room23|Temp" target="_blank">https://<%=Request.Url.Host %>/xml/SensorSetTag/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=250&tag=Room23|Temp</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorSetTag&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
