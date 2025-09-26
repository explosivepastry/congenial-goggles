<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<h3>
    ExternalDataPushList</h3>
<div class="methodDiv">
    <b>Method: ExternalDataPushList</b><br />
     Returns a list of settings used to send data to external source.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>accountID:</td>
            <td>Integer</td>
            <td>Unique identifier of all External Data Pushes that belong to the specific account</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/ExternalDataPushList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101" target="_blank">https://<%:Request.Url.Host %>/xml/ExternalDataPushList/Z3Vlc3Q6cGFzc3dvcmQ=?accountID=101</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;ExternalDataPushList&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:collection&quot;&gt;
&nbsp;&nbsp;&lt;APIDataPushConfigurationList&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataPushConfiguration SensorID=&quot;101&quot; ExternalID=&quot;654&quot; ConnectionInfo=&quot;http://www.mydomain.com/{1}&quot; verb=&quot;get&quot; ConnectionBody=&quot;&quot; LastResult=&quot;&quot;/&gt;
&nbsp;&nbsp;&nbsp;&nbsp;&lt;APIDataPushConfiguration SensorID=&quot;201&quot; ExternalID=&quot;456&quot; ConnectionInfo=&quot;http://www.mydomain.com/{1}&quot; verb=&quot;get&quot; ConnectionBody=&quot;&quot; LastResult=&quot;&quot;/&gt;
&nbsp;&nbsp;&lt;/APIDataPushConfigurationList&gt;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>
</div>