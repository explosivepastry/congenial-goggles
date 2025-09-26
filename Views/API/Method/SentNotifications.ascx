<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SentNotifications</b><br />
    Returns data points recorded in a range of time (limited to 7 days and 5000 records)<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>from</td>
            <td>DateTime</td>
            <td>from is the starting point of the time period for your date range of notifications sent</td>
        </tr>
        <tr>
            <td>to</td>
            <td>DateTime</td>
            <td>to is the ending point of the time period for your date range for notifications sent</td>
        </tr>
    </table>
    <h4>Optional Parameters</h4>
    <table>
        <tr>
            <td>SensorID</td>
            <td>Long</td>
            <td>Limits which sensor notifications will come back. If this field is left null it will bring back all notifications for all sensors.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SentNotifications/Z3Vlc3Q6cGFzc3dvcmQ=?from=9/29/2014+5:59:19+AM&to=9/29/2014+5:59:19+PM&sensorID=0" target="_blank">https://<%=Request.Url.Host %>/xml/SentNotifications/Z3Vlc3Q6cGFzc3dvcmQ=?from=9/29/2014+5:59:19+AM&to=9/29/2014+5:59:19+PM&sensorID=0</a>
                
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
