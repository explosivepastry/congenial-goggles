<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationListAdvancedTypes</b><br />
     Lists all advanced notification types <br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of Account. If omitted,  Your default account will be used.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationListAdvancedTypes/Z3Vlc3Q6cGFzc3dvcmQ=?" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationListAdvancedTypes/Z3Vlc3Q6cGFzc3dvcmQ=?</a>--%>

<input type="button" id="btn_TryAPI_NotificationListAdvancedTypes" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationListAdvancedTypes').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationListAdvancedTypes",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "Unique identifier of Account.If omitted, Your default account will be used.", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;NotificationListAdvancedTypes&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotification AdvancedNotificationID="1" Name="Notify after aware period" AllowsSensors="True" AllowsGateways="False"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotification AdvancedNotificationID="2" Name="Back Online" AllowsSensors="True" AllowsGateways="False"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIAdvancedNotificationList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
