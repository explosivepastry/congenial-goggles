<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GetDatumByType</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Returns the list of datums by application type.<br />

    <h4>Parameters</h4>
    <table>
        <tr>
            <td>ApplicationID:</td>
            <td>Long</td>
            <td>Unique identifier of the sensor</td>
        </tr>
    </table>

    <h4>Example</h4>
    <%--<a href="/xml/GetDatumByType?applicationID=2" target="_blank">https://<%=Request.Url.Host %>/xml/GetDatumByType?applicationID=2</a>--%>
    <input type="button" id="btn_TryAPI_GetDatumByType" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_GetDatumByType').click(function () {
				var json =
				{
					"auth": false,					
					"api": "GetDatumByType",
					"params": [
						{ "name": "ApplicationID", "type": "Long", "description": "Unique identifier of the sensor", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;GetDatumByType&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDatumTypeList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;DatumID="2" Name="Temperature" Index="0"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDatumTypeList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
