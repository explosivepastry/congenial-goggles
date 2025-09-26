<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookResend</b><br />
    Requeue a webhook attempt to be retried. Limit of 10 retries per attempt.<br />
    
    <h4>Parameters</h4>
    <table>      
         <tr>
            <td>attemptID: </td>
            <td>Integer </td>
            <td>Unique identifier of Attempt to resend</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/WebHookResend/Z3Vlc3Q6cGFzc3dvcmQ=?attemptID=1" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookResend/Z3Vlc3Q6cGFzc3dvcmQ=?attemptID=1</a>--%>

<input type="button" id="btn_TryAPI_WebHookResend" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookResend').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookResend",
					"params": [
                        { "name": "attemptID", "type": "Integer", "description": "Unique identifier of Attempt to resend", "optional": false },
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
&nbsp;&nbsp;&lt;Method&gt;WebHookResend&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>