<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationYearlyScheduleSet</b><br />
    Set the yearly schedule for a notification. Selected days will receive notifications.<br />
    
    <h4>Parameters</h4>
    <table>
         <tr>
            <td>notificationID:</td>
            <td>Integer</td>
            <td>Unique identifier of the Notification.</td>
        </tr>
         <tr>
            <td>Month:</td>
            <td>Integer</td>
            <td>Numeric value of the month of the selection ( January = 1 ... December = 12)</td>
        </tr>
         <tr>
            <td>Days:</td>
            <td>Integer</td>
            <td>Numeric values of the days to NOT receive notifications. Example ( days=1|29|30|31 )</td>
        </tr>
    </table>

     <b>*Special Case: Enter 0 for Month and Days to delete entire schedule*</b><br />
   
    <h4>Example</h4>
<%--    <a href="/xml/NotificationYearlyScheduleSet/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=1&month=6&days=16|17|18|19|25" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationYearlyScheduleSet/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=1&amp;month=6&amp;days=16|17|18|19|25</a>--%>

<input type="button" id="btn_TryAPI_NotificationYearlyScheduleSet" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationYearlyScheduleSet').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationYearlyScheduleSet",
					"params": [
                        { "name": "notificationID", "type": "Integer", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "Month", "type": "Integer", "description": "Numeric value of the month of the selection(January = 1 ...December = 12)", "optional": false },
                        { "name": "Days", "type": "Integer", "description": "Numeric values of the days to NOT receive notifications. Example ( days=1|29|30|31 )", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;NotificationYearlyScheduleSet&lt;/Method&gt;
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
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationYearScheduleList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
<h4>*Example Output*</h4>
 <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationYearlyScheduleSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Notification Schedule Deleted&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>


</div>

