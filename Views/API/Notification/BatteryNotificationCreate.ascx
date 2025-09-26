<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: BatteryNotificationCreate</b><br />
    Create or edit a Low-Battery notification.<br />

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
            <td>Threshold:</td>
            <td>String</td>
            <td>Desired battery percentage to notify when below ( 0-100 )</td>
        </tr>
        <tr>
            <td>Snooze:</td>
            <td>Integer</td>
            <td>Duration of snooze in minutes. ( 0 - 65535 )</td>
        </tr>

        <tr>
            <td>JointSnooze:</td>
            <td>Boolean</td>
            <td>Sets the system to snooze all sensors attached to notification  </td>
        </tr>
        <tr>
            <td>AutoAcknowledge:</td>
            <td>Boolean</td>
            <td>Allows the system to acknowlege notifications that return to within its threshold </td>
        </tr>
    </table>

    <h4>Example</h4>
<%--    <a href="/xml/BatteryNotificationCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=Low_battery+Notification&text=This+Is+a+test+notification&smsText=&voiceText=&subject=test+Notification&Threshold=15&snooze=120&autoAcknowledge=true&jointSnooze=true" target="_blank">https://<%:Request.Url.Host %>/xml/BatteryNotificationCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=Low_Battery+Notification&amp;text=This+Is+a+test+notification&amp;smsText=&amp;voiceText=&amp;subject=test+Notification&amp;threshold=15
        &amp;snooze=120&amp;autoAcknowledge=true&amp;jointSnooze=true</a>--%>

<input type="button" id="btn_TryAPI_BatteryNotificationCreate" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_BatteryNotificationCreate').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "BatteryNotificationCreate",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "	Unique identifier of SubAccount. If omitted, Your default account will be used.", "optional": true },
                        { "name": "notificationID", "type": "Long", "description": "Unique identifier of the Notification. If included notification will be edited", "optional": true },					
                        { "name": "Name", "type": "String", "description": "Name of notification", "optional": false },
                        { "name": "Subject", "type": "String", "description": "Text for the subject of the notification", "optional": false },
                        { "name": "Text", "type": "String", "description": "Text for the body of the notification", "optional": false },
                        { "name": "smsText", "type": "String", "description": "Replacement Text for the body of a SMS notification. Default text value will be used if left blank", "optional": false },
                        { "name": "VoiceText", "type": "String", "description": "Replacement Text for a voice notification. Default text value will be used if left blank", "optional": false },
                        { "name": "PushMsgText", "type": "String", "description": "Replacement Text for a push message notification. Default text value will be used if left blank", "optional": false },
                        { "name": "Threshold", "type": "String", "description": "Desired battery percentage to notify when below ( 0-100 )", "optional": false },
                        { "name": "Snooze", "type": "Integer", "description": "Duration of snooze in minutes. 0 - 65535", "optional": false },						
                        { "name": "JointSnooze", "type": "Boolean", "description": "Sets the system to snooze all sensors attached to notification", "optional": false },
                        { "name": "AutoAcknowledge", "type": "Boolean", "description": "Allows the system to acknowlege notifications that return to within its threshold", "optional": false }	
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
&nbsp;&nbsp;&lt;Method&gt;BatteryNotificationCreate&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="519" Name="Low_Battery test" Subject="test notification" Text="" SMSText="" VoiceText="" PushMsgText="" AccountID="1" Active="False" JointSnooze="True" AutoAcknowledge="True" Comparer="Equal" Theshold="15" Scale="" Snooze="120" LastDateSent="8/31/2016 7:35:03 PM" DatumType=""  AdvancedNotificationID="-9223372036854775808" NotificationClass="Low_Battery" Status="Armed" AcknowledgedBy="" AcknowledgedTime="1/1/0001 12:00:00 AM" AcknowledgeMethod="" AcknowledgedByID="" ResetTime="1/1/0001 12:00:00 AM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotification&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
