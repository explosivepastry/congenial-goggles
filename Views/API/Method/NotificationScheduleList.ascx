<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationScheduleList</b><br />
    Returns a list of Schedules for a specific notificaiton<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationID:</td>
            <td>Long</td>
            <td>Brings back a list of all the daily schedules for the specific notification</td>
        </tr>
    </table>

    <h4>Optional Parameters</h4>
    <table>
        <tr>
            <td>day:</td>
            <td>string</td>
            <td>Brings back a single day's schedule</td>
        </tr>
    </table>
    
    
    <h4>Example</h4>
    <a href="/xml/NotificationScheduleList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=101&day=Monday" target="_blank">https://<%=Request.Url.Host %>/xml/NotificationScheduleList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=101&day=Monday</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationScheduleList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationScheduleList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationSchedule DayOfWeek="Monday" FirstEnteredTime="00:00:00" SecondEnteredTime="00:00:00" NotificationSchedule="All_Day"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationScheduleList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

