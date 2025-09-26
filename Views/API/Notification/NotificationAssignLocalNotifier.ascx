<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationAssignLocalNotifier</b><br />
    Assigns a local notifier to the specified notification <br />
    
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
            <td>LED_ON:</td>
            <td>String</td>
            <td>State of device:(Inactive On  Off Toggle)</td>
        </tr>
          <tr>
            <td>Buzzer_ON:</td>
            <td>String</td>
            <td>State of device:(Inactive On  Off Toggle)</td>
        </tr>
         <tr>
            <td>AutoScroll_ON:</td>
            <td>Integer</td>
            <td>Time Delay in seconds for Relay 1 </td>
        </tr>
           <tr>
            <td>BackLight_ON:</td>
            <td>Integer</td>
            <td>Time Delay in seconds for Relay 2 </td>
        </tr>
    </table>
    
    
    <h4>Example</h4>  
<%-- <a href="/xml/NotificationAssignLocalNotifier/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&deviceID=123&LED_ON=True&Buzzer_ON=True&AutoScroll_ON=False&BackLight_ON=False" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationAssignLocalNotifier/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;deviceID=123&amp;LED_ON=True&amp;Buzzer_ON=True&amp;AutoScroll_ON=False&amp;BackLight_ON=False</a> --%>

<input type="button" id="btn_TryAPI_NotificationAssignLocalNotifier" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationAssignLocalNotifier').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationAssignLocalNotifier",
					"params": [
                        { "name": "notificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "DeviceID", "type": "Long", "description": "Unique identifier of device to assign", "optional": false },					
                        { "name": "LED_ON", "type": "String", "description": "State of device:(Inactive On  Off Toggle)", "optional": false },
                        { "name": "Buzzer_ON", "type": "String", "description": "State of device:(Inactive On  Off Toggle)<", "optional": false },
                        { "name": "AutoScroll_ON", "type": "Integer", "description": "Time Delay in seconds for Relay 1", "optional": false },
                        { "name": "BackLight_ON", "type": "Integer", "description": "Time Delay in seconds for Relay 2", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;NotificationAssignLocalNotifier&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationLocalNotifier&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationLocalNotifier LocalNotifierRecipientID="1496" LED_ON="True" Buzzer_ON="True" AutoScroll_ON="False" BackLight_ON="False" NotifierName="Notifier AV - 213"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationLocalNotifier&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>