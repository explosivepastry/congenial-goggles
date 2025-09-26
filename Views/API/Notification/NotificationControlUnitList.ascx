<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationControlUnitList</b><br />
    Returns all Control Units on the specified notification <br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
    </table>
    
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationControlUnitList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=101" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationControlUnitList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=101</a>--%>

<input type="button" id="btn_TryAPI_NotificationControlUnitList" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationControlUnitList').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationControlUnitList",
					"params": [
                        { "name": "notificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;NotificationControlUnitList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationControlUnitList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationControlUnit ControlUnitRecipientID="1496" Relay1State="Off" Relay1Time="0 Seconds" Relay2State="Inactive" Relay2Time="NULL"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationControlUnit ControlUnitRecipientID="1581" Relay1State="Off" Relay1Time="90 Seconds" Relay2State="" Relay2Time="0 Seconds" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationControlUnit ControlUnitRecipientID="1582" Relay1State="Inactive" Relay1Time="NULL" Relay2State="On" Relay2Time="60 Seconds" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationControlUnitList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>