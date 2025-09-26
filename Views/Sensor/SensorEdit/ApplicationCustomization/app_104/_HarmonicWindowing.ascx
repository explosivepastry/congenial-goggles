<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<% 

    string HarmonicWindowing = "";
    HarmonicWindowing = Vibration800.GetHarmonicWindowing(Model).ToString();
    
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Harmonic Windowing","Harmonic Windowing")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="HarmonicWindowing_Manual" name="HarmonicWindowing_Manual" class="ms-0 form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option value="0" <%: HarmonicWindowing == "0"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Rect","Rect")%></option>
            <option value="1" <%: HarmonicWindowing == "1"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Flat Top","Flat Top")%></option>
            <option value="2" <%: HarmonicWindowing == "2"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Hanning","Hanning")%></option>
        </select>        
    </div>
</div>


<script type="text/javascript">
    

    $(document).ready(function () {

        //$(function () {
        //    $("#HarmonicWindowing_Manual").mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        group: true
        //    });
        //});


    });

</script>


