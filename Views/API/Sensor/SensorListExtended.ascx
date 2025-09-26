<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorListExtended</b><br />
    Returns the list of sensors that belongs to user.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>Name:</td>
            <td>String (optional)</td>
            <td>Filters list to sensors with names containing this string. (case-insensitive)</td>
        </tr>
        <tr>
            <td>NetworkID:</td>
            <td>Integer (optional)</td>
            <td>Filters list to sensor that belong to this network id</td>
        </tr>
        <tr>
            <td>ApplicationID:</td>
            <td>Integer (optional)</td>
            <td>Filters list to sensor that are this application type</td>
        </tr>
        <tr>
            <td>Status:</td>
            <td>Integer (optional)</td>
            <td>Filters list to sensor that match this status</td>
        </tr>
        <%--<%if (ViewBag.ShowResellerParameters == true)
          {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} %>--%>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/SensorListExtended/Z3Vlc3Q6cGFzc3dvcmQ=?name=Room3&applicationID=2" target="_blank">https://<%:Request.Url.Host %>/xml/SensorListExtended/Z3Vlc3Q6cGFzc3dvcmQ=?name=Room3&amp;applicationID=2</a>--%>
    <%--<br />
    Only sensors that contain "Room3" in the name are returned
    <br />
    Only sensors with application id 2 (temperature) are returned --%>
        <input type="button" id="btn_TryAPI_SensorListExtended" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorListExtended').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorListExtended",
					"params": [                       
                        { "name": "Name", "type": "String", "description": "Filters list to sensors with names containing this string. (case-insensitive)", "optional": true },
						{ "name": "NetworkID", "type": "Integer", "description": "Filters list to sensor that belong to this network id", "optional": true },
						{ "name": "ApplicationID", "type": "Integer", "description": "Filters list to sensor that are this application type", "optional": true },
						{ "name": "Status", "type": "Integer", "description": "Filters list to sensor that match this status", "optional": true },
						{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true }
					]
				};								
				APITest(json);
			});
		});
	</script>
    
                
    <h4>Example Output</h4>
    <%if (MonnitSession.CurrentTheme.Theme == "Default")
      {  %>
    <%} %>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorListExtended&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorListExtended&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorExteded ReportInterval="0" ActiveStateInterval="0" InactivityAlert="0" MeasurementsPerTransmission="0" MinimumThreshold="0" MaximumThreshold="0" Hysteresis="0" SensorID="101" ApplicationID="2" CSNetID="100" SensorName="Room3_Ceiling" LastCommunicationDate="6/1/2011 4:09:41 PM" NextCommunicationDate="6/13/2011 6:11:41 PM" LastDataMessageID="1" PowerSourceID="1" Status="1" CanUpdate="True" CurrentReading="122° F" BatteryLevel="0" SignalStrength="-36" AlertsActive="True" AccountID="1234" MonnitApplicationID="2" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorExteded ReportInterval="0" ActiveStateInterval="0" InactivityAlert="0" MeasurementsPerTransmission="0" MinimumThreshold="0" MaximumThreshold="0" Hysteresis="0" SensorID="102" ApplicationID="2" CSNetID="100" SensorName="Room3_Floor" LastCommunicationDate="1/1/2099 12:00:00 AM" NextCommunicationDate="1/1/2099 12:01:00 AM" LastDataMessageID="-9223372036854775808" PowerSourceID="1" Status="4" CanUpdate="True" CurrentReading="No Reading Available" BatteryLevel="100" SignalStrength="100" AlertsActive="True" AccountID="1234" MonnitApplicationID="2" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorExteded ReportInterval="0" ActiveStateInterval="0" InactivityAlert="0" MeasurementsPerTransmission="0" MinimumThreshold="0" MaximumThreshold="0" Hysteresis="0" SensorID="10" ApplicationID="2" CSNetID="100" SensorName="Room3_Door" LastCommunicationDate="6/1/2011 5:21:08 PM" NextCommunicationDate="6/13/2011 7:23:08 PM" LastDataMessageID="218" PowerSourceID="1" Status="1" CanUpdate="False" CurrentReading="-1767.8° F" BatteryLevel="100" SignalStrength="-59" AlertsActive="True" AccountID="1234" MonnitApplicationID="2" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APISensorListExtended&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
