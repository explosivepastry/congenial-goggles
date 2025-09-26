<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: RecentlySentNotifications</b><br />
    Returns data points recorded in a range of time (limited to a 12 hour window)<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>minutes:</td>
            <td>Integer</td>
            <td>Number of minutes past Notifications will be returned</td>
        </tr>
    </table>
    <h4>Optional Parameters</h4>
    <table>
        <tr>
            <td>LastSentNotificationID</td>
            <td>Long</td>
            <td>Limits notification results to notifications sent after this ID</td>
        </tr>
        <tr>
            <td>SensorID</td>
            <td>Long</td>
            <td>Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/RecentlySentNotifications/Z3Vlc3Q6cGFzc3dvcmQ=?minutes=10&lastSentNotificationID=0&sensorID=0" target="_blank">https://<%=Request.Url.Host %>/xml/RecentlySentNotifications/Z3Vlc3Q6cGFzc3dvcmQ=?minutes=10&lastSentNotificationID=0&sensorID=0</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;RecentlySentNotifications&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISentNotificationList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISentNotification SentNotificationID="554" NotificationID="228" UserID="10" SensorID="101" GatewayID="0" Text="Temperature is over 100 degrees Farh." Name="Temp - 101 Over 100" Delivered="Email Sent" NotificationDate="1/1/2011 6:36:00 PM"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISentNotification SentNotificationID="556" NotificationID="229" UserID="10" SensorID="101" GatewayID="0" Text="Temperature is under 32 degrees Farh." Name="Temp - 101 Under 32" Delivered="Email Sent" NotificationDate="1/1/2011 6:46:00 PM/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APISentNotificationList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
