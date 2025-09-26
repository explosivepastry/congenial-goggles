<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: Update Password</b><br />
    Updates to a new password<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>NewPassword:</td>
            <td>string</td>
            <td>New password for the given account</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/UpdatePassword/Z3Vlc3Qjkg6zc3dvcmQ=?newpassword=seCurep@ssw0rd" target="_blank">https://<%=Request.Url.Host %>/xml/UpdatePassword/Z3Vlc3Qjkg6zc3dvcmQ=?newpassword=seCurep@ssw0rd</a>
                
    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;UpdatePassword&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;Success.&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>

</div>