<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountUserList</b><br />
    Returns all users on the specified account<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID:</td>
            <td>Long</td>
            <td>Brings back a list of all Users on a specific account</td>
        </tr>
    </table>
    
    
    <h4>Example</h4>
    <a href="/xml/AccountUserList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101" target="_blank">https://<%=Request.Url.Host %>/xml/AccountUserList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;AccountUserList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIUserList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIUser UserID="554" UserName="john.doe" Name="John Doe" EmailAddress="jdoe@someserver.com" SMSNumber="5555555551" RecievesNotificaitonsBySMS="true" DirectSMS="true" ExternalSMSProviderID="1" VoiceNumber=5555555552"" RecievesNotificaitonsByVoice="true" Active="true" Admin="true" RecievesMaintenanceByEmail="true" RecievesMaintenanceBySMS="true"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIUser UserID="556" UserName="jane.doe" Name="Jane Doe" EmailAddress="jdoe1@someserver.com" SMSNumber="5555555553" RecievesNotificaitonsBySMS="true" DirectSMS="false" ExternalSMSProviderID="0" VoiceNumber=5555555554"" RecievesNotificaitonsByVoice="false" Active="true" Admin="false" RecievesMaintenanceByEmail="false" RecievesMaintenanceBySMS="false"/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;/APIUserList&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>