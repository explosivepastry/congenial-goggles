<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3><%: Html.TranslateTag("API/CodeSamples|Code Samples","Code Samples")%></h3>

<div class="methodDiv">
    <table>
        <tr>
            <th></th>
            <th><%: Html.TranslateTag("API/CodeSamples|Language","Language")%></th>
            <th><%: Html.TranslateTag("API/CodeSamples|Methods Used","Methods Used")%></th>
            <th><%: Html.TranslateTag("API/CodeSamples|Contributor","Contributor")%></th>
        </tr>
        <tr>
            <td><a class="codeSample" href="/API/CodeSample/CSharp"><%: Html.TranslateTag("API/CodeSample/CSharp|View","View")%></a></td>
            <td><%: Html.TranslateTag("API/CodeSample/CSharp|C#","C#")%></td>
            <td><%: Html.TranslateTag("API/CodeSample/CSharp|GetAuthToken, SensorList","GetAuthToken, SensorList")%></td>
            <td><%: Html.TranslateTag("API/CodeSample/CSharp|Brandon","Brandon")%></td>
        </tr>
        <tr>
            <td><a class="codeSample" href="/API/CodeSample/PHP"><%: Html.TranslateTag("API/CodeSample/PHP|View","View")%></a></td>
            <td><%: Html.TranslateTag("API/CodeSample/PHP|PHP","PHP")%></td>
            <td><%: Html.TranslateTag("API/CodeSample/PHP|SensorList, SensorDataMessages","SensorList, SensorDataMessages")%></td>
            <td><%: Html.TranslateTag("API/CodeSample/PHP|Marc","Marc")%></td>
        </tr>
        <tr>
            <td><a class="codeSample" href="/API/CodeSample/Javascript"><%: Html.TranslateTag("API/CodeSample/Javascript|View","View")%></a></td>
            <td><%: Html.TranslateTag("API/CodeSample/Javascript|Javascript (JSONP)","Javascript (JSONP)")%></td>
            <td><%: Html.TranslateTag("API/CodeSample/Javascript|GetAuthToken, Login","GetAuthToken, Login")%></td>
            <td><%: Html.TranslateTag("API/CodeSample/Javascript|Brandon","Brandon")%></td>
        </tr>
    </table>
</div>


