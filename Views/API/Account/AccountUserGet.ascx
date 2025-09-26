<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
	<b>Method: AccountUserGet</b><br />
	Returns the user object.<br />

	<h4>Parameters</h4>
	<table>
		<tr>
			<td>userID:</td>
			<td>Integer</td>
			<td>Unique identifier of the user</td>
		</tr>
	</table>

	<h4>Example</h4>
	<%--<a href="/xml/AccountUserGet/Z3Vlc3Q6cGFzc3dvcmQ=?userID=1" target="_blank">https://<%:Request.Url.Host %>/xml/AccountUserGet/Z3Vlc3Q6cGFzc3dvcmQ=?userID=1</a>--%>
	<input type="button" id="btn_TryAPI_AccountUserGet" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_AccountUserGet').click(function () {
				var json =
				{
					"auth": true,					
					"api": "AccountUserGet",
					"params": [
						{ "name": "userID", "type": "Integer", "description": "Unique identifier of the user", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;AccountUserGet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&lt;APIUser UserID="1" Title="Mr" FirstName="John" LastName="Dough" EmailAddress="Invalid@invlaid.com" SMSNumber="+1 555-555-555" VoiceNumber="+1 555-555-555" DirectSMS="False" ReceivesNotificationsBySMS="False" ReceivesNotificationsByVoice="True" Active="True" Admin="True" ReceivesMaintenanceByEmail="True" ReceivesMaintenanceBySMS="False" ExternalSMSProviderID="1" UserName="JohnDoughUser" AccountID="123"/>&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
