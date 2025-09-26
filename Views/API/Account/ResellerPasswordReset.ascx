<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: Reseller Password Reset</b><br />
    Resets the password of the user with userid, if that account is owned by the requesting reseller.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>userID:</td>
            <td>int</td>
            <td>id of the user whose password you would like to reset</td>
        </tr>
        <tr>
            <td>NewPassword:</td>
            <td>string</td>
            <td>New password for the given account</td>
        </tr>
    </table>
   
    <h4>Example</h4>
    <%--<a href="/xml/ResellerPasswordReset/Z3Vlc3Qjkg6zc3dvcmQ=?userid=738&newpassword=seCurep@ssw0rd" target="_blank">https://<%:Request.Url.Host %>/xml/ResellerPasswordReset/Z3Vlc3Qjkg6zc3dvcmQ=?userid=738&newpassword=seCurep@ssw0rd</a>--%>
    <input type="button" id="btn_TryAPI_ResellerPasswordReset" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_ResellerPasswordReset').click(function () {
				var json =
				{
					"auth": true,					
					"api": "ResellerPasswordReset",
					"params": [
                        { "name": "userID", "type": "Integer", "description": "id of the user whose password you would like to reset", "optional": false },
						{ "name": "NewPassword", "type": "String", "description": "New password for the given account", "optional": false }
					]
				};								
				APITest(json);
			});
		});
	</script>
                
    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;ResellerPasswordReset&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>

</div>