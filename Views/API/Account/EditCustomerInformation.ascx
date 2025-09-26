<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
	<b>Method: EditCustomerInformation</b><br />
	Edit Customer Information<br />

	<h4>Parameters</h4>
	<table>
		<tr>
			<td>custID:</td>
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
			<td>SMSCarrierID:</td>
			<td>long (optional)</td>
			<td>SMS Carrier Account ID used to change carriers</td>
		</tr>

	</table>

	<h4>Example</h4>
	<%--<a href="/xml/EditCustomerInformation/Z3Vlc3Q6cGFzc3dvcmQ=?custID=1&FirstName=David&NotificationPhone=8888888888" target="_blank">https://<%:Request.Url.Host %>/xml/EditCustomerInformation/Z3Vlc3Q6cGFzc3dvcmQ=?custID=1&amp;FirstName=David&amp;NotificationPhone=8888888888</a>--%>
	<input type="button" id="btn_TryAPI_EditCustomerInformation" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_EditCustomerInformation').click(function () {
				var json =
				{
					"auth": true,
					"api": "EditCustomerInformation",
					"params": [
						{ "name": "userID", "type": "long", "description": "Customer Number", "optional": false },
						{ "name": "FirstName", "type": "string", "description": "Customers First Name", "optional": true },
						{ "name": "LastName", "type": "string", "description": "Customers Last Name", "optional": true },
						{ "name": "NotificationEmail", "type": "string", "description": "Customers email to send notifications", "optional": true },
						{ "name": "NotificationPhone", "type": "string", "description": "Customers Phone number to send notifications", "optional": true },
						{ "name": "SMSCarrierID", "type": "long", "description": "SMS Carrier Account ID used to change carriers", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;EditCustomerInformation&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Successfully Saved&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</div>
