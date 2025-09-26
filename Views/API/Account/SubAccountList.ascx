<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SubAccountList</b><br />
    Returns the list of account that are sub accounts to the calling users account.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>Name:</td>
            <td>String (optional)</td>
            <td>Filters list to account with account number, company name, contact names containing this string. (case-insensitive)</td>
        </tr>
           <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SubAccountList/Z3Vlc3Q6cGFzc3dvcmQ=?name=TestCompany+Test&accountID=123" target="_blank">https://<%:Request.Url.Host %>/xml/SubAccountList/Z3Vlc3Q6cGFzc3dvcmQ=?name=TestCompany+Test&amp;accountID=123</a>--%>
    <input type="button" id="btn_TryAPI_SubAccountList" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SubAccountList').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SubAccountList",
					"params": [
						{ "name": "Name", "type": "String", "description": "Filters list to account with account number, company name, contact names containing this string. (case-insensitive)", "optional": true },
						{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;SubAccountList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&lt;APIAccountList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAccount PrimaryContactID="1234" AccountID="12345" AccountNumber="Corporate" CompanyName="TestCompany" Address="7304 South Cottonwood" Address2="Suite 240" City="Midvale" PostalCode="85047" State="UT" Country="USA" BusinessTypeID="1" IndustryTypeID="2" TimeZoneID="4" PrimaryContact="TestCompany" ParentID="1"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAccount PrimaryContactID="1234" AccountID="12345" AccountNumber="Corporate" CompanyName="TestCompany" Address="7304 South Cottonwood" Address2="Suite 240" City="Midvale" PostalCode="85047" State="UT" Country="USA" BusinessTypeID="1" IndustryTypeID="2" TimeZoneID="4" PrimaryContact="TestCompany" ParentID="1"/&gt;
&nbsp;&nbsp;&lt;APIAccountList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>