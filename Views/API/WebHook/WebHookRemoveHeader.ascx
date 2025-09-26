<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookRemoveHeader</b><br />
    Deletes desired Header from webhook <br />
    
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
            <td>webHookPropertyID: </td>
            <td>Integer</td>
            <td>Unique identifier of Header.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/WebHookRemoveHeader/Z3Vlc3Q6cGFzc3dvcmQ=?webHookPropertyID=123" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookRemoveHeader/Z3Vlc3Q6cGFzc3dvcmQ=?webHookPropertyID=123</a>--%>


<input type="button" id="btn_TryAPI_WebHookRemoveHeader" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookRemoveHeader').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookRemoveHeader",
					"params": [
                        //{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": false },
                        { "name": "webHookPropertyID", "type": "Integer", "description": "Unique identifier of Header.", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;WebHookRemoveHeader&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>