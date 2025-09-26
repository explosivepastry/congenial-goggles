<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NetworkList</b><br />
    Returns the list of networks that belong to user.<br />
    
    <h4>Parameters</h4>
      <table>
      <%--<%if (ViewBag.ShowResellerParameters == true)
        {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} else { %>
         <tr>
          <td>None</td>
        </tr>
        <%} %>--%>
    </table>
    
    <h4>Example</h4>
    <%--<a href="/xml/NetworkList/Z3Vlc3Q6cGFzc3dvcmQ=" target="_blank">https://<%:Request.Url.Host %>/xml/NetworkList/Z3Vlc3Q6cGFzc3dvcmQ=</a>--%>
    <input type="button" id="btn_TryAPI_NetworkList" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_NetworkList').click(function () {
				var json =
				{
					"auth": true,					
					"api": "NetworkList",
					"params": [
						{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used.", "optional": true }						
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
&nbsp;&nbsp;&lt;Method&gt;NetworkList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINetworkList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINetwork NetworkID="100" NetworkName="OfficeNetwork" SendNotifications="False" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINetwork NetworkID="300" NetworkName="Warehouse" SendNofitications="True" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINetworkList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>