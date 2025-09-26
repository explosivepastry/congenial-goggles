<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountRecentDataMessages</b><br />
    Returns data points recorded in a range of time (limited to a 2 hour window)<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID:</td>
            <td>Integer</td>
            <td>Unique identifier of the account</td>
        </tr>
        <tr>
            <td>minutes:</td>
            <td>Integer</td>
            <td>Number of minutes past messages will be returned</td>
        </tr>
        <tr>
            <td>lastMessageID:</td>
            <td>Integer (optional)</td>
            <td>Only return messages received after this message ID</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/AccountRecentDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101&minutes=10&lastMessageID=226" target="_blank">https://<%=Request.Url.Host %>/xml/AccountRecentDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101&amp;minutes=5&amp;lastMessageID=226</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;AccountRecentDataMessages&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessageList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage MessageID="228" AccountID="101" MessageDate="1/1/2011 6:36:00 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="30" DisplayData="86° F" PlotValue="86" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage MessageID="227" AccountID="101" MessageDate="1/1/2011 6:34:33 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="50" DisplayData="122° F" PlotValue="122" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDataMessageList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
