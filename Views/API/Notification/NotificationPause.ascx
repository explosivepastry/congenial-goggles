<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationPause</b><br />
    Pauses a sensor or gateway from activating a notification <br />
    

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>DeviceID:</td>
            <td>Long</td>
            <td>Unique identifier of Sensor or Gateway to assign</td>
        </tr>
        <tr>
            <td>DeviceType:</td>
            <td>String</td>
            <td>Type of device:(Sensor  Gateway)</td>
        </tr>
        <tr>
            <td>Days:</td>
            <td>Integer</td>
            <td>Number of days from now to resume activiting notification </td>
        </tr>
        <tr>
            <td>Hours:</td>
            <td>Integer</td>
            <td>Number of hours from now to resume activiting notification </td>
        </tr>
        <tr>
            <td>Minutes:</td>
            <td>Integer</td>
            <td>Number of minutes from now to resume activiting notification </td>
        </tr>        
    </table>

    <b>*Special Case: Enter 0 for days, hours, and minutes to pause indefinitely*</b><br />


    <h4>Example</h4>
<%--    <a href="/xml/NotificationPause/Z3Vlc3Q6cGFzc3dvcmQ=?deviceID=123&deviceType=sensor&days=1&hours=12&minutes=0" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationPause/Z3Vlc3Q6cGFzc3dvcmQ=?deviceID=123&amp;deviceType=sensor&amp;days=1&amp;hours=12&amp;minutes=0</a>--%>

<input type="button" id="btn_TryAPI_NotificationPause" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationPause').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationPause",
					"params": [
                        { "name": "DeviceID", "type": "Long", "description": "Unique identifier of Sensor or Gateway to assign", "optional": false },
                        { "name": "DeviceType", "type": "String", "description": "Type of device:(Sensor  Gateway)", "optional": false },					
                        { "name": "Days", "type": "Integer", "description": "Number of days from now to resume activiting notification", "optional": false },
                        { "name": "Hours", "type": "Integer", "description": "Number of hours from now to resume activiting notification", "optional": false },
                        { "name": "Minutes", "type": "Integer", "description": "Number of minutes from now to resume activiting notification", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
    </script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationPause&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationPause  DeviceID="123" DeviceType="sensor" ResumeNotifyDate="9/30/2016 12:44:22 PM" /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
