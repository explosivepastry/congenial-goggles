<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: RecentResellerNotification</b><br />
    Returns recently generated notifications for all sub-accounts <br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>minutes:</td>
            <td>Integer</td>
            <td>Number of minutes past Notifications will be returned</td>
        </tr>
         <tr>
            <td>lastSentNotificationID</td>
            <td>Integer (optional)</td>
            <td>Only returns Notifications received after this notification ID</td>
        </tr>
    </table>
   
    <h4>Example</h4>
    <%--<a href="/xml/RecentResellerNotification/Z3Vlc3Q6cGFzc3dvcmQ=?minutes=10&lastSentNotificationID=0" target="_blank">https://<%:Request.Url.Host %>/xml/RecentResellerNotification/Z3Vlc3Q6cGFzc3dvcmQ=?minutes=10&lastSentNotificationID=0</a>--%>
    <input type="button" id="btn_TryAPI_RecentResellerNotification" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_RecentResellerNotification').click(function () {
				var json =
				{
					"auth": true,					
					"api": "RecentResellerNotification",
					"params": [
                        { "name": "minutes", "type": "Integer", "description": "Number of minutes past Notifications will be returned", "optional": false },
						{ "name": "lastSentNotificationID", "type": "Integer", "description": "Only returns Notifications received after this notification ID", "optional": true }	
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
&nbsp;&nbsp;&lt;Method&gt;RecentResellerNotification&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISentNotificationList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISentNotification SentNotificationID="554" NotificationID="228" UserID="10" SensorID="101" GatewayID="0" Text="&lt;p&gt;Temperature is over 100 degrees Farh.&lt;/p&gt;" Content="Temperature is over 100 degrees Farh." Name="Temp - 101 Over 100" Delivered="Email Sent" NotificationDate="1/1/2011 6:36:00 PM" NotificationStatus="Alarming"  AcknowledgedBy="" AcknowledgedByID="-1" AcknowledgedTime="1/1/0001 12:00:00 AM" AcknowledgeMethod="" ResetTime="1/1/0001 12:00:00 AM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISentNotification SentNotificationID="556" NotificationID="229" UserID="10" SensorID="101" GatewayID="0" Text="&lt;p&gt;Temperature is under 32 degrees Farh.&lt;/p&gt;" Content="Temperature is over 100 degrees Farh." Name="Temp - 101 Under 32" Delivered="Email Sent" NotificationDate="1/1/2011 6:46:00 PM" NotificationStatus="Armed"  AcknowledgedBy="" AcknowledgedByID="" AcknowledgedTime="1/1/0001 12:00:00 AM" AcknowledgeMethod="" ResetTime="1/1/0001 12:00:00 AM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISentNotification SentNotificationID="560" NotificationID="230" UserID="10" SensorID="101" GatewayID="0" Text="&lt;p&gt;Temperature is over 10 degrees Farh.&lt;/p&gt;" Content="Temperature is over 10 degrees Farh." Name="Temp - 101 Over 10" Delivered="Email Sent" NotificationDate="1/1/2011 6:36:00 PM" NotificationStatus="Acknowledged" AcknowledgedBy="John Doe" AcknowledgedByID="1234" AcknowledgedTime="12/14/2018 6:17:17 PM" AcknowledgeMethod="Browser_MainList" ResetTime="12/14/2018 6:17:29 PM"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISentNotification SentNotificationID="561" NotificationID="239" UserID="10" SensorID="101" GatewayID="0" Text="&lt;p&gt;Temperature is under 12 degrees Farh.&lt;/p&gt;" Content="Temperature is over 12 degrees Farh." Name="Temp - 101 Under 12" Delivered="Email Sent" NotificationDate="1/1/2011 6:46:00 PM" NotificationStatus="Not Active" AcknowledgedBy="System_Auto" AcknowledgedByID="0" AcknowledgedTime="9/24/2018 6:17:15 PM" AcknowledgeMethod="System_Auto" ResetTime="9/24/2018 6:17:15 PM" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APISentNotificationList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>