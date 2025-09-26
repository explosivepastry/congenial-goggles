<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: CreateAccountUser</b><br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>UserName:</td>
            <td>string</td>
            <td>Username for primary contact to log into system</td>
        </tr>
         <tr>
            <td>FirstName:</td>
            <td>string</td>
            <td>First name of primary contact</td>
        </tr>
         <tr>
            <td>LastName:</td>
            <td>string</td>
            <td>Last name of primary contact</td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>string</td>
            <td>Password for primary contact to log into system</td>
        </tr>
        <tr>
            <td>ConfirmPassword:</td>
            <td>string</td>
            <td>Confirmation of password for primary contact to log into system</td>
        </tr>       
        <tr>
            <td>NotificationEmail:</td>
            <td>string</td>
            <td>Email address of primary contact</td>
        </tr>
        <tr>
            <td>IsAdmin:</td>
            <td>Boolean</td>
            <td>If User will have Administration abilities</td>
        </tr>
         <tr>
            <td>Title:</td>
            <td>string (optional)</td>
            <td>Users Title (Mr,Mrs,Dr etc..)</td>
        </tr>
        <tr>
            <td>ReceivesMaintenanceBySMS:</td>
            <td>Boolean (optional)</td>
            <td>If User will receive Maintenance informatiton by text</td>
        </tr>
        <tr>
            <td>ReceivesMaintenanceByEmail:</td>
            <td>Boolean (optional)</td>
            <td>If User will receive Maintenance informatiton by Email</td>
        </tr>
         <tr>
            <td>ReceivesNotificationsBySMS</td>
            <td>Boolean (optional)</td>
            <td>If User will receive Notifications  by text</td>
        </tr>
        <tr>
            <td>ReceivesNotificationsByVoice:</td>
            <td>Boolean (optional)</td>
            <td>If User will receive Notifications by Phone</td>
        </tr>
          <tr>
            <td>NotificationPhone:</td>
            <td>string (optional)</td>
            <td>Cell phone number to receive sms notifications</td>
        </tr>
           <tr>
            <td>NotificationPhone2:</td>
            <td>string (optional)</td>
            <td>Cell phone number to receive Voice notifications</td>
        </tr>
        <tr>
            <td>SMSCarrierID:</td>
            <td>integer (optional)</td>
            <td>Identifier of cell phone carrier to receive sms notifications</td>
        </tr>
        <%--<%if (ViewBag.ShowResellerParameters == true)
           {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%}%>--%>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/CreateAccountUser/Z3Vlc3Q6cGFzc3dvcmQ=?userName=TestUser+Testnew&firstName=firstname&lastName=lastname&password=password&confirmPassword=password&notificationEmail=invalid@invalid.com&isAdmin=True" target="_blank">https://<%:Request.Url.Host %>/xml/CreateAccountUser/Z3Vlc3Q6cGFzc3dvcmQ=?userName=TestUser+Testnew&amp;firstName=firstname&amp;lastName=lastname&amp;password=password&amp; confirmPassword=password&amp;notificationEmail=invalid@invalid.com&amp;isAdmin=True</a>--%>
                
    <input type="button" id="btn_TryAPI_CreateAccountUser" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_CreateAccountUser').click(function () {
				var json =
				{
					"auth": true,					
					"api": "CreateAccountUser",
                    "params": [					
						{ "name": "UserName", "type": "String", "description": "Username for primary contact to log into system", "optional": false },
						{ "name": "FirstName", "type": "String", "description": "First name of primary contact", "optional": false },
						{ "name": "LastName", "type": "String", "description": "Last name of primary contact", "optional": false },
						{ "name": "Password", "type": "String", "description": "Password for primary contact to log into system", "optional": false },
						{ "name": "ConfirmPassword", "type": "String", "description": "Confirmation of password for primary contact to log into system", "optional": false },
						{ "name": "NotificationEmail", "type": "String", "description": "Email address of primary contact", "optional": false },
						{ "name": "IsAdmin", "type": "Boolean", "description": "If User will have Administration abilities", "optional": false },
						{ "name": "Title", "type": "String", "description": "Users Title (Mr,Mrs,Dr etc..)", "optional": true },
						{ "name": "ReceivesMaintenanceBySMS", "type": "Boolean", "description": "If User will receive Maintenance informatiton by text", "optional": true },
						{ "name": "ReceivesMaintenanceByEmail", "type": "Boolean", "description": "If User will receive Maintenance informatiton by Email", "optional": true },
						{ "name": "ReceivesNotificationsBySMS", "type": "Boolean", "description": "If User will receive Notifications by text", "optional": true },
						{ "name": "ReceivesNotificationsByVoice", "type": "Boolean", "description": "If User will receive Notifications by Phone", "optional": true },
						{ "name": "NotificationPhone", "type": "String", "description": "Cell phone number to receive sms notifications", "optional": true },
						{ "name": "NotificationPhone2", "type": "String", "description": "Cell phone number to receive voice notifications", "optional": true },
						{ "name": "SMSCarrierID", "type": "Integer", "description": "Identifier of cell phone carrier to receive sms notifications", "optional": true }						
					]
				};								
				APITest(json);
			});
		});
	</script>   

    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CreateAccountUser&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&lt;APIUser UserID="1" Title="Mr" FirstName="John" LastName="Dough" EmailAddress="Invalid@invlaid.com" SMSNumber="+1 555-555-555" VoiceNumber="+1 555-555-555" DirectSMS="False" ReceivesNotificationsBySMS="False" ReceivesNotificationsByVoice="True" Active="True" Admin="True" ReceivesMaintenanceByEmail="True" ReceivesMaintenanceBySMS="False" ExternalSMSProviderID="1" UserName="JohnDoughUser"/>&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
------------------------------------------------------------------------------------------------------------------
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CreateAccountUser&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIValidationErrorList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIValidationError Parameter="UserName" Error="Username not available"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIValidationErrorList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>