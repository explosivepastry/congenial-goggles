<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: RemoveNetwork()</b><br />
     Removes the network from the system.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>networkID:</td>
            <td>Integer</td>
            <td>Unique identifier of the network</td>
        </tr>
    </table>
    
    <h4>Example</h4>
   <%-- <a href="/xml/RemoveNetwork/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=1234" target="_blank">https://<%:Request.Url.Host %>/xml/RemoveNetwork/Z3Vlc3Q6cGFzc3dvcmQ=?networkID=1234</a>--%>
    <input type="button" id="btn_TryAPI_RemoveNetwork" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_RemoveNetwork').click(function () {
				var json =
				{
					"auth": true,					
					"api": "RemoveNetwork",
					"params": [
						{ "name": "networkID", "type": "Integer", "description": "Unique identifier of the network", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;RemoveNetwork&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>