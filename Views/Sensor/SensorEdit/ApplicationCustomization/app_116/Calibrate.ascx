<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>

<%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{ %>

<% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

<%} %>



<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <% if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{
			string[] lastValues = null;
			if (Model.LastDataMessage != null)
			{%>
    <div class="form-group">
        <div class="col-md-1 col-sm-1 col-xs-12 mdBox">
        </div>
        <div class="bold col-md-11 col-sm-11 col-xs-12">
            <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_116|Important", "Important")%></b>: <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_116|Perform these steps in the order indicated.", "Perform these steps in the order indicated.")%>
        </div>

    </div>

    <div class="form-group">
       
        <div class="bold col-md-11 col-sm-11 col-xs-12">
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_116|1. Zero Calibration: Press the zero button.  Keep the sensor in the 0 PPM environment and wait for a full two data points to come in after this command is accepted(red x on status icon clears) for this process to complete, do not send any other calibration or configuration changes before this process completes. After this step is complete the sensor should read near 0 ppm")%>
            <div class="clear"></div>
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_116|2. CO Span Calibration: This step calibrates the sensor according to a ppm reading above 100 ppm. It is recommended to calibrate the sensor in an environment above maximum expected CO concentration in the environment. This step is complete as soon as the red x clears.")%>
            <div class="clear"></div>
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_056|Temperature Calibration: Submit actual Temperature")%>
        </div>

    </div>
    <div class="clear"></div>
    <br />



    <div class="formBody">
        <input type="radio" name="calType" value="2">
        <%: Html.TranslateTag("Temperature Calibration", "Temperature Calibration")%>
        <input type="radio" name="calType" value="3">
        <%: Html.TranslateTag("Zero Calibration", "Zero Calibration")%>
        <input type="radio" name="calType" value="1">
        <%: Html.TranslateTag("Span Calibration", "Span Calibration")%>
        <br>

        <div id="cal-1" style="display: none; margin-top: 15px;">

            <% lastValues = Model.LastDataMessage.Data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); %>
            <div>
                <%: Html.TranslateTag("Adjusts the CO Calibration so that the reading equals the correct CO measurement.", "Adjusts the CO Calibration so that the reading equals the correct CO measurement.")%>

                <hr />
            </div>
            <div>
                <%: Html.TranslateTag("Measured reading", "Measured reading")%> <%: Html.TextBox("observed", lastValues[0], (Dictionary<string, object>)ViewData["HtmlAttributes"])%> ppm
  
            </div>
            <div>
                <%: Html.TranslateTag("Actual reading is", "Actual reading is")%> <%: Html.TextBox("actual", (Dictionary<string, object>)ViewData["HtmlAttributes"])%> ppm              
            </div>
        </div>
        <div id="cal-2" style="display: none; margin-top: 15px;">
            <div>
                <hr />
                <%: Html.TranslateTag("Current Temperature is", "Current Temperature is")%> <%: Model.LastDataMessage != null ? Convert.ToString(lastValues[2].ToDouble()) : "Unavailable" %> C
                <input type="hidden" name="observedTemp" value="<%:lastValues[2].ToDouble()%>" />
            </div>
            <div>
                <%: Html.TranslateTag("Set Temperature", "Set Temperature")%> <%: Html.TextBox("actualTemp", (Dictionary<string, object>)ViewData["HtmlAttributes"])%> C
            </div>
        </div>

        <div id="cal-3" style="display: none; margin-top: 15px;">
            <div>
            </div>
            <div>
                <%: Html.TranslateTag("To zero calibration, click \"Calibrate\".", "To zero calibration, click \"Calibrate\".")%>
            </div>
        </div>
    </div>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
    <%} %>
    <%else
		{%>
    <div><%: Html.TranslateTag("Calibration is not available until sensor has sent data to server.", "Calibration is not available until sensor has sent data to server.")%></div>
    <div class="buttons">&nbsp; </div>
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

    $('#actual').addClass('editField editFieldMedium');
    $('#altitude').addClass('editField editFieldMedium');

    $("#actual").change(function () {
        if (!isANumber($("#actual").val()))
            $("#actual").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
    });

    $("#actualTemp").change(function () {
        if (!isANumber($("#actualTemp").val()))
            $("#actualTemp").val(<%: Model.LastDataMessage != null ?  (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
    });

</script>
