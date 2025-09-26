<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountParentEdit</b><br />
    Edit Account Parent
    <br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID: </td>
            <td>Integer</td>
            <td>The Account Number of your account.</td>
        </tr>

        <tr>
            <td>parentID:</td>
            <td>long</td>
            <td>The Account Number of the desired account.</td>
        </tr>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/AccountParentEdit/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=12345&parentID=14" target="_blank">https://<%=Request.Url.Host %>/xml/AccountParentEdit/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=12345&amp;parentID=14 </a>--%>
    <input type="button" id="btn_TryAPI_AccountParentEdit" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_AccountParentEdit').click(function () {
				var json =
				{
					"auth": true,					
					"api": "AccountParentEdit",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": false },
                        { "name": "parentID", "type": "Integer", "description": "The Account Number of the desired account.", "optional": false }		
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
&nbsp;&nbsp;&lt;Method&gt;AccountParentEdit&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection""&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAccount PrimaryContactID="1234" AccountID="12345" AccountNumber="Corporate" CompanyName="TestCompany" Address="7304 South Cottonwood" Address2="Suite 240" City="Midvale" PostalCode="85047" State="UT" Country="USA" BusinessTypeID="1" IndustryTypeID="2" TimeZoneID="4" PrimaryContact="TestUser" ParentID="14" <%--IsReseller="True"--%> ExpirationDate="4/7/2017 12:00:00 AM"/>/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>
