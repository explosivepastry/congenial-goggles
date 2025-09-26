<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
   
<%
     DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

    CalibrationCertificate certificate = CalibrationCertificate.LoadBySensor(Model);
    bool certIsInternal = (certificate != null && certificate.isInternalCert);
  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (certIsInternal ||(string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow)))
	{%>

<%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{ %>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

<%} %>



<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />
    <input type="hidden" value="<%:MonnitSession.CurrentCustomer.CustomerID%>" name="userID" id="Hidden2" />
    <input type="hidden" value="<%: MonnitSession.CurrentCustomer.Preferences["Date Format"] %>" name="datePrefFormat" id="hidden3" >
                    

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (TempData["CanCalibrate"].ToBool() &&  CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow) || certIsInternal)
		{
			string[] lastValues = null;
			if (Model.LastDataMessage != null)
			{%>
    <div class="form-group">
        <div class="col-sm-1 col-12 mdBox">
        </div>
        <div class="bold col-sm-11 col-12">
            <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|Important", "Important")%></b>: <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|Perform these steps in the order indicated.", "Perform these steps in the order indicated.")%>
        </div>

    </div>

    <div class="form-group">

        <div class="bold col-sm-11 col-12">
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|1. Zero Calibration: Press the zero button.  Keep the sensor in the 0 PPM environment and wait for a full two data points to come in after this command is accepted(red x on status icon clears) for this process to complete, don't send any other calibration or configuration changes before this process completes. After this step is complete the sensor should read near 0 ppm", "1. Zero Calibration: Press the zero button.  Keep the sensor in the 0 PPM environment and wait for a full two data points to come in after this command is accepted(red x on status icon clears) for this process to complete, don't send any other calibration or configuration changes before this process completes. After this step is complete the sensor should read near 0 ppm")%>
            <div class="clear"></div>
            <br />    
            <%if (MonnitSession.CustomerCan("AdvancedCalibration"))
                { %>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|2. H2S Span Calibration: This step calibrates the sensor according to a ppm reading above 100 ppm. It is recommended to calibrate the sensor in an environment above maximum expected H2S concentration in the environment. This step is complete as soon as the red x clears.", "2. H2S Span Calibration: This step calibrates the sensor according to a ppm reading above 100 ppm. It is recommended to calibrate the sensor in an environment above maximum expected H2S concentration in the environment. This step is complete as soon as the red x clears.")%>
            <div class="clear"></div>
            <br />
            <%} %>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|Temperature Calibration: Submit actual Temperature", "Temperature Calibration: Submit actual Temperature")%>
        </div>

    </div>
    <div class="clear"></div>
    <br />



    <div class="formBody">
        <input class="form-check-input" type="radio" name="calType" value="3">
        <%: Html.TranslateTag("Zero Calibration", "Zero Calibration")%>
        <%if (MonnitSession.CustomerCan("AdvancedCalibration"))
            { %>
        <input class="form-check-input" type="radio" name="calType" value="2">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|H2S Span Calibration", "H2S Span Calibration")%>
        <%} %>
        <input class="form-check-input" type="radio" name="calType" value="1">
        <%: Html.TranslateTag("Temperature Calibration", "Temperature Calibration")%>
        <br>
        <div id="cal-1" style="display: none; margin-top: 15px;">

            <% lastValues = Model.LastDataMessage.Data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); %>
            <div>
                <hr />
                Current Temperature is <%: Model.LastDataMessage != null ? Convert.ToString(lastValues[2].ToDouble()) : "Unavailable" %> C
                <input type="hidden" name="observedTemp" value="<%:lastValues[2].ToDouble()%>" />
            </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    Set Temperature
                </div>
                <div class="col sensorEditFormInput">
                    <%: Html.TextBox("actualTemp", (Dictionary<string, object>)ViewData["HtmlAttributes"])%> C
                </div> 
            </div>
        </div>
             <%if (MonnitSession.CustomerCan("AdvancedCalibration"))
                 { %>
        <div id="cal-2" style="display: none; margin-top: 15px;">
            <div>
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|Sensor will compensate for pressure differences based on altitude.", "Sensor will compensate for pressure differences based on altitude.")%>
            </div>
            <div>
                <hr />
                <%: Html.TranslateTag("Current Reading is", "Current Reading is")%> <%: Model.LastDataMessage != null ? lastValues[0].ToDouble().ToString("#0.#") + " " + Html.TranslateTag("ppm", "ppm") : Html.TranslateTag("Unavailable", "Unavailable") %>
                <input type="hidden" name="observedAltitude" value="<%:lastValues[0]%>" />
            </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Set Actual", "Set Actual")%> : 
                </div>
                <div class="col sensorEditFormInput">
                    <%: Html.TextBox("altitude", (Dictionary<string, object>)ViewData["HtmlAttributes"])%> <%: Html.TranslateTag("ppm", "ppm")%>
                </div>
            </div>
        </div>
        <%} %>

        <div id="cal-3" style="display: none; margin-top: 15px;">
            <div>
            </div>
            <div>
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|To zero H2S calibration, click \"Calibrate\".", "To zero H2S calibration, click \"Calibrate\".")%>
            </div>
        </div>
    </div>
    <div id="buttonsDiv">
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
        </div>
    <%} %>  
</form>

<%}
	else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>

<div class="formBody" style="font-weight: bold">
    <div>
        <%: Html.TranslateTag("This sensor has been pre-calibrated in a certified gas chamber.", "This sensor has been pre-calibrated in a certified gas chamber.")%>
    </div>
    <br />
    <div>
        <%: Html.TranslateTag("If you have a certified gas chamber and would like to perform additional calibrations please contact support.", "If you have a certified gas chamber and would like to perform additional calibrations please contact support.")%>
    </div>
</div>
<div class="buttons">&nbsp; </div>
<%}
    }%>


<script type="text/javascript">

    $('#calibrateButton').prop('disabled', true);
    $('#buttonsDiv').hide();
    $(document).ready(function () {



        $("input[name=calType]").on("change", function () {

            $('#cal-1').hide();
            $('#cal-2').hide();
            $('#cal-3').hide();
            $("#cal-" + $(this).val()).show();
            $('#buttonsDiv').show();
            $('#calibrateButton').prop('disabled', false);
        });


    });

    $("#actual").change(function () {
        if (!isANumber($("#actual").val()))
            $("#actual").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0")) : "" %>);
    });

    $("#altitude").change(function () {
        if (!isANumber($("#altitude").val())) {
            $("#altitude").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.0")) : "" %>);
            } else {

                if (parseFloat($("#altitude").val()) < 0.1) {
                    $("#altitude").val(0.1)
                }

                if (parseFloat($("#altitude").val()) > 50.0) {
                    $("#altitude").val(50.0)
                }

            }

    });

    $('#altitude, #actualTemp').addClass('form-control');

</script>
