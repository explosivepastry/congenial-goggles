<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorGet</b><br />
    Returns the sensor object.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SensorGet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599" target="_blank">https://<%=Request.Url.Host %>/xml/SensorGet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorGet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;&lt;APISensor SensorID="101" MonnitApplicationID="2" CSNetID="100" SensorName="Room3_Ceiling" LastCommunicationDate="6/1/2011 4:09:41 PM" NextCommunicationDate="6/13/2011 6:11:41 PM" LastDataMessageID="1" PowerSourceID="1" Status="1" CanUpdate="True" CurrentReading="122° F" BatteryLevel="0" SignalStrength="-36" AlertsActive="True" /&gt;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>