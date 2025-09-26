<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: Check username is unique.</b><br />
  
   
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>username:</td>
            <td>string</td>
            <td>Desired username</td>
        </tr>
    </table>
    
    <h4>Example</h4>
   <%-- <a href="/xml/UserNameAvailable/Z3Vlc3Q6cGFzc3dvcmQ=?username=APIUser+Test" target="_blank">https://<%=Request.Url.Host %>/xml/UserNameAvailable/Z3Vlc3Q6cGFzc3dvcmQ=?username=APIUser+Test</a>--%>
    <input type="button" id="btn_TryAPI_UserNameAvailable" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_UserNameAvailable').click(function () {
				var json =
				{
					"auth": false,					
					"api": "UserNameAvailable",
					"params": [
						{ "name": "username", "type": "String", "description": "Desired username", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
	</script>            

    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;UserNameAvailable&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;username: APIUser Test is unavailable&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>

    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;UserNameAvailable&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;true&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>

</div>