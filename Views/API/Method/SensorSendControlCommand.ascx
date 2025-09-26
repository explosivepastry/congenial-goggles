<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorSendControlCommand</b><br />
    Sends a control command to the control sensor<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>state1:</td>
            <td>Integer</td>
            <td>State to send to relay 1</td>
        </tr>
        <tr>
            <td>time1:</td>
            <td>Integer (optional)</td>
            <td>Number of seconds for relay 1 to hold this new state before resuming the previous state</td>
        </tr>
        <tr>
            <td>state2:</td>
            <td>Integer</td>
            <td>State to send to relay 2</td>
        </tr>
        <tr>
            <td>time2:</td>
            <td>Integer (optional)</td>
            <td>Number of seconds for relay 2 to hold this new state before resuming the previous state</td>
        </tr>
    </table>
    <h4>Relay Command States</h4>
    <table>
        <tr>
            <td>0:</td>
            <td>No change, relay will stay in it's current state.</td>
        </tr>
        <tr>
            <td>1:</td>
            <td>Off, relay will be set to off.</td>
        </tr>
        <tr>
            <td>2:</td>
            <td>On, relay will be set to on.</td>
        </tr>
        <tr>
            <td>3:</td>
            <td>Toggle, relay will be set to the state it currently is not in.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SensorSendControlCommand/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&state1=2&time1=120&state2=0" target="_blank">https://<%=Request.Url.Host %>/xml/SensorSetHeartbeat/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;state1=2&amp;time1=120&amp;state2=0</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorSendControlCommand&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>