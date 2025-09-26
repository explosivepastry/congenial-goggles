<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: AdvancedNotificationParameterList</b><br />
     Lists parameters for an  advanced notification <br />
    
    <h4>Parameters</h4>
    <table>
          <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/AdvancedNotificationParameterList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=123" target="_blank">https://<%:Request.Url.Host %>/xml/AdvancedNotificationParameterList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=123</a>--%>

<input type="button" id="btn_TryAPI_AdvancedNotificationParameterList" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_AdvancedNotificationParameterList').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "AdvancedNotificationParameterList",
					"params": [
                        { "name": "NotificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;AdvancedNotificationParameterList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterValueList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterValue  NotificationID="123" AdvancedNotificationParameterID="9" ParameterValue="Not Aware" ParameterDescription="Trigger_When"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterValue  NotificationID="123" AdvancedNotificationParameterID="10" ParameterValue="65" ParameterDescription="Percentage"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterValue  NotificationID="123" AdvancedNotificationParameterID="11" ParameterValue="" ParameterDescription="Key_Sensor"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIAdvancedNotificationParameterValueList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
