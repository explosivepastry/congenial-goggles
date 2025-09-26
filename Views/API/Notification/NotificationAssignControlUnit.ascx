<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationAssignControlUnit</b><br />
    Assigns a Control Unit to the specified notification <br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
           <tr>
            <td>DeviceID:</td>
            <td>Long</td>
            <td>Unique identifier of device to assign</td>
        </tr>
          <tr>
            <td>Relay1State:</td>
            <td>String</td>
            <td>State of device:(Inactive On  Off Toggle)</td>
        </tr>
          <tr>
            <td>Relay2State:</td>
            <td>String</td>
            <td>State of device:(Inactive On  Off Toggle)</td>
        </tr>
         <tr>
            <td>Relay1Time:</td>
            <td>Integer</td>
            <td>Time Delay in seconds for Relay 1 </td>
        </tr>
           <tr>
            <td>Relay2Time:</td>
            <td>Integer</td>
            <td>Time Delay in seconds for Relay 2 </td>
        </tr>
    </table>
    
    
    <h4>Example</h4> 
<%--    <a href="/xml/NotificationAssignControlUnit/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&deviceID=123&Relay1State=on&Relay2State=inactive&Relay1Time=30&Relay2Time=0" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationAssignControlUnit/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;deviceID=123&amp;Relay1State=on&amp;Relay2State=inactive&amp;Relay1Time=30&amp;Relay2Time=0</a> --%>
    
<input type="button" id="btn_TryAPI_NotificationAssignControlUnit" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationAssignControlUnit').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationAssignControlUnit",
					"params": [
                        { "name": "notificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "DeviceID", "type": "Long", "description": "Unique identifier of device to assign", "optional": false },					
                        { "name": "Relay1State", "type": "String", "description": "State of device:(Inactive On  Off Toggle)", "optional": false },
                        { "name": "Relay2State", "type": "String", "description": "State of device:(Inactive On  Off Toggle)", "optional": false },
                        { "name": "Relay1Time", "type": "Integer", "description": "Time Delay in seconds for Relay 1", "optional": false },
                        { "name": "Relay2Time", "type": "Integer", "description": "Time Delay in seconds for Relay 2", "optional": false }						

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
&nbsp;&nbsp;&lt;Method&gt;NotificationAssignControlUnit&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationControlUnit&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationControlUnit ControlUnitRecipientID="1496" Relay1State="Off" Relay1Time="0 Seconds" Relay2State="Inactive" Relay2Time="NULL"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationControlUnit&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>