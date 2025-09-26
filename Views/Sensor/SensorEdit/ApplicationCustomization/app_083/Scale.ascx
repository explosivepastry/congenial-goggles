<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>


<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/Overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />
    <%
        string label = PressureNPSI.GetLabel(Model.SensorID);
        double getSavedVal = PressureNPSI.GetSavedValue(Model.SensorID);
    %>

    <div class="formtitle">
        Transducer Scale 
    </div>
    <% if (Model.CanUpdate)
       { %>



    <div class="form-group">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_083|Max PSI Rating","Max PSI Rating")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input editFieldSmall" type="text" value="<%: getSavedVal %>"  id="MaxCapcity" name="MaxCapcity" />&nbsp; PSI<br />
             <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_083|WARNING: Transducer Max PSI Rating must match the value of the attached pressure transducer. Mismatched values will lead to inaccurate readings.")%>
        </div>
    </div>
    <div class="clear"></div>
    <br />

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
            <input class="aSettings__input_input" type="text" id="lowValue" name="lowValue" value="<%:Monnit.Pressure50PSI.GetLowValue(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>



    <div class="form-group hideMe" style="display:none;">
        <div class="aSettings__title">
            <label id="Label1"></label>
            PSI Value:
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input" type="text" id="highValue" name="highValue" value="<%:Monnit.Pressure50PSI.GetHighValue(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>

    <div class="form-group hideMe" style="display:none;">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Label:","Label:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input" type="text" id="customLabel" name="customLabel" value="<%:Monnit.Pressure50PSI.GetCustomLabel(Model.SensorID) %>" />
        </div>
        <div class="clear"></div>
        <br />
    </div>

    <div class="form-group hideMe" style="display:none;">
        <div class="aSettings__title">
            <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_079|Number of Decimals:","Number of Decimals:")%>
        </div>
        <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
            <input class="aSettings__input_input" type="text" id="decimalTrunkValue" name="decimalTrunkValue" value="<%:Monnit.PressureNPSI.GetDecimalTrunkValue(Model.SensorID) %>" />
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


    <div class="" style="display: flex; justify-content: flex-end;">
        <button class="gen-btn" type="button" id="save" value="<%: Html.TranslateTag("Save","Save")%>">
            <%: Html.TranslateTag("Save","Save")%>
        </button>
    </div>
        <div style="clear: both;"></div>

    <%}
       else
       { %>
    <div class="formBody" style="font-weight: bold">
        <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_002|Scale for this sensor is not available for edit until pending transaction is complete.","Scale for this sensor is not available for edit until pending transaction is complete.")%>
    </div>
    <div class="buttons">&nbsp; </div>
    <%} %>

    <script>
        $(document).ready(function () {
            

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
                else {
                    $(".hideMe").hide();
                }
            });

            $('#MaxCapcity').blur(function () {
                var hilbl = $('#MaxCapcity').val();
                $('#highvalLabel').text(hilbl);
                $('#highValue').val(hilbl);
            });

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
                });
            });
    </script>

</form>
