<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationAssignSystemAction</b><br />
    Asssigns a system action to a specific notification.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>ActionType:</td>
            <td>String</td>
            <td>Method of system action:(Acknowledge  Reset  Activate  Deactivate)</td>
        </tr>
        <tr>
            <td>TargetNotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the target notification</td>
        </tr>
        <tr>
            <td>DelayMinutes:</td>
            <td>Integer</td>
            <td>Amount of Minutes before triggering system action </td>
        </tr>     
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationAssignSystemAction/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&actionType=Reset&TargetNotificationID=456&delayMinutes=5" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationAssignSystemAction/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;&actionType=Reset&amp;TargetNotificationID=456&amp;delayMinutes=5</a>--%>

<input type="button" id="btn_TryAPI_NotificationAssignSystemAction" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationAssignSystemAction').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationAssignSystemAction",
					"params": [
                        { "name": "NotificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "ActionType", "type": "String", "description": "Method of system action:(Acknowledge Reset Activate Deactivate)", "optional": false },
                        { "name": "TargetNotificationID", "type": "Long", "description": "Unique identifier of the target notification", "optional": false },					
						{ "name": "DelayMinutes", "type": "Integer", "description": "Amount of Minutes before triggering system action", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;NotificationAssignSystemAction&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationSystemAction SystemActionRecipientID="1554" ActionType="Reset" TargetNotificationID="456" DelayMinutes="5" /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>