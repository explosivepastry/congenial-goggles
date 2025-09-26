<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div class="methodDiv">
	<b>Method: CreateSubAccount</b><br />
	Creates a new account<br />

	<h4>Parameters</h4>
	<table>
		<tr>
			<td>CompanyName:</td>
			<td>string</td>
			<td>Company Name used to lookup account</td>
		</tr>
		<tr>
			<td>AccountNumber:</td>
			<td>string (optional)</td>
			<td>Account Number for internal use will default to CompanyName if not provided</td>
		</tr>
		<tr>
			<td>IsPremium:</td>
			<td>boolean (optional)</td>
			<td>Determines if new account receives premiere subscription</td>
		</tr>
		<tr>
			<td>TimeZoneID:</td>
			<td>integer</td>
			<td>Identifier of the timezone to view sensor data</td>
		</tr>
		<tr>
			<td>Address:</td>
			<td>string</td>
			<td>Street address for account</td>
		</tr>
		<tr>
			<td>Address2:</td>
			<td>string (optional)</td>
			<td>Second line of street address for account</td>
		</tr>
		<tr>
			<td>City:</td>
			<td>string</td>
			<td>City for account</td>
		</tr>
		<tr>
			<td>State:</td>
			<td>string</td>
			<td>State or Region for account</td>
		</tr>
		<tr>
			<td>PostalCode:</td>
			<td>string (optional)</td>
			<td>Postal Code for account</td>
		</tr>
		<tr>
			<td>Country:</td>
			<td>string</td>
			<td>Country for account</td>
		</tr>
		<tr>
			<td>parentID:</td>
			<td>Integer (optional)</td>
			<td>The ID of the account that this new subaccount will be under</td>
		</tr>
		<tr>
			<td>UserName:</td>
			<td>string</td>
			<td>User name for primary contact to log into system</td>
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
			<td>NotificationEmail:</td>
			<td>string</td>
			<td>Email address of primary contact</td>
		</tr>
		<tr>
			<td>NotificationPhone:</td>
			<td>string (optional)</td>
			<td>Cell phone number to receive sms notifications</td>
		</tr>
		<tr>
			<td>SMSCarrierID:</td>
			<td>integer (optional)</td>
			<td>Identifier of cell phone carrier to receive sms notifications</td>
		</tr>
		<%--<tr>
			<td>IsReseller:</td>
			<td>boolean (optional)</td>
			<td>Determines if new account is a reseller. Defaults to true if ommited</td>
		</tr>--%>
		<tr>
			<td>IsAdmin:</td>
			<td>boolean (optional)</td>
			<td>Determines if new account will have Administrative privileges. Defaults to true if ommited</td>
		</tr>
	</table>

	<h4>Example</h4>
	<%--<a href="/xml/CreateSubAccount/Z3Vlc3Q6cGFzc3dvcmQ=?companyName=TestCompany+Test&timeZoneID=1&address=7304+South+Cottonwood&address2=Suite+204&city=SLC&state=Utah&postalCode=74047&country=USA&userName=API.Tester&password=password&confirmPassword=password&firstName=API&lastName=Tester&notificationEmail=api.tester@invalid.com" target="_blank">https://<%:Request.Url.Host %>/xml/CreateSubAccount/Z3Vlc3Q6cGFzc3dvcmQ=?companyName=TestCompany+Test&amp;timeZoneID=1&amp;address=7304+South+Cottonwood&amp;address2=Suite+204&amp;city=SLC&amp; state=Utah&amp;postalCode=74047&amp;country=USA&amp;userName=API.Tester&amp;password=password&amp;confirmPassword=password&amp;firstName=API&amp; lastName=Tester&amp;notificationEmail=api.tester@invalid.com</a>--%>
	<input type="button" id="btn_TryAPI_CreateSubAccount" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_CreateSubAccount').click(function () {
				var json =
				{
					"auth": true,					
					"api": "CreateSubAccount",
					"params": [
						{ "name": "CompanyName", "type": "String", "description": "Company Name / Account Number used to lookup account", "optional": false },
						{ "name": "AccountNumber", "type": "String", "description": "Account Number for internal use will default to CompanyName if not provided", "optional": true },
						{ "name": "IsPremium", "type": "Boolean", "description": "	Determines if new account receives premiere subscription", "optional": true },
						{ "name": "TimeZoneID", "type": "Integer", "description": "	Identifier of the timezone to view sensor data", "optional": false },
						{ "name": "Address", "type": "String", "description": "Street address for account", "optional": false },
						{ "name": "Address2", "type": "String", "description": "Second line of street address for account", "optional": true },
						{ "name": "City", "type": "String", "description": "City for account", "optional": false },
						{ "name": "State", "type": "String", "description": "State or Region for account", "optional": false },
						{ "name": "PostalCode", "type": "String", "description": "Postal Code for account", "optional": true },
						{ "name": "Country", "type": "String", "description": "Country for account", "optional": false },
						{ "name": "parentID", "type": "Integer", "description": "The ID of the account that this new subaccount will be under", "optional": true },
						{ "name": "UserName", "type": "String", "description": "Username for primary contact to log into system", "optional": false },
						{ "name": "Password", "type": "String", "description": "Password for primary contact to log into system", "optional": false },
						{ "name": "ConfirmPassword", "type": "String", "description": "Confirmation of password for primary contact to log into system", "optional": false },
						{ "name": "FirstName", "type": "String", "description": "First name of primary contact", "optional": false },
						{ "name": "LastName", "type": "String", "description": "Last name of primary contact", "optional": false },
						{ "name": "NotificationEmail", "type": "String", "description": "Email address of primary contact", "optional": false },
						{ "name": "NotificationPhone", "type": "String", "description": "Cell phone number to receive sms notifications", "optional": true },
						{ "name": "SMSCarrierID", "type": "Integer", "description": "Identifier of cell phone carrier to receive sms notifications", "optional": true },
						/*{ "name": "IsReseller", "type": "Boolean", "description": "Determines if new account is a reseller. Defaults to true if ommited", "optional": true },*/
						{ "name": "IsAdmin", "type": "Boolean", "description": "Determines if new account will have Administrative privileges. Defaults to true if ommited", "optional": true }
					]
				};								
				APITest(json);
			});
		});
	</script>   

	<h4>Example Outputs</h4>
	<pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CreateSubAccount&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAccount PrimaryContactID="1234" AccountID="12345" AccountNumber="Corporate" CompanyName="TestCompany" Address="7304 South Cottonwood" Address2="Suite 240" City="Midvale" PostalCode="85047" State="UT" Country="USA" BusinessTypeID="1" IndustryTypeID="2" TimeZoneID="4" PrimaryContact="TestUser" ParentID="1" <%--IsReseller="True"--%> ExpirationDate="4/7/2018 12:00:00 AM"/>/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
------------------------------------------------------------------------------------------------------------------
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CreateSubAccount&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIValidationErrorList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIValidationError Parameter="CompanyName" Error="Company name already exists"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIValidationError Parameter="UserName" Error="Username not available"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIValidationErrorList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>
