<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationRemoveGateway</b><br />
    Removes a gateway from a specific notification.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>GatewayID:</td>
            <td>Long</td>
            <td>Identifier of gateway to Remove</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationRemoveGateway/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&gatewayID=123" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationRemoveGateway/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;gatewayID=123</a>--%>

<input type="button" id="btn_TryAPI_NotificationRemoveGateway" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationRemoveGateway').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationRemoveGateway",
					"params": [
                        { "name": "NotificationID", "type": "Long", "description": "Unique identifier of the Notification", "optional": false },
                        { "name": "GatewayID", "type": "Long", "description": "Identifier of gateway to Remove", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;NotificationRemoveGateway&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>