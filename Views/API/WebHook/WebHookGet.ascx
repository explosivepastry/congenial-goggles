<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookGet</b><br />
    Returns settings used to send data to external source.<br />
    
    <h4>Parameters</h4>
    <table>
          <%--<%if (ViewBag.ShowResellerParameters == true)
            {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} else { %>
        <tr>
            <td>None</td>
        </tr>
        <%} %>--%>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/WebHookGet/Z3Vlc3Q6cGFzc3dvcmQ=" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookGet/Z3Vlc3Q6cGFzc3dvcmQ=</a>--%>

<input type="button" id="btn_TryAPI_WebHookGet" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookGet').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookGet",
					"params": [
					]
				};								
				APITest(json);
			});
		});
    </script>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;WebHookGet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookGet ConnectionInfo="http://www.mydomain.com/{1}" BrokenCount="0" LastResult="ok" SendingEnabled="True" RetriesDisabled="True" SendWithoutDataMessage="True" AuthenticationEnabled="False"/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt</pre>
</div>