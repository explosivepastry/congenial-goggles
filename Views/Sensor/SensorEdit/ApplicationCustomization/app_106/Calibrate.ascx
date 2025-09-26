<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

  TempData["CanCalibrate"] = true;

  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);                                                                                                                       
  Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

 <% if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	 {%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="Form1" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="Hidden1" />

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{ %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>
       
    <%string[] lastValues = null; %>
    <%if (Model.LastDataMessage != null)
		{%>
    <%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("AdvancedCalibration")))
		{%>
    <div class="form-group">

        <div class="bold col-12">
            <b><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|Important", "Important")%></b>:  <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|Perform these steps in the order indicated.", "Perform these steps in the order indicated.")%>
            <div class="clear"></div>
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|1. Altitude Calibration: Select the Altitude calibration, enter the altitude the sensor will operate at then press the  Calibrate button. Wait for a full two data points to come in after this command is accepted(red x on status icon clears) for this process to complete. Do not send any other calibration or configuration changes during this period.")%>
            <div class="clear"></div>
            <br />
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|2. Fresh Air Calibration: Press the calibrate button. The sensor must be in an outside fresh air environment for a full 15 minutes before performing this step. Keep the sensor in the fresh air environment and wait for a full two data points to come in after this command is accepted(red x on status icon clears) for this process to complete, do not send any other calibration or configuration changes before this process completes. After this step is complete the sensor should read near 400 ppm. ")%>
           
            <div class="clear"></div>
            <br />
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|3. Scalar Calibration", "3. Scalar Calibration.")%>
            <div class="clear"></div>
            <br />
             
        </div>
    </div>
    <div class="clear"></div>
    <hr />

    <%} %>

    <div class="formBody">
        <input type="radio" name="calType" id="calType2" value="2">
        <label for="calType2" >Altitude Calibration</label>
        
        <%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("AdvancedCalibration")))
		{%>
        <input type="radio" name="calType" id="calType3" value="3">
        <label for="calType3" >Nitrogen Calibration</label>
        <%} %>
        
        <input type="radio" name="calType" id="calType1" value="1">
        <label for="calType1" >Fresh Air Calibration </label>

        <%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("AdvancedCalibration")))
        { %>
        <input type="radio" name="calType" id="calType4" value ="4">
        <label for="calType4" > C02 Scalar Calibration </label>      
        <%} %>


        <div id="cal-1" style="display: none; margin-top: 15px;">

           <% lastValues = Model.LastDataMessage.Data.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries); %>
           <div>
               <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|Calibrates the sensor based on a previously measured point or the user input. ", "Calibrates the sensor based on a previously measured point or the user input. ")%>
               <div class="clear"></div>
               <br />
               <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|This step creates an offset to adjust the sensor to a known actual value.", "This step creates an offset to adjust the sensor to a known actual value.")%>
               <div class="clear"></div>
               <br />
               <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_106|Altitude calibration must be performed prior to fresh air calibration. Altitude and fresh air calibration should be performed in the same environment and at the same altitude.", "Altitude calibration must be performed prior to fresh air calibration. Altitude and fresh air calibration should be performed in the same environment and at the same altitude.")%>
               <hr />
           </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Current Offset","Current Offset")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input type="text" class="form-control" value="<%: CO2Meter.GetC02Offset(Model) %>" readonly /> ppm
                    <input type="button" class="btn btn-secondary btn-sm ms-2" id="ZeroOffset" value="Zero Offset" />
                </div>
            </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Measured reading","Measured reading")%>
                </div>
                <div class="col sensorEditFormInput">
                    <input id="observed" name="observed" class="form-control" type="text" readonly value="<%:lastValues[0] %>"> ppm
                </div>
            </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag("Actual reading is","Actual reading is")%>
                </div>
                <div class="col sensorEditFormInput">
                    <%: Html.TextBox("actual", 400, (Dictionary<string, object>)ViewData["HtmlAttributes"])%> ppm
                </div>
            </div>
       </div>
        <div id="cal-2" style="display: none; margin-top: 15px;">
            <div>
                Sensor will compensate for pressure differences based on altitude.
            </div>
            <div>
                <hr />
                <%int altitude = CO2Meter.GetAltitude(Model); %>
                Current Altitude is <%: Model.LastDataMessage != null ? Convert.ToString(altitude) : "Unavailable" %> ft
                <input type="hidden" name="observedAltitude" value="<%:altitude%>" />
            </div>
            <div class="row sensorEditForm">
                <div class="col-12 col-md-3">
                    <%: Html.TranslateTag(" Set Altitude"," Set Altitude")%>
                </div>
                <div class="col sensorEditFormInput">
                    <%: Html.TextBox("altitude", (Dictionary<string, object>)ViewData["HtmlAttributes"])%> ft
                </div>
            </div>
        </div>

        <%if (MonnitSession.CurrentCustomer.Can(CustomerPermissionType.Find("AdvancedCalibration")))
		{%>
        <div id="cal-3" style="display: none; margin-top: 15px;">
            <div>
            </div>
            <div>
                For nitrogen calibration, click "Calibrate".
            </div>
        </div>

         <div id="cal-4" style="display: none; margin-top: 15px;">

            <div>
                Perform Altitude Calibration and Fresh Air Calibration before performing this calibration step. 
               <div class="clear"></div>

            </div>
            <div>
                Measured reading <%: Html.TextBox("observedC02",lastValues[0],(Dictionary<string,object>)ViewData["HtmlAttributes"])%> ppm
  
            </div>
            <div>
                Actual reading is <%: Html.TextBox("actualC02",(Dictionary<string,object>)ViewData["HtmlAttributes"])%> ppm              
            </div>
        </div>
        <%} %>
    </div>
     <div class="clearfix"></div>
    </br>
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>

</form>

<script type="text/javascript">

    $(document).ready(function () {
        $("input[name=calType]").on("change", function () {

            $('#cal-1').hide();
            $('#cal-2').hide();
            $('#cal-3').hide();
			$('#cal-4').hide();
            $("#cal-" + $(this).val()).show();
        });


    });

    $('#actual').addClass('form-control');
    $('#altitude').addClass('form-control');

    $("#actual").change(function () {
        if (!isANumber($("#actual").val()))
            $("#actual").val(<%: Model.LastDataMessage != null ? (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
    });

    $("#altitude").change(function () {
        if (!isANumber($("#altitude").val()))
            $("#altitude").val(<%: Model.LastDataMessage != null ? (Model.LastDataMessage.AppBase.PlotValue.ToDouble().ToString("0.00")) : "" %>);
    });

    var ZeroSpanSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to set your calibration back to default?")%>";
    $("#ZeroOffset").click(function () {

        confirmCustom(ZeroSpanSure, function (result) {
            if (result == true) {
                $.post("/Overview/SensorCalibrate/<%:Model.SensorID %>", { calType: 0 }, function (data) {
                    $('#Form1').html(data);
                });
            }
        });
    })
</script>
<%}
	}%>