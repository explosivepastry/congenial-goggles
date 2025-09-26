<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationYearlyScheduleList</b><br />
    Get the year schedule for a notification<br />
    
    <h4>Parameters</h4>
    <table>
         <tr>
            <td>notificationID:</td>
            <td>Integer</td>
            <td>Unique identifier of the Notification.</td>
        </tr>
    </table>
   
    <h4>Example</h4>
<%--    <a href="/xml/NotificationYearlyScheduleList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=1" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationYearlyScheduleList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=1</a>--%>

<input type="button" id="btn_TryAPI_NotificationYearlyScheduleList" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationYearlyScheduleList').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationYearlyScheduleList",
					"params": [
                        { "name": "notificationID", "type": "Integer", "description": "Unique identifier of the Notification", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;NotificationYearlyScheduleList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearScheduleList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearSchedule Month="1" Day="1" SendNotifications="False"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearSchedule Month="1" Day="2" SendNotifications="False"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearSchedule Month="1" Day="3" SendNotifications="True"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearSchedule Month="1" Day="4" SendNotifications="True"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearSchedule Month="1" Day="5" SendNotifications="False"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearSchedule Month="1" Day="6" SendNotifications="True"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearSchedule Month="1" Day="8" SendNotifications="False"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;...
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationYearSchedule Month="12" Day="31" SendNotifications="False"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationYearScheduleList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>

