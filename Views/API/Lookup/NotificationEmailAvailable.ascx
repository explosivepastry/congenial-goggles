<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: Check notification email is unique.</b><br />
  
   
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>notificationEmail:</td>
            <td>string</td>
            <td>Desired Notification Email</td>
        </tr>
    </table>
    
    <h4>Example</h4>

    <input type="button" id="btn_TryAPI_NotificationEmailAvailable" class="btn btn-primary btn-sm" value="Try this API" />

	<script>
        $(function () {
            $('#btn_TryAPI_NotificationEmailAvailable').click(function () {
                var json =
                {
                    "auth": false,
                    "api": "NotificationEmailAvailable",
                    "params": [
                        { "name": "notificationEmail", "type": "String", "description": "Desired notification email", "optional": false }
                    ]
                };
                APITest(json);
            });
        });
    </script>            

    <h4>Example Outputs</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationEmailAvailable&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;NotificationEmailAvailable: API is unavailable&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>

    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;SensorRestAPI&gt;
&nbsp;&nbsp;&lt;Method&gt;NotificationEmailAvailable&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type=&quot;xsd:string&quot;&gt;true&lt;/Result&gt;
&lt;/SensorRestAPI&gt;
    </pre>

</div>