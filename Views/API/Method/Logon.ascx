<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: Logon</b><br />
    Check authorization token by calling this method<br />
    
    <h4>Parameters</h4>
    None
    
    <h4>Example</h4>
    <a href="/xml/Logon/Z3Vlc3Q6cGFzc3dvcmQ=" target="_blank">https://<%=Request.Url.Host %>/xml/Logon/Z3Vlc3Q6cGFzc3dvcmQ=</a>
                
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;Logon&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;Success&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>