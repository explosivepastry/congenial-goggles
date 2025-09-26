<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />
    <%
        string label = Pressure750PSI.GetLabel(Model.SensorID);
        double getSavedVal = Pressure750PSI.GetSavedValue(Model.SensorID);
    %>
    <div class="form-group">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Scale","Scale")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <select id="label" name="label" class="editFieldSmall tzSelect">
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

    <div class="form-group hideMe" style="display:none;">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|0 PSI Value:","0 PSI Value:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input" type="text" id="lowValue" name="lowValue" value="<%:Monnit.Pressure750PSI.GetLowValue(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>

    <div class="form-group hideMe" style="display:none;">
        <div class="aSettings__title">
            <label id="Label1"></label>
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|750 PSI Value:","750 PSI Value:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input" type="text" id="highValue" name="highValue" value="<%:Monnit.Pressure750PSI.GetHighValue(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>
    <div class="form-group hideMe" style="display:none;">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Label:","Label:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input" type="text" id="customLabel" name="customLabel" value="<%:Monnit.Pressure750PSI.GetCustomLabel(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>
    <div class="form-group hideMe" style="display:none;">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Number of Decimals:","Number of Decimals:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input" type="text" id="decimalTrunkValue" name="decimalTrunkValue" value="<%:Monnit.Pressure750PSI.GetDecimalTrunkValue(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
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


    <div class="" style="text-align: right;">
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
                    $('#highvalLabel').text(300);
                    $("#lowValue").addClass('editField editFieldMedium');
                    $("#highValue").addClass('editField editFieldMedium');
                    $("#label").addClass('editField editFieldMedium');

                    $("#highValue").change(function () {
                        if (!isANumber($("#highValue").val()))
                            $("#highValue").val(300);
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
