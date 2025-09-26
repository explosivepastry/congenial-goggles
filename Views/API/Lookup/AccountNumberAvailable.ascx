<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: Check account number is unique.</b><br />
  
   
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountNumber:</td>
            <td>string</td>
            <td>Desired Account Number</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/AccountNumberAvailable/Z3Vlc3Q6cGFzc3dvcmQ=?accountNumber=APIUser+Test" target="_blank">https://<%=Request.Url.Host %>/xml/AccountNumberAvailable/Z3Vlc3Q6cGFzc3dvcmQ=?accountNumber=APIUser+Test</a>--%>
    <input type="button" id="btn_TryAPI_AccountNumberAvailable" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_AccountNumberAvailable').click(function () {
				var json =
				{
					"auth": false,					
					"api": "AccountNumberAvailable",
					"params": [
						{ "name": "accountNumber", "type": "String", "description": "Desired Account Number", "optional": false }						
					]
				};								
				APITest(json);
			});
		});
	</script>            


    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;AccountNumberAvailable&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;Account Number: APIUser Test is unavailable&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>

    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;AccountNumberAvailable&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;true&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>

</div>