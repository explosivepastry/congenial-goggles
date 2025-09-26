<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: AccountUserImageUpload</b><br />
    Returns the user object.<br />
    <h2 style="color:red;"> This method requires the data to be sent in the body of a POST request</h2>
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>userID:</td>
            <td>Integer</td>
            <td>Unique identifier of the user</td>
        </tr>
          <tr>
            <td>ImageFile</td>
            <td>File (binary)</td>
            <td>jpeg, bmp, png</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/AccountUserImageUpload/Z3Vlc3Q6cGFzc3dvcmQ=?userID=1" target="_blank">https://<%:Request.Url.Host %>/xml/AccountUserImageUpload/Z3Vlc3Q6cGFzc3dvcmQ=?userID=1</a>
                            
    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;ResellerPasswordReset&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
</pre>
</div>