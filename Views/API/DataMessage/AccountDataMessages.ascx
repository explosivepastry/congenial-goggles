<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountDataMessages</b><br />
    Returns data points recorded in a range of time (limited to a 7 day window)<br />
    *Maximum of 5000 Datapoints returned. <br />

    <p style="color:red;">AccountDataMessages is deprecated. To gather data from mulitple sensors utilize the Webhook API's.  This will phase out between Oct 01 2023 - Dec 31 2023.</p>
    <h4>Parameters</h4>
    <table>
        <%--<%if (ViewBag.ShowResellerParameters  == true)
          {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} %>--%>
        <tr>
            <td>networkID</td>
            <td>Integer (optional) </td>
            <td>Unique identifier of the network</td>
        </tr>
        <tr>
            <td>fromDate:</td>
            <td>DateTime</td>
            <td>Start of range from which messages will be returned</td>
        </tr>
        <tr>
            <td>toDate:</td>
            <td>DateTime</td>
            <td>End of range from which messages will be returned</td>
        </tr>
    </table>

    <h4>Example</h4>
<%--    <a href="/xml/AccountDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?fromDate=2011/01/01%206:22:14%20PM&toDate=2011/01/02%206:22:14%20PM" target="_blank">https://<%:Request.Url.Host %>/xml/AccountDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?fromDate=2011/01/01 6:22:14 PM&amp;toDate=2011/01/02 6:22:14 PM</a>--%>

    <input type="button" id="btn_TryAPI_AccountDataMessages" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_AccountDataMessages').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "AccountDataMessages",
					"params": [
                        { "name": "networkIDInteger", "type": "Integer", "description": "Unique identifier of the network", "optional": true },
                        { "name": "fromDate", "type": "DateTime", "description": "Start of range from which messages will be returned", "optional": false },					
						{ "name": "toDate", "type": "DateTime", "description": "End of range from which messages will be returned", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
    </script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto;max-width:835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;AccountDataMessages&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessageList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage  SensorID="101" MessageDate="1/1/2011 6:36:00 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="30" DisplayData="86° F" PlotValue="86" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage  SensorID="101" MessageDate="1/1/2011 6:34:33 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="50" DisplayData="122° F" PlotValue="122" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDataMessageList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>
