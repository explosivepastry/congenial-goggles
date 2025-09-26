<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookAttempts</b><br />
    Returns either all attempts information   or only unsuccessful attempts information
    <br />

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
            <td>showOnlyErrors </td>
            <td>boolean</td>
            <td>Return only unsuccessful attempts</td>
        </tr>
        <tr>
            <td>fromDate:</td>
            <td>DateTime</td>
            <td>Start of range from which attempts will be returned</td>
        </tr>
        <tr>
            <td>toDate:</td>
            <td>DateTime</td>
            <td>End of range from which attempts will be returned</td>
        </tr>
        <tr>
            <td>lastAttemptID:</td>
            <td>Integer (optional)</td>
            <td>Only return attempts received after this attempt ID</td>
        </tr>
    </table>

    <h4>Example</h4>
<%--    <a href="/xml/WebHookAttempts/Z3Vlc3Q6cGFzc3dvcmQ=?showOnlyErrors=false&fromDate=2016/01/01&toDate=2016/01/02" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookAttempts/Z3Vlc3Q6cGFzc3dvcmQ=?showOnlyErrors=false&amp;fromDate=2016/01/01&amp;toDate=2016/02/02</a>--%>

<input type="button" id="btn_TryAPI_WebHookAttempts" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookAttempts').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookAttempts",
					"params": [
                        //{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": false },
                        { "name": "showOnlyErrors", "type": "boolean", "description": "Return only unsuccessful attempts", "optional": false },
                        { "name": "fromDate", "type": "DateTime", "description": "Start of range from which attempts will be returned", "optional": false },
                        { "name": "toDate", "type": "DateTime", "description": "End of range from which attempts will be returned", "optional": false },
                        { "name": "lastAttemptID", "type": "Integer", "description": "Only return attempts received after this attempt ID", "optional": true }

					]
				};								
				APITest(json);
			});
		});
    </script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto;max-width:835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;WebHookAttempts&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookAttemptsList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookAttempts AttemptID="569" CreateDate="4/4/2016 8:29:42 PM" Url="http://mydomain.com/{1}" ProcessDate="4/4/2016 8:29:42 PM" Retry="False" AttemptCount="1" Status="Success"/>&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookAttempts AttemptID="568" CreateDate="4/4/2016 8:29:42 PM" Url="http://mydomain.com/{1}" ProcessDate="4/4/2016 8:28:00 PM" Retry="False" AttemptCount="1" Status="Success"/>&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIWebHookAttemptsList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>
