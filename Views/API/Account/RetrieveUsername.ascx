<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: Retrieve Username</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Sends an email with your username to the email address provided.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>email:</td>
            <td>string</td>
            <td>Email address for the desired username.</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/RetrieveUsername?email=me@mywebsite.com" target="_blank">https://<%:Request.Url.Host %>/xml/RetrieveUsername?email=me@mywebsite.com</a>--%>
                
     <input type="button" id="btn_TryAPI_RetrieveUsername" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_RetrieveUsername').click(function () {
				var json =
				{
					"auth": false,					
					"api": "RetrieveUsername",
					"params": [
						{ "name": "email", "type": "String", "description": "Email address for the desired username.", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
	</script>   

    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;RetrieveUsername&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;Sent email to me@mywebsite.com&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>

</div>