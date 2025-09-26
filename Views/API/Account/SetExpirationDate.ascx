<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SetExpirationDate</b><br />
    Adds 1 year of premium subscription to an account<br />
    
     <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
      
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/SetExpirationDate/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=1" target="_blank">https://<%=Request.Url.Host %>/xml/SetExpirationDate/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=1</a>--%>
    <input type="button" id="btn_TryAPI_SetExpirationDate" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SetExpirationDate').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SetExpirationDate",
					"params": [
						{ "name": "accountID", "type": "Integer", "description": "unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true }						
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
&nbsp;&nbsp;&lt;Method&gt;SetExpirationDate&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection""&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAccount PrimaryContactID="1234" AccountID="12345" AccountNumber="Corporate" CompanyName="TestCompany" Address="7304 South Cottonwood" Address2="Suite 240" City="Midvale" PostalCode="85047" State="UT" Country="USA" BusinessTypeID="1" IndustryTypeID="2" TimeZoneID="4" PrimaryContact="TestUser" ParentID="1" <%--IsReseller="True"--%> ExpirationDate="4/7/2018 12:00:00 AM"/>/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>

