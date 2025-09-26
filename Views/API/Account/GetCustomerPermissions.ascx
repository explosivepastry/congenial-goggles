<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GetCustomerPermissions</b><br />
    Returns customer permissions<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>custID:</td>
            <td style="width: 100px;">long</td>
            <td>Unique identifier of customer</td>
        </tr>
    </table>

    <h4>Example</h4>
   <%-- <a href="/xml/GetCustomerPermissions/Z3Vlc3Q6cGFzc3dvcmQ=?custID=1" target="_blank">https://<%:Request.Url.Host %>/xml/GetCustomerPermissions/Z3Vlc3Q6cGFzc3dvcmQ=?custID=1</a>--%>
     <input type="button" id="btn_TryAPI_GetCustomerPermissions" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GetCustomerPermissions').click(function () {
				var json =
				{
					"auth": true,					
					"api": "GetCustomerPermissions",
					"params": [						
						{ "name": "custID", "type": "Integer", "description": "Unique identifier of customer", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;GetCustomerPermissions&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIPermissionList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIPermission Name="Customer_Edit_Other" Can="False"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIPermission Name="Customer_Edit_Self" Can="True"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIPermissionList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>