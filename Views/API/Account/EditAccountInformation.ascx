<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: EditAccountInformation</b><br />
    Edit Account Information<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID:</td>
            <td>long</td>
            <td>Account Number</td>
        </tr>
        <tr>
            <td>AccountNumber:</td>
            <td>string (optional)</td>
            <td>CompanyName/Account Number  used to lookup account</td>
        </tr>
        <tr>
            <td>CompanyName:</td>
            <td>string (optional)</td>
            <td>Company Name / Account Number used to lookup account</td>
        </tr>
        <tr>
            <td>TimeZoneID:</td>
            <td>long (optional)</td>
            <td>Identifier of the timezone to view sensor data</td>
        </tr>
        <tr>
            <td>Address:</td>
            <td>string (optional)</td>
            <td>Street address for account</td>
        </tr>
        <tr>
            <td>Address2:</td>
            <td>string (optional)</td>
            <td>Second line of street address for account</td>
        </tr>
        <tr>
            <td>City:</td>
            <td>string (optional)</td>
            <td>City for account</td>
        </tr>
        <tr>
            <td>State:</td>
            <td>string (optional)</td>
            <td>State or Region for account</td>
        </tr>
        <tr>
            <td>PostalCode:</td>
            <td>string (optional)</td>
            <td>Postal Code for account</td>
        </tr>
        <tr>
            <td>Country:</td>
            <td>string (optional)</td>
            <td>Country for account</td>
        </tr>
         <tr>
            <td>BusinessTypeID:</td>
            <td>long (optional)</td>
            <td>Business Type</td>
        </tr>
         <tr>
            <td>IndustryTypeID:</td>
            <td>long (optional)</td>
            <td>Industry Type of the Business</td>
        </tr>
         <tr>
            <td>PrimaryContactID:</td>
            <td>long (optional)</td>
            <td>CustomerId of the customer you want to be the Primary Contact for the account.</td>
        </tr>   
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/EditAccountInformation/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=1&CompanyName=TestCompany+Test&TimeZoneID=1&Address=7304+South+Cottonwood&Address2=Suite+204&City=SLC&State=Utah&PostalCode=74047&Country=USA" target="_blank">https://<%:Request.Url.Host %>/xml/EditAccountInformation/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=1&amp;CompanyName=TestCompany+Test&amp;TimeZoneID=1&amp;Address=7304+South+Cottonwood&amp; Address2=Suite+204&amp;City=SLC&amp;State=Utah&amp;PostalCode=74047&amp;Country=USA</a>--%>
       
     <input type="button" id="btn_TryAPI_EditAccountInformation" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_EditAccountInformation').click(function () {
				var json =
				{
					"auth": true,					
					"api": "EditAccountInformation",
                    "params": [
						{ "name": "accountID", "type": "Integer", "description": "Account Number", "optional": false },
						{ "name": "AccountNumber", "type": "String", "description": "CompanyName/Account Number used to lookup account", "optional": true },
						{ "name": "CompanyName", "type": "String", "description": "Company Name / Account Number used to lookup account", "optional": true },
						{ "name": "TimeZoneID", "type": "Integer", "description": "Identifier of the timezone to view sensor data", "optional": true },
						{ "name": "Address", "type": "String", "description": "Street address for account", "optional": true },
						{ "name": "Address2", "type": "String", "description": "Second line of street address for account", "optional": true },
						{ "name": "City", "type": "String", "description": "City for account", "optional": true },
						{ "name": "State", "type": "String", "description": "State or Region for account", "optional": true },
						{ "name": "PostalCode", "type": "String", "description": "Postal Code for account", "optional": true },
						{ "name": "Country", "type": "String", "description": "Country for account", "optional": true },
						{ "name": "BusinessTypeID", "type": "Integer", "description": "Business Type", "optional": true },
						{ "name": "IndustryTypeID", "type": "Integer", "description": "Industry Type of the Business", "optional": true },
						{ "name": "PrimaryContactID", "type": "Integer", "description": "CustomerId of the customer you want to be the Primary Contact for the account.", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;EditAccountInformation&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Successfully Saved&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</div>