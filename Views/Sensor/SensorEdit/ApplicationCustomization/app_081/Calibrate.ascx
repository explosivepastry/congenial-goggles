<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%    DateTime CalibrationCertificationValidUntil = Model.GetCalibrationCertificationValidUntil();

	TempData["CanCalibrate"] = true;

	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationNoDataError.ascx", Model);
	Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_CalibrationPendingError.ascx", Model);%>

<%if (string.IsNullOrEmpty(Model.CalibrationCertification) || CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
	{
		List<DataMessage> dmList = DataMessage.LoadBySensorAndDateRange(Model.SensorID, Model.LastCommunicationDate.AddMinutes(-5), Model.LastCommunicationDate, 5000, null);

		string current = string.Empty;
		string volt = string.Empty;

		foreach (var item in dmList)
		{
			string[] tempData = item.Data.Split('|');
			if (tempData[0].ToInt() == 0)
			{
				volt = tempData[3];
				current = tempData[4];
			}
		}

      %>

<form action="/Overview/SensorCalibrate/<%:Model.SensorID %>" id="simpleCalibrate_<%:Model.SensorID %>" method="post">
    <input type="hidden" value="/Overview/SensorCalibrate/<%:Model.SensorID %>" name="returns" id="returns" />

     <%if (!string.IsNullOrEmpty(Model.CalibrationCertification) && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		 { %>

    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/_CalibrationExperationNote.ascx", Model);%>

    <%} %>

    <%: Html.ValidationSummary(false)%>
    <%: Html.Hidden("id", Model.SensorID)%>
    <% if (TempData["CanCalibrate"].ToBool() && CalibrationCertificationValidUntil < MonnitSession.MakeLocal(DateTime.UtcNow))
		{%>

    <div class="formBody">   
           
        <div style="float:left">
             <input type="hidden" id="acc" />
            <select id="optionSelect">
                <option value="#zerovoltage"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Zero","Zero")%></option>
                <option value="#voltageContainer"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Calibrate AC RMS Voltage","Calibrate AC RMS Voltage")%></option>
                <option value="#currentContainer"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Calibrate AC RMS Current","Calibrate AC RMS Current")%></option>
            </select>
        </div>


        <div id="zerovoltage" style="float:right">
            <div style="width: 582px;">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|With no voltage or current connected to the sensor press the Zero Voltage button.","With no voltage or current connected to the sensor press the Zero Voltage button.")%><br />
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Keep the sensor in a no voltage condition till the sensor checks in.","Keep the sensor in a no voltage condition till the sensor checks in.")%><br />
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Zeroing the voltage sets the zero or neutral voltage point on the sensor.","Zeroing the voltage sets the zero or neutral voltage point on the sensor.")%><br />
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Not setting this may result in a slight voltage offset resulting less accurate readings.","Not setting this may result in a slight voltage offset resulting less accurate readings.")%><br />
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Zero the voltage and current inputs before calibration for best accuracy.","Zero the voltage and current inputs before calibration for best accuracy.")%>
            </div>
            <div style="margin: 0px 150px 0px 0px;">
            <input type="button" class="btn btn-primary" id="Zero" onclick="postForm($(this).closest('form'));" value="Zero Voltage" />
            </div> 
            <div style="clear: both;"></div>
        </div>


        <div id="voltageContainer" ">
            <div class="editor-label calOptions" style="margin: -28px -252px 0px 256px;">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Enter the Expected RMS Voltage and press the calibrate button. Wait till the sensors checks in for the calibration process to complete.","Enter the Expected RMS Voltage and press the calibrate button. Wait till the sensors checks in for the calibration process to complete.")%>
            </div>
            <div class="editor-field calOptions">
                 <input type="text" value="<%: volt %>" readonly="readonly" /> Volts<br /><br />
                <input name="actual" id="voltage" type="text" />Volts
            </div>
             <div style="clear: both;"></div>
        </div>


        <div id="currentContainer">
            <div class="editor-label calOptions" style="margin: -28px -252px 0px 256px;">
              <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_081|Enter the Expected RMS Curent and press the calibrate button. Wait till the sensors checks in for the calibration process to complete.","Enter the Expected RMS Curent and press the calibrate button. Wait till the sensors checks in for the calibration process to complete.")%> 
            </div>
            <div class="editor-field calOptions" >
                <input type="text" value="<%: current %>" readonly="readonly" /> Amps<br /><br />
                <input name="actual" id="current" type="text" /> Amps
            </div>
             <div style="clear: both;"></div>
        </div>


        <div style="clear: both;"></div>
        </div>
        <script>
			$(function () {

				$('#acc').val(3);
				$('#voltageContainer').hide();
				$('#currentContainer').hide();

				$('#optionSelect').change(function () {

					var opt = $(this).val();
					$('#voltageContainer').hide();
					$('#currentContainer').hide();
					$('#zerovoltage').hide();

					switch (opt) {
						case '#voltage':
							$('#acc').val(5);
							break;
						case '#current':
							$('#acc').val(6);
							break;
						case '#zerovoltage':
							$('#acc').val(3);
							break;
					}
					$(opt).show();
				});
			});
        </script>
             
    <br />
    <% Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/Default/_SaveCalibrationButtons.ascx", Model);%>
</form>
<%}
	else if (CalibrationCertificationValidUntil >= MonnitSession.MakeLocal(DateTime.UtcNow))
	{
%>
<div class="formBody" style="font-weight: bold">
  <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Calibration for this sensor is not available for edit until pending transaction is complete.","Calibration for this sensor is not available for edit until pending transaction is complete.")%>
</div>
<div class="buttons">&nbsp; </div>
<%
	}
%>
    <br />

    <div>
        <a target="_blank" href="http://74.93.64.170/iportal/iportal_documents/certs/<%: new Regex("[^0-9-]").Replace(Model.CalibrationCertification, "") %>.pdf"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|View Calibration Certificate","View Calibration Certificate")%></a>
    </div>
<%}%>
