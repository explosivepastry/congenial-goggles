<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>

<% 
    string Phase123ThreshMin = "";
    string Phase123ThreshMax = "";
    string Phase123Hyst = "";
    string Phase123Duty = "";
    string label = "";

    Phase123ThreshMin = ThreePhaseCurrentMeter.GetPhase1ThreshMin(Model).ToString();
    Phase123ThreshMax = ThreePhaseCurrentMeter.GetPhase1ThreshMax(Model).ToString();
    Phase123Hyst = ThreePhaseCurrentMeter.GetPhase1Hyst(Model).ToString();
    Phase123Duty = ThreePhaseCurrentMeter.GetPhase1Duty(Model).ToString();
    label = ThreePhaseCurrentMeter.GetLabel(Model.SensorID).ToString();
%>

<br />
<p class="useAwareState"><%: Html.TranslateTag("Sensor/ApplicationCustomization/app_109|All Phases","All Phases")%>  - Aware State Threshold</p>
<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%-- <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below")%> (<%: label %>)--%>
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Below","Below ")%> (<%=Html.Label("Amps")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Phase123Min_Manual" id="Phase123Min_Manual" value="<%=Phase123ThreshMin %>" />
        <a id="phaseMin" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.MinimumThreshold )%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_109|Above","Above ")%> (<%=Html.Label("Amps")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Phase123Max_Manual" id="Phase123Max_Manual" value="<%:Phase123ThreshMax%>" />
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
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Phase123Hyst_Manual" id="Phase123Hyst_Manual" value="<%:Phase123Hyst%>" />
        <a id="phaseHyst" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
        <%: Html.ValidationMessageFor(model => model.Hysteresis)%>
    </div>
</div>

<div class="row sensorEditForm">
    <div class="col-12 col-md-3">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/Default|Duty Threshold","Duty Threshold")%> (<%=Html.Label("Amps")%>)
    </div>
    <div class="col sensorEditFormInput">
        <input type="number" class="form-control" <%=Model.CanUpdate ? "" : "disabled" %> name="Phase123Duty_Manual" id="Phase123Duty_Manual" value="<%:Phase123Duty%>" />
        <a id="phaseDuty" style="cursor: pointer;"><%=Html.GetThemedSVG("list") %></a>
    </div>
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $('input[name="Phase123Min_Manual"]').blur(function () {
            $(this).val(Math.round($(this).val()));
        });
    });

    $(document).ready(function () {
        $('input[name="Phase123Max_Manual"]').blur(function () {
            $(this).val(Math.round($(this).val()));
        });
    });


    $(document).ready(function () {
        $('input[name="Phase123Hyst_Manual"]').blur(function () {
            $(this).val(Math.round($(this).val()));
        });
    });

    $(document).ready(function () {
        $('input[name="Phase123Duty_Manual"]').blur(function () {
            $(this).val(Math.round($(this).val()));
        });
    });

    $("#Phase123Min_Manual").addClass('editField editFieldSmall');
    $("#Phase123Max_Manual").addClass('editField editFieldSmall');
    $("#Phase123Hyst_Manual").addClass('editField editFieldSmall');
    $("#Phase123Duty_Manual").addClass('editField editFieldSmall');

    $(function () {

          <% if (Model.CanUpdate)
    { %>
        const arrayForSpinner = arrayBuilder(0, 255, 1);
        createSpinnerModal("phaseMin", "<%=Html.TranslateTag("Amps") %>", "Phase123Min_Manual", arrayForSpinner);
        createSpinnerModal("phaseMax", "<%=Html.TranslateTag("Amps") %>", "Phase123Max_Manual", arrayForSpinner);
        createSpinnerModal("phaseHyst", "<%=Html.TranslateTag("Amps") %>", "Phase123Hyst_Manual", arrayForSpinner);
        createSpinnerModal("phaseDuty", "<%=Html.TranslateTag("Amps") %>", "Phase123Duty_Manual", arrayForSpinner);

        <%}%>


        $("#Phase123Min_Manual").change(function () {
            if (isANumber($("#Phase123Min_Manual").val())) {

                if ($("#Phase123Min_Manual").val() < 0)
                    $("#Phase123Min_Manual").val(0);

                if ($("#Phase123Min_Manual").val() > 255)
                    $("#Phase123Min_Manual").val(255);

                if (parseFloat($("#Phase123Min_Manual").val()) > parseFloat($("#Phase123Max_Manual").val()))
                    $("#Phase123Min_Manual").val(parseFloat($("#Phase123Max_Manual").val()));
                $("#Phase123Max_Manual").change();
            }
            else {
                $('#Phase123Min_Manual').val(<%: Phase123ThreshMin%>);
            }
        });


        $("#Phase123Max_Manual").change(function () {
            if (isANumber($("#Phase123Max_Manual").val())) {
                if ($("#Phase123Max_Manual").val() < 0)
                    $("#Phase123Max_Manual").val(0);

                if ($("#Phase123Max_Manual").val() > 255)
                    $("#Phase123Max_Manual").val(255);

                if (parseFloat($("#Phase123Max_Manual").val()) < parseFloat($("#Phase123Min_Manual").val()))
                    $("#Phase123Max_Manual").val(parseFloat($("#Phase123Min_Manual").val()));
                $("#Phase123Min_Manual").change();
            }
            else {
                $('#Phase123Max_Manual').val(<%: Phase123ThreshMax%>);

            }
        });


        $("#Phase123Hyst_Manual").change(function () {
            if (isANumber($("#Phase123Hyst_Manual").val())) {
                if ($("#Phase123Hyst_Manual").val() < 0)
                    $("#Phase123Hyst_Manual").val(0);
                if ($("#Phase123Hyst_Manual").val() > 255)
                    $("#Phase123Hyst_Manual").val(255)
            }
            else {
                $('#Phase123Hyst_Manual').val(<%: Phase123Hyst%>);
            }
        });


        $("#Phase123Duty_Manual").change(function () {
            if (isANumber($("#Phase123Duty_Manual").val())) {
                if ($("#Phase123Duty_Manual").val() < 0)
                    $("#Phase123Duty_Manual").val(0);
                if ($("#Phase123Duty_Manual").val() > 255)
                    $("#Phase123Duty_Manual").val(255)

            }
            else {
                $('#Phase123Duty_Manual').val(<%: Phase123Hyst%>);
            }

        });

    });

</script>
