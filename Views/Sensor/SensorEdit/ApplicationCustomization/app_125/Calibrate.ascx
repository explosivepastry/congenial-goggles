<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

	TempData["CanCalibrate"] = true;

	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
	<input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

	<%: Html.ValidationSummary(false)%>
	<%: Html.Hidden("id", Model.SensorID)%>
	<%  if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{%>

	<a class="helpIco" data-toggle="modal" title="<%: Html.TranslateTag("Overview/SensorEdit|Sensor Help","Sensor Help") %>" data-target=".calibrateHelp"><i class="fa fa-question-circle-o "></i></a>

	<div class="formBody">
		<div class="form-inline">
			<span>Pick specific type of Calibration: </span>
			<span>
				<select id="CalibrationType" name="CalibrationType">
					<option value="4">Fan Override</option>
					<option value="6">Occupancy Override</option>
					<option value="7">Unoccupancy Override</option>
					<option value="10">Temperature Offset</option>
					<option value="11">Humidity Offset</option>
				</select>
			</span>
		</div>
		<div class="mdbox form-inline">
			<div id="myValueContainer">
				Duration:
                <input type='text' id='myval' name='myval' />
				Minutes
			</div>
		</div>
		<div class="clearfix"></div>
		<br />
		<% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
	</div>


	<!-- help button modal -->
	<div class="modal fade calibrateHelp" tabindex="-1" role="dialog" aria-hidden="true">
		<div class="modal-dialog modal-sm">
			<div class="modal-content">

				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal" aria-label="Close">
						<span aria-hidden="true">×</span>
					</button>
					<h4 class="modal-title" id="myModalLabel2"><%: Html.TranslateTag("Overview/SensorCalibrate|Calibrate Sensor","Sensor Calibration")%></h4>
				</div>
				<div class="modal-body">

					<div class="form-inline">
						<div class="word-choice">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Fan Override","Fan Override")%>
						</div>
						<div class="word-def">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Turns fan on for a set period of time.","Turns fan on for a set period of time.")%>
							<hr />
						</div>
					</div>

					<div class="form-inline">
						<div class="word-choice">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Occupancy Override","Occupancy Override")%>
						</div>
						<div class="word-def">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This command will force the thermostat to enter the occupied state and use the Occupied Temperature Settings for selected duration.","This command will force the thermostat to enter the occupied state and use the Occupied Temperature Settings for selected duration.")%>
							<hr />
						</div>
					</div>

					<div class="form-inline">
						<div class="word-choice">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Unoccupancy Override","Unoccupancy Override")%>
						</div>
						<div class="word-def">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|This command forces the thermostat to enter the unoccupied state and use the Unoccupied Temperature Settings for the selected duration.","This command forces the thermostat to enter the unoccupied state and use the Unoccupied Temperature Settings for the selected duration.")%>
							<hr />						</div>
					</div>

					<div class="form-inline">
						<div class="word-choice">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Temperature Offset","Temperature Offset")%>
						</div>
						<div class="word-def">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Adjusts temperature readings by the Offset Value. Example: Offset of 2 C will change a 20 C reading to a 22 C reading.","Adjusts temperature readings by the Offset Value. Example: Offset of 2 C will change a 20 C reading to a 22 C reading.")%>
							<hr />
						</div>
					</div>

					<div class="form-inline">
						<div class="word-choice">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Humidity Offset","Humidity Offset")%>
						</div>
						<div class="word-def">
							<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Adjusts humidity readings by the Offset Value. Example: Offset of 2% will change a 20% reading to a 22% reading. Has no effect if humidity hardware option not installed.","Adjusts humidity readings by the Offset Value. Example: Offset of 2% will change a 20% reading to a 22% reading. Has no effect if humidity hardware option not installed.")%>
							<hr />
						</div>
					</div>

				</div>
			</div>
		</div>
	</div>
	<!-- End help button modal -->

	<script>

        var defaultSure = "<%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Are you sure you want to set your calibration back to default?")%>";
        $(function () {

            $('#CalibrationType').addClass('editField editFieldMedium');
            $('#myval').addClass('editField editFieldMedium');

            $("#CalibrationType").change(function () {

                var type = parseInt($(this).val());

                switch (type) {
                    case 4:
                        var body = "Duration: <input  type='text' id='myval' name='myval'/> Minutes";

                        $('#myValueContainer').html(body);
                        break;
                    case 6:
                    case 7:
                        var body = "Duration: <select id='myval' name='myval'><option value='5'>5 minutes</option><option value='10'>10 minutes</option><option value='15'>15 minutes</option><option value='30'>30 minutes</option><option value='60'>1 hour</option><option value='90'>1 hour and 30 minutes</option><option value='120'>2 hours</option><option value='240'>4 hours</option><option value='480'>8 hours</option><option value='720'>12 hours</option></select> ";
                        $('#myValueContainer').html(body);
                        break;
                    case 10:
                        var body = "Offset Value: <input  type='text' id='myval' name='myval' value='<%= MultiStageThermostat.IsFahrenheit(Model.SensorID) ? Thermostat.GetTemperatureOffset(Model) * 1.8 :MultiStageThermostat.GetTemperatureOffset(Model)%>'/> " + '<%= MultiStageThermostat.IsFahrenheit(Model.SensorID) ? "F":"C"%>';
                        $('#myValueContainer').html(body);
                        break;
                    case 11:
                        var body = "Offset Value: <input type='text' id='myval' name='myval' value='<%=MultiStageThermostat.GetHumidityOffset(Model)%>'/> %";
                        $('#myValueContainer').html(body);
                        break;
                }
            });

            $("#myval").change(function () {
                if (!isANumber($("#myval").val())) {
                    showSimpleMessageModal("<%=Html.TranslateTag("The value must be a number. Setting to five minutes")%>");
                    $("#myval").val(5);
                }
            });

            $('#saveCalibrate').on("click", function () {
                var caltype = $('#CalibrationType').val();
                var canSave = false;
                if (caltype != 10 || caltype != 11)
                    canSave = true;
                else {
                    var myVal = $("#myval").val();

                    if (caltype == 10) {

                        var isFarh = <%=  MultiStageThermostat.IsFahrenheit(Model.SensorID)%>;

                        if (isFarh) {

                            if ((myVal >= <%= (-12.8).ToFahrenheit()%>) || (myVal <= <%= (12.7).ToFahrenheit() %>))
                                canSave = true;
                        }
                        else {
                            if (myVal >= -12.8 || myVal <= 12.7)
                                canSave = true;
                        }
                    }
                    else {
                        if (myVal >= -12.8 || myVal <= 12.7)
                            canSave = true;
                    }
                }

                if (canSave) {
                    postForm($(this).closest('form'));
                }
                else {
                    showSimpleMessageModal("<%=Html.TranslateTag("Unable to save")%>");
                }
            });

            $('#DefaultsCalibrate1').on("click", function () {

                var returnUrl = $('#returns').val();
                var SensorID = '<%: Model.SensorID%>';
                var pID = $('#simpleCalibrate_' + SensorID).parent();




                if (confirm(defaultSure)) {
                    $.post("/Sensor/SetDefaultCalibration", { id: SensorID, url: returnUrl }, function (data) {

                        pID.html(data);
                    });
                }

            });
        });
    </script>


</form>


<%
	}
%>


<%}
	else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow))
	{%>
<div class="formBody">
	<div>
		This sensor has been pre-calibrated and certified by <%: CalibrationFacility.Load(Model.CalibrationFacilityID).Name %>.
	</div>
	<br />

	<div>
		<a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf">View Calibration Certificate</a>
	</div>
</div>
<%}%>
