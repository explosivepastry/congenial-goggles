<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorAssignedToNotificaiton</b><br />
    Returns the list of sensors that belongs to user based on the notification they are assigned to.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationID:</td>
            <td>Long</td>
            <td>Filters list to sensors that belong to this notification id</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/SensorAssignedToNotificaiton/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3" target="_blank">https://<%:Request.Url.Host %>/xml/SensorAssignedToNotificaiton/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3</a>--%>
    <br />Only sensors that contain a notification id of 3 are returned
    <br />
    
<input type="button" id="btn_TryAPI_SensorAssignedToNotificaiton" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_SensorAssignedToNotificaiton').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "SensorAssignedToNotificaiton",
					"params": [
                        { "name": "notificationID", "type": "Long", "description": "Filters list to sensors that belong to this notification id", "optional": false }
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
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorAssignedToNotificaiton&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensor SensorID="101" ApplicationID="2" CSNetID="100" SensorName="Room3_Ceiling" LastCommunicationDate="6/1/2011 4:09:41 PM" NextCommunicationDate="6/13/2011 6:11:41 PM" LastDataMessageID="1" PowerSourceID="1" Status="1" CanUpdate="True" CurrentReading="122° F" BatteryLevel="0" SignalStrength="-36" AlertsActive="True" MonnitApplicationID="2" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensor SensorID="102" ApplicationID="2" CSNetID="100" SensorName="Room3_Floor" LastCommunicationDate="1/1/2099 12:00:00 AM" NextCommunicationDate="1/1/2099 12:01:00 AM" LastDataMessageID="-9223372036854775808" PowerSourceID="1" Status="4" CanUpdate="True" CurrentReading="No Reading Available" BatteryLevel="100" SignalStrength="100" AlertsActive="True" MonnitApplicationID="2" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensor SensorID="10"  ApplicationID="2" CSNetID="100" SensorName="Room3_Door" LastCommunicationDate="6/1/2011 5:21:08 PM" NextCommunicationDate="6/13/2011 7:23:08 PM" LastDataMessageID="218" PowerSourceID="1" Status="1" CanUpdate="False" CurrentReading="-1767.8° F" BatteryLevel="100" SignalStrength="-59" AlertsActive="True" MonnitApplicationID="2" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APISensorList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
