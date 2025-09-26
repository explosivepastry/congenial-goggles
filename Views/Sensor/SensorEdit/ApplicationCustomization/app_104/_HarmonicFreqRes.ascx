<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<% 
   
    string HarmonicFreqResolution = "";
    HarmonicFreqResolution = Vibration800.GetHarmonicFreqResolution(Model).ToString();
   
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Harmonic Frequency Resolution","Harmonic Frequency Resolution")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="HarmonicFreqResolution_Manual" name="HarmonicFreqResolution_Manual" class="ms-0 form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option value="0" <%: HarmonicFreqResolution == "0"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|100 Hz","100 Hz")%></option>
            <option value="1" <%: HarmonicFreqResolution == "1"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|50 Hz","50 Hz")%></option>
            <option value="2" <%: HarmonicFreqResolution == "2"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|25 Hz","25 Hz")%></option>
        </select>        
    </div>
</div>


<script type="text/javascript">
    

    $(document).ready(function () {

        //$(function () {
        //    $("#HarmonicFreqResolution_Manual").mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        group: true
        //    });
        //});


    });

</script>


