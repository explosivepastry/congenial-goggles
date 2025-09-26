<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationListAdvancedParameterOptions</b><br />
     Lists  options for a desired parameter<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>AdvancedNotificationParameterID: </td>
            <td>Integer</td>
            <td>Unique identifier of the advanced notification parameter</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationListAdvancedParameterOptions/Z3Vlc3Q6cGFzc3dvcmQ=?advancedNotificationParameterID=15" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationListAdvancedParameterOptions/Z3Vlc3Q6cGFzc3dvcmQ=?advancedNotificationParameterID=15</a>--%>

<input type="button" id="btn_TryAPI_NotificationListAdvancedParameterOptions" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationListAdvancedParameterOptions').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationListAdvancedParameterOptions",
					"params": [
                        { "name": "AdvancedNotificationParameterID", "type": "Integer", "description": "Unique identifier of the advanced notification parameter", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;NotificationListAdvancedParameterOption&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterOptionList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterOption Description="Above" ValueToEnter="1"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterOption Description="Below" ValueToEnter="0"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIAdvancedNotificationParameterOptionList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
