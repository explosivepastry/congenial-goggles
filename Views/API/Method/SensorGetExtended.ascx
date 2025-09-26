<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorGetExtended</b><br />
    Returns the sensor object with extended Properties.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SensorGetExtended/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=000101" target="_blank">https://<%=Request.Url.Host %>/xml/SensorGetExtended/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=000101</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorGetExtended&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;&lt;APISensorExteded ReportInterval="0" ActiveStateInterval="0" InactivityAlert="0" MeasurementsPerTransmission="0" MinimumThreshold="0" MaximumThreshold="0" Hysteresis="0" SensorID="101" MonnitApplicationID="2" CSNetID="100" SensorName="Temp 101" LastCommunicationDate="6/20/2011 6:36:23 PM" NextCommunicationDate="6/29/2011 8:38:23 PM" LastDataMessageID="230" PowerSourceID="1" Status="1" CanUpdate="True" CurrentReading="86° F" BatteryLevel="0" SignalStrength="-36" AlertsActive="False" /&gt;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>