<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: Reset Password</b><br />
    Sends an email with a link to reset your password.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>Username:</td>
            <td>string</td>
            <td>Username for the given account</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ResetPassword?username=myaccount" target="_blank">https://<%=Request.Url.Host %>/xml/ResetPassword?username=myaccount</a>
                
    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;ResetPassword&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;Success.&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>

</div>