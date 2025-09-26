<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookAddCookie</b><br />
    Adds a cookie to the specified Webhook.<br />

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
            <td>Key: </td>
            <td>String</td>
            <td>Which is a unique identifier for some item of data.</td>
        </tr>
         <tr>
            <td>Value: </td>
            <td>String</td>
            <td>Either the data that is identified or a pointer to the location of that data.</td>
        </tr>
      
    </table>

    <h4>Example</h4>
<%--    <a href="/xml/WebHookAddCookie/Z3Vlc3Q6cGFzc3dvcmQ=?key=name&value=tester" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookAddCookie/Z3Vlc3Q6cGFzc3dvcmQ=?key=CookieKey&amp;value=CookieValue</a>--%>

<input type="button" id="btn_TryAPI_WebHookAddCookie" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookAddCookie').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookAddCookie",
					"params": [
                        //{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": false },
                        { "name": "Key", "type": "String", "description": "Which is a unique identifier for some item of data.", "optional": false },					
                        { "name": "Value", "type": "String", "description": "Either the data that is identified or a pointer to the location of that data.", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
    </script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;WebHookAddCookie&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookPropertyList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookProperty WebHookPropertyID="42" Type="cookie" StringValue="TestKey=TestValue" Key="TestKey" Value="TestValue"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookProperty WebHookPropertyID="43" Type="cookie" StringValue="CookieKey=CookieValue" Key="TestKey" Value="TestValue"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIWebHookPropertyList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>
