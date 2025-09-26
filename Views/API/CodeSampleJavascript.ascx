<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3><%: Html.TranslateTag("API/CodeSampleJavascript|Javascript Sample","Javascript Sample")%></h3>

<div class="methodDiv">

    <h4><%: Html.TranslateTag("API/CodeSampleJavascript|Contributor","Contributor")%></h4>
    <table>
        <tr>
            <td><%: Html.TranslateTag("API/CodeSampleJavascript|Brandon","Brandon")%></td>
        </tr>
    </table>

    <h4><%: Html.TranslateTag("API/CodeSampleJavascript|Methods Used","Methods Used")%></h4>
    <table>
        <tr>
            <td><%: Html.TranslateTag("API/CodeSampleJavascript|GetAuthToken","GetAuthToken")%></td>
            <td><%: Html.TranslateTag("API/CodeSampleJavascript|Login","Login")%></td>
        </tr>
    </table>
   
<div>

<h4><%: Html.TranslateTag("API/CodeSampleJavascript|Javascript Sample","Javascript Sample")%></h4>

<p><%: Html.TranslateTag("API/CodeSampleJavascript|Test JSONP Example Code","Test JSONP Example Code ")%>:</p>
<p><%: Html.TranslateTag("API/CodeSampleJavascript|Username","Username")%>:<input id="username"/><p/>
<p><%: Html.TranslateTag("API/CodeSampleJavascript|Password","Password")%>:<input id="password"/><p/>
<button onclick="test()"; style="margin-left: 65px"><%: Html.TranslateTag("API/CodeSampleJavascript|Test","Test")%></button>
<p style="font-weight: 600"><%: Html.TranslateTag("API/CodeSampleJavascript|Code","Code")%>:</p>

<pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;">
&lt;p&gt;Test JSONP Example Code&lt;p&gt;
&lt;p&gt;Username: &lt;input id="username" /&lt;p&gt;
&lt;p&gt;Password: &lt;input id="password"/ &lt;p&gt;
&lt;button onclick="test();"&gt;Test&lt;/button&gt;&lt;br/&gt;

<%--&lt;script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"&gt;&lt;/script&gt;--%>
&lt;script&gt;
&nbsp;&nbsp;function test(){
&nbsp;&nbsp;&nbsp;&nbsp;$.ajax({
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url: 'https://<%:Request.Url.Host %>/json/GetAuthToken',
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;data: { username: $('#username').val(), password: $('#password').val() },
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dataType: 'jsonp',
&nbsp;&nbsp;&nbsp;&nbsp;}).done(function (results) {
			
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$.ajax({
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;url: 'https://<%:Request.Url.Host %>/json/Logon/' + results.Result,
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;dataType: 'jsonp',
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}).done(function (results) {
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;alert(results.Result);
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;});
&nbsp;&nbsp;&nbsp;&nbsp;});
&nbsp;&nbsp;}
&lt;/script&gt;</pre>

<script type="text/javascript">
    function test() {
        $.ajax({
            url: 'https://<%:Request.Url.Host %>/json/GetAuthToken',
            data: { username: $('#username').val(), password: $('#password').val() },
            dataType: 'jsonp',
        }).done(function (results) {

            $.ajax({
                url: 'https://<%:Request.Url.Host %>/json/Logon/' + results.Result,
                dataType: 'jsonp',
            }).done(function (results) {
                alert(results.Result);
            });
        });
    }
</script>

</div>
</div>





