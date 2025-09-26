<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: WebHookSubAccountFailedAttempts</b><br />
    Returns all unsuccessful attempt information for all sub accounts
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
            <td>fromDate:</td>
            <td>DateTime</td>
            <td>Start of range from which attempts will be returned</td>
        </tr>
        <tr>
            <td>toDate:</td>
            <td>DateTime</td>
            <td>End of range from which attempts will be returned</td>
        </tr>
    </table>

    <h4>Example</h4>
<%--    <a href="/xml/WebHookSubAccountFailedAttempts/Z3Vlc3Q6cGFzc3dvcmQ=?fromDate=2016/01/01&toDate=2016/01/02" target="_blank">https://<%:Request.Url.Host %>/xml/WebHookSubAccountFailedAttempts/Z3Vlc3Q6cGFzc3dvcmQ=?fromDate=2018/01/01&amp;toDate=2018/01/02</a>--%>

<input type="button" id="btn_TryAPI_WebHookSubAccountFailedAttempts" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_WebHookSubAccountFailedAttempts').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "WebHookSubAccountFailedAttempts",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true },
                        { "name": "fromDate", "type": "DateTime", "description": "Start of range from which attempts will be returned", "optional": false },					
                        { "name": "toDate", "type": "DateTime", "description": "End of range from which attempts will be returned", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;WebHookSubAccountFailedAttempts&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookFailuresList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookFailures  AttemptID="569" CreateDate="4/4/2016 8:29:42 PM" Url="http://mydomain.com/{1}" ProcessDate="4/4/2016 8:29:42 PM" Retry="False" AttemptCount="1" Status="1"  AccountID="1234"/>&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIWebHookFailures  AttemptID="568" CreateDate="4/4/2016 8:29:42 PM" Url="http://mydomain.com/{1}" ProcessDate="4/4/2016 8:28:00 PM" Retry="False" AttemptCount="1" Status="1"  AccountID="4567"/>&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIWebHookFailuresList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>
