<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookSetAuthentication</b><br />
    Turns on authentication and sets username and password for desired webhook.<br />
    
    <h4>Parameters</h4>
    <table>
          <%--<%if (ViewBag.ShowResellerParameters == true)
            {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} %>--%>
             <tr>
            <td>Username:</td>
            <td>String</td>
            <td>Username for the webhook authentication.</td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>String</td>
            <td>Password for the webhook authentication.</td>
        </tr>
       
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/WebHookSetAuthentication/Z3Vlc3Q6cGFzc3dvcmQ=?username=user&password=password" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookSetAuthentication/Z3Vlc3Q6cGFzc3dvcmQ=?username=user&amp;password=password</a>--%>

<input type="button" id="btn_TryAPI_WebHookSetAuthentication" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookSetAuthentication').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookSetAuthentication",
					"params": [
                        //{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true },
                        { "name": "Username", "type": "String", "description": "Username for the webhook authentication", "optional": false },
                        { "name": "Password", "type": "String", "description": "Password for the webhook authentication", "optional": false }
					]
				};								
				APITest(json);
			});
		});
    </script>
               
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;WebHookSetAuthentication&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookGet ConnectionInfo="http://www.mydomain.com/{1}" BrokenCount="0" LastResult="ok" SendingEnabled="True" RetriesDisabled="True" SendWithoutDataMessage="True" AuthenticationEnabled="True"/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt</pre>
</div>