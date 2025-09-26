<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Sensor>" %>

<% 

    string AxisMode = "";
    AxisMode = Vibration800.GetAxisMode(Model).ToString();
       
%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Axis Measurement Mode","Axis Measurement Mode")%>
    </div>
    <div class="col sensorEditFormInput">
        <select id="AxisMode_Manual" name="AxisMode_Manual" class="ms-0 form-select" <%:ViewData["disabled"].ToBool() ? "disabled" : ""%>>
            <option value="1" <%: AxisMode == "1"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|X axis","X axis")%></option>
            <option value="2" <%: AxisMode == "2"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Y axis","Y axis")%></option>
            <option value="4" <%: AxisMode == "4"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Z axis","Z axis")%></option>
            <option value="3" <%: AxisMode == "3"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|X,Y axis","X,Y axis")%></option>
            <option value="5" <%: AxisMode == "5"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|X,Z axis","X,Z axis")%></option>
            <option value="6" <%: AxisMode == "6"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|Y,Z axis","Y,Z axis")%></option>
            <option value="7" <%: AxisMode == "7"? "selected":"" %>><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_104|X,Y,Z axis","X,Y,Z axis")%></option>
        </select>        
    </div>
</div>


<script type="text/javascript">
    

    $(document).ready(function () {

        //$(function () {
        //    $("#AxisMode_Manual").mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        group: true
        //    });
        //});


    });

</script>


