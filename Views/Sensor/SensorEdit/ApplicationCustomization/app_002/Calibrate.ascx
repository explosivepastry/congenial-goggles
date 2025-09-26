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

    <%-- //Temporary fix for Bad Data from Temperature Sensor firmware v7.14.7.2--%>
    <%if (Model.ApplicationID == 2 && Model.FirmwareVersion == "7.14.7.2")
        {
            TempData["CanCalibrate"] = false;%>

    <div class="form-inline">
        <div class="formBody">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|This version of firmware (7.14.7.2) does not support calibration.  For assistance updating your sensor to a newer firmware please contact tech support. ", "This version of firmware (7.14.7.2) does not support calibration.  For assistance updating your sensor to a newer firmware please contact tech support. ")%>
        </div>
    </div>
    <% }%>

    <%
        if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
        { %>

    <div class="form-inline">
        <div class="row sensorEditForm">

            <%if (Model.SensorTypeID == 8)
                {%>
            <div>
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|WiFi2 Calibration","This sensor has two calibration ranges that can be calibrated to improve accuracy of the sensor. Range 1 adjusts sensor accuracy from -40C to 29C and Range 2 adjusts sensor accuracy from 29C to 125C. We recommend calibrating between -30C and 20C (ideally at 7C) for Range 1 and between 40C and 120C (ideally at 114C) for Range 2." +
                 "Calibrating outside of these recommended ranges can be performed to produce better measurements at the point of calibration but may produce less accurate average readings across the entire range.")%>
            </div>
            <div>
                <%:Html.TranslateTag("Step 1: Place the temperature lead in a stable temperature environment within one of the ranges above until the readings are stable.") %>
            </div>
            <div>
                <%:Html.TranslateTag("Step 2: Enter the actual temperature in the text box and press Calibrate.") %>
            </div>
            <div>
                <%:Html.TranslateTag("Step 3: Keep sensor in stable temperature till Calibration Success/Fail message appears in readings.") %>
            </div>
            <%}%>

            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Actual reading is","Actual reading is")%>
            </div>
            <div class="col sensorEditFormInput">
                <%: Html.TextBox("actual")%> <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|degrees","degrees")%>
            </div>
        </div>
    </div>
    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
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
        //MobiScroll
        //$(function () {
        //    $('#tempScale').mobiscroll().select({
        //        theme: 'ios',
        //        display: popLocation,
        //        minWidth: 200
        //    });
        //});

        //MobiScroll NumberPad
        $(function () {
            $('#actual').mobiscroll().numpad({

                theme: 'ios',

                display: popLocation,
                min: -300,
                max: 500,
                scale: 1,
                preset: 'decimal'
            });
        });


        $("#actual").change(function () {
            if (!isANumber($("#actual").val()))
                $("#actual").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
        });
    </script>

    <div class="clear"></div>
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<% %>
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
