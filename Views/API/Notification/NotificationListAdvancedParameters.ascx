<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: NotificationListAdvancedParameters</b><br />
     Lists parameters for a desired advanced notification<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of Account. If omitted,  Your default account will be used.</td>
        </tr>
        <tr>
            <td>AdvancedNotificationID: </td>
            <td>Integer</td>
            <td>Unique identifier of the advanced notification</td>
        </tr>
    </table>
    
    <b>*Special Case: If the returned value  ParameterType is "Option". Use API call "NotificationListAdvancedParameterOption" to find values *</b><br />

    <h4>Example</h4>
<%--    <a href="/xml/NotificationListAdvancedParameters/Z3Vlc3Q6cGFzc3dvcmQ=?advancedNotificationID=2" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationListAdvancedParameters/Z3Vlc3Q6cGFzc3dvcmQ=?advancedNotificationID=2</a>--%>

<input type="button" id="btn_TryAPI_NotificationListAdvancedParameters" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationListAdvancedParameters').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationListAdvancedParameters",
					"params": [
                        { "name": "accountID", "type": "Integer", "description": "Unique identifier of Account. If omitted,  Your default account will be used.", "optional": true },
                        { "name": "AdvancedNotificationID", "type": "Integer", "description": "Unique identifier of the advanced notification", "optional": false }						
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
&nbsp;&nbsp;&lt;Method&gt;NotificationListAdvancedParameters&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameterList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAdvancedNotificationParameter AdvancedNotificationParameterID="3" ParameterType="int" Required="True" ParameterDescription="The amount of time in the sensor must not checked in before this notification will be activated on the next checkin."/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIAdvancedNotificationParameterList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
