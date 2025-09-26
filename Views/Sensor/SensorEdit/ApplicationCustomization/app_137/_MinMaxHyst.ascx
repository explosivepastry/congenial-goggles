<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Phase123ThreshMin = "";
    string Phase123ThreshMax = "";
    string Phase123Hyst = "";
    string Phase123Duty = "";
    string label = "";

    Phase123ThreshMin = (ThreePhase20AmpMeter.GetPhase1ThreshMin(Model) / 10.0).ToString();
    Phase123ThreshMax = (ThreePhase20AmpMeter.GetPhase1ThreshMax(Model) / 10.0).ToString();
    Phase123Hyst = (ThreePhase20AmpMeter.GetPhase1Hyst(Model) / 10.0).ToString();
    Phase123Duty = (ThreePhase20AmpMeter.GetPhase1Duty(Model) / 10.0).ToString();
    label = ThreePhase20AmpMeter.GetLabel(Model.SensorID).ToString();
%>

<br />
<p class="useAwareState"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_109|All Phases","All Phases")%>  - Aware State Threshold</p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (<%=Html.Label("Amps")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Phase123Min_Manual" id="Phase123Min_Manual" value="<%=Phase123ThreshMin %>" />
        <a id="phaseMin" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_109|Above","Above")%> (<%=Html.Label("Amps")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Phase123Max_Manual" id="Phase123Max_Manual" value="<%:Phase123ThreshMax%>" />
        <a id="phaseMax" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MaximumThreshold)%>
    </div>
</div>

<!-- Hyst -->
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Aware State Buffer","Aware State Buffer")%> (<%=Html.Label("Amps")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Phase123Hyst_Manual" id="Phase123Hyst_Manual" value="<%:Phase123Hyst%>" />
        <a id="phaseHyst" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Duty Threshold","Duty Threshold")%> (<%=Html.Label("Amps")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input class="form-control" type="number" <%=Model.CanUpdate ? "" : "disabled" %> name="Phase123Duty_Manual" id="Phase123Duty_Manual" value="<%:Phase123Duty%>" />
        <a id="phaseDuty" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">

    $("#Phase123Min_Manual").addClass('form-control');
    $("#Phase123Max_Manual").addClass('form-control');
    $("#Phase123Hyst_Manual").addClass('form-control');
    $("#Phase123Duty_Manual").addClass('form-control');

    $(function () {

          <% if (Model.CanUpdate)
    { %>

        const arrayForSpinner = arrayBuilder(0, 25, 1);
        createSpinnerModal("phaseMin", "Amps", "Phase123Min_Manual", arrayForSpinner);
        createSpinnerModal("phaseMax", "Amps", "Phase123Max_Manual", arrayForSpinner);
        createSpinnerModal("phaseHyst", "Amps", "Phase123Hyst_Manual", arrayForSpinner);
        createSpinnerModal("phaseDuty", "Amps", "Phase123Duty_Manual", arrayForSpinner);

        <%}%>


        $("#Phase123Min_Manual").change(function () {
            if (isANumber($("#Phase123Min_Manual").val())) {

                if ($("#Phase123Min_Manual").val() < 0)
                    $("#Phase123Min_Manual").val(0);

                if ($("#Phase123Min_Manual").val() > 25)
                    $("#Phase123Min_Manual").val(25);

                if (parseFloat($("#Phase123Min_Manual").val()) > parseFloat($("#Phase123Max_Manual").val()))
                    $("#Phase123Min_Manual").val(parseFloat($("#Phase123Max_Manual").val()));
            }
            else {
                $('#Phase123Min_Manual').val(<%: Phase123ThreshMin%>);
            }
        });


        $("#Phase123Max_Manual").change(function () {
            if (isANumber($("#Phase123Max_Manual").val())) {
                if ($("#Phase123Max_Manual").val() < 0)
                    $("#Phase123Max_Manual").val(0);

                if ($("#Phase123Max_Manual").val() > 25)
                    $("#Phase123Max_Manual").val(25);

                if (parseFloat($("#Phase123Max_Manual").val()) < parseFloat($("#Phase123Min_Manual").val()))
                    $("#Phase123Max_Manual").val(parseFloat($("#Phase123Min_Manual").val()));
                //$("#Phase123Min_Manual").change();
            }
            else {
                $('#Phase123Max_Manual').val(<%: Phase123ThreshMax%>);

            }
        });


        $("#Phase123Hyst_Manual").change(function () {

            var hystNum = ((Number($("#Phase123Max_Manual").val()) - Number($("#Phase123Min_Manual").val())) * 0.25);

            if (isANumber($("#Phase123Hyst_Manual").val())) {
                if ($("#Phase123Hyst_Manual").val() < 0)
                    $("#Phase123Hyst_Manual").val(0);
                if ($("#Phase123Hyst_Manual").val() > hystNum)
                    $("#Phase123Hyst_Manual").val(hystNum)
            }
            else {
                $('#Phase123Hyst_Manual').val(<%: Phase123Hyst%>);
            }
        });


        $("#Phase123Duty_Manual").change(function () {
            if (isANumber($("#Phase123Duty_Manual").val())) {
                if ($("#Phase123Duty_Manual").val() < 0)
                    $("#Phase123Duty_Manual").val(0);
                if ($("#Phase123Duty_Manual").val() > 25)
                    $("#Phase123Duty_Manual").val(25)

            }
            else {
                $('#Phase123Duty_Manual').val(<%: Phase123Hyst%>);
            }
        });

    });

</script>
