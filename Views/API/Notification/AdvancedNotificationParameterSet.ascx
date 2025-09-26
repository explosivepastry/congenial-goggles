<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: AdvancedNotificationParameterSet</b><br />
     Sets  advanced notification parameter<br />
    
    <h4>Parameters</h4>
    <table>
          <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>AdvancedNotificationParameterID: </td>
            <td>Integer</td>
            <td>Unique identifier of the advanced notification parameter</td>
        </tr>
        <tr>
            <td>ParameterValue:</td>
            <td>string</td>
            <td>The Value to set for the parameter</td>
        </tr>
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/AdvancedNotificationParameterSet/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=596&AdvancedNotificationParameterID=15&parameterValue=1234" target="_blank">https://<%:Request.Url.Host %>/xml/AdvancedNotificationParameterSet/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=596&amp;AdvancedNotificationParameterID=15&amp;parameterValue=1234</a>--%>

<input type="button" id="btn_TryAPI_AdvancedNotificationParameterSet" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_AdvancedNotificationParameterSet').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "AdvancedNotificationParameterSet",
					"params": [
                        { "name": "NotificationID", "type": "Long", "description": "Unique identifier of the notification", "optional": false },
                        { "name": "AdvancedNotificationParameterID", "type": "Integer", "description": "Unique identifier of the advanced notification parameter", "optional": false },					
                        { "name": "ParameterValue", "type": "string", "description": "The Value to set for the parameter", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;AdvancedNotificationParameterSet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterValueList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterValue  NotificationID="596" AdvancedNotificationParameterID="19" ParameterValue="9" ParameterDescription="CompressorID"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIAdvancedNotificationParameterValueList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
