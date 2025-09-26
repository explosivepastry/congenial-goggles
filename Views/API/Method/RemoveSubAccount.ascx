<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>


<div class="methodDiv">
    <b>Method: RemoveSubAccount()</b><br />
     Removes the account from the system.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID:</td>
            <td>Integer</td>
            <td>Unique identifier of the account</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/RemoveSubAccount/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=1234" target="_blank">https://<%=Request.Url.Host %>/xml/RemoveSubAccount/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=1234</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;RemoveSubAccount&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>