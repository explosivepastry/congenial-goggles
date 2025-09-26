<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorAttributes</b><br />
    Returns the list of attributes that belong to a sensor.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>SensorID:</td>
            <td>interger</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SensorAttributes/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101" target="_blank">https://<%=Request.Url.Host %>/xml/SensorAttributes/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorAttributes&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorAttribute&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorAttribute SensorID="101" Name="CorF" Value="C" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APISensorAttribute&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>