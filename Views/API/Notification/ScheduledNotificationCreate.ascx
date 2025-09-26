<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: ScheduledNotificationCreate</b><br />
    Create or edit a Scheduled/Timed notification.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <tr>
            <td>notificationID:</td>
            <td>Long (optional)</td>
            <td>Unique identifier of the Notification. If included notification will be edited</td>
        </tr>
        <tr>
            <td>Name:</td>
            <td>String</td>
            <td>Name of notification</td>
        </tr>
        <tr>
            <td>Subject:</td>
            <td>String</td>
            <td>Text for the subject of the notification</td>
        </tr>
        <tr>
            <td>Text:</td>
            <td>String</td>
            <td>Text for the body of the notification</td>
        </tr>
        <tr>
            <td>smsText:</td>
            <td>String</td>
            <td>Replacement Text for the body of a SMS notification. Default text value will be used if left blank.</td>
        </tr>
        <tr>
            <td>VoiceText:</td>
            <td>String</td>
            <td>Replacement Text for a voice notification.  Default text value will be used if left blank.</td>
        </tr>
        <tr>
            <td>PushMsgText:</td>
            <td>String</td>
            <td>Replacement Text for a push message notification.  Default text value will be used if left blank.</td>
        </tr>
        <tr>
            <td>LocalHour:</td>
            <td>Integer</td>
            <td>Local hour to send notification ( 0 - 23 )</td>
        </tr>
        <tr>
            <td>LocalMinute:</td>
            <td>Integer</td>
            <td>Local minute to send notification ( 0 - 59 )</td>
        </tr>

        <tr>
            <td>SensorID or GatewayID</td>
            <td>Long (optional)</td>
            <td>Unique identifier of the sensor or gateway. If included notification will return most recent reading</td>
        </tr>
    </table>


    <h4>Example</h4>
<%--    <a href="/xml/ScheduledNotificationCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=Scheduled+Notification&text=This+Is+a+test+notification&smsText=&voiceText=&subject=test+Notification&LocalHour=16&LocalMinute=45&sensorID=1234" target="_blank">https://<%:Request.Url.Host %>/xml/ScheduledNotificationCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=Scheduled+Notification&amp;text=This+Is+a+test+notification&amp;smsText=&amp;voiceText=&amp;subject=test+Notification&amp;LocalHour=16&amp;LocalMinute=45&amp;sensorID=1234</a>--%>

<input type="button" id="btn_TryAPI_ScheduledNotificationCreate" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_ScheduledNotificationCreate').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "ScheduledNotificationCreate",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "	Unique identifier of SubAccount. If omitted, Your default account will be used.", "optional": true },
                        { "name": "notificationID", "type": "Long", "description": "Unique identifier of the Notification. If included notification will be edited", "optional": true },					
                        { "name": "Name", "type": "String", "description": "Name of notification", "optional": false },
                        { "name": "Subject", "type": "String", "description": "Text for the subject of the notification", "optional": false },
                        { "name": "Text", "type": "String", "description": "Text for the body of the notification", "optional": false },
                        { "name": "smsText", "type": "String", "description": "Replacement Text for the body of a SMS notification. Default text value will be used if left blank", "optional": false },
                        { "name": "VoiceText", "type": "String", "description": "Replacement Text for a voice notification. Default text value will be used if left blank", "optional": false },
                        { "name": "PushMsgText", "type": "String", "description": "Replacement Text for a push message notification. Default text value will be used if left blank", "optional": false },
                        { "name": "LocalHour", "type": "Integer", "description": "Local hour to send notification ( 0 - 23 )", "optional": false },
                        { "name": "LocalMinute", "type": "Integer", "description": "Local minute to send notification ( 0 - 59 )", "optional": false },						
                        { "name": "SensorID or GatewayID", "type": "Long", "description": "	Unique identifier of the sensor or gateway. If included notification will return most recent reading", "optional": true }	
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
&nbsp;&nbsp;&lt;Method&gt;ScheduledNotificationCreate&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="519" Name="Scheduled test" Subject="test notification" Text="" SMSText="" VoiceText="" PushMsgText="" AccountID="1" Active="False" JointSnooze="True" AutoAcknowledge="True" Comparer="Equal" Theshold="16:45" Scale="" Snooze="0" LastDateSent="8/31/2016 7:35:03 PM" DatumType=""  AdvancedNotificationID="-9223372036854775808" NotificationClass="Timed" Status="Armed" AcknowledgedBy="" AcknowledgedByID="" AcknowledgedTime="1/1/0001 12:00:00 AM" AcknowledgeMethod="" ResetTime="1/1/0001 12:00:00 AM"  /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotification&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
