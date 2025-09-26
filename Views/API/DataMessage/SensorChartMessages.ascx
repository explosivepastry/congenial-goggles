<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorChartMessages</b><br />
    Returns chartable data points recorded in a range of time <br />
    <b>Interval Sensors:</b> (Temperature, Humidity, etc) datapoints with in the given range.<br />  
    <b>Active ID Sensors:</b> return the number of recorded checkins by date. <br />
    <b>Triggered Sensors:</b> (Open/Closed,Water, etc) return a count by date of the messages that were in the "Aware" state.<br />
    <br />
    *Maximum of 75 Datapoints returned.  If dataset returns more than 75 datapoints every n<sup>th</sup> datapoint (Calculated by Modulous of total datapoints divided by 75) is returned.<br />
    
    <p style="color:red;">SensorChartMessages is deprecated. To gather data from mulitple sensors utilize the Webhook API's.  This will phase out between Oct 01 2023 - Dec 31 2023.</p>
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
<%--    <a href="/xml/SensorChartMessages/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&fromDate=2011/01/01&toDate=2011/01/02" target="_blank">https://<%:Request.Url.Host %>/xml/SensorChartMessages/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=101&amp;fromDate=2011/01/01&amp;toDate=2011/01/02</a>--%>

<input type="button" id="btn_TryAPI_SensorChartMessages" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_SensorChartMessages').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "SensorChartMessages",
					"params": [
                        { "name": "sensorID", "type": "Integer", "description": "Unique identifier of the sensor", "optional": false },
                        { "name": "fromDate", "type": "Date", "description": "Start of range from which messages will be returned", "optional": false },					
						{ "name": "toDate", "type": "Date", "description": "End of range from which messages will be returned", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;SensorChartMessages&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIChartDataPointList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIChartDataPoint Date="1/1/2011 6:36:00 PM" Value="30" SentNotification="True"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIChartDataPoint Date="1/1/2011 6:34:33 PM" Value="50" SentNotification="True"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIChartDataPointList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>