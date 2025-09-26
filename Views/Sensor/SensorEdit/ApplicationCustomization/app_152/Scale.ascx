<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Monnit.Sensor>" %>
<form action="/Overview/SensorScale/<%:Model.SensorID %>" id="SensorScale_<%:Model.SensorID %>" method="post">
    <%: Html.ValidationSummary(false) %>
    <input type="hidden" value="/Overview/SensorScale/<%:Model.SensorID %>" name="returns" id="returns" />

    <%
        string label = LN2Level.GetLabel(Model.SensorID);
    %>

    <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_152/_TankHeight.ascx", Model);%>

    <div class="formBody">

        <div class="form-group">
            <div class="aSettings__title">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|Display Scale","Display Scale")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <select id="label" name="label" class="editFieldSmall tzSelect">
                    <option value="percentage" <%: label.Contains("percentage") ? "selected":"" %>><%:Html.TranslateTag("Level (%)") %></option>
                    <option value="Height" <%: label.Contains("Height") ? "selected":"" %>><%:Html.TranslateTag("Height") %></option>
                    <option value="Volume" <%: label.Contains("Volume") ? "selected":"" %>><%:Html.TranslateTag("Volume") %></option>
                    <option value="Custom" <%: label.Contains("Custom") ? "selected":"" %>><%:Html.TranslateTag("Custom") %></option>
                </select>
            </div>
        </div>

        <div class="clear"></div>
        <br />

        <div class="col sensorEditFormInput hideMe" style="display: none;">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|Empty Value:","Empty Value:")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <input class="form-control user-dets" type="text" id="lowValue" name="lowValue" value="<%:Monnit.LN2Level.GetLowValue(Model.SensorID) %>" />
            </div>
            <div class="clear"></div>
            <br />
        </div>
        <br />
        <div class="col sensorEditFormInput hideMe" style="display: none;">
            <div class="col-12 col-md-3">
                <label id="Label1"></label>
                Full Value:
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <input class="form-control user-dets" type="text" id="highValue" name="highValue" value="<%:Monnit.LN2Level.GetHighValue(Model.SensorID) %>" />
            </div>
            <div class="clear"></div>
            <br />
        </div>
        <br />
        <div class="col sensorEditFormInput hideMe" style="display: none;">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|Label:","Label:")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <input class="form-control user-dets" type="text" id="customLabel" name="customLabel" value="<%:Monnit.LN2Level.GetCustomLabel(Model.SensorID) %>" />
            </div>
            <div class="clear"></div>
            <br />
        </div>
        <br />
        <div class="col sensorEditFormInput hideMe" style="display: none;">
            <div class="col-12 col-md-3">
                <%: Html.TranslateTag("Sensor/ApplicationCustomization/app_152|Number of Decimals:","Number of Decimals:")%>
            </div>
            <div class="col-md-9 col-sm-9 col-xs-12 mdBox">
                <input class="form-control user-dets" type="text" id="decimalTrunkValue" name="decimalTrunkValue" value="<%:Monnit.LN2Level.GetDecimalTrunkValue(Model.SensorID) %>" />
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

        <div id="tankVolume">
            <%Html.RenderPartial("~/Views/Sensor/SensorEdit/ApplicationCustomization/app_152/_TankVolume.ascx", Model);%>            
        </div>

        <div class="" style="text-align: right;">
            <input class="btn btn-primary" type="button" id="save" value="Save" />
            <div style="clear: both;"></div>
        </div>

    </div>

    <script>
        $(document).ready(function () {
            //$('.helpIcon').tipTip();
            //$('.decimalHelpIcon').tipTip();

            if ($('#label :selected').text() == "Custom" || $('#label :selected').text() == '%') {
                $('#tankVolume').hide();
            }

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

                if ($('#label :selected').text() == "Custom")
                {
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
                else
                {
                    $(".hideMe").hide();
                }

                if ($('#label :selected').text() != "Volume" && $('#label :selected').text() != "Height")
                {
                    $('#tankVolume').hide();
                }
                else
                {
                    $('#tankVolume').show();
                }
            });

            $('#save').click(function () {
                postForm($('#SensorScale_<%: Model.SensorID%>'));
            });
        });
    </script>
</form>
