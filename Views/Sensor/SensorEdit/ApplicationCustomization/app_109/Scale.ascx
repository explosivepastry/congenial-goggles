<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />


    <%
        string volts = ThreePhaseCurrentMeter.GetVoltValue(Model.SensorID).ToString();
        string Hours = ThreePhaseCurrentMeter.GetLabel(Model.SensorID);
        double estimatedPowerFactor = ThreePhaseCurrentMeter.GetPowerFactor(Model.SensorID);
        int phaseSelection = ThreePhaseCurrentMeter.GetSelectedCurrentReading(Model.SensorID);
        int useEstimatedPower = ThreePhaseCurrentMeter.GetUseEstimatedPower(Model.SensorID).ToInt();
    %>

    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Label (default: Ah)","Label (default: Ah)")%>
        </div>
        <div class="col sensorEditFormInput">
            <select id="label" name="label" class="form-select">
                <option value="Ah" <%: Hours == "Ah"?"selected":"" %>>Amp Hours</option>
                <option value="MJ" <%: Hours == "MJ"?"selected":"" %>>MegaJoule</option>
                <option value="kWh" <%: Hours == "kWh"?"selected":"" %>>Kilowatt Hours</option>
            </select>
        </div>
    </div>
    <div class="clear"></div>
    <br />


    <div class="row sensorEditForm voltRow" id="voltRow">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Volts","Volts")%>
        </div>
        <div class="col sensorEditFormInput">
            <%: Html.TextBox("voltValue",volts) %>
        </div>
    </div>
    <div class="clear"></div>
    <br />
    <div class="row sensorEditForm voltRow" >
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Power Formula","Power Formula")%> (<%: Html.TranslateTag("Accumulate must be turned off for Estimated Power","Accumulate must be turned off for Estimated Power")%>)
        </div>
        <div class="col sensorEditFormInput">
            <div class="form-check form-switch d-flex align-items-center ps-0">
                <label class="form-check-label"><%: Html.TranslateTag("Estimated Power","Estimated Power")%></label>
                <input class="form-check-input my-0 y-0 mx-2" type="checkbox" name="APorEP_ManualChkBx" id="APorEP_ManualChkBx" <%=useEstimatedPower == 0 ? "checked" : "" %>>
                <label class="form-check-label"><%: Html.TranslateTag("Apparent Power","Apparent Power")%></label>
            </div>
            <div style="display: none;"><input type="hidden" name="APorEP_Manual" id="APorEP_Manual" value="<%=useEstimatedPower %>" /></div>
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="row sensorEditForm apDiv voltRow2" style="display: none;">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Phase for Power Estimate")%>
        </div>
        <div class="col sensorEditFormInput">
            <select id="SelectedPhase" name="SelectedPhase" class="form-select">
                <option value="0" <%: phaseSelection == 0 ? "selected":"" %>>All Phase Average</option>
                <option value="1" <%: phaseSelection == 1 ? "selected":"" %>>Phase 1</option>
                <option value="2" <%: phaseSelection == 2 ? "selected":"" %>>Phase 2</option>
                <option value="3" <%: phaseSelection == 3 ? "selected":"" %>>Phase 3</option>
            </select>
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="row sensorEditForm apDiv voltRow2" style="display: none;">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_093|Estimated Power Factor")%>
        </div>
        <div class="col sensorEditFormInput">
            <input type="number" name="PowerFactorValue" id="PowerFactorValue" min="0" value="<%=estimatedPowerFactor %>" />
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="col-md-12 col-xs-12">
        <span style="color: red;">
            <%: ViewBag.ErrorMessage == null ? "": ViewBag.ErrorMessage %>
        </span>
        <span style="color: black;">
            <%: ViewBag.Message == null ? "":ViewBag.Message %>
        </span>
    </div>

    <div class="clearfix"></div>


    <div class="text-end">
        <input class="btn btn-primary" type="button" id="save" value="Save" />
        <div style="clear: both;"></div>
    </div>

    <script>

        $('#voltValue').addClass('form-control');

        $(document).ready(function () {
            var lab = $('#label').val();
            $('.apDiv').hide();

            if ($('#APorEP_ManualChkBx').prop('checked')) {
                $('.apDiv').hide();
            } else {
                $('.apDiv').show();
            }

            $('#APorEP_ManualChkBx').change(function () {
                $('.apDiv').hide();
                if ($('#APorEP_ManualChkBx').prop('checked')) {
                    $('#APorEP_Manual').val(0);
                    $('.apDiv').hide();
                   
                } else {
                    $('#APorEP_Manual').val(1);
                    $('.apDiv').show();  
                }
            });

            $('.voltRow').hide();
            $('#epRow').hide();

            if (lab != "Ah") {
                $('.voltRow').show();
                if ($('#APorEP_ManualChkBx').prop('checked')) {
                    $('.voltRow2').hide();
                    $('.apDiv').hide();
                } else {
                    $('.voltRow2').show();
                    $('.apDiv').show();
                }
            }

            $('#label').change(function () {
                var lable = $('#label').val();

                if (lable != "Ah") {

                    $('.voltRow').show();
                    if ($('#APorEP_ManualChkBx').prop('checked')) {
                        $('.voltRow2').hide();
                        $('.apDiv').hide();
                    } else {
                        $('.voltRow2').show();
                        $('.apDiv').show();
                    }
                }
                else {
                    $('.voltRow').hide();
            }
            });



            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });
       });
    </script>
</form>
