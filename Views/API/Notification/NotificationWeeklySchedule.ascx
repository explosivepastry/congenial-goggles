<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationWeeklySchedule</b><br />
    Get the Week schedule for a notification<br />
    
    <h4>Parameters</h4>
    <table>
         <tr>
            <td>notificationID:</td>
            <td>Integer</td>
            <td>Unique identifier of the Notification.</td>
        </tr>
    </table>
   
    <h4>Example</h4>
    <a href="/xml/NotificationWeeklySchedule/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=1" target="_blank">https://<%=Request.Url.Host %>/xml/NotificationWeeklySchedule/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=1</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationWeeklySchedule&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklyScheduleList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklySchedule DayOfWeek="Monday" FirstEnteredTime="09:30:00" SecondEnteredTime="00:00:00" NotificationSchedule="Between"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklySchedule DayOfWeek="Tuesday" FirstEnteredTime="00:00:00" SecondEnteredTime="00:00:00" NotificationSchedule="Off"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklySchedule DayOfWeek="Wednesday" FirstEnteredTime="00:00:00" SecondEnteredTime="00:00:00" NotificationSchedule="Before"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklySchedule DayOfWeek="Thursday" FirstEnteredTime="00:00:00" SecondEnteredTime="00:00:00" NotificationSchedule="Before_and_After"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklySchedule DayOfWeek="Friday" FirstEnteredTime="00:00:00" SecondEnteredTime="22:00:00" NotificationSchedule="Before_and_After"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklySchedule DayOfWeek="Saturday" FirstEnteredTime="00:00:00" SecondEnteredTime="00:00:00" NotificationSchedule="All_Day"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklySchedule DayOfWeek="Sunday" FirstEnteredTime="00:00:00" SecondEnteredTime="09:00:00" NotificationSchedule="After"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationWeeklyScheduleList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

