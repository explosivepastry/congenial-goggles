<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="methodDiv">
    <b>Method: SensorEquipmentSet</b><br />
    Returns the sensor object.<br />
    
    <h4>Parameters</h4>
    <table>
        <tr>
            <td>sensorID:</td>
            <td>Integer</td>
            <td>Unique identifier of the sensor</td>
        </tr>
        <tr>
            <td>Make:</td>
            <td>String (optional)</td>
            <td>Make  of the sensor equipment</td>
        </tr>
        <tr>
            <td>Model:</td>
            <td>String (optional)</td>
            <td>Model of the sensor equipment</td>
        </tr>
        <tr>
            <td>SerialNumber:</td>
            <td>String (optional)</td>
            <td>SerialNumber of the sensor equipment</td>
        </tr>
        <tr>
            <td>SensorLocation:</td>
            <td>String (optional)</td>
            <td>Location  of the sensor equipment</td>
        </tr>
        <tr>
            <td>SensorDescription:</td>
            <td>String (optional)</td>
            <td>Short Description of the sensor equipment</td>
        </tr>
        <tr>
            <td>Note:</td>
            <td>String(optional)</td>
            <td>Additional notes for the sensor equipment</td>
        </tr>
    </table>
    
    <h4>Example</h4>
    <a href="/xml/SensorEquipmentSet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599&Make=SensorMake&Model=SensorModel&SerialNumber=xxxxxx&SensorLocation=Meat_Refrigerator&SensorDescription=Cold_Temperature&Note=Notifications_set_at_32_degrees" target="_blank">https://<%:Request.Url.Host %>/xml/SensorEquipmentSet/Z3Vlc3Q6cGFzc3dvcmQ=?sensorID=002599&amp;Make=SensorMake&amp;Model=SensorModel&amp;SerialNumber=xxxxxx&amp;SensorLocation=Meat_Refrigerator&amp; SensorDescription=Cold_Temperature&amp;Note=Notifications_set_at_32_degrees</a>
                            
    <h4>Example Output</h4>
    <pre style="border: solid 1px black; background-color: #DDEEFF; padding:5px; overflow:auto;max-width:835px;" >
&lt;?xml version="1.0" encoding="utf-8"?&gt;
&lt;SensorRestAPI xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema"&gt;
&nbsp;&nbsp;&lt;Method&gt;SensorEquipmentGet&lt;/Method&gt;
&nbsp;&nbsp;&lt;Result xsi:type="xsd:string"&gt;
&nbsp;&nbsp;&lt;APISensorEquipment SensorID="xxx" Make="SensorMake" Model="SensorModel" SerialNumber="xxxxxx" SensorLocation="Meat Refrigerator" SensorDescription="Cold Temperature" Note="Notifications set at 32 degrees"&gt;
&nbsp;&lt;/Result&gt;
&lt;/SensorRestAPI&gt;</pre>
</div>