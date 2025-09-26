<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebhookNotificationSettingsSet</b><br />
    Allows you to set when and to whom emails are sent when your Data Push fails multiple times in a row<br />
    
    <h4>Parameters</h4>
    <table>
          <%--<%if (ViewBag.ShowResellerParameters == true)
            {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} %>--%>
       <tr>
            <td>UserIDToBeNotified: </td>
            <td>long </td>
            <td>Unique identifier of user that will recieve the broken count notification.</td>
        </tr>
         <tr>
            <td>brokenCountThreshold: </td>
            <td>Integer</td>
            <td>The number of failed attempts before a notification is sent.</td>
        </tr>
       
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/WebhookNotificationSettingsSet/Z3Vlc3Q6cGFzc3dvcmQ=?UserIDToBeNotfied=123&brokenCountThreshold=10" target="_blank">https://<%:Request.Url.Host %>/xml/WebhookNotificationSettingsSet/Z3Vlc3Q6cGFzc3dvcmQ=?UserIDToBeNotfied=123&amp;brokenCountThreshold=10</a>--%>

<input type="button" id="btn_TryAPI_WebhookNotificationSettingsSet" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebhookNotificationSettingsSet').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebhookNotificationSettingsSet",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true },
                        { "name": "UserIDToBeNotified", "type": "long", "description": "Unique identifier of user that will recieve the broken count notification.", "optional": false },
                        { "name": "brokenCountThreshold", "type": "Integer", "description": "The number of failed attempts before a notification is sent.", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;WebhookNotificationSettingsSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&lt;APINotificationSettings UserToBeNotified="John Doe" UsersBrokenCountLimit="10" LastEmailDate="10/20/2016 8:57:31 PM" LargestBrokenCount="Webhook 18"/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>