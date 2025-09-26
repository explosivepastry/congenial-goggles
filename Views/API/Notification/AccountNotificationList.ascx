<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountNotificationList</b><br />
    Return all notifications on the specified account<br />
    
    <h4>Parameters</h4>
    <table>
         <%--<%if (ViewBag.ShowResellerParameters == true)
           {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} else { %>
         <tr>
          <td>None</td>
        </tr>
        <%} %>--%>
    </table>
    
    
    <h4>Example</h4>
<%--    <a href="/xml/AccountNotificationList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101" target="_blank">https://<%:Request.Url.Host %>/xml/AccountNotificationList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101</a>--%>

<input type="button" id="btn_TryAPI_AccountNotificationList" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_AccountNotificationList').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "AccountNotificationList",
					"params": [
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
&nbsp;&nbsp;&lt;Method&gt;AccountNotificationList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="519" Name="inactivity test" Subject="inactivity test" Text="" SMSText="" VoiceText="" PushMsgText="" AccountID="1" Active="False" JointSnooze="True" AutoAcknowledge="True" Comparer="Greater_Than" Theshold="3" Scale="" Snooze="0" LastDateSent="8/31/2016 7:35:03 PM" DatumType=""  AdvancedNotificationID="-9223372036854775808" NotificationClass="Inactivity" Status="Armed"  AcknowledgedBy="" AcknowledgedByID="" AcknowledgedTime="1/1/0001 12:00:00 AM" AcknowledgeMethod="" ResetTime="1/1/0001 12:00:00 AM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="457" Name="test1" Subject="test1" Text="test1" SMSText="" VoiceText="" PushMsgText="" AccountID="1" Active="True" JointSnooze="True" AutoAcknowledge="False" Comparer="Less_Than" Theshold="95" Scale="" Snooze="60" LastDateSent="1/1/0001 12:00:00 AM" DatumType=""  AdvancedNotificationID="-9223372036854775808" NotificationClass="Low_Battery" Status="Alarming" AcknowledgedBy="" AcknowledgedByID="-1" AcknowledgedTime="12/14/2018 6:17:17 PM" AcknowledgeMethod="Browser_MainList" ResetTime="12/14/2018 6:17:29 PM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="458" Name="test2" Subject="test2" Text="test2" SMSText="" VoiceText="" PushMsgText="" AccountID="1" Active="True" JointSnooze="True" AutoAcknowledge="False" Comparer="Less_Than" Theshold="95" Scale="" Snooze="60" LastDateSent="1/1/0001 12:00:00 AM" DatumType=""  AdvancedNotificationID="-9223372036854775808" NotificationClass="Application" Status="Acknowledged" AcknowledgedBy="John Doe" AcknowledgedByID="1234" AcknowledgedTime="9/24/2018 6:17:15 PM" AcknowledgeMethod="System_Auto" ResetTime="9/24/2018 6:17:15 PM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="459" Name="test3" Subject="test3" Text="test3" SMSText="" VoiceText="" PushMsgText="" AccountID="1" Active="False" JointSnooze="True" AutoAcknowledge="False" Comparer="Less_Than" Theshold="95" Scale="" Snooze="60" LastDateSent="1/1/0001 12:00:00 AM" DatumType=""  AdvancedNotificationID="-9223372036854775808" NotificationClass="Application" Status="Not Active" AcknowledgedBy="System_Auto" AcknowledgedByID="0" AcknowledgedTime="9/24/2018 6:17:15 PM" AcknowledgeMethod="System_Auto" ResetTime="9/24/2018 6:17:15 PM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

