<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorGroupCreate</b><br />
    Create or Edit a sensor group
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
        <%--<%}%>--%>

        <tr>
            <td>Name:</td>
            <td>String</td>
            <td>Name to give the Group</td>
        </tr>
        <tr>
            <td>sensorGroupID:</td>
            <td>Integer  (optional) </td>
            <td>Unique identifier of the sensor group. If included, group will be edited</td>
        </tr>
        <tr>
            <td>ParentID:</td>
            <td>Integer  (optional) </td>
            <td>Unique identifier of the sensor group Parent.</td>
        </tr>

    </table>

    <h4>Example</h4>
    <%--<a href="/xml/SensorGroupCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=My+group+3" target="_blank">https://<%:Request.Url.Host %>/xml/SensorGroupCreate/Z3Vlc3Q6cGFzc3dvcmQ=?name=My+group+3</a>--%>
     <input type="button" id="btn_TryAPI_SensorGroupCreate" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_SensorGroupCreate').click(function () {
				var json =
				{
					"auth": true,					
					"api": "SensorGroupCreate",
					"params": [
						{ "name": "accountID", "type": "Integer", "description": "Unique identifier of SubAccount. If omitted,  Your default account will be used", "optional": true },
                        { "name": "Name", "type": "String", "description": "Name to give the Group", "optional": false },
						{ "name": "sensorGroupID", "type": "Integer", "description": "Unique identifier of the sensor group. If included, group will be edited", "optional": true },
						{ "name": "ParentID", "type": "Integer", "description": "Unique identifier of the sensor group Parent", "optional": true }
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
&nbsp;&nbsp;&lt;Method&gt;SensorGroupCreate&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroup&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APISensorGroup Name="My group 3" SensorGroupID="18"  ParentID="29"/&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
