<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3><%: Html.TranslateTag("API/BaseAPIExplained|Base API Url Explained","Base API Url Explained")%></h3>

 <div class="methodDiv">

         <p style="font-weight: 600"><%: Html.TranslateTag("API/BaseAPIExplained|Base: Protocol://Host/ResponseType/Method/AuthorizationToken?Parameters","Base: Protocol://Host/ResponseType/Method/AuthorizationToken?Parameters")%> </p>
            <table>
                <tr>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|Protocol","Protocol")%></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|https","https")%></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|Our online API only support https for 256 bit ssl encryption or http for non encrypted communication","Our online API only support https for 256 bit ssl encryption or http for non encrypted communication")%></td>
                </tr>
                <tr>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|Host","Host")%></td>
                    <td><%:Request.Url.Host %></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|Web host domain or resoved IPAddress","Web host domain or resoved IPAddress")%></td>
                </tr>
                <tr>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|ResponseType","ResponseType")%></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|[xml | json | jsonp]","[xml | json | jsonp]")%></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|xml or json","xml or json")%></td>
                </tr>
                <tr>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|Method","Method")%></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|[method name]","[method name]")%></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|Name of the method to call","Name of the method to cal")%></td>
                </tr>
                <tr>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|AuthorizationToken","AuthorizationToken")%></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|[authorization token]","[authorization token]")%></td>
                    <td><%: Html.TranslateTag("API/BaseAPIExplained|authorization token that identifies you to the server.","authorization token that identifies you to the server.")%></td>
                </tr>
            </table>
            
            <h4><%: Html.TranslateTag("API/BaseAPIExplained|Example","Example")%></h4>

            <a href="/xml/Logon/Z3Vlc3Q6cGFzc3dvcmQ=" target="_blank">https://<%:Request.Url.Host %>/xml/Logon/Z3Vlc3Q6cGFzc3dvcmQ=</a>
            
            <h4><%: Html.TranslateTag("API/BaseAPIExplained|Example Output","Example Output")%></h4>

            <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >

&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;Logon&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>

<%--<h4>
Sample Calling Code (C#)</h4>
<pre style="border: solid 1px black; background-color: #DDEEFF">
                            
//Create Client
MonnitAPI.SensorReadingsClient client = new API_Consumption.MonnitAPI.SensorReadingsClient();

//Call API Method
string DataMessages = client.DataMessagesByRange(
"Bob.Smith", 
"Bobs_Password", 
DateTime.UtcNow.AddDays(-1), 
DateTime.UtcNow);
</pre>--%>

</div>