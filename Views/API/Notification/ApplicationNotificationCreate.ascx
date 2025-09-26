<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: ApplicationNotificationCreate</b><br />
     Create or edit a notification.<br />
    
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
            <td>DatumType:</td>
            <td>string</td>
            <td>unique identifier of the datum type ( TemperatureData  or  WaterDetect  ) </td>
        </tr>
           <tr>
            <td>Comparer:</td>
            <td>String</td>
            <td>Choice for how to compare: Temperature-( Equal  Greater_Than  Less_Than ) <br /> Water Detect-( Equal ) </td>
        </tr>
         <tr>
            <td>threshold:</td>
            <td>String</td>
            <td>Temperature (Desired Temperature Integer) Water Detect(water detected = True   water not detected = False) </td>
        </tr>
         <tr>
            <td>Scale:</td>
            <td>String</td>
            <td>Scale of measurement ( C  or  F )  Water Detect(Leave blank)</td>
        </tr>
         <tr>
            <td>Snooze:</td>
            <td>Integer</td>
            <td>Duration of snooze in minutes. 0 - 65535 </td>
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
<%--    <a href="/xml/ApplicationNotificationCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=Temp+Notification&text=This+Is+a+test+notification&smsText=&voiceText=&subject=test+Notification&comparer=Greater_Than&threshold=60&scale=F&datumType=TemperatureData&snooze=60&autoAcknowledge=true&jointSnooze=false" target="_blank">https://<%:Request.Url.Host %>/xml/ApplicationNotificationCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=Temp+Notification&amp;text=This+Is+a+test+notification&amp;smsText=&amp;voiceText=&amp;
        subject=test+Notification&amp;comparer=Greater_Than&amp;threshold=60&amp;scale=F&amp;datumType=TemperatureData&amp;snooze=60&amp;autoAcknowledge=true&amp;jointSnooze=false</a>

    <h4>Water Detect Example</h4>
    <a href="/xml/ApplicationNotificationCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=Water+Notification&text=This+Is+a+test+notification&smsText=&voiceText=&subject=test+Notification&comparer=Equal&threshold=True&scale=&datumType=WaterDetect&snooze=60&autoAcknowledge=true&jointSnooze=true" target="_blank">https://<%:Request.Url.Host %>/xml/ApplicationNotificationCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=Water+Notification&amp;text=This+Is+a+test+notification&amp;smsText=&amp;voiceText=&amp;
        subject=test+Notification&amp;comparer=Equal&amp;threshold=True&amp;scale=&amp;datumType=WaterDetect&amp;snooze=60&amp;autoAcknowledge=true&amp;jointSnooze=true</a>--%>

<input type="button" id="btn_TryAPI_ApplicationNotificationCreate" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_ApplicationNotificationCreate').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "ApplicationNotificationCreate",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "	Unique identifier of SubAccount. If omitted, Your default account will be used.", "optional": true },
                        { "name": "notificationID", "type": "Long", "description": "Unique identifier of the Notification. If included notification will be edited", "optional": true },					
                        { "name": "Name", "type": "String", "description": "Name of notification", "optional": false },
                        { "name": "Subject", "type": "String", "description": "Text for the subject of the notification", "optional": false },
                        { "name": "Text", "type": "String", "description": "Text for the body of the notification", "optional": false },
                        { "name": "smsText", "type": "String", "description": "Replacement Text for the body of a SMS notification. Default text value will be used if left blank", "optional": false },
                        { "name": "VoiceText", "type": "String", "description": "Replacement Text for a voice notification. Default text value will be used if left blank", "optional": false },
                        { "name": "PushMsgText", "type": "String", "description": "Replacement Text for a push message notification. Default text value will be used if left blank", "optional": false },
                        { "name": "DatumType", "type": "string", "description": "Unique identifier of the datum type (TemperatureData or WaterDetect)", "optional": false },
                        { "name": "Comparer", "type": "String", "description": "Choice for how to compare: Temperature-( Equal  Greater_Than  Less_Than ) Water Detect-( Equal ) ", "optional": false },						
                        { "name": "threshold", "type": "String", "description": "Temperature (Desired Temperature Integer) Water Detect(water detected = True water not detected = False)", "optional": false },
                        { "name": "Scale", "type": "String", "description": "Scale of measurement ( C or F ) Water Detect(Leave blank)", "optional": false },
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
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;ApplicationNotificationCreate&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="519" Name="Temp test" Subject="Temperature test" Text="" SMSText="" VoiceText="" PushMsgText="" AccountID="1" Active="False" JointSnooze="True" AutoAcknowledge="True" Comparer="Greater_Than" Theshold="3" Scale="" Snooze="0" LastDateSent="8/31/2016 7:35:03 PM" DatumType="TemperatureData"  AdvancedNotificationID="-9223372036854775808" NotificationClass="Application" Status="Armed"AcknowledgedBy="" AcknowledgedByID="" AcknowledgedTime="1/1/0001 12:00:00 AM" AcknowledgeMethod="" ResetTime="1/1/0001 12:00:00 AM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotification&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
