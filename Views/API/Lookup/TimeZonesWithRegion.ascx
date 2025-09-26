<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: TimeZonesWithRegion</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Returns the list of supported timezones.<br />
    
    <h4>Parameters</h4>
    None
    
    <h4>Example</h4>
    <%--<a href="/xml/TimeZonesWithRegion" target="_blank">https://<%:Request.Url.Host %>/xml/TimeZonesWithRegion</a>--%>
    <input type="button" id="btn_TryAPI_TimeZonesWithRegion" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
		$(function () {
			$('#btn_TryAPI_TimeZonesWithRegion').click(function () {
				var json =
				{
					"auth": false,					
					"api": "TimeZonesWithRegion",
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
&nbsp;&nbsp;&lt;Method&gt;TimeZonesWithRegion&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APITimeZoneList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APITimeZone ID=&quot;1&quot; StandardName=&quot;Hawaiian Standard Time&quot; DisplayName=&quot;USA – Hawaii, Honolulu&quot; Region=&quot;Pacific&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APITimeZone ID=&quot;2&quot; StandardName=&quot;Alaskan Standard Time&quot; DisplayName=&quot;USA – Alaska, Anchorage&quot; Region=&quot;Americas&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APITimeZone ID=&quot;3&quot; StandardName=&quot;Pacific Standard Time&quot; DisplayName=&quot;USA – California, Los Angeles (DST)&quot; Region=&quot;Pacific&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;...
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APITimeZoneList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>