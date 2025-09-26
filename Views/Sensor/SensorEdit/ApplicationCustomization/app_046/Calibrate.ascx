<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%
    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    TempData["CanCalibrate"] = true;

    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
    Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>


<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>


<%
    if(TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
    { %>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_046|Actual reading is","Actual reading is")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Html.TextBox("actual")%>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|degrees","degrees")%>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_046|Actual reading is","Actual reading is")%>
        </div>
        <div class="col sensorEditFormInput">
            <select class="form-select" name="tempScale" id="tempScale">
                <option value="F"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Fahrenheit","Fahrenheit")%></option>
                <option value="C"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Celsius","Celsius")%></option>
            </select>
        </div>
    </div>
    <script>
        var popLocation = '<%=Request.Browser.IsMobileDevice ? "bottom" : "center"%>';

        $('#actual').addClass('form-control');
        //MobiScroll
        //$(function () {
        //    $('#tempScale').mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        minWidth: 200
        //    });
        //});

        //MobiScroll NumberPad
        //$(function () {
        //    $('#actual').mobiscroll().numpad({

        //        theme: 'ios',

        //        display: popLocation,
        //        min: -300,
        //        max: 500,
        //        scale: 1,
        //        preset: 'decimal'
        //    });
        //});


        $("#actual").change(function () {
            if (!isANumber($("#actual").val()))
                $("#actual").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
        });
    </script>

    <div class="clear"></div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>

<%}
     else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow)) 
  {%>

<div class="formBody">
    <div>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This sensor has been pre-calibrated and certified by","This sensor has been pre-calibrated and certified by")%> <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
    </div>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate","View Calibration Certificate")%></a>
    </div>
</div>
<%}%>
</form>