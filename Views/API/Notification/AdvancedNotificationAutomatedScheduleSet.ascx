<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: AdvancedNotificationAutomatedScheduleSet</b><br />
     Sets  process frequency for advanced notifications <br />
    
    <h4>Parameters</h4>
    <table>
          <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>processFrequency: </td>
            <td>Integer</td>
            <td>Time, in minutes, between processing. Example: every hour - 60 , every 6 hours - 360, once daily - 1440  </td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/AdvancedNotificationAutomatedScheduleSet/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=123&processFrequency=1440" target="_blank">https://<%:Request.Url.Host %>/xml/AdvancedNotificationAutomatedScheduleSet/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=123&amp;processFrequency=1440</a>--%>

<input type="button" id="btn_TryAPI_AdvancedNotificationAutomatedScheduleSet" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_AdvancedNotificationAutomatedScheduleSet').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "AdvancedNotificationAutomatedScheduleSet",
					"params": [
                        { "name": "NotificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "processFrequency", "type": "Integer", "description": "Time, in minutes, between processing. Example: every hour - 60 , every 6 hours - 360, once daily - 1440", "optional": false }					
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
&nbsp;&nbsp;&lt;Method&gt;AdvancedNotificationAutomatedScheduleSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationAutomatedSchedule NotificationID="123" AdvancedNotificationID="14" ProcessFrequency="1440" Description=" Low Temperature" LastProcessDate="10/10/2016 2:55:01 PM"/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
