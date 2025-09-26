<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookAttemptBody</b><br />
    Returns Body of specified Attempt<br />
    
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
            <td>attemptID: </td>
            <td>Integer </td>
            <td>Unique identifier of Attempt to resend</td>
        </tr>
        
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/WebHookAttemptBody/Z3Vlc3Q6cGFzc3dvcmQ=?attemptID=1" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookAttemptBody/Z3Vlc3Q6cGFzc3dvcmQ=?attemptID=1</a>--%>

<input type="button" id="btn_TryAPI_WebHookAttemptBody" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookAttemptBody').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookAttemptBody",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true },
                        { "name": "attemptID", "type": "Integer", "description": "Unique identifier of Attempt to resend", "optional": false },
					]
				};								
				APITest(json);
			});
		});
    </script>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;APIWebHookAttemptBody&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookAttemptBody Body="{"gatewayMessage":{ "gatewayID":"xxxx" , "gatewayName":"USB+Service+-+xxxx" , "accountID":"x", "networkID":"xxxx" , "messageType":"0" , "power":"0", "batteryLevel": "101" , "date": "2016-04-04 20:29:47", "count":"1", "signalStrength": "0", "pendingChange": "False" }}"/>&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt</pre>
</div>

