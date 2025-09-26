<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: NotificationAssignDevice</b><br />
    Asssigns a Control Unit or Local Notifier to a specific notification.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NotificationID:</td>
            <td>Long</td>
            <td>Unique identifier of the Notification</td>
        </tr>
        <tr>
            <td>DeviceID:</td>
            <td>Long</td>
            <td>Unique identifier of device to assign</td>
        </tr>
            <tr>
            <td>DeviceType:</td>
            <td>String</td>
            <td>Type of device:(notifier control1  control2)</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/NotificationAssignDevice/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&deviceID=123&deviceType=notifier" target="_blank">https://<%=Request.Url.Host %>/xml/NotificationAssignDevice/Z3Vlc3Q6cGFzc3dvcmQ=?notificationID=3&amp;deviceID=123&amp;deviceType=notifier</a>

    
              
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationAssignDevice&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>