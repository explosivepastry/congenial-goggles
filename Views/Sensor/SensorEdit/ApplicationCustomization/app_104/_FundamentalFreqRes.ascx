<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<% 
   
    string FundFreqResolution = "";
    FundFreqResolution = Vibration800.GetFundFreqResolution(Model).ToString();
   
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fundamental Frequency Resolution","Fundamental Frequency Resolution")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="FundFreqResolution_Manual" name="FundFreqResolution_Manual" class="ms-0 form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option value="0" <%: FundFreqResolution == "0"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|12 Hz","12 Hz")%></option>
            <option value="1" <%: FundFreqResolution == "1"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|6 Hz","6 Hz")%></option>
            <option value="2" <%: FundFreqResolution == "2"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|3 Hz","3 Hz")%></option>
        </select>        
    </div>
</div>


<script type="text/javascript">
    

    $(document).ready(function () {

        //$(function () {
        //    $("#FundFreqResolution_Manual").mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        group: true
        //    });
        //});


    });

</script>


