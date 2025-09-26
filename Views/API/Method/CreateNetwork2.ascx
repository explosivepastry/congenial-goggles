<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: CreateNetwork2</b><br />
    Adds new wireless sensor network to account<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>Name:</td>
            <td>String</td>
            <td>Name of network to be added</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/CreateNetwork2/Z3Vlc3Q6cGFzc3dvcmQ=?name=New+Network+Name" target="_blank">https://<%=Request.Url.Host %>/xml/CreateNetwork2/Z3Vlc3Q6cGFzc3dvcmQ=?name=New Network Name</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CreateNetwork2&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APINetwork NetworkID="100" NetworkName="New Network Name" SendNotifications="True" /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>