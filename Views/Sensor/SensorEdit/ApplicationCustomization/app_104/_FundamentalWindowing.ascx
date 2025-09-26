<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<% 
   
    string FundWindowing = "";
    FundWindowing = Vibration800.GetFundWindowing(Model).ToString();
       
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Fundamental Windowing","Fundamental Windowing")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="FundWindowing_Manual" name="FundWindowing_Manual" class="ms-0 form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option value="0" <%: FundWindowing == "0"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Rect","Rect")%></option>
            <option value="1" <%: FundWindowing == "1"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Flat Top","Flat Top")%></option>
            <option value="2" <%: FundWindowing == "2"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Hanning","Hanning")%></option>
        </select>        
    </div>
</div>


<script type="text/javascript">
    

    $(document).ready(function () {

        //$(function () {
        //    $("#FundWindowing_Manual").mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        group: true
        //    });
        //});


    });

</script>


