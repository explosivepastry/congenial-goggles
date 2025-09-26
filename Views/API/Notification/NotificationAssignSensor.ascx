<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationAssignSensor</b><br />
    Asssigns a sensor to a specific notification.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>SensorID:</td>
            <td>Long</td>
            <td>Identifier of sensor to assign</td>
        </tr>
            <tr>
            <td>Index:</td>
            <td>Integer</td>
            <td>unique identifier of the datum</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationAssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&sensorID=123&index=1" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationAssignSensor/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;sensorID=123&amp;index=1</a>--%>

    <input type="button" id="btn_TryAPI_NotificationAssignSensor" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationAssignSensor').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationAssignSensor",
					"params": [
                        { "name": "NotificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "SensorID", "type": "Long", "description": "Identifier of sensor to assign", "optional": false },					
                        { "name": "Index", "type": "Integer", "description": "Unique identifier of the datum", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;NotificationAssignSensor&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensor SensorID="123" ApplicationID="2" CSNetID="100" SensorName="Room3_Ceiling" LastCommunicationDate="6/1/2011 4:09:41 PM" NextCommunicationDate="6/13/2011 6:11:41 PM" LastDataMessageID="1" PowerSourceID="1" Status="1" CanUpdate="True" CurrentReading="122° F" BatteryLevel="0" SignalStrength="-36" AlertsActive="True" MonnitApplicationID="2" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APISensorList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>