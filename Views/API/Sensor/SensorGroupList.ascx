<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorGroupList</b><br />
    Returns an accounts sensor groups. 
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
        <%--<%}
          else
          { %>
        <tr>
            <td>ParentID:</td>
            <td>Integer  (optional) </td>
            <td>Unique identifier of the sensor group Parent. return only Sensor groups with this parentID </td>
        </tr>
        <%} %>--%>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/SensorGroupList/Z3Vlc3Q6cGFzc3dvcmQ=?" target="_blank">https://<%:Request.Url.Host %>/xml/SensorGroupList/Z3Vlc3Q6cGFzc3dvcmQ=?</a>--%>
     <input type="button" id="btn_TryAPI_SensorGroupList" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorGroupList').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorGroupList",
					"params": [
						{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used", "optional": true },
						{ "name": "ParentID", "type": "Integer", "description": "Unique identifier of the sensor group Parent. return only Sensor groups with this parentID", "optional": true }
					]
				};								
				APITest(json);
			});
		});
	</script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorGroupList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroupList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroup Name="My First Group" SensorGroupID="10" ParentID="29"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroup Name="Another Sweet Group" SensorGroupID="11" ParentID="29"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroup Name="another great group" SensorGroupID="15" ParentID="29"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroup Name="test group" SensorGroupID="17" ParentID="null"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APISensorGroupList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
