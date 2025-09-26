<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookCookieList</b><br />
    Returns list of cookies attached to the specified Webhook.<br />

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
<%--    <a href="/xml/WebHookCookieList/Z3Vlc3Q6cGFzc3dvcmQ=" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookCookieList/Z3Vlc3Q6cGFzc3dvcmQ=</a>--%>

<input type="button" id="btn_TryAPI_WebHookCookieList" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookCookieList').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookCookieList",
					"params": [
                        //{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": false },
					]
				};								
				APITest(json);
			});
		});
    </script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;WebHookCookieList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookPropertyList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookProperty WebHookPropertyID="42" Type="cookie" StringValue="TestKey=TestValue" Key="TestKey" Value="TestValue"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookProperty WebHookPropertyID="43" Type="cookie" StringValue="CookieKey=CookieValue" Key="TestKey" Value="TestValue"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIWebHookPropertyList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>
