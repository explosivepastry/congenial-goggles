<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountUserEdit</b><br />
    Edit User Information<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>userID:</td>
            <td>long</td>
            <td>Customer Number</td>
        </tr>
        <tr>
            <td>FirstName:</td>
            <td>string (optional)</td>
            <td>Customers First Name</td>
        </tr>
        <tr>
            <td>LastName:</td>
            <td>string (optional)</td>
            <td>Customers Last Name</td>
        </tr>
        <tr>
            <td>NotificationEmail:</td>
            <td>string (optional)</td>
            <td>Customers email to send notifications</td>
        </tr>
        <tr>
            <td>NotificationPhone:</td>
            <td>string (optional)</td>
            <td>Customers Phone number to send notifications</td>
        </tr>
         <tr>
            <td>NotificationPhone2:</td>
            <td>string (optional)</td>
            <td>Customers Phone number to send Voice notifications</td>
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
            <td>SMSCarrierID:</td>
            <td>long (optional)</td>
            <td>SMS Carrier Account ID used to change carriers</td>
        </tr>
       
    </table>
    
    <h4>Example</h4>
   <%-- <a href="/xml/AccountUserEdit/Z3Vlc3Q6cGFzc3dvcmQ=?userID=1&FirstName=David&NotificationPhone=8888888888" target="_blank">https://<%:Request.Url.Host %>/xml/AccountUserEdit/Z3Vlc3Q6cGFzc3dvcmQ=?userID=1&amp;FirstName=David&amp;NotificationPhone=8888888888</a>--%>
    <input type="button" id="btn_TryAPI_AccountUserEdit" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_AccountUserEdit').click(function () {
				var json =
				{
					"auth": true,					
					"api": "AccountUserEdit",
                    "params": [					
						{ "name": "userID", "type": "Integer", "description": "Customer Number", "optional": false },
						{ "name": "FirstName", "type": "String", "description": "First name of primary contact", "optional": true },
						{ "name": "LastName", "type": "String", "description": "Last name of primary contact", "optional": true },						
						{ "name": "NotificationEmail", "type": "String", "description": "Email address of primary contact", "optional": true },
						{ "name": "NotificationPhone", "type": "String", "description": "Cell phone number to receive sms notifications", "optional": true },
						{ "name": "NotificationPhone2", "type": "String", "description": "Cell phone number to receive voice notifications", "optional": true },						
						{ "name": "Title", "type": "String", "description": "Users Title (Mr,Mrs,Dr etc..)", "optional": true },
						{ "name": "ReceivesMaintenanceBySMS", "type": "Boolean", "description": "If User will receive Maintenance informatiton by text", "optional": true },
						{ "name": "ReceivesMaintenanceByEmail", "type": "Boolean", "description": "If User will receive Maintenance informatiton by Email", "optional": true },
						{ "name": "ReceivesNotificationsBySMS", "type": "Boolean", "description": "If User will receive Notifications by text", "optional": true },
						{ "name": "ReceivesNotificationsByVoice", "type": "Boolean", "description": "If User will receive Notifications by Phone", "optional": true },						
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
</div>