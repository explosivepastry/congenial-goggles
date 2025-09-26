<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<div class="row">
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Display As","Display As")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sets the Speed scale that the data is displayed in.","Sets the Speed scale that the data is displayed in.")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Max Speed","Max Speed")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This value determines the sample rate of the sensor and assumes a 1 meter object(vehicle) length. If the speed is 40 MPH and the desired object length is 2 meters, changing the Max Speed to 20 MPH will guarantee a sample is taken at least once while a 2 meter object traveling at 40 MPH. If the same 2 meter object is traveling faster than 40 MPH a sample may not be taken while the object passes over the sensor.",
        "This value determines the sample rate of the sensor and assumes a 1 meter object(vehicle) length. If the speed is 40 MPH and the desired object length is 2 meters, changing the Max Speed to 20 MPH will guarantee a sample is taken at least once while a 2 meter object traveling at 40 MPH. If the same 2 meter object is traveling faster than 40 MPH a sample may not be taken while the object passes over the sensor.")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enable Min Speed","Enable Min Speed")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Enables the use of Minimum Speed.","Enables the use of Minimum Speed.")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Min Speed","Min Speed")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|This value is used to determin the zero sampling rate.","This value is used to determin the zero sampling rate.")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity Threshold","Sensitivity Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|A vehicle is counted when magnitude goes above then below this value.","A vehicle is counted when magnitude goes above then below this value.")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Sensitivity Buffer","Sensitivity Buffer")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|After detecting a magnitude above the Sensitivity Threshold the sensor must detect a magnitude less than Sensitivity Threshold - Threshold Buffer to count the vehicle.","After detecting a magnitude above the Sensitivity Threshold the sensor must detect a magnitude less than Sensitivity Threshold - Threshold Buffer to count the vehicle.")%>
        <hr />
    </div>
    <div class="word-choice">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Aware State Threshold","Aware State Threshold")%>
    </div>
    <div class="word-def">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Help|Greater than or equal number of vehicles will cause sensor to go into aware state.","Greater than or equal number of vehicles will cause sensor to go into aware state.")%>
        <hr />
    </div>
</div>


