<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <div style="color: red"><%:Html.TranslateTag("SensorDataMessage API endpoint can be used to periodically gather Data Messages from a device.  But it isn’t intended to be called regularly to pull all data from a device into a separate system.   To gather data from devices for integrating another system utilize Webhook API’s") %></div>
    </br>

    <b>Method: SensorDataMessages</b><br />
    Returns data points recorded in a range of time (limited to a 7 day window)<br />
    *Maximum of 5000 Datapoints returned.
    <br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>fromDate:</td>
            <td>Date</td>
            <td>Start of range from which messages will be returned</td>
        </tr>
        <tr>
            <td>toDate:</td>
            <td>Date</td>
            <td>End of range from which messages will be returned</td>
        </tr>
    </table>

    <h4>Example</h4>
    <%-- <a href="/xml/SensorDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&fromDate=2011/01/01%206:22:14%20PM&toDate=2011/01/02%206:22:14%20PM" target="_blank">https://<%:Request.Url.Host %>/xml/SensorDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;fromDate=2011/01/01 6:22:14 PM&amp;toDate=2011/01/02 6:22:14 PM</a>--%>

    <input type="button" id="btn_TryAPI_SensorDataMessages" class="btn btn-primary btn-sm" value="Try this API" />
    <script>
        $(function () {
            $('#btn_TryAPI_SensorDataMessages').click(function () {
                var json =
                {
                    "auth": true,
                    "api": "SensorDataMessages",
                    "params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the user", "optional": false },
                        { "name": "fromDate", "type": "Date", "description": "Start of range from which messages will be returned", "optional": false },
                        { "name": "toDate", "type": "Date", "description": "End of range from which messages will be returned", "optional": false }
                    ]
                };
                APITest(json);
            });
        });
    </script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorDataMessages&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessageList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage SensorID="101" MessageDate="1/1/2011 6:36:00 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="30" DisplayData="86° F" PlotValue="86" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage  SensorID="101" MessageDate="1/1/2011 6:34:33 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="50" DisplayData="122° F" PlotValue="122" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDataMessageList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
