<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorRecentDataMessages</b><br />
    Returns data points recorded in a range of time (limited to a 1 day window)<br />
     *Maximum of 5000 Datapoints returned. <br />

    <p style="color:red;">SensorRecentDataMessages is deprecated. To gather data from mulitple sensors utilize the Webhook API's.  This will phase out between Oct 01 2023 - Dec 31 2023.</p>
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>minutes:</td>
            <td>Integer</td>
            <td>Number of minutes past messages will be returned</td>
        </tr>
           <tr>
            <td>lastDataMessageGUID:</td>
            <td>GUID (optional)</td>
            <td>Only return messages received after this message ID</td>
        </tr>
    
    
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/SensorRecentDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&minutes=10&lastDataMessageGUID=xxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" target="_blank">https://<%:Request.Url.Host %>/xml/SensorRecentDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;minutes=5&amp;lastDataMessageGUID=xxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx</a>--%>

<input type="button" id="btn_TryAPI_SensorRecentDataMessages" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_SensorRecentDataMessages').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "SensorRecentDataMessages",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
                        { "name": "minutes", "type": "Integer", "description": "Number of minutes past messages will be returned", "optional": false },					
						{ "name": "lastDataMessageGUID", "type": "GUID", "description": "Only return messages received after this message ID", "optional": true }						
					]
				};								
				APITest(json);
			});
		});
    </script>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorRecentDataMessages&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessageList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage SensorID="101" MessageDate="1/1/2011 6:36:00 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="30" DisplayData="86° F" PlotValue="86" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage  SensorID="101" MessageDate="1/1/2011 6:34:33 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="50" DisplayData="122° F" PlotValue="122" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDataMessageList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>