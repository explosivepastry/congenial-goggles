<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountNotificationList</b><br />
    Returns all notificaiton on the specified account<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID:</td>
            <td>Long</td>
            <td>Brings back a list of all notifications on a specific account</td>
        </tr>
    </table>
    
    
    <h4>Example</h4>
    <a href="/xml/AccountNotificationList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101" target="_blank">https://<%=Request.Url.Host %>/xml/AccountNotificationList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;AccountNotificationList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="2730" Name="inactivity" Text="Has missed 2 heartbeats" Active="True" Comparer="Greater_Than" Theshold="120" Snooze="60" LastDateSent="1/1/0001 12:00:00 AM"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotification NotificationID="2731" Name="lowBattery" Text="battery is below 15%" Active="True" Comparer="Less_Than" Theshold="0" Snooze="60" LastDateSent="1/1/0001 12:00:00 AM"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

