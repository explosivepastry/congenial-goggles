<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SubAccountList</b><br />
    Returns the list of account that are sub accounts to the calling users account.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>Name:</td>
            <td>String (optional)</td>
            <td>Filters list to account with account number, company name, contact names containing this string. (case-insensitive)</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SubAccountList/Z3Vlc3Q6cGFzc3dvcmQ=?name=Monnit+Test" target="_blank">https://<%=Request.Url.Host %>/xml/SubAccountList/Z3Vlc3Q6cGFzc3dvcmQ=?name=Monnit+Test</a>
                
    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;CreateAccount&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:collection"&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIAccount AccountID="100" AccountNumber="Monnit Test" CompanyName="Monnit Test" PrimaryContact="API Tester" /&gt;
&nbsp;&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>