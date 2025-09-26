<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3><%: Html.TranslateTag("API/CodeSampleCSharp|C# Sample","C# Sample")%></h3>

<div class="methodDiv">
<h4><%: Html.TranslateTag("API/CodeSampleCSharp|Contributor","Contributor")%></h4>
 <table>
        <tr>
            <td><%: Html.TranslateTag("API/CodeSampleCSharp|Brandon","Brandon")%></td>
        </tr>
    </table>

    <h4><%: Html.TranslateTag("API/CodeSampleCSharp|Methods Used","Methods Used")%></h4>

    <table>
        <tr>
            <td><%: Html.TranslateTag("API/CodeSampleCSharp|GetAuthToken","GetAuthToken")%></td>
            <td><%: Html.TranslateTag("API/CodeSampleCSharp|SensorList","SensorList")%></td>
        </tr>
    </table>
                            
 <h4><%: Html.TranslateTag("API/CodeSampleCSharp|Example Output","Example Output")%></h4>

 <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >

string address = string.Format("https://<%:Request.Url.Host %>/xml/GetAuthToken?username=guest&password=guestpassword");
XDocument xdoc = XDocument.Load(address);

string AuthToken = xdoc.Descendants("Result").Single().Value;
int NetworkID = 1234;

address = string.Format("https://<%:Request.Url.Host %>/xml/SensorList/{1}?NetworkID={2}", AuthToken, NetworkID);
xdoc = XDocument.Load(address);

var SensorList = (from s in xdoc.Descendants("APISensor")
       select new {
                             SensorID = Convert.ToUInt32(s.Attribute("SensorID")),
                             SensorName = s.Attribute("SensorName").Value,
                             NetworkID = Convert.ToInt32(s.Attribute("CSNetID")),
                    }); 
 </pre>
</div>




