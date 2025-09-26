<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationRecipientList</b><br />
    Returns data points recorded in a range of time (limited to a 12 hour window)<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationID:</td>
            <td>Long</td>
            <td>Brings back a list of all Recipients of the specific notificaitonID</td>
        </tr>
    </table>
    
    
    <h4>Example</h4>
    <a href="/xml/NotificationRecipientList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=101" target="_blank">https://<%=Request.Url.Host %>/xml/NotificationRecipientList/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=101</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationRecipientList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationRecipientList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationRecipient UserID="554" Type="Email"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINotificationRecipient UserID="556" Type="SMS"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APINotificationRecipientList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>