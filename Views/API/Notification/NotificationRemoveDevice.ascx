<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationRemoveDevice</b><br />
    Removes a Control Unit or Local Notifier from a specific notification.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>NotificationRecipientID:</td>
            <td>Long</td>
            <td>Unique identifier of recipient device to remove</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/NotificationRemoveDevice/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&notificationRecipientID=123" target="_blank">https://<%=Request.Url.Host %>/xml/NotificationRemoveDevice/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;notificationRecipientID=123</a>

    
              
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationRemoveDevice&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>