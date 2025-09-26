<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountRecentDataMessages</b><br />
    Returns data points recorded in a range of time (limited to a 2 hour window)<br />
     *Maximum of 5000 Datapoints returned. <br />
    
    <p style="color:red;">AccountRecentDataMessages is deprecated. To gather data from mulitple sensors utilize the Webhook API's.  This will phase out between Oct 01 2023 - Dec 31 2023.</p>
    <h4>Parameters</h4>
    <table>
        <%--<%if (ViewBag.ShowResellerParameters == true)
            {%>--%>
        <tr>
            <td>accountID: </td>
            <td>Integer (optional)</td>
            <td>Unique identifier of SubAccount. If omitted,  Your default account will be used.</td>
        </tr>
        <%--<%} %>--%>
        <tr>
            <td>networkID</td>
            <td>Integer (optional) </td>
            <td>Unique identifier of the network</td>
        </tr>
        <tr>
            <td>minutes:</td>
            <td>Integer</td>
            <td>Number of minutes past messages will be returned</td>
        </tr>
        <tr>
            <td>lastDataMessageGUID:</td>
            <td>GUID (optional)</td>
            <td>Only return messages received after this message ID</td>
        </tr>
    </table>

    <h4>Example</h4>
<%--    <a href="/xml/AccountRecentDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101&minutes=10&lastDataMessageGUID=xxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" target="_blank">https://<%:Request.Url.Host %>/xml/AccountRecentDataMessages/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101&amp;minutes=5&amp;lastDataMessageGUID=xxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx</a>--%>

    <input type="button" id="btn_TryAPI_AccountRecentDataMessages" class="btn btn-primary btn-sm" value="Try this API" />
    <script>
        $(function () {
            $('#btn_TryAPI_AccountRecentDataMessages').click(function () {
                var json =
                {
                    "auth": true,
                    "api": "AccountRecentDataMessages",
                    "params": [
                        { "name": "networkID", "type": "Integer", "description": "Unique identifier of the network", "optional": true },
                        { "name": "minutes", "type": "Integer", "description": "Number of minutes past messages will be returned", "optional": false },
                        { "name": "lastDataMessageGUID", "type": "GUID", "description": "Only return messages received after this message ID", "optional": true }
                    ]
                };
                APITest(json);
            });
        });
    </script>

    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding: 5px; overflow: auto; max-width: 835px;">
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;AccountRecentDataMessages&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessageList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage  AccountID="101" MessageDate="1/1/2011 6:36:00 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="30" DisplayData="86° F" PlotValue="86" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataMessage AccountID="101" MessageDate="1/1/2011 6:34:33 PM" State="0" SignalStrength="-36" Voltage="3.1" Battery="100" Data="50" DisplayData="122° F" PlotValue="122" MetNotificationRequirements="False" GatewayID="1234"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIDataMessageList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>
