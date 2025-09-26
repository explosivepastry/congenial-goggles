
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h3><%: Html.TranslateTag("API/CodeSamplePHP|PHP Sample","PHP Sample")%></h3>

<div class="methodDiv">
<h4><%: Html.TranslateTag("API/CodeSamplePHP|Contributor","Contributor")%></h4>
    <table>
        <tr>
            <td><%: Html.TranslateTag("API/CodeSamplePHP|Marc","Marc")%></td>
        </tr>
    </table>

    <h4><%: Html.TranslateTag("API/CodeSamplePHP|Methods Used","Methods Used")%></h4>
    <table>
        <tr>
            <td><%: Html.TranslateTag("API/CodeSamplePHP|SensorDataMessages","SensorDataMessages")%></td>
            <td><%: Html.TranslateTag("API/CodeSamplePHP|SensorList","SensorList")%></td>
        </tr>
    </table>
                    
<h4><%: Html.TranslateTag("API/CodeSamplePHP|User Submited PHP Sample","User Submited PHP Sample")%></h4>

<pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
    
&lt;?php

function getData($AuthToken,$ActionType,$ActionParams) {
    
    $APIurl = 'https://<%:Request.Url.Host %>/xml/' . $ActionType . '/' . $AuthToken.$ActionParams;
    //echo $APIurl;

    $curl = curl_init();
    curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
    curl_setopt($curl, CURLOPT_URL, $APIurl);
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    $xmlResult1 = curl_exec($curl);

    if ($xmlResult1 === false) {
        die('Error fetching data: ' . curl_error($curl));
    }
    curl_close($curl);

    $xml = simplexml_load_string($xmlResult1);

    if ($xml === false) {
        die('Error parsing XML');
    }
    return $xml;
}

$Username = "guest";
$Password = "password";
$Token = base64_encode($Username . ":" . $Password);

$xmlresult = getData($Token, "SensorList","");
$Sensorlist=$xmlresult->Result->APISensorList;

    $sensor=$Sensorlist->APISensor[(Integer)$_POST['name']];

    $timeval=strtotime($sensor["LastCommunicationDate"]);
    $actparam="?sensorID=".$sensor["SensorID"] . "&fromDate=".date("Y/m/d",$timeval-7*24*60*60)."&toDate=".date("Y/m/d",$timeval);
    $datavals=array();
    
    $xmlresult = getData($Token, "SensorDataMessages",$actparam);
    $Messagelist=$xmlresult->Result->APIDataMessageList;
    
    header("Content-Type: application/json"); 
    
    foreach ($Messagelist->APIDataMessage as $datamessage) {
        $point=array((string)$datamessage["MessageDate"],(string)$datamessage["DisplayData"]);
        $datavals[]=$point;
    }
    echo json_encode($datavals);

?&gt;

</pre>
    
<h4><%: Html.TranslateTag("APi/CodeSamplePHP|Html Sample","Html Sample")%></h4>

<pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >

<%--&lt;script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" /&gt;--%>
&lt;script type="text/javascript"&gt;
    $(document).ready(function() {
        $('#getData').click(function() {
            messageval = "";
            $.post('data.php', { name: form.name.value },
                function(data) {
                    console.log(data);
                    $.each(data, function(key, pointvalue) {
                        //alert(eval(value["x"]));
                        messageval += "Message-" + key + ": " + pointvalue[0] + "," + pointvalue[1] + "&lt;br&gt;";
                    });
                    $("div").html(messageval).show();
                }, "json");
        });
    });

&lt;/script&gt;

&lt;form name="form"&gt;
    &lt;input type="text" name="name"/&gt;&lt;input id="getData" type="button" value="get"/&gt;
    &lt;div&gt;&lt;/div&gt;
&lt;/form&gt;
</pre>
</div>

<h3><%: Html.TranslateTag("APi/CodeSamplePHP|PHP Sample 2","PHP Sample  ")%>2</h3>

<div class="methodDiv">

    <h4><%: Html.TranslateTag("APi/CodeSamplePHP|Contributor","Contributor")%></h4>
    <table>
        <tr>
            <td><%: Html.TranslateTag("APi/CodeSamplePHP|Adjustments by Shannon","Adjustments by Shannon")%></td>
        </tr>
    </table>

    <h4><%: Html.TranslateTag("APi/CodeSamplePHP|Methods Used","Methods Used")%></h4>
    <table>
        <tr>
            <td><%: Html.TranslateTag("APi/CodeSamplePHP|SensorDataMessages","SensorDataMessages")%></td>
            <td><%: Html.TranslateTag("APi/CodeSamplePHP|SensorList","SensorList")%></td>
        </tr>
    </table>
                            
  <h4><%: Html.TranslateTag("APi/CodeSamplePHP|User Submited PHP Sample 2","User Submited PHP Sample  ")%>2</h4>

  <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >

&lt;?php

function getData($AuthToken,$ActionType,$ActionParams) {

    $APIurl = 'https://<%:Request.Url.Host %>/xml/' . $ActionType . '/' . $AuthToken.$ActionParams;
    //echo $APIurl;
    
    $curl = curl_init();
    curl_setopt($curl, CURLOPT_SSL_VERIFYPEER, false);
    curl_setopt($curl, CURLOPT_URL, $APIurl);
    curl_setopt($curl, CURLOPT_RETURNTRANSFER, true);
    $xmlResult1 = curl_exec($curl);

    if ($xmlResult1 === false) {
        die('Error fetching data: ' . curl_error($curl));
    }

    curl_close($curl);

    $xml = simplexml_load_string($xmlResult1);

    if ($xml === false) {
        die('Error parsing XML');
    }
    return $xml;
}

$Username = "guest";
$Password = "password";
$Token = base64_encode($Username . ":" . $Password);

$xmlresult = getData($Token, "SensorList","");
$Sensorlist=$xmlresult->Result->APISensorList;
print_r($Sensorlist);
echo ' ';
 
    $sensor=$Sensorlist->APISensor[0];
    print_r($sensor);
    echo ' ';

    $timeval=strtotime($sensor["LastCommunicationDate"]);
    $actparam="?sensorID=".$sensor["SensorID"] . "&fromDate=".date("Y/m/d",$timeval-7*24*60*60)."&toDate=".date("Y/m/d",$timeval);
    $datavals=array();
    
    $xmlresult = getData($Token, "SensorDataMessages",$actparam);
    $Messagelist=$xmlresult->Result->APIDataMessageList;
    
    foreach ($Messagelist->APIDataMessage as $datamessage) {
        $point=array((string)$datamessage["MessageDate"],(string)$datamessage["DisplayData"]);
        $datavals[]=$point;
    }
    echo json_encode($datavals);

?&gt;

</pre>
</div>