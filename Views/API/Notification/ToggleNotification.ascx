<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: ToggleNotification()</b><br />
     sets the Notification active or inactive<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>on:</td>
            <td>Bool</td>
            <td>on flag that sets the notification on or off based off its value</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/ToggleNotification/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=250&on=true" target="_blank">https://<%:Request.Url.Host %>/xml/ToggleNotification/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=250&on=true</a>--%>

<input type="button" id="btn_TryAPI_ToggleNotification" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_ToggleNotification').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "ToggleNotification",
					"params": [
                        { "name": "notificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "on", "type": "Bool", "description": ">on flag that sets the notification on or off based off its value", "optional": false }					
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
&nbsp;&nbsp;&lt;Method&gt;ToggleNotification&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
