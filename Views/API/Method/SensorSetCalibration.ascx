<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorSetCalibration</b><br />
    Sets the calibration values of the sensor.<br />
    
    Calibration if different for each sensor profile (temp, light, etc.) You'll need to know what profile of sensor your calibrating.  The calibration routine for any given provile will not use all of the values available.  Before calling this method you should first call "SensorGetCalibration" to get the correct values for the fields you will not be changing during the calibration.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>calibration1:</td>
            <td>64 bit Integer</td>
            <td>Value used to store calibration values on sensors. (profile dependant)</td>
        </tr>
        <tr>
            <td>calibration2:</td>
            <td>64 bit Integer</td>
            <td>Value used to store calibration values on sensors. (profile dependant)</td>
        </tr>
        <tr>
            <td>calibration3:</td>
            <td>64 bit Integer</td>
            <td>Value used to store calibration values on sensors. (profile dependant)</td>
        </tr>
        <tr>
            <td>calibration4:</td>
            <td>64 bit Integer</td>
            <td>Value used to store calibration values on sensors. (profile dependant)</td>
        </tr>
        <tr>
            <td>eventDetectionType:</td>
            <td>Integer</td>
            <td>Type of event detected (profile dependant)</td>
        </tr>
        <tr>
            <td>eventDetectionCount:</td>
            <td>Integer</td>
            <td>Number of events required to trigger (profile dependant)</td>
        </tr>
        <tr>
            <td>eventDetectionPeriod:</td>
            <td>Integer</td>
            <td>Time window for event count to be reached (profile dependant)</td>
        </tr>
        <tr>
            <td>rearmTime:</td>
            <td>Integer</td>
            <td>Time before event can be triggered again (profile dependant)</td>
        </tr>
        <tr>
            <td>biStable:</td>
            <td>Integer</td>
            <td>Direction of event (profile dependant)</td>
        </tr>
        <tr>
            <td>pushProfileConfig1:</td>
            <td>boolean</td>
            <td>Set the configuration page to be pushed to the sensor (profile dependant)</td>
        </tr>
        <tr>
            <td>pushProfileConfig2:</td>
            <td>boolean</td>
            <td>Set the configuration page to be pushed to the sensor (profile dependant)</td>
        </tr>
        <tr>
            <td>pushAutoCalibrateCommand:</td>
            <td>boolean</td>
            <td>Set the auto calibrate command to be pushed to the sensor (profile dependant)</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SensorSetCalibration/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&calibration1=0&calibration2=0&calibration3=0&calibration4=0&eventDetectionType=0&eventDetectionPeriod=0&eventDetectionCount=0&rearmTime=0&biStable=0&pushProfileConfig1=false&pushProfileConfig2=false&pushAutoCalibrateCommand=false" target="_blank">
        https://<%=Request.Url.Host %>/xml/SensorSetCalibration/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;calibration1=0&amp;calibration2=0&amp;calibration3=0&amp;calibration4=0 &amp;eventDetectionType=0&amp;eventDetectionPeriod=0&amp;eventDetectionCount=0&amp;rearmTime=0&amp;biStable=0&amp;pushProfileConfig1=false&amp;pushProfileConfig2=false &amp;pushAutoCalibrateCommand=false
    </a>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorSetCalibration&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>