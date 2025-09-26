<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: TimeZones</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Returns the list of supported timezones.<br />
    
    <h4>Parameters</h4>
    None
    
    <h4>Example</h4>
    <%--<a href="/xml/TimeZones" target="_blank">https://<%:Request.Url.Host %>/xml/TimeZones</a>--%>
           
    <input type="button" id="btn_TryAPI_TimeZones" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_TimeZones').click(function () {
				var json =
				{
					"auth": false,					
					"api": "TimeZones",
					"params": [
						
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
&nbsp;&nbsp;&lt;Method&gt;TimeZones&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINameIDList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINameID ID="1" Name="_UTC -10 – USA – Hawaii, Honolulu" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINameID ID="2" Name="_UTC -9 – USA – Alaska, Anchorage" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINameID ID="3" Name="_UTC -8 – USA – California, Los Angeles (DST)" /&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINameIDList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>