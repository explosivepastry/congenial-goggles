<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookAddHeader</b><br />
    Adds a custom header to the specified Webhook.<br />

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
<%--    <a href="/xml/WebHookAddHeader/Z3Vlc3Q6cGFzc3dvcmQ=?key=name&value=tester" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookAddHeader/Z3Vlc3Q6cGFzc3dvcmQ=?key=HeaderKey&amp;value=HeaderValue</a>--%>

<input type="button" id="btn_TryAPI_WebHookAddHeader" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookAddHeader').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookAddHeader",
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
&nbsp;&nbsp;&lt;Method&gt;WebHookAddHeader&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookPropertyList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookProperty WebHookPropertyID="42" Type="header" StringValue="TestKey=TestValue" Key="TestKey" Value="TestValue"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookProperty WebHookPropertyID="43" Type="header" StringValue="HeaderKey=HeaderValue" Key="TestKey" Value="TestValue"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIWebHookPropertyList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

</div>
