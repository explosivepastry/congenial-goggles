<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />
    <%
        string label = Pressure50PSI.GetLabel(Model.SensorID);
        double getSavedVal = Pressure50PSI.GetSavedValue(Model.SensorID);
    %>



    <div class="row sensorEditForm">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Scale","Scale")%>
        </div>
        <div class="col sensorEditFormInput">
            <select id="label" name="label" class="form-select">
                <option value="PSI" <%: label.Contains("PSI") || string.IsNullOrWhiteSpace(label) ? "selected":"" %>>PSI</option>
                <option value="atm" <%: label.Contains("atm") ? "selected":"" %>>atm</option>
                <option value="bar" <%: label.Contains("bar") ? "selected":"" %>>bar</option>
                <option value="kPA" <%: label.Contains("kPA") ? "selected":"" %>>kPA</option>
                <option value="Torr" <%: label.Contains("Torr") ? "selected":"" %>>Torr</option>
                <option value="Custom" <%: label.Contains("Custom") ? "selected":"" %>>Custom</option>
            </select>
        </div>
    </div>
    <div class="clear"></div>
    <br />

    <div class="row sensorEditForm hideMe" style="display:none;">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|0 PSI Value:","0 PSI Value:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="text" id="lowValue" name="lowValue" value="<%:Monnit.Pressure50PSI.GetLowValue(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>

    <div class="row sensorEditForm hideMe"style="display:none;">
        <div class="col-12 col-md-3">
            <label id="Label1"></label>
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|50 PSI Value:","50 PSI Value:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="text" id="highValue" name="highValue" value="<%:Monnit.Pressure50PSI.GetHighValue(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>

    <div class="row sensorEditForm hideMe" style="display:none;">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Label:","Label:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="text" id="customLabel" name="customLabel" value="<%:Monnit.Pressure50PSI.GetCustomLabel(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>

    <div class="row sensorEditForm hideMe" style="display:none;">
        <div class="col-12 col-md-3">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Number of Decimals:","Number of Decimals:")%>
        </div>
        <div class="col sensorEditFormInput">
            <input class="form-control" type="text" id="decimalTrunkValue" name="decimalTrunkValue" value="<%:Monnit.PressureNPSI.GetDecimalTrunkValue(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>

    <div class="clear"></div>
    <br />

    <div class="col-12">
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
        $(document).ready(function () {
            //$('.helpIcon').tipTip();
            //$('.decimalHelpIcon').tipTip();

            if ($('#label :selected').text() == "Custom") {

                $(".hideMe").show();
                var hilblVal = $('#MaxCapcity').val();
                $('#highvalLabel').text(hilblVal);
                $("#lowValue").addClass('editField editFieldMedium');
                $("#highValue").addClass('editField editFieldMedium');
                $("#label").addClass('editField editFieldMedium');

                $("#highValue").change(function () {
                    if (!isANumber($("#highValue").val()))
                        $("#highValue").val(hilblVal);
                });

                $("#lowValue").change(function () {
                    if (!isANumber($("#lowValue").val()))
                        $("#lowValue").val(0);
                });
            }

            $('#label').change(function () {

                if ($('#label :selected').text() == "Custom") {
                    $(".hideMe").show();
                    $('#highvalLabel').text(50);
                    $("#lowValue").addClass('editField editFieldMedium');
                    $("#highValue").addClass('editField editFieldMedium');
                    $("#label").addClass('editField editFieldMedium');

                    $("#highValue").change(function () {
                        if (!isANumber($("#highValue").val()))
                            $("#highValue").val(50);
                    });

                    $("#lowValue").change(function () {
                        if (!isANumber($("#lowValue").val()))
                            $("#lowValue").val(0);
                    });
                }
                else {
                    $(".hideMe").hide();
                }
            });

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });
        });
    </script>

</form>
