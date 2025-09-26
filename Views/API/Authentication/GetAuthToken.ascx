<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: GetAuthToken</b><br />
    <b>*Special Case: No Authorization Token Required*</b><br />
    Returns authorization token for username password to be used in all other api methods.  This token remains valid until your password is updated in the portal.<br /><br />
    Note: Authorization token is created by using base 64 encoding of "{username}:{password}" where {username} is your online portal username and {password} is your online portal password. For example "guest:password" when encoded gives us "Z3Vlc3Q6cGFzc3dvcmQ="
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>Username:</td>
            <td>String</td>
            <td>Username you use to access online portal</td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>String</td>
            <td>Password you use to access online portal</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/GetAuthToken?username=guest&password=password" target="_blank">https://<%:Request.Url.Host %>/xml/GetAuthToken?username=guest&amp;password=password</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;GetAuthToken&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Z3Vlc3Q6cGFzc3dvcmQ=&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
    <fieldset>
        <legend>Get Auth Token</legend>
        <table>
            <tr>
                <td>Username</td>
                <td><input id="GetAuthTokenUsername" /></td>
            </tr>
            <tr>
                <td>Password</td>
                <td><input autocomplete="off" type="password" id="GetAuthTokenPassword" style="width: 153px; border-color: #aaa;" /></td>
            </tr>
            <tr>
                <td></td>
                <td><input type="button" value="Get Auth Token" onclick="getAuthToken();" /></td>
            </tr>
            <tr>
                <td>Auth Token</td>
                <td><span id="GetAuthTokenResult"></span></td>
            </tr>
            <tr>
                <td>Logon Result</td>
                <td><span id="GetAuthTokenTest"></span></td>
            </tr>
        </table>
        <script type="text/javascript">
            function getAuthToken() {
                var url = '/json/GetAuthToken?username=' + $('#GetAuthTokenUsername').val() + '&password=' + $('#GetAuthTokenPassword').val();
                $.getJSON(url, function(data) {
                    $('#GetAuthTokenResult').html(data.Result);
                    $.getJSON('/json/Logon/' + data.Result, function(test) {
                        $('#GetAuthTokenTest').html(test.Result);
                    });
                });
            }
        </script>
    </fieldset>
    
</div>