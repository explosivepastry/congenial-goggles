<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountDataMessages</b><br />
    Returns data points recorded in a range of time (limited to a 7 day window)<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>fromDate:</td>
            <td>DateTime</td>
            <td>Start of range from which messages will be returned</td>
        </tr>
        <tr>
            <td>toDate:</td>
            <td>DateTime</td>
            <td>End of range from which messages will be returned</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/AccountDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?fromDate=2011/01/01&toDate=2011/01/02" target="_blank">https://<%=Request.Url.Host %>/xml/AccountDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?fromDate=2011/01/01&amp;toDate=2011/01/02</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;AccountDataMessages&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessageList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage MessageID="228" SensorID="101" MessageDate="1/1/2011 6:36:00 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="30" DisplayData="86° F" PlotValue="86" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage MessageID="227" SensorID="101" MessageDate="1/1/2011 6:34:33 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="50" DisplayData="122° F" PlotValue="122" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDataMessageList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>