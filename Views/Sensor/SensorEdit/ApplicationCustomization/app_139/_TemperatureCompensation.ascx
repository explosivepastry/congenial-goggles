<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<%bool EnableTemperatureCompensation = LightSensor_PPFD.GetEnableTempCalibration(Model).ToBool();%>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Enable Temperature Compensation","Enable Temperature Compensation")%>
    </div>
    <div class="col sensorEditFormInput">
        <div class="form-check form-switch d-flex align-items-center ps-0">
            <label id="tempCompOff" class="form-check-label"><%: Html.TranslateTag("Off","Off")%></label>
            <label id="tempCompOn" class="form-check-label"><%: Html.TranslateTag("On","On")%></label>
            <input class="form-check-input my-0 y-0 mx-2" type="checkbox" <%=Model.CanUpdate ? "" : "disabled" %> name="Calibration3_Manual" id="Calibration3_Manual" <%=(EnableTemperatureCompensation) ? "checked" : "" %> onclick=" tempCompensationToggle()">
        </div>
        <div style="display: none;"><%: Html.TextBoxFor(model => EnableTemperatureCompensation, (Dictionary<string,object>)ViewData["HtmlAttributes"])%></div>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {

        //setTimeout(setTempCompensationButton, 500);
        $('#Calibration3_Manual').change(function () {
            if ($('#Calibration3_Manual').prop('checked')) {
                $('#EnableTemperatureCompensation').val(true);
            } else {
                $('#EnableTemperatureCompensation').val(false);
            }
        });
    });
    function tempCompensationToggle() {
        if ($('#Calibration3_Manual').is(':checked')) {
            $('#tempCompOff').hide();
        } else {
            $('#tempCompOff').show();
        }
        if ($('#Calibration3_Manual').is(':checked') === false) {
            $('#tempCompOn').hide();
        } else {
            $('#tempCompOn').show();
        }
    }
    tempCompensationToggle()

</script>
