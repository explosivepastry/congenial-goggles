<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookCreate</b><br />
    Sets configuration to send data to external source. Returns Webhook configuration<br />
    
    <h4>Parameters</h4>
    <table>
         <%--<%if (ViewBag.ShowResellerParameters == true)
           {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%}%>--%>
          <tr>
            <td>baseUrl:</td>
            <td>String</td>
            <td>Url of 3rd party server and defined data to send (Url Encoded). If this value contains an & character,<br /> it must be replaced by its urlencoding, %26. Depending on the context, you may also have to escape a % using %25</td>
        </tr>
        
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/WebHookCreate/Z3Vlc3Q6cGFzc3dvcmQ=?baseUrl=http://mydomain.com/{1}" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookCreate/Z3Vlc3Q6cGFzc3dvcmQ=?baseUrl=http://mydomain.com/{1} </a>--%>

<input type="button" id="btn_TryAPI_WebHookCreate" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookCreate').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookCreate",
					"params": [
                        //{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true },
                        { "name": "baseURL", "type": "String", "description": "Url of 3rd party server and defined data to send (Url Encoded). If this value contains an & character, it must be replaced by its urlencoding, %26. Depending on the context, you may also have to escape a % using %25", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
    </script>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;WebHookCreate&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookGet ConnectionInfo="http://www.mydomain.com/{1}" BrokenCount="0" LastResult="ok" SendingEnabled="True" RetriesDisabled="True"  SendWithoutDataMessage="True" AuthenticationEnabled="False"/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt</pre>
</div>