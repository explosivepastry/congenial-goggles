<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorGetCalibration</b><br />
    Returns the sensor object with calibration properties.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SensorGetCalibration/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=000101" target="_blank">https://<%=Request.Url.Host %>/xml/SensorGetCalibration/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=000101</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorGetCalibration&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;&lt;APISensorCalibration SensorID="101" MonnitApplicationID="2" CSNetID="1" SensorName="Temp 101" Calibration1="183" Calibration2="100000" Calibration3="560000" Calibration4="10000" EventDetectionType="-2147483648" EventDetectionPeriod="-2147483648" EventDetectionCount="-2147483648" RearmTime="0" BiStable="-2147483648" PushProfileConfig1="False" PushProfileConfig2="False" PushAutoCalibrateCommand="False" /&gt;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>