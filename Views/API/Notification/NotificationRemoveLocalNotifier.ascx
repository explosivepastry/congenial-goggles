<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationRemoveLocalNotifier</b><br />
    Removes a local notifier from  a specific notification.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>LocalNotifierRecipientID:</td>
            <td>Long</td>
            <td>Unique identifier for that notification's local notifier recipient</td>
        </tr>
      
    </table>
    
    <h4>Example</h4>
<%--    <a href="/xml/NotificationRemoveLocalNotifier/Z3Vlc3Q6cGFzc3dvcmQ=?LocalNotifierRecipientID=3" target="_blank">https://<%:Request.Url.Host %>/xml/NotificationRemoveLocalNotifier/Z3Vlc3Q6cGFzc3dvcmQ=?LocalNotifierRecipientID=3</a>--%>

<input type="button" id="btn_TryAPI_NotificationRemoveLocalNotifier" class="btn btn-primary btn-sm" value="Try this API" />
	<script>
		$(function () {
            $('#btn_TryAPI_NotificationRemoveLocalNotifier').click(function () {
				var json =
				{
					"auth": true,					
                    "api": "NotificationRemoveLocalNotifier",
					"params": [
                        { "name": "LocalNotifierRecipientID", "type": "Long", "description": "Unique identifier for that notification's local notifier recipient", "optional": false }
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
&nbsp;&nbsp;&lt;Method&gt;NotificationRemoveLocalNotifier&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>