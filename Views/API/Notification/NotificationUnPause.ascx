<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationUnpause</b><br />
    UnPauses a sensor or gateway from activating a notification <br />
    

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
    </table>

 


    <h4>Example</h4>
<%--    <a href="/xml/NotificationUnpause/Z3Vlc3Q6cGFzc3dvcmQ=?deviceID=123&deviceType=sensor" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationUnpause/Z3Vlc3Q6cGFzc3dvcmQ=?deviceID=123&amp;deviceType=sensor</a>--%>

<input type="button" id="btn_TryAPI_NotificationUnpause" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationUnpause').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationUnpause",
					"params": [
                        { "name": "DeviceID", "type": "Long", "description": "Unique identifier of Sensor or Gateway to assign", "optional": false },
						{ "name": "DeviceType", "type": "String", "description": "Type of device:(Sensor Gateway)", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;NotificationUnpause&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationPause DeviceID="123" DeviceType="sensor" ResumeNotifyDate="9/30/2016 12:44:22 PM" /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
