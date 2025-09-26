<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationDailyScheduleSet</b><br />
    Sets the weekly notification schedule by day<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationID:</td>
            <td>Integer</td>
            <td>Unique identifier of the Notification.</td>
        </tr>
        <tr>
            <td>day:</td>
            <td>String</td>
            <td>Day of the Week</td>
        </tr>
        <tr>
            <td>notificationSchedule:</td>
            <td>String:</td>
            <td>Off, All_Day, After, Before, Before_and_After, Between</td>
        </tr>
        <tr>
            <td>firstEnteredTime:</td>
            <td>Timespan</td>
            <td>Time of day designation</td>
        </tr>
         <tr>
            <td>secondEnteredTime:</td>
            <td>Timespan</td>
            <td>Time of day designation</td>
        </tr>
    </table>

  
    <h4>Example</h4>
<%--    <a href="/xml/NotificationDailyScheduleSet/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=101&day=Friday&notificationSchedule=Between&firstEnteredTime=07:30:00&secondEnteredTime=18:00:00" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationDailyScheduleSet/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=101&amp;day=Friday&amp;notificationSchedule=Between&amp;firstEnteredTime=07:30:00&amp;secondEnteredTime=18:00:00</a>--%>

<input type="button" id="btn_TryAPI_NotificationDailyScheduleSet" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationDailyScheduleSet').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationDailyScheduleSet",
					"params": [
                        { "name": "notificationID", "type": "Integer", "description": "Unique identifier of the notification", "optional": false },
                        { "name": "day", "type": "String", "description": "Day of the Week", "optional": false },
                        { "name": "notificationSchedule", "type": "String", "description": "Off, All_Day, After, Before, Before_and_After, Between", "optional": false },					
                        { "name": "firstEnteredTime", "type": "Timespan", "description": "Time of day designation", "optional": false },					
                        { "name": "secondEnteredTime", "type": "Timespan", "description": "Time of day designation", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;NotificationDailyScheduleSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIUserNotificationScheduleList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationWeeklySchedule DayOfWeek="Friday" FirstEnteredTime="07:30:00" SecondEnteredTime="18:00:00" NotificationSchedule="Between"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIUserNotificationScheduleList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

