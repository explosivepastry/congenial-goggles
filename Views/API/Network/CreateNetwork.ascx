<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: CreateNetwork</b><br />
    Adds new wireless sensor network to account<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>Name:</td>
            <td>String</td>
            <td>Name of network to be added</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/CreateNetwork/Z3Vlc3Q6cGFzc3dvcmQ=?name=New+Network+Name" target="_blank">https://<%:Request.Url.Host %>/xml/CreateNetwork/Z3Vlc3Q6cGFzc3dvcmQ=?name=New Network Name</a>--%>
      <input type="button" id="btn_TryAPI_CreateNetwork" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_CreateNetwork').click(function () {
				var json =
				{
					"auth": true,					
					"api": "CreateNetwork",
					"params": [
                        { "name": "Name", "type": "String", "description": "Name of network to be added", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;CreateNetwork&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>