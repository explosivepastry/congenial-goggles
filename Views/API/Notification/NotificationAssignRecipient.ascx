<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationAssignRecipient</b><br />
    Asssigns a customer to receive a specific notification.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>UserID:</td>
            <td>Long</td>
            <td>Unique identifier of user to assign</td>
        </tr>
        <tr>
            <td>DelayMinutes:</td>
            <td>Integer</td>
            <td>Amount of Minutes after triggering event to alert customer.</td>
        </tr>
         <tr>
            <td>NotificationType:</td>
            <td>String</td>
            <td>Method of notification delivery:(email, sms, both, or phone)</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationAssignRecipient/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&userID=123&delayMinutes=5&notificationType=email" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationAssignRecipient/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;userID=123&amp;delayMinutes=5&amp;notificationType=email</a>--%>

<input type="button" id="btn_TryAPI_NotificationAssignRecipient" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationAssignRecipient').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationAssignRecipient",
					"params": [
                        { "name": "NotificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "UserID", "type": "Long", "description": "Unique identifier of user to assign", "optional": false },
                        { "name": "DelayMinutes", "type": "Integer", "description": "Amount of Minutes after triggering event to alert customer", "optional": false },					
						{ "name": "NotificationType", "type": "String", "description": "Method of notification delivery:(email, sms, both, or phone)", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;NotificationRemoveRecipient&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationRecipient&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationRecipient UserID="123" Type="Email" DelayMinutes="15" RecipientID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationRecipient&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>